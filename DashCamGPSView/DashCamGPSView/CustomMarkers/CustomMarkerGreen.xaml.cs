using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;
using System.Diagnostics;
using DashCamGPSView;

namespace DashCamGPSView.CustomMarkers
{
    /// <summary>
    /// Interaction logic for CustomMarkerDemo.xaml
    /// </summary>
    public partial class CustomMarkerGreen : CustomMarkerBase
    {
        public CustomMarkerGreen(GMapControl map, GMapMarker marker, string title)
              : base(map, marker, title)
        {
            this.InitializeComponent();
            base.SetImage(icon);
        }
    }
}