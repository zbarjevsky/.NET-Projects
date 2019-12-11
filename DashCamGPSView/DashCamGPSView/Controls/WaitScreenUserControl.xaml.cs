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

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for WaitScreenUserControl.xaml
    /// https://github.com/thomaslevesque/WpfAnimatedGif
    /// </summary>
    public partial class WaitScreenUserControl : UserControl
    {
        public WaitScreenUserControl()
        {
            InitializeComponent();
        }

        private void Image_AnimationCompleted(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
