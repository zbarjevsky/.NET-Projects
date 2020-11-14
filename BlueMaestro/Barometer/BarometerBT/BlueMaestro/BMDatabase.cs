using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BarometerBT.Bluetooth;

namespace BarometerBT.BlueMaestro
{
    public class BMDatabase
    {
        public List<BMRecordCurrent> Records { get; } = new List<BMRecordCurrent>();
       
        public BluetoothDevice Device { get; }

        public BMRecordCurrent _max, _min, _avg;

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
    }
}
