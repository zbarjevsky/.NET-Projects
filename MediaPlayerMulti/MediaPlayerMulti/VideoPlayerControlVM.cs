using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MZ.Windows;
using MZ.WPF;

namespace MkZ.MediaPlayer
{
    public class VideoPlayerState
    {
        public MediaState MediaState = MediaState.Manual;
        public string FileName = "";
        public TimeSpan Position = TimeSpan.FromSeconds(0);
        public double Volume = 0.5;
        public Point ScrollOffset = new Point();
        public double Zoom = 1;
        
        public void CopyFrom(VideoPlayerControlVM player, ScrollDragZoom scrollDragZoom)
        {
            if (player.VideoPlayerElement != null)
            {
                MediaState = player.MediaState;
                FileName = player.FileName;
                Position = player.Position;
                Volume = player.Volume;
            }

            if (scrollDragZoom != null)
            {
                ScrollOffset = scrollDragZoom.ScrollOffset;
                Zoom = scrollDragZoom.Zoom;
            }
        }

        public void RestoreState(VideoPlayerControlVM player)
        {
            player.Zoom = Zoom;
            player.VerticalOffset = ScrollOffset.Y;
            player.Open(FileName, Volume);
            player.RestoreMediaState(MediaState, Position);
        }
    }

    public class VideoPlayerControlVM : NotifyPropertyChangedImpl, IVideoPlayer
    {
        private ScrollDragZoom _scrollDragger = null;
        private ScrollViewer _scrollPlayerContainer = null;

        public Action MaximizeAction = () => { };
        public Action VideoEnded = () => { };
        public Action<IVideoPlayer> VideoStarted { get; set; } = (player) => { };
        public Func<ExceptionRoutedEventArgs, MediaElement, bool> VideoFailed = (e, player) => true;
        public Action LeftButtonClick = () => { };
        public Action LeftButtonDoubleClick = () => { };

        private VideoPlayerState _videoPlayerState = new VideoPlayerState();

        public MediaElement VideoPlayerElement { get; private set; } = null;

        public bool IsInitialized { get { return VideoPlayerElement != null; } }

        public bool IsAttached { get { return _scrollPlayerContainer != null; } }

        private MediaState _mediaState = MediaState.Manual;
        public MediaState MediaState
        {
            get { return _mediaState; }
            protected set { _mediaState = value; NotifyPropertyChanged(); }
        }

