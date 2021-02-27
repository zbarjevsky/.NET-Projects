﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MkZ.WPF
{
    public static class DraggableExtension
    {
 
         private static HashSet<Window> _draggableWindows = new HashSet<Window>();
        public static void Draggable(this Window wnd, bool enable = true)
        {
            if (enable)
            {
                if (_draggableWindows.Contains(wnd))
                    return;
                _draggableWindows.Add(wnd);
                wnd.MouseDown += Wnd_MouseDown;
            }
            else
            {
                if (!_draggableWindows.Contains(wnd))
                    return;
                wnd.MouseDown -= Wnd_MouseDown;
                _draggableWindows.Remove(wnd);
            }
        }

        private static void Wnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if(sender is Window wnd)
                    wnd.DragMove();
            }
        }

       private class DraggableData
        {
            public FrameworkElement Element;
            public bool IsDragging = false;
            public Cursor Cursor;
            public Action OnChange = null;

            private Thickness _margin = new Thickness();

            /// <summary>
            /// If dimention is less than one use it as fraction of element dimention
            /// like: if _margin.Width == 0.5 use half of elements width
            /// </summary>
            public Thickness Margin
            {
                get 
                {
                    return new Thickness() 
                    {
                        Left = EvaluateMargin(_margin.Left, Element.RenderSize.Width),
                        Right = EvaluateMargin(_margin.Right, Element.RenderSize.Width),
                        Top = EvaluateMargin(_margin.Top, Element.RenderSize.Height),
                        Bottom = EvaluateMargin(_margin.Bottom, Element.RenderSize.Height),
                    };
                }
            }

            public TranslateTransform Translate { get; private set; }

            public DraggableData(FrameworkElement element, Thickness margin)
            {
                Element = element;
                _margin = margin;
                Cursor = element.Cursor;
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

            private double EvaluateMargin(double margin, double length)
            {
                if (margin > -1 && margin < 1)
                    margin = margin * length;
                return margin;
            }
        }

        // TKey is control to drag, TValue is a flag used while dragging
        private static Dictionary<FrameworkElement, DraggableData> _draggables = new Dictionary<FrameworkElement, DraggableData>();
        private static Point _initialMouseOffset;
        private static Vector _initialTranslateOffset;

        public static void DraggableSubscribeForChange(this FrameworkElement element, Action onChange)
        {
            if (!_draggables.ContainsKey(element))
            {  // return if control is not draggable
                return;
            }

            _draggables[element].OnChange = onChange;
        }

        /// <summary>
        /// Enabling/disabling dragging for control
        /// </summary>
        public static void Draggable(this FrameworkElement element, bool Enable = true, Thickness margin = default(Thickness))
        {
            if (Enable)
            {
                // enable drag feature
                if (_draggables.ContainsKey(element))
                {   // return if control is already draggable
                    return;
                }

                // 'false' - initial state is 'not dragging'
                _draggables.Add(element, new DraggableData(element, margin));

                // assign required event handlersnnn
                element.MouseDown += control_MouseDown;
                element.MouseUp += control_MouseUp;
                element.MouseMove += control_MouseMove;
                element.SizeChanged += control_SizeChanged;

                if (element.Parent is FrameworkElement parent)
                    parent.SizeChanged += Parent_SizeChanged;
            }
            else
            {
                // disable drag feature
                if (!_draggables.ContainsKey(element))
                {  // return if control is not draggable
                    return;
                }

                _draggables[element].OnChange = null;

                // remove event handlers
                element.MouseDown -= control_MouseDown;
                element.MouseUp -= control_MouseUp;
                element.MouseMove -= control_MouseMove;

                if (element.Parent is FrameworkElement parent)
                    parent.SizeChanged -= Parent_SizeChanged;

                _draggables.Remove(element);
            }
        }

        public static Vector GetDraggableOffset(this FrameworkElement element)
        {
            if (!_draggables.ContainsKey(element))
                return new Vector();

            TranslateTransform translate = _draggables[element].Translate;
            if (translate == null)
                return new Vector();

            return new Vector(translate.X, translate.Y);
        }

        public static void SetDraggableOffset(this FrameworkElement element, Vector offset, bool bAbsoluteOffset)
        {
            if (!_draggables.ContainsKey(element))
                return;

            TranslateTransform translate = _draggables[element].Translate;
            if (translate == null)
                return;

            Vector initialOffset = new Vector(translate.X, translate.Y);
            if (bAbsoluteOffset)
                initialOffset = new Vector(); //from 0,0

            ApplyOffsetEnsureInsideMargin(element, initialOffset, offset);
        }

        private static void control_MouseDown(object sender, MouseButtonEventArgs e)
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
            element.Cursor = Cursors.SizeAll;
        }

        private static void control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // turning off dragging
            _draggables[(FrameworkElement)sender].IsDragging = false;

            FrameworkElement element = sender as FrameworkElement;
            element.ReleaseMouseCapture();
            element.Cursor = _draggables[(FrameworkElement)sender].Cursor;
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

            ApplyOffsetEnsureInsideMargin(element, _initialTranslateOffset, offset);
        }

        private static void control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                TranslateTransform translate = _draggables[element].Translate;
                if (translate == null)
                    return;

                //ensure inside margin - still visible
                Vector initialOffset = new Vector(translate.X, translate.Y);
                ApplyOffsetEnsureInsideMargin(element, initialOffset, new Vector());
            }
        }

        private static void Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement parent = sender as FrameworkElement;
            if (parent == null)
                return;

            FrameworkElement element = FindElementFromParent(parent);
            if (element == null)
                return;

            TranslateTransform translate = _draggables[element].Translate;
            if (translate == null)
                return;

            //ensure inside margin - still visible
            Vector initialOffset = new Vector(translate.X, translate.Y);
            ApplyOffsetEnsureInsideMargin(element, initialOffset, new Vector());
        }

        private static void ApplyOffsetEnsureInsideMargin(FrameworkElement element, Vector initialOffset, Vector offset)
        {
            if (!_draggables.ContainsKey(element))
                return;

            Thickness margin = _draggables[element].Margin;

            TranslateTransform translate = _draggables[element].Translate;
            if (translate == null)
                return;

            Rect bounds = RelativeLocation(element);
            //Debug.WriteLine("location: " + bounds);

            Vector offsetDelta = new Vector(offset.X, offset.Y) - (new Vector(translate.X, translate.Y) - initialOffset);

            double corrX = CalculateCorrectionInsideMargin(bounds.X, offsetDelta.X, element.RenderSize.Width, bounds.Width, margin.Left, margin.Right);
            translate.X = initialOffset.X + offset.X - corrX;
            double corrY = CalculateCorrectionInsideMargin(bounds.Y, offsetDelta.Y, element.RenderSize.Height, bounds.Height, margin.Top, margin.Bottom);
            translate.Y = initialOffset.Y + offset.Y - corrY;

            if (_draggables[element].OnChange != null)
                _draggables[element].OnChange();
        }

        //calculate correction to stay inside margin
        private static double CalculateCorrectionInsideMargin(
            double pos, double offsetDelta, 
            double ctrlSize, double parentSize, 
            double marginLo, double marginHi)
        {
            if (pos + offsetDelta < marginLo)
            {
                return offsetDelta - (marginLo - pos); //move upto margin
            }

            double end = pos + ctrlSize;
            if (end + offsetDelta > parentSize - marginHi)
            {
                return offsetDelta - (parentSize - marginHi - end); //move upto margin
            }

            return 0; //no correction
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

        private static FrameworkElement FindElementFromParent(FrameworkElement parent)
        {
            if (_draggables == null || _draggables.Keys.Count == 0)
                return null;

            foreach (FrameworkElement element in _draggables.Keys)
            {
                if (object.ReferenceEquals(element.Parent, parent))
                    return element;
            }
            return null;
        }
    }
}
