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
            Point oldOrigin = Origin(element);
            _origin = newOrigin;
            newOrigin = Origin(element);

            UpdateTranslateTransform(oldOrigin, newOrigin);

            OnPropertyChanged("");
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

            //_translateTransformX -= oWidth; // delta.X;
            //_translateTransformY -= oWidth; // delta.Y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
