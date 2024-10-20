﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.Win32;

using MkZ.MediaPlayer.Utils;
using MkZ.Tools;
using MkZ.Windows.Win32API;
using MkZ.WPF;
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

        private MediaPlayerContext Context => MediaPlayerContext.Instance;

        public MediaDatabaseInfo MediaDB => MediaPlayerContext.Instance.AppConfig.MediaDatabaseInfo;

        public VideoPlayerControlVM PlayerVM => MediaPlayerContext.Instance.PlayerVM;

        private readonly string[] _commandLine;

        public MainWindow(string [] commandLine)
        {
            _commandLine = commandLine;
            Log.d("MediaPlayer.MkZ - MainWindow - c-tor");

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

            Context.PropertyChanged += Config_PropertyChanged;
            Context.AppConfig.MediaDatabaseInfo.PropertyChanged += MediaDatabaseInfo_PropertyChanged;

            _hideHeaderAnimationHelper = new GridLengthAnimationHelper(this, rowHeader);

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            Context.AppConfig.Load();

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

            Context.AppConfig.Settings.MainWindowState.RestoreTo(this);
            this.WindowState = WindowState.Normal; //always normal - to position on correct screen for maximize
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mnuPlayLists.ItemsSource = MediaDB.RootList.PlayLists;

            ConfigurationRestore(Context.AppConfig.Settings);

            //CursorArrow arrow = new CursorArrow();
            //arrow.SetCursorSize(50);
            //arrow.BindToColor(Context.AppConfig.Configuration, "CursorColor.B");
            //this.Cursor = CursorFromControl.Create(arrow, new Size(80, 80));

            _cursorArrow.Visibility = Visibility.Hidden;
            _cursorArrow.Load_Cursor(_gridMain, sizeRatio: 20);
            _cursorArrow.BindToColor(Context.AppConfig.Settings, "CursorColor.B");

            Application.Current.Dispatcher.BeginInvoke(new Action(() => SetPlayList(MediaDB.SelectedPlayList)));

            InitThumbnailToolBarButtons();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MediaDB.SelectedMediaFileIndex = _cmbFilesList.SelectedIndex;

            ConfigurationUpdate(Context.AppConfig.Settings);

            Context.AppConfig.Save();
            _player.ClosePlayer();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _zoomImage.FitWindow(0);
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    break;
                case PowerModes.Suspend:
                    PlayerVM.Pause();
                    break;
            }
        }

        private bool ProcessCommandLine()
        {
            if(_commandLine == null || _commandLine.Length == 0)
                return false;

            string fileName = _commandLine[0];
            if (!File.Exists(fileName))
                return false;

            if (!Context.AppConfig.Settings.IsSupportedMediaFile(fileName))
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

        private void InitThumbnailToolBarButtons()
        {
            TaskbarManagerHelper.Init(new WindowInteropHelper(this).Handle);
            TaskbarManagerHelper.ShowButtons(
                new List<string>() { "Full Screen", "Previous", "Play/Pause", "Next" },
                new List<System.Drawing.Icon>() { Properties.Resources.RestoreFullScreen, Properties.Resources.previus_on, Properties.Resources.pause_on, Properties.Resources.next_on });
            TaskbarManagerHelper.Button(0).DismissOnClick = true;
            TaskbarManagerHelper.ButtonClicked = (index) =>
            {
                if (index == 0)
                    FullScreenToggle();
                if (index == 1)
                    PreviousTrack_Executed(bResetPositionAndPlay: true);
                if (index == 2)
                    PlayerVM.TogglePlayPauseState();
                if (index == 3)
                    NextTrack_Executed(bResetPositionAndPlay: true); 
            };

            //FullScreenImageHelper.OnVisibleChanged = (form, isVisible) =>
            //{
            //    if (isVisible)
            //    {
            //        TaskbarManagerHelper.Button(0).Icon = Properties.Resources.FullScreen16_Restore_light;
            //        m_btnFullScreen.Image = m_imageListFullScreen.Images[3];
            //        m_mnuViewFullScreen.Image = m_imageListFullScreen.Images[3];
            //    }
            //    else
            //    {
            //        TaskbarManagerHelper.Button(0).Icon = Properties.Resources.FullScreen16_Light;
            //        m_btnFullScreen.Image = m_imageListFullScreen.Images[2];
            //        m_mnuViewFullScreen.Image = m_imageListFullScreen.Images[2];
            //    }
            //};
        }

        private void ConfigurationRestore(Configuration config)
        {
            PlayerVM.Volume = config.Volume;
            config.MainWindowState.RestoreTo(this);

            bool bFullScreen = config.MainWindowState.WindowStyle == WindowStyle.None;

            FullScreenSet(bFullScreen);
        }

        private void ConfigurationUpdate(Configuration config)
        {
            config.Volume = PlayerVM.Volume;
            config.MainWindowState.CopyFrom(this);
        }

        private void ComboMediaFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerVM.Pause();
            Context.AppConfig.Save();

            if(_cmbFilesList.SelectedItem is MediaFileInfo info)
            {
                if (info != null && !string.IsNullOrWhiteSpace(info.FileName))
                {
                    Retry:
                    if (!File.Exists(info.FileName))
                    {
                        var res = PopUp.MessageBox("File Not Found: \n" + info.FileName, "Open Media File",
                            MessageBoxImage.Exclamation, TextAlignment.Left,
                            new PopUp.PopUpButtons("_Skip to Next", "Re_move & Next", "Re_try", PopUp.PopUpResult.Btn1), 12000);

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
            if (e.PropertyName == nameof(Context.BackgroundImageFileName))
            {
                this.Background = ColorUtils.CalculateAverageColor(Context.BackgroundImageFileName);
            }
        }

        private void MediaDatabaseInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MediaDatabaseInfo.SelectedMediaFileIndex))
            {
                if (_cmbFilesList.SelectedIndex != Context.AppConfig.MediaDatabaseInfo.SelectedMediaFileIndex)
                    _cmbFilesList.SelectedIndex = Context.AppConfig.MediaDatabaseInfo.SelectedMediaFileIndex;
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

                _clock.DataContext = Context.AppConfig.Settings.ClockConfig;
                _reiKiProgress.DataContext = MediaDB.SelectedPlayList.ReiKiConfig;

                bool bFullScreen = this.WindowStyle == WindowStyle.None;
                UpdateAttachedBounds(bFullScreen);

                Context.NotifyPropertyChanged(nameof(Context.BackgroundImageFileName));
                Context.NotifyPropertyChangedAll();
            }
        }

        private void OnMediaEnded(IMediaPlayer vm)
        {
            PlayList playList = MediaDB.SelectedPlayList;
            bool can_play_next = NextTrack_CanExecute();

            if (playList.PlayMode == ePlayMode.RepeatOne)
            {
                vm.Position = TimeSpan.FromSeconds(0);
            }
            else if (playList.PlayMode == ePlayMode.PlayOne)
            {
                vm.Pause();
            }
            else if (playList.PlayMode == ePlayMode.PlayAll && can_play_next)
            {
                vm.Stop();
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
                    //playList.MediaFiles[0].Volume = PlayerVM.Volume; //same volume as previous
                    playList.SelectedMediaFileIndex = -1;
                    NextTrack_Executed(bResetPositionAndPlay: true);
                }
            }
            else if (playList.PlayMode == ePlayMode.Random)
            {
                vm.Stop();
            }
            else //no next prev - stop
            {
                vm.Stop();
                _reiKiProgress.Pause();
            }
        }

        private bool OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            HRESULT err = new HRESULT(e.ErrorException.HResult);
            Log.e("MainWindow OnMediaFailed({0}) - {1}", e.ErrorException.Message, err.Description);

            string fileName = "";
            if (sender is VideoPlayerControlVM vm)
                fileName = vm.FileName;

            string message = string.Format("Open Media Failed: \n{0}\n{1}\n{2}", 
                err.Description, e.ErrorException.Message, fileName);
            if (Path.GetExtension(fileName) == ".webm")
                message += "\nTry to rename extension to .mkv\n";
            message += "\nKeep file in Play List?";

            PopUp.PopUpResult res = PopUp.MessageBox(message, "Open Media Failed",
               MessageBoxImage.Exclamation, TextAlignment.Left,
               new PopUp.PopUpButtons("_Skip to Next", "Re_move & Next", "Re_try", PopUp.PopUpResult.Btn1), 12000);

            //PopUp.PopUpResult res = this.MessageQuestion(message, "Open Media Failed", PopUp.PopUpButtonsType.CancelNoYes);
            if(res == PopUp.PopUpResult.Btn2)
            {
                if(MediaDB.SelectedPlayList.MediaFiles[MediaDB.SelectedMediaFileIndex].FileName == fileName)
                {
                    int idx = MediaDB.SelectedPlayList.RemoveMediaFileFromList(MediaDB.SelectedPlayList.MediaFiles[MediaDB.SelectedMediaFileIndex]);
                    _cmbFilesList.SelectedIndex = idx;
                }
            }
            else if(res == PopUp.PopUpResult.Btn3) //retry
            {
                System.Threading.Thread.Sleep(1000);
                Dispatcher.BeginInvoke(new Action(() => { this.PlayerVM.Play(); }));
            }
            else //next
            {
                OnMediaEnded(this.PlayerVM);
            }

            return true;
        }

        #region IPlayerMainWindow
        public Window Window => this;

        public void AddNewMediaFiles(string[] fileNames, double volume)
        {
            MediaPlayerContext.Instance.AddNewMediaFiles(MediaDB.SelectedPlayList, fileNames, volume);
            _cmbFilesList.SelectedIndex = MediaDB.SelectedPlayList.SelectedMediaFileIndex;
        }

        public void FullScreenToggle()
        {
            ConfigurationUpdate(Context.AppConfig.Settings);

            bool bFullScreen = this.WindowStyle != WindowStyle.None;
            FullScreenSet(bFullScreen);
        }

        public void FullScreenSet(bool bFullScreen)
        {
            AttachedBoundsPauseUpdate();

            if (bFullScreen == false)//go normal
            {
                //_hideHeaderAnimationHelper.ShowRow(true, false);
                _hideHeaderAnimationHelper.CanHide = false;
                rowHeader.Height = _hideHeaderAnimationHelper.InitialRowHeight; //no animation

                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
                this.WindowState = Context.AppConfig.Settings.MainWindowState.WindowState;

                PlayerVM.IsFullScreen = false;
            }
            else //go full screen
            {
                //_hideHeaderAnimationHelper.ShowRow(false, true);
                _hideHeaderAnimationHelper.CanHide = true;
                rowHeader.Height = new GridLength(0);

                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Normal; //toggle normal/maximize to fill whole screen
                this.WindowState = WindowState.Maximized;
                PlayerVM.IsFullScreen = true;
            }

            UpdateAttachedBounds(bFullScreen);
        }

        public void AttachedBoundsPauseUpdate()
        {
            _clock.Zoomable.BoundsAttach(null);
            _reiKiProgress.Zoomable.BoundsAttach(null);
        }

        public void UpdateAttachedBounds(bool bFullScreen)
        {
            if (bFullScreen == false)//go normal
            {
                _clock.Zoomable.BoundsAttach(MediaDB.SelectedPlayList.ClockBounds.Normal);
                _reiKiProgress.Zoomable.BoundsAttach(MediaDB.SelectedPlayList.ReiKiConfig.BoundsSettings.Normal);
            }
            else //go full screen
            {
                _clock.Zoomable.BoundsAttach(MediaDB.SelectedPlayList.ClockBounds.FullScreen);
                _reiKiProgress.Zoomable.BoundsAttach(MediaDB.SelectedPlayList.ReiKiConfig.BoundsSettings.FullScreen);
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
                //playList.MediaFiles[playList.SelectedMediaFileIndex].Volume = PlayerVM.Volume;
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
                //playList.MediaFiles[playList.SelectedMediaFileIndex].Volume = PlayerVM.Volume;
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
                //playList.MediaFiles[playList.SelectedMediaFileIndex].Volume = PlayerVM.Volume;
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

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            Action<Grid> setCustomCursor = (grid) =>
            {
                CursorArrow arrow = new CursorArrow();
                arrow.SetCursorSize(50);
                arrow.BindToColor(Context.AppConfig.Settings, "CursorColor.B");
                grid.Cursor = CursorFromControl.Create(arrow, new Size(80, 80));
            };

            OptionsWindow.ShowOptionsEx(this, Context.AppConfig, "Settings", setCustomCursor, "Settings", "Clock Configuration", "Clock Font");
        }

        private void ButtonPlayListManager_Click(object sender, RoutedEventArgs e)
        {
            PlayListManagerWindow wnd = new PlayListManagerWindow();
            wnd.Owner = this;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool ok = wnd.ShowDialog().Value;
            Context.MediaPlayerCommands = _mediaPlayerCommands;

            ConfigurationUpdate(Context.AppConfig.Settings);

            Context.AppConfig.Save();
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
            string file = Context.BackgroundImageFileName;
            if (!File.Exists(file))
            {
                ApplicationCommands.Open.Execute(this, null);
                return;
            }

            string dir = Path.GetDirectoryName(file);
            List<string> list = new List<string>();
            foreach (string ext in Context.AppConfig.Settings.SupportedImageExtensions)
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

            Context.BackgroundImageFileName = list[idx];
        }
    }
}
