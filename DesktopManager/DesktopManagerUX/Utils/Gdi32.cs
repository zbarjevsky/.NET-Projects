using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DesktopManagerUX.Utils
{
    public static class Gdi32
    {
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        //public static BitmapSource GetScreenshot()
        //{
        //    // get the screen handle and size
        //    Screen s = UIHelper.getScreenHandle();
        //    Size sz = s.Bounds.Size;

        //    // capture the screen
        //    IntPtr hSrce = CreateDC(null, s.DeviceName, null, IntPtr.Zero);
        //    IntPtr hDest = CreateCompatibleDC(hSrce);
        //    IntPtr hBmp = CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
        //    IntPtr hOldBmp = SelectObject(hDest, hBmp);
        //    bool b = BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
        //    Bitmap bm = Bitmap.FromHbitmap(hBmp);
        //    SelectObject(hDest, hOldBmp);

        //    // convert the Bitmap to BitmapSource
        //    IntPtr hBitmap = bm.GetHbitmap();

        //    // Dispose bms if it holds content, Garbage Collector will do the cleaning
        //    //if (bms != null) bms = null;

        //    // create BitmapSource from Bitmap
        //    BitmapSource bms = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
        //        hBitmap,
        //        IntPtr.Zero,
        //        Int32Rect.Empty,
        //        System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

        //    // tidy up

        //    // CloseHandle throws SEHException using Framework 4
        //    // CloseHandle(hBitmap);

        //    DeleteObject(hBitmap);
        //    hBitmap = IntPtr.Zero;

        //    DeleteObject(hBmp);
        //    DeleteDC(hDest);
        //    DeleteDC(hSrce);

        //    bm.Dispose();
        //    GC.Collect();
        //}
    }
}
