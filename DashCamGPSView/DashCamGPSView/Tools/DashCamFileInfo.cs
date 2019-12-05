using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashCamGPSView.Tools
{
    public class DashCamFileInfo
    {
        public string FrontFileName, BackFileName, NmeaFileName;
        public readonly List<NmeaParser.Nmea.NmeaMessage> GpsInfo = null;

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
        }

        internal string GetLocationInfoForTime(double totalSeconds)
        {
            NmeaParser.Nmea.Rmc inf = FindGpsInfo(totalSeconds);
            if (inf == null)
                return "No GPS info...";
            string info = "Time: " + inf.FixTime.ToString("s") + ", Lon: " + inf.Longitude + ", Lat: " + inf.Latitude + ", Speed: " + inf.Speed + ", Azimuth: " + inf.Course;
            return info;
        }

        internal NmeaParser.Nmea.Rmc FindGpsInfo(double elapsedSeconds)
        {
            if (GpsInfo == null || GpsInfo.Count == 0)
                return null;

            NmeaParser.Nmea.Rmc first = GpsInfo[0] as NmeaParser.Nmea.Rmc;

            foreach (NmeaParser.Nmea.Rmc i in GpsInfo)
            {
                TimeSpan delta = i.FixTime - first.FixTime;
                if (delta.TotalSeconds >= elapsedSeconds)
                    return i;
            }
            return first;
        }
    }
}
