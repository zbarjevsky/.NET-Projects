using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RulerWPF
{
    public static class ExtensionMethods
    {
        public static double Length1(this Vector v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y);
        }
    }
}
