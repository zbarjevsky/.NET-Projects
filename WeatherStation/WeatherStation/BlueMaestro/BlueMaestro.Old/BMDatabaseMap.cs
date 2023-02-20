using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MkZ.Bluetooth;

namespace BlueMaestro.Old
{
    public class BMDatabaseMap
    {
        public Action<BluetoothDevice> OnRecordAddedAction = (dev) => { /*MainWindow.UpdateAllAsync(dev, rec);*/ };

        private Dictionary<ulong, BMDatabase> _map { get; } = new Dictionary<ulong, BMDatabase>();


        public static readonly BMDatabaseMap INSTANCE = new BMDatabaseMap();

        //private constructor - to restrict instance
        private BMDatabaseMap() { }

        public bool Contains(ulong address) { return _map.ContainsKey(address); }
        public List<BMDatabase> Databases { get { return _map.Values.ToList(); } }
        public List<BMRecordCurrent> Records(ulong address) { return _map[address].Records; } 

        public BMDatabase this[ulong address]
        {
            get
            {
                if (_map.ContainsKey(address))
                    return _map[address];
                return null;
            }
        }

        public BMDatabase this[BluetoothDevice device]
        {
            get
            {
                if (_map.ContainsKey(device.Address))
                    return _map[device.Address];
                return null;
            }
        }


        public BMRecordCurrent AddRecord(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            if (!_map.ContainsKey(device.Address))
                _map[device.Address] = new BMDatabase(device);

               var record = _map[device.Address].AddRecord(device, rssi, recordDate, data);

            if (record.IsValid)
                 OnRecordAddedAction(device);

            return record;
        }

        public BMDatabase Merge(BMDatabase db)
        {
            if (!_map.ContainsKey(db.Device.Address))
            {
                _map[db.Device.Address] = db;
            }
            else
            {
                _map[db.Device.Address].Merge(db);
            }

            return _map[db.Device.Address];
        }

        public void Load()
        {
            const string PATTERN = "BMDatabase_*_Main.xml";
            List<string> files = new List<string>(Directory.GetFiles(BMDatabase.DataFolder, PATTERN));
            files.Sort();

            foreach (string file in files)
            {
                BMDatabase db = BMDatabase.Open(file);
                if(db != null)
                    Merge(db);
            }
        }

        public void Save()
        {
            foreach (BMDatabase db in _map.Values)
            {
                db.SaveMain();
            }
        }
    }
}
