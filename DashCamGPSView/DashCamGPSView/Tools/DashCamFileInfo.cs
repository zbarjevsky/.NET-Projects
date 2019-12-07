using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GPSDataParser;
using NmeaParser.Nmea;
using NMEAParser;

namespace DashCamGPSView.Tools
{
    public enum GpsFileFormat
    {
        DuDuBell,
        Viofo,
        Unkn
    }

    public class DashCamFileInfo
    {
        public GpsFileFormat GpsFileFormat = GpsFileFormat.Unkn;

        public string FrontFileName, BackFileName, NmeaFileName;

        private List<GpsPointData> _gpsInfo = null;
        public List<GpsPointData> GpsInfo 
        {
            get 
            {
                if(_gpsInfo == null)
                {
                    if(GpsFileFormat == GpsFileFormat.DuDuBell)
                    {
                        if (File.Exists(NmeaFileName))
                            _gpsInfo = GpsPointData.Convert(NMEAParser.NMEAParser.ReadFile(NmeaFileName));
                    }
                    else if(GpsFileFormat == GpsFileFormat.Viofo)
                    {

                        _gpsInfo = GpsPointData.Convert(NovatekViofoGPSParser.Parser.ReadMP4FileGpsInfo(FrontFileName));
                    }

                    CalculateDelay(FileDate);
                }
                return _gpsInfo;
            } 
        }
        
        public DateTime FileDate { get; private set; } = DateTime.MinValue;
        
        public int TimeZone { get { return _iGpsTimeZoneHours; } }

        private double _dGpsDelaySeconds = 2.3;
        private int _iGpsTimeZoneHours = 0;

        public DashCamFileInfo(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            name = name.Substring(0, name.Length - 1);
            string dirParent = Path.GetDirectoryName(Path.GetDirectoryName(fileName));
            string dirF = Path.Combine(dirParent, "F");
            string dirR = Path.Combine(dirParent, "R");

            if (Directory.Exists(dirF) && Directory.Exists(dirR))
            {
                GpsFileFormat = GpsFileFormat.DuDuBell;
                FileDate = FromDuDuBellFileName(fileName);

                FrontFileName = Path.Combine(dirF, name + "F.MP4");
                if (!File.Exists(FrontFileName))
                    FrontFileName = null;
                NmeaFileName = Path.Combine(dirF, name + "F.NMEA");
                if (!File.Exists(NmeaFileName))
                    NmeaFileName = null;
                BackFileName = Path.Combine(dirR, name + "R.MP4");
                if (!File.Exists(BackFileName))
                    BackFileName = null;
            }
            else //different format
            {
                GpsFileFormat = GpsFileFormat.Viofo;
                FileDate = FromViofoFileName(fileName);

                FrontFileName = fileName;
            }
        }

        /// <summary>
        /// Calculate delay between first GPS FixTime and beginning of the file from filename (PARK191121-141124F.MP4)
        /// </summary>
        /// <param name="name"></param>
        private void CalculateDelay(DateTime fileDateTime)
        {
            if (_gpsInfo == null || _gpsInfo.Count == 0)
                return;

            //timezone correction 
            TimeSpan delta = fileDateTime - _gpsInfo[0].FixTime;
            _iGpsTimeZoneHours = (int)Math.Round(delta.TotalHours);
            delta -= TimeSpan.FromHours(_iGpsTimeZoneHours);

            _dGpsDelaySeconds = delta.TotalSeconds;
        }

        private DateTime FromViofoFileName(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            string date_time = fileName.Substring(0, 20); //"2019_0624_075333_296P"
            return DateTime.ParseExact(date_time, "yyyy_MMdd_HHmmss_fff", CultureInfo.InvariantCulture);
        }

        private DateTime FromDuDuBellFileName(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            string date_time = fileName.Substring(fileName.Length - 14, 13); //191121-141124
            return DateTime.ParseExact(date_time, "yyMMdd-HHmmss", CultureInfo.InvariantCulture);
        }

        internal string GetLocationInfoForTime(double totalSeconds)
        {
            GpsPointData inf = FindGpsInfo(totalSeconds);
            if (inf == null)
                return "No GPS info...";
            string info = "Time: " + inf.FixTime.AddHours(_iGpsTimeZoneHours).ToString("yyyy/MM/dd HH:mm:ss") + 
                ", " + new PointLatLng(inf.Latitude, inf.Longitude).ToString() + 
                ", Speed: " + inf.SpeedMph.ToString("0.0") + 
                ", Azimuth: " + inf.Course;
            return info;
        }

        internal GpsPointData FindGpsInfo(double elapsedSeconds)
        {
            if (_gpsInfo == null || _gpsInfo.Count == 0)
                return null;

            elapsedSeconds += _dGpsDelaySeconds; //correct for GPS delay

            GpsPointData first = _gpsInfo[0] as GpsPointData;
            foreach (GpsPointData i in _gpsInfo)
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
            string gps = _gpsInfo == null ? "No Data" : _gpsInfo.Count.ToString();
            return "N: " + name + ", GPS Data: " + gps;
        }
    }
}
