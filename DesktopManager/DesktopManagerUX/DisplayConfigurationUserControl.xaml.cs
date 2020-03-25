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
    /// Interaction logic for DisplayConfigurationUserControl.xaml
    /// </summary>
    public partial class DisplayConfigurationUserControl : UserControl
    {
        private bool _isInitialized = false;

        public DisplayConfiguration DisplayConfiguration
        {
            get { return (DisplayConfiguration)this.GetValue(DisplayConfigurationProperty); }
            set { this.SetValue(DisplayConfigurationProperty, value); OnDisplayConfigurationChanged(); }
        }

        public static readonly DependencyProperty DisplayConfigurationProperty = DependencyProperty.Register(
          nameof(DisplayConfiguration), typeof(DisplayConfiguration), typeof(DisplayConfigurationUserControl), new PropertyMetadata(null));// new DisplayConfiguration()));

        private void OnDisplayConfigurationChanged()
        {
            string selectedGridSize = DisplayConfiguration.GetSelectedGridSizeText();
            cmbGridSize.ItemsSource = GridSizeData.GetAllSizes();
            foreach (string txtSize in cmbGridSize.Items)
            {
                if (txtSize.EndsWith(selectedGridSize))
                    cmbGridSize.SelectedItem = txtSize;
            }

            gridSizeSelector.OnSelectedSizeChangedAction = (size) => { RebuildAppsGrid(size); };

            _isInitialized = true;

            string txt = (cmbGridSize.SelectedItem as string);
            RebuildAppsGrid(GridSizeData.Parse(txt));
        }

        public DisplayConfigurationUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnDisplayConfigurationChanged();
        }

        private void cmbGridSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string txtSize = (cmbGridSize.SelectedItem as string);
            RebuildAppsGrid(GridSizeData.Parse(txtSize));
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

        private void Apply_ActionOnAll(Action<AppChooserUserControl, int, int> action)
        {
            int rows = gridApps.RowDefinitions.Count;
            int cols = gridApps.ColumnDefinitions.Count;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    AppChooserUserControl chooser = GetAppChooser(row, col);
                    if (!chooser.IsSelected)
                        continue;

                    //AppInfo app = chooser.SelectedApp;
                    //if (chooser.SelectedApp == null)
                    //    continue;

                    action(chooser, row, col);
                }
            }
            AppContext.ViewModel.ActivateMainWindow();

        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Apply_ActionOnAll((chooser, row, col) => Apply(chooser, row, col));
        }

        private void Apply(AppChooserUserControl chooser, int row, int col)
        {
            if (chooser.SelectedApp == null || !chooser.SelectedApp.IsActive)
                return;

            Rect bounds = AppContext.Configuration.Displays[0].SelectedDisplayInfo.Bounds;

            double width = 3 + AppContext.Configuration.Displays[0].CellSize.Width;
            double height = 3 + AppContext.Configuration.Displays[0].CellSize.Height;

            double left = bounds.Left + col * width;
            double top = bounds.Top + row * height;

            int cols = AppContext.Configuration.Displays[0].GridSize.Cols;
            if (col == 0) //first column
                left += 3;
            else if (col == cols - 1) //last column
                left -= 7;

            Logic.MoveWindow(chooser.SelectedApp.HWND, left, top, width, height);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            string txtSize = (cmbGridSize.SelectedItem as string);
            RebuildAppsGrid(GridSizeData.Parse(txtSize));
            this.Cursor = Cursors.Arrow;
        }

        private void CloseSelected_Click(object sender, RoutedEventArgs e)
        {
            Apply_ActionOnAll((chooser, row, col) =>
            {
                if (!chooser.SelectedApp.IsActive)
                    return;

                User32.CloseWindow(chooser.SelectedApp.HWND);
            });
        }

        private void OpenSelected_Click(object sender, RoutedEventArgs e)
        {
            Apply_ActionOnAll((chooser, row, col) => AppContext.Logic.RunApp(chooser.SelectedApp));
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Apply_ActionOnAll((chooser, row, col) => User32.MinimizeWindow(chooser.SelectedApp.HWND));
        }

        private void RebuildAppsGrid(GridSizeData newGridSize)
        {
            if (!_isInitialized)
                return;

            AppContext.Sync = true;
            {
                AppContext.ViewModel.ReloadApps();

                AppContext.Configuration.Displays[0].UpdateApps(newGridSize);

                int rows = AppContext.Configuration.Displays[0].GridSize.Rows;
                int cols = AppContext.Configuration.Displays[0].GridSize.Cols;

                gridSizeSelector.SelectedSize = new GridSizeData(rows, cols);

                RebuildAppsGrid(gridApps, rows, cols, this.DisplayConfiguration, AppContext.ViewModel);
            }
            AppContext.Sync = false;
        }

        private static void RebuildAppsGrid(Grid grid, int rows, int cols, DisplayConfiguration config, ViewModel vm)
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
                if (ul is AppChooserUserControl app)
                    app.Dispose();
            }

            grid.Children.Clear();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    AppChooserUserControl ctrl = new AppChooserUserControl();
                    ctrl.DisplayConfiguration = config;
                    ctrl.Init(row, col);

                    Grid.SetRow(ctrl, row);
                    Grid.SetColumn(ctrl, col);
                    grid.Children.Add(ctrl);
                }
            }
        }
    }
}
