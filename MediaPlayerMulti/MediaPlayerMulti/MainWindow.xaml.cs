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

namespace MediaPlayerMulti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<MediaPlayerTabVM> _players = new ObservableCollection<MediaPlayerTabVM>();

        public MainWindow()
        {
            InitializeComponent();

            tabPlayers.Items.Clear();
            tabPlayers.ItemsSource = _players;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddPlayer_Click(sender, e);
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

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowStyle == WindowStyle.None)
            {
                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
            }

            //_player.FitWindow();
        }

        private void RemovePlayer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerTabVM tab = new MediaPlayerTabVM();
            _players.Add(tab);
            tabPlayers.SelectedIndex = _players.Count - 1;
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