        private string _title = "N/A";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                //txtTitle.Text = value + (IsFlipHorizontally ? " (Flipped)" : "");
                NotifyPropertyChanged();
            }
        }

        private Brush _background = Brushes.Gray;
        public Brush Background
        {
            get { return _background; }
            set { _background = value; NotifyPropertyChanged(); }
        }

        private string _videoResolution;
        public string VideoResolution
        {
            get { return _videoResolution; }
            set { _videoResolution = value; NotifyPropertyChanged(); }
        }

        public string FileName { get; private set; }

        public bool IsFlipHorizontally
        {
            get
            {
                if (VideoPlayerElement.RenderTransform is ScaleTransform scale)
                    return scale.ScaleX == -1; //Flip Horizontally
                return false;
            }
            set
            {
                if (VideoPlayerElement.RenderTransform is ScaleTransform scale)
                    scale.ScaleX = value ? -1 : 1; //Flip Horizontally
                Title = _title; //update flipped
                NotifyPropertyChanged();
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
                    _scrollDragger.VerticalOffset = value * _scrollPlayerContainer.ScrollableHeight;
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

        public VideoPlayerControlVM()
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(IsAttached)
                NotifyPropertyChanged(nameof(Position));
        }

        public void Init(ScrollViewer scrollPlayer)
        {
            _scrollPlayerContainer = scrollPlayer;
            RecreateMediaElement(false);
            _videoPlayerState.CopyFrom(this, _scrollDragger);
        }

        public void Attach(ScrollViewer scrollPlayer)
        {
            _scrollPlayerContainer = scrollPlayer;
            _scrollPlayerContainer.Content = VideoPlayerElement;

            double vOff = 0;
            double zoom = 1;
            if (_scrollDragger != null)
            {
                vOff = _scrollDragger.VerticalOffset;
                zoom = _scrollDragger.Zoom;
                _scrollDragger.Dispose();
            }

            _scrollDragger = new ScrollDragZoom(VideoPlayerElement, _scrollPlayerContainer);
            _scrollDragger.Zoom = zoom;
            _scrollDragger.VerticalOffset = vOff;

            _scrollDragger.SizeChangedAction = () =>
            {
                VideoResolution = string.Format("{0:0}x{1:0} ({2:0.0}%)",
                    VideoPlayerElement.ActualWidth, VideoPlayerElement.ActualHeight, 100.0 * _scrollDragger.Zoom);
            };

            _videoPlayerState.RestoreState(this);
        }

        public void Detach()
        {
            if(VideoPlayerElement != null)
            {
                _videoPlayerState.CopyFrom(this, _scrollDragger);
                Pause();
            }

            if (_scrollPlayerContainer != null)
            {
                _scrollPlayerContainer.Content = null;
                _scrollPlayerContainer = null;
            }

            if(_scrollDragger != null)
            {
                _scrollDragger.Dispose();
                _scrollDragger = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void CopyState(VideoPlayerControlVM playerControl, double volume, bool copySource)
        {
            if (copySource)
                VideoPlayerElement.Source = playerControl.VideoPlayerElement.Source;

            Volume = volume;
            MediaState = playerControl.MediaState;
            Title = playerControl.Title;
            FileName = playerControl.FileName;
            IsFlipHorizontally = playerControl.IsFlipHorizontally;
            Zoom = playerControl.Zoom;
            VerticalOffset = playerControl.VerticalOffset;
            Position = playerControl.Position;
        }

        public void RestoreMediaState(MediaState state, TimeSpan position)
        {
            _videoPlayerState.MediaState = state;
            _videoPlayerState.Position = position;

            Play();
            Position = position;
            if (state == MediaState.Stop)
                Stop();
            if (state == MediaState.Pause)
                Pause();
        }

        public bool Play_CanExecute
        {
            get
            {
                return (VideoPlayerElement != null) && (VideoPlayerElement.Source != null);
            }
        }

        public double SpeedRatio
        {
            get { return VideoPlayerElement.SpeedRatio; }
            set { VideoPlayerElement.SpeedRatio = value; NotifyPropertyChanged(); }
        }

        public double Volume
        {
            get { return VideoPlayerElement.Volume; }
            set { VideoPlayerElement.Volume = value; NotifyPropertyChanged(); }
        }

        public TimeSpan Position
        {
            get { return VideoPlayerElement.Position; }
            set { VideoPlayerElement.Position = value; NotifyPropertyChanged(); }
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
            VideoPlayerElement.Source = string.IsNullOrEmpty(fileName) ? null : new Uri(fileName);
            Volume = volume;
            MediaState = MediaState.Manual;
            Title = Path.GetFileName(fileName);
        }

        public void Close()
        {
            Stop();
            VideoPlayerElement.Source = null;
            FileName = "";
            Background = Brushes.LightGray;
            MediaState = MediaState.Close;
        }

        public void Play()
        {
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Play();
                Background = Brushes.Gray;
                MediaState = MediaState.Play;
            }
        }

        public void Pause()
        {
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Pause();
                Background = Brushes.DarkGray;
                MediaState = MediaState.Pause;
            }
        }

        public void Stop()
        {
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Stop();
                Background = Brushes.DarkGray;
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

        public void FitWidth(bool adjustScroll)
        {
            _scrollDragger.FitWidth();
            if (adjustScroll)
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

        /// <summary>
        /// Sometimes MediaElement crashes - black window
        /// I will replace it with the new one
        /// </summary>
        private void RecreateMediaElement(bool flipHorizontally)
        {
            try
            {
                _videoPlayerState.CopyFrom(this, _scrollDragger);

                if (VideoPlayerElement != null)
                {
                    VideoPlayerElement.Stop();
                    VideoPlayerElement.Close();
                    VideoPlayerElement.Clock = null;
                    VideoPlayerElement.Source = null;
                    VideoPlayerElement.Volume = 0;
                    VideoPlayerElement = null;
                    _scrollPlayerContainer.Content = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                MediaState = MediaState.Manual;

                VideoPlayerElement = new MediaElement();
                VideoPlayerElement.Width = 1920;
                VideoPlayerElement.Height = 1080;
                VideoPlayerElement.LoadedBehavior = MediaState.Manual;
                VideoPlayerElement.Stretch = Stretch.Uniform;
                VideoPlayerElement.MouseWheel += mePlayer_MouseWheel;

                Volume = 0; //reset

                _scrollPlayerContainer.Content = VideoPlayerElement;

                if (_scrollDragger != null)
                {
                    _scrollDragger.Dispose();
                }

                _scrollDragger = new ScrollDragZoom(VideoPlayerElement, _scrollPlayerContainer);

                _scrollDragger.SizeChangedAction = () =>
                {
                    VideoResolution = string.Format("{0:0}x{1:0} ({2:0.0}%)",
                        VideoPlayerElement.ActualWidth, VideoPlayerElement.ActualHeight, 100.0 * _scrollDragger.Zoom);
                };

                //refresh view when change position
                VideoPlayerElement.ScrubbingEnabled = true;

                AddFlipXRenderTransform(VideoPlayerElement, flipHorizontally);

                VideoPlayerElement.MediaOpened += (s, e) =>
                {
                    _scrollDragger.NaturalSize = new Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);
                    _scrollDragger.Zoom = _videoPlayerState.Zoom;
                    Position = _videoPlayerState.Position;
                    MediaState = GetMediaState(VideoPlayerElement);
                    VideoStarted(this);
                };
                VideoPlayerElement.MediaEnded += (s, e) => { VideoEnded(); };
                VideoPlayerElement.MediaFailed += (s, e) => { e.Handled = VideoFailed(e, VideoPlayerElement); };

                _videoPlayerState.RestoreState(this);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void mePlayer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        private void AddFlipXRenderTransform(UIElement element, bool bFlipHorizontally)
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

        public override string ToString()
        {
            return Title + " -- " + VideoResolution;
        }
    }
}
