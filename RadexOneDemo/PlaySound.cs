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
        private double _maxVolume = 100.0;
        private MediaPlayer m_mediaPlayer;
        private Form _app;

        public int Volume
        {
            get { return (int)(m_mediaPlayer.Volume * _maxVolume); }
            set { m_mediaPlayer.Volume = value / _maxVolume; }
        }

        public string VolumeToString()
        {
            return string.Format("{0:0.0}%", 100.0 * m_mediaPlayer.Volume);
        }

        public bool Loop { get; set; }

        public PlaySound(Form app, int volume, double maxVolume)
        {
            _maxVolume = maxVolume;
            _app = app;

            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = Path.Combine(basePath, "Sounds", "Tornado_Siren_II-Delilah.mp3");

            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(new Uri(fileName));

            Volume = volume;

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
            Utils.ExecuteOnUiThreadInvoke(_app, () =>
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
            Utils.ExecuteOnUiThreadInvoke(_app, () =>
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
