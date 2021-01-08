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
        Configuration Config { get; }
        Window Window { get; }
        VideoPlayerControlVM MediaPlayerVM { get; }
        void AddNewMediaFile(string fileName, double volume);
        void ToggleFullScreen();
        bool PreviousTrack_CanExecute();
        void PreviousTrack_Executed();
        bool NextTrack_CanExecute();
        void NextTrack_Executed();
    }
}
