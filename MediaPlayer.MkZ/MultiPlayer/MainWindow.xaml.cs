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

        /// <summary>
        /// If true - play all players one after another
        /// </summary>
        public bool IsGlobalRepeatAllMode
        {
            get { return (bool)GetValue(IsGlobalRepeatAllModeProperty); }
            set { SetValue(IsGlobalRepeatAllModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsGlobalRepeatAllMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsGlobalRepeatAllModeProperty =
            DependencyProperty.Register(nameof(IsGlobalRepeatAllMode), typeof(bool), typeof(MainWindow), 
                new PropertyMetadata(false, OnGlobalRepeatAllModeChanged));

        private static void OnGlobalRepeatAllModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wnd = (MainWindow)d;
            bool newValue = (bool)e.NewValue;

            //notify all player controls of this change
            foreach (var video in wnd._videos)
            {
                video.VM.NotifyPropertyChanged(nameof(video.VM.SelectedPlayModeIndex));
            }
        }

        public static MainWindow Instance { get; private set; } = null;

        public MainWindow()
        {
            InitializeComponent();

            _videos = new List<VideoPlayerUserControl> { 
                _videoA, _videoB, _videoC, 
                _video00, _video01, _video02, _video03, 
                _video10, _video11, _video12, _video13 };

            if (!SingleInstanceHelper.IsSingleInstance(true))
            {
                this.Close();
            }

            Instance = this;
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

        private void OpenRecent_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = _settings.DataFolder;
            ofd.FileName = _settings.RecentFilesFileName;
            ofd.Filter = "XML Files (*.xml)|*.xml";
            if (ofd.ShowDialog(this).Value == true)
            {
                _settings.UpdateRecentFiles(ofd.FileName);
            }
        }

        private async void LoadSettings(string fileName)
        {
            _settings.Load(fileName);

            _settings.MainWindowState.RestoreTo(this);

            //RecentFiles.Clear();
            foreach (RecentFile f in _settings.RecentFiles.RecentFilesList)
            {
                if (!RecentFiles.ContainsKey(f.FileName)) 
                    RecentFiles.Add(f.FileName, f);
                else
                    RecentFiles[f.FileName] = f;
            }

            SplittersLoad(_settings.GridSplitterPositionsMain, _gridMain);
            SplittersLoad(_settings.GridSplitterPositionsTop, _gridTop);
            SplittersLoad(_settings.GridSplitterPositionsBottom, _gridBottom);

            this.IsGlobalRepeatAllMode = _settings.IsGlobalRepeatAllMode;

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
            SplittersSave(_settings.GridSplitterPositionsMain, _gridMain);
            SplittersSave(_settings.GridSplitterPositionsTop, _gridTop);
            SplittersSave(_settings.GridSplitterPositionsBottom, _gridBottom);

            _settings.IsGlobalRepeatAllMode = this.IsGlobalRepeatAllMode;

            _settings.Update(_videos);

            _settings.MainWindowState.CopyFrom(this);

            _settings.RecentFiles.Clear();
            foreach (var f in RecentFiles)
                _settings.RecentFiles.RecentFilesList.Add(f.Value);

            if (_settings.HasData())
                _settings.Save(fileName);
        }

        private static void SplittersLoad(GridSplitterPositions pos, Grid grid)
        {
            RowDefinitionCollection rows = grid.RowDefinitions;
            ColumnDefinitionCollection cols = grid.ColumnDefinitions;

            if (pos.RowsSizes != null && pos.RowsSizes.Count == rows.Count)
            for (int i = 0; i < rows.Count; i++)
                rows[i].Height = pos.RowsSizes[i].Pos;

            if (pos.ColsSizes != null && pos.ColsSizes.Count == cols.Count)
                for (int i = 0; i < cols.Count; i++)
                    cols[i].Width = pos.ColsSizes[i].Pos;
        }

        private static void SplittersSave(GridSplitterPositions pos, Grid grid)
        {
            RowDefinitionCollection rows = grid.RowDefinitions;
            ColumnDefinitionCollection cols = grid.ColumnDefinitions;

            pos.RowsSizes = new List<SplitterPos>();
            foreach (RowDefinition row in rows)
                pos.RowsSizes.Add(new SplitterPos(row.Height));

            pos.ColsSizes = new List<SplitterPos>();
            foreach (ColumnDefinition col in cols)
                pos.ColsSizes.Add(new SplitterPos(col.Width));
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
            ResetLayout(_gridMain);
            ResetLayout(_gridTop);
            ResetLayout(_gridBottom);
        }

        private static void ResetLayout(Grid grid)
        {
            if (grid.RowDefinitions.Count > 1)
                for (int i = 0; i < grid.RowDefinitions.Count; i++)
                    if (grid.RowDefinitions[i].Height.IsStar)
                        grid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);

            if (grid.ColumnDefinitions.Count > 1)
                for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
                    if (grid.ColumnDefinitions[i].Width.IsStar)
                        grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
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

        public System.Windows.Media.Brush InactiveBackgroundBrush => _settings.InactiveBackgroundColor;

        //private static System.Windows.Media.Brush GetBrush(System.Drawing.Color c)
        //{
        //    return new System.Windows.Media.SolidColorBrush(new System.Windows.Media.Color() { A = 255, R = c.R, G = c.G, B = c.B });
        //}

        public static void UpdateRecentFile(string fileName, bool isFavorite)
        {
            RecentFile recentFile = FindRecentFile(fileName);
            if (recentFile != null)
                recentFile.IsFavorite = isFavorite;
        }

        public void PauseAll()
        {
            VideoCommandsVM.PopUpVM.PopUpPause();

            foreach (VideoPlayerUserControl v in _videos)
            {
                v.VM.Pause(updateUI: true);
            }
        }

        public void PlayNext(VideoPlayerUserControl player)
        {
            if ( IsGlobalRepeatAllMode == false)
                return;

            for (int i = 0; i < _videos.Count; i++)
            {
                if ( _videos[i] == player )
                {
                    //stop current
                    //_videos[i].VM.Stop();

                    int next = FindNext(i);
                    if (next != -1)
                    {
                        //play next
                        _videos[next].VM.Position = TimeSpan.FromSeconds(0);
                        _videos[next].VM.Play();
                    }
                }
            }       
        }

        private int FindNext(int i)
        {
            //find next
            i++;
            if (i >= _videos.Count)
                i = 0;

            for (int j = 0; j < _videos.Count; j++)
            {
                if (File.Exists(_videos[i].FileName))
                    return i;

                i++;
                if (i >= _videos.Count)
                    i = 0;
            }
            
            return -1;
        }

        private void PauseAll_Click(object sender, RoutedEventArgs e)
        {
            PauseAll();
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