using MkZ.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace MkZ.WPF.PropertyGrid
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    ///http://www.java2s.com/Tutorial/CSharp/0470__Windows-Presentation-Foundation/HostingaWindowsFormsPropertyGridinWPF.htm
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private int _splitterPosition = 0;

        public static bool? ShowOptions(Window owner, object options, string title, double height, int firstColumnWidth = -1, params string[] expandNames)
        {
            OptionsWindow wnd = new OptionsWindow();
            wnd.Owner = owner;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.Height = height;
            wnd.Title = title;
            wnd.SetPropertiesObject(options, expandNames);
            wnd._splitterPosition = firstColumnWidth;

            return wnd.ShowDialog();
        }

        public static bool? ShowOptionsEx(Window owner, object options, string title, Action<Grid> initAction, int firstColumnWidth = -1, params string[] expandNames)
        {
            OptionsWindow wnd = new OptionsWindow();
            wnd.Owner = owner;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.Title = title;
            wnd.SetPropertiesObject(options, expandNames);
            wnd._splitterPosition = firstColumnWidth;

            initAction?.Invoke(wnd._gridMain);

            return wnd.ShowDialog();
        }

        public OptionsWindow()
        {
            InitializeComponent();
        }

        public void SetPropertiesObject(object o, params string [] expandNames)
        {
            _propertyGrid.SelectedObject = o;
            _propertyGrid.PropertySort = PropertySort.Categorized;// PropertySort.NoSort;

            foreach (string name in expandNames)
            {
                ExpandGridItem(_propertyGrid, name);
            }
        }

        public void ExpandAll()
        {
            _propertyGrid.ExpandAllGridItems();
        }

        public void MoveSplitterTo(int width)
        {
            _propertyGrid.MoveSplitterTo(width);
        }

        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void ExpandGridItem(System.Windows.Forms.PropertyGrid propertyGrid, string name)
        {
            GridItem root = propertyGrid.SelectedGridItem;
            //Get the parent
            while (root.Parent != null)
                root = root.Parent;

            ExpandGridItem(root, name);
        }

        private void ExpandGridItem(GridItem root, string name)
        {
            if (root != null)
            {
                foreach (GridItem g in root.GridItems)
                {
                    ExpandGridItem(g, name);
                    if (g.Label == name)
                        g.Expanded = true;
                }
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
                this.Close();
        }

        private void PropertyGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_splitterPosition > 0)
                MoveSplitterTo(_splitterPosition);
        }
    }
}
