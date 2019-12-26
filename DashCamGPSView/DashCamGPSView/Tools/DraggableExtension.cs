using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DashCamGPSView.Tools
{
    public static class DraggableExtension
    {
        // TKey is control to drag, TValue is a flag used while dragging
        private static Dictionary<FrameworkElement, bool> _draggables = new Dictionary<FrameworkElement, bool>();
        private static Point _mouseOffset;
        private static Vector _translateOffset;

        private static double MARGIN = 5000; //do not let drag it beyond margin

        /// <summary>
        /// Enabling/disabling dragging for control
        /// </summary>
        public static void Draggable(this FrameworkElement element, bool Enable = true, double margin = 5000)
        {
            MARGIN = margin;

            if (Enable)
            {
                // enable drag feature
                if (_draggables.ContainsKey(element))
                {   // return if control is already draggable
                    return;
                }

                element.RenderTransform = new TranslateTransform();

                // 'false' - initial state is 'not dragging'
                _draggables.Add(element, false);

                // assign required event handlersnnn
                element.MouseDown += control_MouseDown;
                element.MouseUp += control_MouseUp;
                element.MouseMove += control_MouseMove;
            }
            else
            {
                // disable drag feature
                if (!_draggables.ContainsKey(element))
                {  // return if control is not draggable
                    return;
                }
                // remove event handlers
                element.MouseDown -= control_MouseDown;
                element.MouseUp -= control_MouseUp;
                element.MouseMove -= control_MouseMove;
                _draggables.Remove(element);
            }
        }

        static void control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null)
                return;

            TranslateTransform translate = element.RenderTransform as TranslateTransform;
            if (translate == null)
                return;

            _mouseOffset = element.PointToScreen(e.GetPosition(element));
            _translateOffset = new Vector(translate.X, translate.Y);

            // turning on dragging
            _draggables[(FrameworkElement)sender] = true;

            element.CaptureMouse();
        }

        static void control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // turning off dragging
            _draggables[(FrameworkElement)sender] = false;

            FrameworkElement element = sender as FrameworkElement;
            element.ReleaseMouseCapture();
        }

        private static void control_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null)
                return;

            TranslateTransform translate = element.RenderTransform as TranslateTransform;
            if (translate == null)
                return;

            // only if dragging is turned on
            if (!_draggables[element] || !element.IsMouseCaptured)
                return;

            // calculations of control's new position
            Point pt = element.PointToScreen(e.GetPosition(element));
            Vector offset = pt - _mouseOffset;
            if (offset.Length < 0.0001)
                return;

            Rect bounds = RelativeLocation(element);
            Debug.WriteLine("location: " + bounds);

            Vector offsetDelta = new Vector(offset.X, offset.Y) - (new Vector(translate.X, translate.Y) - _translateOffset);
            
            if (IsNewOffsetInsideMargin(bounds.X, offsetDelta.X, element.RenderSize.Width, bounds.Width, MARGIN))
                translate.X = _translateOffset.X + offset.X;
            if (IsNewOffsetInsideMargin(bounds.Y, offsetDelta.Y, element.RenderSize.Height, bounds.Height, MARGIN))
                translate.Y = _translateOffset.Y + offset.Y;
        }

        private static bool IsNewOffsetInsideMargin(double pos, double offsetDelta, double ctrlSize, double parentSize, double margin)
        {
            if (offsetDelta == 0)
                return false;

            if (pos + offsetDelta <= -margin)
            {
                return false; //no move
            }

            double end = pos + ctrlSize;
            if (end + offsetDelta >= parentSize + margin)
            {
                return false; //no move
            }

            return true;
        }

        //rect.TopLeft is elemnt relative position to container grid row/column
        //rect.Size - is cell size counting rowSpan and column span 
        private static Rect RelativeLocation(FrameworkElement element)
        {
            FrameworkElement parent = element.Parent as FrameworkElement;

            Point pos = element.TransformToAncestor(parent).Transform(new Point(0, 0));
            
            Rect bounds = GetParentBoundRectFromElement(element);
            bounds.X = pos.X - bounds.X; //correct to row/col location
            bounds.Y = pos.Y - bounds.Y;
            return bounds;
        }

        private static Rect GetParentBoundRectFromElement(FrameworkElement element)
        {
            FrameworkElement parent = element.Parent as FrameworkElement;

            if (parent is Grid grid)
            {
                Rect r = new Rect();
                if (grid.ColumnDefinitions.Count == 0)
                    r.Width = parent.RenderSize.Width;
                if (grid.RowDefinitions.Count == 0)
                    r.Height = parent.RenderSize.Height;

                int rowIdx = Grid.GetRow(element);
                int colIdx = Grid.GetColumn(element);
                int rowSpan = Grid.GetRowSpan(element);
                int colSpan = Grid.GetColumnSpan(element);

                for (int col = 0; col < grid.ColumnDefinitions.Count; col++)
                {
                    if (col < colIdx)
                        r.X += grid.ColumnDefinitions[col].ActualWidth;
                    if (col >= colIdx && col < colIdx + colSpan)
                        r.Width += grid.ColumnDefinitions[col].ActualWidth;
                }

                for (int row = 0; row < grid.RowDefinitions.Count; row++)
                {
                    if (row < rowIdx)
                        r.Y += grid.RowDefinitions[row].ActualHeight;
                    if (row >= rowIdx && row < rowIdx + rowSpan)
                        r.Height += grid.RowDefinitions[row].ActualHeight;
                }

                return r;
            }
            else
            {
                return new Rect(new Point(), parent.RenderSize);
            }
        }
    }
}
