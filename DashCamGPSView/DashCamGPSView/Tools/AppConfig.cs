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
using DashCamGPSView.Controls;
using GPSDataParser;


using MkZ.Tools;
using MkZ.WPF;

namespace DashCamGPSView.Tools
{
    public class SplitterPosition
    {
        public double Value { get; set; } = 1;
        public GridUnitType Type { get; set; } = GridUnitType.Star;

        public GridLength GetGridLength() { return new GridLength(Value, Type); }

        public void SetGridLength(ColumnDefinition col)
        {
            Value = col.Width.Value;
            Type = col.Width.GridUnitType;

            if (col.ActualWidth == col.Width.Value)
                Type = GridUnitType.Pixel;
        }

        public void SetGridLength(RowDefinition row)
        {
            Value = row.Height.Value;
            Type = row.Height.GridUnitType;

            if (row.ActualHeight == row.Height.Value)
                Type = GridUnitType.Pixel;
        }

        public override string ToString()
        {
            return GetGridLength().ToString();
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PlayerControlSettings
    {
        public double SoundVolume { get; set; } = 0.5;
        public bool FlipHorizontally { get; set; } = false;
        public double Zoom { get; set; } = 1.0;
        public eZoomState ZoomState { get; set; } = eZoomState.FitWidth;
        public double ScrollOffsetX { get; set; } = 0.5;
        public double ScrollOffsetY { get; set; } = 0.5;
        public SplitterPosition SplitterOffset { get; set; } = new SplitterPosition();

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
            ZoomState = v.ZoomState;
        }

        public void RestoreTo(IVideoPlayer v, bool bRestoreVolume)
        {
            v.Volume = bRestoreVolume?SoundVolume:0;
            v.IsFlipHorizontally = FlipHorizontally;
            v.ZoomStateSet(ZoomState, true);
            v.ScrollOffsetY = ScrollOffsetY;
        }

        public override string ToString()
        {
            return "PlayerControlSettings: " + SplitterOffset;
        }
    }

    public class AppConfig
    {
        private string _dataFolder;
        private string _fileName;

        public string LastSelectedFileName { get; set; } = "";

        public string SpeedUnits { get; set; } = GPSDataParser.SpeedUnits.mph.ToString();

        public TimeZoneUI TimeZone { get; set; } = new TimeZoneUI();

        public MainWindowState MainWindowState { get; set; } = new MainWindowState();

        public PlayerControlSettings SpeedChart { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings RightPanel { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings GpsMap { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings GpsInfo { get; set; } = new PlayerControlSettings();

        public PlayerControlSettings PlayerF { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings PlayerI { get; set; } = new PlayerControlSettings();
        public PlayerControlSettings PlayerR { get; set; } = new PlayerControlSettings();

        private void CopyFrom(AppConfig settings)
        {
            LastSelectedFileName = settings.LastSelectedFileName;

            SpeedUnits = settings.SpeedUnits;

            MainWindowState.CopyFrom(settings.MainWindowState);

            RightPanel.CopyFrom(settings.RightPanel);
            SpeedChart.CopyFrom(settings.SpeedChart);
            GpsMap.CopyFrom(settings.GpsMap);
            GpsInfo.CopyFrom(settings.GpsInfo);

            PlayerF.CopyFrom(settings.PlayerF);
            PlayerI.CopyFrom(settings.PlayerI);
            PlayerR.CopyFrom(settings.PlayerR);
        }

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
                    AppConfig settings = XmlHelper.Open<DashCamGPSView.Tools.AppConfig>(_fileName);
                    this.CopyFrom(settings);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Cannot load Settings From File");
                }
            }
        }
    }
}
