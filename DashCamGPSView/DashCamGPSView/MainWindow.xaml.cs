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
using MZ.Tools;
using MZ.WPF;

namespace DashCamGPSView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IVideoPlayer
    {
        private DashCamFileInfo _dashCamFileInfo;

        //this action needed to Update View Once file is loaded
        private Action<IVideoPlayer, bool> VideoStartedPostAction = (player, reset) => { };

        public MainWindow()
        {
            InitializeComponent();

            waitScreen.Show(RepeatBehavior.Forever);
            speedGauge.Draggable(true, new Thickness(-10));

            playerF.Volume = Settings.Default.SoundVolume;
            playerR.Volume = 0;

            playerR.IsFlipHorizontally = true;
            
            treeGroups.TreeItemDoubleClickAction = (fileName) =>
            {
                PlayFile(fileName);
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

            playerF.VerticalOffset = Settings.Default.FrontPlayerVerticalOffset;
            playerR.VerticalOffset = Settings.Default.RearPlayerVerticalOffset;

            maxScreen.CloseAction = (position, state, volume) => CloseMaximizedPlayer(position, state, volume);
            playerF.MaximizeAction = () => MaximizePlayer(playerF);
            playerR.MaximizeAction = () => MaximizePlayer(playerR);

            playerF.VideoStarted = (player) => 
            { 
                UpdateGpsInfo(false);
                bool isFrontPlayerOnly = string.IsNullOrWhiteSpace(playerR.FileName);
                VideoStartedPostAction(player, isFrontPlayerOnly); //reset now if there is no R player
                VideoStarted(player);
                waitScreen.Hide(); 
            };
            playerR.VideoStarted = (player) => { VideoStartedPostAction(player, true); };

            playerF.VideoEnded = () => { if (chkAutoPlay.IsChecked.Value) PlayNext(); };

            playerF.LeftButtonClick = TogglePlayPauseState;
            playerR.LeftButtonClick = TogglePlayPauseState;

            playerF.LeftButtonDoubleClick = () => MaximizePlayer(playerF);
            playerR.LeftButtonDoubleClick = () => MaximizePlayer(playerR);

            playerF.VideoFailed = (e, player) => VideoFailed(e, player);
            playerR.VideoFailed = (e, player) => VideoFailed(e, player);

            playerF.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); }; //delegate property changes from player

            statusBar.OnVideoPositionChanged = (position) => { UpdateGpsInfo(); };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = Settings.Default.InitialLocation.X;
            this.Top = Settings.Default.InitialLocation.Y;
            this.WindowState = WindowState.Maximized;

            if (File.Exists(Settings.Default.LastFileName))
            {
                DashCamFileTree groups = new DashCamFileTree(Settings.Default.LastFileName);
                VideoFile v =treeGroups.LoadTree(groups, Settings.Default.LastFileName);
                if (v != null && v._dashCamFileInfo.HasGpsInfo) //initial location
                {
                    MainMap.Position = v._dashCamFileInfo.Position(0);
                    MainMap.Zoom = 16;
                }
            }

            //FIRST time ONLY - fit width after file opened
            //I need <reset> to remove this action after both controls adjusted
            VideoStartedPostAction = (player, reset) =>
            {
                player.FitWidth(false);

                if (_dashCamFileInfo.HasGpsInfo)
                {
                    MainMap.Position = _dashCamFileInfo.Position(0);
                    MainMap.Zoom = 16;
                }

                //reset this action until next time
                if (reset)
                    VideoStartedPostAction = (p, r) => { };
            };

            waitScreen.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClosePayer();
            Settings.Default.SoundVolume = playerF.Volume;

            Settings.Default.FrontPlayerVerticalOffset = playerF.VerticalOffset;
            Settings.Default.RearPlayerVerticalOffset = playerR.VerticalOffset;

            if (_dashCamFileInfo != null)
                Settings.Default.SpeedUnits = _dashCamFileInfo.SpeedUnits.ToString();

            Settings.Default.InitialLocation = new System.Drawing.Point((int)this.Left, (int)this.Top);
            Settings.Default.Save();
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
        public double SpeedRatio { get { return playerF.SpeedRatio; } set { playerF.SpeedRatio = playerR.SpeedRatio = value; } }
        public double Volume { get { return playerF.Volume; } set { playerF.Volume = value; } }
        public TimeSpan Position { get { return playerF.Position; } set { playerF.Position = playerR.Position = value; UpdateGpsInfo(); } }
        public Size NaturalSize { get { return playerF.NaturalSize; } }
        public double NaturalDuration { get { return playerF.NaturalDuration; } }

        public void Play()
        {
            playerF.Play();
            playerR.Play();
        }

        public void Pause()
        {
            playerF.Pause();
            playerR.Pause();
        }

        public void Stop()
        {
            playerF.Stop();
            playerR.Stop();
        }

        public void TogglePlayPauseState()
        {
            if (MediaState == MediaState.Pause)
                Play();
            else if (MediaState == MediaState.Play)
                Pause();
        }

        public void FitWidth(bool adjustScroll)
        {
            playerF.FitWidth(adjustScroll);
            playerR.FitWidth(adjustScroll);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private bool _bMapWasCollapsed = false;
        private bool _bRearViewWasCollapsed = false;
        private void PlayFile(string fileName, double startFrom = 0)
        {
            string prevFile = treeGroups.FindPrevFile(fileName);
            if(_dashCamFileInfo != null && prevFile != _dashCamFileInfo.FrontFileName)
            {
                MainMap.SetRouteAndCar(null); //reset route 
            }

            gpsInfo.UpdateInfo(null, -1); //reset GPS Info control

            _dashCamFileInfo = new DashCamFileInfo(fileName, Settings.Default.SpeedUnits);

            txtFileName.Text = _dashCamFileInfo.FrontFileName;
            playerF.Open(_dashCamFileInfo.FrontFileName, playerF.Volume);
            playerR.Open(_dashCamFileInfo.BackFileName, 0);

            Settings.Default.LastFileName = playerF.FileName;
            Settings.Default.Save();

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
                    GridLengthAnimation.AnimateRow(rowMaps, new GridLength(5, GridUnitType.Star), 500, () => treeGroups.SelectFile(fileName));
                    GridLengthAnimation.AnimateRow(rowGpsInfo, new GridLength(2, GridUnitType.Star));
                    GridLengthAnimation.AnimateRow(rowSpeedGraph, new GridLength(1.3, GridUnitType.Star));
                }
            }
            else //no GPS info
            {
                //MainMap.Position = new PointLatLng(first.Latitude, first.Longitude);
                if (!_bMapWasCollapsed)// && mapColumn.Width.Value > 300)
                {
                    _bMapWasCollapsed = true;
                    MainMap.Zoom = 2;

                    GridLengthAnimation.AnimateRow(rowMaps, new GridLength(0));
                    GridLengthAnimation.AnimateRow(rowGpsInfo, new GridLength(0));
                    GridLengthAnimation.AnimateRow(rowSpeedGraph, new GridLength(0));
                }
            }

            if(File.Exists(_dashCamFileInfo.BackFileName))
            {
                if (_bRearViewWasCollapsed)
                {
                    _bRearViewWasCollapsed = false;
                    GridLengthAnimation.AnimateRow(rowFrontView, new GridLength(5, GridUnitType.Star));
                    GridLengthAnimation.AnimateRow(rowRearView, new GridLength(3, GridUnitType.Star));
                }
            }
            else
            {
                if(!_bRearViewWasCollapsed)
                {
                    _bRearViewWasCollapsed = true;
                    GridLengthAnimation.AnimateRow(rowFrontView, new GridLength(5, GridUnitType.Star));
                    GridLengthAnimation.AnimateRow(rowRearView, new GridLength(0));
                }
            }

            treeGroups.SelectFile(fileName);

            playerF.Play();
            playerR.Play();
        }

        private void ClosePayer()
        {
            playerF.Close();
            playerR.Close();

            MainMap.SetRouteAndCar(null);
        }

        private void PlayNext()
        {
            string fileName = treeGroups.FindNextFile(_dashCamFileInfo.FrontFileName);
            if (!File.Exists(fileName))
                return;

            PlayFile(fileName);
        }

        private void PlayPrev()
        {
            string fileName = treeGroups.FindPrevFile(_dashCamFileInfo.FrontFileName);
            if (!File.Exists(fileName))
              return;

            PlayFile(fileName);
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

                DashCamFileTree groups = new DashCamFileTree(openFileDialog.FileName);
                treeGroups.LoadTree(groups, openFileDialog.FileName);

                MainMap.SetRouteAndCar(null); //reset

                PlayFile(openFileDialog.FileName);

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
            string baseFileName = System.IO.Path.GetFileNameWithoutExtension(infos[0].FrontFileName);
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
            string fileName = null;
            if (_dashCamFileInfo != null)
                fileName = treeGroups.FindNextFile(_dashCamFileInfo.FrontFileName);

            e.CanExecute = !string.IsNullOrWhiteSpace(fileName);
        }

        private void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayNext();
        }

        private void Prev_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string fileName = null;
            if (_dashCamFileInfo != null)
                fileName = treeGroups.FindPrevFile(_dashCamFileInfo.FrontFileName);

            e.CanExecute = !string.IsNullOrWhiteSpace(fileName);
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
            
            playerF.Position = TimeSpan.FromSeconds(position);
            playerR.Position = TimeSpan.FromSeconds(position);

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

            int idx = _dashCamFileInfo.FindGpsInfo(playerF.Position.TotalSeconds, playerF.NaturalDuration);
            if (_dashCamFileInfo.HasGpsInfo)
            {
                speedGauge.Visibility = Visibility.Visible;

                if (idx >= 0 && idx < _dashCamFileInfo.GpsInfo.Count)
                {
                    speedGauge.SpeedUnits = _dashCamFileInfo.SpeedUnits.ToString();
                    speedGauge.Speed = _dashCamFileInfo.GetSpeed(idx).ToString("0");

                    _lastValidPosition = _dashCamFileInfo.Position(idx);
                }
                else
                {
                    speedGauge.Speed = "---";
                }

                gpsInfo.UpdateInfo(_dashCamFileInfo, idx);

                MainMap.UpdateRouteAndCar(_lastValidPosition, idx);
            }
            else
            {
                //speedGauge.DialText = "Speed Mph";
                speedGauge.Speed = "?";
                speedGauge.Visibility = Visibility.Hidden;
            }

            graphSpeedInfo.SetCarPosition(idx);
        }

        private void GridSplitter1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            playerF.FitWidth(false);
            playerR.FitWidth(false);
        }

        private void Screenshot_Click(object sender, RoutedEventArgs e)
        {
            GpsFileFormat format = GpsFileFormat.Unkn;
            string fileName = @"C:\Temp\Screenshot.png";
            if (_dashCamFileInfo != null)
            {
                fileName = _dashCamFileInfo.FrontFileName;
                format = _dashCamFileInfo.GpsFileFormat;
            }

            Tools.Tools.Screenshot(format, fileName, playerF.Position, this);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            RecreateMediaElements();
        }

        private void RecreateMediaElements()
        {
            MediaState state = playerF.MediaState;
            double position = statusBar.sliProgress.Value;
            Settings.Default.SoundVolume = playerF.Volume;

            playerF.RecreateMediaElement(false);
            playerR.RecreateMediaElement(true);
            if (_dashCamFileInfo != null)
            {
                PlayFile(_dashCamFileInfo.FrontFileName, position); //load file - move to specific position
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
