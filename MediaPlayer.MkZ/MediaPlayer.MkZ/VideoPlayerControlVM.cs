using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;
using MkZ.MediaPlayer.Utils;
using MkZ.Tools;
using MkZ.Windows;
using MkZ.WPF;
using MkZ.WPF.MessageBox;

namespace MkZ.MediaPlayer
{
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
        [XmlAttribute("FileName")]
        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value); }

        private double _positionInSeconds = 0.0;
        public double PositionInSeconds { get => _positionInSeconds; set => SetProperty(ref _positionInSeconds, value); }

        private double _naturalDuration = 0.0;
        public double NaturalDuration { get => _naturalDuration; set => SetProperty(ref _naturalDuration, value); }

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

        public override string ToString()
        {
            return "MediaFileInfo: " + Position + " " + FileName;
        }
    }

    public class VideoPlayerControlVM : NotifyPropertyChangedImpl, IMediaPlayer
    {
        public VideoPlayerContext Context => VideoPlayerContext.Instance;
        
        private ScrollDragZoom _scrollDragger = null;
        private ScrollViewer _scrollPlayerContainer = null;

        public Action<IMediaPlayer> MaximizeAction = (player) => { };

        DispatcherTimer _timer = new DispatcherTimer();

        public Action<IMediaPlayer> MediaStartedAction { get; set; } = (player) => { };
        public Action<IMediaPlayer> MediaEndedAction { get; set; } = (player) => { };
        public Func<object, ExceptionRoutedEventArgs, bool> MediaFailedAction { get; set; } = (sender, e) => true;
        
        public Action<VideoPlayerControlVM> LeftButtonClick = (vm) => { vm.TogglePlayPauseState(); };
        public Action<VideoPlayerControlVM> LeftButtonDoubleClick = (vm) => { };

        public MediaFileInfo State { get; set; } = new MediaFileInfo();

        MediaState _mediaState = MediaState.Manual;
        public MediaState MediaState
        {
            get { return _mediaState; }
            protected set { SetProperty(ref _mediaState, value); }
        }

        public MediaElement VideoPlayerElement { get; private set; } = null;

        public bool IsInitialized { get { return VideoPlayerElement != null; } }

        public bool IsOpen { get { return (IsInitialized && VideoPlayerElement.Source  != null); } }

        public bool IsAttached { get { return _scrollPlayerContainer != null; } }

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

        private Brush _background = Brushes.Transparent;
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

        private bool _isFullScreen = false;
        public bool IsFullScreen { get => _isFullScreen; set => SetProperty(ref _isFullScreen, value); }

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

            _timer.Interval = TimeSpan.FromSeconds(0.233);
            _timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!CheckMediaOpened())
                return;

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

            Prompt = "Use Ctrl+O or Drop file here...";
        }

        //main method - open and play media file
        public void Open(MediaFileInfo info)
        {
            //save previous state
            SaveAndClear();

            //set new state
            State = info;

            Debug.WriteLine("RestoreState: {0}\nPosition: {1}, Size: {2}, Duration: {3}",
                State.FileName, State.Position, NaturalSize, NaturalDuration);

            Zoom = State.Zoom;
            VerticalOffset = State.ScrollOffset.Y;

            Volume = State.Volume;

            Open(State.FileName);
        }

        private void Open(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    FileName = fileName;
                    VideoPlayerElement.Source = null;
                    VideoPlayerElement.Source = new Uri(fileName);
                    MediaState = MediaState.Play;
                    Title = Path.GetFileName(fileName);

                    Volume = State.Volume;
                    IsMuted = true; //load silently

                    //https://stackoverflow.com/questions/6716100/strange-behavior-with-wpf-mediaelement
                    VideoPlayerElement.ScrubbingEnabled = false; //faster load

                    //wait for source opened
                    for (int i = 0; i < 10; i++)
                    {
                        Debug.WriteLine("Check NaturalDuration, try {0} - {1:###,##0} sec", i, NaturalDuration);
                        if (NaturalDuration > 0)
                            break;

                        VideoPlayerElement.ForceRender();
                        Thread.Sleep(100);
                    }

                    State.NaturalDuration = NaturalDuration;
                    Position = State.Position;
                    Debug.WriteLine("Open(file): Position: {0} - Duration: {1:###,###}", State.Position, State.NaturalDuration);

                    Prompt = "Loading, Please Wait...";

                    //sometimes loading media is stuck - use timer to detect it
                    _stopperMediaOpened.Restart();

                    //this will open media - reset state on MediaOpened event
                    VideoPlayerElement.Play();

                    _timer.Start();

                    NotifyPropertyChangedAll();
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
            catch (Exception err)
            {
                Log.e("Open exception: {0}", err);
            }        
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
            set 
            {
                if (value < 0)
                    VideoPlayerElement.Volume = 0;
                else if (value > 1)
                    VideoPlayerElement.Volume = 1;
                else
                    VideoPlayerElement.Volume = value;

                State.Volume = VideoPlayerElement.Volume;

                NotifyPropertyChanged(); 
            }
        }

        public bool IsMuted
        {
            get { return VideoPlayerElement.IsMuted; }
            set { VideoPlayerElement.IsMuted = value; NotifyPropertyChanged(); }
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

        public void SaveAndClear()
        {
            if (!string.IsNullOrWhiteSpace(this.FileName))
            {
                Pause(); //always save in Pause state
                State.CopyFrom(this, _scrollDragger);
            }

            Stop();

            State = new MediaFileInfo();
            VideoPlayerElement.Source = null;
            FileName = "";
            MediaState = MediaState.Manual;
            Background = Brushes.Transparent;
            _OpenMediaTryCount = 0;

            Title = "N/A";
            Prompt = "Use Ctrl+O or Drop file here...";

            NotifyPropertyChangedAll();
        }

        public void Play()
        {
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Play();
                MediaState = MediaStateRetrieve();
                MediaState = MediaState.Play;
                State.CopyFrom(this, _scrollDragger);
            }
        }

        public void Pause()
        {
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Pause();
                MediaState = MediaState.Pause;
                State.CopyFrom(this, _scrollDragger);
            }
        }

        public void Stop()
        {
            _timer.Stop();
            MediaState = MediaState.Stop;
            Position = TimeSpan.FromSeconds(0);
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Stop();
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
            _scrollDragger.FitWindow(0);
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
                    //VideoPlayerElement.Draggable(false);

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

                //VideoPlayerElement.Draggable(true);

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

        private int _OpenMediaTryCount = 0;
        private Stopwatch _stopperMediaOpened = new Stopwatch();
        private bool CheckMediaOpened()
        {
            if (!_stopperMediaOpened.IsRunning)
                return true; //media is opened

            //check why media did not open
            TimeSpan waitToOpen = _stopperMediaOpened.Elapsed;
            double timeout = 3000.0;
            if (State.NaturalDuration > 0)
                timeout = 100.0 + State.NaturalDuration / 1000.0;

            if (waitToOpen.TotalMilliseconds > timeout) //timeout
            {
                _OpenMediaTryCount++;
                Debug.WriteLine("Error: Timeout for Open: {0:0.0} ms > {1:0.0} ms, Pos: {2}, FileName: {3}",
                    waitToOpen.TotalMilliseconds, timeout, State.Position, State.FileName);

                _stopperMediaOpened.Stop();
                _timer.Stop();
                Stop();

                if (_OpenMediaTryCount < 2)
                {
                    if (File.Exists(State.FileName))
                    {
                        Open(State.FileName);
                    }
                }
                else //exceed number of tries
                {
                    PopUp.Error("Media open FAILED: \n" + State.FileName, "Open Media File Error");
                }
            }
            else //not opened yet - wait more
            {
                Debug.WriteLine("* Wait for Open: {0:0.0} ms, Pos: {1}, FileName: {2}",
                    waitToOpen.TotalMilliseconds, State.Position, State.FileName);
            }

            return false;
        }

        private void VideoPlayerElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan position = State.Position;

                _stopperMediaOpened.Stop();
                Debug.WriteLine("MediaOpened: Position: {0} - {1}, Open took: {2:0.0} ms",
                    position, State.NaturalDuration, _stopperMediaOpened.Elapsed.TotalMilliseconds);

                TimeSpan waitForRender = TimeSpan.FromSeconds(0.5);
                if (position > waitForRender)
                {
                    Position = position - waitForRender;
                    Thread.Sleep(waitForRender); //wait to render video
                }
                else //position is almost zero
                {
                    Thread.Sleep(waitForRender); //wait to render video
                    Position = TimeSpan.FromSeconds(0);
                }

                VideoPlayerElement.ScrubbingEnabled = true; //enable preview

                if (State.MediaState == MediaState.Stop)
                    Stop();
                if (State.MediaState == MediaState.Pause || State.MediaState == MediaState.Manual)
                    Pause();

                _scrollDragger.NaturalSize = new Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);

                IsMuted = false;

                Prompt = Title;
                State.NaturalDuration = NaturalDuration;
                State.MediaState = MediaState = MediaStateRetrieve();

                Debug.WriteLine("Media Opened: {0}\nPosition: {1}, Size: {2}, Duration: {3}",
                    VideoPlayerElement.Source, VideoPlayerElement.Position, NaturalSize, NaturalDuration);

                Background = GetBackgroundForOpenedFile();

                FitWindow();
                NotifyPropertyChanged(nameof(NaturalDuration));
                MediaStartedAction(this);
            }
            catch (Exception err)
            {
                Log.e("VideoPlayerElement_MediaOpened exception: {0}", err);
                PopUp.Error("VideoPlayerElement_MediaOpened FAILED: \n" + err.ToString(), "Open Media File Error");
            }
        }

        private void VideoPlayerElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Stop();
            State.CopyFrom(this, _scrollDragger);
            MediaEndedAction(this);
        }

        private void VideoPlayerElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Stop();
            Log.e("1 VideoPlayerElement_MediaFailed({0}) - {1} - {2}", State.Volume, State.MediaState, State.FileName);
            e.Handled = MediaFailedAction(this, e);
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

        public MediaState MediaStateRetrieve()
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

        private Brush GetBackgroundForOpenedFile()
        {
            if (Context.Config.Configuration.IsSupportedVideoFile(State.FileName))
                return Brushes.Black;
                
            return Brushes.Transparent;
        }

        public override string ToString()
        {
            return Title + " -- " + VideoResolution;
        }
    }
}
