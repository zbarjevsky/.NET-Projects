using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DashCamGPSView
{
    public class ScrollDragZoom
    {
        private readonly ScrollViewer _scrollViewer;
        private readonly FrameworkElement _content;
        private Point _scrollMousePoint;
        private double _vOff = 1, _hOff = 1;

        public double VerticalOffset
        {
            get { return _scrollViewer.VerticalOffset; }
            set { _scrollViewer.ScrollToVerticalOffset(value); }
        }

        private double _zoom = 1.0;
        public double Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                _content.Width = _zoom * _origWidth;
                _content.Height = _zoom * _origHeight;
            }
        }

        private double _origWidth, _origHeight;
        public Size NaturalSize
        {
            get { return new Size(_origWidth, _origHeight); }
            set 
            { 
                _origWidth = value.Width; 
                _origHeight = value.Height;
                _zoom = _content.Width / _origWidth;
            }
        }

        public ScrollDragZoom()
        {

        }

        public ScrollDragZoom(FrameworkElement content, ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
            _content = content;

            if (_content != null)
            {
                _origWidth = _content.Width;
                _origHeight = _content.Height;

                content.MouseRightButtonDown += scrollViewer_MouseRightButtonDown;
                content.PreviewMouseMove += scrollViewer_PreviewMouseMove;
                content.PreviewMouseRightButtonUp += scrollViewer_PreviewMouseRightButtonUp;
                content.MouseWheel += content_MouseWheel;
            }
        }

        public void ScrollToCenter()
        {
            _scrollViewer.UpdateLayout();
            _scrollViewer.ScrollToVerticalOffset(_scrollViewer.ScrollableHeight / 2);
            _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.ScrollableWidth / 2);
        }

        public void FitWidth(double margin = 18)
        {
            _content.Width = _scrollViewer.ActualWidth - margin;

            //proportionally change height
            _content.Height = _content.Width * NaturalSize.Height / NaturalSize.Width;

            _zoom = _content.Width / NaturalSize.Width;
        }

        public void OriginalSize()
        {
            _content.Width = NaturalSize.Width;
            _content.Height = NaturalSize.Height;
            _zoom = 1;
        }

        internal void FitWindow(double margin = 18)
        {
            if (_scrollViewer.ActualHeight > margin && _scrollViewer.ActualWidth > margin)
            {
                _content.Width = _scrollViewer.ActualWidth - 18;
                _content.Height = _scrollViewer.ActualHeight - 18;
            }

            ScrollToCenter();
        }

        private void content_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoom = _zoom * ((e.Delta > 0) ? 1.1 : 0.9);
            if (zoom < 0.1)
                zoom = 0.1;
            if (zoom > 10)
                zoom = 10;

            Zoom = zoom; //update control

            //ScrollToCenter();
        }

        private void scrollViewer_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _content.CaptureMouse();
            _scrollMousePoint = e.GetPosition(_scrollViewer);
            _vOff = _scrollViewer.VerticalOffset;
            _hOff = _scrollViewer.HorizontalOffset;
        }

        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_content.IsMouseCaptured)
            {
                var newVOffset = _vOff + (_scrollMousePoint.Y - e.GetPosition(_scrollViewer).Y);
                _scrollViewer.ScrollToVerticalOffset(newVOffset);

                var newHOffset = _hOff + (_scrollMousePoint.X - e.GetPosition(_scrollViewer).X);
                _scrollViewer.ScrollToHorizontalOffset(newHOffset);
            }
        }

        private void scrollViewer_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _content.ReleaseMouseCapture();
        }
    }
}
