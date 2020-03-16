using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Threading;
using System.Windows.Threading;

namespace DesktopManagerUX
{
    /// <summary>
    /// Interaction logic for AppChooserUserControl.xaml
    /// </summary>
    public partial class AppChooserUserControl : UserControl
    {
        private DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Background);

        public int Row { get; private set; }
        public int Col { get; private set; }

        public AppChooserUserControl()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(3);
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            RefreshSnapshot();
        }

        public void Init(int row, int col)
        {
            string selectedTitle = AppContext.Configuration[row, col].Title;

            this.DataContext = AppContext.ViewModel;
            cmbApps.ItemsSource = AppContext.ViewModel.Apps;
            Row = row;
            Col = col;

            int idx = AppInfo.FindApp(selectedTitle, AppContext.ViewModel.Apps);
            if(idx == 0) //not found, but exists in configuration
            {
                if (File.Exists(AppContext.Configuration[row, col].ProcessPath))
                {
                    AppContext.ViewModel.Apps.Add(AppContext.Configuration[row, col]);
                    idx = AppContext.ViewModel.Apps.Count - 1;
                }
            }
            cmbApps.SelectedIndex = idx;

            borderMain.BorderThickness = ThicknessFromRowCol(row, col);

            //save
            AppContext.Configuration[row, col] = SelectedApp;
            
            _timer.Start();
        }

        public bool HasSelection { get { return cmbApps.SelectedIndex > 0 && cmbApps.SelectedIndex < AppContext.ViewModel.Apps.Count; } }

        public AppInfo SelectedApp
        {
            get { if(HasSelection) return AppContext.ViewModel.Apps[cmbApps.SelectedIndex]; return null; }
        }

        private void cmbApps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshSnapshot();
        }

        private void RefreshSnapshot()
        {
            if (HasSelection)
            {
                SelectedApp.Refresh();

                imagePreview.Source = Logic.CaptureApplication(SelectedApp.HWND, AppContext.ViewModel.DPI);
                if (imagePreview.Source != null)
                    txtInfo.Content = "Current Size: " + imagePreview.Source.Width + "x" + imagePreview.Source.Height;
                else
                    txtInfo.Content = "Select Another Application";

                btnRun.ToolTip = "Open " + SelectedApp.ProcessName;
                btnRun.IsEnabled = File.Exists(SelectedApp.ProcessPath);
                AppContext.Configuration[Row, Col] = SelectedApp;
                AppContext.Configuration.Save();
            }
            else
            {
                btnRun.ToolTip = "";
                btnRun.IsEnabled = false;
                imagePreview.Source = null;
                txtInfo.Content = "? Select Window ?";
                AppContext.Configuration[Row, Col] = AppInfo.GetEmptyAppInfo();
                AppContext.Configuration.Save();
            }
        }

        private void RunApp_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(SelectedApp.ProcessPath))
            {
                Process p = Process.Start(SelectedApp.ProcessPath);
                for (int i = 0; i < 30; i++)
                {
                    if(p.MainWindowHandle == IntPtr.Zero)
                        Thread.Sleep(330);
                } 
                p.WaitForInputIdle(10000);
                Thread.Sleep(330);
                AppContext.ViewModel.ReloadApps();
                Init(Row, Col);
            }
        }

        private Thickness ThicknessFromRowCol(int row, int col)
        {
            int rows = AppContext.Configuration.GridSize.Rows;
            int cols = AppContext.Configuration.GridSize.Cols;

            const double thin = 1, thick = 3;

            double left = (col == 0) ? thick: thin;
            double right = (col == cols-1) ? thick : thin;
            double top = (row == 0) ? thick : thin;
            double bottom = (row == rows - 1) ? thick: thin;

            return new Thickness(left, top, right, bottom);
        }
    }
}
