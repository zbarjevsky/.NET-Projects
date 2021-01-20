using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MkZ.WPF.WpfAnalogClock.Controls
{
    /// <summary>
    /// Interaction logic for AnalogClock.xaml
    /// </summary>
    public partial class AnalogClock : UserControl
    {

        public Brush DigitalClockColor
        {
            get { return txtTime.Foreground; }
            set { txtTime.Foreground = value; }
        }

        public bool DisableScreensaver
        {
            get;
            set;
        }

        public Visibility DigitalClockVisibility
        {
            get { return txtTime.Visibility; }
            set { txtTime.Visibility = value; }
        }

        public AnalogClock()
        {
            InitializeComponent();

            dispatcherTimer_Tick(this, null);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 120);

            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double secondAngle = 6.0 * now.Second;
            if (secondAngle != rotateSecond.Angle)
                rotateSecond.Angle = secondAngle;

            double minuteAngle = 6.0 * (now.Minute + (now.Second / 60.0));
            if (minuteAngle != rotateMinute.Angle)
                rotateMinute.Angle = minuteAngle;

            double hourAngle = 30.0 * ((now.Hour % 12) + (now.Minute / 60.0));
            if (hourAngle != rotateHour.Angle)
                rotateHour.Angle = hourAngle;

            txtTime.Text = now.ToString("T");

            if(DisableScreensaver)
                ScreenSaver.ResetIdleTimer(DisableScreensaver); 
        }
    }
}
