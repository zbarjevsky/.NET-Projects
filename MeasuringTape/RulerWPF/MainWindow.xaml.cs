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

namespace RulerWPF
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

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            AddAdorner(_ruler);
        }

        private void AddAdorner(UIElement element)
        {
            AdornerLayer adornerlayer = AdornerLayer.GetAdornerLayer(element);
            if (adornerlayer.GetAdorners(element) == null || adornerlayer.GetAdorners(element).Length == 0)
            {
                RotateResizeAdorner adorner = new RotateResizeAdorner(element);
                adornerlayer.Add(adorner);
            }
        }

        private void RemoveAllAdorners()
        {
            foreach (UIElement element in _canvas.Children)
            {
                AdornerLayer adornerlayer = AdornerLayer.GetAdornerLayer(element);
                var adorners = adornerlayer.GetAdorners(element);
                if (adorners != null)
                {
                    for (int i = adorners.Length - 1; i >= 0; i--)
                    {
                        adornerlayer.Remove(adorners[i]);
                    }
                }
            }
        }
    }
}
