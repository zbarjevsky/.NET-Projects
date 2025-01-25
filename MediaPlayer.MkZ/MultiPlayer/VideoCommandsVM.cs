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
            Settings.Volume = volume;

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
                Open(ofd.FileName);
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

            _player.LeftButtonClick = () => { TogglePlayPauseCommand.Execute(null); };
            _player.LeftButtonDoubleClick = () => { Maximize_Click(this, null); };
            _player.VideoEnded = (player) => { MediaPlayEnded(player); };
        }

        bool _isInUpdate = false;
        public void Update(OnePlayerSettings s, bool pop = false)
        {
            IsPopWindowMode = pop;

            _isInUpdate = true;

            _cmd._volume.Value = s.Volume * 1000.0;
            _cmd._position.Maximum = s.Duration;
            _cmd._position.Value = s.Position;

            _cmd._speed.SelectedIndex = SpeedRatio(s.SpeedRatio);

            _cmd._fit.SelectedIndex = (int)s.ZoomState;
            if (s.ZoomState == eZoomState.Custom)
                _player.Zoom = s.Zoom;

            _cmd._timeLbl.Text = SecondsToString(s.Position);

            NotifyPropertyChanged(nameof(SelectedFitIndex));

            Replay.UpdateReplay(s.ReplayEndTime, s.ReplayDuration);

            AdjustMarginsForVisibleScrollBars();
            AdjustSizeAndLayout();

            _isInUpdate = false;
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

        public void Open(string fileName)
        {
            Settings.FileName = fileName;

            _player.Open(Settings.FileName, Settings.Volume);
            _cmd._position.Maximum = _player.NaturalDuration;
            _cmd._position.Value = 0;
            Replay.IsReplayChecked = false;
            Play();

            CommandManager.InvalidateRequerySuggested();
        }

        private void MediaPlayEnded(IVideoPlayer player)
        {
            Stop();
            switch (Settings.PlayMode)
            {
                case ePlayMode.PlayOne:
                    break;
                case ePlayMode.PlayAll:
                    PlayNext(random: false, loop: false);
                    break;
                case ePlayMode.RepeatOne:
                    Play();
                    break;
                case ePlayMode.RepeatAll:
                    PlayNext(random: false, loop: true);
                    break;
                case ePlayMode.Random:
                    PlayNext(random: true, loop: true);
                    break;
                default:
                    Play();
                    break;
            }
        }

        public void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player != null && _cmd != null)
            {
                 Settings.Volume = _cmd._volume.Value / 1000.0;
                _player.Volume = Settings.Volume;
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
            Open(fileNames[idx]);
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
                Open(fileNames[idx]);
        }

        public List<string> GetFileNames(string fileName, out int idx)
        {
            idx = -1;
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
                return new List<string>();

            string dir = System.IO.Path.GetDirectoryName(fileName);

            List<string> fileNames = System.IO.Directory.EnumerateFiles(dir).ToList();
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
                _cmd._docSliders.Width = windowWidth - _cmd._timeLbl.ActualWidth - 4;
                if (windowWidth < _cmd._stackButtons.ActualWidth)
                    _cmd._scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else //one line
            {
                _cmd._docSliders.Width = width - 4;
            }
        }

        public ReplayLoop Replay { get; }

        public class ReplayLoop : NotifyPropertyChangedImpl
        {
            VideoCommandsVM VM { get; }

            private bool _isReplayChecked = false;
            public bool IsReplayChecked
            {
                get { return _isReplayChecked; }
                set { SetProperty(ref _isReplayChecked, value); }
            }

            private int _replayDurationIndex = 4;
            public int ReplayDurationIndex //for ComboBox
            {
                get { return _replayDurationIndex; }
                set 
                { 
                    SetProperty(ref _replayDurationIndex, value);
                    VM.Settings.ReplayDuration = GetReplayDuration(_replayDurationIndex);
                    NotifyPropertyChanged(nameof(ReplayToolTip)); }
            }

            public string ReplayToolTip
            {
                get
                {
                    return $"Replay {SecondsToString(VM.Settings.ReplayDuration)}, End: {SecondsToString(VM.Settings.ReplayEndTime)}";
                }
            }

            private double ReplayEndPosition 
            {  
                get => VM.Settings.ReplayEndTime;
                set
                {
                    VM.Settings.ReplayEndTime = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(ReplayToolTip));
                }
            }

            public ReplayLoop(VideoCommandsVM vm)
            {
                VM = vm;
                UpdateReplay(VM.Settings.ReplayEndTime, VM.Settings.ReplayDuration);
            }

            public void UpdateReplay(double end, double duration)
            {
                ReplayEndPosition = end;
                ReplayDurationIndex = GetReplayDurationIndex(duration);
            }

            public void ReplaySet(bool? isChecked)
            {
                if (isChecked == true)
                {
                    UpdateReplay(VM.Settings.Position, GetReplayDuration(ReplayDurationIndex));
                }
            }

            static double[] durations = { 1.0, 3.0, 5.0, 7.0, 10.0, 15.0, 30.0, 60.0 };
            private static double GetReplayDuration(int idx)
            {
                return durations[idx];
            }

            private static int GetReplayDurationIndex(double duration)
            {
                for (int i = 0; i < durations.Length; i++)
                {
                    if (durations[i] == duration)
                        return i;
                }
                return 4;
            }

            private void ReplaySetStart()
            {
                double replayDuration = GetReplayDuration(ReplayDurationIndex);
                VM._cmd._position.Value = ReplayEndPosition - replayDuration;
                VM.Settings.Position = VM._cmd._position.Value;
            }

            public void ReplayCheckAndUpdate()
            {
                if (IsReplayChecked && VM._cmd._position.Value > ReplayEndPosition)
                    ReplaySetStart();
            }
        }
    }
}
