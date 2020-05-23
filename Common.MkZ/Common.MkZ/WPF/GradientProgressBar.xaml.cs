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

using System.Runtime.CompilerServices;
using System.Windows.Shapes;

namespace MZ.WPF
{
    /// <summary>
    /// Interaction logic for ReikiProgressBar.xaml
    /// </summary>
    public partial class GradientProgressBar : UserControl, INotifyPropertyChanged
    {
        private Timer m_Timer = new Timer(300);
        private DateTime m_LastTime = DateTime.Now;
        private TimeSpan m_ElapsedTime = TimeSpan.FromSeconds(0);
        private bool m_bPaused = false;

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); DrawTicks(); OnPropertyChanged(); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(GradientProgressBar), new PropertyMetadata(default(double)));

        public GradientProgressBar()
        {
            InitializeComponent();

            m_Timer.Enabled = false;
            m_Timer.Elapsed += m_Timer_Elapsed;

            DrawTicks();
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
            }
            else
            {
                Value = 0;
            }

            UpdateTooltip(Max - Value);

            m_Timer.Start();
        }

        private void UpdateTooltip(double secondsLeft)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                const string FMT = @"m\:ss"; // @"hh\:mm\:ss"

                string interval = "3 min"; // TimeSpan.FromSeconds(Settings.ProgressInterval).ToString(FMT);
                string value = TimeSpan.FromSeconds(secondsLeft).ToString(FMT);

                if (Max > 10)
                {
                    this.ToolTip = string.Format("Bell: {0}, Interval: {1:0} Time Left: {2:0}",
                        chk.IsChecked == true ? "On" : "Off", interval, value);
                }
                else
                {
                    this.ToolTip = "None";
                }
            }));
        }

        //private double _value = 30;
        //public double Value
        //{
        //    get { return _value; }
        //    set { _value = value;  OnPropertyChanged(); }
        //}

        private double _max = 180;
		public double Max
		{
			get 
			{ 
				return _max; 
			}
			set 
			{
                _max = value;
                DrawTicks();
                OnPropertyChanged(); 
			}
		}

        #region INotifyPropertyChanged Members


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void DrawTicks()
        {
            _canvas.Children.Clear();
            double line_count = Max;  //(Max / 30.0); //line per 30 sec
            double line_offset = (_canvas.ActualWidth-3) / line_count;
            double smallDelta = _canvas.ActualHeight / 10;
            double bigDelta = _canvas.ActualHeight / 4;

            for (int i = 0; i < line_count + 1; i++)
            {
                double thickness = -1;
                double delta = bigDelta;

                if (i % 10 == 0)
                {
                    thickness = 0.5;
                    delta = bigDelta;
                }
                if (i % 30 == 0)
                {
                    thickness = 2;
                    delta = bigDelta;
                }
                if (i % 60 == 0)
                {
                    thickness = 2;
                    delta = smallDelta;
                }
                if (thickness < 0)
                    continue;

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

        private void Progress_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Progress_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawTicks();
        }

        private void chk_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
