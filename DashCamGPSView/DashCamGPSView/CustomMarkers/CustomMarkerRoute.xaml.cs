using GMap.NET.WindowsPresentation;
using GPSDataParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace DashCamGPSView.CustomMarkers
{
    /// <summary>
    /// Interaction logic for CustomMarkerRoute.xaml
    /// </summary>
    public partial class CustomMarkerRoute : UserControl, INotifyPropertyChanged
    {
        public List<GpsPointData> Route = new List<GpsPointData>();

        //public Point StartPoint { get; set; } = new Point();
        //public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();

        public CustomMarkerRoute()
        {
            InitializeComponent();

            UpdateRouteAndCar(null, null, null);
        }

        public void UpdateRouteAndCar(List<GpsPointData> route, GPSDataParser.GpsPointData inf, GMapControl map)
        {
            Route.Clear();
            _figure.StartPoint = new Point();
            _segment.Points.Clear();
            Visibility = Visibility.Hidden;
            if (route == null || route.Count < 3)
                return;

            Route.Add(route[0]);
            _figure.StartPoint = GetPoint(route[0], map);
            for (int i = 1; i < route.Count; i++)
            {
                Route.Add(route[i]);
                _segment.Points.Add(GetPoint(route[i], map));
            }

            GMap.NET.PointLatLng currentPosition = new GMap.NET.PointLatLng(inf.Latitude, inf.Longitude);
            GMap.NET.GPoint ptCar = map.FromLatLngToLocal(currentPosition);

            Canvas.SetLeft(_car, ptCar.X - 20);
            Canvas.SetTop(_car, ptCar.Y - 20);
            arrowDirection.Angle = inf.Course;

            Visibility = Visibility.Visible;
        }

        private Point GetPoint(GpsPointData data, GMapControl map)
        {
            GMap.NET.GPoint pt0 = map.FromLatLngToLocal(new GMap.NET.PointLatLng(data.Latitude, data.Longitude));
            return new Point(pt0.X, pt0.Y);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
