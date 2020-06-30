using DesktopManagerUX.Utils;
using MZ.WPF.Utils;
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

            tabLayouts.Items.Clear();
            tabLayouts.ItemsSource = AppContext.Configuration.Layouts;
            tabLayouts.SelectedIndex = 0; //AppContext.Configuration.SelectedDisplayInfo.Index;

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
            AppContext.Configuration.SmartDisplaysUpdate();
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

        private void AddLayout_Click(object sender, RoutedEventArgs e)
        {
            string name = GenerateNewLayoutName();
            AppContext.Configuration.Layouts.Add(new LayoutConfiguration(true, name));
            tabLayouts.SelectedIndex = tabLayouts.Items.Count - 1; //last
        }

        private string GenerateNewLayoutName()
        {
            for (int i = 0; i < 200; i++)
            {
                LayoutConfiguration found = AppContext.Configuration.Layouts.FirstOrDefault(l => l.Name.StartsWith("Layout " + (i+1)));
                if (found == null)
                    return "Layout " + (i + 1);
            }
            return "<<>>";
        }

        private void RemoveLayout_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if(btn.DataContext is LayoutConfiguration layout)
                {
                    int idx = AppContext.Configuration.Layouts.IndexOf(layout);
                    if(idx>=0)
                        AppContext.Configuration.Layouts.RemoveAt(idx);
                }
            }
        }

        //click inside TextBox does not select tab - do it here
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox txt)
            {
                if (txt.DataContext is LayoutConfiguration layout)
                {
                    int idx = AppContext.Configuration.Layouts.IndexOf(layout);
                    if (idx >= 0)
                        tabLayouts.SelectedIndex = idx;
                }
            }
        }
    }
}
