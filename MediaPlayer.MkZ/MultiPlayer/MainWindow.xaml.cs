using Microsoft.Win32;
using MkZ.Tools;
using MkZ.WPF;
using MkZ.WPF.PropertyGrid;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace MultiPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MultiPlayerSettings _settings = new MultiPlayerSettings();
        List<VideoPlayerUserControl> _videos = new List<VideoPlayerUserControl>();

        public static Dictionary<string, RecentFile> RecentFiles { get; } = new Dictionary<string, RecentFile>();

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
            LoadSettings(_settings.DefaultSettingsFileName);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings(_settings.DefaultSettingsFileName);
            VideoCommandsVM.PopUpVM.Exit();    
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = _settings.DataFolder;
            ofd.FileName = _settings.DefaultSettingsFileName;
            ofd.Filter = "XML Files (*.xml)|*.xml";
            if (ofd.ShowDialog(this).Value == true)
            {
                LoadSettings(ofd.FileName);
            }
        }

        private async void LoadSettings(string fileName)
        {
            _settings.Load(fileName);

            _settings.MainWindowState.RestoreTo(this);

            RecentFiles.Clear();
            foreach (RecentFile f in _settings.RecentFiles)
                RecentFiles.Add(f.FileName, f);

            SplittersLoad(_gridMain.RowDefinitions, _gridMain.ColumnDefinitions);

            if (_settings.PlayerSettings.Count > 2)
            {
                await InitFirstPlayerForMp3();

                for (int i = 0; i < _videos.Count && i < _settings.PlayerSettings.Count; i++)
                {
                    _ = _videos[i].LoadSetting(_settings.PlayerSettings[i]);
                }

                if (_settings.PopUpPlayerSettings != null && File.Exists(_settings.PopUpPlayerSettings.FileName))
                {
                     VideoCommandsVM.PopUpVM.LoadSettings(_settings.PopupWindowState,  _settings.PopUpPlayerSettings);
                }
                else
                {
                    VideoCommandsVM.PopUpVM.PopUpHide();
                }
            }
            else
            {
                foreach (VideoPlayerUserControl v in _videos)
                {
                    _ = v.VM.OpenFromFile(@"E:\Temp\YouTube\Music\20210315--＂The Lonely Shepherd''- James Last- pan flute cover-Karla Herescu--ITaj0qAehD8.mp4", true);
                }
            }
        }

        /// <summary>
        /// HACK
        /// If the first player has MP3 file - it is stuck
        /// pre-load it will work second time
        /// </summary>
        private async Task InitFirstPlayerForMp3()
        {
            string fileName = _settings.PlayerSettings[0].FileName.ToLower();
            if (string.IsNullOrEmpty(fileName))
                return;

            if (System.IO.Path.GetExtension(fileName) == ".mp3")
            {
                //preload settings to init player control
                _ = _videos[0].LoadSetting(_settings.PlayerSettings[0]);
                await Task.Delay(333);
            }
        }

        private void SaveSettings(string fileName)
        {
            SplittersSave(_gridMain.RowDefinitions, _gridMain.ColumnDefinitions);

            _settings.Update(_videos);

            _settings.MainWindowState.CopyFrom(this);

            _settings.RecentFiles.Clear();
            foreach (var f in RecentFiles)
                _settings.RecentFiles.Add(f.Value);

            if (_settings.HasData())
                _settings.Save(fileName);
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings(_settings.DefaultSettingsFileName);
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = _settings.DataFolder;
            sfd.FileName = _settings.DefaultSettingsFileName;
            sfd.Filter = "XML Files (*.xml)|*.xml";
            if (sfd.ShowDialog(this).Value == true)
            {
                SaveSettings(sfd.FileName);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var video = _videos.FirstOrDefault(v => v.IsInFocus);

            e.Handled = true;

            if (e.Key == _settings.KeyCloseApp)
                this.Close();
            else
                e.Handled = MainWindow_PreviewKeyDown(video, sender, e);
        }

        public bool MainWindow_PreviewKeyDown(VideoPlayerUserControl? video, object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool handled = true;

            if (e.Key == _settings.KeyClearAll)
                this.ClearAll_Click(sender, e);
            else if (e.Key == _settings.KeySaveAsLast)
                this.SaveSettings(_settings.LastSettingsFileName);
            else if (e.Key == _settings.KeyLoadDefault)
                this.LoadSettings(_settings.DefaultSettingsFileName);
            else if (e.Key == _settings.KeySaveAsDefault)
                this.SaveSettings(_settings.DefaultSettingsFileName);
            else if (e.Key == _settings.KeyBatchOp)
                this.Magic_Click(sender, e);
            else
                handled = video?.VM.Control_KeyDown(e) == true;

            return handled;
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
            VideoCommandsVM.PopUpVM.PopUpHide();
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

        private void SaveAsRecent_Click(object sender, RoutedEventArgs e)
        {
            this.SaveSettings(_settings.LastSettingsFileName);
        }

        private void OpenDefault_Click(object sender, RoutedEventArgs e)
        {
            this.LoadSettings(_settings.DefaultSettingsFileName);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptions(this, _settings, "Settings", 650, 170);
        }

        public static RecentFile FindRecentFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            string name = Path.GetFileName(fileName);
            if (RecentFiles.ContainsKey(name))
                return RecentFiles[name];

            string shortName = name.TrimStart('_');
            if (RecentFiles.ContainsKey(shortName))
                return RecentFiles[shortName];

            return null;
        }

        public static RecentFile FindOrCreateRecentFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            RecentFile recentFile = FindRecentFile(fileName);
            if (recentFile != null)
                return recentFile;

            string name = Path.GetFileName(fileName);
            string shortName = name.TrimStart('_');

            RecentFiles.Add(shortName, new RecentFile() { FileName = shortName });
            return RecentFiles[shortName];
        }

        public static bool IsSupportedFileExtension(string fileName)
        {
            return _settings.SupportedFileExtensions.IsSupportedFileExtension(fileName);
        }

        public static bool IsFavorite(string fileName)
        {
            RecentFile recentFile = FindRecentFile(fileName);
            if (recentFile != null)
                return recentFile.IsFavorite;

            return false;
        }

        public static System.Windows.Media.Brush InactiveBackgroundBrush => GetBrush(_settings.InactiveBackgroundColor);

        private static System.Windows.Media.Brush GetBrush(System.Drawing.Color c)
        {
            return new System.Windows.Media.SolidColorBrush(new System.Windows.Media.Color() { A = 255, R = c.R, G = c.G, B = c.B });
        }

        public static void UpdateRecentFile(string fileName, bool isFavorite)
        {
            RecentFile recentFile = FindRecentFile(fileName);
            if (recentFile != null)
                recentFile.IsFavorite = isFavorite;
        }

        private void PauseAll_Click(object sender, RoutedEventArgs e)
        {
            VideoCommandsVM.PopUpVM.PopUpPause();

            foreach (VideoPlayerUserControl v in _videos)
            {
                v.VM.Pause(updateUI: true);
            }
        }

        //Pause -> Save Recent -> Clear -> Load Default
        private void Magic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PauseAll_Click(sender, e);
                SaveAsRecent_Click(sender, e);
                ClearAll_Click(sender, e);
                OpenDefault_Click(sender, e);

                //System.Windows.MessageBox.Show(this, "Magic Done!", this.Title);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(this, ex.Message, this.Title);
            }        
        }
    }
}