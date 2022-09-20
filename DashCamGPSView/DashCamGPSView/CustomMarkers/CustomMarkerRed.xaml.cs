using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using DashCamGPSView;
using DynamicMap.NET.WindowsPresentation;

namespace DashCamGPSView.CustomMarkers
{
    /// <summary>
    /// Interaction logic for CustomMarkerDemo.xaml
    /// </summary>
    public partial class CustomMarkerRed : CustomMarkerBase
    {
        public CustomMarkerRed(DynMapControl map, DynMapMarker marker, string title)
            : base(map, marker, title)
        {
            this.InitializeComponent();

            base.SetImage(icon);
        }
    }
}