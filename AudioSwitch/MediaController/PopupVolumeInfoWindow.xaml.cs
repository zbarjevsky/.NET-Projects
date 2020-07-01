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

namespace MZ.Media.WPF
{
    public class PopupVolumeInfoHelper
    {
        private PopupVolumeInfoWindow _popupVolumeInfoWindow;
        private System.Windows.Point _location = new System.Windows.Point(100, 100);

        public Action<int> OnVolumeChangedAction = (volume) => { };

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point((int)_location.X, (int)_location.Y); }
            set { _location = new System.Windows.Point(value.X, value.Y); }
        }

        public string InfoText { get; set; } = "No Device (None)";

        public PopupVolumeInfoHelper(System.Drawing.Point location, Action<int> onVolumeChangedAction)
        {
            Location = location;
            OnVolumeChangedAction = onVolumeChangedAction;
        }

        public void ShowPopupVolume(int volume)
        {
            bool bForceShow = false;
            if (_popupVolumeInfoWindow == null)
            {
                bForceShow = true;

                _popupVolumeInfoWindow = new PopupVolumeInfoWindow(_location);
                _popupVolumeInfoWindow.ShowActivated = false;
                _popupVolumeInfoWindow.OnVolumeChangedAction = OnVolumeChangedAction;
                _popupVolumeInfoWindow.OnBeforeCloseAnimation = (window) => 
                {
                    _location = new System.Windows.Point(window.Left, window.Top);
                    _popupVolumeInfoWindow = null; 
                    return true; 
                };
            }

            _popupVolumeInfoWindow.UpdateAndShow(volume, InfoText, bForceShow);
        }

        public void Close()
        {
            if (_popupVolumeInfoWindow != null)
                _popupVolumeInfoWindow.Close();
        }
    }

    /// <summary>
    /// Interaction logic for PopupInfoWindow.xaml
    /// </summary>
    public partial class PopupVolumeInfoWindow : Window
    {
        private DispatcherTimer m_Timer = new DispatcherTimer(DispatcherPriority.Background);
        private long _timer_count = 0;

        public TimeSpan CloseTimeOut { get; set; } = TimeSpan.FromSeconds(4);

        public Action<int> OnVolumeChangedAction = (volume) => { };
        public Func<Window, bool> OnBeforeCloseAnimation = (wnd) => true;
        public Action OnAfterCloseAnimation = () => { };

        private bool _enableVolumeChangeEvent = true;
        public int Volume 
        { 
            get { return (int)_slider.Value; } 
            set 
            { 
                _enableVolumeChangeEvent = false; 
                _slider.Value = value;
                _enableVolumeChangeEvent = true;
            } 
        }

        public string InfoText 
        { 
            get { return _txtInfo.ToolTip.ToString(); } 
            set { _txtInfo.ToolTip = value; _txtInfo.Text = ShortName(value); } 
        }

        private static string ShortName(string longDeviceName)
        {
            string ellipse = "";
            int idx = longDeviceName.IndexOf("(");
            if (idx > 32)
            {
                ellipse = "...";
                idx = 30;
            }
            string name = longDeviceName.Substring(0, idx) + ellipse;
            return name;
        }

        public void UpdateAndShow(int volume, string deviceName, bool bForceShow)
        {
            InfoText = deviceName;
            if (this.Volume != volume || bForceShow) 
            {
                _timer_count = 0;
                this.Volume = volume;
                this.Opacity = 0.9;

                m_Timer.Stop();
                m_Timer.Start();

                this.Show();
           }
        }

        private PopupVolumeInfoWindow() 
        {
            InitializeComponent(WindowStartupLocation.CenterScreen, new Point());
        }

        internal PopupVolumeInfoWindow(Point location)
        {
            InitializeComponent(WindowStartupLocation.Manual, location);
        }

        private PopupVolumeInfoWindow(WindowStartupLocation startupLocation, Point location, Window owner)
        {
            //set owner will minimize with parent window
            //this.Owner = owner;
            InitializeComponent(startupLocation, location);
        }

        //for WinForms
        private PopupVolumeInfoWindow(WindowStartupLocation startupLocation, Point location, IntPtr ownerHandle)
        {
            //set owner will minimize with parent window
            //WindowInteropHelper helper = new WindowInteropHelper(this);
            //helper.Owner = ownerHandle;
            InitializeComponent(startupLocation, location);
        }

        private void InitializeComponent(WindowStartupLocation startupLocation, Point location)
        {
            this.DataContext = this;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _slider.Focus();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double maxCount = CloseTimeOut.TotalSeconds / m_Timer.Interval.TotalSeconds;

            m_Timer.Stop();
            if (_timer_count++ > maxCount)
            {
                _timer_count = 0;
                VisibilityHideAnimation();
                return; //do not start timer
            }
            m_Timer.Start();
        }

        private void VisibilityHideAnimation()
        {
            if (!OnBeforeCloseAnimation(this))
                return;

            var animation = new DoubleAnimation
            {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(0.7),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => 
            { 
                this.Opacity = 0; 
                this.Visibility = Visibility.Collapsed;
                this.Close();
                OnAfterCloseAnimation();
            };

            this.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            m_Timer.Stop();
            _timer_count = 0;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            _timer_count = 0;
            m_Timer.Start();
        }

        private void _slider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //NotifyVolumeChanged();
        }

        private void _slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NotifyVolumeChanged();
        }

        private void NotifyVolumeChanged()
        {
            Debug.WriteLine("Volume: " + (int)_slider.Value);
            if (_enableVolumeChangeEvent)
            {
                _enableVolumeChangeEvent = false;
                OnVolumeChangedAction((int)_slider.Value);
                _enableVolumeChangeEvent = true;
            }
        }

        //enable arrow keys for slider (does not work on SmallChange)
        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            double change = 0;
            if (e.Key == Key.Up || e.Key == Key.Right)
                change += _slider.SmallChange;
            if (e.Key == Key.Down || e.Key == Key.Left)
                change -= _slider.SmallChange;
            
            if (change != 0)
            {
                _slider.Value += change;
                NotifyVolumeChanged();
            }
        }
    }
}
