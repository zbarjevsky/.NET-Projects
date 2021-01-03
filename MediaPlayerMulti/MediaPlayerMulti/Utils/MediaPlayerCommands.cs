﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


using MZ.WPF;

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

            _mainWindow.Window.InputBindings.Clear();
            _mainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.Play, new KeyGesture(Key.Play)));
            _mainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.Pause, new KeyGesture(Key.Pause)));
            _mainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.TogglePlayPause, new KeyGesture(Key.MediaPlayPause)));
            _mainWindow.Window.InputBindings.Add(new KeyBinding(MediaCommands.TogglePlayPause, new KeyGesture(Key.Space)));

            //Full Screen
            F11Command = new RelayCommand(FullScreen_Execute, (o) => true);
            _mainWindow.Window.InputBindings.Add(new KeyBinding(F11Command, new KeyGesture(Key.F11)));

            //Escape Command
            EscapeCommand = new RelayCommand(Escape_Execute, (o) => true);
            _mainWindow.Window.InputBindings.Add(new KeyBinding(EscapeCommand, new KeyGesture(Key.Escape)));
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
            e.CanExecute = ms == MediaState.Play || ms == MediaState.Pause;
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
                _mainWindow.AddNewMediaFile(openFileDialog.FileName);
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