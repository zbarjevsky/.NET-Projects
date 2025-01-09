using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<VideoPlayerUserControl> _videos = new List<VideoPlayerUserControl>();

        public MainWindow()
        {
            InitializeComponent();

            _videos = new List<VideoPlayerUserControl> { _video00, _video01, _video02, _video10, _video11, _video12 };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (VideoPlayerUserControl v in _videos)
            {
                v.Open(@"C:\Temp\YouTube\Music\20220916--По вашим просьбам вся песня  ＂Хочу назад в СССР＂.--2UGKOyH13Gc.mp4");
                v.ZoomStateSet(MkZ.WPF.eZoomState.FitHeight, true);
                v.Play();
            }

            _video00.Volume = 1;
        }
    }
}