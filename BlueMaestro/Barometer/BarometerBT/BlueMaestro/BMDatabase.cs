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
        [XmlIgnore]
        public BMRecordCurrent _max, _min, _avg;

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

            BMRecordCurrent last = Records.LastOrDefault();
            if (last != null && last == record) //not changed - update 
            {
                Records[Records.Count-1] = record;
            }
            else //new record
            {
                if(last == null)
                {
                    last = record;
                    _max = new BMRecordCurrent(record);
                    _min = new BMRecordCurrent(record);
                    _avg = new BMRecordCurrent(record);
                }

                Records.Add(record);

                _max.Max(record);
                _min.Min(record);
            }

            return record;
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
            db.UpdateLimits();
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

        private void UpdateLimits()
        {
            foreach (BMRecordCurrent r in Records)
            {
                if (_max == null)
                    _max = new BMRecordCurrent(r);
                _max.Max(r);

                if (_min == null)
                    _min = new BMRecordCurrent(r);
                _min.Min(r);
            }
        }
    }
}
