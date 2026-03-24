using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiPlayer
{
    public class PopUpWindowVM
    {
        private readonly PopUpWindow WndMax = new PopUpWindow();
        private VideoPlayerUserControl _player = null;
        private VideoCommandsVM _parentVM = null;

        public bool IsPopUpWindowActive => WndMax.IsActive;

        public PopUpWindowVM()
        {
            _player = WndMax._video;
            _parentVM = _player.VM;
        }

        public void LoadSettings(MainWindowState popupWindowState, OnePlayerSettings popUpPlayerSettings)
        {
            if (!File.Exists(popUpPlayerSettings?.FullFileName))
                return;

            OpenPopUpWindowFromSettings(popUpPlayerSettings);
            popupWindowState.RestoreTo(WndMax);
        }

        public OnePlayerSettings GetSettings(MainWindowState popupWindowState)
        {
            if (WndMax.Visibility != Visibility.Visible)
                return null;

            popupWindowState?.CopyFrom(WndMax);

            if (!File.Exists(_player.FileName))
                return null;

            OnePlayerSettings s = new OnePlayerSettings(_player);
            BookmarkSettings recentFile = MainWindow.Instance.RecentFilesCache.FindOrCreateRecentFile(s.FullFileName);
            recentFile.UpdateFrom(s.BookmarkSettings, force: false);
            s.UpdateBookmarks(recentFile);

            return s;
        }

        public bool IsFullScreen(bool isPopWindowMode)
        {
            if (isPopWindowMode)
                return WndMax.IsFullScreen;
            return false;
        }

        public VideoPlayerUserControl PopUpPlayer { get => WndMax._video; }

        public bool IsPopupOpen()
        {
            return WndMax.Visibility == Visibility.Visible;
        }

        public void PopUpClear()
        {
            WndMax.ClearVideoControl();
        }

        public void PopUpPause()
        {
            WndMax.Pause();
        }

        public void PopUpHide()
        {
            WndMax.ClearVideoControl();
            WndMax.Visibility = Visibility.Collapsed;
        }

        public void OpenPopUpWindowFromSettings(OnePlayerSettings s)
        {
            if (WndMax.Visibility == Visibility.Collapsed)
            {
                WndMax.InitWindow(System.Windows.Application.Current.MainWindow, matchMainWindow: false);
                WndMax.Show();
                WndMax.LoadSettings(new OnePlayerSettings(s));
            }
            else
            {
                WndMax.LoadSettings(new OnePlayerSettings(s));
                WndMax.BringToFront();
            }
        }

        public void MaximizeToggle(bool hide, VideoCommandsVM fromVM)
        {
            if (fromVM.IsPopWindowMode) //calling from PopUpWindow
            {
                if (hide)
                {
                    PopUpHide();
                }
                else
                {
                    WndMax.MaximizeToggle();
                }
            }
            else //calling from grid Window
            {
                OpenPopUpWindowFromSettings(fromVM.Settings);
                fromVM.Pause(updateUI: true);
            }
        }

        public void Exit()
        {
            WndMax.Exit();
        }
    }
}
