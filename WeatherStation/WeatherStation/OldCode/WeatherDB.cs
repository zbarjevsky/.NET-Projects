using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MkZ.BlueMaestroLib;
using MkZ.Tools;
using MkZ.Weather.Utils;
using MkZ.RadexOneLib;

namespace MkZ.Weather
{
    public class WeatherDB_Old
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

        public void CopyFrom(WeatherDB_Old weatherDB)
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
}
