using System;
using System.Collections.Generic;
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
            BMDatabase db = XmlHelper.Open<BMDatabase>(fileName);
            return db;
        }

        public static BMDatabase Merge(BMDatabase db1, BMDatabase db2)
        {
            if (db1.Device.Name != db2.Device.Name)
                return null;

            BMDatabase db = new BMDatabase(db1.Device);
            if (db1.Records.Count == 0 && db2.Records.Count == 0)
                return db;
            if (db1.Records.Count == 0)
                return db2;
            if (db2.Records.Count == 0)
                return db1;

            if(db1.Records.Last().Date < db2.Records.First().Date)
            {
                db.Records.AddRange(db1.Records);
                db.Records.AddRange(db2.Records);
            }
            else
            {
                db.Records.AddRange(db2.Records);
                db.Records.AddRange(db1.Records);
            }
            return db;
        }

        public override string ToString()
        {
            return string.Format("Count({0}) -- {1}", Records.Count, Device);
        }
    }
}
