﻿using DashCamGPSView.Tools;
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
        public List<GpsPointData> RouteMain = new List<GpsPointData>();
        public List<GpsPointData> RoutePrev = new List<GpsPointData>();

        private int _iCurrentPointIndex = -1;

        //public Point StartPoint { get; set; } = new Point();
        //public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();

        public CustomMarkerRoute()
        {
            InitializeComponent();

            SetRouteAndCar(null);
        }

        public void SetRouteAndCar(DashCamFileInfo dashCamFileInfo)
        {
            if (dashCamFileInfo == null || dashCamFileInfo.GpsInfo == null || dashCamFileInfo.GpsInfo.Count == 0)
            {
                RoutePrev.Clear();
                _figurePrev.StartPoint = new Point();
                _segmentPrev.Points.Clear();

                RouteMain.Clear();
                _figureMain.StartPoint = new Point();
                _segmentMain.Points.Clear();

                _iCurrentPointIndex = -1;

                Visibility = Visibility.Hidden;

                return;
            }

            RoutePrev = new List<GpsPointData>(RouteMain); //copy
            _figurePrev.StartPoint = _figureMain.StartPoint;
            _segmentPrev.Points = new PointCollection(_segmentMain.Points);

            RouteMain = new List<GpsPointData>(dashCamFileInfo.GpsInfo); //copy

            if (RouteMain.Count > 0)
            {
                if(RoutePrev.Count > 0)
                    RoutePrev.Add(RouteMain[0]); //add first point of current route to prev route

                _iCurrentPointIndex = 0;
            }
        }

        //update route when index change
        public void UpdateRouteAndCar(int idx, GMapControl map)
        {
            if (RouteMain == null || RouteMain.Count == 0)
            {
                _iCurrentPointIndex = -1;
                Visibility = Visibility.Hidden;
                return;
            }

            _iCurrentPointIndex = idx;
            UpdateRouteAndCarRefresh(map);
        }

        /// <summary>
        /// Map moved to new position by dragging
        /// </summary>
        /// <param name="point"></param>
        public void UpdateRouteAndCarRefresh(GMapControl map)
        {
            if (RouteMain.Count == 0)
            {
                Visibility = Visibility.Hidden;
                return;
            }

            UpdateRouteUIPoints(_segmentMain, _figureMain, RouteMain, map);
            UpdateRouteUIPoints(_segmentPrev, _figurePrev, RoutePrev, map);

            //update car position
            if (_iCurrentPointIndex >= 0)
            {
                _car.Opacity = 0.8;

                GMap.NET.PointLatLng currentPosition = new GMap.NET.PointLatLng(RouteMain[_iCurrentPointIndex].Latitude, RouteMain[_iCurrentPointIndex].Longitude);
                GMap.NET.GPoint pt0 = map.FromLatLngToLocal(currentPosition);
                Point ptCar = new Point(pt0.X, pt0.Y);

                Canvas.SetLeft(_car, ptCar.X - _car.ActualWidth * _car.RenderTransformOrigin.X); //middle width
                Canvas.SetTop(_car, ptCar.Y - _car.ActualHeight * _car.RenderTransformOrigin.Y); //toward the front of car
                carDirection.Angle = RouteMain[_iCurrentPointIndex].Course;
                UpdateCarScale(map.Zoom);
            }
            else
            {
                _car.Opacity = 0.1;
            }

            Visibility = Visibility.Visible;
        }

        private void UpdateCarScale(double zoom)
        {
            const double MAX_ZOOM = 24;
            const double MIN_ZOOM = 10;

            const double MAX_SCALE = 3;
            const double MIN_SCALE = 0.5;

            if (zoom < MIN_ZOOM) //state level
            {
                carScale.ScaleX = MIN_SCALE;
                carScale.ScaleY = MIN_SCALE;
            }
            else if(zoom >= MIN_ZOOM && zoom < MAX_ZOOM)
            {
                double range = MAX_ZOOM - MIN_ZOOM;
                double val = zoom - MIN_ZOOM;
                double scale = MIN_SCALE + (MAX_SCALE - MIN_SCALE) * val / range;
                carScale.ScaleX = scale;
                carScale.ScaleY = scale;
            }
            else
            {
                carScale.ScaleX = MAX_SCALE;
                carScale.ScaleY = MAX_SCALE;
            }
        }

        private void UpdateRouteUIPoints(PolyLineSegment segment, PathFigure figure, List<GpsPointData> route, GMapControl map)
        {
            segment.Points.Clear();
            if (route.Count == 0)
                return;

            figure.StartPoint = GetPoint(route[0], map);
            for (int i = 0; i < route.Count; i++)
            {
                Point pt = GetPoint(route[i], map);
                if(i>0) //not first point
                {
                    Vector v = pt - segment.Points.Last();
                    if (v.Length < 4)
                        continue; //skip points too close to each other
                }
                segment.Points.Add(pt);
            }
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
