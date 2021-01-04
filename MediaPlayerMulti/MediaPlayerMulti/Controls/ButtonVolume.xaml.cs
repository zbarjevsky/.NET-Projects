using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


using MZ.WPF;

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for ButtonVolume.xaml
    /// </summary>
    public partial class ButtonVolume : ToggleButton
    {
        public ButtonVolume()
        {
            InitializeComponent();
        }

        private void _button_MouseEnter(object sender, MouseEventArgs e)
        {
            PopupVolumeControlWindow.Show(this, DataContext);
        }

        private void _button_Click(object sender, RoutedEventArgs e)
        {
            PopupVolumeControlWindow.Show(this, DataContext);
        }

        //private void PopupVolume_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void PopupVolume_Opened(object sender, EventArgs e)
        //{
        //    if (sender is Popup popup)
        //    {
        //        DispatcherTimer timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(3);
        //        timer.Tick += (s1, e1) =>
        //        {
        //            timer.Stop();
        //            if (popup.IsOpen)
        //                popup.IsOpen = false;
        //            Debug.WriteLine("Popup timer.Tick - is open: " + popup.IsOpen);
        //        };
        //        timer.Start();
        //    }
        //}
    }
}
