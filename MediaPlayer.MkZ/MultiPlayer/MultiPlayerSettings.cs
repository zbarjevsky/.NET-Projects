using MkZ.Tools;
using MkZ.WPF;
using MultiPlayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace MultiPlayer
{
    //directory tree style play list
    public enum ePlayMode : int
    {
        [Description("Play One")]
        PlayOne = 0,
        [Description("Play All")]
        PlayAll,
        [Description("Repeat One")]
        RepeatOne,
        [Description("Repeat All")]
        RepeatAll,
        [Description("Shuffle")]
        Random
    }

    public enum eBookmarkName : int
    {
        A = 0,
        B = 1, 
        C = 2, 
        D = 3
    }

    [Serializable]
    public class SplitterPos
    {
        public double Length { get; set; }
        public GridUnitType Type { get; set; }

        public SplitterPos() { }

        public SplitterPos(GridLength length)
        {
            Pos = length;
        }

        [XmlIgnore]
        public GridLength Pos
        {
            get { return new GridLength(Length, Type); }
            set { Length = value.Value; Type = value.GridUnitType; }
        }

        public override string ToString()
        {
            return Pos.ToString();
        }
    }

    [Serializable]
    public class RecentFile
    {
        public string FileName { get; set; } = string.Empty;

        [XmlAttribute]
        public double Position { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosA { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosB { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosC { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosD { get; set; } = 0.0;

        [XmlIgnore]
        public DateTime LastUpdate { get; private set; } = DateTime.MinValue;

        public void Update(OnePlayerSettings settings)
        {
            //if it was updated after this setting was updated
            if (LastUpdate > settings.LastUpdate)
                return;

            LastUpdate = settings.LastUpdate;

            FileName = Path.GetFileName(settings.FileName);

            Position = Math.Round(settings.Position, 3);

            ReplayPosA = Math.Round(settings.ReplayPosA, 1);
            ReplayPosB = Math.Round(settings.ReplayPosB, 1);

            ReplayPosC = Math.Round(settings.ReplayPosC, 1);
            ReplayPosD = Math.Round(settings.ReplayPosD, 1);
        }

        public override string ToString()
        {
            return $"RecentFile: Pos: {Position}, Name: {FileName}";
        }
    }

    [Serializable]
    public class OnePlayerSettings
    {
        public string FileName { get; set; } = string.Empty;
        public double Position { get; set; } = 0.0;
        public double Duration { get; set; } = 0.0;
        public ePlayMode PlayMode { get; set; } = ePlayMode.RepeatOne;
        public eZoomState ZoomState {  get; set; } = eZoomState.FitWindow;
        public double Zoom { get; set; } = 1.0;
        public MediaState MediaState { get; set; } = MediaState.Play;
        public double Volume { get; set; } = 0.0;
        public double SpeedRatio { get; set; } = 1.0;
        public double ReplayPosA { get; set; } = 0.0;
        public double ReplayPosB { get; set; } = 0.0;
        public double ReplayPosC { get; set; } = 0.0;
        public double ReplayPosD { get; set; } = 0.0;
        public bool ReplayIsOn {  get; set; } = false;

        public string[] SupportedImageExtensions { get; set; } = new string[0];
        public string[] SupportedAudioExtensions { get; set; } = new string[0];
        public string[] SupportedVideoExtensions { get; set; } = new string[0];

        [XmlIgnore]
        public DateTime LastUpdate { get; private set; } = DateTime.MinValue;

        public OnePlayerSettings()
        {
            EnsureHasValues();
        }

        public OnePlayerSettings(VideoPlayerUserControl v)
        {
            Update(v);
        }

        public OnePlayerSettings(OnePlayerSettings s)
        {
            Update(s);
        }

        public void Update(OnePlayerSettings s)
        {
            LastUpdate = s.LastUpdate;

            FileName = s.FileName;
            Duration = s.Duration;
            Position = s.Position;

            PlayMode = s.PlayMode;
            
            Zoom = s.Zoom;
            ZoomState = s.ZoomState;

            MediaState = s.MediaState;
            Volume = s.Volume;
            SpeedRatio = s.SpeedRatio;

            UpdateBookmarks(s);
        }

        public void Update(VideoPlayerUserControl v, double duration = 0.0)
        {
            LastUpdate = v.VM.Settings.LastUpdate;

            FileName = v.FileName;

            Duration = v.Duration > 0 ? v.Duration : duration;
            Position = v.Position.TotalSeconds > 0.5 ? v.Position.TotalSeconds : v.VM.Settings.Position;

            PlayMode = v.VM.Settings.PlayMode;

            ZoomState = v.ZoomState;
            Zoom = v.Zoom;
            MediaState = v.MediaState;
            Volume = v.Volume;
            SpeedRatio = v.SpeedRatio;

            UpdateBookmarks(v.VM.Settings);

            EnsureHasValues();
        }

        public void UpdateBookmarks(OnePlayerSettings s)
        {
            Position = s.Position;

            ReplayIsOn = s.ReplayIsOn;

            ReplayPosA = s.ReplayPosA;
            ReplayPosB = s.ReplayPosB;

            ReplayPosC = s.ReplayPosC;
            ReplayPosD = s.ReplayPosD;
        }

        public void UpdateBookmarks(bool on, RecentFile f)
        {
            ReplayIsOn = on;

            Position = f.Position;

            ReplayPosA = f.ReplayPosA;
            ReplayPosB = f.ReplayPosB;

            ReplayPosC = f.ReplayPosC;
            ReplayPosD = f.ReplayPosD;
        }

        public void EnsureHasValues()
        {
            if (SupportedImageExtensions == null || SupportedImageExtensions.Length == 0)
                SupportedImageExtensions = new string[] { ".jpg", ".png", ".bmp", ".gif" };

            if (SupportedAudioExtensions == null || SupportedAudioExtensions.Length == 0)
                SupportedAudioExtensions = new string[] { ".mp3", ".wav", ".ogg" };

            if (SupportedVideoExtensions == null || SupportedVideoExtensions.Length == 0)
                SupportedVideoExtensions = new string[] { ".avi", ".mpg", ".mpeg", ".mkv", ".mp4", ".webm" };
        }

        public void BookmarkPositionSet(eBookmarkName name, double pos)
        {
            LastUpdate = DateTime.Now;

            switch (name)
            {
                case eBookmarkName.A:
                    ReplayPosA = pos;
                    break;
                case eBookmarkName.B:
                    ReplayPosB = pos;
                    break;
                case eBookmarkName.C:
                    ReplayPosC = pos;
                    break;
                case eBookmarkName.D:
                    ReplayPosD = pos;
                    break;
                default:
                    break;
            }
        }

        public double BookmarkPositionGet(eBookmarkName name)
        {
            switch (name)
            {
                case eBookmarkName.A:
                    return (ReplayPosA);
                case eBookmarkName.B:
                    return (ReplayPosB);
                case eBookmarkName.C:
                    return (ReplayPosC);
                case eBookmarkName.D:
                    return (ReplayPosD);
                default:
                    return -1;
            }
        }

        public override string ToString()
        {
            return "OnePlayerSettings: " + FileName;
        }
    }

    [Serializable]
    public class MultiPlayerSettings
    {
        const string APP = "1. Application";
        const string VID = "2. Video Player";

        [XmlIgnore]
        public string DataFolder { get; private set; }
        [XmlIgnore]
        public string DefaultSettingsFileName { get; private set; }
        [XmlIgnore]
        public string LastSettingsFileName { get; private set; }

        [XmlIgnore]
        [DisplayName("Close App Key"), Category(APP)]
        public System.Windows.Input.Key KeyCloseApp { get; private set; } = System.Windows.Input.Key.Escape;
        [DisplayName("Clear All Key"), Category(APP)]
        public System.Windows.Input.Key KeyClearAll { get; set; } = System.Windows.Input.Key.F1;
        [DisplayName("Save As Last Key"), Category(APP)]
        public System.Windows.Input.Key KeySaveAsLast { get; set; } = System.Windows.Input.Key.F2;
        [DisplayName("Load Default Key"), Category(APP)]
        public System.Windows.Input.Key KeyLoadDefault { get; set; } = System.Windows.Input.Key.F3;
        [DisplayName("Save as default Key"), Category(APP)]
        public System.Windows.Input.Key KeySaveAsDefault { get; set; } = System.Windows.Input.Key.F5;

        [Description("Rows Sizes"), Category(VID)]
        public List<SplitterPos> RowsSizes { get; set; }
        [Description("Column Sizes"), Category(VID)]
        public List<SplitterPos> ColsSizes { get; set; }

        [Description("Players Settings x8"), Category(VID)]
        public List<OnePlayerSettings> PlayerSettings { get; set; }

        [Description("Recent Files"), Category(APP)]
        public List<RecentFile> RecentFiles { get; set; }

        public MultiPlayerSettings() 
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DataFolder = Path.Combine(commonPath, "MkZ", assemblyName);
            Directory.CreateDirectory(DataFolder);

            string debug = "";
#if DEBUG
            debug = "_debug";
#endif
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("{0}_{1}{2}.xml", assemblyName, "Files", debug);
            DefaultSettingsFileName = Path.Combine(DataFolder, fileName);
        }

        public bool HasData()
        {
            foreach (OnePlayerSettings item in PlayerSettings)
            {
                if (!string.IsNullOrWhiteSpace(item.FileName))
                    return true;
            }
            return false;
        }

        public void Save(string fileName)
        {
            LastSettingsFileName = fileName;
            XmlHelper.Save(fileName, this);
        }

        public void Load()
        {
            Load(DefaultSettingsFileName);
        }

        public void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    MultiPlayerSettings appConfig = XmlHelper.Open<MultiPlayerSettings>(fileName);
                    this.CopyFrom(appConfig);
                    LastSettingsFileName = fileName;
                }
                catch (Exception err)
                {
                    System.Windows.MessageBox.Show(err.ToString(), "Cannot load Settings From File");
                }
            }
            this.EnsureHasValues();
        }

        private void CopyFrom(MultiPlayerSettings appConfig)
        {
            KeyCloseApp = appConfig.KeyCloseApp;
            KeyClearAll = appConfig.KeyClearAll;
            KeySaveAsLast = appConfig.KeySaveAsLast;
            KeyLoadDefault = appConfig.KeyLoadDefault;
            KeySaveAsDefault = appConfig.KeySaveAsDefault;

            this.RowsSizes = appConfig.RowsSizes;
            this.ColsSizes = appConfig.ColsSizes;
            this.PlayerSettings = appConfig.PlayerSettings;
            this.RecentFiles = appConfig.RecentFiles;
        }

        private void EnsureHasValues()
        {
            if (PlayerSettings == null)
                PlayerSettings = new List<OnePlayerSettings>();

            if (RecentFiles == null)
                RecentFiles = new List<RecentFile>();

            foreach (OnePlayerSettings item in PlayerSettings)
            {
                item.EnsureHasValues();
            }
        }

        public void Update(List<VideoPlayerUserControl> videos)
        {
            this.PlayerSettings = new List<OnePlayerSettings>();
            foreach(VideoPlayerUserControl v in videos)
            {
                OnePlayerSettings s = new OnePlayerSettings(v);
                RecentFile recentFile = MainWindow.FindOrCreateRecentFile(s.FileName);
                recentFile.Update(s);
                s.UpdateBookmarks(s.ReplayIsOn, recentFile);

                this.PlayerSettings.Add(s);
            }
        }
    }
}
