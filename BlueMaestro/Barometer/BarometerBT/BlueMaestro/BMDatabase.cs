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
        public const int MIN_RECORDS_TO_FILTER = 1000;

        public BluetoothDevice Device { get; set; }

        public List<BMRecordCurrent> Records { get; } = new List<BMRecordCurrent>();

        [XmlIgnore]
        public UnitsDescriptor Units { get; set; } = new UnitsDescriptor();

        public static string DataFolder
        {
            get
            {
                return Log.DataFolder;
            }
        }

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

        public List<BMRecordCurrent> GetLastRecords(TimeSpan interval)
        {
            DateTime last = Records.Last().Date;
            DateTime first = last - interval;

            List<BMRecordCurrent> records = new List<BMRecordCurrent>();
            for (int i = 0; i < Records.Count; i++)
            {
                if (Records[i].Date > first)
                    records.Add(Records[i]);
            }

            return records;
        }

        public List<BMRecordCurrent> DilluteByPointAndConvertUnits(List<BMRecordCurrent> recordsIn, int zoom)
        {
            lock (recordsIn)
            {
                int bucketSize = (int)(zoom);
                if (bucketSize < 2 || bucketSize > (recordsIn.Count / 3)) //all records
                    return new List<BMRecordCurrent>(recordsIn);

                DateTime first = recordsIn.First().Date;
                DateTime last = recordsIn.Last().Date;
                TimeSpan interval = last - first;

                List<BMRecordCurrent> recordsOut = new List<BMRecordCurrent>();
                for (int i = 0; i < recordsIn.Count - 1; i += bucketSize)
                {
                    recordsOut.Add(GetAverageValue(recordsIn, i, bucketSize));
                }

                //anyway add last record as is
                recordsOut.Add(new BMRecordCurrent(recordsIn.Last()));

                return recordsOut;
            }
        }

        public List<BMRecordCurrent> DilluteByTimeAndConvertUnits(List<BMRecordCurrent> recordsIn, double bucketIntervalInSec) //default 15 min
        {
            lock (recordsIn)
            {
                if (recordsIn.Count < MIN_RECORDS_TO_FILTER || bucketIntervalInSec <= 1.0) //return all records, convert units
                    return new List<BMRecordCurrent>(recordsIn.Select(r => ConvertUnitsCurr(new BMRecordCurrent(r))));

                DateTime first = recordsIn.First().Date;
                DateTime last = recordsIn.Last().Date;
                TimeSpan interval = last - first;

                int i = 0;
                int bucketStart = i;
                int bucketIndex = 0;

                List<BMRecordCurrent> recordsOut = new List<BMRecordCurrent>();

                for (; i < recordsIn.Count - 1; i++)
                {
                    double secondsFromFirst = (recordsIn[i].Date - first).TotalSeconds;
                    int idx = (int)(secondsFromFirst / bucketIntervalInSec);
                    if (idx > bucketIndex) //next bucket
                    {
                        recordsOut.Add(ConvertUnitsCurr(GetAverageValue(recordsIn, bucketStart, i - bucketStart)));
                        bucketStart = i;
                        bucketIndex = idx;
                    }
                }

                //last bucket - always add last record as is
                if (bucketStart < recordsIn.Count - 2)
                {
                    for (int j = bucketStart; j < recordsIn.Count; j++)
                    {
                        recordsOut.Add(ConvertUnitsCurr(new BMRecordCurrent(recordsIn[j])));
                    }
                }
                else
                {
                    recordsOut.Add(ConvertUnitsCurr(new BMRecordCurrent(recordsIn.Last())));
                }

                return recordsOut;
            }
        }

        private static BMRecordCurrent GetAverageValue(List<BMRecordCurrent> records, int start, int bucketSize)
        {
            if (start >= records.Count || bucketSize <= 0)
                return new BMRecordCurrent();

            BMRecordCurrent rec = new BMRecordCurrent(records[start]);
            if (bucketSize == 1)
                return rec;

            int count = 1;
            for (int i = start + 1; i < records.Count && i < (start + bucketSize); i++, count++)
            {
                rec += records[i];
            }
            rec /= count;

            TimeSpan interval = records[start + count - 1].Date - records[start].Date;
            TimeSpan halfInterval = TimeSpan.FromMilliseconds(interval.TotalMilliseconds / 2);
            rec.Date = records[start].Date + halfInterval;

            if(interval.TotalSeconds < 1.0)
            {
                Log.d("Interval is too small: idx: {0} count: {1} time: {2}", start, bucketSize, interval);
            }

            return rec;
        }

        public void SaveBackupWithDate()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("BMDatabase_{0}_{1}_{2}.xml", Device.Name, Device.Address, date);
            fileName = Path.Combine(DataFolder, fileName);

            Save(fileName);
        }

        public void SaveMain()
        {
            string fileName = string.Format("BMDatabase_{0}_{1}_Main.xml", Device.Name, Device.Address);
            fileName = Path.Combine(DataFolder, fileName);

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
                Log.e("File: {0} Error: {1}", fileName, err);
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
