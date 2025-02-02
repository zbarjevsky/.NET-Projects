using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MultiPlayer.MkZ.WPF
{
    /// <summary>
    /// Interaction logic for WaitCircleUserControl.xaml
    /// </summary>
    public partial class WaitCircleUserControl : System.Windows.Controls.UserControl
    {
        DispatcherTimer _timer;
        Stopwatch _stopwatch = new Stopwatch();

        public WaitCircleUserControl()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(33);
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            _text.Text = "Loading...\n" + _stopwatch.Elapsed.ToString("mm':'ss'.'f");
        }        

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                _stopwatch.Restart();
                _timer.Start();
            }
            else
            {
                _stopwatch.Stop();
                _timer.Stop();
            }
        }
    }
}
