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

            //if version changed - settings location changed - get settings from previous location/version
            if (Properties.Settings.Default.UpgradeNeeded)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeNeeded = false;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }

            _VM.AddAppChooser(app0x0, 0, 0, Properties.Settings.Default.AppTitle0x0);
            _VM.AddAppChooser(app0x1, 0, 1, Properties.Settings.Default.AppTitle0x1);
            _VM.AddAppChooser(app1x0, 1, 0, Properties.Settings.Default.AppTitle1x0);
            _VM.AddAppChooser(app1x1, 1, 1, Properties.Settings.Default.AppTitle1x1);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.AppTitle0x0 = (app0x0.cmbApps.SelectedItem as AppInfo).Name;
            Properties.Settings.Default.AppTitle0x1 = (app0x1.cmbApps.SelectedItem as AppInfo).Name;
            Properties.Settings.Default.AppTitle1x0 = (app1x0.cmbApps.SelectedItem as AppInfo).Name;
            Properties.Settings.Default.AppTitle1x1 = (app1x1.cmbApps.SelectedItem as AppInfo).Name;
            Properties.Settings.Default.Save();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            WpfScreen screen = WpfScreen.GetScreenFrom(this);
            Rect bounds = screen.WorkingArea;

            int rows = _VM.AppChoosers.GetLength(0);
            int cols = _VM.AppChoosers.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Apply(row, col, bounds);
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

            double width = 7 + bounds.Width / cols;
            double height = bounds.Height / rows;

            double left = bounds.Left + col * width;
            double top = bounds.Top + row * height;

            Logic.MoveWindow(app.Process, left, top, width, height);
        }
    }
}
