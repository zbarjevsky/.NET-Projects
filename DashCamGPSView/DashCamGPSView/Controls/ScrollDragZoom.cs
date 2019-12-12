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

        private double Zoom = 1.0;
        private double _origWidth, _origHeight;

        public ScrollDragZoom(FrameworkElement content, ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
            _content = content;

            _origWidth = _content.Width;
            _origHeight = _content.Height;

            content.MouseRightButtonDown += scrollViewer_MouseRightButtonDown;
            content.PreviewMouseMove += scrollViewer_PreviewMouseMove;
            content.PreviewMouseRightButtonUp += scrollViewer_PreviewMouseRightButtonUp;
            content.MouseWheel += content_MouseWheel;
        }

        public void ScrollToCenter()
        {
            _scrollViewer.ScrollToVerticalOffset(_scrollViewer.ScrollableHeight / 2);
            _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.ScrollableWidth / 2);
        }

        private void content_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Zoom *= ((e.Delta > 0) ? 1.1 : 0.9);
            if (Zoom < 0.1)
                Zoom = 0.1;
            if (Zoom > 10)
                Zoom = 10;

            _content.Width = Zoom * _origWidth;
            _content.Height = Zoom * _origHeight;

            ScrollToCenter();
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
