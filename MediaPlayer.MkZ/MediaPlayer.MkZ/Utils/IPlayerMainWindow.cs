using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MkZ.MediaPlayer.Utils
{
    public interface IPlayerMainWindow
    {
        Window Window { get; }
        void AddNewMediaFiles(string [] fileNames, double volume);
        void FullScreenToggle();
        bool PreviousTrack_CanExecute();
        MediaFileInfo PreviousTrackInfo();
        void PreviousTrack_Executed(bool bResetPositionAndPlay);
        bool NextTrack_CanExecute();
        MediaFileInfo NextTrackInfo();
        void NextTrack_Executed(bool bResetPositionAndPlay);
        void RemoveMediaFileAndSelectNext(MediaFileInfo info, bool bResetPositionAndPlay, bool bRemoveFromList);
    }
}
