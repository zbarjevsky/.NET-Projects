using MkZ.WPF;
using MkZ.WPF.PropertyGrid;
using MultiPlayer.MkZ.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for VideoPlayerUserControl.xaml
    /// </summary>
    public partial class VideoPlayerUserControl : System.Windows.Controls.UserControl //, IVideoPlayer
    {
        private ScrollDragZoom _scrollDragger;

        private string DragDropDataFormat = "MultiPlayer.OnePlayerSettings";
        private static VideoPlayerUserControl? DragDropSource = null;

        private readonly FadeAnimationHelper _controlsHideAndShow;

        public VideoCommandsVM VM { get => _commands.VM; }

        public Action MaximizeAction = () => { };
        public Action<IVideoPlayer> VideoEnded = (player) => { };
        public Action<IVideoPlayer> VideoStartedAction { get; set; } = (player) => { };
        public Func<ExceptionRoutedEventArgs, MediaElement, bool> VideoFailed = (e, player) => true;
        public Action LeftButtonClick = () => { };
        public Action LeftButtonDoubleClick = () => { };

        public VideoPlayerUserControl()
        {
            InitializeComponent();

            this.DataContext = VM;

            _scrollDragger = new ScrollDragZoom(null, _scrollPlayerContainer);

            _controlsHideAndShow = new FadeAnimationHelper(this, 2,
                _borderTitle, _commands, _commands1);
            _controlsHideAndShow.CanHideControls = () => { return !(_ctxMenu.IsOpen || _btnFileList.ContextMenu.IsOpen); };
            _controlsHideAndShow.OnShowCompleted = (ctrl) => { if (ctrl is VideoCommandsUserControl) VM.AdjustSizeAndLayout(); };

            RecreateMediaElement(false);

            _timer.Tick += _timer_Tick;

            _commands.Init(this);
        }

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

        public string FileName => VM.Settings.FileName;

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
                //VM.Title = _title; //update flipped
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
            VM.Settings.ZoomState = zoom;
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
            VM.Settings.Zoom = Zoom;
            OnPropertyChanged(nameof(ZoomState));
        }

        public eZoomState ZoomStateGet()
        {
            return _scrollDragger.ZoomState;
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop();
            VM.Settings.Update(this);
            VM.Update(VM.Settings, VM.IsPopWindowMode, lockUpdate: true);
            VM.Replay.ReplayCheckAndUpdate();
            _timer.Start();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _commands.VM;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ZoomState = ZoomState; //update zoom
            UpdateMaximizeButtonImage();
        }

        //public void CopyState(VideoPlayerUserControl player, double volume, bool copySource)
        //{
        //    if (copySource)
        //        VideoPlayerElement.Source = player.VideoPlayerElement.Source;

        //    Volume = volume;
        //    MediaState = player.MediaState;
        //    VM.Title = player.VM.Title;
        //    VM.Settings.FileName = player.FileName;
        //    IsFlipHorizontally = player.IsFlipHorizontally;
        //    Zoom = player.Zoom;
        //    ZoomStateSet(player.ZoomStateGet(), true);
        //    ScrollOffsetY = player.ScrollOffsetY;
        //    PositionSet(player.Position, true);
        //}

        //public void RestoreMediaState(MediaState state, TimeSpan position)
        //{
        //    Play();
        //    PositionSet(position, true);
        //    if (state == MediaState.Stop)
        //        Stop();
        //    if (state == MediaState.Pause)
        //        Pause();
        //}

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.OriginalSource is MediaElement)
                LeftButtonDoubleClick();
        }

        private Point _mousePos = new Point();
        private void UserControl_PreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _mousePos = e.GetPosition(this);
        }

        private void UserControl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(this);
            if ((pos - _mousePos).Length > 12.0) //detect mouse moved more than 12 points
                return;

            object s = e.OriginalSource;
            if (e.ChangedButton == MouseButton.Left && ((s is MediaElement) || (s is ScrollViewer) || (s is Grid)))
                LeftButtonClick();
        }

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if ((VM.IsPopWindowMode && VM.IsPopUpWindowActive && VM.IsFullScreen()) || 
                (!VM.IsPopWindowMode && Application.Current.MainWindow.IsActive))
            {
                this.Focus();
                IsInFocus = true;
                _borderMain.BorderBrush = Brushes.Tan; // Brushes.DodgerBlue;
            }
        }

        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsInFocus = false;
            _borderMain.BorderBrush = Brushes.Transparent;
        }

        private bool _isDragging = false;
        private void UserControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            const double scrollBarWidth = 12.0;
            Point pt = e.GetPosition(_scrollPlayerContainer);

            if (e.LeftButton == MouseButtonState.Pressed && !_isDragging)
            {
                //if drag start point is out of view
                if (pt.X > _scrollPlayerContainer.ActualWidth - scrollBarWidth ||
                    pt.Y > _scrollPlayerContainer.ActualHeight - scrollBarWidth)
                    return;

                //not moved enough to start drag
                if (Math.Abs((_mousePos - pt).Length) < scrollBarWidth)
                    return;

                _isDragging = true;
                DragDropSource = this;
                DragDrop.DoDragDrop(this, VM.Settings, System.Windows.DragDropEffects.Move | System.Windows.DragDropEffects.Copy);
                e.Handled = true;
            }

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _isDragging = false;
            }
        }

        private async void UserControl_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

                // handling code you have defined.
                _ = VM.OpenFromFile(files[0], startFrom0: false);
            }
            else if (e.Data.GetDataPresent(DragDropDataFormat))
            {
                VideoPlayerUserControl vFrom = DragDropSource;
                OnePlayerSettings setFrom = new OnePlayerSettings((OnePlayerSettings)(e.Data.GetData(DragDropDataFormat)));
                OnePlayerSettings setTo = new OnePlayerSettings(this);
                if (!string.IsNullOrWhiteSpace(setFrom.FileName) && setFrom.FileName != setTo.FileName)
                {
                    _ = this.LoadSetting(setFrom, VM.IsPopWindowMode);
                    //if CTRL is pressed - "copy" the conthent
                    //else - update vFrom - "exchange"
                    if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                        _ = vFrom?.LoadSetting(setTo, vFrom.VM.IsPopWindowMode);
                }
            }
            DragDropSource = null;
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
            VM.Settings.Position = position.TotalSeconds;
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
        public double Duration => NaturalDuration > 0.0 ? NaturalDuration : VM.Settings.Duration;

        public bool IsInFocus { get; private set; } = false;

        public void ScrollToCenter()
        {
            _scrollDragger.ScrollToCenter();
        }

        //public async Task Open_OLD(string fileName, double volume = 0)
        //{
        //    VM.Settings.FileName = fileName;
        //    VideoPlayerElement.Source = null;
        //    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
        //    {
        //        VM.IsLoading = true;
        //        VideoPlayerElement.Source = new Uri(fileName);
        //        //await VM.WaitForMediaOpened();
        //    }

        //    //wait for source opened
        //    //await WaitForSourceOpened(fileName);

        //    Volume = volume;
        //    VideoPlayerElement.IsMuted = true; //load silently
        //    MediaState = MediaState.Manual;

        //    if (NaturalDuration > 0)
        //        VM.Settings.Duration = NaturalDuration;

        //    VM.Title = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(fileName)) + "/" + System.IO.Path.GetFileName(fileName);
        //    List<string> fileNames = VM.GetFileNames(fileName, out int idx);
        //    if (idx >= 0)
        //        VM.Title = $"{idx}/{fileNames.Count} " + VM.Title;
        //}

        public async Task LoadSetting(OnePlayerSettings s, bool pop = false)
        {
            await VM.OpenFromSettings(s, pop);
        }

        public void Clear()
        {
            this.Background = Brushes.LightGray;
            MediaState = MediaState.Close;
            VM.Clear();
        }

        public async Task Play()
        {
            if (VideoPlayerElement.Source != null)
            {
                bool isMuted = VideoPlayerElement.IsMuted;
                VideoPlayerElement.IsMuted = true;

                _timer.Start();
                VideoPlayerElement.Play();
                VideoPlayerElement.IsMuted = true;
                SetBackColor(bActive: true);
                MediaState = MediaState.Play;
                PositionSet(TimeSpan.FromSeconds(VM.Settings.Position), false);
                
                VideoPlayerElement.IsMuted = isMuted;
            }
            else
            {
                Clear();
                _timer.Stop();
            }
        }

        public void Pause(bool updateUI)
        {
            if (VideoPlayerElement.Source != null)
            {
                _timer.Stop();

                VideoPlayerElement.Pause();
                MediaState = MediaState.Pause;

                if (updateUI)
                {
                    VM.Settings.Update(this);
                    VM.Update(VM.Settings, VM.IsPopWindowMode, lockUpdate: false);
                    SetBackColor(bActive: false);
                }
            }
        }

        public void SetBackColor(bool bActive)
        {
            Brush color = bActive ? Brushes.Black : Brushes.LightGray;

            if (VideoPlayerElement.Source != null)
            {
                if (!bActive)
                {
                    this.Background = color;
                    this.Background = ColorUtils.CalculateAverageColor(VideoPlayerElement); // Brushes.DarkGray;
                    //ScrollToCenter();
                }
                else
                {
                    this.Background = color;
                }
            }
            else
            {
                this.Background = Brushes.Gray;
            }
        }

        public async Task Stop()
        {
            if (VideoPlayerElement.Source != null)
            {
                _timer.Stop();
                VideoPlayerElement.Stop();
                SetBackColor(bActive: false);
                MediaState = MediaState.Stop;
                
                VM.Settings.Update(this);
                VM.Update(VM.Settings, VM.IsPopWindowMode, lockUpdate: false);

                //clear window - sometimes it is not closed properly
                RecreateMediaElement(false);
            }
        }

        public void TogglePlayPauseState()
        {
            if (MediaState == MediaState.Play)
                Pause(updateUI: true);
            else //if (MediaState == MediaState.Pause || MediaState == MediaState.Manual)
                Play();
        }

        private void mePlayer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftShift))
                return;

            e.Handled = true;
            VM.VolumeUpdate(e.Delta);
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
                    VideoPlayerElement.MouseWheel -= mePlayer_MouseWheel;
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
                eZoomState zoomState = _scrollDragger.ZoomState;

                _scrollDragger = new ScrollDragZoom(VideoPlayerElement, _scrollPlayerContainer);
                _scrollDragger.Zoom = zoom;
                _scrollDragger.VerticalOffset = vOff;
                ZoomStateSet(zoomState, true);

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
                    VideoStartedAction(null);
                };
                VideoPlayerElement.MediaEnded += (s, e) => { VideoEnded(null); };
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            _ = VM.OpenFromSettings(new OnePlayerSettings(VM.Settings), VM.IsPopWindowMode);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptions(Application.Current.MainWindow, VM.Settings, "Settings", 650);
            VM.NotifyPropertyChangedAll();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            VM.MaximizeToggle(hide: false);
            UpdateMaximizeButtonImage();
        }

        private void UpdateMaximizeButtonImage()
        {
            bool isMaximized = VM.IsFullScreen();

            _down.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
            _up.Visibility = isMaximized ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Speed_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string parameter = menuItem.Tag as string;
                if (int.TryParse(parameter, out int speedIndex))
                    VM.SetSpeed(speedIndex, true);
            }
        }

        private void Fit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string parameter = menuItem.Tag as string;
                if (int.TryParse(parameter, out int fit))
                    VM.SetFit(fit, true);
            }
        }

        private void PlayMode_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string parameter = menuItem.Tag as string;
                if (int.TryParse(parameter, out int mode))
                    VM.Settings.PlayMode = (ePlayMode)mode;
                VM.NotifyPropertyChanged(nameof(VM.SelectedPlayModeIndex));
            }
        }

        private void _commands_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Cursor = _commands.IsVisible ? System.Windows.Input.Cursors.Arrow : System.Windows.Input.Cursors.None;
        }

        private void BookmarksClear_Click(object sender, RoutedEventArgs e)
        {
            VM.Replay.BookmarksClear();
        }

        private void FileListMenu_OpenMenu(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button && button.ContextMenu != null)
            {
                if (button.ContextMenu.IsOpen)
                {
                    button.ContextMenu.IsOpen = false;
                }
                else
                {
                    button.ContextMenu.PlacementTarget = button;
                    button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                    button.ContextMenu.Items.Clear();

                    List<string> fileNames = VM.GetFileNames(FileName, out int idx);
                    for (int i = 0; i < fileNames.Count; i++)
                    {
                        MenuItem item = new MenuItem();

                        bool isFavorite = MainWindow.IsFavorite(fileNames[i]);
                        FontWeight fontWeight = (i == idx || isFavorite) ? FontWeights.Bold : FontWeights.Normal;
                        Brush foreground = (isFavorite && i != idx) ? Brushes.SaddleBrown : Brushes.Navy;

                        item.Icon = new TextBlock { Text = (i + 1).ToString(), FontSize = 12, FontWeight = fontWeight, Foreground = foreground };
                        item.Header = new TextBlock { Text = System.IO.Path.GetFileName(fileNames[i]), FontWeight = fontWeight, Foreground = foreground };

                        item.Command = VM.OpenFileByNameCommand;
                        item.CommandParameter = fileNames[i];
                        
                        button.ContextMenu.Items.Add(item);
                    }

                    button.ContextMenu.IsOpen = true;
                }
            }
        }

        private void DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            VM.DeleteFile(bPrev: true);
        }
    }
}
