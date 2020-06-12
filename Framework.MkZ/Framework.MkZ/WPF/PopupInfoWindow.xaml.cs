using MZ.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MZ.WPF
{
    /// <summary>
    /// Interaction logic for PopupInfoWindow.xaml
    /// </summary>
    public partial class PopupInfoWindow : Window
    {
        private DispatcherTimer m_Timer = new DispatcherTimer(DispatcherPriority.Background);
        private long _timer_count = 0;

        public TimeSpan CloseTimeOut { get; set; } = TimeSpan.FromSeconds(4);

        public string InfoText
        {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }

        public static readonly DependencyProperty InfoTextProperty =
            DependencyProperty.Register("InfoText", typeof(string), typeof(PopupInfoWindow), new PropertyMetadata(""));

        public PopupInfoWindow() 
        {
            InitializeComponent(WindowStartupLocation.CenterScreen, new Point());
        }

        public PopupInfoWindow(WindowStartupLocation startupLocation, Point location, Window owner)
        {
            this.Owner = owner;
            InitializeComponent(startupLocation, location);
        }

        //for WinForms
        public PopupInfoWindow(WindowStartupLocation startupLocation, Point location, IntPtr ownerHandle)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            helper.Owner = ownerHandle;
            InitializeComponent(startupLocation, location);
        }

        private void InitializeComponent(WindowStartupLocation startupLocation, Point location)
        {
            InitializeComponent();

            this.WindowStartupLocation = startupLocation;

            this.Draggable(true);

            this.Left = location.X;
            this.Top = location.Y;

            m_Timer.Interval = TimeSpan.FromMilliseconds(100);
            m_Timer.Tick += Timer_Tick;

            m_Timer.Start();
            _timer_count = 0;

            _progress.ProgressTheme = GradientProgressBar.TicksTheme.GetBase100Theme();
            _progress.Maximum = 100;
            _progress.TickColor = Brushes.Navy;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double maxCount = CloseTimeOut.TotalSeconds / m_Timer.Interval.TotalSeconds;

            m_Timer.Stop();
            if (_timer_count++ > maxCount)
            {
                this.Close();
                return; //do not start timer
            }
            m_Timer.Start();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            m_Timer.Stop();
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            _timer_count = 0;
            m_Timer.Start();
        }
    }
}
