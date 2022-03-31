using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MkZ.Windows;

namespace MkZ.WPF
{
    /// <summary>
    /// Interaction logic for ReiKiZoomableProgress.xaml
    /// </summary>
    public partial class ReiKiZoomableProgress : UserControl
    {
        public class ReiKiConfig : NotifyPropertyChangedImpl
        {
            public bool IsValid()
            {
                return true;
            }

            [Browsable(false)]
            [DefaultValue(180.0)]
            public double ProgressInterval { get; set; } = 180.0; //seconds = 3 min

            [Browsable(false)]
            public bool BellAtTheEnd { get; set; } = false;


            private BoundsSettings _bounds = new BoundsSettings();
            [Category("ReiKi")]
            public BoundsSettings BoundsSettings
            {
                get { return _bounds; }
                set { SetProperty(ref _bounds, value); }
            }
        }

        private DispatcherTimer _Timer;
        private DateTime _LastTime = DateTime.Now;
        private TimeSpan _ElapsedTime = TimeSpan.FromSeconds(0);
        private bool _bPaused = false;
        private NETSoundPlayer m_SoundPlayer = new NETSoundPlayer();
        private string m_sExePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private ReiKiConfig Config => DataContext as ReiKiConfig;

        public ScrollDragZoomControl Zoomable { get; private set; }

        //use as ScrollDragZoomControl z = ReiKi;
        public static implicit operator ScrollDragZoomControl(ReiKiZoomableProgress reiKi) => reiKi.Zoomable;

        /// <summary>
        /// Set MediaPlayer to get pause/play and timings
        /// </summary>
        public IMediaPlayer MediaPlayer
        {
            get { return (IMediaPlayer)GetValue(MediaPlayerProperty); }
            set { SetValue(MediaPlayerProperty, value); }
        }

        public static readonly DependencyProperty MediaPlayerProperty =
            DependencyProperty.Register(nameof(MediaPlayer), typeof(IMediaPlayer), typeof(ReiKiZoomableProgress), 
                new UIPropertyMetadata(null, OnMediaPlayerChanged));

