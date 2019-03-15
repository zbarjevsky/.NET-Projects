using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Resources;
using System.IO;
using System.Windows.Media;
using System.Reflection;
using System.Windows.Forms;

namespace RadexOneDemo
{
    public class PlaySound
    {
        //private readonly Stream _streamSound;
        //SoundPlayer player;

        private MediaPlayer m_mediaPlayer;
        private Form _app;

        public PlaySound(Form app)
        {
            _app = app;
            //System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            ////_streamSound = a.GetManifestResourceStream("RadexOneDemo.Sounds.ir_end.wav");
            //_streamSound = a.GetManifestResourceStream("RadexOneDemo.Sounds.Tornado_Siren_II-Delilah.wav");
            //player = new SoundPlayer(_streamSound);

            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = Path.Combine(basePath, "Sounds", "Tornado_Siren_II-Delilah.mp3");

            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(new Uri(fileName));
            m_mediaPlayer.Volume = 0.2;
        }

        public void Play()
        {
            //player.PlayLooping();
            Utils.ExecuteOnUiThreadBeginInvoke(_app, () =>
            {
                try
                {
                    m_mediaPlayer.Play();
                }
                catch (Exception err)
                {
                }
            });
        }

        public void Stop()
        {
            Utils.ExecuteOnUiThreadBeginInvoke(_app, () =>
            {
                try
                {
                    //player.Stop();
                    m_mediaPlayer.Stop();
                }
                catch (Exception err)
                {
                }
            });
        }
    }
}
