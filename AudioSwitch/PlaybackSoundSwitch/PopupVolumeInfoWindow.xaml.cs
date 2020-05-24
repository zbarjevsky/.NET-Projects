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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PlaybackSoundSwitch.WPF
{
    /// <summary>
    /// Interaction logic for PopupInfoWindow.xaml
    /// </summary>
    public partial class PopupVolumeInfoWindow : Window
    {
        private DispatcherTimer m_Timer = new DispatcherTimer(DispatcherPriority.Background);
        private long _timer_count = 0;

        public TimeSpan CloseTimeOut { get; set; } = TimeSpan.FromSeconds(4);

        public int Volume { get { return (int)_slider.Value; } set { _slider.Value = value; } }

        public Action<int> OnVolumeChangedAction = (volume) => { };

        public void ShowIfVolumeChanged(int volume)
        {
            if (this.Volume != volume) 
            {
                _timer_count = 0;
                this.Volume = volume;
                if (this.Visibility != Visibility.Visible)
                {
                    m_Timer.Start();
                    this.Opacity = 1;
                    this.Visibility = Visibility.Visible; //modeless
                }
            }
        }

        public PopupVolumeInfoWindow() 
        {
            InitializeComponent(WindowStartupLocation.CenterScreen, new Point());
        }

        public PopupVolumeInfoWindow(WindowStartupLocation startupLocation, Point location, Window owner)
        {
            this.Owner = owner;
            InitializeComponent(startupLocation, location);
        }

        //for WinForms
        public PopupVolumeInfoWindow(WindowStartupLocation startupLocation, Point location, IntPtr ownerHandle)
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

            _progress.ProgressTheme = GradientProgressBar.Theme.GetBase100Theme();
            _progress.Maximum = 100;
            _progress.TickColor = Brushes.Navy;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double maxCount = CloseTimeOut.TotalSeconds / m_Timer.Interval.TotalSeconds;

            m_Timer.Stop();
            if (_timer_count++ > maxCount)
            {
                _timer_count = 0;
                //this.Visibility = Visibility.Collapsed;
                VisibilityHideAnimation();
                return; //do not start timer
            }
            m_Timer.Start();
        }

        private void VisibilityHideAnimation()
        {
            var animation = new DoubleAnimation
            {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => { this.Opacity = 0; this.Visibility = Visibility.Collapsed; };

           this.BeginAnimation(UIElement.OpacityProperty, animation);
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

        private void _slider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Debug.WriteLine("Volume: " + (int)_slider.Value);
            OnVolumeChangedAction((int)_slider.Value);
        }
    }
}
