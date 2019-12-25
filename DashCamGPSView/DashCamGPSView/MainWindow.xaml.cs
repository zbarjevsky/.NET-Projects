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

namespace DashCamGPSView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool userIsDraggingSlider = false;

        private DashCamFileInfo _dashCamFileInfo;
        DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            waitScreen.Show(RepeatBehavior.Forever);

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += timer_Tick;

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

            maxScreen.CloseAction = (player) => CloseMaximizedPlayer(player);
            playerF.MaximizeAction = () => MaximizePlayer(playerF);
            playerR.MaximizeAction = () => MaximizePlayer(playerR);

            playerF.VideoStarted = () => { waitScreen.Hide(); };

            playerF.VideoEnded = () => { if (chkAutoPlay.IsChecked.Value) PlayNext(); };

            playerF.LeftButtonClick = TogglePlayPauseState;
            playerR.LeftButtonClick = TogglePlayPauseState;

            playerF.LeftButtonDoubleClick = () => MaximizePlayer(playerF);
            playerR.LeftButtonDoubleClick = () => MaximizePlayer(playerR);

            playerF.VideoFailed = (e, player) => VideoFailed(e, player);
            playerR.VideoFailed = (e, player) => VideoFailed(e, player);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = Settings.Default.InitialLocation.X;
            this.Top = Settings.Default.InitialLocation.Y;
            this.WindowState = WindowState.Maximized;

            waitScreen.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClosePayer();
            Settings.Default.SoundVolume = playerF.Volume;
            Settings.Default.InitialLocation = new System.Drawing.Point((int)this.Left, (int)this.Top);
            Settings.Default.Save();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(maxScreen.Visibility == Visibility.Visible)
            {
                ProcessKeyDown(maxScreen.sliProgress, maxScreen.Player, e, maxScreen.TogglePlayPauseState);
            }
            else
            {
                ProcessKeyDown(this.sliProgress, this.playerF, e, this.TogglePlayPauseState);
            }
        }

        public static void ProcessKeyDown(Slider s, VideoPlayer player, KeyEventArgs e, Action processSpaceKey)
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

        private void TogglePlayPauseState()
        {
            if (playerF.MediaState == MediaState.Pause)
                Play_Executed(this, null);
            else if (playerF.MediaState == MediaState.Play)
                Pause_Executed(this, null);
        }

        private bool _bMapWasCollapsed = false;
        private void PlayFile(string fileName, double startFrom = 0)
        {
            string prev = treeGroups.FindPrevFile(fileName);
            if(_dashCamFileInfo != null && prev != _dashCamFileInfo.FrontFileName)
            {
                MainMap.SetRouteAndCar(null); //reset route 
            }

            gpsInfo.UpdateInfo(null); //reset GPS Info control

            treeGroups.SelectFile(fileName);

            _dashCamFileInfo = new DashCamFileInfo(fileName);

            txtFileName.Text = _dashCamFileInfo.FrontFileName;
            playerF.Open(_dashCamFileInfo.FrontFileName, playerF.Volume);
            playerR.Open(_dashCamFileInfo.BackFileName, 0);

            if (_dashCamFileInfo.GpsInfo != null && _dashCamFileInfo.GpsInfo.Count > 0)
            {
                MainMap.SetRouteAndCar(_dashCamFileInfo);
                UpdateGpsInfo();

                if (_bMapWasCollapsed)// && mapColumn.Width.Value < 300)
                {
                    _bMapWasCollapsed = false;
                    MainMap.Zoom = 16;
                    //GridLengthAnimation.AnimateColumn(mapColumn, mapColumn.Width, 500);
                    GridLengthAnimation.AnimateRow(rowMaps, rowMaps.Height, new GridLength(5, GridUnitType.Star),
                        () => { rowMaps.Height = new GridLength(5, GridUnitType.Star); });
                    GridLengthAnimation.AnimateRow(rowGpsInfo, rowGpsInfo.Height, new GridLength(2, GridUnitType.Star),
                        () => { rowGpsInfo.Height = new GridLength(2, GridUnitType.Star); });
                }
            }
            else //no GPS info
            {
                //MainMap.Position = new PointLatLng(first.Latitude, first.Longitude);
                if (!_bMapWasCollapsed)// && mapColumn.Width.Value > 300)
                {
                    _bMapWasCollapsed = true;
                    MainMap.Zoom = 2;

                    GridLengthAnimation.AnimateRow(rowMaps, rowMaps.Height, new GridLength(0));
                    GridLengthAnimation.AnimateRow(rowGpsInfo, rowGpsInfo.Height, new GridLength(0));
                }
            }

            if(File.Exists(_dashCamFileInfo.BackFileName))
            {
                GridLengthAnimation.AnimateRow(rowRearView, rowRearView.Height, new GridLength(3, GridUnitType.Star));
            }
            else
            {
                GridLengthAnimation.AnimateRow(rowRearView, rowRearView.Height, new GridLength(0));
            }

            playerF.Play();
            playerR.Play();
            _timer.Start();

            if (startFrom > 1)
            {
                sliProgress.Value = startFrom;
                sliProgress_ValueChanged(sliProgress, null);
            }
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
            openFileDialog.Filter = "Media files (*.mp3;*.mp4;*.mpg;*.mpeg)|*.mp3;*.mp4;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Tools.Tools.ForceUIToUpdate();

                //Task myTask = Task.Factory.StartNew(() => { });
                //myTask.Wait();

                DashCamFileTree groups = new DashCamFileTree(openFileDialog.FileName);
                treeGroups.LoadTree(groups, openFileDialog.FileName);

                MainMap.SetRouteAndCar(null); //reset

                PlayFile(openFileDialog.FileName);
                
                playerF.FitWidth();
                playerR.FitWidth();

                if (_dashCamFileInfo.GpsInfo != null && _dashCamFileInfo.GpsInfo.Count > 0)
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
                    if(info.GpsInfo != null && info.GpsInfo.Count > 0)
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
            e.CanExecute = playerF.Play_CanExecute;
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playerF.Play();
            playerR.Play();
            _timer.Start();
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (playerF.MediaState == MediaState.Play);
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playerF.Pause();
            playerR.Pause();
            _timer.Stop();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (playerF.MediaState == MediaState.Play) || (playerF.MediaState == MediaState.Pause); 
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playerF.Stop();
            playerR.Stop();
        }

        private void MaximizePlayer(VideoPlayer player)
        {
            maxScreen.ShowWithControl(player, playerF.Volume);
            Pause_Executed(this, null);
        }

        private void CloseMaximizedPlayer(VideoPlayer player)
        {
            playerF.CopyState(player, player.Volume, false);
            UpdateGpsInfo();
            _timer.Start();
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
            Pause_Executed(sender, null);
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            //playerF.Position = TimeSpan.FromSeconds(sliProgress.Value);
            //playerR.Position = TimeSpan.FromSeconds(sliProgress.Value);
            //UpdateGpsInfo();
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan tsPos = TimeSpan.FromSeconds(sliProgress.Value);
            TimeSpan tsMax = TimeSpan.FromSeconds(playerF.NaturalDuration);

            playerF.Position = tsPos;
            playerR.Position = tsPos;
            
            lblProgressStatus.Text = tsPos.ToString(@"hh\:mm\:ss") + "/" + tsMax.ToString(@"hh\:mm\:ss");

            UpdateGpsInfo();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(playerF.MediaState == MediaState.Play)
                UpdateGpsInfo();
        }

        private void UpdateGpsInfo()
        {
            if (_dashCamFileInfo == null)
                return;

            sliProgress.Minimum = 0;

            if (playerF.NaturalDuration != 0)
            {
                sliProgress.Maximum = playerF.NaturalDuration;
                sliProgress.Value = playerF.Position.TotalSeconds;
                if (sliProgress.Maximum >= 60)
                    sliProgress.SmallChange = 1;
                else //if less than minute - have 60 tics
                    sliProgress.SmallChange = sliProgress.Maximum / 60.0;

                sliProgress.LargeChange = sliProgress.Maximum / 10.0;
            }

            txtGPSInfo.Text = _dashCamFileInfo.GetLocationInfoForTime(playerF.Position.TotalSeconds);

            int idx = _dashCamFileInfo.FindGpsInfo(playerF.Position.TotalSeconds);
            if (_dashCamFileInfo.GpsInfo != null && _dashCamFileInfo.GpsInfo.Count > idx)
            {
                speedGauge.Visibility = Visibility.Visible;
                speedGauge.Speed = _dashCamFileInfo.GpsInfo[idx].SpeedMph;
                //speedGauge.DialText = string.Format("{0:0} mph", _dashCamFileInfo.GpsInfo[idx].SpeedMph);

                gpsInfo.UpdateInfo(_dashCamFileInfo.GpsInfo[idx], _dashCamFileInfo.TimeZone);
                
                PointLatLng currentPosition = new PointLatLng(_dashCamFileInfo.GpsInfo[idx].Latitude, _dashCamFileInfo.GpsInfo[idx].Longitude);
                MainMap.UpdateRouteAndCar(currentPosition, idx);
            }
            else
            {
                //speedGauge.DialText = "Speed Mph";
                speedGauge.Speed = 0;
                speedGauge.Visibility = Visibility.Hidden;
            }
        }

        private void GridSplitter1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            playerF.FitWidth();
            playerR.FitWidth();
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
            double position = sliProgress.Value;
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
            MessageBox.Show("Media Failed: " + e.ErrorException.Message +
                "\nMediaState: " + playerF.MediaState +
                "\nSource: " + player.Source +
                "\n" + e.ErrorException,
                "Media Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            return true;
        }
    }
}
