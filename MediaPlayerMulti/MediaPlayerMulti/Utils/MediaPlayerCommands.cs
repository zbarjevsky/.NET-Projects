using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


using MkZ.WPF;

namespace MkZ.MediaPlayer.Utils
{
    public class MediaPlayerCommands
    {
        private readonly IPlayerMainWindow _mainWindow;
        
        private ICommand F11Command { get; set; }
        private ICommand EscapeCommand { get; set; }

        private VideoPlayerControlVM PlayerVM { get => _mainWindow.MediaPlayerVM; }

        public MediaPlayerCommands(IPlayerMainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            _mainWindow.Window.CommandBindings.Clear();
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed, Open_CanExecute));
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, Close_Executed, Close_CanExecute));
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.TogglePlayPause, TogglePlayPause_Executed, TogglePlayPause_CanExecute));
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Play, Play_Executed, Play_CanExecute));
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, Pause_Executed, Pause_CanExecute));
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, Stop_Executed, Stop_CanExecute));
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.PreviousTrack, PreviousTrack_Executed, PreviousTrack_CanExecute));
            _mainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.NextTrack, NextTrack_Executed, NextTrack_CanExecute));

            _mainWindow.Window.InputBindings.Clear();
            _mainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.Play, new KeyGesture(Key.Play)));
            _mainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.Pause, new KeyGesture(Key.Pause)));
            _mainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.TogglePlayPause, new KeyGesture(Key.MediaPlayPause)));

            //Full Screen
            F11Command = new RelayCommand(FullScreen_Execute, (o) => true);
            _mainWindow.Window.InputBindings.Add(new KeyBinding(F11Command, new KeyGesture(Key.F11)));

            //Escape Command
            EscapeCommand = new RelayCommand(Escape_Execute, (o) => true);
            _mainWindow.Window.InputBindings.Add(new KeyBinding(EscapeCommand, new KeyGesture(Key.Escape)));

            _mainWindow.Window.PreviewKeyDown += Window_PreviewKeyDown;
        }

        private void PreviousTrack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _mainWindow.PreviousTrack_CanExecute();
        }

        private void PreviousTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _mainWindow.PreviousTrack_Executed();
        }

        private void NextTrack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
           e.CanExecute = _mainWindow.NextTrack_CanExecute();
        }

        private void NextTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _mainWindow.NextTrack_Executed();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                e.Handled = Skip(-5);
            
            if (e.Key == Key.Right)
                e.Handled = Skip(5);

            if (e.Key == Key.Up)
                e.Handled = VolumeDelta(0.05);

            if (e.Key == Key.Down)
                e.Handled = VolumeDelta(-0.05);

            if (e.Key == Key.Space)
            {
                TogglePlayPause_Executed(sender, null);
                e.Handled = true;
            }
        }

        private bool VolumeDelta(double delta)
        {
            double vol = 1000 * PlayerVM.Volume;

            delta /= Math.Abs(delta);

            if ((vol + delta) >= 10 && (vol + delta) <= 100)
            {
                vol -= (vol % 10); //round to nearest 10
                delta *= 10;
            }
            else if ((vol + delta) > 100)
            {
                vol -= (vol % 100); //round to nearest 100
                delta *= 100;
            }

            vol += (int)delta;

            if (vol < 0) vol = 0;
            if (vol > 1000) vol = 1000;

            PlayerVM.Volume = vol / 1000.0;

            return true;
        }

        private bool Skip(double seconds)
        {
            bool resume = false;
            if (PlayerVM.MediaState == MediaState.Play)
            {
                PlayerVM.Pause();
                resume = true;
            }

            double pos = PlayerVM.Position.TotalSeconds;
            pos += seconds;
            
            if (pos < 0) pos = 0;
            if (pos > PlayerVM.NaturalDuration) pos = PlayerVM.NaturalDuration - 1;

            PlayerVM.Position = TimeSpan.FromSeconds(pos);

            if (resume)
                PlayerVM.Play();

            return true;
        }

        private void Escape_Execute(object obj)
        {
            //if full screen
            if (_mainWindow.Window.WindowStyle == WindowStyle.None)
                _mainWindow.ToggleFullScreen();

            Pause_Executed(this, null);
        }

        private void FullScreen_Execute(object obj)
        {
            _mainWindow.ToggleFullScreen();
        }

        private void TogglePlayPause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MediaState ms = PlayerVM.MediaState;
            e.CanExecute = ms == MediaState.Play || ms == MediaState.Pause || ms == MediaState.Stop;
        }

        private void TogglePlayPause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayerVM.TogglePlayPauseState();
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
            {
                _mainWindow.AddNewMediaFile(openFileDialog.FileName, PlayerVM.Volume > 0 ? PlayerVM.Volume : 0.5);
            }
        }

        public void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (PlayerVM.IsOpen && PlayerVM.MediaState != MediaState.Play);
        }

        public void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayerVM.Play();
        }

        public void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = PlayerVM.MediaState == MediaState.Play;
        }

        public void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayerVM.Pause();
        }

        public void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MediaState ms = PlayerVM.MediaState;
            e.CanExecute = ms == MediaState.Play || ms == MediaState.Pause;
        }

        public void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayerVM.Stop();
        }

        public void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayerVM.Pause();
            _mainWindow.Window.Close();
        }
    }
}
