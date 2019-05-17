using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RulerWPF
{
    public static class DraggableExtension
    {
        // TKey is control to drag, TValue is a flag used while dragging
        private static Dictionary<FrameworkElement, bool> _draggables = new Dictionary<FrameworkElement, bool>();
        private static Point _mouseOffset;
        private static Vector _translateOffset;

        /// <summary>
        /// Enabling/disabling dragging for control
        /// </summary>
        public static void Draggable(this FrameworkElement element, bool Enable = true)
        {
            if (Enable)
            {
                // enable drag feature
                if (_draggables.ContainsKey(element))
                {   // return if control is already draggable
                    return;
                }

                TranslateTransform translate = FindTranslateTransform(element);
                if(translate == null)
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

        private static TranslateTransform FindTranslateTransform(FrameworkElement element)
        {
            if (element.RenderTransform is TransformGroup)
            {
                TransformGroup g = element.RenderTransform as TransformGroup;
                foreach (var item in g.Children)
                {
                    if (item is TranslateTransform)
                        return item as TranslateTransform;
                }
            }
            else if (element.RenderTransform is TranslateTransform)
            {
                return element.RenderTransform as TranslateTransform;
            }

            return null;
        }

        static void control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null)
                return;

            TranslateTransform translate = FindTranslateTransform(element);
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

            TranslateTransform translate = FindTranslateTransform(element);
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

            Point newLocationOffset = UpdateOffset(element, translate, offset, 5000.0);

            translate.X = _translateOffset.X + newLocationOffset.X;
            translate.Y = _translateOffset.Y + newLocationOffset.Y;
        }

        const int HARD_MARGIN = 200;
        private static Point UpdateOffset(FrameworkElement element, TranslateTransform transform, Vector offset, double margin = HARD_MARGIN)
        {

            FrameworkElement parent = element.Parent as FrameworkElement;
            Size parentSize = parent.RenderSize;
            Point location = RelativeLocation(element);
            //Debug.WriteLine("location: " + location);

            Point newOffset = new Point();
            newOffset.X = UpdateOffset(location.X, offset.X, element.RenderSize.Width, parentSize.Width, margin);
            newOffset.Y = UpdateOffset(location.Y, offset.Y, element.RenderSize.Height, parentSize.Height, margin);
            //Debug.WriteLine("new offset: " + newOffset);

            return newOffset;
        }

        private static double UpdateOffset(double pos, double delta, double ctrlSize, double parentSize, double margin)
        {
            if (pos + delta < -margin)
            {
                delta = -margin - pos;
            }

            double end = pos + ctrlSize;
            if (end + delta > parentSize + margin)
            {
                delta = parentSize + margin - end;
            }

            return delta;
        }

        private static Point RelativeLocation(FrameworkElement element)
        {
            FrameworkElement parent = element.Parent as FrameworkElement;
            Size parentSize = parent.RenderSize;
            Point pos = element.TransformToAncestor(parent).Transform(new Point(0, 0));
            return pos;
        }
    }
}
