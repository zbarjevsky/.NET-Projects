using MkZ.Tools;
using MkZ.Windows;
using MkZ.WPF;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
    public class BookmarkSettings : NotifyPropertyChangedImpl
    {
        //when loading from XML disable updating 'LastUpdate'
        [XmlIgnore]
        public static  bool PauseLastUpdateForXmlSerialization = true;

        private double _position = 0.0;
        public double Position { get => _position; set => SetProperty(ref _position, value); }

        private DateTime _lastUpdate = DateTime.MinValue;
        public DateTime LastUpdate { get => _lastUpdate; set => SetProperty(ref _lastUpdate, value); }
        
        private string _fileName = string.Empty;
        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value); }

        private bool _isFavorite = false;
        [XmlAttribute]
        public bool IsFavorite { get => _isFavorite; set => UpdatePropertyAndTime(ref _isFavorite, value); }

        private bool _replayIsOn = false;
        [XmlAttribute]
        public bool ReplayIsOn { get => _replayIsOn; set => UpdatePropertyAndTime(ref _replayIsOn, value); }

        private bool _isMoreBookmarksOpen = false;
        [XmlAttribute]
        public bool IsMoreBookmarksOpen { get => _isMoreBookmarksOpen; set => UpdatePropertyAndTime(ref _isMoreBookmarksOpen, value); }

        private double _replayPosA = 0.0;
        [XmlAttribute]
        public double ReplayPosA { get => _replayPosA; set => UpdatePropertyAndTime(ref _replayPosA, value); }

        private double _replayPosB = 0.0;
        [XmlAttribute]
        public double ReplayPosB { get => _replayPosB; set => UpdatePropertyAndTime(ref _replayPosB, value); }

        private double _replayPosC = 0.0;
        [XmlAttribute]
        public double ReplayPosC { get => _replayPosC; set => UpdatePropertyAndTime(ref _replayPosC, value); }

        private double _replayPosD = 0.0;
        [XmlAttribute]
        public double ReplayPosD { get => _replayPosD; set => UpdatePropertyAndTime(ref _replayPosD, value); }

        private double _replayPosE = 0.0;
        [XmlAttribute]
        public double ReplayPosE { get => _replayPosE; set => UpdatePropertyAndTime(ref _replayPosE, value); }

        private double _replayPosF = 0.0;
        [XmlAttribute]
        public double ReplayPosF { get => _replayPosF; set => UpdatePropertyAndTime(ref _replayPosF, value); }

        private double _replayPosG = 0.0;
        [XmlAttribute]
        public double ReplayPosG { get => _replayPosG; set => UpdatePropertyAndTime(ref _replayPosG, value); }

        private double _replayPosH = 0.0;
        [XmlAttribute]
        public double ReplayPosH { get => _replayPosH; set => UpdatePropertyAndTime(ref _replayPosH, value); }

        private double _replayPosI = 0.0;
        [XmlAttribute]
        public double ReplayPosI { get => _replayPosI; set => UpdatePropertyAndTime(ref _replayPosI, value); }

        private double _replayPosJ = 0.0;
        [XmlAttribute]
        public double ReplayPosJ { get => _replayPosJ; set => UpdatePropertyAndTime(ref _replayPosJ, value); }

        private double _replayPosK = 0.0;

        [XmlAttribute]
        public double ReplayPosK { get => _replayPosK; set => UpdatePropertyAndTime(ref _replayPosK, value); }

        public bool ShouldSerializeReplayPosC() => ReplayPosC > 0.0;
        public bool ShouldSerializeReplayPosD() => ReplayPosD > 0.0;
        public bool ShouldSerializeReplayPosE() => ReplayPosE > 0.0;
        public bool ShouldSerializeReplayPosF() => ReplayPosF > 0.0;
        public bool ShouldSerializeReplayPosG() => ReplayPosG > 0.0;
        public bool ShouldSerializeReplayPosH() => ReplayPosH > 0.0;
        public bool ShouldSerializeReplayPosI() => ReplayPosI > 0.0;
        public bool ShouldSerializeReplayPosJ() => ReplayPosJ > 0.0;
        public bool ShouldSerializeReplayPosK() => ReplayPosK > 0.0;

        private bool UpdatePropertyAndTime<T>(ref T prop, T val, [CallerMemberName] string propertyName = null)
        {
            bool res = SetProperty(ref prop, val, propertyName);
            if (res && !PauseLastUpdateForXmlSerialization)
                LastUpdate = DateTime.Now;
            return res;
        }

        public void UpdateFrom(BookmarkSettings settings, bool force)
        {
            //if it was updated after this setting was updated
            if (!force && LastUpdate > DateTime.MinValue && LastUpdate > settings.LastUpdate)
                return;

            const int POS = 3;

            _lastUpdate = settings.LastUpdate;

            _fileName = settings.FileName;

            _isFavorite = settings.IsFavorite;

            _position = Math.Round(settings.Position, 3);

            _replayIsOn = settings.ReplayIsOn;

            _isMoreBookmarksOpen = settings.IsMoreBookmarksOpen;

            _replayPosA = Math.Round(settings.ReplayPosA, POS);
            _replayPosB = Math.Round(settings.ReplayPosB, POS);

            _replayPosC = Math.Round(settings.ReplayPosC, POS);
            _replayPosD = Math.Round(settings.ReplayPosD, POS);
            _replayPosE = Math.Round(settings.ReplayPosE, POS);
            _replayPosF = Math.Round(settings.ReplayPosF, POS);
            _replayPosG = Math.Round(settings.ReplayPosG, POS);
            _replayPosH = Math.Round(settings.ReplayPosH, POS);
            _replayPosI = Math.Round(settings.ReplayPosI, POS);
            _replayPosJ = Math.Round(settings.ReplayPosJ, POS);
            _replayPosK = Math.Round(settings.ReplayPosK, POS);
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

        public void SetFileName(string fullFileName)
        {
            FileName = GetShortFileName(fullFileName);
        }

        public static string GetShortFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            string name = Path.GetFileName(fileName);
            string shortName = name.TrimStart('_');
            return shortName;
        }

        public override string ToString()
        {
            string favorite = IsFavorite ? "*" : "";
            return $"{favorite}BookmarkSettings: Pos: {Position}, Name: {FileName}";
        }
    }

    [Serializable]
    public class RecentFiles
    {
        [XmlArray("RecentFilesList")]              // <RecentFilesList>...</RecentFilesList>
        [XmlArrayItem("RecentFile")]               // each item will be <RecentFile>
        public List<BookmarkSettings> RecentFilesList { get; set; } = new();

        public void CopyFrom(RecentFiles list)
        {
            RecentFilesList = new List<BookmarkSettings>(list.RecentFilesList);
        }

        public void Clear()
        {
            RecentFilesList.Clear();
        }

        public void Merge(RecentFiles recentFiles)
        {
            foreach (BookmarkSettings file in recentFiles.RecentFilesList)
            {
                if (string.IsNullOrWhiteSpace(file?.FileName))
                    continue;

                int idx = RecentFilesList.FindIndex(f => f.FileName == file.FileName);
                if (idx < 0) //new file
                    RecentFilesList.Add(file);
                else if (file.LastUpdate == DateTime.MinValue || RecentFilesList[idx].LastUpdate < file.LastUpdate)
                    RecentFilesList[idx] = file;
            }
        }
    }

    [Serializable]
    public class OnePlayerSettings : NotifyPropertyChangedImpl
    {
        public BookmarkSettings BookmarkSettings { get; set; } = new();

        private string _fullFileName = string.Empty;
        [XmlElement("FileName")]
        public string FullFileName { get => _fullFileName; set { _fullFileName = value; BookmarkSettings.SetFileName(value); } }
        
        [XmlIgnore]
        public double Position { get => BookmarkSettings.Position; set => BookmarkSettings.Position = value; }
        public double Duration { get; set; } = 0.0;
        public ePlayMode PlayMode { get; set; } = ePlayMode.RepeatOne;
        public eZoomState ZoomState {  get; set; } = eZoomState.FitWindow;
        public double Zoom { get; set; } = 1.0;
        public MediaState MediaState { get; set; } = MediaState.Play;
        public double Volume { get; set; } = 0.0;
        public double SpeedRatio { get; set; } = 1.0;

        public void EnsureHasValues()
        {

        }

        //default constructor for serialization
        public OnePlayerSettings() { }

        public OnePlayerSettings(VideoPlayerUserControl v)
        {
            Update(v);
        }

        public OnePlayerSettings(OnePlayerSettings s)
        {
            Update(s, true);
        }

        public void Update(OnePlayerSettings s, bool force)
        {
            BookmarkSettings.UpdateFrom(s.BookmarkSettings, force);

            FullFileName = s.FullFileName;

            Duration = s.Duration;
            PlayMode = s.PlayMode;
            
            Zoom = s.Zoom;
            ZoomState = s.ZoomState;

            MediaState = s.MediaState;
            Volume = s.Volume;
            SpeedRatio = s.SpeedRatio;
        }

        public void Update(VideoPlayerUserControl v, double duration = 0.0)
        {
            BookmarkSettings.UpdateFrom(v.VM.Settings.BookmarkSettings, force: true);

            FullFileName = v.VM.Settings.FullFileName;

            Duration = v.Duration > 0 ? v.Duration : duration;
            BookmarkSettings.Position = v.Position.TotalSeconds > 0.5 ? v.Position.TotalSeconds : v.VM.Settings.BookmarkSettings.Position;

            PlayMode = v.VM.Settings.PlayMode;

            ZoomState = v.ZoomState;
            Zoom = v.Zoom;
            MediaState = v.MediaState;
            Volume = v.Volume;
            SpeedRatio = v.SpeedRatio;

            EnsureHasValues();
        }

        public void UpdateBookmarks(BookmarkSettings f)
        {
            BookmarkSettings.UpdateFrom(f, true);
        }

        public void BookmarkPositionSet(eBookmarkName name, double pos)
        {
            BookmarkSettings.BookmarkPositionSet(name, pos);
        }

        public double BookmarkPositionGet(eBookmarkName name)
        {
            return BookmarkSettings.BookmarkPositionGet(name);
        }

        public override string ToString()
        {
            return "OnePlayerSettings: " + BookmarkSettings.FileName;
        }
    }

    public class SupportedFileExtensions
    {
        public string[] SupportedImageExtensions { get; set; } = new string[0];
        public string[] SupportedAudioExtensions { get; set; } = new string[0];
        public string[] SupportedVideoExtensions { get; set; } = new string[0];

        public void EnsureHasValues()
        {
            if (SupportedImageExtensions == null || SupportedImageExtensions.Length == 0)
                SupportedImageExtensions = new string[] { ".jpg", ".png", ".bmp", ".gif" };

            if (SupportedAudioExtensions == null || SupportedAudioExtensions.Length == 0)
                SupportedAudioExtensions = new string[] { ".mp3", ".wav", ".ogg" };

            if (SupportedVideoExtensions == null || SupportedVideoExtensions.Length == 0)
                SupportedVideoExtensions = new string[] { ".avi", ".mpg", ".mpeg", ".mkv", ".mp4", ".webm" };
        }

        public bool IsSupportedFileExtension(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            return SupportedVideoExtensions.Contains(ext) || SupportedAudioExtensions.Contains(ext);
        }
    }

    public class MainWindowState
    {
        public System.Windows.Rect Bounds { get; set; }

        public WindowState WindowState { get; set; } = WindowState.Normal;

        public WindowStyle WindowStyle { get; set; } = WindowStyle.ThreeDBorderWindow;

        public MainWindowState()
        {
            double width = 1600;
            if (width > SystemParameters.PrimaryScreenWidth)
                width = SystemParameters.PrimaryScreenWidth - 80;

            double height = 900;
            if (height > SystemParameters.PrimaryScreenHeight)
                height = SystemParameters.PrimaryScreenHeight - 80;

            double x = (SystemParameters.PrimaryScreenWidth - width) / 2.0;
            double y = (SystemParameters.PrimaryScreenHeight - height) / 2.0;

            //center screen
            Bounds = new System.Windows.Rect(x, y, width, height);
        }

        public void CopyFrom(Window wnd)
        {
            bool bFullScreen = wnd.WindowStyle == WindowStyle.None;

            if (!wnd.RestoreBounds.IsEmpty)
                Bounds = wnd.RestoreBounds;
            else if (wnd.WindowState == WindowState.Normal)
                Bounds = new Rect(wnd.Left, wnd.Top, wnd.ActualWidth, wnd.ActualHeight);

            WindowStyle = wnd.WindowStyle;
            if (!bFullScreen)
            {
                WindowState = wnd.WindowState;
            }
        }

        public void CopyFrom(MainWindowState state)
        {
            Bounds = state.Bounds;

            WindowState = state.WindowState;
            WindowStyle = state.WindowStyle;
        }

        public void RestoreTo(Window wnd)
        {
            Rect virtScreen = new Rect(
                SystemParameters.VirtualScreenLeft,
                SystemParameters.VirtualScreenTop,
                SystemParameters.VirtualScreenWidth,
                SystemParameters.VirtualScreenHeight);

            if (virtScreen.Contains(Bounds))
            {
                wnd.WindowStartupLocation = WindowStartupLocation.Manual;

                wnd.Left = Bounds.Left;
                wnd.Top = Bounds.Top;
                wnd.Width = Bounds.Width;
                wnd.Height = Bounds.Height;
            }
            else
            {
                wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            wnd.WindowState = WindowState;
            wnd.WindowStyle = WindowStyle;
        }

        public override string ToString()
        {
            return string.Format("MainWindowState: {0}, Bounds: {1}", WindowState, Bounds);
        }
    }

    public class GridSplitterPositions
    {
        [Description("Rows Sizes"), Category(MultiPlayerSettings.VID)]
        public List<SplitterPos> RowsSizes { get; set; } = new List<SplitterPos>();
        [Description("Column Sizes"), Category(MultiPlayerSettings.VID)]
        public List<SplitterPos> ColsSizes { get; set; } = new List<SplitterPos>();

        public void CopyFrom(GridSplitterPositions pos)
        {
            RowsSizes = new List<SplitterPos>(pos.RowsSizes);
            ColsSizes = new List<SplitterPos>(pos.ColsSizes);
        }
    }

    [Serializable]
    public class MultiPlayerSettings
    {
        public const string APP = "1. Application";
        public const string VID = "2. Video Player";

        [XmlIgnore]
        public string DataFolder { get; private set; }
        [XmlIgnore]
        public string DefaultSettingsFileName { get; private set; }
        [XmlIgnore]
        public string LastSettingsFileName { get; private set; }
        [XmlIgnore]
        public string RecentFilesFileName { get; private set; }

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

        //play one after another
        [Description("Global Repeat All Mode - as Play List"), Category(VID)]
        public bool IsGlobalRepeatAllMode { get; set; } = false;

        [XmlIgnore]
        [Description("Inactive Background Color"), Category(VID)]
        public SolidColorBrush InactiveBackgroundColor { get; set; } = System.Windows.Media.Brushes.DarkGray;

        [XmlElement("InactiveBackgroundColor")]
        public string InactiveBackgroundColorHtml
        {
            get
            {
                if (InactiveBackgroundColor is SolidColorBrush scb)
                    return scb.Color.ToString(); // "#FFFF0000" for Red
                return "";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    InactiveBackgroundColor = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(value));
            }
        }

        [Category("Location")]
        public MainWindowState MainWindowState { get; set; } = new MainWindowState();

        [Category("Location")]
        public MainWindowState PopupWindowState { get; set; } = new MainWindowState();

        [Description("Splitter Positions Main"), Category(VID)]
        public GridSplitterPositions GridSplitterPositionsMain { get; set; } = new GridSplitterPositions();
        [Description("Splitter Positions Top"), Category(VID)]
        public GridSplitterPositions GridSplitterPositionsTop { get; set; } = new GridSplitterPositions();
        [Description("Splitter Positions Bottom"), Category(VID)]
        public GridSplitterPositions GridSplitterPositionsBottom { get; set; } = new GridSplitterPositions();

        public SupportedFileExtensions SupportedFileExtensions { get; set; } = new ();

        [Description("PopUp Player Settings"), Category(VID)]
        public OnePlayerSettings PopUpPlayerSettings { get; set; }

        [Description("Players Settings x8"), Category(VID)]
        public List<OnePlayerSettings> PlayerSettings { get; set; }

        [Description("Recent Files"), Category(APP)]
        [XmlIgnore] //save it to separate file
        public RecentFiles RecentFiles { get; set; } = new RecentFiles();

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
            
            fileName = string.Format("{0}_{1}{2}.xml", assemblyName, "RecentFiles", debug);
            RecentFilesFileName = Path.Combine(DataFolder, fileName);
        }

        public bool HasData()
        {
            foreach (OnePlayerSettings item in PlayerSettings)
            {
                if (!string.IsNullOrWhiteSpace(item.BookmarkSettings.FileName))
                    return true;
            }
            return false;
        }

        public void Save(string fileName)
        {
            LastSettingsFileName = fileName;
            XmlHelper.Save(fileName, this);
            XmlHelper.Save(RecentFilesFileName, this.RecentFiles);
        }

        public void Load()
        {
            Load(DefaultSettingsFileName);
        }

        public void Load(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                try
                {
                    BookmarkSettings.PauseLastUpdateForXmlSerialization = true;
                    MultiPlayerSettings appConfig = XmlHelper.Open<MultiPlayerSettings>(fileName);
                    this.CopyFrom(appConfig);
                    this.LoadRecentFiles();
                    LastSettingsFileName = fileName;
                }
                catch (Exception err)
                {
                    System.Windows.MessageBox.Show(err.ToString(), "Cannot load Settings From File");
                }
                finally
                {
                    BookmarkSettings.PauseLastUpdateForXmlSerialization = false;
                }
            }
             
            this.EnsureHasValues();
        }

        private void LoadRecentFiles()
        {
            RecentFiles = LoadRecentFiles(RecentFilesFileName);
            if (RecentFiles == null)
                RecentFiles = new();
        }

        private static RecentFiles LoadRecentFiles(string recentFilesFileName)
        {
            if (System.IO.File.Exists(recentFilesFileName))
            {
                try
                {
                    BookmarkSettings.PauseLastUpdateForXmlSerialization = true;
                    RecentFiles recentFiles = XmlHelper.Open<RecentFiles>(recentFilesFileName);
                    return recentFiles;
                }
                catch (Exception err)
                {
                    System.Windows.MessageBox.Show("Cannot load Recent files From: \n" + recentFilesFileName + "\n\n" + err.ToString(), "Error loading recent files");
                }
                finally
                {
                    BookmarkSettings.PauseLastUpdateForXmlSerialization = false;
                }
            }
            return null;
        }

        public void UpdateRecentFiles(string recentFilesFileName)
        {
            RecentFiles recentFiles = LoadRecentFiles(recentFilesFileName);
            if(recentFiles != null)
            {
                RecentFiles.Merge(recentFiles);
            }
        }

        private void CopyFrom(MultiPlayerSettings appConfig)
        {
            MainWindowState = appConfig.MainWindowState;
            PopupWindowState = appConfig.PopupWindowState;

            KeyCloseApp = appConfig.KeyCloseApp;
            KeyClearAll = appConfig.KeyClearAll;
            KeySaveAsLast = appConfig.KeySaveAsLast;
            KeyLoadDefault = appConfig.KeyLoadDefault;
            KeyBatchOp = appConfig.KeyBatchOp;
            KeySaveAsDefault = appConfig.KeySaveAsDefault;

            this.GridSplitterPositionsMain.CopyFrom(appConfig.GridSplitterPositionsMain);
            this.GridSplitterPositionsTop.CopyFrom(appConfig.GridSplitterPositionsTop);
            this.GridSplitterPositionsBottom.CopyFrom(appConfig.GridSplitterPositionsBottom);

            this.InactiveBackgroundColorHtml = appConfig.InactiveBackgroundColorHtml;
            this.IsGlobalRepeatAllMode = appConfig.IsGlobalRepeatAllMode;

            this.SupportedFileExtensions = appConfig.SupportedFileExtensions;

            this.PlayerSettings = appConfig.PlayerSettings;
            this.PopUpPlayerSettings = appConfig.PopUpPlayerSettings;
            
            this.RecentFiles = appConfig.RecentFiles;
        }

        private void EnsureHasValues()
        {
            if (PlayerSettings == null)
                PlayerSettings = new List<OnePlayerSettings>();

            if (RecentFiles == null)
                RecentFiles = new RecentFiles();

            SupportedFileExtensions.EnsureHasValues();

            foreach (OnePlayerSettings item in PlayerSettings)
                item.EnsureHasValues();
        }

        public void Update(List<VideoPlayerUserControl> videos)
        {
            this.PlayerSettings = new List<OnePlayerSettings>();
            foreach(VideoPlayerUserControl v in videos)
            {
                if (string.IsNullOrWhiteSpace(v.FileName))
                    continue;

                OnePlayerSettings s = new OnePlayerSettings(v);
                BookmarkSettings recentFile = MainWindow.Instance.RecentFilesCache.FindOrCreateRecentFile(s.FullFileName);
                recentFile.UpdateFrom(s.BookmarkSettings, force: false);
                s.UpdateBookmarks(recentFile);

                this.PlayerSettings.Add(s);
            }
                
            this.PopUpPlayerSettings = VideoCommandsVM.PopUpVM.GetSettings(PopupWindowState);
        }
    }
}
