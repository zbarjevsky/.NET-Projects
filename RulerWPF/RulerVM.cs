using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MkZ.WPF.RulerWPF
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

    //https://stackoverflow.com/questions/32256222/a-generic-way-to-create-a-checkable-context-menu-from-a-list-of-enum-values
    public enum MeasurementUnits : int
    {
        [Description("px")]
        Pixels = 0,
        [Description("in")]
        Inches = 1,
        [Description("cm")]
        Centimeters = 2
    }

    public class RulerVM : INotifyPropertyChanged
    {
        private MeasurementUnits _units = MeasurementUnits.Pixels;
        public MeasurementUnits MeasurementUnits { get { return _units; } set { _units = value; OnPropertyChanged(); } }

        public double oDisplayDiagonal { get { return Properties.Settings.Default.MonitorDiagonal; } set { Properties.Settings.Default.MonitorDiagonal = value; OnPropertyChanged(); } }

        public MouseMoveOp MouseMoveOp { get; set; } = MouseMoveOp.None;
        public FrameworkElement CurrentElement { get; set; } = null;

        private double _angle = 0;
        public double oAngle { get { return _angle; } set { SetAngle(value); OnPropertyChanged(); OnPropertyChanged("oAngleText"); } }
        public string oAngleText { get { return _angle.ToString("0.0"); } set { SetAngle(value); OnPropertyChanged(""); } }

        private Point _origin = new Point();
        public Point oRenderTransformOrigin { get { return _origin; } set { _origin = value; OnPropertyChanged(); } }

        private double _width = 400;
        public double oWidth { get { return _width; } set { _width = value; oThumbLeft = value - 30; OnPropertyChanged(); } }

        private double _height = 60;
        public double oHeight { get { return _height; } set { _height = value; OnPropertyChanged(); } }

        private Cursor _cursor = Cursors.AppStarting;
        public Cursor oSizeCursor { get { return _cursor; } set { _cursor = value; OnPropertyChanged(); } }

        private double _cursorX = 0;
        public double oCursorPosX { get { return _cursorX; } set { _cursorX = value; OnPropertyChanged(); } }

        private Visibility _cursorXVisibility = Visibility.Visible;
        public Visibility oCursorLineVisibility { get { return _cursorXVisibility; } set { _cursorXVisibility = value; OnPropertyChanged(); } }

        private double _thumbLeft = 370;
        public double oThumbLeft { get { return _thumbLeft; } set { _thumbLeft = value; OnPropertyChanged(); } }

        private double _translateTransformX = 0;
        public double oTranslateTransformX { get { return _translateTransformX; } set { _translateTransformX = value; OnPropertyChanged(); } }

        private double _translateTransformY = 0;
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
            oSizeCursor = Cursors.SizeWE;
        }

        public void UpdateCurrentOperation(MouseMoveOp operation, FrameworkElement element)
        {
            CurrentElement = element;
            MouseMoveOp = operation;

            oCursorLineVisibility = MouseMoveOp == MouseMoveOp.None ? Visibility.Visible : Visibility.Collapsed;
        }

        public Point OriginOnScreen(UIElement element)
        {
            return element.PointToScreen(ExpandOriginToRealLocation(_origin));
        }

        //if origin < 1 - it's relative - expand to real location
        private Point ExpandOriginToRealLocation(Point origin)
        {
            if (origin.X <= 1 && origin.Y <= 1)
            {
                origin.X *= oWidth;
                origin.Y *= oHeight;
            }
            return origin;
        }

        public void UpdateRenderTransformOrigin(Point newOrigin, UIElement element)
        {
            if(newOrigin == _origin)
                return;

            Point oldOrigin = Utils.ScaleToWPF_DPI(OriginOnScreen(element));
            oRenderTransformOrigin = newOrigin;
            newOrigin = Utils.ScaleToWPF_DPI(OriginOnScreen(element));

            CompensateTranslateTransform(oldOrigin, newOrigin);
        }

        public void SetAngle(double angle)
        {
            _angle = angle % 360;

            if (_angle < 0)
                _angle += 360;

            UpdateSizeCursor();
        }

        public void SetAngle(string strAngle)
        {
            double angle;
            if(double.TryParse(strAngle, out angle))
                oAngle = angle;
        }

        private Cursor[] _sizeCursors = {Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeNS, Cursors.SizeNESW};
        private double[] _angleSectors = { 0, 45, 90, 135, 180, 225, 270, 315, 360 };

        private void UpdateSizeCursor()
        {
            double angle = oAngle + 23; //shift for easy calculations
            if (angle > 360)
                angle -= 360;

            for (int i = 0; i < _angleSectors.Length-1; i++)
            {
                if(angle >= _angleSectors[i] && angle < _angleSectors[i + 1])
                {
                    oSizeCursor = _sizeCursors[i%4];
                    break;
                }
            }
        }

        public static double SnapToGrid(double angle)
        {
            double[] snapAngles = new double[] { 0, 90, 180, 270, 360 };
            foreach (double a in snapAngles)
            {
                double diff = Math.Abs(a - angle);
                if (diff < 3)
                    return a;
            }
            return angle;
        }

        public double Distance(Point ptClick, FrameworkElement currentElement)
        {
            Point elementLocationOnScreen = Utils.ScaleToWPF_DPI(currentElement.PointToScreen(new Point()));

            Point ptRelative = currentElement.PointFromScreen(Utils.ScaleFromWPF_DPI(ptClick));
            Rect bounds = new Rect(new Point(), new Point(currentElement.ActualWidth, currentElement.ActualHeight));
            if( bounds.IntersectsWith(new Rect(ptRelative, new Size(1, 1))))
                return 0;

            return Utils.MinimumDistance(bounds, ptRelative);
        }

        private void CompensateTranslateTransform(Point originOld, Point originNew)
        {
            if (originOld == originNew)
                return;

            Vector deltaOrigin = originNew - originOld;
            deltaOrigin = new Vector(deltaOrigin.X, deltaOrigin.Y);

            double signX = deltaOrigin.X < 0 ? -1 : 1;
            double signY = deltaOrigin.Y < 0 ? -1 : 1;
            if (deltaOrigin.X == 0)
                signX = signY;

            double deltaX = signX * (oWidth - Math.Abs(deltaOrigin.X));

            if ((oAngle >= 0 && oAngle <= 90) || (oAngle > 270 && oAngle < 360))
            {
                oTranslateTransformX -= deltaX; 
            }

            if (oAngle > 90 && oAngle <= 270) 
            {
                oTranslateTransformX += (deltaX + 2 * deltaOrigin.X); 
            }

            oTranslateTransformY += deltaOrigin.Y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
