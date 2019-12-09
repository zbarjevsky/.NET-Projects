using DashCamGPSView.Tools;
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

        private int _iCurrentPointIndex = -1;

        //public Point StartPoint { get; set; } = new Point();
        //public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();

        public CustomMarkerRoute()
        {
            InitializeComponent();

            UpdateRouteAndCar(null, -1, null);
        }

        public void UpdateRouteAndCar(List<GpsPointData> data, int idx, GMapControl map)
        {
            Route.Clear();
            _figure.StartPoint = new Point();
            _segment.Points.Clear();
            _iCurrentPointIndex = -1;
            Visibility = Visibility.Hidden;

            if (data == null || data.Count == 0)
                return;

            _iCurrentPointIndex = idx;
            Route = new List<GpsPointData>(data); //copy

            UpdateRouteAndCarRefresh(map);
        }

        /// <summary>
        /// Map moved to new position by dragging
        /// </summary>
        /// <param name="point"></param>
        public void UpdateRouteAndCarRefresh(GMapControl map)
        {
            _segment.Points.Clear();
            if (Route.Count == 0)
                return;

            _figure.StartPoint = GetPoint(Route[0], map);
            for (int i = 1; i < Route.Count; i++)
            {
                _segment.Points.Add(GetPoint(Route[i], map));
            }

            GMap.NET.PointLatLng currentPosition = new GMap.NET.PointLatLng(Route[_iCurrentPointIndex].Latitude, Route[_iCurrentPointIndex].Longitude);
            GMap.NET.GPoint pt0 = map.FromLatLngToLocal(currentPosition);
            Point ptCar = new Point(pt0.X, pt0.Y);

            Canvas.SetLeft(_car, ptCar.X - 20);
            Canvas.SetTop(_car, ptCar.Y - 20);
            arrowDirection.Angle = Route[_iCurrentPointIndex].Course;

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
