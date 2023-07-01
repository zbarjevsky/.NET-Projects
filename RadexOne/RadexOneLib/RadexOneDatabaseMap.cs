using MkZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.RadexOneLib
{
    public class RadexOneDatabaseMap
    {
        private Dictionary<string, RadexOneDeviceLog> _map { get; } = new Dictionary<string, RadexOneDeviceLog>();

        public Action<RadexOneDeviceInfo, RadiationDataPoint> OnRecordAddedAction = (dev, rec) => { };

        public bool Contains(string serialNumber) { return _map.ContainsKey(serialNumber); }
        public List<RadexOneDeviceLog> Databases { get { return _map.Values.ToList(); } }
        public List<RadiationDataPoint> Records(RadexOneDeviceInfo dev, TimeSpan daysBack) 
        {
            if(_map.ContainsKey(dev.SerialNumber.ToString()))
                return _map[dev.SerialNumber.ToString()]?.GetLastRecords(daysBack);
            return null;
        }

        public RadexOneDeviceLog this[string serialNumber]
        {
            get
            {
                if (_map.ContainsKey(serialNumber))
                    return _map[serialNumber];
                return null;
            }
        }

        public void Clear()
        {
            _map.Clear();
        }

        public RadiationDataPoint AddRecord(RadexOneDeviceInfo device, DateTime recordDate, RadiationDataPoint data, string dataFolder)
        {
            if (device == null || device.SerialNumber == null)
                return null;

            if (!_map.ContainsKey(device.SerialNumber.ToString()))
                _map[device.SerialNumber.ToString()] = new RadexOneDeviceLog() { RadexOneDevice = device };

            RadiationDataPoint record = _map[device.SerialNumber.ToString()].AddRecord(device, data, dataFolder);

            //if (record.IsValid)
            //    OnRecordAddedAction?.Invoke(device, record);

            return record;
        }

        public RadexOneDeviceLog Merge(RadexOneDeviceLog db)
        {
            if (!_map.ContainsKey(db.RadexOneDevice.SerialNumber.ToString()))
            {
                _map[db.RadexOneDevice.SerialNumber.ToString()] = db;
            }
            else
            {
                _map[db.RadexOneDevice.SerialNumber.ToString()].Merge(db);
            }

            return _map[db.RadexOneDevice.SerialNumber.ToString()];
        }

        public void Merge(List<RadiationDataPoint> bMRecords, RadexOneDeviceInfo dev)
        {
            if (bMRecords == null || bMRecords.Count == 0)
                return;

            RadexOneDeviceLog db = new RadexOneDeviceLog()
            {
                RadexOneDevice = dev
            };
            db.RadiationData.AddRange(bMRecords);

            Merge(db);
        }

        public void Load(string dataFolder)
        {
            string PATTERN = string.Format(RadexOneDeviceLog.FILE_NAME_MAIN, "*");
            List<string> files = new List<string>(System.IO.Directory.GetFiles(dataFolder, PATTERN));
            files.Sort();

            foreach (string file in files)
            {
                RadexOneDeviceLog db = RadexOneDeviceLog.Open(file);
                if (db != null)
                    Merge(db);
            }
        }

        public void Save(string dataFolder)
        {
            foreach (RadexOneDeviceLog db in _map.Values)
            {
                db.SaveMain(dataFolder);
            }
        }

        public void SaveAsBackup(string dataFolder)
        {
            foreach (RadexOneDeviceLog db in _map.Values)
            {
                db.SaveBackup(dataFolder);
            }
        }
    }
}
