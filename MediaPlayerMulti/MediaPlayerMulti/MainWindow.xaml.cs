using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Microsoft.Win32;


using MkZ.MediaPlayer;
using MkZ.MediaPlayer.Controls;
using MkZ.MediaPlayer.Utils;

namespace MediaPlayerMulti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPlayerMainWindow
    {
        private MediaPlayerCommands _mediaPlayerCommands;
        private AppConfig _appConfig { get { return VideoPlayerContext.Instance.Config; } }

        private readonly ObservableCollection<MediaFileInfo> _mediaFiles = new ObservableCollection<MediaFileInfo>();

        public Configuration Config => _appConfig.Configuration;

        public Window Window => this;

        public VideoPlayerControlVM MediaPlayerVM => _player.DataContext as VideoPlayerControlVM;

        public MainWindow()
        {
            InitializeComponent();

            //tabPlayers.Items.Clear();
            //tabPlayers.ItemsSource = _players;

            _cmbFilesList.Items.Clear();
            _cmbFilesList.ItemsSource = _mediaFiles;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _appConfig.Load();

            _mediaFiles.Clear();
            _appConfig.RootList.MediaFiles.ForEach(file => { file.MediaState = MediaState.Pause; _mediaFiles.Add(file); });

            if (_mediaFiles.Count > 0 && _appConfig.RootList.SelectedIndex < _mediaFiles.Count)
                _cmbFilesList.SelectedIndex = _appConfig.RootList.SelectedIndex;

            _mediaPlayerCommands = new MediaPlayerCommands(this);

            _player.OnFullScreenButtonClick = (vm) => ToggleFullScreen();

            _player.OnFileDropAction = (fileName) =>
            {
                AddNewMediaFile(fileName);
            };
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _appConfig.RootList.SelectedIndex = _cmbFilesList.SelectedIndex;

            _player.ClosePlayer();
            _appConfig.RootList.MediaFiles.Clear();
            _appConfig.RootList.MediaFiles.AddRange(_mediaFiles);
            _appConfig.Save();
        }

        public void AddNewMediaFile(string fileName)
        {
            _mediaFiles.Insert(0, new MediaFileInfo() { FileName = fileName, MediaState = MediaState.Play });
            _cmbFilesList.SelectedIndex = 0;
        }

        public void ToggleFullScreen()
        {
            if (this.WindowStyle == WindowStyle.None)
            {
                rowHeader.Height = new GridLength(40);
                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
                this.WindowState = WindowState.Normal;
                MediaPlayerVM.IsFullScreen = false;
            }
            else //full screen
            {
                rowHeader.Height = new GridLength(0);
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                MediaPlayerVM.IsFullScreen = true;
            }
        }

        public bool PreviousTrack_CanExecute()
        {
            return _cmbFilesList.SelectedIndex > 0;
        }

        public void PreviousTrack_Executed()
        {
            _cmbFilesList.SelectedIndex--;
        }

        public bool NextTrack_CanExecute()
        {
            return _cmbFilesList.SelectedIndex < _cmbFilesList.Items.Count - 1;
        }

        public void NextTrack_Executed()
        {
            _cmbFilesList.SelectedIndex++;
        }

        private MediaFileInfo GetMediaItem(int idx)
        {
            MediaFileInfo o = _cmbFilesList.Items[idx] as MediaFileInfo;
            return o;
        }

        private MediaFileInfo GetSelectedMediaFile()
        {
            return GetMediaItem(_cmbFilesList.SelectedIndex);
        }

        private void RemoveMediaFile_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is MediaFileInfo vm)
            {
                _mediaFiles.Remove(vm);
                if(_mediaFiles.Count>0)
                    _cmbFilesList.SelectedIndex = 0;
            }
        }

        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow wnd = new OptionsWindow();
            wnd.Owner = this;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.ShowDialog();
        }
    }
}
