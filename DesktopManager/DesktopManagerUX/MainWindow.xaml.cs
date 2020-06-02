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
        public MainWindow()
        {
            AppContext.Init(this);

            InitializeComponent();

            this.DataContext = AppContext.ViewModel;

            tabDisplays.Items.Clear();
            tabDisplays.ItemsSource = AppContext.Configuration.Displays;
            tabDisplays.SelectedIndex = 0; //AppContext.Configuration.SelectedDisplayInfo.Index;

            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //AppContext.Configuration.SelectedDisplayInfo = cmbDisplays.SelectedItem as DisplayInfo;
            AppContext.Configuration.Save();
            Properties.Settings.Default.Save();
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            AppContext.Configuration.UpdateDisplays();
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

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseSelected_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
