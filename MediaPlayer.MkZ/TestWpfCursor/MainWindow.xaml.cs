using MkZ.WPF;
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

namespace TestWpfCursor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CursorArrow arrow = new CursorArrow();

            arrow.Background = Brushes.Transparent;
            arrow.Stroke = Brushes.Lime;
            arrow.SetCursorSize(70);

            //arrow.BindToColor(Context.AppConfig.Configuration, "CursorColor.B");
            this.Cursor = CursorFromControl.Create(arrow, new Size(80, 80));
            _cmb.Cursor = this.Cursor;

            //_gridMain.Children.Add(arrow);
        }

        Random r = new Random();
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            CursorArrow arrow = new CursorArrow();

            arrow.Background = Brushes.Transparent;
            arrow.Stroke = Brushes.Lime;
            arrow.SetCursorSize(70);

            //arrow.BindToColor(Context.AppConfig.Configuration, "CursorColor.B");
            this.Cursor = CursorFromControl.Create(arrow, new Size(80, 80));
            _cmb.Cursor = this.Cursor;

            double left = r.Next(30, (int)(this.ActualWidth - 100.0));
            double top = r.Next(30, (int)(this.ActualHeight - 100.0));
            arrow.Margin = new Thickness(left, top, 0, 0);

            RenderTargetBitmap bmp = CursorFromControl.CreateBitmap(arrow, new Size(80, 80));
            CursorFromControl.SaveBitmap(bmp);
            _image.Source = bmp;

            _gridMain.Children.Add(arrow);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
