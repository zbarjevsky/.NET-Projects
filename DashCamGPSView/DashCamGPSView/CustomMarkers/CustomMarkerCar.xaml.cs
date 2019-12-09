using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashCamGPSView.CustomMarkers
{
    /// <summary>
    /// Interaction logic for CustomMarkerCar.xaml
    /// </summary>
    public partial class CustomMarkerCar : UserControl
    {
        public CustomMarkerCar()
        {
            InitializeComponent();
        }

        public void UpdatePosition(double top, double left, double direction)
        {
            arrowDirection.Angle = direction;

            Canvas.SetTop(_car, top);
            Canvas.SetLeft(_car, left);
        }
    }
}
