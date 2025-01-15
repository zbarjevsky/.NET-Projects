using Microsoft.Win32;
using MkZ.WPF;
using MkZ.WPF.PropertyGrid;
using System.Windows;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

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
            LoadSettings(_settings.FileName);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Update(_videos);
            _settings.Save(_settings.FileName);
            VideoCommandsUserControl.Exit();    
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = _settings.DataFolder;
            ofd.FileName = _settings.FileName;
            ofd.Filter = "XML Files (*.xml)|*.xml";
            if (ofd.ShowDialog(this).Value == true)
            {
                LoadSettings(ofd.FileName);
            }
        }

        private void LoadSettings(string fileName)
        {
            _settings.Load(fileName);

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
                    v.ZoomStateSet(eZoomState.FitHeight, true);
                    v.Play();
                }
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = _settings.DataFolder;
            sfd.FileName = _settings.FileName;
            sfd.Filter = "XML Files (*.xml)|*.xml";
            if (sfd.ShowDialog(this).Value == true)
            {
                _settings.Update(_videos);
                _settings.Save(sfd.FileName);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
                this.Close();
        }

        private void CloseAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _videos.Count; i++)
            {
                VideoPlayerUserControl v = _videos[i];
                v.LoadSetting(new OnePlayerSettings());
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptions(this, _settings, "Settings", 650);
        }
    }
}