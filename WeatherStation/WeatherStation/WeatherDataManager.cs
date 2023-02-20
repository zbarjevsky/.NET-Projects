using MkZ.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MkZ.BlueMaestroLib;
using MkZ.RadexOneLib;
using MkZ.Bluetooth;

namespace MkZ.Weather
{
    public class WeatherDataManager
    {
        public BMDatabaseMap BMDatabaseMap = new BMDatabaseMap();
        public RadexOneDatabaseMap RadexOneDatabaseMap = new RadexOneDatabaseMap();

        public Action<BluetoothDevice, BMRecordCurrent> OnBMRecordAddedAction = (dev, rec) => { };
        public Action<RadexOneDeviceInfo, RadiationDataPoint> OnRORecordAddedAction = (dev, rec) => { };

        public static string DataFolder
        {
            get
            {
                return Log.DataFolder;
            }
        }

        private WeatherDataManager()
        {
            BMDatabaseMap.OnRecordAddedAction = (dev, rec) => OnBMRecordAddedAction(dev, rec);
            RadexOneDatabaseMap.OnRecordAddedAction = (dev, rec) => OnRORecordAddedAction(dev, rec);
        }

        public static WeatherDataManager INSTANCE { get; } = new WeatherDataManager();

        public RadiationDataPoint AddRadiationData(RadexOneDeviceInfo dev, RadiationDataPoint pt)
        {
            RadiationDataPoint data = RadexOneDatabaseMap.AddRecord(dev, pt.Date, pt, DataFolder);
            if (data.IsValid)
                OnRORecordAddedAction?.Invoke(dev, data);

            return data;
        }

        public BMRecordCurrent AddMeteorologicalData(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            return BMDatabaseMap.AddRecord(device, rssi, recordDate, data);
        }

        public List<RadiationDataPoint> GetRadiationDataPoints(RadexOneDeviceInfo dev, TimeSpan daysBack)
        {
            return RadexOneDatabaseMap.Records(dev, daysBack);
        }

        public void Load()
        {
            BMDatabaseMap.Clear();
            BMDatabaseMap.Load(DataFolder);

            RadexOneDatabaseMap.Clear();
            RadexOneDatabaseMap.Load(DataFolder);
        }

        public void Save()
        {
            BMDatabaseMap.Save(DataFolder);
            RadexOneDatabaseMap.Save(DataFolder);
        }

        public void SaveAsBackup()
        {
            BMDatabaseMap.SaveAsBackup(DataFolder);
            RadexOneDatabaseMap.SaveAsBackup(DataFolder);
        }

        //public void Copy(BMDatabase db)
        //{
        //    weatherDB.BMRecords.AddRange(db.Records);
        //}

        //public void Merge(List<RadiationDataPoint> points)
        //{
        //    weatherDB.Merge(points);
        //}

        //public void Merge(BMDatabase bMDatabase)
        //{
        //    weatherDB.Merge(bMDatabase);
        //}

        //public static string GenerateFileName()
        //{
        //    string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
        //    string fileName = string.Format("WeatherDatabase_V1_{0}.xml", date);
        //    fileName = Path.Combine(DataFolder, fileName);
        //    return fileName;
        //}

        //public void SaveAs(string fileName, double bucketIntervalInSec)
        //{
        //    XmlHelper.Save(fileName, weatherDB);
        //}
    }
}
