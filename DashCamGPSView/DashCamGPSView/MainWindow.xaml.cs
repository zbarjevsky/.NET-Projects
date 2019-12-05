using DashCamGPSView.Properties;
using DashCamGPSView.Tools;
using Demo.WindowsPresentation.CustomMarkers;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = Settings.Default.InitialLocation.X;
            this.Top = Settings.Default.InitialLocation.Y;
            this.WindowState = WindowState.Maximized;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Settings.Default.InitialLocation = new System.Drawing.Point((int)this.Left, (int)this.Top);
            Settings.Default.Save();
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mp4;*.mpg;*.mpeg)|*.mp3;*.mp4;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                DashCamFileTree test = new DashCamFileTree(openFileDialog.FileName);

                _dashCamFileInfo = new DashCamFileInfo(openFileDialog.FileName);

                txtFileName.Text = _dashCamFileInfo.FrontFileName;
                playerF.Open(_dashCamFileInfo.FrontFileName);
                playerR.Open(_dashCamFileInfo.BackFileName);

                playerF.Volume = 0.5;
                playerF.UpdateVideoSize();
                playerF.Play();

                playerR.Volume = 0;
                playerR.UpdateVideoSize();
                playerR.Play();

                if(_dashCamFileInfo.GpsInfo != null && _dashCamFileInfo.GpsInfo.Count > 0)
                {
                    MainMap.Zoom = 17;
                    NmeaParser.Nmea.Rmc first = _dashCamFileInfo.GpsInfo[0] as NmeaParser.Nmea.Rmc;
                    MainMap.Position = new PointLatLng(first.Latitude, first.Longitude);
                }
                //else
                //{
                //    MainMap.Zoom = 2;
                //    MainMap.Position = new PointLatLng(first.Latitude, first.Longitude);
                //}

                mediaPlayerIsPlaying = true;
                mediaPlayerIsPaused = false;
            }
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
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
            playerF.Position = TimeSpan.FromSeconds(sliProgress.Value);
            playerR.Position = TimeSpan.FromSeconds(sliProgress.Value);
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
            sliProgress.Minimum = 0;

            if (playerF.NaturalDuration != 0)
            {
                sliProgress.Maximum = playerF.NaturalDuration;
                sliProgress.Value = playerF.Position.TotalSeconds;
            }

            txtGPSInfo.Text = _dashCamFileInfo.GetLocationInfoForTime(playerF.Position.TotalSeconds);

            NmeaParser.Nmea.Rmc inf = _dashCamFileInfo.FindGpsInfo(playerF.Position.TotalSeconds);
            if (inf != null)
            {
                compass.Direction = inf.Course;
                MainMap.Position = new PointLatLng(inf.Latitude, inf.Longitude);
            }
        }

        private void GridSplitter1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            playerF.UpdateVideoSize();
            playerR.UpdateVideoSize();
        }
    }
}
