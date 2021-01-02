using Microsoft.Win32;
using MkZ.MediaPlayer.Utils;
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
    public partial class FullScreenPlayerWindow : Window, IPlayerMainWindow
    {
        private VideoPlayerControlVM _vm = new VideoPlayerControlVM();
        private VideoPlayerState _playerState = new VideoPlayerState();
        private MediaPlayerCommands _mediaPlayerCommands;

        public Window Window => this;

        public VideoPlayerControlVM PlayerVM => _vm;

        public FullScreenPlayerWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;
            _player.DataContext = _vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _playerState.RestoreStateTo(_vm);

            //here is - close full screen window
            _player.OnFullScreenButtonClick = (vm) => 
            { 
                _playerState.CopyFrom(vm.GetPlayerState()); 
                vm.Stop(); 
                this.Close(); 
            };

            _mediaPlayerCommands = new MediaPlayerCommands( this);
        }

        public void Init(VideoPlayerState state)
        {
            _playerState.CopyFrom(state);
        }

        public VideoPlayerState GetPlayerState()
        {
            return _playerState;
        }
    }
}
