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
        private DateTime _playSartTime = DateTime.MinValue;

        public OnePlayerSettings Settings { get; set; } = new OnePlayerSettings();

        public bool IsPopWindowMode { get; private set; } = false;

        public RelayCommand TogglePlayPauseCommand { get; }
        public RelayCommand OpenFileCommand { get; }
        public RelayCommand PrevFileCommand { get; }
        public RelayCommand NextFileCommand { get; }

        public VideoCommandsVM()
        {
            Replay = new ReplayLoop(this);

            TogglePlayPauseCommand = new RelayCommand(TogglePlayPauseCommandExecute, TogglePlayPauseCommandCanExecute);
            OpenFileCommand = new RelayCommand(OpenFileCommandExecute);
            PrevFileCommand = new RelayCommand(PrevFileCommandExecute, PrevFileCommandCanExecute);
            NextFileCommand = new RelayCommand(NextFileCommandExecute, NextFileCommandCanExecute);
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

        public void Play()
        {
            _playSartTime = DateTime.Now;

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
            double volume = _cmd._volume.Value;
            Settings = new OnePlayerSettings();
            Update(Settings, IsPopWindowMode);
            _cmd._volume.Value = volume;
            Settings.Volume = volume / 1000.0;

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

            if (ofd.ShowDialog().Value)
            {
                OpenFile(ofd.FileName, startFrom0: true);
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
        public void Update(OnePlayerSettings s, bool pop = false)
        {
            IsPopWindowMode = pop;

            _isInUpdate = true;

            Settings.Update(s);

            _cmd._volume.Value = s.Volume * 1000.0;
            _cmd._position.Maximum = s.Duration;
            _cmd._position.Value = s.Position;

            _cmd._speed.SelectedIndex = SpeedRatio(s.SpeedRatio);

            _cmd._fit.SelectedIndex = (int)s.ZoomState;
            if (s.ZoomState == eZoomState.Custom)
                _player.Zoom = s.Zoom;

            _cmd._timeLbl.Text = SecondsToString(s.Position);

            NotifyPropertyChanged(nameof(SelectedFitIndex));

            Replay.UpdateReplay(s.ReplayIsOn, s.ReplayPosA, s.ReplayPosB);

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

        public void OpenFile(string fileName, bool startFrom0)
        {
            if (_recentFile != null) //update previous recent file
                _recentFile.Update(Settings);
            _player.Clear();

            Settings.FileName = fileName;

            _player.Open(Settings.FileName, Settings.Volume);
            _cmd._position.Maximum = _player.NaturalDuration;

            _recentFile = MainWindow.FindOrCreateRecentFile(fileName);

            Settings.Position = startFrom0 ? 0 : _recentFile.Position;

            Replay.UpdateReplay(false, _recentFile.ReplayPosA, _recentFile.ReplayPosB);

            Play();

            CommandManager.InvalidateRequerySuggested();
        }

        private void MediaPlayStarted(IVideoPlayer player)
        {
            TimeSpan delta = DateTime.Now - _playSartTime;
            Debug.WriteLine($"### MediaPlayStarted: {delta} -- {player.FileName}");
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
                    OpenFile(Settings.FileName, startFrom0: true);
                    break;
            }
        }

        private bool MediaPlayFailed(ExceptionRoutedEventArgs e, MediaElement player)
        {
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
            double vol = _cmd._volume.Value;
            if (delta > 0)
            {
                if (vol < 100)
                    vol += 20;
                else
                    vol += 100;
            }
            if (delta < 0)
            {
                if (vol > 200)
                    vol -= 100;
                else if (vol > 100) 
                    vol = 100;
                else
                    vol -= 20;
            }
            if (vol < 0) vol = 0;
            if (vol > 1000) vol = 1000;

            _cmd._volume.Value = vol;
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

            bool resume = (_player.MediaState == MediaState.Play);
            if (resume)
                Pause();

            Settings.Position = e.NewValue;
            _cmd._position.Value = Settings.Position;
            _player.PositionSet(TimeSpan.FromSeconds(Settings.Position), false);

            _cmd._timeLbl.Text = SecondsToString(_player.Position.TotalSeconds);

            if (resume)
                Play();

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
            OpenFile(fileNames[idx], startFrom0: true);
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
                OpenFile(fileNames[idx], startFrom0: true);
        }

        public List<string> GetFileNames(string fileName, out int idx)
        {
            idx = -1;
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
                return new List<string>();

            string dir = System.IO.Path.GetDirectoryName(fileName);

            List<string> fileNames = System.IO.Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                .Where(
                    file => Settings.SupportedVideoExtensions.Contains(Path.GetExtension(file).ToLower()) || 
                            Settings.SupportedAudioExtensions.Contains(Path.GetExtension(file).ToLower())).ToList();
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

        public static void ClearPopUp()
        {
            WndMax.ClearVideoControl();
        }

        private static readonly PopUpWindow WndMax = new PopUpWindow();
        public void MaximizeToggle(bool hide)
        {
            if (IsPopWindowMode)
            {
                if (hide)
                {
                    this.Pause();
                    WndMax.Visibility = Visibility.Collapsed;
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

        public void PrevFrame_Click(object sender, RoutedEventArgs e)
        {
            _cmd._position.Value -= 0.1;
            Settings.Position = _cmd._position.Value;
        }

        public void NextFrame_Click(object sender, RoutedEventArgs e)
        {
            _cmd._position.Value += 0.1;
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
            if (windowWidth < 30)
                windowWidth = 620;

            if (windowWidth > _cmd._stackButtons.ActualWidth)
                _cmd._wrapPanel.Width = windowWidth;
            else
                _cmd._wrapPanel.Width = _cmd._stackButtons.ActualWidth;

            double width = windowWidth - _cmd._stackButtons.ActualWidth - _cmd._timeLbl.ActualWidth;
            if (width < _cmd._docSliders.MinWidth) //wrapped to two lines
            {
                _cmd._docSliders.Width = _cmd._stackButtons.ActualWidth; // windowWidth - _cmd._timeLbl.ActualWidth - 4;
                if (windowWidth < _cmd._stackButtons.ActualWidth)
                    _cmd._scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else //one line
            {
                _cmd._docSliders.Width = width - 4;
            }

            Replay.UpdateTicks();
        }

        private WaitForEventImpl _waitMediaOpenedEvent = new WaitForEventImpl();

        public async Task WaitForMediaOpened()
        {
            await _waitMediaOpenedEvent.WaitForEvent();
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
                UpdateReplay(false, 0, 0);
            }

            public void UpdateReplay(bool on, double a, double b)
            {
                VM.Settings.ReplayIsOn = on;
                VM.Settings.ReplayPosA = a;
                VM.Settings.ReplayPosB = b;

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

            //set ticks positions and color if active
            public void UpdateTicks()
            {
                VM._cmd._lineA.X1 = VM._cmd._lineA.X2 = XfromTime(VM.Settings.ReplayPosA);
                VM._cmd._lineB.X1 = VM._cmd._lineB.X2 = XfromTime(VM.Settings.ReplayPosB);

                VM._cmd._lineA.Stroke = IsReplayChecked ? Brushes.Lime : Brushes.AliceBlue;
                VM._cmd._lineB.Stroke = IsReplayChecked ? Brushes.Lime : Brushes.AliceBlue;
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
                if (IsReplayChecked && VM._cmd._position.Value > VM.Settings.ReplayPosB)
                    ReplaySetStart();
            }
        }
    }
}
