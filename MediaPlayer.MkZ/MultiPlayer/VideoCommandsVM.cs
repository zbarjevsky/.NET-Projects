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
using MultiPlayer.Properties;
using System.Runtime;


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

        public bool IsFavorite 
        { 
            get => Settings.IsFavorite; 
            set 
            {  
                Settings.IsFavorite = value;
                MainWindow.UpdateRecentFile(Settings.FileName, Settings.IsFavorite);
                NotifyPropertyChanged(); 
            } 
        }

        public OnePlayerSettings Settings { get; set; } = new OnePlayerSettings();

        public bool IsPopWindowMode { get; private set; } = false;

        public RelayCommand TogglePlayPauseCommand { get; }
        public RelayCommand OpenFileCommand { get; }
        public RelayCommand OpenFileByNameCommand { get; }
        public RelayCommand PrevFileCommand { get; }
        public RelayCommand NextFileCommand { get; }
        public RelayCommand SkipOneFrameCommand { get; }
        public RelayCommand SkipXSecondsCommand { get; }
        public RelayCommand BookmarkSetCommand { get; }
        public RelayCommand BookmarkGoToCommand { get; }
        public RelayCommand BookmarkClearCommand { get; }

        public VideoCommandsVM()
        {
            Replay = new ReplayLoop(this);

            TogglePlayPauseCommand = new RelayCommand(TogglePlayPauseCommandExecute, TogglePlayPauseCommandCanExecute);
            OpenFileCommand = new RelayCommand(OpenFileCommandExecute);
            OpenFileByNameCommand = new RelayCommand(OpenFileByIndexCommandExecute);
            PrevFileCommand = new RelayCommand(PrevFileCommandExecute, PrevFileCommandCanExecute);
            NextFileCommand = new RelayCommand(NextFileCommandExecute, NextFileCommandCanExecute);
            SkipXSecondsCommand = new RelayCommand(SkipXSecondsCommandExecute, (o) => true);
            SkipOneFrameCommand = new RelayCommand(SkipOneFrameCommandExecute, (o) => true);
            BookmarkSetCommand = new RelayCommand(BookmarkSetCommandExecute, (o) =>  Settings.Duration > 1.0);
            BookmarkGoToCommand = new RelayCommand(BookmarkGoToCommandExecute, BookmarkGoToCommandCanExecute);
            BookmarkClearCommand = new RelayCommand(BookmarkClearCommandExecute, BookmarkClearCommandCanExecute);
        }

        private string _title = "File Name";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _fileIndex = "0/0";
        public string FileIndex
        {
            get { return _fileIndex; }
            set { SetProperty(ref _fileIndex, value); }
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

        public void Pause(bool updateUI)
        {
            _player.Pause(updateUI);
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
            UpdateRrecentFile(Settings);

            _player.Stop();
            Settings.FileName = "";
            _player.VideoPlayerElement.Source = null;
            _player.SetBackColor(bActive: false);

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
            FileIndex = "0/0";
            TogglePlayPauseCommand.RefreshBoundControls();
        }

        private void OpenFileCommandExecute(object obj)
        {
            string fileName = Settings.FileName;
            string dir = System.IO.Path.GetDirectoryName(Settings.FileName);
            string dirUP = string.Empty;
            if (obj is string parameter && parameter == "(..)")
                dirUP = System.IO.Path.GetDirectoryName(dir); //go one UP

            OpenFileDialog ofd = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(dirUP) && Directory.Exists(dir))
            {
                ofd.InitialDirectory = dir;
                ofd.FileName = fileName;
            }
            else if (Directory.Exists(dirUP)) //open dirUP
            {
                ofd.InitialDirectory = dirUP;
                ofd.FileName = System.IO.Path.GetFileName(dir);
            }

            MediaState state = _player.MediaState;
            if (state == MediaState.Play)
                Pause(updateUI: false);

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

        private void OpenFileByIndexCommandExecute(object parameter)
        {
            if (parameter is string fileName)
            {
                _ = OpenFromFile(fileName, startFrom0: false);
            }
        }

        private void TogglePlayPauseCommandExecute(object parameter)
        {
            if (_player.MediaState == MediaState.Play)
                Pause(updateUI: true);
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
            _player.VideoEnded = (player) => MediaPlayEndedAsync(player); 
            _player.VideoFailed = (e, player) => MediaPlayFailed(e, player);
        }

        bool _isInUpdate = false;
        public void Update(OnePlayerSettings s, bool pop = false, bool lockUpdate = true)
        {
            if (_isInUpdate)
                return;

            IsPopWindowMode = pop;

            if (lockUpdate)
                _isInUpdate = true;

            Settings.Update(s);
            NotifyPropertyChanged(nameof(Settings));

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

            Settings.UpdateBookmarks(s);

            AdjustMarginsForVisibleScrollBars();
            AdjustSizeAndLayout();

            _isInUpdate = false;
        }

        public void UpdateRrecentFile(OnePlayerSettings s)
        {
            if (string.IsNullOrWhiteSpace(s.FileName))
                return;

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
            if (IsLoading)
                return;

            if (string.IsNullOrWhiteSpace(fileName))
                return;

            if (fileName.StartsWith("file:///"))
                fileName = fileName.Substring("file:///".Length).Replace('/', '\\');

            if (!File.Exists(fileName))
                return;

            UpdateRrecentFile(Settings); //update previous recent file

            _player.Clear(); //reset all settings, retain volume, fit, playMode

            Settings.FileName = fileName;

            _recentFile = MainWindow.FindOrCreateRecentFile(fileName);

            Settings.IsFavorite = _recentFile.IsFavorite;
            Settings.IsMoreBookmarksOpen = _recentFile.IsMoreBookmarksOpen;

            Settings.UpdateBookmarks(_recentFile.ReplayIsOn, _recentFile);

            Settings.Position = startFrom0 ? 0 : _recentFile.Position;

            Settings.MediaState = MediaState.Play;

            await OpenFromSettings(new OnePlayerSettings(Settings), IsPopWindowMode);

            //CommandManager.InvalidateRequerySuggested();
        }

        public async Task OpenFromSettings(OnePlayerSettings s, bool pop = false)
        {
            IsPopWindowMode = pop;

            if (IsLoading)
                return;

            Clear();
            if (string.IsNullOrEmpty(s.FileName) || !File.Exists(s.FileName))
            {
                _player.VideoPlayerElement.ForceRender();
                return;
            }

            this.IsLoading = true;

            this.IsMuted = true; //load silently
            _player.Volume = 0;
            
            //Settings.Update(s);

            _player.VideoPlayerElement.Source = new Uri(s.FileName);
            _player.PositionSet(TimeSpan.FromSeconds(s.Position), false);

            this.Title = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(s.FileName)) + "/" + System.IO.Path.GetFileName(s.FileName);
            List<string> fileNames = this.GetFileNames(s.FileName, out int idx);
            this.FileIndex = $"{(idx+1)}/{fileNames.Count} ";

            this.Update(s, pop, lockUpdate: false); //update settings before play()

            this.Play();

            //wait for source opened
            await WaitForNaturalDurationUpdated(s.FileName);

            if (_player.NaturalDuration > 0)
                s.Duration = _player.NaturalDuration;

            if (s.MediaState != MediaState.Play)
            {
                this.Pause(updateUI: true);
            }

            await WaitForMediaOpened();

            this.Update(s, pop, lockUpdate: false);
            this.UpdateRrecentFile(s);

            //_player.Volume = s.Volume;
            this.IsMuted = false; //restore volume

            this.TogglePlayPauseCommand.RefreshBoundControls();
            this.BookmarkGoToCommand.RefreshBoundControls();
            this.BookmarkSetCommand.RefreshBoundControls();
            this.NextFileCommand.RefreshBoundControls();
            this.PrevFileCommand.RefreshBoundControls();

            this.NotifyPropertyChanged(nameof(SelectedPlayModeIndex));
            this.NotifyPropertyChanged(nameof(IsFavorite));

            this.AdjustSizeAndLayout();

            //_player.SetBackColor(bActive: false);
        }

        private async Task WaitForNaturalDurationUpdated(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                //wait for source opened
                int i = 0;
                for (; i < 10; i++)
                {
                    if (_player.NaturalDuration > 0 || !IsLoading)
                        break;

                    _player.VideoPlayerElement.ForceRender();
                    await Task.Delay(333);
                }

                Debug.WriteLine("### Check NaturalDuration, tries {0} - Duration: {1:###,##0} sec, file: {2}", i, _player.NaturalDuration, fileName);
            }
        }

        private void MediaPlayStarted(MediaElement player)
        {
            IsLoading = false;
            TimeSpan delta = _playSartTime.Elapsed;
            Debug.WriteLine($"### MediaPlayStarted - Elapsed: {delta.TotalSeconds.ToString("0.000")}, File: {Settings.FileName}");
            _waitMediaOpenedEvent.TriggerEvent();
        }

        private async Task MediaPlayEndedAsync(MediaElement player)
        {
            IsLoading = false;

            //when song is empty it is ended after next song started - ignore it
            if (string.IsNullOrEmpty(player?.Source?.ToString()))
                return;

            Stop();
            Settings.Position = 0;
            await Task.Delay(100);

            if (MainWindow.Instance.IsGlobalRepeatAllMode)
            {
                Settings.MediaState = MediaState.Pause;
                _ = OpenFromSettings(new OnePlayerSettings(Settings), IsPopWindowMode);
                MainWindow.Instance.PlayNext(_player);
            }
            else
            {
                switch (Settings.PlayMode)
                {
                    case ePlayMode.PlayOne:
                        break;
                    case ePlayMode.PlayAll:
                        PlayNext(random: false, loop: false, startFrom0: true);
                        break;
                    case ePlayMode.RepeatAll:
                        PlayNext(random: false, loop: true, startFrom0: true);
                        break;
                    case ePlayMode.Random:
                        PlayNext(random: true, loop: true, startFrom0: true);
                        break;
                    case ePlayMode.RepeatOne:
                    default:
                        _ = OpenFromFile(Settings.FileName, startFrom0: true);
                        break;
                }
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

            NotifyPropertyChanged(nameof(Volume));
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
                Pause(updateUI: false);
        }

        public void Pos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player == null || _isInUpdate)
                return;

            IsMuted = true;
            bool resume = (_player.MediaState == MediaState.Play);
            if (resume)
                Pause(updateUI: false);

            Settings.Position = e.NewValue;
            _cmd._position.Value = Settings.Position;
            _player.PositionSet(TimeSpan.FromSeconds(Settings.Position), false);

            _cmd._timeLbl.Text = SecondsToString(_player.Position.TotalSeconds);

            if (Replay.IsReplayChecked && (Settings.Position < Settings.ReplayPosA || Settings.Position > Settings.ReplayPosB))
            {
                Replay.IsReplayChecked = false; //uncheck REPLAY if clicked out of replay range
                Replay.UpdateReplayUI();
            }

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
            PlayNext(random: false, loop: true, startFrom0: false);
        }

        private bool PrevFileCommandCanExecute(object obj)
        {
            List<string> fileNames = GetFileNames(Settings.FileName, out int idx);
            return idx > 0;
        }

        private void PrevFileCommandExecute(object obj)
        {
            PlayPrev(Settings.FileName);
        }

        private void PlayPrev(string fileName)
        {
            IsLoading = false; //reset 

            List<string> fileNames = GetFileNames(fileName, out int idx);
            if (fileNames.Count == 0)
                return;

            idx--;
            if (idx < 0)
                idx = fileNames.Count - 1;

            _ = OpenFromFile(fileNames[idx], startFrom0: false);
        }

        private Random _random = new Random();
        private void PlayNext(bool random = false, bool loop = false, bool startFrom0 = false)
        {
            IsLoading = false; //reset 
            Pause(true);

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
                _ = OpenFromFile(fileNames[idx], startFrom0: startFrom0);
        }

        public List<string> GetFileNames(string fileName, out int idx)
        {
            idx = -1;
            if (string.IsNullOrWhiteSpace(fileName))
                return new List<string>();

            string dir = System.IO.Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
                return new List<string>();

            List<string> fileNames = System.IO.Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => MainWindow.IsSupportedFileExtension(file)).ToList();
            fileNames.Sort();
            idx = fileNames.IndexOf(fileName.Replace('/', '\\'));

            return fileNames;
        }

        public void Maximize_Click(object sender, RoutedEventArgs e)
        {
            PopUpVM.MaximizeToggle(hide: false, this);
        }

        public static PopUpWindowVM PopUpVM { get; set; } = new PopUpWindowVM();

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
                SkipXSecondsCommand.Execute(e.Key == System.Windows.Input.Key.Right);
            else if (e.Key == System.Windows.Input.Key.OemComma || e.Key == System.Windows.Input.Key.OemPeriod)
                SkipOneFrameCommand.Execute(e.Key == System.Windows.Input.Key.OemPeriod);
            else if (e.Key == System.Windows.Input.Key.A)
                BookmarkKeyDown(eBookmarkName.A.ToString());
            else if (e.Key == System.Windows.Input.Key.B)
                BookmarkKeyDown(eBookmarkName.B.ToString());
            else if (e.Key == System.Windows.Input.Key.D1 || e.Key == Key.NumPad1)
                BookmarkKeyDown(eBookmarkName.C.ToString());
            else if (e.Key == System.Windows.Input.Key.D2 || e.Key == Key.NumPad2)
                BookmarkKeyDown(eBookmarkName.D.ToString());
            else if (e.Key == System.Windows.Input.Key.D3 || e.Key == Key.NumPad3)
                BookmarkKeyDown(eBookmarkName.E.ToString());
            else if (e.Key == System.Windows.Input.Key.D4 || e.Key == Key.NumPad4)
                BookmarkKeyDown(eBookmarkName.F.ToString());
            else if (e.Key == System.Windows.Input.Key.D5 || e.Key == Key.NumPad5)
                BookmarkKeyDown(eBookmarkName.G.ToString());
            else if (e.Key == System.Windows.Input.Key.D6 || e.Key == Key.NumPad6)
                BookmarkKeyDown(eBookmarkName.H.ToString());
            else if (e.Key == System.Windows.Input.Key.D7 || e.Key == Key.NumPad7)
                BookmarkKeyDown(eBookmarkName.I.ToString());
            else if (e.Key == System.Windows.Input.Key.D8 || e.Key == Key.NumPad8)
                BookmarkKeyDown(eBookmarkName.J.ToString());
            else if (e.Key == System.Windows.Input.Key.D9 || e.Key == Key.NumPad9)
                BookmarkKeyDown(eBookmarkName.K.ToString());
            else
                return false;

            return true;
        }

        private void BookmarkKeyDown(string bookmarkName)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
                BookmarkSetCommandExecute(bookmarkName);
            else
                BookmarkGoToCommandExecute(bookmarkName);
        }

        private void SkipXSecondsCommandExecute(object obj)
        {
            const double X = 5.0;
            if (obj is bool nextFrame)
                SkipPosition(nextFrame ? X : -X);
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

            Replay.UpdateReplayUI();
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

        private void BookmarkSetCommandExecute(object bookMarkName)
        {
            eBookmarkName name = (eBookmarkName)Enum.Parse(typeof(eBookmarkName), (string)bookMarkName);
            Replay.BookmarkSet(name, _player.Duration);
            _player._ctxMenu.IsOpen = false;
            NotifyPropertyChanged(nameof(Settings));
        }

        private void BookmarkGoToCommandExecute(object bookMarkName)
        {
            eBookmarkName name = (eBookmarkName)Enum.Parse(typeof(eBookmarkName), (string)bookMarkName);
            Replay.BookmarkGo2(name);
            _player._ctxMenu.IsOpen = false;
        }

        private bool BookmarkGoToCommandCanExecute(object bookMarkName)
        {
            if (bookMarkName == null)
                return false;

            eBookmarkName name = (eBookmarkName)Enum.Parse(typeof(eBookmarkName), (string)bookMarkName);
            double position = Replay.BookmarkPositionGet(name);
            return position > 0 && this.Settings.Duration > 1.0;
        }

        private void BookmarkClearCommandExecute(object bookMarkName)
        {
            eBookmarkName name = (eBookmarkName)Enum.Parse(typeof(eBookmarkName), (string)bookMarkName);
            Replay.BookmarkPositionClear(name);
        }

        private bool BookmarkClearCommandCanExecute(object bookMarkName)
        {
            if (bookMarkName == null)
                return false;

            eBookmarkName name = (eBookmarkName)Enum.Parse(typeof(eBookmarkName), (string)bookMarkName);
            double position = Replay.BookmarkPositionGet(name);
            return position > 0 && this.Settings.Duration > 1.0;
        }

        public void DeleteFile(bool bPrev)
        {
            string fileName = Settings.FileName;
            if (File.Exists(fileName))
            {
                List<string> fileNames = GetFileNames(fileName, out int idx);
                Clear();
                File.Delete(fileName);

                fileNames = GetFileNames(fileName, out int idxDummy);
                if (fileNames.Count > 0)
                {
                    if (idx < 0) idx = 0;
                    if (idx >= fileNames.Count) idx = fileNames.Count - 1;
                        
                    _ = OpenFromFile(fileNames[idx], startFrom0: false);
                }
            }
            else
            {
                Clear();
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

            public void UpdateReplayUI()
            {
                if (IsReplayChecked && VM.Settings.ReplayPosA > 0 && VM.Settings.ReplayPosB > 0)
                {
                    VM._cmd._lblReplay.Background = Brushes.Chartreuse;
                }
                else
                {
                    VM._cmd._lblReplay.Background = Brushes.Gainsboro;
                }

                UpdateTicks();
                NotifyPropertyChanged(nameof(IsReplayChecked));
                NotifyPropertyChanged(nameof(ReplayToolTip));
            }

            public void ReplayToggle(bool isChecked)
            {
                ReplayCheckAndUpdate();
            }

            public void GoToPosition(double pos)
            {
                if (pos <= 0) 
                    return;

                VM._cmd._position.Value = pos;
                VM.Settings.Position = VM._cmd._position.Value;
            }

            public void ClearD()
            {
                VM.Settings.ReplayPosD = 0;
                UpdateReplayUI();
            }

            //set ticks positions and color if active
            private void UpdateTicks()
            {
                UpdateTick(VM._cmd._lineA, VM.Settings.ReplayPosA, IsReplayChecked ? Brushes.Lime : Brushes.AliceBlue);
                UpdateTick(VM._cmd._lineB, VM.Settings.ReplayPosB, IsReplayChecked ? Brushes.Lime : Brushes.AliceBlue);

                UpdateTick(VM._cmd._lineC, VM.Settings.ReplayPosC, BookmarkColors.BookmarkBrush1);
                UpdateTick(VM._cmd._lineD, VM.Settings.ReplayPosD, BookmarkColors.BookmarkBrush2);
                UpdateTick(VM._cmd._lineE, VM.Settings.ReplayPosE, BookmarkColors.BookmarkBrush3);
                UpdateTick(VM._cmd._lineF, VM.Settings.ReplayPosF, BookmarkColors.BookmarkBrush4);
                UpdateTick(VM._cmd._lineG, VM.Settings.ReplayPosG, BookmarkColors.BookmarkBrush5);
                UpdateTick(VM._cmd._lineH, VM.Settings.ReplayPosH, BookmarkColors.BookmarkBrush6);
                UpdateTick(VM._cmd._lineI, VM.Settings.ReplayPosI, BookmarkColors.BookmarkBrush7);
                UpdateTick(VM._cmd._lineJ, VM.Settings.ReplayPosJ, BookmarkColors.BookmarkBrush8);
                UpdateTick(VM._cmd._lineK, VM.Settings.ReplayPosK, BookmarkColors.BookmarkBrush9);
            }

            private void UpdateTick(Line line, double position, System.Windows.Media.Brush stroke)
            {
                if (position > 0)
                {
                    line.Visibility = Visibility.Visible;

                    double center = line.StrokeThickness / 2;
                    line.X1 = line.X2 = XfromTime(position) - center;
                    line.Stroke = stroke;

                    line.ToolTip = SecondsToString(position); //tooltip is not visible because Canvas is BELOW slider
                }
                else
                {
                    line.Visibility = Visibility.Collapsed;
                }
            }

            private double XfromTime(double time)
            {
                double width = VM._cmd._position.ActualWidth - 11;
                double seconds = VM.Settings.Duration;

                if (seconds <= 0 || time <= 0)
                    return -1000;

                double x = 8 + time * width / seconds;
                
                return x;
            }

            public void ReplayCheckAndUpdate()
            {
                if (IsReplayChecked && (VM._cmd._position.Value < VM.Settings.ReplayPosA || VM._cmd._position.Value > VM.Settings.ReplayPosB))
                        GoToPosition(VM.Settings.ReplayPosA);
                UpdateReplayUI();
            }

            public void BookmarkSet(eBookmarkName name, double duration)
            {
                double delta = VM.Settings.ReplayPosB - VM.Settings.ReplayPosA;
                if (delta < 1.0) delta = 10.0;

                VM.Settings.BookmarkPositionSet(name, VM.Settings.Position);

                if (name == eBookmarkName.A)
                {
                    if (VM.Settings.ReplayPosB < 1.0) //not set yet
                        VM.Settings.ReplayPosB = duration - 1.0; //end of file
                    else if (VM.Settings.ReplayPosB - VM.Settings.ReplayPosA < 1.0)
                        VM.Settings.ReplayPosB = duration - 1.0; //end of file
                }

                if (name == eBookmarkName.B && VM.Settings.ReplayPosB - VM.Settings.ReplayPosA < 1.0)
                    VM.Settings.ReplayPosA = VM.Settings.ReplayPosB - delta;

                UpdateReplayUI();
            }

            public void BookmarkGo2(eBookmarkName name)
            {
                double position = BookmarkPositionGet(name);
                GoToPosition(position);
            }

            public double BookmarkPositionGet(eBookmarkName name)
            {
                return VM.Settings.BookmarkPositionGet(name);
            }

            public void BookmarksClear()
            {
                VM.Settings.BookmarkPositionSet(eBookmarkName.A, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.B, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.C, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.D, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.E, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.F, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.G, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.H, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.I, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.J, 0.0);
                VM.Settings.BookmarkPositionSet(eBookmarkName.K, 0.0);

                UpdateReplayUI();
                VM.BookmarkGoToCommand.RefreshBoundControls();
            }

            public void BookmarkPositionClear(eBookmarkName name)
            {
                VM.Settings.BookmarkPositionSet(name, 0.0);
                
                UpdateReplayUI();
                VM.BookmarkGoToCommand.RefreshBoundControls();
            }
        }
    }
}
