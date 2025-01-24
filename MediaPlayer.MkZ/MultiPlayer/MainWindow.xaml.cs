using Microsoft.Win32;
using MkZ.Tools;
using MkZ.WPF;
using MkZ.WPF.PropertyGrid;
using System.Windows;
using System.Windows.Controls;
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

            _videos = new List<VideoPlayerUserControl> { _video00, _video01, _video02, _video03, _video10, _video11, _video12, _video13 };

            if (!SingleInstanceHelper.IsSingleInstance(true))
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings(_settings.FileName);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SplittersSave(_gridMain.RowDefinitions, _gridMain.ColumnDefinitions);

            _settings.Update(_videos);
            if (_settings.HasData())
                _settings.Save(_settings.FileName);
            VideoCommandsVM.Exit();    
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

            SplittersLoad(_gridMain.RowDefinitions, _gridMain.ColumnDefinitions);

            if (_settings.PlayerSettings.Count > 2)
            {
                for (int i = 0; i < _videos.Count && i < _settings.PlayerSettings.Count; i++)
                {
                    VideoPlayerUserControl v = _videos[i];
                    v.LoadSetting(_settings.PlayerSettings[i]);
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

        private void SplittersLoad(RowDefinitionCollection rows, ColumnDefinitionCollection cols)
        {
            if (_settings.RowsSizes != null && _settings.RowsSizes.Count == rows.Count)
            for (int i = 0; i < rows.Count; i++)
                rows[i].Height = _settings.RowsSizes[i].Pos;

            if (_settings.ColsSizes != null && _settings.ColsSizes.Count == cols.Count)
                for (int i = 0; i < cols.Count; i++)
                    cols[i].Width = _settings.ColsSizes[i].Pos;
        }

        private void SplittersSave(RowDefinitionCollection rows, ColumnDefinitionCollection cols)
        {
            _settings.RowsSizes = new List<SplitterPos>();
            foreach (RowDefinition row in rows)
                _settings.RowsSizes.Add(new SplitterPos(row.Height));

            _settings.ColsSizes = new List<SplitterPos>();
            foreach (ColumnDefinition col in cols)
                _settings.ColsSizes.Add(new SplitterPos(col.Width));
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

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            _ = ClearAllAsync();
        }

        private async Task ClearAllAsync()
        {
            foreach (VideoPlayerUserControl v in _videos)
            {
                v.Clear();
                await Task.Delay(3);
            }
            VideoCommandsVM.ClearPopUp();
        }

        private void ResetLayout_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _gridMain.RowDefinitions.Count; i++)
                if (_gridMain.RowDefinitions[i].Height.IsStar)
                    _gridMain.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);

            for (int i = 0; i < _gridMain.ColumnDefinitions.Count; i++)
                if (_gridMain.ColumnDefinitions[i].Width.IsStar)
                    _gridMain.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptions(this, _settings, "Settings", 650);
        }
    }
}