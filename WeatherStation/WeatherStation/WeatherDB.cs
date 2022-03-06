using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MkZ.BlueMaestroLib;
using MkZ.RadexOne;
using MkZ.Tools;
using MkZ.Weather.Utils;

namespace MkZ.Weather
{
    public class WeatherDB
    {
        public List<BMRecordCurrent> BMRecords = new List<BMRecordCurrent>(1024);
        public List<RadiationDataPoint> RadiationDataPoints = new List<RadiationDataPoint>(1024);

        public void Clear()
        {
            BMRecords.Clear();
            RadiationDataPoints.Clear();
        }

        public void Merge(List<RadiationDataPoint> points)
        {
            RadiationDataPoints = ListUtils<RadiationDataPoint>.Merge(RadiationDataPoints, points, 
                (p1, p2) => { return p1.Date.CompareTo(p2.Date); });
        }

        public void Merge(BMDatabase bMDatabase)
        {
            BMRecords = ListUtils<BMRecordCurrent>.Merge(BMRecords, bMDatabase.Records,
                (p1, p2) => { return p1.Date.CompareTo(p2.Date); });
        }
    }

    public class WeatherDataManager
    {
        public WeatherDB weatherDB = new WeatherDB();

        public static string DataFolder
        {
            get
            {
                return Log.DataFolder;
            }
        }

        private WeatherDataManager()
        {

        }

        public static WeatherDataManager INSTANCE { get; } = new WeatherDataManager();

        public void Load()
        {
            weatherDB.Clear();

            const string PATTERN = "WeatherDatabase_*_Main.xml";
            List<string> files = new List<string>(Directory.GetFiles(DataFolder, PATTERN));
            if(files == null || files.Count == 0)
            {
                return;
            }
            
            files.Sort();
            string fileName = files.Last();

            weatherDB = XmlHelper.Open<WeatherDB>(fileName);
        }

        public void Save()
        {
            string fileName = string.Format("WeatherDatabase_V1_Main.xml");
            fileName = Path.Combine(DataFolder, fileName);
            XmlHelper.Save(fileName, weatherDB);
        }

        public void Copy(BMDatabase db)
        {
            weatherDB.BMRecords.AddRange(db.Records);
        }

        public void Merge(List<RadiationDataPoint> points)
        {
            weatherDB.Merge(points);
        }

        public void Merge(BMDatabase bMDatabase)
        {
            weatherDB.Merge(bMDatabase);
        }
    }
}
