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
            if (!Map.ContainsKey(device.getAddress()))
                Map[device.getAddress()] = new BMDatabase(device);

            return Map[device.getAddress()].AddRecord(device, rssi, recordDate, data);
        }
    }
}
