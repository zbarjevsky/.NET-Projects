﻿using System;
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

    public class VideoPlayerControlVM : NotifyPropertyChangedImpl, IVideoPlayer
    {
        public VideoPlayerContext Context => VideoPlayerContext.Instance;
        
        private ScrollDragZoom _scrollDragger = null;
        private ScrollViewer _scrollPlayerContainer = null;

        public Action<IVideoPlayer> MaximizeAction = (player) => { };
        
        public Action<IVideoPlayer> MediaStartedAction { get; set; } = (player) => { };
        public Action<IVideoPlayer> MediaEndedAction { get; set; } = (player) => { };
        public Func<VideoPlayerControlVM, ExceptionRoutedEventArgs, bool> MediaFailedAction { get; set; } = (vm, e) => true;
        
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

            Prompt = "Use Ctrl+O or Drop file here...";
        }

        //main method - open and play media file
        public void Open(MediaFileInfo info)
        {
            //save previous state
            Clear();

            //set new state
            State = info;

            Background = Brushes.Transparent;

            Debug.WriteLine("RestoreState: {0}\nPosition: {1}, Size: {2}, Duration: {3}",
                State.FileName, State.Position, NaturalSize, NaturalDuration);

            Zoom = State.Zoom;
            VerticalOffset = State.ScrollOffset.Y;

            Volume = State.Volume;

            Open(State.FileName);
        }

        public void Open(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    FileName = fileName;
                    VideoPlayerElement.Source = null;
                    VideoPlayerElement.Source = new Uri(fileName);
                    MediaState = MediaState.Manual;
                    Title = Path.GetFileName(fileName);

                    Volume = State.Volume;
                    IsMuted = true; //load silently
                    //sometimes loading mp3 stuck
                    //https://stackoverflow.com/questions/6716100/strange-behavior-with-wpf-mediaelement
                    VideoPlayerElement.ScrubbingEnabled = false; //faster load
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

        public void Clear()
        {
            if (string.IsNullOrWhiteSpace(this.FileName))
                State.CopyFrom(this, _scrollDragger);

            Stop();

            State = new MediaFileInfo();
            VideoPlayerElement.Source = null;
            FileName = "";
            MediaState = MediaState.Manual;

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
            if (VideoPlayerElement.Source != null)
            {
                VideoPlayerElement.Stop();
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

        private void VideoPlayerElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                Position = State.Position;

                _scrollDragger.NaturalSize = new Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);

                if (State.MediaState == MediaState.Stop)
                    Stop();
                if (State.MediaState == MediaState.Pause || State.MediaState == MediaState.Manual)
                    Pause();

                Thread.Sleep(100);
                Volume = State.Volume;
                IsMuted = false;
                VideoPlayerElement.ScrubbingEnabled = true; //enable preview
                MediaState = MediaStateRetrieve();

                Prompt = Title;
                State.NaturalDuration = NaturalDuration;

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