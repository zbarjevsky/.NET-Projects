using DashCamGPSView.Properties;
using DashCamGPSView.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl, INotifyPropertyChanged
    {
        private ScrollDragZoom _scrollDragger;

        public Action VideoEnded = () => { };
        public Action VideoStarted = () => { };

        public MediaState MediaState { get; private set; } = MediaState.Manual;

        MediaElement mePlayer = null;

        public VideoPlayer()
        {
            InitializeComponent();
            RecreateMediaElement();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        public bool Play_CanExecute 
        { 
            get
            {
                return (mePlayer != null) && (mePlayer.Source != null);
            } 
        }

        public double Volume
        {
            get { return mePlayer.Volume; }
            set { mePlayer.Volume = value; OnPropertyChanged(); }
        }

        public TimeSpan Position 
        { 
            get { return mePlayer.Position; }
            set { mePlayer.Position = value; OnPropertyChanged(); } 
        }

        public Size NaturalSize
        {
            get
            {
                if (mePlayer.Source != null)
                {
                    return new Size(mePlayer.NaturalVideoWidth, mePlayer.NaturalVideoHeight);
                }
                return new Size(1920, 1080);
            }
        }

        public double NaturalDuration
        {
            get
            {
                if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan))
                {
                    return mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                }
                return 0;
            }
        }

        public void ScrollToCenter()
        {
            _scrollDragger.ScrollToCenter();
        }

        public void Open(string fileName, double volume)
        {
            mePlayer.Source = string.IsNullOrEmpty(fileName)? null : new Uri(fileName);
            Volume = volume;
            MediaState = MediaState.Manual;
        }

        internal void Close()
        {
            Stop();
            mePlayer.Source = null;
            this.Background = Brushes.Wheat;
            MediaState = MediaState.Close;
        }

        public void Play() 
        { 
            if (mePlayer.Source != null) 
            { 
                mePlayer.Play(); 
                this.Background = Brushes.Black;
                MediaState = MediaState.Play;
            }
        }

        public void Pause()
        {
            if (mePlayer.Source != null)
            { 
                mePlayer.Pause(); 
                this.Background = Brushes.Black;
                MediaState = MediaState.Pause;
            }
        }

        public void Stop() 
        { 
            if (mePlayer.Source != null) 
            { 
                mePlayer.Stop(); 
                this.Background = Brushes.Black;
                MediaState = MediaState.Stop;
            }
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void mePlayer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        public void FitWidth()
        {
            mePlayer.Width = this.ActualWidth - 18;

            //proportionally change height
            Size sz = NaturalSize;
            mePlayer.Height = mePlayer.Width * sz.Height / sz.Width;

            ScrollToCenter();
        }

        internal void FitWindow()
        {
            if (this.ActualHeight > 18 && this.ActualWidth > 18)
            {
                mePlayer.Width = this.ActualWidth - 18;
                mePlayer.Height = this.ActualHeight - 18;
            }

            ScrollToCenter();
        }

        internal void ResizeToActualSize()
        {
            Size sz = NaturalSize;
            mePlayer.Width = sz.Width;
            mePlayer.Height = sz.Height;

            ScrollToCenter();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sometimes MediaElement crashes - black window
        /// I will replace it with the new one
        /// </summary>
        internal void RecreateMediaElement()
        {
            if (mePlayer != null)
            {
                mePlayer.Stop();
                mePlayer.Source = null;
                mePlayer.Volume = 0;
            }

            MediaState = MediaState.Manual;

            mePlayer = new MediaElement();
            mePlayer.Width = 1920;
            mePlayer.Height = 1080;
            mePlayer.LoadedBehavior = MediaState.Manual;
            mePlayer.Stretch = Stretch.Uniform;
            mePlayer.MouseWheel += mePlayer_MouseWheel;

            scrollPlayer.Content = mePlayer;

            double vOff = Settings.Default.RearPlayerVerticalOffset;
            if (_scrollDragger != null)
                vOff = _scrollDragger.VerticalOffset;

            _scrollDragger = new ScrollDragZoom(mePlayer, scrollPlayer);
            _scrollDragger.VerticalOffset = vOff;

            //refresh view when change position
            mePlayer.ScrubbingEnabled = true;

            mePlayer.MediaOpened += (s, e) => { VideoStarted(); MediaState state = GetMediaState(mePlayer); };
            mePlayer.MediaEnded += (s, e) => { VideoEnded(); };
            mePlayer.MediaFailed += (s, e) => 
            {
                e.Handled = true;
                MessageBox.Show("Media Failed: " + e.ErrorException.Message + 
                    "\nMediaState: " + GetMediaState(mePlayer) +  
                    "\nSource: "+mePlayer.Source + 
                    "\n" + e.ErrorException,
                    "Media Failed", MessageBoxButton.OK, MessageBoxImage.Error); 
            };
            mePlayer.MouseLeftButtonDown += (s, e) =>
            {
                if (MediaState == MediaState.Play)
                    Pause();
                else if (MediaState == MediaState.Pause)
                    Play();
            };
        }

        private MediaState GetMediaState(MediaElement myMedia)
        {
            FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
            object helperObject = hlp.GetValue(myMedia);
            FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            MediaState state = (MediaState)stateField.GetValue(helperObject);
            return state;
        }
    }
}
