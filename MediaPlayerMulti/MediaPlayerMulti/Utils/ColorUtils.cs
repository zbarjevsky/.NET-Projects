using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MkZ.MediaPlayer.Utils
{
    public static class ColorUtils
    {
        public static System.Windows.Media.SolidColorBrush CalculateAverageColor(string fileName)
        {
            Bitmap bmp = new Bitmap(fileName);
            Color c = CalculateAverageColor(bmp);
            return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(c.R, c.G, c.B));
        }

        //https://stackoverflow.com/questions/6177499/how-to-determine-the-background-color-of-document-when-there-are-3-options-usin/6185448#6185448
        public static System.Drawing.Color CalculateAverageColor(Bitmap bm)
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

            BitmapData srcData = bm.LockBits(new System.Drawing.Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
            int stride = srcData.Stride;
            IntPtr Scan0 = srcData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int idx = (y * stride) + x * bppModifier;
                        red = p[idx + 2];
                        green = p[idx + 1];
                        blue = p[idx];
                        if(red == green && green == blue) //gray point
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

            return System.Drawing.Color.FromArgb(avgR, avgG, avgB);
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
