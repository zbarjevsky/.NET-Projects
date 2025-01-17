using MkZ.WPF;
using MkZ.WPF.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using Application = System.Windows.Application;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for VideoPlayerUserControl.xaml
    /// </summary>
    public partial class VideoPlayerUserControl : System.Windows.Controls.UserControl, IVideoPlayer
    {
        private ScrollDragZoom _scrollDragger;

        private string DragDropDataFormat = "MultiPlayer.OnePlayerSettings";
        private static VideoPlayerUserControl? DragDropSource = null;

        private readonly FadeAnimationHelper _controlsHideAndShow;

        public Action MaximizeAction = () => { };
        public Action<IVideoPlayer> VideoEnded = (player) => { };
        public Action<IVideoPlayer> VideoStartedAction { get; set; } = (player) => { };
        public Func<ExceptionRoutedEventArgs, MediaElement, bool> VideoFailed = (e, player) => true;
        public Action LeftButtonClick = () => { };
        public Action LeftButtonDoubleClick = () => { };

        public OnePlayerSettings Settings {  get; set; } = new OnePlayerSettings();

        private DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.ContextIdle)
        {
            Interval = TimeSpan.FromSeconds(0.3),
        };

        public MediaElement VideoPlayerElement { get; private set; } = null;

        private MediaState _mediaState = MediaState.Manual;
        public MediaState MediaState
        {
            get { return _mediaState; }
            protected set { _mediaState = value; OnPropertyChanged(); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                //txtTitle.Text = value + (IsFlipHorizontally ? " (Flipped)" : "");
                OnPropertyChanged();
            }
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
                OnPropertyChanged();
            }
        }

        public double ScrollOffsetY
        {
            get
            {
                return _scrollDragger.VerticalOffset;
            }

            set
            {
                if (value > 0 && value <= 1) //relative to Height
                {
                    _scrollDragger.VerticalOffset = value * _scrollPlayerContainer.ScrollableHeight;
                }
                else if (value > 1)
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

        public eZoomState ZoomState
        {
            get => _scrollDragger.ZoomState;
            set => ZoomStateSet(value, true);
        }

        public void ZoomStateSet(eZoomState zoom, bool adjustScroll)
        {
            switch (zoom)
            {
                case eZoomState.Original:
                    OriginalSize(adjustScroll);
                    break;
                case eZoomState.FitWidth:
                    FitWidth(adjustScroll);
                    break;
                case eZoomState.FitHeight:
                    FitHeight(adjustScroll);
                    break;
                case eZoomState.FitWindow:
                    FitWindow();
                    break;
                case eZoomState.Custom:
                default:
                    Zoom = Zoom;
                    break;
            }
            OnPropertyChanged(nameof(ZoomState));
        }

        public eZoomState ZoomStateGet()
        {
            return _scrollDragger.ZoomState;
        }

        public VideoPlayerUserControl()
        {
            InitializeComponent();

            _scrollDragger = new ScrollDragZoom(null, _scrollPlayerContainer);

            _controlsHideAndShow = new FadeAnimationHelper(this, 2,
                _borderTitle, _commands, _commands1);

            RecreateMediaElement(false);

            _timer.Tick += _timer_Tick;

            _commands.Init(this);

            VideoPlayerElement.MouseWheel += UserControl_MouseWheel;
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop();
            Settings.Update(this);
            _commands.Update(Settings, _commands.IsPopWindowMode);
            _timer.Start();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ZoomState = ZoomState; //update zoom
        }

        public void CopyState(VideoPlayerUserControl player, double volume, bool copySource)
        {
            if (copySource)
                VideoPlayerElement.Source = player.VideoPlayerElement.Source;

            Volume = volume;
            MediaState = player.MediaState;
            Title = player.Title;
            FileName = player.FileName;
            IsFlipHorizontally = player.IsFlipHorizontally;
            Zoom = player.Zoom;
            ZoomStateSet(player.ZoomStateGet(), true);
            ScrollOffsetY = player.ScrollOffsetY;
            PositionSet(player.Position, true);
        }

        public void RestoreMediaState(MediaState state, TimeSpan position)
        {
            Play();
            PositionSet(position, true);
            if (state == MediaState.Stop)
                Stop();
            if (state == MediaState.Pause)
                Pause();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.OriginalSource is MediaElement)
                LeftButtonDoubleClick();
        }

        private Point _mousePos = new Point();
        private void UserControl_PreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mousePos = e.GetPosition(this);
        }

        private void UserControl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(this);
            if ((pos - _mousePos).Length > 12.0) //detect mouse move
                return;

            object s = e.OriginalSource;
            if (e.ChangedButton == MouseButton.Left && ((s is MediaElement) || (s is ScrollViewer) || (s is Grid)))
                LeftButtonClick();
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
            set { VideoPlayerElement.SpeedRatio = value; OnPropertyChanged(); }
        }

        public double Volume
        {
            get { return VideoPlayerElement.Volume; }
            set { VideoPlayerElement.Volume = value; OnPropertyChanged(); }
        }

        public TimeSpan Position
        {
            get { return VideoPlayerElement.Position; }
            //set { VideoPlayerElement.Position = value; OnPropertyChanged(); } 
        }

        public void PositionSet(TimeSpan position, bool notify)
        {
            Settings.Position = position.TotalSeconds;
            VideoPlayerElement.Position = position;
            if (notify)
                OnPropertyChanged(nameof(Position));
        }

        public System.Windows.Size NaturalSize
        {
            get
            {
                if (VideoPlayerElement.Source != null)
                {
                    return new System.Windows.Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);
                }
                return new System.Windows.Size(1920, 1080);
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

        //sometimes if video was not opened yet - NaturalDuration is 0 - use saved in settings duration
        public double Duration => NaturalDuration > 0.0 ? NaturalDuration : Settings.Duration;

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
            Title = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(fileName)) + "/" + System.IO.Path.GetFileName(fileName);
        }

        public void LoadSetting(OnePlayerSettings s, bool pop = false)
        {
            Open(s.FileName, s.Volume);

            PositionSet(TimeSpan.FromSeconds(s.Position), false);
            SpeedRatio = s.SpeedRatio;
            ZoomStateSet(s.ZoomState, true);

            _commands.Play();
            if (s.MediaState != MediaState.Play)
                _commands.Pause();

            Settings.Update(s);
            _commands.Update(Settings, pop);
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
                _timer.Start();
                VideoPlayerElement.Play();
                this.Background = Brushes.DimGray;
                MediaState = MediaState.Play;
                PositionSet(TimeSpan.FromSeconds(Settings.Position), false);
            }
            else
            {
                Close();
                _timer.Stop();
            }
        }

        public void Pause()
        {
            if (VideoPlayerElement.Source != null)
            {
                _timer.Stop();
                Settings.Position = VideoPlayerElement.Position.TotalSeconds;
                VideoPlayerElement.Pause();
                this.Background = Brushes.DarkGray;
                MediaState = MediaState.Pause;

                Settings.Update(this);
                _commands.Update(Settings, _commands.IsPopWindowMode);
            }
        }

        public void Stop()
        {
            if (VideoPlayerElement.Source != null)
            {
                _timer.Stop();
                Settings.Position = 0.0;
                VideoPlayerElement.Stop();
                this.Background = Brushes.DarkGray;
                MediaState = MediaState.Stop;
                
                Settings.Update(this);
                _commands.Update(Settings, _commands.IsPopWindowMode);
            }
        }

        public void TogglePlayPauseState()
        {
            if (MediaState == MediaState.Play)
                Pause();
            else //if (MediaState == MediaState.Pause || MediaState == MediaState.Manual)
                Play();
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
            _scrollDragger.FitWidth(0);
            if (adjustScroll)
                ScrollToCenter();
        }

        public void FitHeight(bool adjustScroll)
        {
            _scrollDragger.FitHeight(0);
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

                double vOff = _scrollDragger.VerticalOffset;
                double zoom = _scrollDragger.Zoom;

                _scrollDragger = new ScrollDragZoom(VideoPlayerElement, _scrollPlayerContainer);
                _scrollDragger.Zoom = zoom;
                _scrollDragger.VerticalOffset = vOff;

                _scrollDragger.SizeChangedAction = () =>
                {
                    UpdateResolutionText();
                    OnPropertyChanged(nameof(ZoomState));
                };

                //refresh view when change position
                VideoPlayerElement.ScrubbingEnabled = true;

                AddFlipXRenderTransform(VideoPlayerElement, flipHorizontally);

                VideoPlayerElement.MediaOpened += (s, e) =>
                {
                    double zoom_save = _scrollDragger.Zoom;
                    eZoomState zoomState = _scrollDragger.ZoomState;
                    _scrollDragger.NaturalSize = new System.Windows.Size(VideoPlayerElement.NaturalVideoWidth, VideoPlayerElement.NaturalVideoHeight);
                    _scrollDragger.Zoom = zoom_save;
                    ZoomStateSet(zoomState, false);
                    MediaState = GetMediaState(VideoPlayerElement);
                    VideoStartedAction(this);
                };
                VideoPlayerElement.MediaEnded += (s, e) => { VideoEnded(this); };
                VideoPlayerElement.MediaFailed += (s, e) => { e.Handled = VideoFailed(e, VideoPlayerElement); };

                UpdateResolutionText();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.ToString());
            }
        }

        private void UpdateResolutionText()
        {
            //if (VideoPlayerElement.ActualWidth > 0)
            //{
            //    txtVideoResolution.Text = string.Format("{0:0}x{1:0} ({2:0.0}%)",
            //        VideoPlayerElement.ActualWidth, VideoPlayerElement.ActualHeight, 100.0 * _scrollDragger.Zoom);
            //}
            //else
            //{
            //    txtVideoResolution.Text = "";
            //}
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

        public static MediaState GetMediaState(MediaElement myMedia)
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

        private bool _isDragging = false;
        private void UserControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            const double scrollBarWidth = 12.0;

            if (e.LeftButton == MouseButtonState.Pressed && !_isDragging)
            {
                Point pt = e.GetPosition(_scrollPlayerContainer);
                if (pt.X > _scrollPlayerContainer.ActualWidth - scrollBarWidth ||
                    pt.Y > _scrollPlayerContainer.ActualHeight - scrollBarWidth)
                    return;

                _isDragging = true;
                DragDropSource = this;
                DragDrop.DoDragDrop(this, Settings, System.Windows.DragDropEffects.Move);
                e.Handled = true;
            }

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _isDragging = false;
            }
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftShift))
                return;

            _commands.VolumeUpdate(e.Delta);
        }

        private void UserControl_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

                // handling code you have defined.
                _commands.Open(files[0]);
            }
            else if (e.Data.GetDataPresent(DragDropDataFormat))
            {
                VideoPlayerUserControl vFrom = DragDropSource;
                OnePlayerSettings setFrom = (OnePlayerSettings)(e.Data.GetData(DragDropDataFormat));
                OnePlayerSettings setTo = new OnePlayerSettings(this);
                if (setFrom.FileName != setTo.FileName)
                {
                    this.LoadSetting(setFrom, _commands.IsPopWindowMode);
                    vFrom.LoadSetting(setTo, vFrom._commands.IsPopWindowMode);
                }
            }
            DragDropSource = null;
       }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            _commands.Open_Click(sender, e);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptions(Application.Current.MainWindow, Settings, "Settings", 650);
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            _commands.Maximize_Click(sender, e);
        }
    }
}
