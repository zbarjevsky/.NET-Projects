using MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VideoModule.Tools;

namespace VideoModule
{
    public class DataBuffer
    {
        public int Stride { get; }

        private WriteableBitmap _bmp;

        public DataBuffer(int pitch, int width, int height)
        {
            Stride = pitch;
            _bmp = new WriteableBitmap(width, height, 72, 72, PixelFormats.Bgr32, null);
        }

        public IntPtr LockBuffer()
        {
            _bmp.Lock();
            return _bmp.BackBuffer;
        }

        public void UnlockBuffer()
        {
            _bmp.Unlock();
        }
    }

    public class DeviceImage : IDisposable
    {
        private readonly Image _image;
        private BitmapSource _bitmapSource;

        public bool DoubleBuffering { get; internal set; }

        public DeviceImage(Image image)
        {
            _image = image;
        }

        public HResult TestCooperativeLevel()
        {
            return HResult.S_OK;
        }

        internal HResult Reset()
        {
            return HResult.S_OK;
        }

        public void Dispose()
        {

        }

        public HResult Present()
        {
            WPFUtils.ExecuteOnUIThread(() => _image.Source = _bitmapSource);
            return HResult.S_OK;
        }

        public void ColorFill(Color nullBackColor)
        {
            
        }

        internal void UpdateBuffer(IntPtr sourcePtr, int pitch, int width, int height)
        {
            _bitmapSource = BitmapSource.Create(width, height, 72, 72, PixelFormats.Bgr32, null, sourcePtr, pitch * height, pitch);
        }

        //Bitmap GetBitmap(BitmapSource source)
        //{
        //    Bitmap bmp = new Bitmap(
        //      source.PixelWidth,
        //      source.PixelHeight,
        //      PixelFormat.Format32bppPArgb);
        //    BitmapData data = bmp.LockBits(
        //      new Rectangle(Point.Empty, bmp.Size),
        //      ImageLockMode.WriteOnly,
        //      PixelFormat.Format32bppPArgb);
        //    source.CopyPixels(
        //      Int32Rect.Empty,
        //      data.Scan0,
        //      data.Height * data.Stride,
        //      data.Stride);
        //    bmp.UnlockBits(data);
        //    return bmp;
        //}
    }
}
