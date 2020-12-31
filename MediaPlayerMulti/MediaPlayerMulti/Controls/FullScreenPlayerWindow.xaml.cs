using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for FullScreenPlayerWindow.xaml
    /// </summary>
    public partial class FullScreenPlayerWindow : Window
    {
        private VideoPlayerControlVM _vm = new VideoPlayerControlVM();
        private VideoPlayerState _playerState = new VideoPlayerState();

        public FullScreenPlayerWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;
            _player.DataContext = _vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _playerState.RestoreState(_vm);

            //here is - close full screen window
            _player.OnFullScreenButtonClick = (vm) => 
            { 
                _playerState.CopyFrom(vm.GetPlayerState()); 
                vm.Stop(); 
                this.Close(); 
            };
        }

        public void Init(VideoPlayerState state)
        {
            _playerState.CopyFrom(state);
        }

        public VideoPlayerState GetPlayerState()
        {
            return _playerState;
        }

        private VideoPlayerControlVM GetSelectedPlayer()
        {
            return _vm;
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mkv;*.mp4)|*.mp3;*.mpg;*.mpeg;*.mkv;*.mp4|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                GetSelectedPlayer().OpenAndPlay(openFileDialog.FileName);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (GetSelectedPlayer().IsOpen && GetSelectedPlayer().MediaState != MediaState.Play);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().Play();
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = GetSelectedPlayer().MediaState == MediaState.Play;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            MediaState ms = GetSelectedPlayer().MediaState;
            e.CanExecute = ms == MediaState.Play || ms == MediaState.Pause;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetSelectedPlayer().Stop();
        }

    }
}
