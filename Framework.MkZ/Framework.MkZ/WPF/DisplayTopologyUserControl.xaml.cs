using MZ.WPF;
using MZ.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApplication6
{
    /// <summary>
    /// Interaction logic for DisplayTopologyUserControl.xaml
    /// </summary>
    public partial class DisplayTopologyUserControl : UserControl
    {
        private List<Border> _diplayRectangles = new List<Border>();
        public DisplayTopologyUserControl()
        {
            InitializeComponent();

            CreateDisplaysRectangles();
        }

        private void CreateDisplaysRectangles()
        {
            var displays = WpfScreen.AllScreens();
            displays.Sort((d1,d2) => (int)(d1.DeviceBounds.Left - d2.DeviceBounds.Left));

            _diplayRectangles.ForEach(disp => disp.Draggable(false));
            _diplayRectangles.Clear();

            var desktopBounds = WPF_Helper.VirtualScreenBounds();

            displays.ForEach((disp) => 
            {
                Border r = new Border();
                r.Opacity = 0.85;
                r.BorderBrush = Brushes.DimGray;
                r.Background = Brushes.AliceBlue;
                r.BorderThickness = new Thickness(3);
                r.CornerRadius = new CornerRadius(3);
                r.Width = disp.DeviceBounds.Width / 10.0;
                r.Height = disp.DeviceBounds.Height / 10.0;

                Canvas.SetLeft(r, (disp.DeviceBounds.Left - desktopBounds.Left) / 10.0);
                Canvas.SetTop(r, (disp.DeviceBounds.Top - desktopBounds.Top) / 10.0);

                r.Draggable();
                r.LayoutUpdated += (s, e) => { Debug.WriteLine("R - moved"); };

                TextBlock txt = new TextBlock();
                txt.FontSize = 16;
                txt.TextAlignment = TextAlignment.Center;
                txt.Text = string.Format("Display [{0}]\n\n{1}", disp.Index, disp.WorkingArea);
                r.Child = txt;

                _diplayRectangles.Add(r);
            });
        }

        public void DrawDisplays()
        {
            _canvas.Children.Clear();
            _diplayRectangles.ForEach(r => _canvas.Children.Add(r));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DrawDisplays();
        }
    }
}
