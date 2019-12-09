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
    public partial class CustomMarkerCar : CustomMarkerBase
    {
        public double Direction
        {
            get { return arrowDirection.Angle; }
            set { arrowDirection.Angle = value; }
        }

        public CustomMarkerCar(GMapControl map, GMapMarker marker, string title) 
            : base(map, marker, title)
        {
            InitializeComponent();

            base.SetImage(icon);

            marker.Offset = new Point();

            IsDraggable = false; //do not allow to drag control
        }

        public override void UpdateOffset(double width, double heigth)
        {
            _marker.Offset = new Point(-width / 2, -heigth / 2); //center
        }
    }
}
