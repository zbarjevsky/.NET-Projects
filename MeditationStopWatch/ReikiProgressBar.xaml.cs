using System;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.ComponentModel;
using System.Windows.Resources;
using System.IO;
using MeditationStopWatch;
using System.Runtime.CompilerServices;
using System.Windows.Shapes;

namespace Wizard
{
    /// <summary>
    /// Interaction logic for ReikiProgressBar.xaml
    /// </summary>
    public partial class ReikiProgressBar : UserControl, INotifyPropertyChanged
    {
        private Timer m_Timer = new Timer(300);
        private DateTime m_LastTime = DateTime.Now;
        private TimeSpan m_ElapsedTime = TimeSpan.FromSeconds(0);
        private bool m_bPaused = false;
        private bool m_bSoundPlayed;
        //private SoundPlayer m_SoundPlayer;
        private MCIPLayer m_SoundPlayer = new MCIPLayer();
        private string m_sExePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private Options _options;

        public ReikiProgressBar()
        {
            InitializeComponent();
			
			chkBell3min.IsChecked = true;
			chkBellOnOff.IsChecked = true;

            m_Timer.Enabled = false;
            m_Timer.Elapsed += m_Timer_Elapsed;
        }

        public void PlayDing()
        {
            string sName = System.IO.Path.Combine(m_sExePath, "Sounds", "ding.mp3");
            m_SoundPlayer.Play(sName, "ding");
            m_SoundPlayer.SetVolume(_options.Volume);
        }

        private void UpdateTooltip(double secondsLeft)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                const string FMT = @"m\:ss"; // @"hh\:mm\:ss"

                string interval = TimeSpan.FromSeconds(_options.ProgressInterval).ToString(FMT);
                string value = TimeSpan.FromSeconds(secondsLeft).ToString(FMT);

                if (Max > 10)
                {
                    this.ToolTip = string.Format("Bell: {0}, Interval: {1:0} Time Left: {2:0}",
                        _options.BellAtTheEnd ? "On" : "Off", interval, value);
                }
                else
                {
                    this.ToolTip = "None";
                }
            }));
        }

		private void m_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_Timer.Stop();

            DateTime now = DateTime.Now;
            if (!m_bPaused)
                m_ElapsedTime += (now - m_LastTime);
            m_LastTime = now;

            if (Max > 10)
            {
                Value = m_ElapsedTime.TotalSeconds % Max;

                if (_options.BellAtTheEnd && Max - Value < 2 && !m_bSoundPlayed)
                {
                    m_bSoundPlayed = true;
                    PlayDing();
                }

                if (Max - Value > 5)
                    m_bSoundPlayed = false;
            }
            else
            {
                Value = 0;
            }

            UpdateTooltip(Max - Value);

            m_Timer.Start();
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { _value = value;  OnPropertyChanged(); }
        }

		public double Max
		{
			get 
			{ 
				if (_options == null) 
					return 180; 
				return _options.ProgressInterval; 
			}
			set 
			{ 
				_options.ProgressInterval = value;
                DrawTicks();

                OnPropertyChanged(); 
			}
		}

		public Options Options
		{
			set 
			{ 
				_options = value;
				chkBellOnOff.IsChecked = _options.BellAtTheEnd;
				InitInterval();
				OnPropertyChanged("Max"); 
			}
		}
        
        public void Start()
        {
            m_LastTime = DateTime.Now;
            m_ElapsedTime = TimeSpan.FromSeconds(0);
            m_bPaused = false;

            Value = 0;
            m_Timer.Enabled = true;
        }

        public void Pause()
        {
            m_bPaused = true;
            PlayDing();
        }

        public void Resume()
        {
            m_bPaused = false;
        }
        
        public void Stop()
        {
            m_Timer.Enabled = false;
            m_bPaused = false;
            Value = 0;
        }

        #region INotifyPropertyChanged Members


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void OnStart(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void OnStop(object sender, RoutedEventArgs e)
        {
            Stop();
        }

		private void OnBellOnOffClicked(object sender, RoutedEventArgs e)
		{
			var menu = e.Source as MenuItem;
			_options.BellAtTheEnd = menu.IsChecked = !menu.IsChecked;
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
		}

		private void SetInterval(MenuItem mnuClicked, MenuItem mnuInterval, int iTimeout)
		{
			if (mnuClicked.Name == mnuInterval.Name)
			{
				mnuInterval.IsChecked = true;
				Max = iTimeout;
			}
			else
			{
				mnuInterval.IsChecked = false;
			}
		}

		private void InitInterval()
		{
            if (_options.ProgressInterval < 31)
                SetInterval(chkNoProgress);
            else if (_options.ProgressInterval > 30 && _options.ProgressInterval < 61)
                SetInterval(chkBell1min);
            else if (_options.ProgressInterval > 60 && _options.ProgressInterval < 121)
                SetInterval(chkBell2min);
            else if (_options.ProgressInterval > 120 && _options.ProgressInterval < 181)
				SetInterval(chkBell3min);
			else if (_options.ProgressInterval > 180 && _options.ProgressInterval < 241)
				SetInterval(chkBell4min);
			else if (_options.ProgressInterval > 240)
				SetInterval(chkBell5min);
			else //default
				SetInterval(chkBell3min);
		}

        private void DrawTicks()
        {
            _canvas.Children.Clear();
            double line_count = (Max / 30.0); //line per 30 sec
            double line_offset = (_canvas.ActualWidth-3) / line_count;
            double smallDelta = _canvas.ActualHeight / 10;
            double bigDelta = _canvas.ActualHeight / 4;

            for (int i = 0; i < line_count + 1; i++)
            {
                bool odd = i % 2 == 1;
                double thickness = odd ? 1 : 2;
                double delta = odd ? bigDelta : smallDelta;

                Line line = new Line();
                line.Stroke = System.Windows.Media.Brushes.Green;
                line.StrokeThickness = thickness;
                line.X1 = 1 + i * line_offset;
                line.X2 = line.X1;
                line.Y1 = delta;
                line.Y2 = _canvas.ActualHeight - delta;

                _canvas.Children.Add(line);
            }
        }

        private void ReiKi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Start();
        }

        private void ReiKi_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawTicks();
        }
    }
}
