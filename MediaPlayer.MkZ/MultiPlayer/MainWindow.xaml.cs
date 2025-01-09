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
        MultiPlayerSettings _settings = new MultiPlayerSettings();
        List<VideoPlayerUserControl> _videos = new List<VideoPlayerUserControl>();

        public MainWindow()
        {
            InitializeComponent();

            _videos = new List<VideoPlayerUserControl> { _video00, _video01, _video02, _video10, _video11, _video12 };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _settings.Load();

            if (_settings.Settings.Count == _videos.Count)
            {
                for (int i = 0; i < _videos.Count; i++)
                {
                    VideoPlayerUserControl v = _videos[i];
                    v.LoadSetting(_settings.Settings[i]);
                }
            }
            else
            {
                foreach (VideoPlayerUserControl v in _videos)
                {
                    v.Open(@"E:\Temp\YouTube\Music\20210315--＂The Lonely Shepherd''- James Last- pan flute cover-Karla Herescu--ITaj0qAehD8.mp4");
                    v.ZoomStateSet(MkZ.WPF.eZoomState.FitHeight, true);
                    v.Play();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Update(_videos);
            _settings.Save();
            e.Cancel = false;
        }
    }
}