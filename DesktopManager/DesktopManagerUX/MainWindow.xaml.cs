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
        private bool _isInitialized = false;

        public MainWindow()
        {
            AppContext.Init(this);

            InitializeComponent();

            this.DataContext = AppContext.ViewModel;

            cmbDisplays.ItemsSource = Logic.GetDisplays();
            cmbDisplays.SelectedIndex = AppContext.Configuration.SelectedDisplayInfo.Index;

            string selectedGridSize = AppContext.Configuration.GetSelectedGridSizeText();
            cmbGridSize.ItemsSource = GridSizeData.GetAllSizes();
            foreach (string txtSize in cmbGridSize.Items)
            {
                if (txtSize.EndsWith(selectedGridSize))
                    cmbGridSize.SelectedItem = txtSize;
            }

            _isInitialized = true;

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
            AppContext.Configuration.SelectedDisplayInfo = cmbDisplays.SelectedItem as DisplayInfo;
            AppContext.Configuration.Save();
            Properties.Settings.Default.Save();
        }

        private AppChooserUserControl GetAppChooser(int row, int col)
        {
            foreach (AppChooserUserControl item in gridApps.Children)
            {
                if (Grid.GetRow(item) == row && Grid.GetColumn(item) == col)
                    return item;
            }
            return null;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            AppContext.Configuration.SelectedDisplayInfo = cmbDisplays.SelectedItem as DisplayInfo;

            int rows = AppContext.Configuration.GridSize.Rows;
            int cols = AppContext.Configuration.GridSize.Cols;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Apply(row, col, AppContext.Configuration.SelectedDisplayInfo.Bounds);
                }
            }

            this.Activate();
        }

        private void Apply(int row, int col, Rect bounds)
        {
            AppInfo app = GetAppChooser(row, col).SelectedApp;
            if (app == null)
                return;

            int rows = AppContext.Configuration.GridSize.Rows;
            int cols = AppContext.Configuration.GridSize.Cols;

            double width = 3 + bounds.Width / cols;
            double height = 3 + bounds.Height / rows;

            double left = bounds.Left + col * width;
            double top = bounds.Top + row * height;

            if (col == 0)
                left += 3;
            if (col == cols - 1)
                left -= 7;

            Logic.MoveWindow(app.HWND, left, top, width, height);
        }

        private void RebuildAppsGrid()
        {
            if (!_isInitialized)
                return;

            AppContext.Sync = true;
            {
                AppContext.ViewModel.ReloadApps();

                string txtSize = (cmbGridSize.SelectedItem as string);
                AppContext.Configuration.SetSelectedgridSizeText(txtSize);

                int rows = AppContext.Configuration.GridSize.Rows;
                int cols = AppContext.Configuration.GridSize.Cols;

                RebuildAppsGrid(gridApps, rows, cols, AppContext.ViewModel);
            }
            AppContext.Sync = false;
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

            foreach (UIElement ul in grid.Children)
            {
                if(ul is AppChooserUserControl app)
                    app.Dispose();
            }

            grid.Children.Clear();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    AppChooserUserControl ctrl = new AppChooserUserControl();
                    ctrl.Init(row, col);
                    
                    Grid.SetRow(ctrl, row);
                    Grid.SetColumn(ctrl, col);
                    grid.Children.Add(ctrl);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            RebuildAppsGrid();
            this.Cursor = Cursors.Arrow;
        }

        private void CloseSelected_Click(object sender, RoutedEventArgs e)
        {
            int rows = gridApps.RowDefinitions.Count;
            int cols = gridApps.ColumnDefinitions.Count;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    AppInfo app = GetAppChooser(row, col).SelectedApp;
                    if (app == null)
                        continue;

                    User32.CloseWindow(app.HWND);
                }
            }
            this.Activate();
        }

        private void OpenSelected_Click(object sender, RoutedEventArgs e)
        {
            int rows = gridApps.RowDefinitions.Count;
            int cols = gridApps.ColumnDefinitions.Count;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    AppInfo app = GetAppChooser(row, col).SelectedApp;
                    if (app == null)
                        continue;

                    AppContext.Logic.RunApp(app);
                }
            }
            this.Activate();
        }
    }
}
