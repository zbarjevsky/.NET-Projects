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

        public Configuration Config => _appConfig.Configuration;

        public MediaDatabaseInfo DB => _appConfig.MediaDatabaseInfo;

        public Window Window => this;

        public VideoPlayerControlVM MediaPlayerVM => _player.DataContext as VideoPlayerControlVM;

        public MainWindow()
        {
            InitializeComponent();

            //tabPlayers.Items.Clear();
            //tabPlayers.ItemsSource = _players;

            _cmbFilesList.Items.Clear();
            _cmbFilesList.ItemsSource = null;

            Config.PropertyChanged += Config_PropertyChanged;
        }

        private void Config_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Config.BackgroundImageFileName))
            {
                MediaPlayerVM.BackgroundImage = new BitmapImage(new Uri(Config.BackgroundImageFileName));
                _player.Background = ColorUtils.CalculateAverageColor(Config.BackgroundImageFileName);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _appConfig.Load();

            SetPlayList(DB.GetSelectedPlayList());
            DB.OnPlayListSelectionChangedAction = (playList) =>
            {
                 SetPlayList(playList);
            };

            _ctxPlayLists.ItemsSource = DB.RootList.PlayLists;

            _mediaPlayerCommands = new MediaPlayerCommands(this);

            _player.OnFullScreenButtonClick = (vm) => ToggleFullScreen();
            _player.OnFileDropAction = (fileName) =>
            {
                AddNewMediaFile(fileName, MediaPlayerVM.Volume > 0 ? MediaPlayerVM.Volume : 0.3);
            };
        }

        private void SetPlayList(PlayList playList)
        {
            if (playList != null && playList.IsSelectedPlayList)
            {
                _btnSelectPlayList.Content = playList.Name;
                _cmbFilesList.ItemsSource = playList.MediaFiles;

                if (playList.MediaFiles.Count > 0 && DB.SelectedMediaFileIndex < playList.MediaFiles.Count)
                    _cmbFilesList.SelectedIndex = playList.SelectedMediaFileIndex;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DB.SelectedMediaFileIndex = _cmbFilesList.SelectedIndex;

            _player.ClosePlayer();
            _appConfig.Save();
        }

        public void AddNewMediaFile(string fileName, double volume)
        {
            if (Config.IsSupportedImageFile(fileName))
                Config.BackgroundImageFileName = fileName;
            else if (Config.IsSupportedMediaFile(fileName))
                _cmbFilesList.SelectedIndex = DB.AddNewMediaFile(fileName, volume);
            else
                MessageBox.Show("File type is not supported.\n" + fileName);
        }

        public void ToggleFullScreen()
        {
            if (this.WindowStyle == WindowStyle.None)
            {
                rowHeader.Height = new GridLength(46);
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
            PlayList playList = DB.GetSelectedPlayList();
            return playList.SelectedMediaFileIndex > 0;
        }

        public void PreviousTrack_Executed()
        {
            PlayList playList = DB.GetSelectedPlayList();
            
            playList.SelectedMediaFileIndex--;

            playList.MediaFiles[playList.SelectedMediaFileIndex].MediaState = MediaState.Play;
            playList.MediaFiles[playList.SelectedMediaFileIndex].PositionInSeconds = 0;
            
            _cmbFilesList.SelectedIndex = playList.SelectedMediaFileIndex;
        }

        public bool NextTrack_CanExecute()
        {
            PlayList playList = DB.GetSelectedPlayList();
            return playList.SelectedMediaFileIndex < playList.MediaFiles.Count - 1;
        }

        public void NextTrack_Executed()
        {
            PlayList playList = DB.GetSelectedPlayList();
            playList.SelectedMediaFileIndex++;

            playList.MediaFiles[playList.SelectedMediaFileIndex].MediaState = MediaState.Play;
            playList.MediaFiles[playList.SelectedMediaFileIndex].PositionInSeconds = 0;

            _cmbFilesList.SelectedIndex = playList.SelectedMediaFileIndex;
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
                _cmbFilesList.SelectedIndex = DB.GetSelectedPlayList().DeleteMediaFile(vm);
            }
        }

        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow wnd = new OptionsWindow();
            wnd.Owner = this;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.ShowDialog();
        }

        private void ButtonPlayListManager_Click(object sender, RoutedEventArgs e)
        {
            PlayListManagerWindow wnd = new PlayListManagerWindow();
            wnd.Owner = this;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if(wnd.ShowDialog().Value)
            {
                _cmbFilesList.SelectedIndex = DB.GetSelectedPlayList().SelectedMediaFileIndex;
            }
        }
    }
}
