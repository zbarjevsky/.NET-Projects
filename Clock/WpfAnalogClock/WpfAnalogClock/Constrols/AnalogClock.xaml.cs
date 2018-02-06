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

namespace WpfAnalogClock.Constrols
{
    /// <summary>
    /// Interaction logic for AnalogClock.xaml
    /// </summary>
    public partial class AnalogClock : UserControl
    {
        DispatcherTimer dispatcherTimer;

        public AnalogClock()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            rotateMinute.Angle = now.Second * 6;// / Math.PI;
        }
    }
}
