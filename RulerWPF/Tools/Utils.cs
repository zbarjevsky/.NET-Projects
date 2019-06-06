using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace RulerWPF
{
    public class Utils
    {
        private static CompositionTarget _CompositionTarget;

        public static double ScaleWPF { get { return _CompositionTarget.TransformToDevice.M11; } }

        // Return minimum distance between line segment vw and point p
        public static double MinimumDistance(Point v, Point w, Point p)
        {
            return Distance(p.X, p.Y, v.X, v.Y, w.X, w.Y);
        }

        public static double MinimumDistance(Rect bounds, Point p)
        {
            // Return minimum distance between line segment vw and point p
            double[] distance =
            {
                MinimumDistance(bounds.TopLeft, bounds.TopRight, p),
                MinimumDistance(bounds.TopRight, bounds.BottomRight, p),
                MinimumDistance(bounds.BottomRight, bounds.BottomLeft, p),
                MinimumDistance(bounds.BottomLeft, bounds.TopLeft, p)
            };
            return distance.Min();
        }

        public static double Distance(double x, double y, double x1, double y1, double x2, double y2)
        {
            double A = x - x1;
            double B = y - y1;
            double C = x2 - x1;
            double D = y2 - y1;

            double dot = A * C + B * D;
            double len_sq = C * C + D * D;
            double param = -1;
            if (len_sq != 0) //in case of 0 length line
                param = dot / len_sq;

            double xx, yy;

            if (param < 0)
            {
                xx = x1;
                yy = y1;
            }
            else if (param > 1)
            {
                xx = x2;
                yy = y2;
            }
            else
            {
                xx = x1 + param * C;
                yy = y1 + param * D;
            }

            double dx = x - xx;
            double dy = y - yy;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        //dot per mm
        public static Point DPCM
        {
            get
            {
                const double inch2cm = 2.54;

                Point dpi = DPI;
                Point dpcm = new Point(dpi.X / inch2cm, dpi.Y / inch2cm);
                return dpcm;
            }
        }

        public static Point DPI
        {
            get
            {
                //https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?redirectedfrom=MSDN&view=netframework-4.8#System_Windows_FrameworkElement_Height
                //px(default) is device-independent units(1/96th inch per unit)
                //in is inches; 1in==96px
                //cm is centimeters; 1cm==(96/2.54) px
                //pt is points; 1pt==(96/72) px
                const double scale = (96.0 / 72.0);

                Point dpi = new Point(96, 96);
                dpi.X *= scale;
                dpi.Y *= scale;

                return dpi;
            }
        }

        public static Point GetMousePosition()
        {
            return Mouse.GetPosition(null);
        }

        public static Point ScaleLocationUp(Point pt)
        {
            double scale = ScaleFromGraphics();
            Point ptScaled = new Point(pt.X * scale, pt.Y * scale);
            return ptScaled;
        }

        public static Point ScaleLocationDown(Point pt)
        {
            double scale = ScaleFromGraphics();
            Point ptScaled = new Point(pt.X / scale, pt.Y / scale);
            return ptScaled;
        }

        public static Point ScaleToWPF_DPI(Point pt)
        {
            return _CompositionTarget.TransformFromDevice.Transform(pt);
            //double scale = 1.0;
            //Point ptScaled = new Point(pt.X / scale, pt.Y / scale);
            //return ptScaled;
        }

        public static Point ScaleFromWPF_DPI(Point pt)
        {
            return _CompositionTarget.TransformToDevice.Transform(pt);
            //double scale = 1.0;
            //Point ptScaled = new Point(pt.X * scale, pt.Y * scale);
            //return ptScaled;
        }

        public static Size GetElementPixelSize(UIElement element)
        {
            Matrix transformToDevice;
            var source = PresentationSource.FromVisual(element);
            if (source != null)
                transformToDevice = source.CompositionTarget.TransformToDevice;
            else
                using (var source1 = new HwndSource(new HwndSourceParameters()))
                    transformToDevice = source1.CompositionTarget.TransformToDevice;

            if (element.DesiredSize == new Size()) //not measured yet
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            return (Size)transformToDevice.Transform((Vector)element.DesiredSize);
        }

        public static void UpdateScaleWPF(PresentationSource source)
        {
            _CompositionTarget = source.CompositionTarget;
        }

        public static double ScaleFromGraphics()
        {
            return SystemParameters.VirtualScreenHeight / SystemParameters.PrimaryScreenHeight;
        }
    }
}
