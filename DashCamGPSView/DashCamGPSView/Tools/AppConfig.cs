using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using DashCamGPSView.Controls;
using GPSDataParser;


using MkZ.Tools;
using MkZ.WPF;

namespace DashCamGPSView.Tools
{
    public class PlayerControlSettings
    {
        public double SoundVolume { get; set; } = 0.5;
        public bool FlipHorizontally { get; set; } = false;
        public double Zoom { get; set; } = 1.0;
        public eZoomState ZoomState { get; set; } = eZoomState.FitWidth;
        public double ScrollOffsetX { get; set; } = 0.5;
        public double ScrollOffsetY { get; set; } = 0.5;
        public double SplitterOffset { get; set; } = 0.0;

        public void CopyFrom(PlayerControlSettings player)
        {
            SoundVolume = player.SoundVolume;
            FlipHorizontally = player.FlipHorizontally;
            Zoom = player.Zoom;
            ZoomState = player.ZoomState;
            ScrollOffsetX = player.ScrollOffsetX;
            ScrollOffsetY = player.ScrollOffsetY;
            SplitterOffset = player.SplitterOffset;
        }

        public void CopyFrom(VideoPlayerControl v)
        {
            SoundVolume = v.Volume;
            FlipHorizontally = v.IsFlipHorizontally;
            ScrollOffsetY = v.ScrollOffsetY;
            ZoomState = v.ZoomStateGet();
        }

        public void RestoreTo(IVideoPlayer v, bool bRestoreVolume)
        {
            v.Volume = bRestoreVolume?SoundVolume:0;
            v.IsFlipHorizontally = FlipHorizontally;
            v.ZoomStateSet(ZoomState, true);
            v.ScrollOffsetY = ScrollOffsetY;
        }
    }

    public class AppConfig
    {
        private string _dataFolder;
        private string _fileName;

        public string LastSelectedFileName { get; set; } = "";

        public string SpeedUnits { get; set; } = GPSDataParser.SpeedUnits.mph.ToString();

        public MainWindowState MainWindowState { get; set; } = new MainWindowState();

        public PlayerControlSettings SpeedChart { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings GpsInfo { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings RightPanel { get; set; } = new PlayerControlSettings();

        public PlayerControlSettings PlayerF { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings PlayerI { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings PlayerR { get; set; } = new PlayerControlSettings();

        public AppConfig()
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
                    AppConfig settings = XmlHelper.Open<AppConfig>(_fileName);
                    this.CopyFrom(settings);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Cannot load Settings From File");
                }
            }
        }

        private void CopyFrom(AppConfig settings)
        {
            LastSelectedFileName = settings.LastSelectedFileName;

            SpeedUnits = settings.SpeedUnits;

            MainWindowState.CopyFrom(settings.MainWindowState);

            RightPanel.CopyFrom(settings.RightPanel);
            SpeedChart.CopyFrom(settings.SpeedChart);
            GpsInfo.CopyFrom(settings.GpsInfo);

            PlayerF.CopyFrom(settings.PlayerF);
            PlayerI.CopyFrom(settings.PlayerI);
            PlayerR.CopyFrom(settings.PlayerR);
        }
    }
}
