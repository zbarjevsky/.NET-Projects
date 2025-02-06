using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls.Primitives;

using MkZ.Windows;
using MkZ.WPF;
using MultiPlayer.MkZ.WPF;

using Brushes = System.Windows.Media.Brushes;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Point = System.Windows.Point;
using System.Diagnostics;
using System.Windows.Shapes;


namespace MultiPlayer
{
    public class VideoCommandsVM : NotifyPropertyChangedImpl
    {
        public const string PLAY_TEXT  = "\xE768";
        public const string PAUSE_TEXT = "\xE769";
        public const string NEXT_TEXT  = "\xE893";
        public const string PREV_TEXT  = "\xE892";
        public const string CHECK_TEXT = "\xE73E";

        VideoPlayerUserControl _player;
        VideoCommandsUserControl _cmd;

        private RecentFile _recentFile = null;
        private Stopwatch _playSartTime = Stopwatch.StartNew();

        private bool _isLoading;
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        public OnePlayerSettings Settings { get; set; } = new OnePlayerSettings();

        public bool IsPopWindowMode { get; private set; } = false;

        public RelayCommand TogglePlayPauseCommand { get; }
        public RelayCommand OpenFileCommand { get; }
        public RelayCommand PrevFileCommand { get; }
        public RelayCommand NextFileCommand { get; }
        public RelayCommand SkipOneFrameCommand { get; }
        public RelayCommand Skip10SecondsCommand { get; }

