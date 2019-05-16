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
            oRenderTransformOrigin = new Point(0.5, 0.5);
            oTranslateTransformX = oTranslateTransformY = 0;
            oThumbLeft = 375;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
