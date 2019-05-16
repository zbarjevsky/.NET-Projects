using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RulerWPF
{
    public enum MouseMoveOp
    {
        None,
        LeftSize,
        RightSize,
        LeftRotate,
        RightRotate,
        Move
    }

    public class RulerVM : INotifyPropertyChanged
    {
        public MouseMoveOp MouseMoveOp { get; set; } = MouseMoveOp.None;
        public FrameworkElement CurrentElement { get; set; } = null;

        private double _angle;
        public double oAngle { get { return _angle; } set { _angle = value; OnPropertyChanged(); } }

        private Point _origin;
        public Point oRenderTransformOrigin { get { return _origin; } set { _origin = value; OnPropertyChanged(); } }

        private double _width;
        public double oWidth { get { return _width; } set { _width = value; OnPropertyChanged(); } }

        private double _height;
        public double oHeight { get { return _height; } set { _height = value; OnPropertyChanged(); } }

        private double _thumbLeft;
        public double oThumbLeft { get { return _thumbLeft; } set { _thumbLeft = value; OnPropertyChanged(); } }

        private double _translateTransformX;
        public double oTranslateTransformX { get { return _translateTransformX; } set { _translateTransformX = value; OnPropertyChanged(); } }

        private double _translateTransformY;
        public double oTranslateTransformY { get { return _translateTransformY; } set { _translateTransformY = value; OnPropertyChanged(); } }

        public RulerVM()
        {
            Reset();
        }

        public void Reset()
        {
            oAngle = 0;
            oWidth = 400;
            oHeight = 60;
            oRenderTransformOrigin = new Point();
            oTranslateTransformX = oTranslateTransformY = 0;
            oThumbLeft = 375;
        }

        public void UpdateCurrentOperation(MouseMoveOp operation, FrameworkElement element)
        {
            CurrentElement = element;
            MouseMoveOp = operation;
        }

        public Point Origin(UIElement element)
        {
            return element.PointToScreen(Origin(_origin));
        }

        public void UpdateRenderTransformOrigin(Point newOrigin, UIElement element)
        {
            if(newOrigin == _origin)
                return;

            //double angle = oAngle;
            //oAngle = 0;
            Point oldOrigin = Origin(element);
            oRenderTransformOrigin = newOrigin;
            newOrigin = Origin(element);
            //oAngle = angle;

            CompensateTranslateTransform(oldOrigin, newOrigin);
        }

        public void SetAngle(double angle, bool snapToGrid)
        {
            if (angle >= 360)
                angle -= 360;
            if (angle < 0)
                angle += 360;

            if(snapToGrid)
                angle = SnapToGrid(angle);

            oAngle = angle;
        }

        private double SnapToGrid(double angle)
        {
            double[] snapAngles = new double[] { 0, 90, 180, 270, 360 };
            foreach (double a in snapAngles)
            {
                double diff = Math.Abs(a - angle);
                if (diff < 45)
                    return a;
            }
            return angle;
        }

        private Point Origin(Point origin)
        {
            if (origin.X <= 1 && origin.Y <= 1)
            {
                origin.X *= oWidth;
                origin.Y *= oHeight;
            }
            return origin;
        }

        public double Distance(Point pt, FrameworkElement currentElement)
        {
            Point ptRelative = currentElement.PointFromScreen(pt);
            Rect bounds = new Rect(new Point(), new Point(currentElement.ActualWidth, currentElement.ActualHeight));
            if( bounds.IntersectsWith(new Rect(ptRelative, new Size(1, 1))))
                return 0;

            return Utils.MinimumDistance(bounds, ptRelative);
        }

        private void CompensateTranslateTransform(Point originOld, Point originNew)
        {
            if (originOld == originNew)
                return;

            Point left = CurrentElement.PointToScreen(new Point());
            Point right = CurrentElement.PointToScreen(new Point(CurrentElement.ActualWidth, 0));

            Vector deltaOrigin = originNew - originOld;
            Vector deltaPosition = left - right;
            Vector delta = deltaOrigin + deltaPosition;


            double angle = Math.PI * (oAngle) / 180.0; //radian
            double sin = Math.Sin(angle);
            double cos =  Math.Cos(angle);

            if (oAngle == 0) // sin = 0, cos = 1
            {
                oTranslateTransformX += cos * deltaOrigin.Y + sin * deltaOrigin.X; // = 0
                oTranslateTransformY += deltaOrigin.Y; // = 0
            }
            if (oAngle == 90) // sin = 1, cos = 0
            {
                oTranslateTransformX -= sin * deltaOrigin.Y + cos * deltaOrigin.X; // = width/-width
                oTranslateTransformY += deltaOrigin.Y;
            }
            if (oAngle == 180) // sin = 0, cos = -1
            {
                oTranslateTransformX += sin * deltaOrigin.Y + (1 - cos) * deltaOrigin.X; // = width/-width
                oTranslateTransformY += deltaOrigin.Y; // = 0
            }
            if (oAngle == 270) // sin = -1, cos = 0
            {
                oTranslateTransformX -= sin * deltaOrigin.Y + cos * deltaOrigin.X; // = width/-width
                oTranslateTransformY += deltaOrigin.Y;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
