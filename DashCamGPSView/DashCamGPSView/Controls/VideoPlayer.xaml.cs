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

        public Action MaximizeAction = () => { };
        public Action VideoEnded = () => { };
        public Action VideoStarted = () => { };
        public Func<ExceptionRoutedEventArgs, MediaElement, bool> VideoFailed = (e, player) => true;
        public Action LeftButtonClick = () => { };
        public Action LeftButtonDoubleClick = () => { };

        public MediaElement VideoPlayerElement { get; private set; } = null;

        public MediaState MediaState { get; private set; } = MediaState.Manual;

        private string _title;
        public string Title 
        { 
            get { return _title; } 
            set 
            { 
                _title = value; 
                txtTitle.Text = value + (IsFlipHorizontally?" (Flipped)": ""); 
                OnPropertyChanged(); 
            }
        }

        public string FileName { get; private set; }

        public bool IsFlipHorizontally 
        {
            get
            {
                if (VideoPlayerElement.RenderTransform is ScaleTransform scale)
                    return  scale.ScaleX == -1; //Flip Horizontally
                return false;
            }
            set
            {
                if (VideoPlayerElement.RenderTransform is ScaleTransform scale)
                    scale.ScaleX = value ? -1 : 1; //Flip Horizontally
                Title = _title; //update flipped
                OnPropertyChanged();
            }
        }

        public double VerticalOffset
        {
            get
            {
                return _scrollDragger.VerticalOffset;
            }

            set
            {
                if (value < 1) //relative to Height
                {
                    _scrollDragger.VerticalOffset = value * scrollPlayer.ScrollableHeight;
                }
                else
                {
                    _scrollDragger.VerticalOffset = value;
                }
            }
        }

        public double Zoom
        {
            get
            {
                return _scrollDragger.Zoom;
            }

            set
            {
                _scrollDragger.Zoom = value;
            }
        }

        public VideoPlayer()
        {
            InitializeComponent();

            _scrollDragger = new ScrollDragZoom(null, scrollPlayer);

            RecreateMediaElement(false);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        public void CopyState(VideoPlayer player, double volume, bool copySource)
        {
            if(copySource)
                VideoPlayerElement.Source = player.VideoPlayerElement.Source;

            Volume = volume;
            MediaState = player.MediaState;
            Title = player.Title;
            FileName = player.FileName;
            IsFlipHorizontally = player.IsFlipHorizontally;
            Zoom = player.Zoom;
            VerticalOffset = player.VerticalOffset;

            Play();
            
            Position = player.Position;
            if (player.MediaState == MediaState.Stop)
                Stop();
            if (player.MediaState == MediaState.Pause)
                Pause();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                LeftButtonDoubleClick();
        }

        private void UserControl_PreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && (e.OriginalSource is MediaElement))
                LeftButtonClick();
        }

        public bool Play_CanExecute 
        { 
            get
            {
                return (VideoPlayerElement != null) && (VideoPlayerElement.Source != null);
            } 
        }

        public double Volume
        {
            get { return VideoPlayerElement.Volume; }
            set { VideoPlayerElement.Volume = value; OnPropertyChanged(); }
        }

        public TimeSpan Position 
        { 
            get { return VideoPlayerElement.Position; }
            set { VideoPlayerElement.Position = value; OnPropertyChanged(); } 
        }

        public Size NaturalSize
        {
            get
            {
                if (VideoPlayerElement.Source != null)
                {
                    return new Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);
                }
                return new Size(1920, 1080);
            }
        }

        public double NaturalDuration
        {
            get
            {
                if ((VideoPlayerElement.Source != null) && (VideoPlayerElement.NaturalDuration.HasTimeSpan))
                {
                    return VideoPlayerElement.NaturalDuration.TimeSpan.TotalSeconds;
                }
                return 0;
            }
        }

        public void ScrollToCenter()
        {
            _scrollDragger.ScrollToCenter();
        }

        public void Open(string fileName, double volume = 0)
        {
            FileName = fileName;
            VideoPlayerElement.Source = string.IsNullOrEmpty(fileName)? null : new Uri(fileName);
            Volume = volume;
            MediaState = MediaState.Manual;
        }

        internal void Close()
        {
            Stop();
            VideoPlayerElement.Source = null;
            FileName = "";
            this.Background = Brushes.LightGray;
            MediaState = MediaState.Close;
        }

        public void Play() 
        { 
            if (VideoPlayerElement.Source != null) 
            { 
                VideoPlayerElement.Play(); 
                this.Background = Brushes.Gray;
                MediaState = MediaState.Play;
            }
        }

        public void Pause()
        {
            if (VideoPlayerElement.Source != null)
            { 
                VideoPlayerElement.Pause(); 
                this.Background = Brushes.DarkGray;
                MediaState = MediaState.Pause;
            }
        }

        public void Stop() 
        { 
            if (VideoPlayerElement.Source != null) 
            { 
                VideoPlayerElement.Stop(); 
                this.Background = Brushes.DarkGray;
                MediaState = MediaState.Stop;
            }
        }

        public void TogglePlayPauseState()
        {
            if (MediaState == MediaState.Pause)
                Play();
            else if (MediaState == MediaState.Play)
                Pause();
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void mePlayer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        public void FitWidth(bool adjustScroll)
        {
            _scrollDragger.FitWidth(); 
            if(adjustScroll)
                ScrollToCenter();
        }

        public void OriginalSize(bool adjustScroll)
        {
            _scrollDragger.OriginalSize();
            if (adjustScroll)
                ScrollToCenter();
        }

        internal void FitWindow()
        {
            _scrollDragger.FitWindow();
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
        internal void RecreateMediaElement(bool flipHorizontally)
        {
            try
            {
                if (VideoPlayerElement != null)
                {
                    Settings.Default.SoundVolume = VideoPlayerElement.Volume;

                    VideoPlayerElement.Stop();
                    VideoPlayerElement.Source = null;
                    VideoPlayerElement.Volume = 0;
                }

                MediaState = MediaState.Manual;

                VideoPlayerElement = new MediaElement();
                VideoPlayerElement.Width = 1920;
                VideoPlayerElement.Height = 1080;
                VideoPlayerElement.LoadedBehavior = MediaState.Manual;
                VideoPlayerElement.Stretch = Stretch.Uniform;
                VideoPlayerElement.MouseWheel += mePlayer_MouseWheel;
                
                Volume = Settings.Default.SoundVolume; //restore

                scrollPlayer.Content = VideoPlayerElement;

                double vOff = _scrollDragger.VerticalOffset;
                double zoom = _scrollDragger.Zoom;

                _scrollDragger = new ScrollDragZoom(VideoPlayerElement, scrollPlayer);
                _scrollDragger.Zoom = zoom;
                _scrollDragger.VerticalOffset = vOff;

                //refresh view when change position
                VideoPlayerElement.ScrubbingEnabled = true;

                AddFlipXRenderTransform(VideoPlayerElement, flipHorizontally);

                VideoPlayerElement.MediaOpened += (s, e) => 
                {
                    _scrollDragger.NaturalSize = new Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);
                    MediaState = GetMediaState(VideoPlayerElement); 
                    VideoStarted(); 
                };
                VideoPlayerElement.MediaEnded += (s, e) => { VideoEnded(); };
                VideoPlayerElement.MediaFailed += (s, e) => { e.Handled = VideoFailed(e, VideoPlayerElement); };
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }        
        }

        internal void AddFlipXRenderTransform(UIElement element, bool bFlipHorizontally)
        {
            element.RenderTransformOrigin = new Point(0.5, 0.5);

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleY = 1;
            scaleTransform.ScaleX = bFlipHorizontally ? -1 : 1;

            //// Create a TransformGroup to contain the transforms 
            //// and add the transforms to it. 
            //TransformGroup myTransformGroup = new TransformGroup();
            //myTransformGroup.Children.Add(myScaleTransform);

            element.RenderTransform = scaleTransform; // myTransformGroup;
        }

        private MediaState GetMediaState(MediaElement myMedia)
        {
            FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
            object helperObject = hlp.GetValue(myMedia);
            FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            MediaState state = (MediaState)stateField.GetValue(helperObject);
            return state;
        }

        private void btnFitWidth_Click(object sender, RoutedEventArgs e)
        {
            FitWidth(true);
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            MaximizeAction();
        }

        private void btnOriginalSize_Click(object sender, RoutedEventArgs e)
        {
            OriginalSize(true);
        }

        private void btnFitWindow_Click(object sender, RoutedEventArgs e)
        {
            FitWindow();
        }

        private void btnFlipHorizontally_Click(object sender, RoutedEventArgs e)
        {
            IsFlipHorizontally = !IsFlipHorizontally;
        }
    }
}
