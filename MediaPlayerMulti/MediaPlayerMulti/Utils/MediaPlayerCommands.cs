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
    public class MediaPlayerCommands : IDisposable
    {
        public IPlayerMainWindow MainWindow { get; private set; }

        private ICommand F11Command { get; set; }
        private ICommand EscapeCommand { get; set; }

        private VideoPlayerControlVM PlayerVM  => VideoPlayerContext.Instance.PlayerVM;

        public Configuration Config => VideoPlayerContext.Instance.Config.Configuration;

        public MediaPlayerCommands(IPlayerMainWindow mainWindow, bool enableKeyboardShortcuts)
        {
            MainWindow = mainWindow;

            MainWindow.Window.CommandBindings.Clear();
            MainWindow.Window.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed, Open_CanExecute));
            MainWindow.Window.CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, Close_Executed, Close_CanExecute));
            MainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.TogglePlayPause, TogglePlayPause_Executed, TogglePlayPause_CanExecute));
            MainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Play, Play_Executed, Play_CanExecute));
            MainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, Pause_Executed, Pause_CanExecute));
            MainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, Stop_Executed, Stop_CanExecute));
            MainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.PreviousTrack, PreviousTrack_Executed, PreviousTrack_CanExecute));
            MainWindow.Window.CommandBindings.Add(new CommandBinding(MediaCommands.NextTrack, NextTrack_Executed, NextTrack_CanExecute));

            MainWindow.Window.InputBindings.Clear();
            MainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.Play, new KeyGesture(Key.Play)));
            MainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.Pause, new KeyGesture(Key.Pause)));
            MainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.TogglePlayPause, new KeyGesture(Key.MediaPlayPause)));

            //Full Screen
            F11Command = new RelayCommand(FullScreen_Execute, (o) => true);
            MainWindow.Window.InputBindings.Add(new KeyBinding(F11Command, new KeyGesture(Key.F11)));

            //Escape Command
            EscapeCommand = new RelayCommand(Escape_Execute, (o) => true);
            MainWindow.Window.InputBindings.Add(new KeyBinding(EscapeCommand, new KeyGesture(Key.Escape)));

            if(enableKeyboardShortcuts)
                MainWindow.Window.PreviewKeyDown += Window_PreviewKeyDown;
        }

        public void Dispose()
        {
            if (MainWindow != null)
            {
                MainWindow.Window.CommandBindings.Clear();
                MainWindow.Window.InputBindings.Clear();

                MainWindow.Window.PreviewKeyDown -= Window_PreviewKeyDown;
            }
            MainWindow = null;
        }

        public void PreviousTrack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = MainWindow.PreviousTrack_CanExecute();
        }

        public void PreviousTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow.PreviousTrack_Executed(bResetPositionAndPlay: false);
        }

        public void NextTrack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
           e.CanExecute = MainWindow.NextTrack_CanExecute();
        }

        public void NextTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow.NextTrack_Executed(bResetPositionAndPlay: false);
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

        public void Escape_Execute(object obj)
        {
            //if full screen
            if (MainWindow.Window.WindowStyle == WindowStyle.None)
                MainWindow.ToggleFullScreen();

            Pause_Executed(this, null);
        }

        public void FullScreen_Execute(object obj)
        {
            MainWindow.ToggleFullScreen();
        }

        public void TogglePlayPause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MediaState ms = PlayerVM.MediaState;
            e.CanExecute = ms == MediaState.Play || ms == MediaState.Pause || ms == MediaState.Stop;
        }

        public void TogglePlayPause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PlayerVM.TogglePlayPauseState();
        }

        public void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string allSupportedExtensions = Config.GetAllSupportedExtensions();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Media files ("+ allSupportedExtensions + ")|"+ allSupportedExtensions + "|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                MainWindow.AddNewMediaFiles(openFileDialog.FileNames, PlayerVM.Volume > 0 ? PlayerVM.Volume : 0.5);
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
            MainWindow.Window.Close();
        }
    }
}
