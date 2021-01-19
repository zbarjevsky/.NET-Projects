using MkZ.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MkZ.MediaPlayer.Controls
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        //http://www.java2s.com/Tutorial/CSharp/0470__Windows-Presentation-Foundation/HostingaWindowsFormsPropertyGridinWPF.htm
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            propertyGrid.SelectedObject = VideoPlayerContext.Instance.Config;
            propertyGrid.PropertySort = PropertySort.NoSort;

            propertyGrid.ExpandGridItem("Configuration");
            propertyGrid.ExpandGridItem("Clock Configuration");
            propertyGrid.ExpandGridItem("Clock Font");
        }

        private static void ExpandGroup(PropertyGrid propertyGrid, string groupName)
        {
            GridItem root = propertyGrid.SelectedGridItem;
            //Get the parent
            while (root.Parent != null)
                root = root.Parent;

            if (root != null)
            {
                foreach (GridItem g in root.GridItems)
                {
                    if (g.GridItemType == GridItemType.Category && g.Label == groupName)
                    {
                        g.Expanded = true;
                        break;
                    }
                }
            }
        }
    }
}
