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
        private ViewModel _VM = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _VM;

            app0x0.SetVM(_VM);
            app0x1.SetVM(_VM);
            app1x0.SetVM(_VM);
            app1x1.SetVM(_VM);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            WpfScreen screen = WpfScreen.GetScreenFrom(this);
            Rect bounds = screen.WorkingArea;

            double width = bounds.Width / 2;
            double height = bounds.Height / 2;
            double left = 0;
            double top = 0;

            AppInfo app = app0x0.SelectedApp;
            Logic.MoveWindow(app.Process, left, top, width, height);

            app = app0x1.SelectedApp;
            left = width;
            Logic.MoveWindow(app.Process, left, top, width, height);

            app = app1x0.SelectedApp;
            left = 0;
            top = height;
            Logic.MoveWindow(app.Process, left, top, width, height);

            app = app1x1.SelectedApp;
            left = width;
            top = height;
            Logic.MoveWindow(app.Process, left, top, width, height);
        }
    }
}
