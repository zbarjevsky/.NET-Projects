using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Resources;
using System.IO;

namespace RadexOneDemo
{
    public class PlaySound
    {
        private readonly Stream _streamSound;
        SoundPlayer player;

        public PlaySound()
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            _streamSound = a.GetManifestResourceStream("RadexOneDemo.Sounds.ir_end.wav");
            player = new SoundPlayer(_streamSound);

        }
        public void Play()
        {
            player.PlayLooping(); 
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}
