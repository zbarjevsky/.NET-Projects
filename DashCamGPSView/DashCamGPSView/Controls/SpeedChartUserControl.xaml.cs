using GPSDataParser;
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

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for ChartUserControl.xaml
    /// </summary>
    public partial class SpeedChartUserControl : UserControl
    {
        public List<GpsPointData> RouteMain = new List<GpsPointData>();

        public SpeedChartUserControl()
        {
            InitializeComponent();
        }

        public void SetGpsInfo(List<GpsPointData> points)
        {
            RouteMain.Clear();
            if(points != null)
                RouteMain = new List<GpsPointData>(points); //copy
            UpdateRoute(_segmentMain, _figureMain);
        }

        public void SetCarPosition(int index)
        {
            if (index >= 0 && index < RouteMain.Count)
            {
                double maxSpeed = FindMaxSpeed(RouteMain);
                TimeSpan interval = RouteMain.Last().FixTime - RouteMain.First().FixTime;

                Point car = GetPoint(index, maxSpeed, interval);
                _carPosition.X = GetValidPosition(car.X - _car.Width / 2, 0, _car.Width, this.ActualWidth);
                _carPosition.Y = GetValidPosition(car.Y - _car.Height / 2, -10, -10, this.ActualHeight);

                _txtInfo.Text = string.Format("Speed: {0:0.0} mph\n{1}\nMax Speed: {2:0.0} mph", 
                    RouteMain[index].SpeedMph, 
                    RouteMain[index].FixTime.ToString("yyyy/MM/dd HH:mm:ss"), 
                    maxSpeed);

                _textPosition.X = GetValidPosition(car.X - _txtInfo.ActualWidth/2, 0, _txtInfo.ActualWidth, this.ActualWidth);

                _carDirection.Angle = RouteMain[index].Course;

                _car.Visibility = Visibility.Visible;
            }
            else
            {
                _car.Visibility = Visibility.Collapsed;
                _txtInfo.Text = "No GPS info";
                _textPosition.X = 0;
            }
        }

        private double GetValidPosition(double position, double loMargin, double hiMargin, double maxPosition)
        {
            double resPosition = position;
            if (resPosition < loMargin)
                resPosition = loMargin;
            if (resPosition > maxPosition - hiMargin)
                resPosition = maxPosition - hiMargin;
            return resPosition;
        }

        private void UpdateRoute(PolyLineSegment segment, PathFigure figure)
        {
            segment.Points.Clear();
            if (RouteMain.Count == 0)
                return;

            double maxSpeed = FindMaxSpeed(RouteMain);
            TimeSpan interval = RouteMain.Last().FixTime - RouteMain.First().FixTime;

            figure.StartPoint = GetPoint(0, maxSpeed, interval);
            for (int i = 0; i < RouteMain.Count; i++)
            {
                Point pt = GetPoint(i, maxSpeed, interval);
                segment.Points.Add(pt);
            }

            SetCarPosition(0);
        }

        private Point GetPoint(int idx, double maxSpeed, TimeSpan interval)
        {
            double speed = RouteMain[idx].SpeedMph;
            TimeSpan time = RouteMain[idx].FixTime - RouteMain.First().FixTime;
            double w = this.ActualWidth;
            double h = this.ActualHeight - 30; //margin adjust

            double x = time.TotalSeconds * w / interval.TotalSeconds;
            double y = h - (speed * h / maxSpeed);

            return new Point(x, y);
        }

        private static double FindMaxSpeed(List<GpsPointData> route)
        {
            double max = 1; //max is at least 1 mph
            foreach (GpsPointData g in route)
            {
                max = Math.Max(max, g.SpeedMph);
            }
            return max;
        }
    }
}
