using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MkZ.MediaPlayer.Utils;

namespace MkZ.MediaPlayer
{
    public class VideoPlayerContext
    {
        public static VideoPlayerContext Instance { get; } = new VideoPlayerContext();

        public AppConfig Config { get; } = new AppConfig();
    }
}
