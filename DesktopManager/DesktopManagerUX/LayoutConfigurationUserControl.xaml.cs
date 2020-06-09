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
using System.Windows.Desktop;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopManagerUX
{
    /// <summary>
    /// Interaction logic for LayoutConfigurationUserControl.xaml
    /// </summary>
    public partial class LayoutConfigurationUserControl : UserControl
    {
        private bool _isInitialized = false;

        public LayoutConfiguration LayoutConfiguration
        {
            get { return (LayoutConfiguration)this.GetValue(LayoutConfigurationProperty); }
            set { this.SetValue(LayoutConfigurationProperty, value); OnSelectedTabChanged(); }
        }

        public static readonly DependencyProperty LayoutConfigurationProperty = DependencyProperty.Register(
          nameof(LayoutConfiguration), typeof(LayoutConfiguration), typeof(LayoutConfigurationUserControl), new PropertyMetadata(LayoutConfigurationPropertyChanged));// new DisplayConfiguration()));

        private static void LayoutConfigurationPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is LayoutConfigurationUserControl This)
                This.OnSelectedTabChanged();
        }

        private void OnSelectedTabChanged()
        {
            if (LayoutConfiguration == null)
                return;

            if (LayoutConfiguration.SelectedMonitorInfo == null)
                LayoutConfiguration.SelectedMonitorInfo = AppContext.Configuration.Displays[0];

            int idx = LayoutConfiguration.SelectedMonitorInfo.Index;
            cmbDisplays.ItemsSource = AppContext.Configuration.Displays;
            cmbDisplays.SelectedIndex = idx;

            _isInitialized = true;

            RebuildAppsGrid(LayoutConfiguration.GridSize);
        }

        public LayoutConfigurationUserControl()
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

        private void cmbDisplays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LayoutConfiguration.SelectedMonitorInfo = cmbDisplays.SelectedItem as DisplayInfo;
        }

        private UIElement GetGridItem(int row, int col)
        {
            foreach (UIElement item in gridApps.Children)
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
                    if (GetGridItem(row, col) is AppChooserUserControl chooser)
                    {
                        if (!chooser.IsSelected)
                            continue;

                        //AppInfo app = chooser.SelectedApp;
                        //if (chooser.SelectedApp == null)
                        //    continue;

                        action(chooser, row / 2, col / 2);
                    }
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

            LayoutConfiguration layout = this.LayoutConfiguration; // AppContext.Configuration.Displays[0];

            double width = layout.CellSize.Width;
            double height = layout.CellSize.Height;
            double left = layout.SelectedMonitorInfo.Bounds.Left + col * width;
            double top = layout.SelectedMonitorInfo.Bounds.Top + row * height;

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
            AppContext.ViewModel.ReloadApps();
            AppContext.Configuration.SmartDisplaysUpdate();
            RebuildAppsGrid(this.LayoutConfiguration.GridSize);
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
                //AppContext.ViewModel.ReloadApps();

                this.LayoutConfiguration.UpdateApps(newGridSize);

                int rows = this.LayoutConfiguration.GridSize.Rows;
                int cols = this.LayoutConfiguration.GridSize.Cols;

                gridSizeSelector.SelectedSize = new GridSizeData(rows, cols);

                RebuildAppsGrid(gridApps, rows, cols, this.LayoutConfiguration, AppContext.ViewModel);
            }
            AppContext.Sync = false;
        }

        private static void RebuildAppsGrid(Grid grid, int rows, int cols, LayoutConfiguration config, ViewModel vm)
        {
            //for splitters
            int newRows = rows * 2 - 1; 
            int newCols = cols * 2 - 1;

            GridLength oneStar = new GridLength(1, GridUnitType.Star);
            GridLength splitterSize = new GridLength(5, GridUnitType.Pixel);

            grid.ColumnDefinitions.Clear();
            for (int col = 0; col < newCols; col++)
            {
                //every odd column for splitter
                GridLength width = (col % 2) == 0 ? oneStar : splitterSize;
                
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = width });
            }

            grid.RowDefinitions.Clear(); //all but first
            for (int row = 0; row < newRows; row++)
            {
                //every odd row for splitter
                GridLength height = (row % 2) == 0 ? oneStar : splitterSize;

                grid.RowDefinitions.Add(new RowDefinition() { Height = height });
            }

            foreach (UIElement ul in grid.Children)
            {
                if (ul is AppChooserUserControl app)
                    app.Dispose();
            }

            grid.Children.Clear();
            for (int row = 0; row < newRows; row++)
            {
                if ((row % 2) != 0) //create row splitter
                {
                    CreateGridSplitterH(grid, newRows, newCols, row, 0);
                }
                else
                {
                    for (int col = 0; col < newCols; col++)
                    {
                        if (row == 0 && (col % 2) != 0)
                        {
                            CreateGridSplitterV(grid, newRows, newCols, row, col);
                        }
                        else if((col % 2) == 0)
                        {
                            AppChooserUserControl ctrl = new AppChooserUserControl();
                            ctrl.DisplayConfiguration = config;
                            ctrl.Init(row / 2, col / 2);

                            Grid.SetColumn(ctrl, col);
                            Grid.SetRow(ctrl, row);
                            grid.Children.Add(ctrl);
                        }
                    }
                }
            }

        }

        private static void CreateGridSplitterH(Grid grid, int rows, int cols, int row, int col)
        {
            GridSplitter splitter = new GridSplitter()
            {
                Height = 5,
                Background = Brushes.DarkGray,
                ResizeDirection = GridResizeDirection.Rows,
                ResizeBehavior = GridResizeBehavior.PreviousAndNext,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top
            };

            Grid.SetRow(splitter, row);
            Grid.SetColumn(splitter, 0);
            Grid.SetColumnSpan(splitter, cols);

            grid.Children.Add(splitter);
        }

        private static void CreateGridSplitterV(Grid grid, int rows, int cols, int row, int col)
        {
            GridSplitter splitter = new GridSplitter()
            {
                Width = 5,
                Background = Brushes.DarkGray,
                ResizeDirection = GridResizeDirection.Columns,
                ResizeBehavior = GridResizeBehavior.PreviousAndNext,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Grid.SetRow(splitter, 0);
            Grid.SetColumn(splitter, col);
            Grid.SetRowSpan(splitter, rows);

            grid.Children.Add(splitter);
        }
    }
}
