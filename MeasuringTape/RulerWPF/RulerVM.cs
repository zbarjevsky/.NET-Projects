using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            oRenderTransformOrigin = new Point(0.5, 0.5);
            oTranslateTransformX = oTranslateTransformY = 0;
            oThumbLeft = 375;
        }

        public Point Origin(UIElement element)
        {
            return element.PointToScreen(Origin(_origin));
        }

        public void UpdateRenderTransformOrigin(Point newOrigin, UIElement element)
        {
            double angle = oAngle;
            oAngle = 0;
            Point oldOrigin = Origin(element);
            oRenderTransformOrigin = newOrigin;
            newOrigin = Origin(element);
            oAngle = angle;

            UpdateTranslateTransform(oldOrigin, newOrigin);
        }

        public void SetAngle(double angle, bool snapToGrid)
        {
            if (angle > 360)
                angle -= 360;
            if (angle < 0)
                angle += 360;

            if(snapToGrid)
                angle = SnapToGrid(angle);

            oAngle = angle;
        }

        private double SnapToGrid(double angle)
        {
            double[] snapAngles = new double[] { 0, 90, 180, 360 };
            foreach (double a in snapAngles)
            {
                double diff = Math.Abs(a - angle);
                if (diff < 0.5)
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

        private void UpdateTranslateTransform(Point originOld, Point originNew)
        {
            if (originOld == originNew)
                return;

            Vector delta = originNew - originOld;

            //double angle = oAngle;
            //oAngle = 0;
            //oTranslateTransformX -= delta.X;
            //oTranslateTransformY -= delta.Y;
            //oAngle = angle;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
