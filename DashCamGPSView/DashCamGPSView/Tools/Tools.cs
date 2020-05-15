using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using MZ.Tools;

namespace DashCamGPSView.Tools
{
    public static class Tools
    {
        public static BitmapSource ScreenshotWindow(Window wnd)
        {
            System.Windows.Point pos = wnd.GetAbsolutePosition();
            int width = (int)wnd.ActualWidth;
            var height = (int)wnd.ActualHeight;

            using (var screenBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen((int)pos.X, (int)pos.Y, 0, 0, new System.Drawing.Size(width, height));
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        screenBmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }
        public static void UIElementToPng(UIElement element, string filename)
        {
            //var rect = new Rect(element.RenderSize);
            //var visual = new DrawingVisual();

            //using (var dc = visual.RenderOpen())
            //{
            //    dc.DrawRectangle(new VisualBrush(element), null, rect);
            //}

            //var bitmap = new RenderTargetBitmap(
            //    (int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
            //bitmap.Render(visual);

            var bitmap = UIElementToBitmap(element);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var file = File.OpenWrite(filename))
            {
                encoder.Save(file);
            }
        }

        public static BitmapSource UIElementToBitmap(UIElement element)
        {
            var rect = new Rect(element.RenderSize);
            var visual = new DrawingVisual();

            using (var dc = visual.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(element), null, rect);
            }

            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                (int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
            bitmap.Render(visual);

            return bitmap;
        }

        public static void Snapshot(GpsFileFormat format, string videoFileName, TimeSpan position, UIElement element)
        {
            string fileName = @"C:\Temp\Screenshot.png";
            if (File.Exists(videoFileName))
                fileName = DashCamFileInfo.GetScreenshotFileName(format, videoFileName);

            fileName = string.Format("{0}_at{1}.png", fileName, position.ToString("hh\\.mm\\.ss"));
            UIElementToPng(element, fileName);
            Process.Start(fileName);
        }

        public static void Screenshot(GpsFileFormat format, string videoFileName, TimeSpan position, Window mainWindow)
        {
            string fileName = @"C:\Temp\Screenshot.png";
            if (File.Exists(videoFileName))
                fileName = DashCamFileInfo.GetScreenshotFileName(format, videoFileName);

            fileName = string.Format("{0}_at{1}.png", fileName, position.ToString("hh\\.mm\\.ss"));
            SaveWindowScreenshotToFile(mainWindow, fileName);
            Process.Start(fileName);
        }

        public static void SaveWindowScreenshotToFile(Window wnd, string fileName)
        {
            BitmapSource bmp = ScreenshotWindow(wnd);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(bmp));
            using (Stream stm = File.Create(fileName))
            {
                png.Save(stm);
            }
        }

        public static void ForceUIToUpdate()
        {
            DispatcherFrame frame = new DispatcherFrame();

            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate (object parameter)
            {
                frame.Continue = false;
                return null;
            }), null);

            Dispatcher.PushFrame(frame);
        }
    }
}
