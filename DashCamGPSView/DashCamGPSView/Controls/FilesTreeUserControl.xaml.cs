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

using DashCamGPSView.Tools;

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for FilesTreeUserControl.xaml
    /// </summary>
    public partial class FilesTreeUserControl : UserControl
    {
        public Action<VideoFile> TreeItemDoubleClickAction = (video) => { };
        public Action OpenFileAction = () => { };
        public Action<List<DashCamFileInfo>> ExportGPSAction = (infos) => { };
        public Action<ObservableCollection<VideoFile>> DeleteRecordingsAction = (videos) => { };
        public Action<ObservableCollection<VideoFile>> FileTreeUpdatedAction = (deletedVideos) => { };

        private ObservableCollection<VideoGroup> _itemsSource = new ObservableCollection<VideoGroup>();

        public FilesTreeUserControl()
        {
            InitializeComponent();

            //inser empty group - as prompt
            treeFiles.ItemsSource = new List<VideoGroup>() { VideoGroup.Empty() };
            
            this.DeleteRecordingsAction = (videos) =>
            {
                if (MessageBoxResult.OK != MessageBox.Show(
                    "Confirm Deletion of " + videos.Count + " Recordings", "Delete Recordings",
                    MessageBoxButton.OKCancel, MessageBoxImage.Exclamation))
                    return;

                foreach (VideoFile v in videos)
                {
                    DeleteVideoFile(v);
                }
                FileTreeUpdatedAction(videos);
            };
        }

        public VideoFile LoadTree(DashCamFileTree tree, string selectFile)
        {
            _itemsSource.Clear();
            foreach (List<DashCamFileInfo> group in tree.fileGroups)
            {
                _itemsSource.Add(new VideoGroup(group));
            }
            
            treeFiles.ItemsSource = _itemsSource;
            treeFiles.UpdateLayout();
            return SelectFile(selectFile);
        }

        internal VideoFile SelectFile(string fileName)
        {
            VideoFile v = null;
            foreach (VideoGroup group in treeFiles.Items)
            {
                var tvi = treeFiles.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                foreach (VideoFile videoFile in tvi.Items)
                {
                    TreeViewItem childItem = tvi.ItemContainerGenerator.ContainerFromItem(videoFile) as TreeViewItem;
                    if (childItem != null)
                    {
                        childItem.IsSelected = videoFile.HasFileName(fileName);
                        if (childItem.IsSelected)
                        {
                            v = videoFile;
                            childItem.BringIntoView();
                        }
                    }
                }
            }
            return v;
        }

        internal TreeViewItem FindFile(string fileName)
        {
            foreach (VideoGroup group in treeFiles.Items)
            {
                var tvi = treeFiles.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                foreach (var subItem in tvi.Items)
                {
                    TreeViewItem childItem = tvi.ItemContainerGenerator.ContainerFromItem(subItem) as TreeViewItem;
                    if (childItem != null)
                    {
                        VideoFile v = childItem.DataContext as VideoFile;
                        if (string.Compare(fileName, v.FileName, true) == 0)
                            return childItem;
                    }
                }
            }
            return null;
        }

        internal void DeleteVideoFile(VideoFile file)
        {
            foreach (VideoGroup g in _itemsSource)
            {
                foreach (VideoFile v in g.Members)
                {
                    if(v.FileName == file.FileName)
                    {
                        v.DeleteRecording();
                        g.Members.Remove(v);
                        break;
                    }
                }
                //remove group if all files were removed
                if (g.Members.Count == 0)
                {
                    _itemsSource.Remove(g);
                    break;
                }
            }
        }

        internal VideoFile FindNextFile(string fileName)
        {
            foreach (VideoGroup group in treeFiles.Items)
            {
                var tvi = treeFiles.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                for (int i = 0; i < group.Members.Count; i++)
                {
                    VideoFile v = group.Members[i];
                    if (string.Compare(fileName, v.FileName, true) == 0)
                    {
                        if (i + 1 < group.Members.Count) //has next
                            return group.Members[i + 1];
                    }
                }
            }
            return null;
        }

        internal VideoFile FindPrevFile(string fileName)
        {
            foreach (VideoGroup group in treeFiles.Items)
            {
                var tvi = treeFiles.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                for (int i = 0; i < group.Members.Count; i++)
                {
                    VideoFile v = group.Members[i];
                    if (string.Compare(fileName, v.FileName, true) == 0)
                    {
                        if (i - 1 >= 0) //has prev
                            return group.Members[i - 1];
                    }
                }
            }
            return null;
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem item)
            {
                if (item.DataContext is VideoFile v)
                {
                    TreeItemDoubleClickAction(v);
                }
                else if (item.DataContext is VideoGroup g)
                {
                    if (g.Members.Count == 0)
                        OpenFileAction();
                }
            }
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem item)
            {
                item.IsSelected = true;
            }
        }

        private void GroupMenu_Export_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                if (item.DataContext is VideoGroup g)
                {
                    if (g.Members.Count > 0)
                    {
                        ExportGPSAction(g.InfoList);
                    }
                }
                else if (item.DataContext is VideoFile v)
                {
                    ExportGPSAction(new List<DashCamFileInfo>() { v._dashCamFileInfo });
                }
            }
        }

        private void GroupMenu_Open_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                if (item.DataContext is VideoGroup g)
                {
                    OpenFileAction();
                }
            }
        }

        private void FileMenu_Play_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                if (item.DataContext is VideoFile v)
                {
                    TreeItemDoubleClickAction(v);
                }
            }
        }

        private void GroupMenu_OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                if (item.DataContext is VideoFile v)
                {
                    OpenFolderWithSelectedFile(v.FileName);
                }
                else if (item.DataContext is VideoGroup g)
                {
                    if (g.Members.Count > 0)
                    {
                        OpenFolderWithSelectedFile(g.Members[0].FileName);
                    }
                    else
                    {
                        _itemsSource.Remove(g);
                    }
                }
            }
        }

        private void OpenFolderWithSelectedFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filePath + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void GroupMenu_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                if (item.DataContext is VideoFile v)
                {
                    DeleteRecordingsAction(new ObservableCollection<VideoFile>() { v });
                }
                else if (item.DataContext is VideoGroup g)
                {
                    if (g.Members.Count > 0)
                    {
                        DeleteRecordingsAction(new ObservableCollection<VideoFile>(g.Members));
                    }
                    else
                    {
                        _itemsSource.Remove(g);
                    }
                }
            }
        }
    }

    public class VideoGroup
    {
        public bool IsSelected { get; set; } = false;

        public string GroupName { get; set; }

        public ObservableCollection<VideoFile> Members { get; set; } = new ObservableCollection<VideoFile>();

        public static VideoGroup Empty()
        {
            return new VideoGroup();
        }

        public VideoGroup(List<DashCamFileInfo> group = null)
        {
            if (group != null && group.Count > 0)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    Members.Add(new VideoFile(i, group[i]));
                }

                string dir = Path.GetDirectoryName(group[0].FileNameFront);
                GroupName = group[0].FileDateStart.ToString("yyyy/MM/dd HH:mm:ss") + ", " + dir;
            }
            else
            {
                GroupName = "Please select file";
            }
        }

        public List<DashCamFileInfo> InfoList
        {
            get
            {
                List<DashCamFileInfo> infos = new List<DashCamFileInfo>();
                foreach (VideoFile v in Members)
                    infos.Add(v._dashCamFileInfo);
                return infos;
            }
        }
    }

    public class VideoFile
    {
        public DashCamFileInfo _dashCamFileInfo { get; } = null;

        public bool IsSelected { get; set; } = false;

        public string FileName { get { return _dashCamFileInfo.FileNameFront; } }

        public FileType FileType { get => _dashCamFileInfo.FileType; }

        public string FileNameForDisplay { get; private set; }

        public string Description { get; private set; } = "";

        public VideoFile(int indexInGroup, DashCamFileInfo info)
        {
            _dashCamFileInfo = info;
            IsSelected = false;
            FileNameForDisplay = string.Format("{0:000}. {1}", indexInGroup+1, Path.GetFileNameWithoutExtension(FileName));

            FileInfo fi1 = new FileInfo(info.FileNameFront);

            TimeSpan duration = info.FileDateEnd - info.FileDateStart;
            duration += TimeSpan.FromSeconds(1); //correct for last second
            string sDuration = duration.TotalSeconds.ToString("0");
            if (duration.TotalSeconds > 1000)
                sDuration = duration.ToString();

            string cameras = "FR";
            long size = fi1.Length;
            if (File.Exists(info.FileNameInside))
            {
                FileInfo fi2 = new FileInfo(info.FileNameInside);
                cameras += "+IN";
                size += fi2.Length; 
            }
            if (File.Exists(info.FileNameRear))
            {
                FileInfo fi3 = new FileInfo(info.FileNameRear);
                cameras += "+RR";
                size += fi3.Length;
            }

            Description = string.Format(" ({0}, {1:###,###.0} MB, {2} s)", 
                cameras, size / (1024.0*1024.0), sDuration);
        }

        internal bool HasFileName(string fileName)
        {
            if (string.Compare(fileName, _dashCamFileInfo.FileNameFront, true) == 0)
                return true;
            if (string.Compare(fileName, _dashCamFileInfo.FileNameRear, true) == 0)
                return true;
            if (string.Compare(fileName, _dashCamFileInfo.FileNameInside, true) == 0)
                return true;

            return false;
        }

        internal void DeleteRecording()
        {
            _dashCamFileInfo.DeleteRecording();
        }
    }
}