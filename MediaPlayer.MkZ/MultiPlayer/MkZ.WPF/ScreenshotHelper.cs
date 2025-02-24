using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using Application = System.Windows.Application;
using Size = System.Windows.Size;

namespace MultiPlayer.MkZ.WPF
{
    public class ScreenshotHelper
    {
        public static async Task<Bitmap> CaptureElementAsync(FrameworkElement element, Size? screenshotSize = null)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            // Run rendering on the UI thread
            return await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (element.ActualWidth == 0 || element.ActualHeight == 0)
                {
                    return new Bitmap(10, 10);
                }
                
                if (!screenshotSize.HasValue)
                    screenshotSize = new Size(element.ActualWidth, element.ActualHeight);

                // Ensure the element is measured and arranged before rendering
                element.Measure(screenshotSize.Value);
                element.Arrange(new Rect(screenshotSize.Value));

                RenderTargetBitmap bitmap = new RenderTargetBitmap(
                     (int)screenshotSize.Value.Width,
                     (int)screenshotSize.Value.Height,
                     96, 96, System.Windows.Media.PixelFormats.Pbgra32);

                 bitmap.Render(element);
                 return ConvertRenderTargetBitmapToBitmap(bitmap);
            });
        }

        private static Bitmap ConvertRenderTargetBitmapToBitmap(RenderTargetBitmap renderTargetBitmap)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return new Bitmap(memoryStream);
            }
        }
    }
}
