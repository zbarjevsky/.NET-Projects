using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
