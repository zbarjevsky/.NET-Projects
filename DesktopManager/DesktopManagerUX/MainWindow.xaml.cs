using DesktopManagerUX.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopManagerUX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _VM;

        public MainWindow()
        {
            _VM = new ViewModel(this);

            InitializeComponent();

            this.DataContext = _VM;

            cmbDisplays.ItemsSource = Logic.GetDisplays();
            cmbDisplays.SelectedIndex = 0;

            foreach (ComboBoxItem item in cmbGridSize.Items)
            {
                string txtSize = item.Content.ToString();
                if (txtSize == Properties.Settings.Default.SelectedGridSize)
                    cmbGridSize.SelectedItem = item;
            }

            RebuildAppsGrid();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            WpfScreen screen = WpfScreen.GetScreenFrom(this);
            Rect bounds = screen.WorkingArea;
            if (this.ActualWidth > bounds.Width || this.ActualHeight > bounds.Height)
            {
                //this.Width = bounds.Width / 2;
                //this.Height = bounds.Height / 2;
            }
        }

        private void cmbGridSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RebuildAppsGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            DisplayInfo itm = cmbDisplays.SelectedItem as DisplayInfo;

            int rows = _VM.AppChoosers.GetLength(0);
            int cols = _VM.AppChoosers.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Apply(row, col, itm.Bounds);
                }
            }

            this.Activate();
        }

        private void Apply(int row, int col, Rect bounds)
        {
            AppInfo app = _VM.AppChoosers[row, col].SelectedApp;
            if (app == null)
                return;

            int rows = _VM.AppChoosers.GetLength(0);
            int cols = _VM.AppChoosers.GetLength(1);

            double width = 3 + bounds.Width / cols;
            double height = 3 + bounds.Height / rows;

            double left = bounds.Left + col * width;
            double top = bounds.Top + row * height;

            if (col == 0)
                left += 3;
            if (col == cols - 1)
                left -= 7;

            Logic.MoveWindow(app.Process, left, top, width, height);
            Logic.SettingSave(app.Name, row, col);
        }

        private void RebuildAppsGrid()
        {
            if (gridApps == null)
                return;

            string txtSize = (cmbGridSize.SelectedItem as ComboBoxItem).Content.ToString();
            Properties.Settings.Default.SelectedGridSize = txtSize;

            int rows = int.Parse(txtSize.Substring(0, 1));
            int cols = int.Parse(txtSize.Substring(2, 1));
            RebuildAppsGrid(gridApps, rows, cols, _VM);
        }

        private static void RebuildAppsGrid(Grid grid, int rows, int cols, ViewModel vm)
        {
            grid.ColumnDefinitions.Clear();
            for (int col = 0; col < cols; col++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            grid.RowDefinitions.Clear(); //all but first
            for (int row = 0; row < rows; row++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            vm.AppChoosers = new AppChooserUserControl[rows, cols];

            grid.Children.Clear();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    AppChooserUserControl ctrl = new AppChooserUserControl();
                    ctrl.Init(vm, row, col);
                    vm.AppChoosers[row, col] = ctrl;

                    Grid.SetRow(ctrl, row);
                    Grid.SetColumn(ctrl, col);
                    grid.Children.Add(ctrl);
                }
            }
        }
    }
}
