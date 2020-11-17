using BarometerBT.Bluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.BlueMaestro
{
    public class BMDatabaseMap
    {
        public Dictionary<ulong, BMDatabase> Map { get; } = new Dictionary<ulong, BMDatabase>();

        public List<BMRecordCurrent> Records(ulong address) { return Map[address].Records; } 

        public static readonly BMDatabaseMap INSTANCE = new BMDatabaseMap();

        //private constructor - to restrict instance
        private BMDatabaseMap() { }

        public BMRecordCurrent AddRecord(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            if (!Map.ContainsKey(device.Address))
                Map[device.Address] = new BMDatabase(device);

            return Map[device.Address].AddRecord(device, rssi, recordDate, data);
        }

        public BMDatabase Merge(BMDatabase db)
        {
            if (!Map.ContainsKey(db.Device.Address))
            {
                Map[db.Device.Address] = db;
            }
            else
            {
                Map[db.Device.Address].Merge(db);
            }

            return Map[db.Device.Address];
        }
    }
}
