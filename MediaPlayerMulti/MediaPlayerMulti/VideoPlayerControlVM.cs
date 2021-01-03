using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Serialization;
using MZ.Windows;
using MZ.WPF;

namespace MkZ.MediaPlayer
{
    public class MediaDataContext
    {
        public MediaFileInfo MediaFileInfo { get; set; } = null;
    }

    [Serializable]
    public class MediaFileInfo : NotifyPropertyChangedImpl
    {
        [XmlIgnore]
        public TimeSpan Position
        {
            get { return TimeSpan.FromSeconds(_positionInSeconds); }
            set { SetProperty(ref _positionInSeconds, value.TotalSeconds); }
        }

        private MediaState _mediaState = MediaState.Manual;
        [XmlIgnore]
        public MediaState MediaState { get => _mediaState; set => SetProperty(ref _mediaState, value); }
        
        private string _fileName = "";
        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value); }

        private double _positionInSeconds = 0.0;
        public double PositionInSeconds { get => _positionInSeconds; set => SetProperty(ref _positionInSeconds, value); }

        private double _volume = 0.5;
        public double Volume { get => _volume; set => SetProperty(ref _volume, value); }

        private Point _scrollOffset = new Point();
        public Point ScrollOffset { get => _scrollOffset; set => SetProperty(ref _scrollOffset, value); }

        private double _zoom = 1;
        public double Zoom { get => _zoom; set => SetProperty(ref _zoom, value); }

        private bool _isFlipHorizontally = false;
        public bool IsFlipHorizontally { get => _isFlipHorizontally; set => SetProperty(ref _isFlipHorizontally, value); }

        public void CopyFrom(MediaFileInfo state)
        {
            MediaState = state.MediaState;
            FileName = state.FileName;
            Position = state.Position;
            Volume = state.Volume;
            IsFlipHorizontally = state.IsFlipHorizontally;

            ScrollOffset = state.ScrollOffset;
            Zoom = state.Zoom;
        }

        public void CopyFrom(VideoPlayerControlVM player, ScrollDragZoom scrollDragZoom)
        {
            if (player.VideoPlayerElement != null)
            {
                MediaState = player.MediaState;
                FileName = player.FileName;
                Position = player.Position;
                Volume = player.Volume;
                IsFlipHorizontally = player.IsFlipHorizontally;
            }

            if (scrollDragZoom != null)
            {
                ScrollOffset = scrollDragZoom.ScrollOffset;
                Zoom = scrollDragZoom.Zoom;
            }
        }

        public void RestoreStateTo(VideoPlayerControlVM player)
        {
            player.State.CopyFrom(this);

            Debug.WriteLine("RestoreState: {0}\nPosition: {1}, Size: {2}, Duration: {3}",
                FileName, Position, player.NaturalSize, player.NaturalDuration);

            player.Zoom = Zoom;
            player.VerticalOffset = ScrollOffset.Y;
            
            player.Volume = Volume;
            
            player.Open(FileName);
        }

        public override string ToString()
        {
            return Position + " " + FileName;
        }
    }

    public class VideoPlayerControlVM : NotifyPropertyChangedImpl, IVideoPlayer
    {
        private ScrollDragZoom _scrollDragger = null;
        private ScrollViewer _scrollPlayerContainer = null;

        public Action MaximizeAction = () => { };
        public Action VideoEndedAction = () => { };
        public Action<IVideoPlayer> VideoStartedAction { get; set; } = (player) => { };
        public Func<ExceptionRoutedEventArgs, MediaElement, bool> VideoFailedAction = (e, player) => true;
        public Action<VideoPlayerControlVM> LeftButtonClick = (vm) => { vm.TogglePlayPauseState(); };
        public Action<VideoPlayerControlVM> LeftButtonDoubleClick = (vm) => { };

        public MediaDataContext _dataContext = new MediaDataContext();
        public MediaFileInfo State { get => _dataContext.MediaFileInfo; }

        public MediaElement VideoPlayerElement { get; private set; } = null;

        public bool IsInitialized { get { return VideoPlayerElement != null; } }

        public bool IsOpen { get { return !string.IsNullOrWhiteSpace(FileName); } }

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

        private string _prompt = "Use Ctrl+O or Drop file here...";
        public string Prompt 
        {
            get { return _prompt; }
            set { SetProperty(ref _prompt, value); }
        }

        private Brush _background = Brushes.Gray;
        public Brush Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }

        private string _videoResolution = "";
        public string VideoResolution
        {
            get { return _videoResolution; }
            set { SetProperty(ref _videoResolution, value); }
        }

        private string _fileName = "";
        public string FileName 
        { 
            get { return _fileName; } 
            private set { SetProperty(ref _fileName, value); NotifyPropertyChanged(nameof(Prompt)); } 
        }

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
            get { return _scrollDragger.VerticalOffset; }

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
                NotifyPropertyChanged();
            }
        }

        public double Zoom
        {
            get { return _scrollDragger.Zoom; }
            set { _scrollDragger.Zoom = value; }
        }

        public VideoPlayerControlVM()
        {
            RecreateMediaElement(false);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.233);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(IsAttached)
                NotifyPropertyChanged(nameof(Position));
            
            if (MediaState == MediaState.Play)
                State.Position = Position;
        }

        public void Init(ScrollViewer scrollPlayer)
        {
            _scrollPlayerContainer = scrollPlayer;
            _scrollPlayerContainer.Content = VideoPlayerElement;

            VideoPlayerElement.Volume = State.Volume;

            if (_scrollDragger != null)
                _scrollDragger.Dispose();

            _scrollDragger = new ScrollDragZoom(VideoPlayerElement, _scrollPlayerContainer);

            _scrollDragger.SizeChangedAction = () =>
            {
                VideoResolution = string.Format("{0:0}x{1:0} ({2:0.0}%)",
                    VideoPlayerElement.ActualWidth, VideoPlayerElement.ActualHeight, 100.0 * _scrollDragger.Zoom);
            };

            Prompt = "Loading, Please Wait...";
            State.RestoreStateTo(this);
        }

        public void Detach()
        {
            if(VideoPlayerElement != null)
            {
                State.CopyFrom(this, _scrollDragger);
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

        //public void CopyState(VideoPlayerControlVM playerControl, double volume, bool copySource)
        //{
        //    if (copySource)
        //        VideoPlayerElement.Source = playerControl.VideoPlayerElement.Source;

        //    Volume = volume;
        //    MediaState = playerControl.MediaState;
        //    Title = playerControl.Title;
        //    FileName = playerControl.FileName;
        //    IsFlipHorizontally = playerControl.IsFlipHorizontally;
        //    Zoom = playerControl.Zoom;
        //    VerticalOffset = playerControl.VerticalOffset;
        //    Position = playerControl.Position;
        //}

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

        public void OpenAndPlay(string fileName)
        {
            State = new MediaFileInfo() //reset
            {
                FileName = fileName,
                PositionInSeconds = 0.0,
                Volume = this.Volume == 0 ? 0.5 : this.Volume,
                MediaState = MediaState.Play
            };

            Open(fileName);
        }

        public void Open(string fileName)
        {
            if(File.Exists(fileName))
            {
                FileName = fileName;
                VideoPlayerElement.Source = null;
                VideoPlayerElement.Source = new Uri(fileName);
                MediaState = MediaState.Manual;
                Title = Path.GetFileName(fileName);

                Volume = 0; //load silently
                Position = State.Position;

                Prompt = "Loading, Please Wait...";

                //this will open media - reset state on MediaOpened event
                VideoPlayerElement.Play();
            }
            else
            {
                FileName = "";
                VideoPlayerElement.Source = null;
                MediaState = MediaState.Manual;
                Title = "N/A";
                Prompt = "Use Ctrl+O or Drop file here...";
            }
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
                
                Background = Brushes.Black;
                MediaState = MediaState.Play;
            }
        }

        public void Pause()
        {
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Pause();
                Background = Brushes.Gray;
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
            if (MediaState == MediaState.Pause || MediaState == MediaState.Stop)
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
            _scrollDragger?.FitWindow(1);
        }

        /// <summary>
        /// Sometimes MediaElement crashes - black window
        /// I will replace it with the new one
        /// </summary>
        private void RecreateMediaElement(bool flipHorizontally)
        {
            try
            {
                if (VideoPlayerElement != null)
                {
                    VideoPlayerElement.Stop();
                    VideoPlayerElement.Close();
                    VideoPlayerElement.Clock = null;
                    VideoPlayerElement.Source = null;
                    VideoPlayerElement.Volume = 0;
                    VideoPlayerElement = null;

                    VideoPlayerElement.MouseWheel -= VideoPlayerElement_MouseWheel;
                    VideoPlayerElement.MediaOpened -= VideoPlayerElement_MediaOpened;
                    VideoPlayerElement.MediaEnded -= VideoPlayerElement_MediaEnded;
                    VideoPlayerElement.MediaFailed -= VideoPlayerElement_MediaFailed;

                    _scrollPlayerContainer.Content = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                MediaState = MediaState.Manual;

                VideoPlayerElement = new MediaElement();
                VideoPlayerElement.Width = 320;
                VideoPlayerElement.Height = 240;
                VideoPlayerElement.LoadedBehavior = MediaState.Manual;
                VideoPlayerElement.Stretch = Stretch.Uniform;

                //refresh view when change position
                VideoPlayerElement.ScrubbingEnabled = true;

                VideoPlayerElement.Volume = 0; //reset

                AddFlipXRenderTransform(VideoPlayerElement, flipHorizontally);

                VideoPlayerElement.MouseWheel += VideoPlayerElement_MouseWheel;
                VideoPlayerElement.MediaOpened += VideoPlayerElement_MediaOpened;
                VideoPlayerElement.MediaEnded += VideoPlayerElement_MediaEnded;
                VideoPlayerElement.MediaFailed += VideoPlayerElement_MediaFailed;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void VideoPlayerElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            Position = State.Position;

            _scrollDragger.NaturalSize = new Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);
            
            if (State.MediaState == MediaState.Stop)
                Stop();
            if (State.MediaState == MediaState.Pause)
                Pause();

            Volume = State.Volume;
            MediaState = GetMediaState();

            Prompt = Title;

            Debug.WriteLine("Media Opened: {0}\nPosition: {1}, Size: {2}, Duration: {3}",
                VideoPlayerElement.Source, VideoPlayerElement.Position, NaturalSize, NaturalDuration);
            
            FitWindow();
            NotifyPropertyChanged(nameof(NaturalDuration));
            VideoStartedAction(this);
        }

        private void VideoPlayerElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Stop();
            NotifyPropertyChangedAll();
            VideoEndedAction();
        }

        private void VideoPlayerElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            e.Handled = VideoFailedAction(e, VideoPlayerElement);
        }

        private void VideoPlayerElement_MouseWheel(object sender, MouseWheelEventArgs e)
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

        public MediaState GetMediaState()
        {
            if(VideoPlayerElement != null)
                return GetMediaState(VideoPlayerElement);
            return MediaState.Manual;
        }

        private static MediaState GetMediaState(MediaElement myMedia)
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
