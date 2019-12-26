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
        private class DraggableData
        {
            public FrameworkElement Element;
            public bool IsDragging = false;

            private Size _margin = new Size();

            /// <summary>
            /// If dimention is less than one use it as fraction of element dimention
            /// like: if _margin.Width == 0.5 use half of elements width
            /// </summary>
            public Size Margin
            {
                get 
                {
                    Size margin = _margin;
                    if (_margin.Width > -1 && _margin.Width < 1)
                        margin.Width = _margin.Width * Element.RenderSize.Width;
                    if (_margin.Height > -1 && _margin.Height < 1)
                        margin.Height = _margin.Height * Element.RenderSize.Height;
                    return margin;
                }
            }

            public TranslateTransform Translate { get; private set; }

            public DraggableData(FrameworkElement element, double marginX, double marginY)
            {
                Element = element;
                _margin = new Size(marginX, marginY);
                InitTransform();
            }

            private void InitTransform()
            {
                if (Element.RenderTransform == null)
                {
                    Element.RenderTransform = Translate = new TranslateTransform();
                }
                else if (Element.RenderTransform is TranslateTransform t1)
                {
                    Translate = t1;
                }
                else if (Element.RenderTransform is TransformGroup group1)
                {
                    foreach (Transform item in group1.Children)
                    {
                        if (item is TranslateTransform t2)
                            Translate = t2;
                    }

                    //not found - create
                    if(Translate == null)
                    {
                        Translate = new TranslateTransform();
                        group1.Children.Add(Translate);
                    }
                }
                else if (Element.RenderTransform != null)
                {
                    Transform t3 = Element.RenderTransform;
                    Translate = new TranslateTransform();
                    TransformGroup group2 = new TransformGroup();
                    group2.Children.Add(t3);
                    group2.Children.Add(Translate);
                    Element.RenderTransform = group2;
                }
            }
        }

        // TKey is control to drag, TValue is a flag used while dragging
        private static Dictionary<FrameworkElement, DraggableData> _draggables = new Dictionary<FrameworkElement, DraggableData>();
        private static Point _initialMouseOffset;
        private static Vector _initialTranslateOffset;

        /// <summary>
        /// Enabling/disabling dragging for control
        /// </summary>
        public static void Draggable(this FrameworkElement element, bool Enable = true, double marginX = 0, double marginY = 0)
        {
            if (Enable)
            {
                // enable drag feature
                if (_draggables.ContainsKey(element))
                {   // return if control is already draggable
                    return;
                }

                // 'false' - initial state is 'not dragging'
                _draggables.Add(element, new DraggableData(element, marginX, marginY));

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

            TranslateTransform translate = _draggables[element].Translate;
            if (translate == null)
                return;

            _initialMouseOffset = element.PointToScreen(e.GetPosition(element));
            _initialTranslateOffset = new Vector(translate.X, translate.Y);

            // turning on dragging
            _draggables[element].IsDragging = true;

            element.CaptureMouse();
        }

        static void control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // turning off dragging
            _draggables[(FrameworkElement)sender].IsDragging = false;

            FrameworkElement element = sender as FrameworkElement;
            element.ReleaseMouseCapture();
        }

        private static void control_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null)
                return;

            if (!_draggables[element].IsDragging || !element.IsMouseCaptured)
                return;

            TranslateTransform translate = _draggables[element].Translate;
            if (translate == null)
                return;

            // only if dragging is turned on
            // calculations of control's new position
            Point pt = element.PointToScreen(e.GetPosition(element));
            Vector offset = pt - _initialMouseOffset;
            if (offset.Length < 0.0001)
                return;

            Size margin = _draggables[element].Margin;

            Rect bounds = RelativeLocation(element);
            Debug.WriteLine("location: " + bounds);

            Vector offsetDelta = new Vector(offset.X, offset.Y) - (new Vector(translate.X, translate.Y) - _initialTranslateOffset);
            
            if (IsNewOffsetInsideMargin(bounds.X, offsetDelta.X, element.RenderSize.Width, bounds.Width, margin.Width))
                translate.X = _initialTranslateOffset.X + offset.X;
            if (IsNewOffsetInsideMargin(bounds.Y, offsetDelta.Y, element.RenderSize.Height, bounds.Height, margin.Height))
                translate.Y = _initialTranslateOffset.Y + offset.Y;
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
