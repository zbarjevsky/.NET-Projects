using DashCamGPSView.Tools;
using GPSDataParser;
using MkZ.WPF;
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

        IVideoPlayer _player;
        DashCamFileInfo _info;

        public SpeedChartUserControl()
        {
            InitializeComponent();
        }

        public void SetInfo(DashCamFileInfo info, IVideoPlayer player)
        {
            _info = info;
            _player = player;

            RouteMain.Clear();
            if(_info != null && _info.GpsInfo != null)
                RouteMain = new List<GpsPointData>(_info.GpsInfo); //copy

            SetCarPosition(0);
        }

        public void SetCarPosition(int index)
        {
            if (this.ActualHeight < 30)
                return; //do not update if too small

            double maxSpeed = FindMaxSpeed(RouteMain);
            UpdateRoute(_segmentMain, _canvasPoints, _figureMain, maxSpeed);
            WPFUtils.ExecuteOnUIThread(() =>
            {
                if (index >= 0 && index < RouteMain.Count)
                {
                    TimeSpan interval = RouteMain.Last().FixTime - RouteMain.First().FixTime;

                    Point car = GetPoint(index, maxSpeed, interval);
                    _carPosition.X = GetValidPosition(car.X - _car.Width * _car.RenderTransformOrigin.X, 0, _car.Width, this.ActualWidth);
                    _carPosition.Y = GetValidPosition(car.Y - _car.Height * _car.RenderTransformOrigin.Y, -10, -10, this.ActualHeight);

                    _txtInfo.Text = GetInfoText(index, maxSpeed);

                    _textPosition.X = GetValidPosition(car.X - _txtInfo.ActualWidth / 2, 0, _txtInfo.ActualWidth, this.ActualWidth);

                    _carDirection.Angle = RouteMain[index].Course;

                    _car.Visibility = Visibility.Visible;
                }
                else
                {
                    _car.Visibility = Visibility.Collapsed;
                    _txtInfo.Text = "No GPS info";
                    _textPosition.X = 0;
                }
            });
        }

        private string GetInfoText(int index, double maxSpeed)
        {
            return string.Format("Speed: {0:0.0} mph\n{1}\nMax Speed: {2:0.0} mph",
                    RouteMain[index].SpeedMph,
                    RouteMain[index].FixTime.ToString("yyyy/MM/dd HH:mm:ss"),
                    maxSpeed);
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

        private void UpdateRoute(PolyLineSegment segment, Canvas canvas, PathFigure figure, double maxSpeed)
        {
            WPFUtils.ExecuteOnUIThread(() =>
            {
                canvas.Children.Clear();
                segment.Points.Clear();
                if (RouteMain.Count == 0)
                    return;

                TimeSpan interval = RouteMain.Last().FixTime - RouteMain.First().FixTime;

                figure.StartPoint = GetPoint(0, maxSpeed, interval);
                for (int i = 0; i < RouteMain.Count; i++)
                {
                    Point pt = GetPoint(i, maxSpeed, interval);
                    segment.Points.Add(pt);
                    canvas.Children.Add(GetCircle(i, pt, RouteMain[i].GetSpeed(SpeedUnits.mph), maxSpeed));
                }
            });
        }

        private Ellipse GetCircle(int idx, Point pt, double speed, double maxSpeed, double radius = 8)
        {
            Ellipse e = new Ellipse();
            e.Width = radius;
            e.Height = radius;
            Canvas.SetLeft(e, pt.X - radius/2);
            Canvas.SetTop(e, pt.Y - radius/2);
            e.Fill = Brushes.DarkGoldenrod;
            e.ToolTip = GetInfoText(idx, maxSpeed);
            e.Tag = idx;
            e.MouseLeftButtonUp += Circle_MouseLeftButtonUp;

            return e;
        }

        private void Circle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(sender is Ellipse circle)
            {
                int idx = (int)circle.Tag;
                TimeSpan pos = _info.FindPositionFromGpsIndex(idx, false, 15);
                _player.PositionSet(pos, true);
            }
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
