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

        private const double MIN_ZOOM = 0.1;
        private const double MAX_ZOOM = 10.0;
        private double _zoom = 1.0;
        public double Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < MIN_ZOOM) _zoom = MIN_ZOOM;
                if (_zoom > MAX_ZOOM) _zoom = MAX_ZOOM;

                _content.Width = _zoom * _origWidth;
                _content.Height = _zoom * _origHeight;

                _content.UpdateLayout();

                SizeChangedAction();
            }
        }

        private void InternalUpdateZoomFromContent()
        {
            double width = _content.ActualWidth > 16 ? _content.ActualWidth : _content.Width;
            _zoom = width / _origWidth;
            if (_zoom < MIN_ZOOM) _zoom = MIN_ZOOM;
            if (_zoom > MAX_ZOOM) _zoom = MAX_ZOOM;
        }

        private double _origWidth, _origHeight;
        public Size NaturalSize
        {
            get { return new Size(_origWidth, _origHeight); }
            set 
            { 
                _origWidth = value.Width; 
                _origHeight = value.Height;
                InternalUpdateZoomFromContent();
            }
        }

        public Action SizeChangedAction { get; internal set; } = () => { };

        public ScrollDragZoom(FrameworkElement content, ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
            _content = content;

            if (_content != null)
            {
                _origWidth = _content.Width;
                _origHeight = _content.Height;

                _zoom = 1;

                content.MouseRightButtonDown += scrollViewer_MouseRightButtonDown;
                content.PreviewMouseMove += scrollViewer_PreviewMouseMove;
                content.PreviewMouseRightButtonUp += scrollViewer_PreviewMouseRightButtonUp;
                content.MouseWheel += content_MouseWheel;
            }
        }

        public void UpdateLayout()
        {
            if (_content != null)
                _content.UpdateLayout();

            if(_scrollViewer  != null)
                _scrollViewer.UpdateLayout();
        }

        public void ScrollToCenter()
        {
            _scrollViewer.UpdateLayout();
            _scrollViewer.ScrollToVerticalOffset(_scrollViewer.ScrollableHeight / 2);
            _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.ScrollableWidth / 2);
        }

        public void FitWidth(double margin = 18)
        {
            if (_scrollViewer.ActualWidth < margin)
                return;

            _content.Width = _scrollViewer.ActualWidth - margin;

            //proportionally change height
            _content.Height = _content.Width * NaturalSize.Height / NaturalSize.Width;
            _content.UpdateLayout();

            InternalUpdateZoomFromContent();

            SizeChangedAction();
        }

        public void OriginalSize()
        {
            _content.Width = NaturalSize.Width;
            _content.Height = NaturalSize.Height;
            _zoom = 1;

            SizeChangedAction();
        }

        internal void FitWindow(double margin = 18)
        {
            if (_scrollViewer.ActualHeight > margin && _scrollViewer.ActualWidth > margin)
            {
                _content.Width = _scrollViewer.ActualWidth - 18;
                _content.Height = _scrollViewer.ActualHeight - 18;
                _content.UpdateLayout();
            }

            InternalUpdateZoomFromContent();

            ScrollToCenter();

            SizeChangedAction();
        }

        private void content_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoom = _zoom * ((e.Delta > 0) ? 1.1 : 0.9);
            Zoom = zoom; //update control
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
