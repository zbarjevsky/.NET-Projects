using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MkZ.MediaPlayer.Utils
{
    public class MediaPlayerCommands
    {
        private readonly IPlayerMainWindow _player;

        public MediaPlayerCommands(IPlayerMainWindow player)
        {
            _player = player;

            _player.Window.CommandBindings.Clear();
            _player.Window.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed, Open_CanExecute));
            _player.Window.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, Close_Executed, Close_CanExecute));
            _player.Window.CommandBindings.Add(new CommandBinding(MediaCommands.TogglePlayPause, TogglePlayPause_Executed, TogglePlayPause_CanExecute));
            _player.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Play, Play_Executed, Play_CanExecute));
            _player.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, Pause_Executed, Pause_CanExecute));
            _player.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, Stop_Executed, Stop_CanExecute));

            _player.Window.InputBindings.Clear();
            _player.Window.InputBindings.Add(new KeyBinding(ApplicationCommands.Close, new KeyGesture(Key.Escape)));
            _player.Window.InputBindings.Add(new KeyBinding(MediaCommands.Play, new KeyGesture(Key.Play)));
            _player.Window.InputBindings.Add(new KeyBinding(MediaCommands.Pause, new KeyGesture(Key.Pause)));
            _player.Window.InputBindings.Add(new KeyBinding(MediaCommands.TogglePlayPause, new KeyGesture(Key.MediaPlayPause)));
            _player.Window.InputBindings.Add(new KeyBinding(MediaCommands.TogglePlayPause, new KeyGesture(Key.Space)));
        }

        private void TogglePlayPause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MediaState ms = GetSelectedPlayer().MediaState;
            e.CanExecute = ms == MediaState.Play || ms == MediaState.Pause;
        }

        private void TogglePlayPause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().TogglePlayPauseState();
        }

        private VideoPlayerControlVM GetSelectedPlayer()
        {
            return _player.PlayerVM;
        }

        public void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mkv;*.mp4)|*.mp3;*.mpg;*.mpeg;*.mkv;*.mp4|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                GetSelectedPlayer().OpenAndPlay(openFileDialog.FileName);
        }

        public void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (GetSelectedPlayer().IsOpen && GetSelectedPlayer().MediaState != MediaState.Play);
        }

        public void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().Play();
        }

        public void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = GetSelectedPlayer().MediaState == MediaState.Play;
        }

        public void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().Pause();
        }

        public void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MediaState ms = GetSelectedPlayer().MediaState;
            e.CanExecute = ms == MediaState.Play || ms == MediaState.Pause;
        }

        public void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().Stop();
        }

        public void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().Pause();
            GetSelectedPlayer().GetPlayerState();

            _player.Window.Close();
        }
    }
}
