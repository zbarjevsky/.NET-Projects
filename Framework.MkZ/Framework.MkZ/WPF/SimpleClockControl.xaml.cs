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

namespace MkZ.WPF
{
    /// <summary>
    /// Interaction logic for SimpleClockControl.xaml
    /// </summary>
    public partial class SimpleClockControl : UserControl
    {
        public SimpleClockControl()
        {
            InitializeComponent();

            UpdateHands();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.033);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateHands();
        }

        private void UpdateHands()
        {
            DateTime now = DateTime.Now;

            _minute.Angle = now.TimeOfDay.TotalMinutes * 6.0;
            _hour.Angle = now.TimeOfDay.TotalHours * 30.0;
            _second.Angle = now.TimeOfDay.TotalSeconds * 6.0;
        }
    }
}
