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
    }
}
