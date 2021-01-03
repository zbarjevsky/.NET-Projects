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
        VideoPlayerControlVM MediaPlayerVM { get; }
        void AddNewMediaFile(string fileName);
        void ToggleFullScreen();
    }
}
