using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using NmeaParser.Nmea;

namespace DashCamGPSView.Tools
{
    public class DashCamFileInfo
    {
        public string FrontFileName, BackFileName, NmeaFileName;
        public readonly List<NmeaMessage> GpsInfo = null;
        public DateTime FileDate { get; private set; } = DateTime.MinValue;

        private double _dGpsDelaySeconds = 2.3;
        private int _iGpsTimeZoneHours = 0;

        public DashCamFileInfo(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            name = name.Substring(0, name.Length - 1);
            string dirParent = Path.GetDirectoryName(Path.GetDirectoryName(fileName));
            string dirF = Path.Combine(dirParent, "F");
            string dirR = Path.Combine(dirParent, "R");

            FrontFileName = Path.Combine(dirF, name + "F.MP4");
            if (!File.Exists(FrontFileName))
                FrontFileName = null;
            NmeaFileName = Path.Combine(dirF, name + "F.NMEA");
            if (!File.Exists(NmeaFileName))
                NmeaFileName = null;
            BackFileName = Path.Combine(dirR, name + "R.MP4");
            if (!File.Exists(BackFileName))
                BackFileName = null;
            if(File.Exists(NmeaFileName))
                GpsInfo = NMEAParser.NMEAParser.ReadFile(NmeaFileName);

            CalculateDelay(name);
        }

        /// <summary>
        /// Calculate delay between first GPS FixTime and beginning of the file from filename (PARK191121-141124F.MP4)
        /// </summary>
        /// <param name="name"></param>
        private void CalculateDelay(string name)
        {
            string date_time = name.Substring(name.Length - 13); //191121-141124
            FileDate = DateTime.ParseExact(date_time, "yyMMdd-HHmmss", CultureInfo.InvariantCulture);

            if (GpsInfo == null || GpsInfo.Count == 0)
                return;

            Rmc inf = GpsInfo[0] as Rmc;
            //timezone correction 
            TimeSpan delta = FileDate - inf.FixTime;
            _iGpsTimeZoneHours = (int)Math.Round(delta.TotalHours);
            delta -= TimeSpan.FromHours(_iGpsTimeZoneHours);

            _dGpsDelaySeconds = delta.TotalSeconds;
        }

        internal string GetLocationInfoForTime(double totalSeconds)
        {
            Rmc inf = FindGpsInfo(totalSeconds);
            if (inf == null)
                return "No GPS info...";
            string info = "Time: " + inf.FixTime.AddHours(_iGpsTimeZoneHours).ToString("yyyy/MM/dd HH:mm:ss") + 
                ", " + new PointLatLng(inf.Latitude, inf.Longitude).ToString() + 
                ", Speed: " + inf.Speed + 
                ", Azimuth: " + inf.Course;
            return info;
        }

        internal Rmc FindGpsInfo(double elapsedSeconds)
        {
            if (GpsInfo == null || GpsInfo.Count == 0)
                return null;

            elapsedSeconds += _dGpsDelaySeconds; //correct for GPS delay

            Rmc first = GpsInfo[0] as Rmc;
            foreach (Rmc i in GpsInfo)
            {
                TimeSpan delta = i.FixTime - first.FixTime;
                if (delta.TotalSeconds >= (elapsedSeconds))
                    return i;
            }
            return first;
        }

        public override string ToString()
        {
            string name = Path.GetFileName(FrontFileName);
            string gps = GpsInfo == null ? "No Data" : GpsInfo.Count.ToString();
            return "N: " + name + ", GPS Data: " + gps;
        }
    }
}