        public VideoCommandsVM()
        {
            Replay = new ReplayLoop(this);

            TogglePlayPauseCommand = new RelayCommand(TogglePlayPauseCommandExecute, TogglePlayPauseCommandCanExecute);
            OpenFileCommand = new RelayCommand(OpenFileCommandExecute);
            PrevFileCommand = new RelayCommand(PrevFileCommandExecute, PrevFileCommandCanExecute);
            NextFileCommand = new RelayCommand(NextFileCommandExecute, NextFileCommandCanExecute);
            Skip10SecondsCommand = new RelayCommand(Skip10SecondsCommandExecute, (o) => true);
            SkipOneFrameCommand = new RelayCommand(SkipOneFrameCommandExecute, (o) => true);
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public double Volume
        {
            get { return _player != null ? _player.Volume : 0; }
        }


        public bool IsMuted
        {
            get { return _player.VideoPlayerElement.IsMuted; }
            set { _player.VideoPlayerElement.IsMuted = value; NotifyPropertyChanged(); }
        }

        public TimeSpan Position
        {
            get { return _player.VideoPlayerElement.Position; }
            set { _player.VideoPlayerElement.Position = value; Settings.Position = value.TotalSeconds; NotifyPropertyChanged(); }
        }
        private string _playPauseIconText = PLAY_TEXT;
        public string PlayPauseIconText
        {
            get { return _playPauseIconText; }
            set { SetProperty(ref _playPauseIconText, value); }
        }

        public int SelectedSpeedIndex
        {
            get { return _cmd._speed.SelectedIndex; }
        }

        public int SelectedFitIndex
        {
            get { return (int)Settings.ZoomState; }
        }

        public int SelectedPlayModeIndex
        {
            get { return (int)Settings.PlayMode; }
        }

        public void Play()
        {
            //Debug.WriteLine($"### MediaPlayStarted - Requested, File: {Settings.FileName}");

            _playSartTime.Restart();

            _player.Play();
            _cmd._btnPlayPause.Background = Brushes.LightGreen;
            PlayPauseIconText = PAUSE_TEXT;
        }

        public void Pause()
        {
            _player.Pause();
            _cmd._btnPlayPause.Background = Brushes.LightGoldenrodYellow;
            PlayPauseIconText = PLAY_TEXT;
        }

        public void Stop()
        {
            _player.Stop();
            _cmd._btnPlayPause.Background = Brushes.LightGray;
            PlayPauseIconText = PLAY_TEXT;
        }

        public void Clear()
        {
            _player.Stop();
            Settings.FileName = "";
            _player.VideoPlayerElement.Source = null;

            double volume = _cmd._volume.Value;
            ePlayMode playMode = Settings.PlayMode;
            int fitIndex = _cmd._fit.SelectedIndex;

            Settings = new OnePlayerSettings();
            Update(Settings, IsPopWindowMode, lockUpdate: false);

            _cmd._volume.Value = volume;
            _cmd._fit.SelectedIndex = fitIndex; //ZoomState;
            Settings.Volume = volume / 1000.0;
            Settings.PlayMode = playMode;

            Replay.IsReplayChecked = false;

            Title = "";
            TogglePlayPauseCommand.RefreshBoundControls();
        }

        private void OpenFileCommandExecute(object obj)
        {
            string fileName = Settings.FileName;
            string dir = System.IO.Path.GetDirectoryName(Settings.FileName);
            OpenFileDialog ofd = new OpenFileDialog();
            if (Directory.Exists(dir))
            {
                ofd.InitialDirectory = dir;
                ofd.FileName = fileName;
            }

            MediaState state = _player.MediaState;
            if (state == MediaState.Play)
                Pause();

            if (ofd.ShowDialog().Value)
            {
                _ = OpenFromFile(ofd.FileName, startFrom0: true);
            }
            else // cancel - continue play if needed
            {
                if (state == MediaState.Play)
                    Play();
            }
        }

        private void TogglePlayPauseCommandExecute(object parameter)
        {
            if (_player.MediaState == MediaState.Play)
                Pause();
            else
                Play();
        }

        private bool TogglePlayPauseCommandCanExecute(object parameter)
        {
            bool res = !string.IsNullOrWhiteSpace(Settings.FileName);
            return res;
        }

        public void Init(VideoPlayerUserControl v, VideoCommandsUserControl c)
        {
            _cmd = c;

            _player = v;
            _player.PropertyChanged += _videoPlayerUserControl_PropertyChanged;

            _player.LeftButtonClick = () => TogglePlayPauseCommand.Execute(null);
            _player.LeftButtonDoubleClick = () => Maximize_Click(this, null);

            _player.VideoStartedAction = (player) => MediaPlayStarted(player);
            _player.VideoEnded = (player) => MediaPlayEnded(player); 
            _player.VideoFailed = (e, player) => MediaPlayFailed(e, player); 
        }

        bool _isInUpdate = false;
        public void Update(OnePlayerSettings s, bool pop = false, bool lockUpdate = true)
        {
            IsPopWindowMode = pop;

            if (lockUpdate)
                _isInUpdate = true;

            Settings.Update(s);

            _cmd._volume.Value = s.Volume * 1000.0;
            _cmd._position.Maximum = s.Duration;
            _cmd._position.Value = s.Position;

            _player._progress.Maximum = s.Duration;
            _player._progress.Value = s.Position;

            _cmd._speed.SelectedIndex = SpeedRatio(s.SpeedRatio);

            _cmd._fit.SelectedIndex = (int)s.ZoomState;
            if (s.ZoomState == eZoomState.Custom)
                _player.Zoom = s.Zoom;

            _cmd._timeLbl.Text = SecondsToString(s.Position);

            NotifyPropertyChanged(nameof(SelectedFitIndex));

            Replay.UpdateReplay(s.ReplayIsOn, s.ReplayPosA, s.ReplayPosB, s.ReplayPosC, s.ReplayPosD);

            AdjustMarginsForVisibleScrollBars();
            AdjustSizeAndLayout();

            _isInUpdate = false;
        }

        public void UpdateRrecentFile(OnePlayerSettings s)
        {
            _recentFile = MainWindow.FindOrCreateRecentFile(s.FileName);
            _recentFile.Update(s);
        }

        private void _videoPlayerUserControl_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Position":
                    _cmd._position.Value = _player.Position.TotalSeconds;
                    break;
                default:
                    break;
            }
        }

        //use settings from current player
        // - volume
        // - play mode
        // - window fit
        public async Task OpenFromFile(string fileName, bool startFrom0)
        {
            if (_recentFile != null) //update previous recent file
                _recentFile.Update(Settings);
            _player.Clear();

            Settings.FileName = fileName;

            _recentFile = MainWindow.FindOrCreateRecentFile(fileName);

            Settings.Position = startFrom0 ? 0 : _recentFile.Position;

            Replay.UpdateReplay(false, _recentFile.ReplayPosA, _recentFile.ReplayPosB, _recentFile.ReplayPosC, _recentFile.ReplayPosD);

            Settings.MediaState = MediaState.Play;

            await OpenFromSettings(new OnePlayerSettings(Settings), IsPopWindowMode);

            //CommandManager.InvalidateRequerySuggested();
        }

