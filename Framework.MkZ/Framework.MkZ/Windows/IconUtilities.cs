using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MkZ.Windows
{
    public static class IconUtilities
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        public static ImageSource ConvertToImageSource(this Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                throw new Win32Exception();
            }

            return wpfBitmap;
        }

        public static System.Windows.Controls.Image ConvertToImage(this Icon icon)
        {
            return new System.Windows.Controls.Image
            {
                Source = Imaging.CreateBitmapSourceFromHIcon(icon.Handle,
                            new Int32Rect(0, 0, icon.Width, icon.Height),
                            BitmapSizeOptions.FromEmptyOptions())
            };
        }

        public static System.Windows.Controls.Image GetIconFromFile(string fileName)
        {
            return new System.Windows.Controls.Image
            {
                Source = new BitmapImage(new Uri(fileName, UriKind.Relative))
            };
        }

        private static System.Windows.Controls.Image ConvertPathToImage(string PathPath, System.Windows.Media.Brush brush)
        {
            Geometry gp = Geometry.Parse(PathPath);
            GeometryDrawing gd = new GeometryDrawing(brush, new System.Windows.Media.Pen(brush, 1.0), gp);
            DrawingImage di = new DrawingImage { Drawing = gd };
            return new System.Windows.Controls.Image() { Source = di };
        }
    }
}
