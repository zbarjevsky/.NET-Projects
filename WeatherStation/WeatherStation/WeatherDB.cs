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

        public List<RadiationDataPoint> GetLastRecords(TimeSpan interval)
        {
            List<RadiationDataPoint> records = new List<RadiationDataPoint>();
            if (RadiationDataPoints.Count < 2)
                return RadiationDataPoints;

            DateTime last = RadiationDataPoints.Last().Date;
            DateTime first = last - interval;

            for (int i = 0; i < RadiationDataPoints.Count; i++)
            {
                if (RadiationDataPoints[i].Date > first)
                    records.Add(RadiationDataPoints[i]);
            }

            return records;
        }

        public void CopyFrom(WeatherDB weatherDB)
        {
            Clear();

            //remove points with exactly same date
            RadiationDataPoints.AddRange(weatherDB.RadiationDataPoints.GroupBy(p => new {p.Date.Year, p.Date.Month, p.Date.Day, p.Date.Hour, p.Date.Minute, p.Date.Second })
                .Select(g => g.FirstOrDefault()).ToList());
            RadiationDataPoints.Sort((p1, p2) => p1.Date.CompareTo(p2.Date));

            //remove points with exactly same date
            BMRecords.AddRange(weatherDB.BMRecords.GroupBy(p => new { p.Date.Year, p.Date.Month, p.Date.Day, p.Date.Hour, p.Date.Minute, p.Date.Second })
                .Select(g => g.FirstOrDefault()).ToList());
            BMRecords.Sort((p1, p2) => p1.Date.CompareTo(p2.Date));
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

            weatherDB.CopyFrom(XmlHelper.Open<WeatherDB>(fileName));
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

        public static string GenerateFileName()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("WeatherDatabase_V1_{0}.xml", date);
            fileName = Path.Combine(DataFolder, fileName);
            return fileName;
        }

        public void SaveAs(string fileName, double bucketIntervalInSec)
        {
            XmlHelper.Save(fileName, weatherDB);
        }
    }
}
