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

namespace DashCamGPSView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool mediaPlayerIsPaused = false;
        private bool userIsDraggingSlider = false;

        private DashCamFileInfo _dashCamFileInfo;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            treeGroups.TreeItemDoubleClickAction = (fileName) => 
            {
                PlayFile(fileName);
            };

            treeGroups.OpenFileAction = () =>
            {
                OpenVideoFile();
            };

            playerF.VideoEnded = () => { PlayNext(); };
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                if (!mediaPlayerIsPlaying)
                    return;

                if (mediaPlayerIsPaused)
                    Play_Executed(this, null);
                else
                    Pause_Executed(this, null);
            }
            else if (e.Key == Key.Up || e.Key == Key.VolumeUp)
            {
                playerF.Volume += 0.1 * playerF.Volume;
            }
            else if (e.Key == Key.Down || e.Key == Key.VolumeDown)
            {
                playerF.Volume -= 0.1 * playerF.Volume;
            }
            else if (e.Key == Key.Left)
            {
                sliProgress.Value -= sliProgress.SmallChange;
            }
            else if (e.Key == Key.Right)
            {
                sliProgress.Value += sliProgress.SmallChange;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = Settings.Default.InitialLocation.X;
            this.Top = Settings.Default.InitialLocation.Y;
            this.WindowState = WindowState.Maximized;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClosePayer();
            Settings.Default.InitialLocation = new System.Drawing.Point((int)this.Left, (int)this.Top);
            Settings.Default.Save();
        }

        private void PlayFile(string fileName)
        {
            ClosePayer();
            if (!File.Exists(fileName))
                return;

            treeGroups.SelectFile(fileName);

            _dashCamFileInfo = new DashCamFileInfo(fileName);

            txtFileName.Text = _dashCamFileInfo.FrontFileName;
            playerF.Open(_dashCamFileInfo.FrontFileName, 0.5);
            playerR.Open(_dashCamFileInfo.BackFileName, 0);

            if (_dashCamFileInfo.GpsInfo != null && _dashCamFileInfo.GpsInfo.Count > 0)
            {
                MainMap.Zoom = 17;
                UpdateGpsInfo();
            }
            //else
            //{
            //    MainMap.Zoom = 2;
            //    MainMap.Position = new PointLatLng(first.Latitude, first.Longitude);
            //}

            playerF.Play();
            playerR.Play();

            mediaPlayerIsPlaying = true;
            mediaPlayerIsPaused = false;
        }

        private void ClosePayer()
        {
            playerF.Close();
            playerR.Close();
            mediaPlayerIsPlaying = false;
            mediaPlayerIsPaused = false;
            MainMap.UpdateRouteAndCar(null, 0);
        }

        private void PlayNext()
        {
            ClosePayer();

            string fileName = treeGroups.FindNextFile(_dashCamFileInfo.FrontFileName);
            if (!File.Exists(fileName))
                return;

            PlayFile(fileName);
        }

        private void PlayPrev()
        {
            ClosePayer();

            string fileName = treeGroups.FindPrevFile(_dashCamFileInfo.FrontFileName);
            if (!File.Exists(fileName))
                return;

            PlayFile(fileName);
        }

        private void OpenVideoFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mp4;*.mpg;*.mpeg)|*.mp3;*.mp4;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                DashCamFileTree groups = new DashCamFileTree(openFileDialog.FileName);
                treeGroups.LoadTree(groups, openFileDialog.FileName);
                PlayFile(openFileDialog.FileName);
                playerF.FitWidth();
                playerR.FitWidth();
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

            mediaPlayerIsPlaying = true;
            mediaPlayerIsPaused = false;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying && !mediaPlayerIsPaused;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playerF.Pause();
            playerR.Pause();

            mediaPlayerIsPaused = true;
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            playerF.Stop();
            playerR.Stop();
            mediaPlayerIsPlaying = false;
            mediaPlayerIsPaused = false;
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
            if (!mediaPlayerIsPlaying || mediaPlayerIsPaused || userIsDraggingSlider)
                return;

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
            gpsInfo.UpdateInfo(_dashCamFileInfo.GpsInfo[idx], _dashCamFileInfo.TimeZone);
            MainMap.UpdateRouteAndCar(_dashCamFileInfo, idx);
        }

        private void GridSplitter1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            playerF.FitWindow();
            playerR.FitWindow();
        }

        private void Screenshot_Click(object sender, RoutedEventArgs e)
        {
            string fileName = @"C:\Temp\Screenshot.png";
            if (_dashCamFileInfo != null)
                fileName = _dashCamFileInfo.GetScreenshotFileName();

            fileName = string.Format("{0}_at{1:0.00}.png", fileName, playerF.Position.TotalSeconds);
            Tools.Tools.SaveWindowScreenshotToFile(this, fileName);
            Process.Start(fileName);
        }
    }
}