        public async Task OpenFromSettings(OnePlayerSettings s, bool pop = false)
        {
            IsPopWindowMode = pop;

            Clear();
            if (string.IsNullOrEmpty(s.FileName) || !File.Exists(s.FileName))
            {
                _player.VideoPlayerElement.ForceRender();
                return;
            }

            this.IsLoading = true;
            Settings.FileName = s.FileName;
            Settings.Position = s.Position;
            Settings.Volume = s.Volume;

            _player.VideoPlayerElement.Source = new Uri(s.FileName);
            _player.PositionSet(TimeSpan.FromSeconds(s.Position), false);
            _player.Volume = s.Volume;

            this.Title = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(s.FileName)) + "/" + System.IO.Path.GetFileName(s.FileName);
            List<string> fileNames = this.GetFileNames(s.FileName, out int idx);
            if (idx >= 0)
                this.Title = $"{idx}/{fileNames.Count} " + this.Title;

            this.IsMuted = true; //load silently

            this.Play();

            //wait for source opened
            await WaitForNaturalDurationUpdated(s.FileName);

            if (_player.NaturalDuration > 0)
                s.Duration = _player.NaturalDuration;

            if (s.MediaState != MediaState.Play)
            {
                this.Pause();
            }

            await WaitForMediaOpened();

            this.IsMuted = false; //restore volume

            this.Update(s, pop, lockUpdate: false);
            this.UpdateRrecentFile(s);

