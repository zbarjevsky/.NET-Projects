using MkZ.Tools;
using MkZ.WPF;
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
    public enum ePlayMode
    {
        [Description("Play One")]
        PlayOne,
        [Description("Play All")]
        PlayAll,
        [Description("Repeat One")]
        RepeatOne,
        [Description("Repeat All")]
        RepeatAll,
        [Description("Shuffle")]
        Random
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

        public string[] SupportedImageExtensions { get; set; } = new string[0];
        public string[] SupportedAudioExtensions { get; set; } = new string[0];
        public string[] SupportedVideoExtensions { get; set; } = new string[0];

        public OnePlayerSettings()
        {
            EnsureHasValues();
        }

        public OnePlayerSettings(VideoPlayerUserControl v)
        {
            Update(v);
        }

        public void Update(OnePlayerSettings s)
        {
            FileName = s.FileName;
            Duration = s.Duration;
            Position = s.Position;

            PlayMode = s.PlayMode;
            
            Zoom = s.Zoom;
            ZoomState = s.ZoomState;

            MediaState = s.MediaState;
            Volume = s.Volume;
            SpeedRatio = s.SpeedRatio;
        }

        public void Update(VideoPlayerUserControl v, double duration = 0.0)
        {
            FileName = v.FileName;

            Duration = v.Duration > 0 ? v.Duration : duration;
            Position = v.Position.TotalSeconds > 0.5 ? v.Position.TotalSeconds : v.VM.Settings.Position;

            PlayMode = v.VM.Settings.PlayMode;

            ZoomState = v.ZoomState;
            Zoom = v.Zoom;
            MediaState = v.MediaState;
            Volume = v.Volume;
            SpeedRatio = v.SpeedRatio;

            EnsureHasValues();
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

        public override string ToString()
        {
            return "OnePlayerSettings: " + FileName;
        }
    }

    [Serializable]
    public class MultiPlayerSettings
    {
        [XmlIgnore]
        public string DataFolder { get; private set; }
        [XmlIgnore]
        public string FileName { get; private set; }

        public List<SplitterPos> RowsSizes { get; set; }
        public List<SplitterPos> ColsSizes { get; set; }

        public List<OnePlayerSettings> PlayerSettings { get; set; }

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
            FileName = Path.Combine(DataFolder, fileName);
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
            XmlHelper.Save(fileName, this);
        }

        public void Load()
        {
            Load(FileName);
        }

        public void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    MultiPlayerSettings appConfig = XmlHelper.Open<MultiPlayerSettings>(fileName);
                    this.CopyFrom(appConfig);
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
            this.RowsSizes = appConfig.RowsSizes;
            this.ColsSizes = appConfig.ColsSizes;
            this.PlayerSettings = appConfig.PlayerSettings;
        }

        private void EnsureHasValues()
        {
            if (PlayerSettings == null)
                PlayerSettings = new List<OnePlayerSettings>();

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
                this.PlayerSettings.Add(new OnePlayerSettings(v));
            }
        }
    }
}
