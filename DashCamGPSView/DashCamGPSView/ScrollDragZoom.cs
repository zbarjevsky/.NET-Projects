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

        public ScrollDragZoom(FrameworkElement content, ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
            _content = content;

            content.MouseLeftButtonDown += scrollViewer_MouseLeftButtonDown;
            content.PreviewMouseMove += scrollViewer_PreviewMouseMove;
            content.PreviewMouseLeftButtonUp += scrollViewer_PreviewMouseLeftButtonUp;
            content.MouseWheel += content_MouseWheel;
        }

        private void content_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double deltaX = _content.Width * ((e.Delta > 0) ? 0.1 : -0.1);
            double deltaY = _content.Height * ((e.Delta > 0) ? 0.1 : -0.1);

            var newVOffset = e.GetPosition(_scrollViewer).Y + deltaY/2;
            _scrollViewer.ScrollToVerticalOffset(newVOffset);

            var newHOffset = e.GetPosition(_scrollViewer).X + deltaX/2;
            _scrollViewer.ScrollToHorizontalOffset(newHOffset);

            _content.Width += deltaX;
            _content.Height += deltaY;
        }

        private void scrollViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _content.ReleaseMouseCapture();
        }
    }
}
