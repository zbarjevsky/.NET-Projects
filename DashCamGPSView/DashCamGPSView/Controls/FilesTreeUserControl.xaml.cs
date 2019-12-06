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
            
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem item)
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
        public string GroupName { get; set; }

        public ObservableCollection<VideoFile> Members { get; set; } = new ObservableCollection<VideoFile>();

        public VideoGroup(List<DashCamFileInfo> group)
        {
            if (group != null && group.Count > 0)
            {
                foreach (DashCamFileInfo info in group)
                {
                    int count = info.GpsInfo != null ? info.GpsInfo.Count : 0;
                    VideoFile v = new VideoFile() { FileName = info.FrontFileName, GpsPointsCount = count };
                    Members.Add(v);
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
        public string FileName { get; set; }

        public int GpsPointsCount { get; set; }
    }
}