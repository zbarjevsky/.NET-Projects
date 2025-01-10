using MkZ.Tools;
using MkZ.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MultiPlayer
{
    [Serializable]
    public class OnePlayerSettings
    {
        public string FileName { get; set; } = string.Empty;
        public double Position { get; set; } = 0.0;
        public double Duration { get; set; } = 0.0;
        public eZoomState ZoomState {  get; set; } = eZoomState.FitHeight;
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

        public void Update(VideoPlayerUserControl v, double duration = 0.0)
        {
            FileName = v.FileName;
            Duration = v.Duration > 0 ? v.Duration : duration;
            Position = v.Position.TotalSeconds;
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
    }

    [Serializable]
    public class MultiPlayerSettings
    {
        string _dataFolder;
        string _fileName;

        public List<OnePlayerSettings> Settings { get; set; }

        public MultiPlayerSettings() 
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataFolder = Path.Combine(commonPath, "MkZ", assemblyName);
            Directory.CreateDirectory(_dataFolder);

            string debug = "";
#if DEBUG
            debug = "_debug";
#endif
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("{0}_{1}{2}.xml", assemblyName, "Files", debug);
            _fileName = Path.Combine(_dataFolder, fileName);
        }

        public void Save()
        {
            XmlHelper.Save(_fileName, this);
        }

        public void Load()
        {
            if (File.Exists(_fileName))
            {
                try
                {
                    MultiPlayerSettings appConfig = XmlHelper.Open<MultiPlayerSettings>(_fileName);
                    this.CopyFrom(appConfig);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Cannot load Settings From File");
                }
            }
            this.EnsureHasValues();
        }

        private void CopyFrom(MultiPlayerSettings appConfig)
        {
            this.Settings = appConfig.Settings;
        }

        private void EnsureHasValues()
        {
            if (Settings == null)
                Settings = new List<OnePlayerSettings>();

            foreach (OnePlayerSettings item in Settings)
            {
                item.EnsureHasValues();
            }
        }

        public void Update(List<VideoPlayerUserControl> videos)
        {
            this.Settings = new List<OnePlayerSettings>();
            foreach(VideoPlayerUserControl v in videos)
            {
                this.Settings.Add(new OnePlayerSettings(v));
            }
        }
    }
}
