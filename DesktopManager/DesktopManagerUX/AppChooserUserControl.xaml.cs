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
using DesktopManagerUX.Utils;

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

        public bool IsSelected { get { return chkSelected.IsChecked.Value; } set { chkSelected.IsChecked = value; } }

        public DisplayConfiguration DisplayConfiguration
        {
            get { return (DisplayConfiguration)this.GetValue(DisplayConfigurationProperty); }
            set { this.SetValue(DisplayConfigurationProperty, value); }
        }
        public static readonly DependencyProperty DisplayConfigurationProperty = DependencyProperty.Register(
          nameof(DisplayConfiguration), typeof(DisplayConfiguration), typeof(AppChooserUserControl), new PropertyMetadata(new DisplayConfiguration()));

        public AppChooserUserControl()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(3);
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            RefreshSnapshot(false);
        }

        public void Init(int row, int col)
        {
            AppInfo savedInfo = DisplayConfiguration[row, col];

            this.DataContext = AppContext.ViewModel;
            cmbApps.ItemsSource = AppContext.ViewModel.Apps;
            
            Row = row;
            Col = col;

            RestoreSelected(savedInfo);

            borderMain.BorderThickness = ThicknessFromRowCol(row, col);

            //save
            DisplayConfiguration[row, col] = cmbApps.SelectedItem as AppInfo;
            
            _timer.Start();
        }

        public void Dispose()
        {
            if(_timer != null)
            {
                _timer.Stop();
            }
            _timer = null;
            Row = -1;
            Col = -1;

            this.DataContext = null;
            cmbApps.ItemsSource = null;
            txtInfo.Content = "<<DISPOSED>>";
        }

        public void UpdateInfo()
        {
            txtInfo.Content = "Expected: "+ DisplayConfiguration.CellSize.Width + "x" + DisplayConfiguration.CellSize.Height + ", ";
            if (imagePreview.Source != null)
                txtInfo.Content += "Snapshot Size: " + imagePreview.Source.Width + "x" + imagePreview.Source.Height;
            else
                txtInfo.Content += "No Snaphot";
        }

        public bool HasSelection { get { return cmbApps.SelectedIndex > 0 && cmbApps.SelectedIndex < AppContext.ViewModel.Apps.Count; } }

        private void RestoreSelected(AppInfo savedInfo)
        {
            int idx = AppInfo.FindApp(savedInfo.Title, AppContext.ViewModel.Apps);
            if (idx == 0) //not found, but exists in configuration
            {
                if (File.Exists(savedInfo.ProcessPath))
                {
                    AppContext.ViewModel.Apps.Add(savedInfo);
                    idx = AppContext.ViewModel.Apps.Count - 1;
                }
            }
            cmbApps.SelectedIndex = idx;
        }

        private AppInfo FindAppInCombo(AppInfo a)
        {
            foreach (AppInfo item in cmbApps.Items)
            {
                if (a.Title == item.Title)
                    return item;
            }
            return AppInfo.GetEmptyAppInfo();
        }

        public AppInfo SelectedApp
        {
            get 
            { 
                if(HasSelection) 
                    return AppContext.ViewModel.Apps[cmbApps.SelectedIndex]; 
                return null; 
            }
        }

        private void cmbApps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshSnapshot(true);
            AppContext.Configuration.Save();
        }

        private void RefreshSnapshot(bool bRestoreBeforeSnapshot)
        {
            if (_timer == null) //disposed
                return;

            if (HasSelection)
            {
                SelectedApp.Refresh();

                if (SelectedApp.IsActive)
                {
                    if (bRestoreBeforeSnapshot || !User32.IsMinimized(SelectedApp.HWND))
                    {
                        imagePreview.Source = Logic.CaptureApplication(SelectedApp.HWND, bRestoreBeforeSnapshot);
                        UpdateInfo();
                    }
                    else if (imagePreview == null) //no snapshot
                    {

                    }
                }
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (HasSelection)
            {
                //btnRun.ToolTip = "Open " + SelectedApp.ProcessName;
                //btnRun.IsEnabled = File.Exists(SelectedApp.ProcessPath);
                mnuRun.IsEnabled = File.Exists(SelectedApp.ProcessPath);
                mnuMinimize.IsEnabled = SelectedApp.IsActive;
                mnuRestore.IsEnabled = SelectedApp.IsActive;

                DisplayConfiguration[Row, Col] = SelectedApp;
            }
            else
            {
                //btnRun.ToolTip = "";
                //btnRun.IsEnabled = false;
                mnuRun.IsEnabled = false;
                mnuMinimize.IsEnabled = false;
                mnuRestore.IsEnabled = false;

                imagePreview.Source = null;
                txtInfo.Content = "? Select Window ?";
                DisplayConfiguration[Row, Col] = AppInfo.GetEmptyAppInfo();
            }
        }

        private void RunApp_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(SelectedApp.ProcessPath))
            {
                List<Process> exist = Logic.FindProcess(SelectedApp.ProcessName);
                if(exist.Count > 0)
                {
                    MessageBox.Show("Process alredy running: " + SelectedApp.ProcessName);
                    return;
                }

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

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            User32.RestoreWindow(SelectedApp.HWND);
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            User32.MinimizeWindow(SelectedApp.HWND);
        }

        private Thickness ThicknessFromRowCol(int row, int col)
        {
            int rows = DisplayConfiguration.GridSize.Rows;
            int cols = DisplayConfiguration.GridSize.Cols;

            const double thin = 4, thick = 2 * thin;

            double left = (col == 0) ? thick: thin;
            double right = (col == cols-1) ? thick : thin;
            double top = (row == 0) ? thick : thin;
            double bottom = (row == rows - 1) ? thick: thin;

            return new Thickness(left, top, right, bottom);
        }
    }
}