            this.TogglePlayPauseCommand.RefreshBoundControls();
            this.AdjustSizeAndLayout();
        }

        private async Task WaitForNaturalDurationUpdated(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                //wait for source opened
                int i = 0;
                for (; i < 10; i++)
                {
                    if (_player.NaturalDuration > 0)
                        break;

                    _player.VideoPlayerElement.ForceRender();
                    await Task.Delay(333);
                }

                Debug.WriteLine("### Check NaturalDuration, tries {0} - Duration: {1:###,##0} sec, file: {2}", i, _player.NaturalDuration, fileName);
            }
        }

        private void MediaPlayStarted(IVideoPlayer player)
        {
            IsLoading = false;
            TimeSpan delta = _playSartTime.Elapsed;
            Debug.WriteLine($"### MediaPlayStarted - Elapsed: {delta.TotalSeconds.ToString("0.000")}, File: {Settings.FileName}");
            _waitMediaOpenedEvent.TriggerEvent();
        }

        private void MediaPlayEnded(IVideoPlayer player)
        {
            Stop();
            Settings.Position = 0;

            switch (Settings.PlayMode)
            {
                case ePlayMode.PlayOne:
                    break;
                case ePlayMode.PlayAll:
                    PlayNext(random: false, loop: false);
                    break;
                case ePlayMode.RepeatAll:
                    PlayNext(random: false, loop: true);
                    break;
                case ePlayMode.Random:
                    PlayNext(random: true, loop: true);
                    break;
                case ePlayMode.RepeatOne:
                default:
                    OpenFromFile(Settings.FileName, startFrom0: true);
                    break;
            }
        }

        private bool MediaPlayFailed(ExceptionRoutedEventArgs e, MediaElement player)
        {
            IsLoading = false;
            System.Windows.MessageBox.Show(e.ErrorException.Message + "\n" + Settings.FileName, "MediaPlayFailed");
            e.Handled = true;
            return true;
        }

        public void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player != null && _cmd != null)
            {
                Settings.Volume = _cmd._volume.Value / 1000.0;
                _player.Volume = Settings.Volume;
                NotifyPropertyChanged(nameof(Volume));
            }
        }

        public void VolumeUpdate(int delta)
        {
            int idx = GetVolumeIndex();
            if (delta > 0)
                idx++;

            if (delta < 0)
                idx--;

            if (idx < 0) idx = 0;
            if (idx >= _volumeLevels.Length - 1) idx = _volumeLevels.Length - 2;

            _cmd._volume.Value = _volumeLevels[idx];
        }

        double [] _volumeLevels = { 0, 10, 20, 30, 40, 50, 60, 80, 100, 200, 300, 400, 500, 600, 800, 1000, 1001 };

        private int GetVolumeIndex()
        {
            double vol = _cmd._volume.Value;
            for (int i = 0; i < _volumeLevels.Length; i++)
            {
                if (_volumeLevels[i] <= vol && vol < _volumeLevels[i+1])
                    return i;
            }
            return 100;
        }

        double[] _speedRatios = { 0.01, 0.1, 0.2, 0.5, 1.0, 1.5 };

        public void Speed_Selected(object sender, SelectionChangedEventArgs e)
        {
            SetSpeed(_cmd._speed.SelectedIndex);
        }

        private int SpeedRatio(double speed)
        {
            for (int i = 0; i < _cmd._speed.Items.Count; i++)
            {
                if (speed == _speedRatios[i])
                    return i;
            }
            return 4; //1.0x
        }

        internal void SetSpeed(int speedIndex, bool updateComboBox = false)
        {
            if (_player == null || _isInUpdate)
                return;

            Settings.SpeedRatio = _speedRatios[speedIndex];
            _player.SpeedRatio = Settings.SpeedRatio;

            if (updateComboBox)
                _cmd._speed.SelectedIndex = speedIndex;

            NotifyPropertyChanged(nameof(SelectedSpeedIndex));
        }

        public void Fit_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (_cmd == null)
                return;

            SetFit(_cmd._fit.SelectedIndex);
        }

        internal void SetFit(int fitIndex, bool updateComboBox = false)
        {
            if (_player == null || _isInUpdate)
                return;

            Settings.ZoomState = (eZoomState)fitIndex;
            _player.ZoomState = Settings.ZoomState;

            if (updateComboBox)
                _cmd._fit.SelectedIndex = fitIndex;

            NotifyPropertyChanged(nameof(SelectedFitIndex));

            AdjustMarginsForVisibleScrollBars();
        }

        private void AdjustMarginsForVisibleScrollBars()
        {
            double bottom = (_player._scrollPlayerContainer.ComputedHorizontalScrollBarVisibility == Visibility.Visible) ? 12.0 : 0.0;
            _cmd.Margin = new Thickness(0, 0, 0, bottom);
        }

        private bool _resume = false;
        public void Pos_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (_player == null || _isInUpdate)
                return;

            _resume = (_player.MediaState == MediaState.Play);
            if (_resume)
                Pause();
        }

        public void Pos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player == null || _isInUpdate)
                return;

            IsMuted = true;
            bool resume = (_player.MediaState == MediaState.Play);
            if (resume)
                Pause();

            Settings.Position = e.NewValue;
            _cmd._position.Value = Settings.Position;
            _player.PositionSet(TimeSpan.FromSeconds(Settings.Position), false);

            _cmd._timeLbl.Text = SecondsToString(_player.Position.TotalSeconds);

            if (resume)
                Play();

            IsMuted = false;
            e.Handled = true;
        }

        public static string SecondsToString(double seconds)
        {
            if (seconds < 3600)
                return TimeSpan.FromSeconds(seconds).ToString("m':'ss'.'f");
            else
                return TimeSpan.FromSeconds(seconds).ToString("h':'mm':'ss'.'f");
        }

        public void Pos_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (_player == null || _isInUpdate)
                return;

            Settings.Position = _cmd._position.Value;
            _player.PositionSet(TimeSpan.FromSeconds(Settings.Position), false);

            if (_resume)
                Play();

            e.Handled = true;
        }

        private bool NextFileCommandCanExecute(object obj)
        {
            List<string> fileNames = GetFileNames(Settings.FileName, out int idx);
            return idx >= 0 && idx < fileNames.Count - 1;
        }

        private void NextFileCommandExecute(object obj)
        {
            PlayNext();
        }

        private bool PrevFileCommandCanExecute(object obj)
        {
            List<string> fileNames = GetFileNames(Settings.FileName, out int idx);
            return idx > 0;
        }

        private void PrevFileCommandExecute(object obj)
        {
            List<string> fileNames = GetFileNames(Settings.FileName, out int idx);
            idx--;
            if (idx < 0)
                idx = fileNames.Count - 1;
            OpenFromFile(fileNames[idx], startFrom0: true);
        }

        private Random _random = new Random();
        private void PlayNext(bool random = false, bool loop = false)
        {
            List<string> fileNames = GetFileNames(Settings.FileName, out int idx);
            if (random)
            {
                idx = _random.Next(fileNames.Count - 1);
            }
            else
            {
                idx++;
                if (loop && (idx >= fileNames.Count || idx < 0))
                    idx = 0; //loop
            }

            if (idx >= 0 && idx < fileNames.Count)
                OpenFromFile(fileNames[idx], startFrom0: true);
        }

        public List<string> GetFileNames(string fileName, out int idx)
        {
            idx = -1;
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
                return new List<string>();

            string dir = System.IO.Path.GetDirectoryName(fileName);

            List<string> fileNames = System.IO.Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                .Where(
                    file => Settings.SupportedVideoExtensions.Contains(System.IO.Path.GetExtension(file).ToLower()) || 
                            Settings.SupportedAudioExtensions.Contains(System.IO.Path.GetExtension(file).ToLower())).ToList();
            fileNames.Sort();
            idx = fileNames.IndexOf(fileName);

            return fileNames;
        }

        public void Maximize_Click(object sender, RoutedEventArgs e)
        {
            MaximizeToggle(hide: false);
        }

        public bool IsFullScreen()
        {
            if (IsPopWindowMode)
                return WndMax.IsFullScreen;
            return false;
        }

        public static void PopUpClear()
        {
            WndMax.ClearVideoControl();
        }

        public static void PopUpHide()
        {
            WndMax.ClearVideoControl();
            WndMax.Visibility = Visibility.Collapsed;
        }

        private static readonly PopUpWindow WndMax = new PopUpWindow();
        public void MaximizeToggle(bool hide)
        {
            if (IsPopWindowMode)
            {
                if (hide)
                {
                    PopUpHide();
                }
                else
                {
                    WndMax.MaximizeToggle();
                }
            }
            else
            {
                if (WndMax.Visibility == Visibility.Collapsed)
                {
                    WndMax.InitWindow(System.Windows.Application.Current.MainWindow);
                    WndMax.Show();
                    WndMax.LoadSettings(new OnePlayerSettings(_player));

                    this.Pause();
                }
            }
        }

        internal static void Exit()
        {
            WndMax.Exit();
        }

        public void Pos_MouseMove(object sender, MouseEventArgs e)
        {
            ShowTimeToolTip(sender, e);
        }

        private void ShowTimeToolTip(object sender, MouseEventArgs e)
        {
            if (sender is Slider slider)
            {
                Point currentPos = e.GetPosition(slider);
                if (currentPos.Y < 30)
                {
                    if (!_cmd._popupSliderTooltip.IsOpen)
                        _cmd._popupSliderTooltip.IsOpen = true;

                    Track track = slider.Template.FindName("PART_Track", slider) as Track;

                    _cmd._txtSliderTooltip.Text = SecondsToString(track.ValueFromPoint(currentPos));
                    string dur = SecondsToString(_player.Duration);
                    _cmd._txtSliderTooltip.Text += " / " + dur;

                    _cmd._popupSliderTooltip.HorizontalOffset = currentPos.X - (_cmd._borderSliderTooltip.ActualWidth / 2);
                    _cmd._popupSliderTooltip.VerticalOffset = -20;
                }
                else
                {
                    _cmd._popupSliderTooltip.IsOpen = false;
                }
            }
        }

        public void Pos_MouseLeave(object sender, MouseEventArgs e)
        {
            _cmd._popupSliderTooltip.IsOpen = false;
        }

        public bool Control_KeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space || e.Key == System.Windows.Input.Key.MediaPlayPause)
                TogglePlayPauseCommand.Execute(null);
            else if (e.Key == System.Windows.Input.Key.Up || e.Key == System.Windows.Input.Key.Down)
                VolumeUpdate(e.Key == System.Windows.Input.Key.Up ? 120 : -120);
            else if (e.Key == System.Windows.Input.Key.Left || e.Key == System.Windows.Input.Key.Right)
                Skip10SecondsCommand.Execute(e.Key == System.Windows.Input.Key.Right);
            else if (e.Key == System.Windows.Input.Key.OemComma || e.Key == System.Windows.Input.Key.OemPeriod)
                SkipOneFrameCommand.Execute(e.Key == System.Windows.Input.Key.OemPeriod);
            else
                return false;

            return true;
        }

        private void Skip10SecondsCommandExecute(object obj)
        {
            if (obj is bool nextFrame)
                SkipPosition(nextFrame ? 10.0 : -10.0);
        }

        private void SkipOneFrameCommandExecute(object obj)
        {
            if (obj is bool nextFrame)
                SkipPosition(nextFrame ? 0.1 : -0.1);
        }

        public void SkipPosition(double seconds)
        {
            _cmd._position.Value += seconds;
            Settings.Position = _cmd._position.Value;
        }

        public void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AdjustSizeAndLayout();
        }

        public void AdjustSizeAndLayout()
        {
            if (_cmd == null || _player == null) 
                return;

            _cmd._scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            double windowWidth = _cmd.ActualWidth;
            if (windowWidth < 50)
                return; //too small

            double xMargin = 2 + 2;
            double secondMinWidth = _cmd._timeLbl.ActualWidth + _cmd._docSliders.MinWidth;
            if (windowWidth - _cmd._stackButtons.ActualWidth > secondMinWidth) //enough for one line
            {
                _cmd._docSliders.Width = windowWidth - _cmd._timeLbl.ActualWidth - _cmd._stackButtons.ActualWidth - xMargin;
                _cmd._wrapPanel.Width = windowWidth;
            }
            else //two lines
            {
                if (windowWidth > _cmd._stackButtons.ActualWidth) //two lines - enough for buttons
                {
                    _cmd._docSliders.Width = windowWidth - _cmd._timeLbl.ActualWidth - 12;
                    _cmd._wrapPanel.Width = windowWidth;
                }
                else //wrapped to two lines and need scroll
                {
                    double minWidth = _cmd._stackButtons.ActualWidth + 12;
                    _cmd._docSliders.Width = minWidth - _cmd._timeLbl.ActualWidth;
                    _cmd._wrapPanel.Width = minWidth;

                    _cmd._scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
            }

            Replay.UpdateTicks();
        }

        private WaitForEventImpl _waitMediaOpenedEvent = new WaitForEventImpl();

        public async Task WaitForMediaOpened()
        {
            if (IsLoading == false) //already loaded
                return; 

            Stopwatch sw = Stopwatch.StartNew();

            await _waitMediaOpenedEvent.WaitForEvent();
            //await Task.Delay(100);

            TimeSpan elapsed = sw.Elapsed;
            Debug.WriteLine($"*** WaitForMediaOpened {elapsed.TotalSeconds.ToString("0.000")} sec - file: {Settings.FileName}");
        }

        public class WaitForEventImpl
        {
            public async Task WaitForEvent()
            {
                var tcs = new TaskCompletionSource<object>();

                // Simulate an event
                EventHandler handler = null;
                handler = (sender, args) =>
                {
                    tcs.SetResult(null);
                    Event -= handler; // Unsubscribe to prevent memory leaks
                };

                Event += handler;

                // Await the task until the event is triggered
                await tcs.Task;
            }

            public event EventHandler Event;

            // Method to trigger the event (can be called externally)
            public void TriggerEvent()
            {
                Event?.Invoke(this, EventArgs.Empty);
            }
        }

        public ReplayLoop Replay { get; }

        /// <summary>
        /// Replay in loop between two points A-B
        /// </summary>
        public class ReplayLoop : NotifyPropertyChangedImpl
        {
            VideoCommandsVM VM { get; }

            public bool IsReplayChecked
            {
                get { return VM.Settings.ReplayIsOn; }
                set { VM.Settings.ReplayIsOn = value; NotifyPropertyChanged(); }
            }

            public string ReplayToolTip
            {
                get
                {
                    if (VM.Settings.ReplayPosA <= 0 || VM.Settings.ReplayPosB <= 0)
                        return "Positions A-B not set";
                    return $"Replay from: {SecondsToString(VM.Settings.ReplayPosA)} to: {SecondsToString(VM.Settings.ReplayPosB)}";
                }
            }

            public ReplayLoop(VideoCommandsVM vm)
            {
                VM = vm;
            }

            public void Clear()
            {
                UpdateReplay(false, 0, 0, 0, 0);
            }

            public void UpdateReplay(bool on, double a, double b, double c, double d)
            {
                VM.Settings.ReplayIsOn = on;
                VM.Settings.ReplayPosA = a;
                VM.Settings.ReplayPosB = b;

                VM.Settings.ReplayPosC = c;
                VM.Settings.ReplayPosD = d;

                UpdateTicks();
                NotifyPropertyChanged(nameof(IsReplayChecked));
                NotifyPropertyChanged(nameof(ReplayToolTip));
            }

            public void ReplayToggle(bool isChecked)
            {
                if (isChecked && (VM.Settings.Position < VM.Settings.ReplayPosA || VM.Settings.Position > VM.Settings.ReplayPosB)) 
                    ReplaySetStart();
                UpdateTicks();
            }

            public void SetA()
            {
                double delta = VM.Settings.ReplayPosB - VM.Settings.ReplayPosA;
                if (delta < 1.0) delta = 10.0;
                
                if (VM.Settings.Position < VM.Settings.ReplayPosB)
                {
                    VM.Settings.ReplayPosA = VM.Settings.Position;
                }
                else //move B to A + delta
                {
                    VM.Settings.ReplayPosA = VM.Settings.Position;
                    VM.Settings.ReplayPosB = VM.Settings.Position + delta;
                }

                UpdateTicks();
                NotifyPropertyChanged(nameof(ReplayToolTip));
            }

            public void SetB()
            {
                double delta = VM.Settings.ReplayPosB - VM.Settings.ReplayPosA;
                if (delta < 1.0) delta = 10.0;

                if (VM.Settings.Position > VM.Settings.ReplayPosA)
                {
                    VM.Settings.ReplayPosB = VM.Settings.Position;
                }
                else
                {
                    VM.Settings.ReplayPosB = VM.Settings.Position;
                    VM.Settings.ReplayPosA = VM.Settings.Position - delta;
                }

                UpdateTicks();
                NotifyPropertyChanged(nameof(ReplayToolTip));
            }

            public void SetC()
            {
                VM.Settings.ReplayPosC = VM.Settings.Position;
                UpdateTicks();
            }

            public void SetD()
            {
                VM.Settings.ReplayPosD = VM.Settings.Position;
                UpdateTicks();
            }

            //set ticks positions and color if active
            public void UpdateTicks()
            {
                UpdateTick(VM._cmd._lineA, VM.Settings.ReplayPosA, IsReplayChecked ? Brushes.Lime : Brushes.AliceBlue);
                UpdateTick(VM._cmd._lineB, VM.Settings.ReplayPosB, IsReplayChecked ? Brushes.Lime : Brushes.AliceBlue);

                UpdateTick(VM._cmd._lineC, VM.Settings.ReplayPosC, Brushes.Pink);
                UpdateTick(VM._cmd._lineD, VM.Settings.ReplayPosD, Brushes.Yellow);
            }

            private void UpdateTick(Line line, double position, System.Windows.Media.Brush stroke)
            {
                line.X1 = line.X2 = XfromTime(position);
                line.Stroke = stroke;

                //line.ToolTip = new System.Windows.Controls.ToolTip()
                //{
                //    Content = SecondsToString(position),
                //    Background = Brushes.LightYellow
                //};
            }

            private double XfromTime(double time)
            {
                double width = VM._cmd._position.ActualWidth - 12;
                double seconds = VM.Settings.Duration;

                if (seconds <= 0 || time <= 0)
                    return -1000;

                double x = 8 + time * width / seconds;
                
                return x;
            }

            private void ReplaySetStart()
            {
                VM._cmd._position.Value = VM.Settings.ReplayPosA;
                VM.Settings.Position = VM._cmd._position.Value;
            }

            public void ReplayCheckAndUpdate()
            {
                if (IsReplayChecked && (VM._cmd._position.Value < VM.Settings.ReplayPosA || VM._cmd._position.Value > VM.Settings.ReplayPosB))
                    ReplaySetStart();
            }
        }
    }
}
