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
        public Action<string> TreeItemDoubleClickAction = (fileName) => { };
        public Action OpenFileAction = () => { };
        public Action ExportGPSAction = () => { };

        public FilesTreeUserControl()
        {
            InitializeComponent();

            //inser empty group - as prompt
            treeFiles.ItemsSource = new List<VideoGroup>() { new VideoGroup(null, "") };
        }

        public void LoadTree(DashCamFileTree tree, string selectedFileName)
        {
            List<VideoGroup> fileGroups = new List<VideoGroup>();

            foreach (List<DashCamFileInfo> group in tree.fileGroups)
            {
                fileGroups.Add(new VideoGroup(group, selectedFileName));
            }
            
            treeFiles.ItemsSource = fileGroups;
        }

        internal void SelectFile(string fileName)
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
                        childItem.IsSelected = (string.Compare(fileName, v.FileName, true) == 0);
                        if (childItem.IsSelected)
                            childItem.BringIntoView();
                    }
                }
            }
        }

        internal string FindNextFile(string fileName)
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
                            return group.Members[i + 1].FileName;
                    }
                }
            }
            return null;
        }

        internal string FindPrevFile(string fileName)
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
                            return group.Members[i - 1].FileName;
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
                    TreeItemDoubleClickAction(v.FileName);
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
                    if (g.Members.Count == 0)
                        ExportGPSAction();
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
                    TreeItemDoubleClickAction(v.FileName);
                }
            }
        }
    }

    public class VideoGroup
    {
        public bool IsSelected { get; set; } = false;

        public string GroupName { get; set; }

        public ObservableCollection<VideoFile> Members { get; set; } = new ObservableCollection<VideoFile>();

        public VideoGroup(List<DashCamFileInfo> group, string selectedFileName)
        {
            if (group != null && group.Count > 0)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    Members.Add(new VideoFile(i, group[i], selectedFileName));
                }

                string dir = Path.GetDirectoryName(group[0].FrontFileName);
                GroupName = group[0].FileDate.ToString("yyyy/MM/dd HH:mm:ss") + ", " + dir;
            }
            else
            {
                GroupName = "Please select file";
            }
        }
    }

    public class VideoFile
    {
        private DashCamFileInfo _dashCamFileInfo = null;

        public bool IsSelected { get; set; } = false;

        public string FileName { get { return _dashCamFileInfo.FrontFileName; } }

        public string FileNameForDisplay { get; private set; }

        public string Description { get; private set; } = "";

        public VideoFile(int indexInGroup, DashCamFileInfo info, string selectedFileName)
        {
            _dashCamFileInfo = info;
            IsSelected = (FileName == selectedFileName);
            FileNameForDisplay = string.Format("{0:000}. {1}", indexInGroup+1, Path.GetFileNameWithoutExtension(FileName));

            FileInfo fi = new FileInfo(FileName);
            Description = string.Format(" ({0:###,###.0} KB)", fi.Length / 1024.0);
        }
    }
}