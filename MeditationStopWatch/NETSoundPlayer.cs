using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MeditationStopWatch
{
    public class NETSoundPlayer
    {
        private MediaPlayer _player = new MediaPlayer();
        MCIPLayer.PlayingMode _mode = MCIPLayer.PlayingMode.none;

        public EventHandler OnMediaOpened;
        public EventHandler<ExceptionEventArgs> OnMediaFailed;
        public EventHandler OnMediaEnded;

        public string FileName { get; internal set; }

        public NETSoundPlayer()
        {
            _player.MediaOpened += _player_MediaOpened;
            _player.MediaFailed += _player_MediaFailed;
            _player.MediaEnded += _player_MediaEnded;
        }

        internal void CmdClose()
        {
            FileName = string.Empty;
            _player.Close();
            _mode = MCIPLayer.PlayingMode.none;
        }

        private void _player_MediaEnded(object sender, EventArgs e)
        {
            _mode = MCIPLayer.PlayingMode.stopped;
            if (OnMediaEnded != null)
                OnMediaEnded(sender, e);
        }

        private void _player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            _mode = MCIPLayer.PlayingMode.error;
            if (OnMediaFailed != null)
                OnMediaFailed(sender, e);
        }

        private void _player_MediaOpened(object sender, EventArgs e)
        {
            _mode = MCIPLayer.PlayingMode.open;
            if (OnMediaOpened != null)
                OnMediaOpened(sender, e);
        }

        internal void SetPosition(int v)
        {
            _player.Position = TimeSpan.FromMilliseconds(v);
        }

        internal MCIPLayer.PlayingMode GetStatusMode()
        {
            return _mode;
        }

        internal int GetCurentMilisecond()
        {
            return (int)_player.Position.TotalMilliseconds;
        }

        internal void Play(string fullName, string shortName)
        {
            Open(fullName, shortName);
            CmdPlay();
        }

        internal void SetVolume(int volume)
        {
            _player.Volume = volume / 1000.0;
        }

        internal TimeSpan GetSongLenght()
        {
            //WaitForStatus(MCIPLayer.PlayingMode.playing, MCIPLayer.PlayingMode.open);
            Debug.Assert(_player.NaturalDuration.HasTimeSpan, "Duration not updated yet");
            return _player.NaturalDuration.TimeSpan;
        }

        internal void Open(string fullName, string shortName)
        {
            if (FileName != shortName)
            {
                FileName = shortName;

                _player.Close();
                _player.Open(new Uri(fullName));

                //WaitForStatus(MCIPLayer.PlayingMode.open, MCIPLayer.PlayingMode.error);
            }
        }

        private void WaitForStatus(MCIPLayer.PlayingMode m1, MCIPLayer.PlayingMode m2)
        {
            int count = 0;
            while (_mode != m1 && _mode != m2)
            {
                if (count++ > 40)
                    throw new Exception("Cannot play file: " + FileName);

                Thread.Sleep(128);
                System.Windows.Forms.Application.DoEvents();
            }

            if(_mode == MCIPLayer.PlayingMode.error)
                throw new Exception("Error play file: " + FileName);
        }

        internal void CmdPause()
        {
            _player.Pause();
            _mode = MCIPLayer.PlayingMode.paused;
        }

        internal void CmdResume()
        {
            _player.Play();
            _mode = MCIPLayer.PlayingMode.playing;
        }

        internal void CmdPlay()
        {
            _player.Stop();
            _player.Play();
            _mode = MCIPLayer.PlayingMode.playing;
        }

        internal void CmdStop()
        {
            _player.Stop();
            _mode = MCIPLayer.PlayingMode.stopped;
        }
    }
}
