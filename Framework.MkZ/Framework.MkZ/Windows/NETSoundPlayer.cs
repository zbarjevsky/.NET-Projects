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

namespace MkZ.Windows
{
    public class NETSoundPlayer
    {
        public enum PlayingState
        {
            none,
            playing,
            open,
            paused,
            stopped,
            error
        }

        private MediaPlayer _player = new MediaPlayer();

        public EventHandler OnMediaOpened;
        public EventHandler<ExceptionEventArgs> OnMediaFailed;
        public EventHandler OnMediaEnded;
        public EventHandler<PlayingState> OnStateChanged;

        public string FileName { get; internal set; }

        PlayingState _state = PlayingState.none;
        public PlayingState State { get { return _state; } private set { _state = value; OnStateChanged?.Invoke(this, value); } }

        public NETSoundPlayer()
        {
            _player.MediaOpened += _player_MediaOpened;
            _player.MediaFailed += _player_MediaFailed;
            _player.MediaEnded += _player_MediaEnded;
        }

        private void _player_MediaEnded(object sender, EventArgs e)
        {
            State = PlayingState.stopped;
            OnMediaEnded?.Invoke(sender, e);
        }

        private void _player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            State = PlayingState.error;
            OnMediaFailed?.Invoke(sender, e);
        }

        private void _player_MediaOpened(object sender, EventArgs e)
        {
            State = PlayingState.playing;
            OnMediaOpened?.Invoke(sender, e);
        }

        internal int PositionMs
        {
            get { return (int)_player.Position.TotalMilliseconds; }
            set { _player.Position = TimeSpan.FromMilliseconds(value); }
        }

        internal void Play(string fullName, string shortName, int volume)
        {
            Open(fullName, shortName);
            SetVolume(volume);
            CmdPlay();
        }

        internal void SetVolume(int volume)
        {
            _player.Volume = volume / 1000.0;
        }

        internal TimeSpan GetSongLenght()
        {
            Debug.Assert(_player.NaturalDuration.HasTimeSpan, "Duration not updated yet");
            return _player.NaturalDuration.TimeSpan;
        }

        internal void Open(string fullName, string shortName)
        {
            FileName = shortName;

            _player.Close();
            _player.Open(new Uri(fullName));
        }

        internal void CmdPause()
        {
            _player.Pause();
            State = PlayingState.paused;
        }

        internal void CmdResume()
        {
            _player.Play();
            State = PlayingState.playing;
        }

        internal void CmdPlay()
        {
            _player.Stop();
            _player.Play();
            State = PlayingState.playing;
        }

        internal void CmdStop()
        {
            _player.Stop();
            State = PlayingState.stopped;
        }

        internal void CmdClose()
        {
            State = PlayingState.none;
            FileName = string.Empty;
            _player.Close();
        }
    }
}
