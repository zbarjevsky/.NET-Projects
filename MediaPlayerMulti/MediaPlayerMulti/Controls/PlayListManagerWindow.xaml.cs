using MkZ.MediaPlayer.Utils;
using MkZ.WPF.DragDrop;
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
    public partial class PlayListManagerWindow : Window, IPlayerMainWindow
    {
        private MediaPlayerCommands _mediaPlayerCommands;

        PlayListManagerVM VM = new PlayListManagerVM();
        ListViewDragDropManager<MediaFileInfo> dragMgr = new ListViewDragDropManager<MediaFileInfo>();

        public PlayListManagerWindow()
        {
            DataContext = VM;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dragMgr.ListView = _listMediaFiles;
            dragMgr.ShowDragAdorner = true;
            dragMgr.DragAdornerOpacity = 0.5;

            _mediaPlayerCommands = new MediaPlayerCommands(this);

            VM.NotifyPropertyChangedAll();
        }

        private void _treePlayLists_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayList list = _treePlayLists.SelectedItem as PlayList;
        }

        private void RemoveMediaFile_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is MediaFileInfo file)
                {
                    var files = _listMediaFiles.ItemsSource as System.Collections.ObjectModel.ObservableCollection<MediaFileInfo>;
                    files.Remove(file);
                }
            }
        }

        private void RemovePlayList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is PlayList list)
                {
                    VM.RemovePlayList(list);
                }
            }
        }

        private void AddPlayList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is PlayList list)
                    VM.DB.AddNewPlayList("NewPlayList", list);
            }
        }

        private void ButtonAddRootPlayList_Click(object sender, RoutedEventArgs e)
        {
            VM.DB.AddNewPlayList("NewPlayList", null);
        }

        private void _listMediaFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MediaFileInfo info = _listMediaFiles.SelectedItem as MediaFileInfo;
            PlayListManagerVM vm = _listMediaFiles.DataContext as PlayListManagerVM;
            PlayList list = _treePlayLists.SelectedItem as PlayList;

            if (info != null && vm != null && list != null)
            {
                info.MediaState = MediaState.Play;
                list.SelectedMediaFileIndex = list.MediaFiles.IndexOf(info);
                list.IsSelectedPlayList = true;

                this.DialogResult = true;
                this.Close();
            }
        }

        #region IPlayerMainWindow

        public Window Window => this;

        public void AddNewMediaFiles(string[] fileNames, double volume)
        {
            PlayList playList  = _treePlayLists.SelectedItem as PlayList;
            VideoPlayerContext.Instance.AddNewMediaFiles(playList, fileNames, volume);
        }

        public void ToggleFullScreen()
        {
        }

        public bool PreviousTrack_CanExecute()
        {
            return false;
        }

        public void PreviousTrack_Executed()
        {
        }

        public bool NextTrack_CanExecute()
        {
            return false;
        }

        public void NextTrack_Executed()
        {
        }

        #endregion    
    }
}
