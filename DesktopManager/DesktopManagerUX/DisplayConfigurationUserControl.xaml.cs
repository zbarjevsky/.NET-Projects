using DesktopManagerUX.Utils;
using MZ.Tools;
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
          nameof(DisplayConfiguration), typeof(DisplayConfiguration), typeof(DisplayConfigurationUserControl), new PropertyMetadata(DisplayConfigurationPropertyChanged));// new DisplayConfiguration()));

        private static void DisplayConfigurationPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is DisplayConfigurationUserControl This)
                This.OnDisplayConfigurationChanged();
        }

        private void OnDisplayConfigurationChanged()
        {
            if (DisplayConfiguration == null)
                return;

            string selectedGridSize = DisplayConfiguration.GetSelectedGridSizeText();
            //cmbGridSize.ItemsSource = GridSizeData.GetAllSizes();
            //foreach (string txtSize in cmbGridSize.Items)
            //{
            //    if (txtSize.EndsWith(selectedGridSize))
            //        cmbGridSize.SelectedItem = txtSize;
            //}


            _isInitialized = true;

            RebuildAppsGrid(DisplayConfiguration.GridSize);
        }

        public DisplayConfigurationUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            gridSizeSelector.OnSelectedSizeChangedAction = (size) => { RebuildAppsGrid(size); };
        }

        //private void cmbGridSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string txtSize = (cmbGridSize.SelectedItem as string);
        //    RebuildAppsGrid(GridSizeData.Parse(txtSize));
        //}

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

            DisplayConfiguration activeDisplay = this.DisplayConfiguration; // AppContext.Configuration.Displays[0];

            double width = activeDisplay.CellSize.Width;
            double height = activeDisplay.CellSize.Height;
            double left = activeDisplay.MonitorInfo.Bounds.Left + col * width;
            double top = activeDisplay.MonitorInfo.Bounds.Top + row * height;

            User32.RECT border = DesktopWindowManager.GetWindowBorderSize(chooser.SelectedApp.HWND);

            left -= border.left;
            top -= border.top;
            width += border.left + border.right;
            height += border.top + border.bottom;

            Logic.MoveWindow(chooser.SelectedApp.HWND, left, top, width, height);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AppContext.Configuration.UpdateDisplays();
            RebuildAppsGrid(this.DisplayConfiguration.GridSize);
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

                this.DisplayConfiguration.UpdateApps(newGridSize);

                int rows = this.DisplayConfiguration.GridSize.Rows;
                int cols = this.DisplayConfiguration.GridSize.Cols;

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
