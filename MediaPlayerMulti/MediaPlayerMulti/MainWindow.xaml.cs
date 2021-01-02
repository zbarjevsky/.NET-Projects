using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using Microsoft.Win32;


using MkZ.MediaPlayer;
using MkZ.MediaPlayer.Controls;
using MkZ.MediaPlayer.Utils;

namespace MediaPlayerMulti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPlayerMainWindow
    {
        private readonly ObservableCollection<MediaPlayerTabVM> _players = new ObservableCollection<MediaPlayerTabVM>();
        private MediaPlayerCommands _mediaPlayerCommands;
        private AppConfig _appConfig = new AppConfig();

        public Window Window => this;

        public VideoPlayerControlVM PlayerVM => GetSelectedPlayer();

        public MainWindow()
        {
            InitializeComponent();

            tabPlayers.Items.Clear();
            tabPlayers.ItemsSource = _players;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _appConfig.Load();
            
            if (_appConfig.OpenedFiles.Count > 0)
            {
                foreach (VideoPlayerState state in _appConfig.OpenedFiles)
                {
                    AddPlayer(state);
                }
            }
            else
            {
                AddPlayer();
            }
            tabPlayers.SelectedIndex = 0;

            _mediaPlayerCommands = new MediaPlayerCommands(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _appConfig.OpenedFiles.Clear();
            _appConfig.OpenedFiles = _players.Select(vm => vm.PlayerVM.GetPlayerState()).ToList();
            _appConfig.Save();
        }

        private VideoPlayerControlVM GetTabItem(int idx)
        {
            MediaPlayerTabVM o = tabPlayers.Items[idx] as MediaPlayerTabVM;
            return o.PlayerVM;
        }

        private VideoPlayerControlVM GetSelectedPlayer()
        {
            return GetTabItem(tabPlayers.SelectedIndex);
        }

        private void RemovePlayer_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).DataContext is MediaPlayerTabVM vm)
                _players.Remove(vm);
            
            if (_players.Count == 0)
                AddPlayer_Click(sender, e);
        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            AddPlayer();
            tabPlayers.SelectedIndex = _players.Count - 1;
        }

        private void AddPlayer(VideoPlayerState state = null)
        {
            MediaPlayerTabVM tab = new MediaPlayerTabVM();
            _players.Add(tab);

            if (state != null)
            {
                state.MediaState = MediaState.Pause;

                tab.PlayerVM.Init(state);
                tab.NotifyPropertyChangedAll();
            }
        }
    }
}
