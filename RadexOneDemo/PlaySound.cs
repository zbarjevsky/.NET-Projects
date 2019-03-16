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
        private MediaPlayer m_mediaPlayer;
        private Form _app;

        public int Volume
        {
            get { return (int)(m_mediaPlayer.Volume * 100.0); }
            set { m_mediaPlayer.Volume = value / 100.0; }
        }

        public bool Loop { get; set; }

        public PlaySound(Form app)
        {
            _app = app;

            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = Path.Combine(basePath, "Sounds", "Tornado_Siren_II-Delilah.mp3");

            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(new Uri(fileName));
            m_mediaPlayer.Volume = 0.2;
            Loop = true;

            m_mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (Loop)
                m_mediaPlayer.Position = TimeSpan.Zero;
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
