using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RulerWPF
{
    public class Utils
    {
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
        public static Point DPPX(Visual visual)
        {
            return ScaleFromVisual(visual);
        }

        //dot per mm
        public static Point DPCM(Visual visual)
        {
            const double inch2cm = 2.54;

            Point dpi = DPI(visual);
            Point dpcm = new Point(dpi.X / inch2cm, dpi.Y / inch2cm);
            return dpcm;
        }

        public static Point DPI(Visual visual)
        {
            PresentationSource source = PresentationSource.FromVisual(visual);
            Point dpi = new Point(96, 96);
            if (source != null)
            {
                dpi.X = 96.0 * source.CompositionTarget.TransformToDevice.M11;
                dpi.Y = 96.0 * source.CompositionTarget.TransformToDevice.M22;
            }

            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.height?redirectedfrom=MSDN&view=netframework-4.8#System_Windows_FrameworkElement_Height
            //px(default) is device-independent units(1/96th inch per unit)
            //in is inches; 1in==96px
            //cm is centimeters; 1cm==(96/2.54) px
            //pt is points; 1pt==(96/72) px
            const double scale = (96.0 / 72.0);

            dpi.X *= scale;
            dpi.Y *= scale;

            return dpi;
        }

        public static Point MouseLocationOnScreenScaled(Visual visual)
        {
            return ScaleLocationUp(Mouse.GetPosition(null), visual);
        }

        public static Point ScaleFromVisual(Visual visual)
        {
            PresentationSource source = PresentationSource.FromVisual(visual);
            Point scale = new Point(
                source.CompositionTarget.TransformToDevice.M11,
                source.CompositionTarget.TransformToDevice.M22);
            return scale;
        }

        public static Point ScaleLocationUp(Point pt, Visual visual)
        {
            Point scale = ScaleFromVisual(visual);
            scale = new Point(pt.X * scale.X, pt.Y * scale.Y);
            return scale;
        }

        public static Point ScaleLocationDown(Point pt, Visual visual)
        {
            Point scale = ScaleFromVisual(visual);
            scale = new Point(pt.X / scale.X, pt.Y / scale.Y);
            return scale;
        }
    }
}
