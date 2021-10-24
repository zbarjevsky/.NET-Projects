using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public enum FileType : int
    {
        Unknown = 0,
        Recording,
        Parking,
        ReadOnly,
    }

    public class FileInfoWithDateFromFileName
    {
        public FileInfo Info { get; }
        public DateTime Date { get; }

        public FileInfoWithDateFromFileName(string fileName)
        {
            Info = new FileInfo(fileName);

            fileName = Path.GetFileNameWithoutExtension(fileName);

            if (fileName.Length >= 16 && fileName.Length <= 22)
            {
                string date_time = fileName.Substring(0, 16); //"2019_0624_075333_296P"
                Date = DateTime.ParseExact(date_time, "yyyy_MMdd_HHmmss", CultureInfo.InvariantCulture);
            }
            else if (fileName.Length == 15)
            {
                string date_time = fileName.Substring(0, 15); //"20190624_075333"
                Date = DateTime.ParseExact(date_time, "yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            }
            else
            {
                Date = DateTime.MinValue;
            }
        }

        public override string ToString()
        {
            return Date + " " + Info.FullName;
        }
    }

    public class DashCamFileInfo
    {
        public GpsFileFormat GpsFileFormat = GpsFileFormat.Unkn;

        public FileType FileType = FileType.Unknown;

        public string FileNameFront, FileNameRear, FileNameInside, FileNameNmea;

        private List<GpsPointData> _gpsInfo = new List<GpsPointData>();
        public List<GpsPointData> GpsInfo
        {
            get
            {
                InitGpsInfo();
                return _gpsInfo;
            }
        }

        public bool HasGpsInfo
        {
            get
            {
                InitGpsInfo();
                return _gpsInfo != null && _gpsInfo.Count > 0;
            }
        }

        public GpsPointData this[int index] { get { return GpsInfo[index]; } }

        public PointLatLng Position(int i) { return new PointLatLng(this[i].Latitude, this[i].Longitude); }

        public SpeedUnits SpeedUnits { get; set; } = SpeedUnits.mph;

        public double GetSpeed(int idx) { return this[idx].GetSpeed(SpeedUnits); }

        public DateTime FileDate { get; private set; } = DateTime.MinValue;
        
        public int TimeZone { get { return _iGpsTimeZoneHours; } }

        private double _dGpsDelaySeconds = 2.3;
        private int _iGpsTimeZoneHours = 0;

        public DashCamFileInfo(List<FileInfoWithDateFromFileName> allFiles, FileInfoWithDateFromFileName currentInfo, string speedUnits)
        {
            string fileName = currentInfo.Info.FullName;
            SpeedUnits = (SpeedUnits)Enum.Parse(typeof(SpeedUnits), speedUnits);

            string name = Path.GetFileNameWithoutExtension(fileName);
            name = name.Substring(0, name.Length - 1);
            string dirParent = Path.GetDirectoryName(Path.GetDirectoryName(fileName));
            string dirF = "", dirR = "";
            if (!string.IsNullOrWhiteSpace(dirParent))
            {
                dirF = Path.Combine(dirParent, "F");
                dirR = Path.Combine(dirParent, "R");
            }

            if (Directory.Exists(dirF) && Directory.Exists(dirR))
            {
                GpsFileFormat = GpsFileFormat.DuDuBell;
                FileType = FileType.Recording;
                FileDate = FromDuDuBellFileName(fileName);

                FileNameFront = Path.Combine(dirF, name + "F.MP4");
                if (!File.Exists(FileNameFront))
                    FileNameFront = null;
                FileNameNmea = Path.Combine(dirF, name + "F.NMEA");
                if (!File.Exists(FileNameNmea))
                    FileNameNmea = null;
                FileNameRear = Path.Combine(dirR, name + "R.MP4");
                if (!File.Exists(FileNameRear))
                    FileNameRear = null;
            }
            else //different format
            {
                GpsFileFormat = GpsFileFormat.Viofo;
                FileDate = currentInfo.Date;
                if(!FromViofoFileName(allFiles, currentInfo, ref FileType, ref FileNameFront, ref FileNameRear, ref FileNameInside))
                    FileNameFront = currentInfo.Info.FullName;
            }
        }

        public DashCamFileInfo(DashCamFileInfo source, string speedUnits)
        {
            SpeedUnits = (SpeedUnits)Enum.Parse(typeof(SpeedUnits), speedUnits);
            GpsFileFormat = source.GpsFileFormat;

            FileNameFront = source.FileNameFront;
            FileNameRear = source.FileNameRear;
            FileNameInside = source.FileNameInside;
            FileNameNmea = source.FileNameNmea;

            FileDate = source.FileDate;

            _iGpsTimeZoneHours = source._iGpsTimeZoneHours;
            _isGpsInfoInitialized = source._isGpsInfoInitialized;
            _gpsInfo = source._gpsInfo;
        }

        private bool _isGpsInfoInitialized = false;
        private void InitGpsInfo()
        {
            if (!_isGpsInfoInitialized)
            {
                if (GpsFileFormat == GpsFileFormat.DuDuBell)
                {
                    if (File.Exists(FileNameNmea))
                        _gpsInfo = GpsPointData.Convert(NMEAParser.NMEAParser.ReadFile(FileNameNmea));
                }
                else if (GpsFileFormat == GpsFileFormat.Viofo)
                {
                    _gpsInfo = GpsPointData.Convert(NovatekViofoGPSParser.ViofoParser.ReadMP4FileGpsInfo(FileNameFront));
                }
                else
                {
                    _gpsInfo = new List<GpsPointData>(); //empty
                }

                CalculateDelay(FileDate);

                _isGpsInfoInitialized = true;
            }
        }

        internal void DeleteRecording()
        {
            try
            {
                if (File.Exists(FileNameFront))
                    File.Delete(FileNameFront);
                if (File.Exists(FileNameNmea))
                    File.Delete(FileNameNmea);
                if (File.Exists(FileNameRear))
                    File.Delete(FileNameRear);
                if (File.Exists(FileNameInside))
                    File.Delete(FileNameInside);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Exception deleting files: " + err);
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

        private bool FromViofoFileName(List<FileInfoWithDateFromFileName> allFiles, FileInfoWithDateFromFileName currentInfo,
            ref FileType fileType, ref string frontFileName, ref string rearFileName, ref string insideFileName)
        {
            frontFileName = "";
            rearFileName = "";
            insideFileName = "";

            foreach (FileInfoWithDateFromFileName info in allFiles)
            {
                double delta = Math.Abs((info.Date - currentInfo.Date).TotalSeconds);
                if (delta < 3)
                {
                    if (info.Info.Name.EndsWith("_F.MP4", true, CultureInfo.InvariantCulture))
                    {
                        frontFileName = info.Info.FullName;
                        FileType = FileType.Recording;
                    }
                    else if (info.Info.Name.EndsWith("_R.MP4", true, CultureInfo.InvariantCulture))
                    {
                        rearFileName = info.Info.FullName;
                        FileType = FileType.Recording;
                    }
                    else if (info.Info.Name.EndsWith("_I.MP4", true, CultureInfo.InvariantCulture))
                    {
                        insideFileName = info.Info.FullName;
                        FileType = FileType.Recording;
                    }
                    else if (info.Info.Name.EndsWith("_PF.MP4", true, CultureInfo.InvariantCulture))
                    {
                        frontFileName = info.Info.FullName;
                        FileType = FileType.Parking;
                    }
                    else if (info.Info.Name.EndsWith("_PR.MP4", true, CultureInfo.InvariantCulture))
                    {
                        rearFileName = info.Info.FullName;
                        FileType = FileType.Parking;
                    }
                    else if (info.Info.Name.EndsWith("_PI.MP4", true, CultureInfo.InvariantCulture))
                    {
                        insideFileName = info.Info.FullName;
                        FileType = FileType.Parking;
                    }

                    if (info.Info.IsReadOnly)
                        FileType = FileType.ReadOnly;
                }
            }

            //check for simple file name
            //for one camera only
            string name = currentInfo.Info.FullName;
            if (string.Compare(name, frontFileName, true) != 0 &&
                string.Compare(name, rearFileName, true) != 0 &&
                string.Compare(name, insideFileName, true) != 0 )
            {
                frontFileName = name;
                rearFileName = "";
                insideFileName = "";
            }

            return !string.IsNullOrWhiteSpace(frontFileName);
        }

        private DateTime FromDuDuBellFileName(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            string date_time = fileName.Substring(fileName.Length - 14, 13); //191121-141124
            return DateTime.ParseExact(date_time, "yyMMdd-HHmmss", CultureInfo.InvariantCulture);
        }

        internal string GetLocationInfoForTime(double elapsedSeconds, double totalSeconds)
        {
            int idx = FindGpsInfoIdx(elapsedSeconds, totalSeconds);
            if (idx < 0)
                return "No GPS info...";

            string info = "Time: " + GpsInfo[idx].FixTime.AddHours(_iGpsTimeZoneHours).ToString("yyyy/MM/dd HH:mm:ss") + 
                ", " + new GMap.NET.PointLatLng(GpsInfo[idx].Latitude, GpsInfo[idx].Longitude).ToString() + 
                ", Speed: " + GpsInfo[idx].SpeedMph.ToString("0.0 mph") + 
                ", Azimuth: " + GpsInfo[idx].Course.ToString("0.0");
            return info;
        }

        internal int FindGpsInfoIdx(double elapsedSeconds, double totalSeconds)
        {
            if (_gpsInfo == null || _gpsInfo.Count == 0 || totalSeconds == 0)
                return -1;

            elapsedSeconds += _dGpsDelaySeconds; //correct for GPS delay

            DateTime start = _gpsInfo.First().FixTime;
            TimeSpan duration = (_gpsInfo.Last().FixTime - start);

            for (int i = 0; i < _gpsInfo.Count; i++)
            {
                TimeSpan delta = _gpsInfo[i].FixTime - start;
                if (delta.TotalSeconds >= (elapsedSeconds))
                    return i;
            }
            return _gpsInfo.Count - 1; //last index
        }

        public static string GetScreenshotFileName(GpsFileFormat format, string videoFileName)
        {
            string dirParent = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (format == GpsFileFormat.DuDuBell)
                dirParent = Path.Combine(dirParent, "DuDuBell");
            else //if(GpsFileFormat == GpsFileFormat.Viofo)
                dirParent = Path.Combine(dirParent, "DashcamScreenshots");

            Directory.CreateDirectory(dirParent);

            string fileName = Path.GetFileNameWithoutExtension(videoFileName);
            fileName = Path.Combine(dirParent, fileName);

            return fileName;
        }

        public override string ToString()
        {
            string name = Path.GetFileName(FileNameFront);
            string gps = _gpsInfo == null ? "No Data" : _gpsInfo.Count.ToString();
            return "N: " + name + ", GPS Data: " + gps;
        }
    }
}
