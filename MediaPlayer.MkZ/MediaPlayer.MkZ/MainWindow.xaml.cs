using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Resources;
using Microsoft.Win32;


using MkZ.MediaPlayer.Controls;
using MkZ.MediaPlayer.Utils;
using MkZ.WPF;
using MkZ.WPF.Controls;
using MkZ.WPF.MessageBox;
using MkZ.WPF.PropertyGrid;

namespace MkZ.MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPlayerMainWindow
    {
        private MediaPlayerCommands _mediaPlayerCommands;
        private readonly FadeAnimationHelper _controlsHideAndShow;
        private readonly GridLengthAnimationHelper _hideHeaderAnimationHelper;
        ScrollDragZoom _zoomImage = null;

        private CursorArrow _cursorArrow = new CursorArrow();

        private VideoPlayerContext Context => VideoPlayerContext.Instance;

        public MediaDatabaseInfo MediaDB => VideoPlayerContext.Instance.Config.MediaDatabaseInfo;

        public VideoPlayerControlVM PlayerVM => VideoPlayerContext.Instance.PlayerVM;

        private readonly string[] _commandLine;

        public MainWindow(string [] commandLine)
        {
            _commandLine = commandLine;

            InitializeComponent();

            this.DataContext = Context;

            Context.PlayerVM = _player.DataContext as VideoPlayerControlVM;
            Context.MediaPlayerCommands = _mediaPlayerCommands = new MediaPlayerCommands(this, enableKeyboardShortcuts: true);

            _cmbFilesList.Items.Clear();
            _cmbFilesList.ItemsSource = null;

            _controlsHideAndShow = new FadeAnimationHelper(this, 2, _imagesNavigation, _cursorArrow);

            _clock.Zoomable.EnableZoom(_scrollMain);
            _reiKiProgress.Zoomable.EnableZoom(_scrollMain);

            _zoomImage = new ScrollDragZoom(_imageBackground, _scrollMain);
            _zoomImage.FitWindow(0);

            Context.Config.Configuration.PropertyChanged += Config_PropertyChanged;
            Context.Config.MediaDatabaseInfo.PropertyChanged += MediaDatabaseInfo_PropertyChanged;

            _hideHeaderAnimationHelper = new GridLengthAnimationHelper(this, rowHeader);

            Context.Config.Load();

            //add or select file if exists
            ProcessCommandLine();

            MediaDB.OnPlayListSelectionChangedAction = (playList) =>
            {
                SetPlayList(playList);
            };

            _player.OnFullScreenButtonClick = (vm) => FullScreenToggle();
            _player.OnFileDropAction = (fileNames) =>
            {
                AddNewMediaFiles(fileNames, PlayerVM.Volume > 0 ? PlayerVM.Volume : 0.3);
            };

            PlayerVM.MediaEndedAction = (vm) => OnMediaEnded(vm);
            PlayerVM.MediaFailedAction = (vm, ex) => OnMediaFailed(vm, ex);

            Context.Config.Configuration.MainWindowState.RestoreTo(this);
            this.WindowState = WindowState.Normal; //always normal - to position on correct screen for maximize
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mnuPlayLists.ItemsSource = MediaDB.RootList.PlayLists;

            LocationsRestore(Context.Config.Configuration);

            //CursorArrow arrow = new CursorArrow();
            //arrow.SetCursorSize(50);
            //arrow.BindToColor(Context.Config.Configuration, "CursorColor.B");
            //this.Cursor = CursorFromControl.Create(arrow, new Size(80, 80));

            _cursorArrow.Visibility = Visibility.Hidden;
            _cursorArrow.Load_Cursor(_gridMain, sizeRatio: 20);
            _cursorArrow.BindToColor(Context.Config.Configuration, "CursorColor.B");

            Application.Current.Dispatcher.BeginInvoke(new Action(() => SetPlayList(MediaDB.SelectedPlayList)));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MediaDB.SelectedMediaFileIndex = _cmbFilesList.SelectedIndex;

            LocationsSave();

            Context.Config.Save();
            _player.ClosePlayer();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _zoomImage.FitWindow(0);
        }

        private bool ProcessCommandLine()
        {
            if(_commandLine == null || _commandLine.Length == 0)
                return false;

            string fileName = _commandLine[0];
            if (!File.Exists(fileName))
                return false;

            if (!Context.Config.Configuration.IsSupportedMediaFile(fileName))
                return false;

            PlayList list = MediaDB.RootList.FindPlayListContainingFile(fileName);
            if(list == null)
            {
                MediaDB.AddNewMediaFile(fileName, PlayerVM.Volume);
                MediaFileInfo info = MediaDB.RootList.FindFile(fileName);
                info.MediaState = MediaState.Play;
            }
            else
            {
                MediaDB.RootList.SetSelectedPlayList(list);
                MediaFileInfo info = list.FindFile(fileName);
                info.MediaState = MediaState.Play;
                MediaDB.SelectedMediaFileIndex = list.MediaFiles.IndexOf(info);
            }

            return true;
        }

        private void LocationsRestore(Configuration config)
        {
            config.MainWindowState.RestoreTo(this);

            bool bFullScreen = config.MainWindowState.WindowStyle == WindowStyle.None;

            FullScreenSet(bFullScreen);

            _clock.DataContext = config.ClockConfig;
            _reiKiProgress.DataContext = config.ReiKiConfig;
        }

        private void LocationsSave()
        {
            Context.Config.Configuration.MainWindowState.CopyFrom(this);

            if (this.WindowStyle == WindowStyle.None) //full screen
            {
                _clock.Zoomable.BoundsGet(Context.Config.Configuration.ClockConfig.Bounds_FullScreen);
                _reiKiProgress.Zoomable.BoundsGet(Context.Config.Configuration.ReiKiConfig.Bounds_FullScreen);
            }
            else
            {
                _clock.Zoomable.BoundsGet(Context.Config.Configuration.ClockConfig.Bounds_Normal);
                _reiKiProgress.Zoomable.BoundsGet(Context.Config.Configuration.ReiKiConfig.Bounds_Normal);
            }
        }

        private void ComboMediaFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_cmbFilesList.SelectedItem is MediaFileInfo info)
            {
                if (info != null && !string.IsNullOrWhiteSpace(info.FileName))
                {
                    Retry:
                    if (!File.Exists(info.FileName))
                    {
                        var res = PopUp.MessageBox("File Not Found: \n" + info.FileName, "Open Media File",
                            MessageBoxImage.Exclamation, TextAlignment.Left,
                            new PopUp.PopUpButtons("_Skip to Next", "Re_move & Next", "Re_try", PopUp.PopUpResult.Btn3));

                        if (res == PopUp.PopUpResult.Btn3)
                            goto Retry;

                        ePlayMode playMode = MediaDB.SelectedPlayList.PlayMode;
                        bool bResetPositionAndPlayNext = false;
                        if (playMode == ePlayMode.PlayAll || playMode == ePlayMode.RepeatAll)
                            bResetPositionAndPlayNext = true;

                        bool bRemoveFromList = (res == PopUp.PopUpResult.Btn2);

                        RemoveMediaFileAndSelectNext(info, bResetPositionAndPlayNext, bRemoveFromList);
                        return;
                    }

                    MediaDB.SelectedMediaFileIndex = _cmbFilesList.SelectedIndex;

                    PlayerVM.Open(info);
                }
                else
                {
                    PlayerVM.SaveAndClear();
                }
            }
            else
            {
                PlayerVM.SaveAndClear();
            }
        }

        private void Config_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Context.Config.Configuration.BackgroundImageFileName))
            {
                this.Background = ColorUtils.CalculateAverageColor(Context.Config.Configuration.BackgroundImageFileName);
            }
        }

        private void MediaDatabaseInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MediaDatabaseInfo.SelectedMediaFileIndex))
            {
                if (_cmbFilesList.SelectedIndex != Context.Config.MediaDatabaseInfo.SelectedMediaFileIndex)
                    _cmbFilesList.SelectedIndex = Context.Config.MediaDatabaseInfo.SelectedMediaFileIndex;
            }
        }

        private void SetPlayList(PlayList playList)
        {
            _cmbFilesList.SelectedIndex = -1;

            if (playList != null && playList.IsSelectedPlayList)
            {
                _mnuPlayLists.Header = string.Format("{0} [{1}]", playList.Name, playList.MediaFiles.Count);

                //_btnSelectPlayList.Content = playList.Name;
                _cmbFilesList.ItemsSource = playList.MediaFiles;

                if (playList.SelectedMediaFileIndex >= 0 && playList.SelectedMediaFileIndex < playList.MediaFiles.Count)
                {
                    playList.MediaFiles[playList.SelectedMediaFileIndex].MediaState = MediaState.Pause;
                    _cmbFilesList.SelectedIndex = playList.SelectedMediaFileIndex;
                }

                Context.NotifyPropertyChangedAll();
            }
        }

        private void OnMediaEnded(IMediaPlayer vm)
        {
            PlayList playList = MediaDB.SelectedPlayList;
            bool can_play_next = NextTrack_CanExecute();

            if (playList.PlayMode == ePlayMode.RepeatOne)
            {
                PlayerVM.Play();
            }
            else if (playList.PlayMode == ePlayMode.PlayAll && can_play_next)
            {
                NextTrack_Executed(bResetPositionAndPlay: true);
            }
            else if (playList.PlayMode == ePlayMode.RepeatAll)
            {
                if (can_play_next)
                {
                    NextTrack_Executed(bResetPositionAndPlay: true);
                }
                else if (playList.MediaFiles.Count > 0)
                {
                    playList.MediaFiles[0].Volume = PlayerVM.Volume; //same volume as previous
                    playList.SelectedMediaFileIndex = -1;
                    NextTrack_Executed(bResetPositionAndPlay: true);
                }
            }
            else if (playList.PlayMode == ePlayMode.Random)
            {

            }
            else //no next prev - stop
            {
                _reiKiProgress.Pause();
            }
        }

        private bool OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if(sender is VideoPlayerControlVM vm)
                PopUp.Error("Open Media Failed: \n" + vm.FileName + "\n" + e.ErrorException.Message);
            return true;
        }

        #region IPlayerMainWindow
        public Window Window => this;

        public void AddNewMediaFiles(string[] fileNames, double volume)
        {
            VideoPlayerContext.Instance.AddNewMediaFiles(MediaDB.SelectedPlayList, fileNames, volume);
            _cmbFilesList.SelectedIndex = MediaDB.SelectedPlayList.SelectedMediaFileIndex;
        }

        public void FullScreenToggle()
        {
            LocationsSave();

            bool bFullScreen = this.WindowStyle != WindowStyle.None;
            FullScreenSet(bFullScreen);
        }

        public void FullScreenSet(bool bFullScreen)
        {
            if (bFullScreen == false)//go normal
            {
                //_hideHeaderAnimationHelper.ShowRow(true, false);
                _hideHeaderAnimationHelper.CanHide = false;
                rowHeader.Height = _hideHeaderAnimationHelper.InitialRowHeight; //no animation

                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
                this.WindowState = WindowState.Normal;
                PlayerVM.IsFullScreen = false;
                _clock.Zoomable.BoundsSet(Context.Config.Configuration.ClockConfig.Bounds_Normal);
                _reiKiProgress.Zoomable.BoundsSet(Context.Config.Configuration.ReiKiConfig.Bounds_Normal);
            }
            else //go full screen
            {
                //_hideHeaderAnimationHelper.ShowRow(false, true);
                _hideHeaderAnimationHelper.CanHide = true;
                rowHeader.Height = new GridLength(0);

                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                PlayerVM.IsFullScreen = true;
                _clock.Zoomable.BoundsSet(Context.Config.Configuration.ClockConfig.Bounds_FullScreen);
                _reiKiProgress.Zoomable.BoundsSet(Context.Config.Configuration.ReiKiConfig.Bounds_FullScreen);
            }
        }

        public MediaFileInfo PreviousTrackInfo()
        {
            PlayList playList = MediaDB.SelectedPlayList;
            if (playList.MediaFiles.Count > 0 && playList.SelectedMediaFileIndex > 0)
            {
                return playList.MediaFiles[playList.SelectedMediaFileIndex - 1];
            }
            return null;
        }

        public bool PreviousTrack_CanExecute()
        {
            return PreviousTrackInfo() != null;
        }

        public void PreviousTrack_Executed(bool bResetPositionAndPlay)
        {
            if (!PreviousTrack_CanExecute())
                return;

            PlayList playList = MediaDB.SelectedPlayList;
            playList.SelectedMediaFileIndex--;

            if (bResetPositionAndPlay)
            {
                playList.MediaFiles[playList.SelectedMediaFileIndex].MediaState = MediaState.Play;
                playList.MediaFiles[playList.SelectedMediaFileIndex].PositionInSeconds = 0;
                playList.MediaFiles[playList.SelectedMediaFileIndex].Volume = PlayerVM.Volume;
            }

            _cmbFilesList.SelectedIndex = playList.SelectedMediaFileIndex;
        }

        public MediaFileInfo NextTrackInfo()
        {
            PlayList playList = MediaDB.SelectedPlayList;
            if (playList.MediaFiles.Count > 0 && playList.SelectedMediaFileIndex < playList.MediaFiles.Count - 1)
            {
                return playList.MediaFiles[playList.SelectedMediaFileIndex + 1];
            }
            return null;
        }

        public bool NextTrack_CanExecute()
        {
            return NextTrackInfo() != null;
        }

        public void NextTrack_Executed(bool bResetPositionAndPlay)
        {
            if (!NextTrack_CanExecute())
                return;

            PlayList playList = MediaDB.SelectedPlayList;
            playList.SelectedMediaFileIndex++;

            if (bResetPositionAndPlay)
            {
                playList.MediaFiles[playList.SelectedMediaFileIndex].MediaState = MediaState.Play;
                playList.MediaFiles[playList.SelectedMediaFileIndex].PositionInSeconds = 0;
                playList.MediaFiles[playList.SelectedMediaFileIndex].Volume = PlayerVM.Volume;
            }

            _cmbFilesList.SelectedIndex = playList.SelectedMediaFileIndex;
        }

        public void RemoveMediaFileAndSelectNext(MediaFileInfo info, bool bResetPositionAndPlay, bool bRemoveFromList)
        {
            PlayList playList = MediaDB.SelectedPlayList;
            int infoIdx = playList.MediaFiles.IndexOf(info);

            if (bRemoveFromList)
                playList.SelectedMediaFileIndex = playList.RemoveMediaFileFromList(info);
            else //skip missing file - select next
                playList.SelectedMediaFileIndex = playList.SelectNextMediaFile(infoIdx);

            if (bResetPositionAndPlay)
            {
                playList.MediaFiles[playList.SelectedMediaFileIndex].MediaState = MediaState.Play;
                playList.MediaFiles[playList.SelectedMediaFileIndex].PositionInSeconds = 0;
                playList.MediaFiles[playList.SelectedMediaFileIndex].Volume = PlayerVM.Volume;
            }

            _cmbFilesList.SelectedIndex = playList.SelectedMediaFileIndex;
        }

        #endregion

        private void RemoveMediaFile_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is MediaFileInfo vm)
            {
                _cmbFilesList.SelectedIndex = MediaDB.SelectedPlayList.RemoveMediaFileFromList(vm);
            }
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Action<Grid> setCustomCursor = (grid) =>
            {
                CursorArrow arrow = new CursorArrow();
                arrow.SetCursorSize(50);
                arrow.BindToColor(Context.Config.Configuration, "CursorColor.B");
                grid.Cursor = CursorFromControl.Create(arrow, new Size(80, 80));
            };

            OptionsWindow.ShowOptionsEx(this, Context.Config, "Settings", setCustomCursor, "Configuration", "Clock Configuration", "Clock Font");
        }

        private void ButtonPlayListManager_Click(object sender, RoutedEventArgs e)
        {
            PlayListManagerWindow wnd = new PlayListManagerWindow();
            wnd.Owner = this;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool ok = wnd.ShowDialog().Value;
            Context.MediaPlayerCommands = _mediaPlayerCommands;

            LocationsSave();
            Context.Config.Save();
        }

        private void MenuSelectPlayList_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuSelectPlayList_MouseDown(object sender, RoutedEventArgs e)
        {
        }

        //MenuItem that has subitems does not fire 'Click' event
        private void MenuSelectPlayList_MouseUp(object sender, RoutedEventArgs e)
        {
            var propDataContext = e.OriginalSource.GetType().GetProperty("DataContext");
            var dataContext = propDataContext.GetValue(e.OriginalSource);

            if (dataContext is PlayList list)
            {
                if (list.IsSelectedPlayList == false)
                {
                    list.IsSelectedPlayList = true;
                }

                e.Handled = true;
                _cmbFilesList.Focus(); //close menu
            }
        }

        private void ButtonPrevImage_Click(object sender, RoutedEventArgs e)
        {
            OpenClosestImageFile(-1);
        }

        private void ButtonNextImage_Click(object sender, RoutedEventArgs e)
        {
            OpenClosestImageFile(1);
        }

        private void OpenClosestImageFile(int direction)
        {
            string file = Context.Config.Configuration.BackgroundImageFileName;
            if (!File.Exists(file))
            {
                ApplicationCommands.Open.Execute(this, null);
                return;
            }

            string dir = Path.GetDirectoryName(file);
            List<string> list = new List<string>();
            foreach (string ext in Context.Config.Configuration.SupportedImageExtensions)
            {
                string filter = "*" + ext;
                string[] files = Directory.GetFiles(dir, filter);
                list.AddRange(files);
            }
            list.Sort();
            int idx = list.IndexOf(file);

            idx += direction;
            if (idx < 0)
                idx = list.Count - 1;
            if (idx >= list.Count)
                idx = 0;

            Context.Config.Configuration.BackgroundImageFileName = list[idx];
        }
    }
}
