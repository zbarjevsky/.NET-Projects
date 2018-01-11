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
        private SoundPlayer m_SoundPlayer;
		private Options m_Options;

        public ReikiProgressBar()
        {
            InitializeComponent();
			
			chkBell3min.IsChecked = true;
			chkBellOnOff.IsChecked = true;

            m_Timer.Enabled = false;
            m_Timer.Elapsed += m_Timer_Elapsed;
        }

        public SoundPlayer SoundPlayer
        {
            get
            {
                if (m_SoundPlayer == null)
                {
                    //string assName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
                    string uri = string.Format(@"pack://application:,,,/{0}", @"Sounds/onhold.wav");
                    var info = Application.GetResourceStream(new Uri(uri));
                    if (info != null)
                    {
                        m_SoundPlayer = new SoundPlayer(info.Stream);
                    }
                }
                return m_SoundPlayer;
            }
        }

		void m_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if (!m_bPaused) m_ElapsedTime += (now - m_LastTime);
            m_LastTime = now;

			Value = m_ElapsedTime.TotalSeconds % Max;

			if (m_Options.BellAtTheEnd && Max - Value < 2 && !m_bSoundPlayed)
            {
                m_bSoundPlayed = true;
                SoundPlayer.Play();
            }
			if (Max - Value > 5) m_bSoundPlayed = false;
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { _value = value;  OnPropertyChanged("Value"); }
        }

		public double Max
		{
			get 
			{ 
				if (m_Options == null) 
					return 180; 
				return m_Options.ProgressInterval; 
			}
			set 
			{ 
				m_Options.ProgressInterval = value; 
				OnPropertyChanged("Max"); 
			}
		}

		public Options Options
		{
			set 
			{ 
				m_Options = value;
				chkBellOnOff.IsChecked = m_Options.BellAtTheEnd;
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
            SoundPlayer.Play();
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


        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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
			m_Options.BellAtTheEnd = menu.IsChecked = !menu.IsChecked;
		}

		private void OnBellIntervalClicked(object sender, RoutedEventArgs e)
		{
			SetInterval(e.Source as MenuItem);
		}

		private void SetInterval(MenuItem mnuClicked)
		{
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
			if (m_Options.ProgressInterval > 0 && m_Options.ProgressInterval < 121)
				SetInterval(chkBell2min);
			else if (m_Options.ProgressInterval > 120 && m_Options.ProgressInterval < 181)
				SetInterval(chkBell3min);
			else if (m_Options.ProgressInterval > 180 && m_Options.ProgressInterval < 241)
				SetInterval(chkBell4min);
			else if (m_Options.ProgressInterval > 240)
				SetInterval(chkBell5min);
			else //default
				SetInterval(chkBell3min);
		}
	}
}
