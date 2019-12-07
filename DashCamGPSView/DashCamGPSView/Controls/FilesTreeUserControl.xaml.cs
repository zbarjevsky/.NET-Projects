using DashCamGPSView.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for FilesTreeUserControl.xaml
    /// </summary>
    public partial class FilesTreeUserControl : UserControl
    {
        public Action<string> TreeItemDoubleClickAction = (fileName) => { };
        public Action OpenFileAction = () => { };

        public FilesTreeUserControl()
        {
            InitializeComponent();

            //inser empty group - as prompt
            treeFiles.ItemsSource = new List<VideoGroup>() { new VideoGroup(null) };
        }

        public void LoadTree(DashCamFileTree tree, string selectedFileName)
        {
            List<VideoGroup> fileGroups = new List<VideoGroup>();

            foreach (List<DashCamFileInfo> group in tree.fileGroups)
            {
                fileGroups.Add(new VideoGroup(group));
            }
            
            treeFiles.ItemsSource = fileGroups;
        }

        internal void SelectFile(string fileName)
        {
            foreach (VideoGroup group in treeFiles.Items)
            {
                var tvi = treeFiles.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                foreach (VideoFile v in group.Members)
                {
                    v.IsSelected = (string.Compare(fileName, v.FileName, true) == 0);
                    if (v.IsSelected)
                        tvi.BringIntoView();
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
    }

    public class VideoGroup
    {
        public bool IsSelected { get; set; } = false;

        public string GroupName { get; set; }

        public ObservableCollection<VideoFile> Members { get; set; } = new ObservableCollection<VideoFile>();

        public VideoGroup(List<DashCamFileInfo> group)
        {
            if (group != null && group.Count > 0)
            {
                foreach (DashCamFileInfo info in group)
                {
                    Members.Add(new VideoFile(info));
                }

                GroupName = group[0].FileDate.ToString() + ", (Count: " + Members.Count + ")";
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

        public int GpsPointsCount 
        { 
            get 
            {
                return 0; // _dashCamFileInfo.GpsInfo != null ? _dashCamFileInfo.GpsInfo.Count : 0;
            }
        }

        public VideoFile(DashCamFileInfo info)
        {
            _dashCamFileInfo = info;
        }
    }
}