using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DashCam.Tools;
using GMap.NET;
using GPSDataParser;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
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

    public enum RecordingType : int
    {
        Unknown = 0,
        Driving,
        Parking,
    }

    public class FileInfoWithDateFromFileName
    {
        public FileInfo Info { get; private set; }
        public DateTime Date { get; private set; }

        public FileInfoWithDateFromFileName(string fileName)
        {
            Create(fileName);
        }

        public void Create(string fileName)
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
                Date = Info.CreationTime;
            }
        }

        public void CopyFrom(FileInfoWithDateFromFileName info)
        {
            Info = new FileInfo(info.Info.FullName);
            Date = info.Date;
        }

        public void MoveInfo(params string [] newFileNames)
        {
            foreach (string newFileName in newFileNames)
            {
                if(File.Exists(newFileName))
                {
                    Create(newFileName);
                    break;
                }
            }
        }

        public override string ToString()
        {
            return $"File: {Date:s} - Created: {Info.CreationTime:s} - End: {Info.LastWriteTime:s} - Name: {Info.Name}";
        }
    }

    public class DashCamFileInfo
    {
        public GpsFileFormat GpsFileFormat = GpsFileFormat.Unkn;

        public RecordingType RecordingType = RecordingType.Unknown;

        public bool IsProtected { get; private set; } = false;

        public string FileNameFront = "", FileNameRear = "", FileNameInside = "", FileNameNmea = "";

        public string FileName => Info.Info.FullName;

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

        public PointLatLngUI Position(int i) { return new PointLatLngUI(this[i].Latitude, this[i].Longitude); }

        public SpeedUnits SpeedUnits { get; set; } = SpeedUnits.mph;

        public double GetSpeed(int idx) { return this[idx].GetSpeed(SpeedUnits); }

        public DateTime FileDateStart { get; private set; } = new DateTime(1970, 1, 1);

        public DateTime FileDateEnd { get; private set; } = new DateTime(1970, 1, 1);

        public FileInfoWithDateFromFileName Info { get; private set; } = null;

        public int TimeZone { get { return _iGpsTimeZoneHours; } }

        private double _dGpsDelaySeconds = 1;
        private int _iGpsTimeZoneHours = 0;

        public DashCamFileInfo(List<FileInfoWithDateFromFileName> allFiles, ref int idx, string speedUnits)
        {
            FileInfoWithDateFromFileName currentInfo = allFiles[idx];

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
                RecordingType = RecordingType.Driving;
                IsProtected = false;
                Info = currentInfo;
                FileDateStart = FromDuDuBellFileName(fileName);
                FileDateEnd = currentInfo.Info.LastWriteTime;

                FileNameFront = Path.Combine(dirF, name + "F.MP4");
                if (!File.Exists(FileNameFront))
                    FileNameFront = null;
                FileNameNmea = Path.Combine(dirF, name + "F.NMEA");
                if (!File.Exists(FileNameNmea))
                    FileNameNmea = null;
                FileNameRear = Path.Combine(dirR, name + "R.MP4");
                if (!File.Exists(FileNameRear))
                    FileNameRear = null;

                idx++;
            }
            else //viofo 2ch or 3ch format
            {
                GpsFileFormat = GpsFileFormat.Viofo;
                FileDateStart = currentInfo.Date;
                FileDateEnd = currentInfo.Info.LastWriteTime;
                if (!FromViofoFileName(allFiles, ref idx, ref RecordingType, ref FileNameFront, ref FileNameRear, ref FileNameInside))
                {
                    FileNameFront = currentInfo.Info.FullName;
                    Info = currentInfo;
                    idx++;
                }

                string dir = Path.GetDirectoryName(Info.Info.FullName);
                IsProtected = dir.EndsWith("RO");
            }
        }

        public DashCamFileInfo(DashCamFileInfo source, string speedUnits)
        {
            SpeedUnits = (SpeedUnits)Enum.Parse(typeof(SpeedUnits), speedUnits);
            GpsFileFormat = source.GpsFileFormat;

            Info = source.Info;
            IsProtected = source.IsProtected;

            FileNameFront = source.FileNameFront;
            FileNameRear = source.FileNameRear;
            FileNameInside = source.FileNameInside;
            FileNameNmea = source.FileNameNmea;

            FileDateStart = source.FileDateStart;
            FileDateEnd = source.FileDateEnd;

            _iGpsTimeZoneHours = source._iGpsTimeZoneHours;
            _dGpsDelaySeconds = source._dGpsDelaySeconds;
            _isGpsInfoInitialized = source._isGpsInfoInitialized;
            _gpsInfo = source._gpsInfo;
        }

        public TimeSpan Duration
        {
            get
            {
                using (ShellObject shell = ShellObject.FromParsingName(this.FileName))
                {
                    // alternatively: shell.Properties.GetProperty("System.Media.Duration");
                    uint duration = (uint)shell.Properties.System.Media.Duration.Value;
                    TimeSpan ts = TimeSpan.FromSeconds(duration/10000000);
                    return ts;
                }
            }
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
                    _gpsInfo = GpsPointData.Convert(NovatekViofoGPSParser.ViofoParser.ReadMP4FileGpsInfo(Info.Info.FullName));
                }
                else
                {
                    _gpsInfo = new List<GpsPointData>(); //empty
                }

                CalculateDelay(FileDateStart);

                _isGpsInfoInitialized = true;
            }
        }

        internal void SetProtected(bool protect)
        {
            if (protect == IsProtected)
                return;

            string dirParent = Path.GetDirectoryName(FileNameFront);
            if (dirParent.EndsWith(@"\RO") || dirParent.EndsWith(@"Parking"))
                dirParent = Path.GetDirectoryName(dirParent);

            string moveToDir = dirParent;
            if (protect)
            {
                moveToDir = Path.Combine(dirParent, "RO");
            }
            else
            {
                if (this.RecordingType == RecordingType.Parking)
                    moveToDir = Path.Combine(dirParent, "Parking");
            }

            MoveAllFiles(moveToDir, protect);
        }

        internal void MoveAllFiles(string moveToDir, bool protect)
        {
            try
            {
                Directory.CreateDirectory(moveToDir);

                MoveFile(ref FileNameFront, moveToDir, protect);
                MoveFile(ref FileNameNmea, moveToDir, protect);
                MoveFile(ref FileNameRear, moveToDir, protect);
                MoveFile(ref FileNameInside, moveToDir, protect);

                Info.MoveInfo(FileNameFront, FileNameInside, FileNameRear);

                IsProtected = protect;
            }
            catch (Exception err)
            {
                Debug.WriteLine("Exception moving files: " + err);
                MessageBox.Show("Exception moving files: " + err);
            }
        }

        private void MoveFile(ref string src, string dstDir, bool setReadonly)
        {
            if (File.Exists(src))
            {
                FileInfo f = new FileInfo(src);
                f.IsReadOnly = false;
                string dst = Path.Combine(dstDir, f.Name);

                File.Move(src, dst);
                f = new FileInfo(dst);
                f.IsReadOnly = setReadonly;
                src = dst;
            }
            else if (!string.IsNullOrWhiteSpace(src))
            {
                throw new FileNotFoundException("Mov not found: "+src);
            }
        }

        internal void DeleteRecording()
        {
            Action<string> deleteFile = (name) =>
            {
                if (File.Exists(name))
                {
                    FileInfo f = new FileInfo(name);
                    f.IsReadOnly = false;
                    File.Delete(name);
                }
            };

            try
            {
                deleteFile(FileNameFront);
                deleteFile(FileNameNmea);
                deleteFile(FileNameRear);
                deleteFile(FileNameInside);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Exception deleting files: " + err);
                MessageBox.Show("Exception deleting files: " + err);
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

        private bool FromViofoFileName(List<FileInfoWithDateFromFileName> allFiles, ref int idx,
            ref RecordingType fileType, ref string frontFileName, ref string rearFileName, ref string insideFileName)
        {
            frontFileName = "";
            rearFileName = "";
            insideFileName = "";

            FileInfoWithDateFromFileName info1 = allFiles[idx];
            DateTime startDate1 = info1.Date;
            DateTime endDate1 = info1.Info.LastWriteTime;

            int start = idx; int end = Math.Min(idx + 3, allFiles.Count);
            for (int i = start; i < end; i++)
            {
                FileInfoWithDateFromFileName info2 = allFiles[i];
                DateTime startDate2 = info2.Date;
                DateTime endDate2 = info2.Info.LastWriteTime;

                double delta1 = Math.Abs((startDate2 - startDate1).TotalSeconds);
                double delta2 = Math.Abs((endDate2 - endDate1).TotalSeconds);
                if (delta1 < 3 || delta2 < 3)
                {
                    if (info2.Info.Name.EndsWith("_F.MP4", true, CultureInfo.InvariantCulture))
                    {
                        frontFileName = info2.Info.FullName;
                        RecordingType = RecordingType.Driving;
                        Info = info2;
                        idx++;
                    }
                    else if (info2.Info.Name.EndsWith("_R.MP4", true, CultureInfo.InvariantCulture))
                    {
                        rearFileName = info2.Info.FullName;
                        RecordingType = RecordingType.Driving;
                        idx++;
                    }
                    else if (info2.Info.Name.EndsWith("_I.MP4", true, CultureInfo.InvariantCulture))
                    {
                        insideFileName = info2.Info.FullName;
                        RecordingType = RecordingType.Driving;
                        idx++;
                    }
                    else if (info2.Info.Name.EndsWith("_PF.MP4", true, CultureInfo.InvariantCulture))
                    {
                        frontFileName = info2.Info.FullName;
                        RecordingType = RecordingType.Parking;
                        Info = info2;
                        idx++;
                    }
                    else if (info2.Info.Name.EndsWith("_PR.MP4", true, CultureInfo.InvariantCulture))
                    {
                        rearFileName = info2.Info.FullName;
                        RecordingType = RecordingType.Parking;
                        idx++;
                    }
                    else if (info2.Info.Name.EndsWith("_PI.MP4", true, CultureInfo.InvariantCulture))
                    {
                        insideFileName = info2.Info.FullName;
                        RecordingType = RecordingType.Parking;
                        idx++;
                    }

                    IsProtected = info2.Info.IsReadOnly;
                }
            }
            if (Info == null)
                Info = info1;

            return  !string.IsNullOrWhiteSpace(frontFileName) || 
                    !string.IsNullOrWhiteSpace(rearFileName) || 
                    !string.IsNullOrWhiteSpace(insideFileName);
        }

        private DateTime FromDuDuBellFileName(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            string date_time = fileName.Substring(fileName.Length - 14, 13); //191121-141124
            return DateTime.ParseExact(date_time, "yyMMdd-HHmmss", CultureInfo.InvariantCulture);
        }

        internal string GetLocationInfoForTime(double elapsedSeconds, double totalSeconds)
        {
            int idx = FindGpsInfoIdx(elapsedSeconds, totalSeconds, false, 30, 1.0);
            if (idx < 0)
                return "No GPS info...";

            string info = "Time: " + GpsInfo[idx].FixTime.AddHours(_iGpsTimeZoneHours).ToString("yyyy/MM/dd HH:mm:ss") + 
                ", " + new PointLatLngUI(GpsInfo[idx].Latitude, GpsInfo[idx].Longitude).ToString() + 
                ", Speed: " + GpsInfo[idx].SpeedMph.ToString("0.0 mph") + 
                ", Azimuth: " + GpsInfo[idx].Course.ToString("0.0");
            return info;
        }

        public int FindGpsInfoIdx(double elapsedSeconds, double totalSeconds, bool isTimeLapse, int fps, double speedRatio)
        {
            if (_gpsInfo == null || _gpsInfo.Count == 0 || totalSeconds == 0)
                return -1;

            if(isTimeLapse)
            {
                double timeLapseMultiplier = 30.0 / fps;
                elapsedSeconds *= timeLapseMultiplier;
                totalSeconds *= timeLapseMultiplier;
            }

            elapsedSeconds += _dGpsDelaySeconds; //correct for GPS delay

            DateTime start = _gpsInfo.First().FixTime;
            TimeSpan duration = (_gpsInfo.Last().FixTime - start);

            for (int i = 0; i < _gpsInfo.Count; i++)
            {
                TimeSpan delta = _gpsInfo[i].FixTime - start;
                if (delta.TotalSeconds >= (elapsedSeconds))
                    return Math.Max(0, i);
            }
            return _gpsInfo.Count - 1; //last index
        }

        public TimeSpan FindPositionFromGpsIndex(int idx, bool isTimeLapse, int fps)
        {
            if (_gpsInfo == null || _gpsInfo.Count == 0 || idx < 0)
                return TimeSpan.FromSeconds(0);

            double pos = (_gpsInfo[idx].FixTime - _gpsInfo.First().FixTime).TotalSeconds - _dGpsDelaySeconds;

            if(isTimeLapse)
            {

            }

            return TimeSpan.FromSeconds(pos);
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
            string name = Info.Info?.Name;
            string gps = _gpsInfo == null ? "No Data" : _gpsInfo.Count.ToString();
            return "N: " + name + ", GPS Data: " + gps + ", Date: " + FileDateStart.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }
    }
}
