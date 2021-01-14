using MkZ.MediaPlayer.Utils;
using MkZ.WPF.DragDrop;
using System;
using System.Collections.Generic;
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

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for PlayListManagerWindow.xaml
    /// </summary>
    public partial class PlayListManagerWindow : Window, IPlayerMainWindow
    {
        private VideoPlayerContext Context => VideoPlayerContext.Instance;

        private MediaPlayerCommands _mediaPlayerCommands;

        PlayListManagerVM VM = new PlayListManagerVM();
        ListViewDragDropManager<MediaFileInfo> dragMgr = new ListViewDragDropManager<MediaFileInfo>();

        public PlayListManagerWindow()
        {
            DataContext = VM;

            InitializeComponent();

            Context.MediaPlayerCommands = _mediaPlayerCommands = new MediaPlayerCommands(this, enableKeyboardShortcuts: false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dragMgr.ListView = _listMediaFiles;
            dragMgr.ShowDragAdorner = true;
            dragMgr.DragAdornerOpacity = 0.5;

            VM.NotifyPropertyChangedAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _mediaPlayerCommands.Dispose();
            _mediaPlayerCommands = null;
        }

        private void _treePlayLists_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayList list = _treePlayLists.SelectedItem as PlayList;
            _bSortAscending = true;
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

        public void PreviousTrack_Executed(bool bResetPositionAndPlay)
        {
        }

        public bool NextTrack_CanExecute()
        {
            return false;
        }

        public void NextTrack_Executed(bool bResetPositionAndPlay)
        {
        }

        public void RemoveMediaFileAndSelectNext(MediaFileInfo info, bool bResetPositionAndPlay, bool bRemoveFromList)
        {
            throw new NotImplementedException();
        }

        #endregion

        private bool _bSortAscending = true;
        private void ButtonSort_Click(object sender, RoutedEventArgs e)
        {
            PlayList playList = _treePlayLists.SelectedItem as PlayList;
            if(playList != null && playList.MediaFiles.Count > 1)
            {
                List<MediaFileInfo> list = new List<MediaFileInfo>(playList.MediaFiles);
                list.Sort((f1, f2) => 
                { 
                    if(_bSortAscending)
                        return Path.GetFileNameWithoutExtension(f1.FileName).CompareTo(Path.GetFileNameWithoutExtension(f2.FileName)); 
                    else
                        return Path.GetFileNameWithoutExtension(f2.FileName).CompareTo(Path.GetFileNameWithoutExtension(f1.FileName));
                });

                playList.MediaFiles.Clear();
                playList.MediaFiles.AddRange(list);

                _bSortAscending = !_bSortAscending;
                _btnSort.IsChecked = _bSortAscending;
            }
        }

        private void FileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox txt)
            {
                if (txt.DataContext is MediaFileInfo info)
                {
                    try
                    {
                        string newFileName = txt.Text;
                        string newPath = Path.Combine(Path.GetDirectoryName(info.FileName), newFileName);
                        if (newPath != info.FileName)
                        {
                            File.Move(info.FileName, newPath);
                            info.FileName = newPath;
                        }
                    }
                    catch (Exception err)
                    {
                        txt.Text = info.FileName;
                        MessageBox.Show(err.ToString());
                    }
                }
            }
        }
    }
}