        private static void OnMediaPlayerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ReiKiZoomableProgress This)
                This.Initialize();
        }

        public ReiKiZoomableProgress()
        {
            DataContext = new ReiKiConfig();

            InitializeComponent();

            Zoomable = new ScrollDragZoomControl(this, null, true);

            _progress.ProgressTheme = MkZ.WPF.GradientProgressBar.TicksTheme.GetBase60Theme();

            chkBell3min.IsChecked = true;
            mnuBellOnOff.IsChecked = true;
            _progress.IsChecked = true;
            _progress.OnCheckClicked = (isChecked) =>
            {
                Config.BellAtTheEnd = isChecked;
                OnBellOnOffClicked(false);
            };

            _Timer = new DispatcherTimer();
            _Timer.Interval = TimeSpan.FromSeconds(0.3);
            _Timer.Tick += Timer_Tick;

            this.DataContextChanged += ReiKiZoomableProgress_DataContextChanged;
        }

        private void _control_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        public void Initialize()
        {
            if (Config == null)
                return;

            mnuBellOnOff.IsChecked = Config.BellAtTheEnd;
            _progress.IsChecked = Config.BellAtTheEnd;
            _progress.Value = 0;
            InitInterval();

            if (MediaPlayer != null)
            {
                MediaPlayer.PropertyChanged -= MediaPlayer_PropertyChanged;
                MediaPlayer.PropertyChanged += MediaPlayer_PropertyChanged;
                MediaPlayer.MediaStartedAction = (mp) => { };
            }

            string sDingFileName = System.IO.Path.Combine(m_sExePath, "Sounds", "ding.mp3");
            m_SoundPlayer.Open(sDingFileName, "ding");
        }

        private void ReiKiZoomableProgress_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Initialize();
        }

        private void MediaPlayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IMediaPlayer.MediaState))
            {
                if(MediaPlayer.MediaState == MediaState.Play)
                {
                    _bPaused = false;
                }
                else
                {
                    _bPaused = true;
                }
            }
        }

        private void _control_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                bool bPaused = true;
                if(MediaPlayer != null)
                    bPaused = MediaPlayer.MediaState != MediaState.Play;
                Start(bPaused);
            }
            else
            {
                Stop();
            }
        }

        public void Start(bool bPaused)
        {
            _LastTime = DateTime.Now;
            _ElapsedTime = TimeSpan.FromSeconds(0);
            _bPaused = bPaused;

            _progress.Value = 0;

            if(!_Timer.IsEnabled)
                _Timer.Start();
        }

        public void Pause()
        {
            _bPaused = true;
            //PlayDing();
        }

        public void Resume()
        {
            _bPaused = false;
        }

        public void Stop()
        {
            if(_Timer.IsEnabled)
                _Timer.Stop();

            _bPaused = false;
            _progress.Value = 0;
        }

        private bool _wasSoundPlayed = false;
        private void Timer_Tick(object sender, EventArgs e)
        {
            _Timer.Stop();

            DateTime now = DateTime.Now;
            if (!_bPaused)
                _ElapsedTime += (now - _LastTime);
            _LastTime = now;

            if (_progress.Maximum > 10)
            {
                _progress.Value = _ElapsedTime.TotalSeconds % _progress.Maximum;

                if (Config.BellAtTheEnd && _progress.Maximum - _progress.Value < 2 && !_wasSoundPlayed)
                {
                    _wasSoundPlayed = true;
                    PlayDing();
                }

                if (_progress.Maximum - _progress.Value > 5)
                    _wasSoundPlayed = false;
            }
            else
            {
                _progress.Value = 0;
            }

            UpdateTooltip(_progress.Maximum - _progress.Value);

            _Timer.Start();
        }

        public void PlayDing()
        {
            m_SoundPlayer.SetVolume((int)(MediaPlayer.Volume * 1000.0));
            m_SoundPlayer.CmdPlay();
        }

        private void UpdateTooltip(double secondsLeft)
        {
            //this.Dispatcher.BeginInvoke((Action)(() =>
            //{
                const string FMT = @"m\:ss"; // @"hh\:mm\:ss"

                string interval = TimeSpan.FromSeconds(Config.ProgressInterval).ToString(FMT);
                string remaining = TimeSpan.FromSeconds(secondsLeft).ToString(FMT);
                string value = TimeSpan.FromSeconds(_progress.Value).ToString(FMT);


                if (_progress.Maximum > 10)
                {
                    this.ToolTip = string.Format("Bell: {0}, Interval: {1:0} Time Left: {2:0}",
                        Config.BellAtTheEnd ? "On" : "Off", interval, remaining);
                    _txt.Text = string.Format("{0} {1} {2}", value, interval, remaining);
                }
                else
                {
                    this.ToolTip = "None";
                }
            //}));
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            Start(bPaused: false);
        }

        private void OnStop(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void OnBellOnOffClicked(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
                OnBellOnOffClicked(true);
        }

        private void OnBellOnOffClicked(bool fromMenu)
        {
            if (fromMenu)
            {
                mnuBellOnOff.IsChecked = !mnuBellOnOff.IsChecked;
                if (Config != null)
                    Config.BellAtTheEnd = mnuBellOnOff.IsChecked;

                _progress.IsChecked = mnuBellOnOff.IsChecked;
            }
            else //from checkBox
            {
                if (Config != null)
                    Config.BellAtTheEnd = _progress.IsChecked;

                mnuBellOnOff.IsChecked = _progress.IsChecked;
            }
        }

        private void OnBellIntervalClicked(object sender, RoutedEventArgs e)
        {
            SetInterval(e.Source as MenuItem);
        }

        private void SetInterval(MenuItem mnuClicked)
        {
            SetInterval(mnuClicked, chkNoProgress, 1);
            SetInterval(mnuClicked, chkBell1min, 60);
            SetInterval(mnuClicked, chkBell2min, 120);
            SetInterval(mnuClicked, chkBell3min, 180);
            SetInterval(mnuClicked, chkBell4min, 240);
            SetInterval(mnuClicked, chkBell5min, 300);
            SetInterval(mnuClicked, chkBell10min, 600);
        }

        private void SetInterval(MenuItem mnuClicked, MenuItem mnuInterval, int iTimeout)
        {
            if (mnuClicked.Name == mnuInterval.Name)
            {
                mnuInterval.IsChecked = true;
                _progress.Maximum = iTimeout;
                Config.ProgressInterval = iTimeout;
                _txt.Text = string.Format("{0} min", iTimeout/60);
            }
            else
            {
                mnuInterval.IsChecked = false;
            }
        }

        private void InitInterval()
        {
            if (Config.ProgressInterval < 31)
                SetInterval(chkNoProgress);
            else if (Config.ProgressInterval > 30 && Config.ProgressInterval < 61)
                SetInterval(chkBell1min);
            else if (Config.ProgressInterval > 60 && Config.ProgressInterval < 121)
                SetInterval(chkBell2min);
            else if (Config.ProgressInterval > 120 && Config.ProgressInterval < 181)
                SetInterval(chkBell3min);
            else if (Config.ProgressInterval > 180 && Config.ProgressInterval < 241)
                SetInterval(chkBell4min);
            else if (Config.ProgressInterval > 240)
                SetInterval(chkBell5min);
            else //default
                SetInterval(chkBell3min);
        }

        private void ReiKi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Start(bPaused: false);
        }

        private void OnMenuHide(object sender, RoutedEventArgs e)
        {
            Config.BoundsSettings.IsVisible = false;
        }
    }
}
