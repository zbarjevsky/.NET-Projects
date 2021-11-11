using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using DashCamGPSView.Properties;
using DashCamGPSView.Tools;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GPSDataParser;
using System.Windows.Media.Animation;
using DashCamGPSView.Controls;
using System.Runtime.CompilerServices;
using MkZ.Tools;
using MkZ.WPF;
using MkZ.WPF.PropertyGrid;

namespace DashCamGPSView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IVideoPlayer
    {
        private DashCamFileInfo _dashCamFileInfo;
        private AppConfig AppConfig = new AppConfig();

        //this action needed to Update View Once file is loaded
        private Action<IVideoPlayer, PlayerControlSettings, bool> VideoStartedPostAction = (player, config, reset) => { };

        public MainWindow()
        {
            InitializeComponent();

            waitScreen.Show(RepeatBehavior.Forever);
            speedGauge.Draggable(true, new Thickness(-10));
            compass.Draggable(true, new Thickness(-10));

            playerF.Volume = AppConfig.PlayerF.SoundVolume;
            playerR.Volume = 0;
            playerI.Volume = 0;

            playerR.IsFlipHorizontally = true;
            playerI.IsFlipHorizontally = true;

            treeGroups.TreeItemDoubleClickAction = (video) =>
            {
                PlayFile(video._dashCamFileInfo);
            };

            treeGroups.OpenFileAction = () =>
            {
                OpenVideoFile();
            };

            treeGroups.ExportGPSAction = (infos) =>
            {
                ExportGPSData(infos);
            };

            treeGroups.FileTreeUpdatedAction = (deletedFiles) =>
            {
                ClosePayer();
            };

            maxScreen.CloseAction = (position, state, volume) => CloseMaximizedPlayer(position, state, volume);
            playerF.MaximizeAction = () => MaximizePlayer(playerF);
            playerR.MaximizeAction = () => MaximizePlayer(playerR);
            playerI.MaximizeAction = () => MaximizePlayer(playerI);

            playerF.VideoStarted = (player) => 
            { 
                UpdateGpsInfo(false);
                bool isFrontPlayerOnly = string.IsNullOrWhiteSpace(playerR.FileName);
                VideoStartedPostAction(player, AppConfig.PlayerF, isFrontPlayerOnly); //reset now if there is no R player
                VideoStarted(player);
                waitScreen.Hide(); 
            };
            playerR.VideoStarted = (player) => { VideoStartedPostAction(player, AppConfig.PlayerR, false); };
            playerI.VideoStarted = (player) => { VideoStartedPostAction(player, AppConfig.PlayerI, false); };

            playerF.VideoEnded = (player) => 
            { 
                VideoEndedPostAction(player, AppConfig.PlayerF); 
                if (chkAutoPlay.IsChecked.Value) 
                    PlayNext(); 
            };
            playerR.VideoEnded = (player) => VideoEndedPostAction(player, AppConfig.PlayerR);
            playerI.VideoEnded = (player) => VideoEndedPostAction(player, AppConfig.PlayerI);

            playerF.LeftButtonClick = TogglePlayPauseState;
            playerR.LeftButtonClick = TogglePlayPauseState;
            playerI.LeftButtonClick = TogglePlayPauseState;

            playerF.LeftButtonDoubleClick = () => MaximizePlayer(playerF);
            playerR.LeftButtonDoubleClick = () => MaximizePlayer(playerR);
            playerI.LeftButtonDoubleClick = () => MaximizePlayer(playerI);

            playerF.VideoFailed = (e, player) => VideoFailed(e, player);
            playerR.VideoFailed = (e, player) => VideoFailed(e, player);
            playerI.VideoFailed = (e, player) => VideoFailed(e, player);

            playerF.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); }; //delegate property changes from player

            statusBar.OnVideoPositionChanged = (position) => { UpdateGpsInfo(); };
        }

        private void VideoEndedPostAction(IVideoPlayer player, PlayerControlSettings config)
        {
            config.SoundVolume = player.Volume;
            config.ZoomState = player.ZoomStateGet();
            config.FlipHorizontally = player.IsFlipHorizontally;
            config.ScrollOffsetY = player.ScrollOffsetY;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Left = Settings.Default.InitialLocation.X;
            //this.Top = Settings.Default.InitialLocation.Y;
            //this.WindowState = WindowState.Maximized;

            LoadState();

            if (File.Exists(AppConfig.LastSelectedFileName))
            {
                DashCamFileTree groups = new DashCamFileTree(AppConfig.LastSelectedFileName, AppConfig.SpeedUnits);
                VideoFile v = treeGroups.LoadTree(groups, AppConfig.LastSelectedFileName);
                if (v != null && v._dashCamFileInfo.HasGpsInfo) //initial location
                {
                    MainMap.Position = v._dashCamFileInfo.Position(0);
                    MainMap.Zoom = 16;
                }

                PlayFile(v._dashCamFileInfo);
                Pause();
            }

            //FIRST time ONLY - fit width after file opened
            //I need <reset> to remove this action after all controls adjusted
            VideoStartedPostAction = (player, config, reset) =>
            {
                config.RestoreTo(player, true);

                if (_dashCamFileInfo.HasGpsInfo)
                {
                    MainMap.Position = _dashCamFileInfo.Position(0);
                    MainMap.Zoom = 16;
                }

                //reset this action until next time
                if (reset)
                    VideoStartedPostAction = (p, c, r) => { };
            };

            waitScreen.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClosePayer();
            SaveState();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RearrangeSplitters();
        }

        private void LoadState()
        {
            AppConfig.Load();

            AppConfig.MainWindowState.RestoreTo(this);
            //this.WindowState = WindowState.Normal; //always normal - to position on correct screen for maximize

            //Right Panel
            if (AppConfig.RightPanel.SplitterOffset.Value != 0)
                mapColumn.Width = AppConfig.RightPanel.SplitterOffset.GetGridLength();
            //Speed Chart
            if (AppConfig.SpeedChart.SplitterOffset.Value != 0)
                rowSpeedGraph.Height = AppConfig.SpeedChart.SplitterOffset.GetGridLength();
            //Main splitter
            if (AppConfig.PlayerF.SplitterOffset.Value != 0)
                rowPlayerF.Height = AppConfig.PlayerF.SplitterOffset.GetGridLength();
            //Rear View
            if (AppConfig.PlayerR.SplitterOffset.Value != 0)
                columnPlayerR.Width = AppConfig.PlayerR.SplitterOffset.GetGridLength();
            //GPS Info
            //if (AppConfig.GpsInfo.SplitterOffset.Value != 0)
            //    rowGpsInfo.Height = new GridLength(AppConfig.GpsInfo.SplitterOffset, GridUnitType.Pixel);
            //Map Info
            //if (AppConfig.GpsMap.SplitterOffset.Value != 0)
            //    rowMaps.Height = AppConfig.GpsMap.SplitterOffset.GetGridLength();

            LoadPlayersState();
        }

        private void RearrangeSplitters()
        {
            mapColumn.Width = new GridLength(1, GridUnitType.Star);
            //Speed Chart
            rowSpeedGraph.Height = new GridLength(1, GridUnitType.Star);
            //Main splitter
            rowPlayerF.Height = new GridLength(5, GridUnitType.Star);
            //Rear View
            columnPlayerR.Width = new GridLength(2, GridUnitType.Star);
            //Rear View
            columnPlayerI.Width = new GridLength(1, GridUnitType.Star);
            //GPS Info
            columnGpsInfo.Width = new GridLength(400, GridUnitType.Pixel);
            //Map Info
            //rowMaps.Height = new GridLength(8, GridUnitType.Star);
        }

        private void LoadPlayersState()
        {
            AppConfig.PlayerF.RestoreTo(playerF, true);
            AppConfig.PlayerI.RestoreTo(playerI, false);
            AppConfig.PlayerR.RestoreTo(playerR, false);
        }

        private void SaveState()
        {
            if (_dashCamFileInfo != null)
                AppConfig.SpeedUnits = _dashCamFileInfo.SpeedUnits.ToString();

            AppConfig.MainWindowState.CopyFrom(this);

            SavePlayersState();

            //Right Panel
            AppConfig.RightPanel.SplitterOffset.SetGridLength(mapColumn);
            //Speed Chart
            AppConfig.SpeedChart.SplitterOffset.SetGridLength(rowSpeedGraph);
            //Main splitter
            AppConfig.PlayerF.SplitterOffset.SetGridLength(rowPlayerF);
            //Rear View
            AppConfig.PlayerR.SplitterOffset.SetGridLength(columnPlayerR);
            //GPS Info
            AppConfig.GpsInfo.SplitterOffset.SetGridLength(columnGpsInfo);
            //Map Info
            //AppConfig.GpsMap.SplitterOffset.SetGridLength(rowMaps);

            AppConfig.Save();
        }

        private void SavePlayersState()
        {
            AppConfig.PlayerF.CopyFrom(playerF);
            AppConfig.PlayerI.CopyFrom(playerI);
            AppConfig.PlayerR.CopyFrom(playerR);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(maxScreen.Visibility == Visibility.Visible)
            {
                ProcessKeyDown(maxScreen.sliProgress, maxScreen._player, e, maxScreen.TogglePlayPauseState);
            }
            else
            {
                ProcessKeyDown(statusBar.sliProgress, this.playerF, e, this.TogglePlayPauseState);
            }
        }

        public static void ProcessKeyDown(Slider s, VideoPlayerControl player, KeyEventArgs e, Action processSpaceKey)
        {
            if (e.Key == Key.Space)
            {
                processSpaceKey();
                e.Handled = true;
            }
            else if (e.Key == Key.Up || e.Key == Key.VolumeUp)
            {
                player.Volume += 0.1 * player.Volume;
                e.Handled = true;
            }
            else if (e.Key == Key.Down || e.Key == Key.VolumeDown)
            {
                player.Volume -= 0.1 * player.Volume;
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                s.Value -= s.SmallChange;
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                s.Value += s.SmallChange;
                e.Handled = true;
            }
        }

        #region IVideoPlayer

        public IVideoPlayer MainPlayer { get { return this; } }

        public Action<IVideoPlayer> VideoStarted { get; set; } = (player) => { };

        public MediaState MediaState { get { return playerF.MediaState; } }
        public string FileName { get { return playerF.FileName; } }
        public double SpeedRatio { get { return playerF.SpeedRatio; } set { playerF.SpeedRatio = playerR.SpeedRatio = playerI.SpeedRatio = value; } }
        public double Volume { get { return playerF.Volume; } set { playerF.Volume = value; } }
        public TimeSpan Position { get { return playerF.Position; } set { playerF.Position = playerR.Position = playerI.Position = value; UpdateGpsInfo(); } }
        public Size NaturalSize { get { return playerF.NaturalSize; } }
        public double NaturalDuration { get { return playerF.NaturalDuration; } }

        public void Play()
        {
            playerF.Play();
            playerR.Play();
            playerI.Play();
        }

        public void Pause()
        {
            playerF.Pause();
            playerR.Pause();
            playerI.Pause();
        }

        public void Stop()
        {
            playerF.Stop();
            playerR.Stop();
            playerI.Stop();
        }

        public void TogglePlayPauseState()
        {
            if (MediaState == MediaState.Pause)
                Play();
            else if (MediaState == MediaState.Play)
                Pause();
        }
        
        public void ZoomStateSet(eZoomState zoom, bool adjustScroll)
        {
            playerF.ZoomStateSet(zoom, adjustScroll);
        }
        public eZoomState ZoomStateGet()
        {
            return playerF.ZoomStateGet();
        }

        public bool IsFlipHorizontally { get => playerF.IsFlipHorizontally; set => playerF.IsFlipHorizontally = value; }
        public double ScrollOffsetY { get => playerF.ScrollOffsetY; set => playerF.ScrollOffsetY = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private bool _bMapWasCollapsed = false;
        private bool _bRearViewWasCollapsed = false;
        private void PlayFile(DashCamFileInfo fileInfo, double startFrom = 0)
        {
            VideoFile prevFile = treeGroups.FindPrevFile(fileInfo.FileNameFront);
            if(prevFile == null || (_dashCamFileInfo != null && prevFile._dashCamFileInfo.FileNameFront != _dashCamFileInfo.FileNameFront))
            {
                MainMap.SetRouteAndCar(null); //reset route 
            }

            gpsInfo.UpdateInfo(null, -1); //reset GPS Info control

            if (_dashCamFileInfo != null)
                SavePlayersState();

            _dashCamFileInfo = new DashCamFileInfo(fileInfo, AppConfig.SpeedUnits);

            txtFileName.Text = _dashCamFileInfo.FileNameFront;
            playerF.Open(_dashCamFileInfo.FileNameFront, playerF.Volume);
            playerR.Open(_dashCamFileInfo.FileNameRear, 0);
            playerI.Open(_dashCamFileInfo.FileNameInside, 0);

            graphSpeedInfo.SetGpsInfo(_dashCamFileInfo.GpsInfo);

            if (_dashCamFileInfo.HasGpsInfo)
            {
                MainMap.SetRouteAndCar(_dashCamFileInfo);
                UpdateGpsInfo();

                if (_bMapWasCollapsed)// && mapColumn.Width.Value < 300)
                {
                    _bMapWasCollapsed = false;
                    MainMap.Zoom = 16;
                    //GridLengthAnimation.AnimateColumn(mapColumn, mapColumn.Width, 500);
                    //select file AFTER map is expanded
                    GridLengthAnimation.AnimateRow(rowMap, new GridLength(5, GridUnitType.Star), 500, () => treeGroups.SelectFile(_dashCamFileInfo.FileNameFront));
                    GridLengthAnimation.AnimateRow(rowFilesTree, new GridLength(1, GridUnitType.Star));
                    GridLengthAnimation.AnimateRow(rowSpeedGraph, new GridLength(1, GridUnitType.Star));
                }
            }
            else //no GPS info
            {
                //MainMap.Position = new PointLatLng(first.Latitude, first.Longitude);
                if (!_bMapWasCollapsed)// && mapColumn.Width.Value > 300)
                {
                    _bMapWasCollapsed = true;
                    MainMap.Zoom = 2;

                    GridLengthAnimation.AnimateRow(rowMap, new GridLength(0));
                    //GridLengthAnimation.AnimateRow(rowGpsInfo, new GridLength(0));
                    GridLengthAnimation.AnimateRow(rowSpeedGraph, new GridLength(0));
                }
            }

            if(File.Exists(_dashCamFileInfo.FileNameRear))
            {
                if (_bRearViewWasCollapsed)
                {
                    _bRearViewWasCollapsed = false;
                    GridLengthAnimation.AnimateRow(rowPlayerF, new GridLength(5, GridUnitType.Star));
                    GridLengthAnimation.AnimateRow(rowPlayerR, new GridLength(3, GridUnitType.Star));
                }
            }
            else
            {
                if(!_bRearViewWasCollapsed)
                {
                    _bRearViewWasCollapsed = true;
                    GridLengthAnimation.AnimateRow(rowPlayerF, new GridLength(5, GridUnitType.Star));
                    GridLengthAnimation.AnimateRow(rowPlayerR, new GridLength(0));
                }
            }

            treeGroups.SelectFile(_dashCamFileInfo.FileNameFront);

            playerF.Play();
            playerR.Play();
            playerI.Play();

            LoadPlayersState();
            AppConfig.LastSelectedFileName = playerF.FileName;
        }

        private void ClosePayer()
        {
            playerF.Close();
            playerR.Close();
            playerI.Close();

            MainMap.SetRouteAndCar(null);
        }

        private void PlayNext()
        {
            Stop();

            VideoFile video = treeGroups.FindNextFile(_dashCamFileInfo.FileNameFront);
            if (video == null || !File.Exists(video._dashCamFileInfo.FileNameFront))
                return;

            PlayFile(video._dashCamFileInfo);
        }

        private void PlayPrev()
        {
            VideoFile video = treeGroups.FindPrevFile(_dashCamFileInfo.FileNameFront); 
            if (video == null || !File.Exists(video._dashCamFileInfo.FileNameFront))
              return;

            PlayFile(video._dashCamFileInfo);
        }

        private void OpenVideoFile()
        {
            waitScreen.Show();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mp4;*.mpg;*.mpeg)|*.mp3;*.mp4;*.mpg;*.mpeg;*.webm|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                //Task myTask = Task.Factory.StartNew(() => { });
                //myTask.Wait();

                DashCamFileTree groups = new DashCamFileTree(openFileDialog.FileName, AppConfig.SpeedUnits);
                VideoFile selected =  treeGroups.LoadTree(groups, openFileDialog.FileName);

                MainMap.SetRouteAndCar(null); //reset

                PlayFile(selected._dashCamFileInfo);

                if (_dashCamFileInfo.HasGpsInfo)
                    MainMap.Zoom = 16;
            }
            else
            {
                waitScreen.Hide();
            }
        }

        private void ExportGPSData(List<DashCamFileInfo> infos)
        {
            if (infos == null || infos.Count == 0)
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Google GPS Format (Extended KML) (*.kml)|*.kml|Google GPS Format (Simple KML) (*.kml)|*.kml|GPS Exchange Format (GPX) (*.gpx)|*.gpx";
            saveFileDialog.FilterIndex = 1;
            string baseFileName = System.IO.Path.GetFileNameWithoutExtension(infos[0].FileNameFront);
            saveFileDialog.FileName = string.Format("TrackData_{0}.kml", baseFileName);

            if (saveFileDialog.ShowDialog() == true)
            {
                List<GpsPointData> list = new List<GpsPointData>();
                foreach (DashCamFileInfo info in infos)
                {
                    if(info.HasGpsInfo)
                        list.AddRange(info.GpsInfo);
                }

                try
                {
                    ExportUtils.SaveGPSData(list, saveFileDialog.FilterIndex, saveFileDialog.FileName);
                }
                catch (Exception err)
                {
                    MessageBox.Show(this, err.Message, "Error");
                }            
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenVideoFile();
        }

        private void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            VideoFile video = null;
            e.CanExecute = false;
            if (_dashCamFileInfo != null)
            {
                video = treeGroups.FindNextFile(_dashCamFileInfo.FileNameFront);
                if(video != null)
                    e.CanExecute = File.Exists(video._dashCamFileInfo.FileNameFront);
            }
        }

        private void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayNext();
        }

        private void Prev_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            VideoFile video = null;
            e.CanExecute = false;
            if (_dashCamFileInfo != null)
            {
                video = treeGroups.FindPrevFile(_dashCamFileInfo.FileNameFront);
                if(video != null)
                    e.CanExecute = File.Exists(video._dashCamFileInfo.FileNameFront);
            }
        }

        private void Prev_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayPrev();
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = playerF!= null?playerF.Play_CanExecute:false;
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Play();
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = playerF != null?(playerF.MediaState == MediaState.Play):false;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = playerF != null ? (playerF.MediaState == MediaState.Play) || (playerF.MediaState == MediaState.Pause) : false; 
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Stop();
        }

        private void MaximizePlayer(VideoPlayerControl player)
        {
            maxScreen.ShowWithControl(player, playerF.Volume);
            Pause_Executed(this, null);
        }

        private void CloseMaximizedPlayer(double position, MediaState state, double volume)
        {
            playerF.Volume = volume;
            playerR.Volume = 0;
            playerI.Volume = 0;

            playerF.Position = TimeSpan.FromSeconds(position);
            playerR.Position = TimeSpan.FromSeconds(position);
            playerI.Position = TimeSpan.FromSeconds(position);

            //sliProgress.Value = position;

            Play_Executed(this, null);
            if (state == MediaState.Stop)
                Stop_Executed(this, null);
            if (state == MediaState.Pause)
                Pause_Executed(this, null);

            UpdateGpsInfo();
        }

        private PointLatLng _lastValidPosition;
        private void UpdateGpsInfo(bool updateSlider = true)
        {
            if (_dashCamFileInfo == null)
                return;

            int idx = -1;
            if (_dashCamFileInfo.HasGpsInfo)
            {
                double seconds = playerF.Position.TotalSeconds;
                idx = _dashCamFileInfo.FindGpsInfoIdx(seconds, playerF.NaturalDuration);
                speedGauge.Visibility = Visibility.Visible;
                compass.Visibility = Visibility.Visible;

                if (idx >= 0 && idx < _dashCamFileInfo.GpsInfo.Count)
                {
                    speedGauge.SpeedUnits = _dashCamFileInfo.SpeedUnits.ToString();
                    speedGauge.Speed = _dashCamFileInfo.GetSpeed(idx).ToString("0");
                    compass.SetDirection(_dashCamFileInfo[idx].Course, _dashCamFileInfo.GetSpeed(idx));
                    _txtGpsInfo.Text = string.Format("{0} of {1}\nSpeed: {2:0.0} {3}\nCourse: {4:0.0}°", 
                        idx, _dashCamFileInfo.GpsInfo.Count, 
                        _dashCamFileInfo.GetSpeed(idx), speedGauge.SpeedUnits,
                        _dashCamFileInfo[idx].Course
                        );

                    _lastValidPosition = _dashCamFileInfo.Position(idx);
                }
                else
                {
                    speedGauge.Speed = "---";
                    compass.SetDirection(0, false);
                    _txtGpsInfo.Text = "---";
                }

                gpsInfo.UpdateInfo(_dashCamFileInfo, idx);

                MainMap.UpdateRouteAndCar(_lastValidPosition, idx);
            }
            else
            {
                //speedGauge.DialText = "Speed Mph";
                speedGauge.Speed = "?";
                _txtGpsInfo.Text = "No GPS info";
                speedGauge.Visibility = Visibility.Hidden;
                compass.Visibility = Visibility.Hidden;
            }

            graphSpeedInfo.SetCarPosition(idx);
        }

        private void GridSplitter1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            playerF.ZoomState = playerF.ZoomState;
            playerI.ZoomState = playerI.ZoomState;
            playerR.ZoomState = playerR.ZoomState;

            SavePlayersState();
        }

        private void Screenshot_Click(object sender, RoutedEventArgs e)
        {
            GpsFileFormat format = GpsFileFormat.Unkn;
            string fileName = @"C:\Temp\Screenshot.png";
            if (_dashCamFileInfo != null)
            {
                fileName = _dashCamFileInfo.FileNameFront;
                format = _dashCamFileInfo.GpsFileFormat;
            }

            Tools.Tools.Screenshot(format, fileName, playerF.Position, this);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptionsEx(this, AppConfig, 
                "Settings", null, "Settings", "Clock Configuration", "Clock Font");
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            RecreateMediaElements();
        }

        private void RecreateMediaElements()
        {
            MediaState state = playerF.MediaState;
            double position = statusBar.sliProgress.Value;
            AppConfig.PlayerF.SoundVolume = playerF.Volume;

            playerF.RecreateMediaElement(false);
            playerR.RecreateMediaElement(true);
            playerI.RecreateMediaElement(true);
            if (_dashCamFileInfo != null)
            {
                PlayFile(_dashCamFileInfo, position); //load file - move to specific position
                if (state != MediaState.Play) //pause if was not playing
                {
                    Pause_Executed(this, null);
                }
            }
        }

        private bool VideoFailed(ExceptionRoutedEventArgs e, MediaElement player)
        {
            waitScreen.Hide();
            MessageBox.Show("Media Failed: " + e.ErrorException.Message +
                "\nMediaState: " + playerF.MediaState +
                "\nSource: " + player.Source +
                "\n" + e.ErrorException,
                "Media Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            return true;
        }
    }
}
