using MkZ.Tools;
using MkZ.Windows;
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
using System.Windows.Input;
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
        A = 0, B, C, D, E, F, G, H, I, J, K,
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
        public bool  IsFavorite { get; set; } = false;

        [XmlAttribute]
        public double Position { get; set; } = 0.0;
        [XmlAttribute]
        public bool ReplayIsOn { get; set; } = false;

        [XmlAttribute]
        public bool IsMoreBookmarksOpen { get; set; } = false;

        [XmlAttribute]
        public double ReplayPosA { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosB { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosC { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosD { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosE { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosF { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosG { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosH { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosI { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosJ { get; set; } = 0.0;
        [XmlAttribute]
        public double ReplayPosK { get; set; } = 0.0;

        [XmlIgnore]
        public DateTime LastUpdate { get; private set; } = DateTime.MinValue;

        public void Update(OnePlayerSettings settings)
        {
            //if it was updated after this setting was updated
            if (LastUpdate > settings.LastUpdate)
                return;

            LastUpdate = settings.LastUpdate;

            FileName = Path.GetFileName(settings.FileName);
            
            IsFavorite = settings.IsFavorite;

            Position = Math.Round(settings.Position, 3);

            ReplayIsOn = settings.ReplayIsOn;

            IsMoreBookmarksOpen = settings.IsMoreBookmarksOpen;

            ReplayPosA = Math.Round(settings.ReplayPosA, 3);
            ReplayPosB = Math.Round(settings.ReplayPosB, 3);

            ReplayPosC = Math.Round(settings.ReplayPosC, 3);
            ReplayPosD = Math.Round(settings.ReplayPosD, 3);
            ReplayPosE = Math.Round(settings.ReplayPosE, 3);
            ReplayPosF = Math.Round(settings.ReplayPosF, 3);
            ReplayPosG = Math.Round(settings.ReplayPosG, 3);
            ReplayPosH = Math.Round(settings.ReplayPosH, 3);
            ReplayPosI = Math.Round(settings.ReplayPosI, 3);
            ReplayPosJ = Math.Round(settings.ReplayPosJ, 3);
            ReplayPosK = Math.Round(settings.ReplayPosK, 3);
        }

        public override string ToString()
        {
            string favorite = IsFavorite?"*":"";
            return $"{favorite}RecentFile: Pos: {Position}, Name: {FileName}";
        }
    }

    [Serializable]
    public class OnePlayerSettings : NotifyPropertyChangedImpl
    {
        [XmlAttribute]
        public bool IsFavorite { get; set; } = false;
        public string FileName { get; set; } = string.Empty;
        public double Position { get; set; } = 0.0;
        public double Duration { get; set; } = 0.0;
        public ePlayMode PlayMode { get; set; } = ePlayMode.RepeatOne;
        public eZoomState ZoomState {  get; set; } = eZoomState.FitWindow;
        public double Zoom { get; set; } = 1.0;
        public MediaState MediaState { get; set; } = MediaState.Play;
        public double Volume { get; set; } = 0.0;
        public double SpeedRatio { get; set; } = 1.0;

        private bool _isMoreBookmarksOpen = false;
        public bool IsMoreBookmarksOpen { get => _isMoreBookmarksOpen; set { if (SetProperty(ref _isMoreBookmarksOpen, value)) LastUpdate = DateTime.Now; } }

        private double _replayPosA = 0.0;
        public double ReplayPosA { get => _replayPosA; set => SetProperty(ref _replayPosA, value); }
        
        private double _replayPosB = 0.0;
        public double ReplayPosB { get => _replayPosB; set => SetProperty(ref _replayPosB, value); }
        
        private double _replayPosC = 0.0;                  
        public double ReplayPosC { get => _replayPosC; set => SetProperty(ref _replayPosC, value); }
        
        private double _replayPosD = 0.0;                  
        public double ReplayPosD { get => _replayPosD; set => SetProperty(ref _replayPosD, value); }
        
        private double _replayPosE = 0.0;                   
        public double ReplayPosE { get => _replayPosE; set => SetProperty(ref _replayPosE, value); }
        private double _replayPosF = 0.0;                  
        public double ReplayPosF { get => _replayPosF; set => SetProperty(ref _replayPosF, value); }
                                                           
        private double _replayPosG = 0.0;                  
        public double ReplayPosG { get => _replayPosG; set => SetProperty(ref _replayPosG, value); }
                                                           
        private double _replayPosH = 0.0;                  
        public double ReplayPosH { get => _replayPosH; set => SetProperty(ref _replayPosH, value); }
                                                           
        private double _replayPosI = 0.0;                  
        public double ReplayPosI { get => _replayPosI; set => SetProperty(ref _replayPosI, value); }
        
        private double _replayPosJ = 0.0;
        public double ReplayPosJ { get => _replayPosJ; set => SetProperty(ref _replayPosJ, value); }
        
        private double _replayPosK = 0.0;

        public double ReplayPosK { get => _replayPosK; set => SetProperty(ref _replayPosK, value); }

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
            IsFavorite = s.IsFavorite;
            Duration = s.Duration;
            Position = s.Position;

            PlayMode = s.PlayMode;
            
            Zoom = s.Zoom;
            ZoomState = s.ZoomState;

            MediaState = s.MediaState;
            Volume = s.Volume;
            SpeedRatio = s.SpeedRatio;

            IsMoreBookmarksOpen = s.IsMoreBookmarksOpen;

            UpdateBookmarks(s);
        }

        public void Update(VideoPlayerUserControl v, double duration = 0.0)
        {
            LastUpdate = v.VM.Settings.LastUpdate;

            FileName = v.FileName;
            IsFavorite = v.VM.Settings.IsFavorite;

            Duration = v.Duration > 0 ? v.Duration : duration;
            Position = v.Position.TotalSeconds > 0.5 ? v.Position.TotalSeconds : v.VM.Settings.Position;

            PlayMode = v.VM.Settings.PlayMode;

            ZoomState = v.ZoomState;
            Zoom = v.Zoom;
            MediaState = v.MediaState;
            Volume = v.Volume;
            SpeedRatio = v.SpeedRatio;

            IsMoreBookmarksOpen = v.VM.Settings.IsMoreBookmarksOpen;

            UpdateBookmarks(v.VM.Settings);

            EnsureHasValues();
        }

        public void UpdateBookmarks(OnePlayerSettings s)
        {
            Position = s.Position;

            ReplayIsOn = s.ReplayIsOn;

            IsMoreBookmarksOpen = s.IsMoreBookmarksOpen;

            ReplayPosA = s.ReplayPosA;
            ReplayPosB = s.ReplayPosB;

            ReplayPosC = s.ReplayPosC;
            ReplayPosD = s.ReplayPosD;
            ReplayPosE = s.ReplayPosE;
            ReplayPosF = s.ReplayPosF;
            ReplayPosG = s.ReplayPosG;
            ReplayPosH = s.ReplayPosH;
            ReplayPosI = s.ReplayPosI;
            ReplayPosJ = s.ReplayPosJ;
            ReplayPosK = s.ReplayPosK;
        }

        public void UpdateBookmarks(bool on, RecentFile f)
        {
            ReplayIsOn = on;

            Position = f.Position;

            IsMoreBookmarksOpen = f.IsMoreBookmarksOpen;

            ReplayPosA = f.ReplayPosA;
            ReplayPosB = f.ReplayPosB;

            ReplayPosC = f.ReplayPosC;
            ReplayPosD = f.ReplayPosD;
            ReplayPosE = f.ReplayPosE;
            ReplayPosF = f.ReplayPosF;
            ReplayPosG = f.ReplayPosG;
            ReplayPosH = f.ReplayPosH;
            ReplayPosI = f.ReplayPosI;
            ReplayPosJ = f.ReplayPosJ;
            ReplayPosK = f.ReplayPosK;
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
                case eBookmarkName.A: ReplayPosA = pos; break;
                case eBookmarkName.B: ReplayPosB = pos; break;
                case eBookmarkName.C: ReplayPosC = pos; break;
                case eBookmarkName.D: ReplayPosD = pos; break;
                case eBookmarkName.E: ReplayPosE = pos; break;
                case eBookmarkName.F: ReplayPosF = pos; break;
                case eBookmarkName.G: ReplayPosG = pos; break;
                case eBookmarkName.H: ReplayPosH = pos; break;
                case eBookmarkName.I: ReplayPosI = pos; break;
                case eBookmarkName.J: ReplayPosJ = pos; break;
                case eBookmarkName.K: ReplayPosK = pos; break;
                default:
                    break;
            }
        }

        public double BookmarkPositionGet(eBookmarkName name)
        {
            switch (name)
            {
                case eBookmarkName.A: return (ReplayPosA);
                case eBookmarkName.B: return (ReplayPosB);
                case eBookmarkName.C: return (ReplayPosC);
                case eBookmarkName.D: return (ReplayPosD);
                case eBookmarkName.E: return (ReplayPosE);
                case eBookmarkName.F: return (ReplayPosF);
                case eBookmarkName.G: return (ReplayPosG);
                case eBookmarkName.H: return (ReplayPosH);
                case eBookmarkName.I: return (ReplayPosI);
                case eBookmarkName.J: return (ReplayPosJ);
                case eBookmarkName.K: return (ReplayPosK);
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
        [DisplayName("Execute Batch Operation"), Category(APP)]
        public System.Windows.Input.Key KeyBatchOp { get; set; } = System.Windows.Input.Key.F4;
        [DisplayName("Save as default Key"), Category(APP)]
        public System.Windows.Input.Key KeySaveAsDefault { get; set; } = System.Windows.Input.Key.F5;

        [Description("Rows Sizes"), Category(VID)]
        public List<SplitterPos> RowsSizes { get; set; }
        [Description("Column Sizes"), Category(VID)]
        public List<SplitterPos> ColsSizes { get; set; }
        [Description("Inactive Background Color"), Category(VID)]
        public Color InactiveBackgroundColor { get; set; } = Color.DarkGray;

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
            KeyBatchOp = appConfig.KeyBatchOp;
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
                if (string.IsNullOrWhiteSpace(v.FileName))
                    continue;

                OnePlayerSettings s = new OnePlayerSettings(v);
                RecentFile recentFile = MainWindow.FindOrCreateRecentFile(s.FileName);
                recentFile.Update(s);
                s.UpdateBookmarks(s.ReplayIsOn, recentFile);

                this.PlayerSettings.Add(s);
            }
        }
    }
}
