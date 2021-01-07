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
    /// Interaction logic for PlayListManagerWindow.xaml
    /// </summary>
    public partial class PlayListManagerWindow : Window
    {
        PlayListManagerVM VM = new PlayListManagerVM();

        public PlayListManagerWindow()
        {
            DataContext = VM;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VM.NotifyPropertyChangedAll();
        }

        private void _treePlayLists_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayList list = _treePlayLists.SelectedItem as PlayList;
        }

        private void RemoveMediaFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemovePlayList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is PlayList list)
                {
                    MessageBox.Show("Not Implemented");
                    VM.RemovePlayList(list);
                }
            }
        }

        private void AddPlayList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if(btn.DataContext is PlayList list)
                    VM.DB.AddNewPlayList("NewPlayList", list);
            }
        }

        private void ButtonAddRootPlayList_Click(object sender, RoutedEventArgs e)
        {
            VM.DB.AddNewPlayList("NewPlayList", null);
        }
    }
}
