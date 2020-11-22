using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BarometerBT.Bluetooth;
using BarometerBT.Utils;

namespace BarometerBT.BlueMaestro
{
    public class BMDatabase
    {
        public BluetoothDevice Device { get; set; }

        public List<BMRecordCurrent> Records { get; } = new List<BMRecordCurrent>();

        [XmlIgnore]
        public UnitsDescriptor Units { get; set; } = new UnitsDescriptor();

        //for serialization
        public BMDatabase()
        {

        }
       
        public BMDatabase(BluetoothDevice device)
        {
            Device = device;
        }

        public BMRecordCurrent AddRecord(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            BMRecordCurrent record = new BMRecordCurrent(device, rssi, recordDate, data);

            lock (Records)
            {
                BMRecordCurrent last = Records.LastOrDefault();
                if (last != null && last == record) //not changed - update 
                {
                    Records[Records.Count - 1] = record;
                }
                else //new record
                {
                    if (last == null)
                    {
                        last = record;
                    }

                    Records.Add(record);
                }
            }

            return record;
        }

        public void Merge(BMDatabase db)
        {
            lock (Records)
            {
                Records.AddRange(db.Records);
                Records.Sort((r1, r2) => r1.Date.CompareTo(r2.Date));

                //remove duplicates
                for (int i = Records.Count - 1; i > 0; i--)
                {
                    if (Records[i].Date == Records[i - 1].Date)
                        Records.RemoveAt(i);
                }
            }
        }

        public List<BMRecordCurrent> DilluteByPointAndConvertUnits(int zoom)
        {
            lock (Records)
            {
                int bucketSize = (int)(zoom);
                if (bucketSize < 2 || bucketSize > (Records.Count / 3)) //all records
                    return new List<BMRecordCurrent>(Records);

                List<BMRecordCurrent> records = new List<BMRecordCurrent>();
                for (int i = 0; i < Records.Count - 1; i += bucketSize)
                {
                    records.Add(GetAverageValue(i, bucketSize));
                }

                //anyway add last record as is
                records.Add(new BMRecordCurrent(Records.Last()));

                return records;
            }
        }

        public List<BMRecordCurrent> DilluteByTimeAndConvertUnits(double combineIntervalInSec = 900) //default 15 min
        {
            lock (Records)
            {
                if (Records.Count < 1000) //return all records, convert units
                    return new List<BMRecordCurrent>(Records.Select(r => ConvertUnitsCurr(new BMRecordCurrent(r))));

                DateTime first = Records.First().Date;
                DateTime last = Records.Last().Date;
                TimeSpan interval = last - first;

                int bucketStart = 0;
                int bucketIndex = 0;

                List<BMRecordCurrent> records = new List<BMRecordCurrent>();

                int i = 0;
                for (; i < Records.Count - 1; i++)
                {
                    double secondsFromFirst = (Records[i].Date - first).TotalSeconds;
                    int idx = (int)(secondsFromFirst / combineIntervalInSec);
                    if (idx > bucketIndex)
                    {
                        records.Add(ConvertUnitsCurr(GetAverageValue(bucketStart, i - bucketStart)));
                        bucketStart = i;
                        bucketIndex = idx;
                    }
                }

                //last bucket
                records.Add(ConvertUnitsCurr(GetAverageValue(bucketStart, i - bucketStart)));

                //always add last record as is
                records.Add(ConvertUnitsCurr(new BMRecordCurrent(Records.Last())));

                return records;
            }
        }

        private BMRecordCurrent GetAverageValue(int start, int bucketSize)
        {
            if (start >= Records.Count)
                return new BMRecordCurrent();

            BMRecordCurrent rec = new BMRecordCurrent(Records[start]);

            int count = 1;
            for (int i = start + 1; i < Records.Count && i < (start + bucketSize); i++, count++)
            {
                rec += Records[i];
            }
            rec /= count;

            TimeSpan interval = Records[start + count - 1].Date - Records[start].Date;
            TimeSpan halfInterval = TimeSpan.FromMilliseconds(interval.TotalMilliseconds / 2);
            rec.Date = Records[start].Date + halfInterval;

            return rec;
        }

        public void Save()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("BMDatabase_{0}_{1}_{2}.xml", Device.Name, Device.Address, date);
            string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string dataFolder = Path.Combine(commonPath, "MarkZ", "BarometerBT");
            Directory.CreateDirectory(dataFolder);
            
            fileName = Path.Combine(dataFolder, fileName);
            
            Save(fileName);
        }

        public void Save(string fileName)
        {
            XmlHelper.Save(fileName, this);
        }

        public static BMDatabase Open(string fileName)
        {
            try
            {
                BMDatabase db = XmlHelper.Open<BMDatabase>(fileName);
                if (db.Device == null)
                    throw new Exception("Old file format");
                    
                return db;
            }
            catch (Exception err)
            {
                Debug.WriteLine("File: {0} Error: {1}", fileName, err);
                return null;
            }
        }

        public BMRecordBase ConvertUnits(BMRecordBase r)
        {
            r.Temperature = this.Units.ConvertTemperature(r.Temperature);
            r.AirPressure = this.Units.ConvertPressure(r.AirPressure);
            return r;
        }

        public BMRecordCurrent ConvertUnitsCurr(BMRecordCurrent r)
        {
            return ConvertUnits(r) as BMRecordCurrent;
        }

        public override string ToString()
        {
            return string.Format("Count({0}) -- {1}", Records.Count, Device);
        }
    }
}
