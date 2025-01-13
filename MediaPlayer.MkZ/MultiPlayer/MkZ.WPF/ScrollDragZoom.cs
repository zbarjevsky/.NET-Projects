using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cursors = System.Windows.Input.Cursors;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace MkZ.WPF
{
    public enum eZoomState
    {
        Custom = 0, //manual zoom
        Original = 1, // zoom 1:1
        FitWidth,
        FitWindow,
        FitHeight
    }

    /// <summary>
    /// Drag on Right Mouse only if scrolls are enable - content bigger than scrollView
    /// </summary>
    public class ScrollDragZoom : IDisposable
    {
        private ScrollViewer _scrollViewer;
        private FrameworkElement _content;
        private Point _scrollMousePoint;
        private double _vOff = 1, _hOff = 1;

        public eZoomState ZoomState { get; private set; } = eZoomState.Custom;

        public double VerticalOffset
        {
            get => _scrollViewer.VerticalOffset;
            set => _scrollViewer.ScrollToVerticalOffset(value);
        }

        public Point ScrollOffset
        {
            get { return new Point(_scrollViewer.HorizontalOffset, _scrollViewer.VerticalOffset); }
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

                ZoomState = eZoomState.Custom;

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

        public Action SizeChangedAction { get; set; } = () => { };

        public ScrollDragZoom(FrameworkElement content, ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
            _content = content;

            if (_content != null)
            {
                _origWidth = _content.Width;
                _origHeight = _content.Height;

                _zoom = 1;

                _content.MouseRightButtonDown += content_MouseRightButtonDown;
                _content.PreviewMouseMove += content_PreviewMouseMove;
                _content.PreviewMouseRightButtonUp += content_PreviewMouseRightButtonUp;
                _content.MouseWheel += content_MouseWheel;
            }
        }

        public void Dispose()
        {
            if (_content != null)
            {
                _content.MouseRightButtonDown -= content_MouseRightButtonDown;
                _content.PreviewMouseMove -= content_PreviewMouseMove;
                _content.PreviewMouseRightButtonUp -= content_PreviewMouseRightButtonUp;
                _content.MouseWheel -= content_MouseWheel;
            }
            _content = null;
            _scrollViewer = null;
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
            ZoomState = eZoomState.FitWidth;
            if (_scrollViewer.ActualWidth < margin)
                return;

            _content.Width = _scrollViewer.ActualWidth - margin;

            //proportionally change height
            _content.Height = _content.Width * NaturalSize.Height / NaturalSize.Width;
            _content.UpdateLayout();

            InternalUpdateZoomFromContent();


            SizeChangedAction();
        }

        public void FitHeight(double margin = 18)
        {
            ZoomState = eZoomState.FitHeight;
            if (_scrollViewer.ActualHeight < margin)
                return;

            _content.Height = _scrollViewer.ActualHeight - margin;

            //proportionally change width
            _content.Width = _content.Height * NaturalSize.Width / NaturalSize.Height;
            _content.UpdateLayout();

            InternalUpdateZoomFromContent();


            SizeChangedAction();
        }

        public void OriginalSize()
        {
            ZoomState = eZoomState.Original;

            _content.Width = NaturalSize.Width;
            _content.Height = NaturalSize.Height;
            _zoom = 1;

            SizeChangedAction();
        }

        public void FitWindow(double margin = 18)
        {
            ZoomState = eZoomState.FitWindow;

            if (_scrollViewer.ActualHeight > margin && _scrollViewer.ActualWidth > margin)
            {
                //hide scrollbars - after fit they are still visible - so hide and then set to Auto
                _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

                _content.Width = _scrollViewer.ActualWidth - margin;
                _content.Height = _scrollViewer.ActualHeight - margin;
                
                _content.UpdateLayout();

                _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }

            InternalUpdateZoomFromContent();

            ScrollToCenter();

            SizeChangedAction();
        }

        private void content_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftShift))
            {
                e.Handled = true;

                //Vector delta = MoveContentAndMouseToCenterBeforeZoom(e);
                Point offsetOld = ScrollOffset;

                double deltaZoom = ((e.Delta > 0) ? 1.1 : 0.9);
                Zoom = _zoom * deltaZoom;

                Vector delta = MoveContentAndMouseToCenterAfterZoom(deltaZoom, offsetOld, e);
            }
        }

        private void content_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _content.Cursor = Cursors.SizeAll;
            _content.CaptureMouse();
            _scrollMousePoint = e.GetPosition(_scrollViewer);
            _vOff = _scrollViewer.VerticalOffset;
            _hOff = _scrollViewer.HorizontalOffset;
        }

        private void content_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_content.IsMouseCaptured)
            {
                var newVOffset = _vOff + (_scrollMousePoint.Y - e.GetPosition(_scrollViewer).Y);
                _scrollViewer.ScrollToVerticalOffset(newVOffset);

                var newHOffset = _hOff + (_scrollMousePoint.X - e.GetPosition(_scrollViewer).X);
                _scrollViewer.ScrollToHorizontalOffset(newHOffset);
            }
        }

        private void content_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _content.ReleaseMouseCapture();
            _content.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private Vector MoveContentAndMouseToCenterAfterZoom(double deltaZoom, Point offsetOld, MouseWheelEventArgs e)
        {
            Vector offsetDelta = ScrollOffset - offsetOld;

            Point mousePosContent = e.GetPosition(_content);
            Point mousePosScroll = e.GetPosition(_scrollViewer);
            Point center = new Point(_scrollViewer.ActualWidth / 2, _scrollViewer.ActualHeight / 2);
            Vector deltaMouse = center - mousePosScroll; //for mouse pointer to move to center

            Point posAfterZoom = new Point(mousePosContent.X * deltaZoom, mousePosContent.Y * deltaZoom);
            Vector deltaVideoPointAfterZoom = posAfterZoom - mousePosContent; //for video point that was under mouse
            Vector deltaToMoveTocenter = deltaMouse - deltaVideoPointAfterZoom + offsetDelta;

            //try scroll from mouse pointer to scrollview center as close as possible
            Vector newDelta = TryMoveScroll(deltaToMoveTocenter);
            Vector deltaChange = deltaToMoveTocenter - newDelta;

            Point newMousePos = center - deltaChange;// - offsetDelta;

            Vector newDeltaMouseNoScroll = CalcNewMousePosDeltaIfNoScroll(mousePosContent, deltaZoom);
            if (_scrollViewer.ScrollableWidth == 0)
                newMousePos.X = mousePosScroll.X + newDeltaMouseNoScroll.X - offsetDelta.X;
            if (_scrollViewer.ScrollableHeight == 0)
                newMousePos.Y = mousePosScroll.Y + newDeltaMouseNoScroll.Y - offsetDelta.Y;

            if (deltaChange.Length < 0.001)
            {
            }
            else
            {
            }

            //_scrollViewer.SetMousePosition(newMousePos);

            return deltaChange;
        }

        private Vector CalcNewMousePosDeltaIfNoScroll(Point mousePosContent, double deltaZoom)
        {
            //find point that was under mouse before this change
            double x = CalcNewMousePosDeltaIfNoScroll(mousePosContent.X, _content.ActualWidth, deltaZoom);
            double y = CalcNewMousePosDeltaIfNoScroll(mousePosContent.Y, _content.ActualHeight, deltaZoom);

            return new Vector(x, y);
        }

        private double CalcNewMousePosDeltaIfNoScroll(double currentPos, double length, double deltaZoom)
        {
            //find offset to point that was under mouse before this change
            double center = length / 2;
            double delta = center - currentPos;
            double oldDelta = delta / deltaZoom;
            double offset = oldDelta - delta;
            
            return offset;
        }

        private Vector TryMoveScroll(Vector delta)
        {
            double hOff = _scrollViewer.HorizontalOffset - delta.X;
            if (hOff < 0)
                hOff = 0;
            if (hOff > _scrollViewer.ScrollableWidth)
                hOff = _scrollViewer.ScrollableWidth;

            double vOff = _scrollViewer.VerticalOffset - delta.Y;
            if (vOff < 0)
                vOff = 0;
            if (vOff > _scrollViewer.ScrollableHeight)
                vOff = _scrollViewer.ScrollableHeight;

            Vector newDelta = new Vector(
                _scrollViewer.HorizontalOffset - hOff,
                _scrollViewer.VerticalOffset - vOff);

            if(hOff != _scrollViewer.HorizontalOffset)
                _scrollViewer.ScrollToHorizontalOffset(hOff);

            if(vOff != _scrollViewer.VerticalOffset)
                _scrollViewer.ScrollToVerticalOffset(vOff);

            _scrollViewer.UpdateLayout();

            return newDelta;
        }

        private Vector MoveContentAndMouseToCenterBeforeZoom(MouseWheelEventArgs e)
        {
            Point mousePosContent = e.GetPosition(_content);
            Point mousePos = e.GetPosition(_scrollViewer);
            Point center = new Point(_scrollViewer.ActualWidth / 2, _scrollViewer.ActualHeight / 2);
            Vector deltaMouse = center - mousePos; //for mouse pointer to move to center

            //try scroll from mouse pointer to center as close as possible
            Vector newDelta = TryMoveScroll(deltaMouse);
            //_scrollViewer.SetMousePosition(mousePos + newDelta);

            return newDelta;
        }
    }
}
