using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using System.Windows;

namespace MultiPlayer.MkZ.WPF
{
    public static class ColorUtils
    {
        public static SolidColorBrush CalculateAverageColor(string fileName)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Bitmap bmp = new Bitmap(fileName);
            Color c = CalculateAverageColor(bmp);

            Debug.WriteLine("Color calculated time (from file): " + sw.Elapsed);

            return new SolidColorBrush(System.Windows.Media.Color.FromRgb(c.R, c.G, c.B));
        }

        public static SolidColorBrush CalculateAverageColor(FrameworkElement element)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Bitmap bmp = ScreenshotHelper.CaptureScreenArea(element); 
            Color c = CalculateAverageColor(bmp);
            
            SaveBitmapToTempFile(bmp);

            Debug.WriteLine("Color calculated time (from UI): " + sw.Elapsed);

            return new SolidColorBrush(System.Windows.Media.Color.FromRgb(c.R, c.G, c.B));
        }

        private static void SaveBitmapToTempFile(Bitmap bmp)
        {
            //Color c = await Task.Run(() =>
            //{
            string dir = "C:\\Temp\\1";
            Directory.CreateDirectory(dir);

            string fileName = string.Format("{0}\\Bitmap_{1}.jpg", dir, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss.fff"));
            if (bmp.Width > 30)
                bmp.Save(fileName, ImageFormat.Jpeg);

            //c = Color.Pink;
            //});
        }

        //https://stackoverflow.com/questions/6177499/how-to-determine-the-background-color-of-document-when-there-are-3-options-usin/6185448#6185448
        public static Color CalculateAverageColor(Bitmap bm)
        {
            int width = bm.Width;
            int height = bm.Height;
            int red = 0;
            int green = 0;
            int blue = 0;
            int minDiversion = 15; // drop pixels that do not differ by at least minDiversion between color values (white, gray or black)
            int dropped = 0; // keep track of dropped pixels
            long[] totals = new long[] { 0, 0, 0 };

            int bppModifier = BppModifier(bm.PixelFormat);
            if (bppModifier < 0)
                return Color.Black; //not supported

            BitmapData srcData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
            int stride = srcData.Stride;
            nint Scan0 = srcData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int idx = y * stride + x * bppModifier;
                        red = p[idx + 2];
                        green = p[idx + 1];
                        blue = p[idx];
                        if (red == green && green == blue) //gray point
                        {
                            totals[2] += red;
                            totals[1] += green;
                            totals[0] += blue;
                        }
                        else if (Math.Abs(red - green) > minDiversion || Math.Abs(red - blue) > minDiversion || Math.Abs(green - blue) > minDiversion)
                        {
                            totals[2] += red;
                            totals[1] += green;
                            totals[0] += blue;
                        }
                        else
                        {
                            dropped++;
                        }
                    }
                }
            }

            int count = width * height - dropped;
            int avgR = (int)(totals[2] / count);
            int avgG = (int)(totals[1] / count);
            int avgB = (int)(totals[0] / count);

            return Color.FromArgb(avgR, avgG, avgB);
        }

        // cutting corners, will fail on anything else but 32 and 24 bit images
        private static int BppModifier(PixelFormat pixelFormat)
        {
            if (pixelFormat == PixelFormat.Format32bppArgb ||
                pixelFormat == PixelFormat.Format32bppRgb ||
                pixelFormat == PixelFormat.Format32bppPArgb)
                return 4;

            if (pixelFormat == PixelFormat.Format24bppRgb)
                return 3;

            return -1;
        }
    }
}
