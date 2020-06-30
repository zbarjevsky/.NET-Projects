using MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VideoModule.Tools;

namespace VideoModule
{
    public class ImageDisplayData
    {
        public Action<ImageSource, bool> OnUpdateVideoAction = (imageSource, isLive) => { };

        public bool IsFlipHorizontally { get; set; } = true;

        public void Notify(ImageSource source, bool isLive)
        {
            source.Freeze();
            WPFUtils.ExecuteOnUIThreadBeginInvoke(() =>
            {
                OnUpdateVideoAction(source, isLive);
            });
        }
    }

    public class DeviceImage : IDisposable
    {
        private readonly ImageDisplayData _imageData;

        private BitmapSource _bitmapSource;

        //public bool DoubleBuffering { get; set; } = true;
        
        private object LockObj = new object();

        public DeviceImage(ImageDisplayData imageData)
        {
            _imageData = imageData;
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
            lock (LockObj)
            {
                _bitmapSource = null;
            }
        }

        public HResult Present(bool isLive)
        {
            lock (LockObj)
            {
                if (_bitmapSource != null)
                    _imageData.Notify(_bitmapSource, isLive);
            }
            return HResult.S_OK;
        }

        public void ColorFill(Color nullBackColor)
        {
            lock (LockObj)
            {
                _bitmapSource = CreateEmptyBitmapSource(640, 480, nullBackColor);
            }
        }

        internal BitmapSource DrawFrame(IntPtr sourcePtr, int pitch, TransformData formatInformation)
        {
            lock (LockObj)
            {
                if (formatInformation.m_convertFn == null) //not set
                    return null;

                _bitmapSource = ConvertToBitmapSourceBgr32(sourcePtr, pitch, formatInformation, _imageData.IsFlipHorizontally);

                return _bitmapSource;
            }
        }

        unsafe private static BitmapSource ConvertToBitmapSourceBgr32(
            IntPtr pbScanline0, int lStride, 
            TransformData tf, bool isFlipHorizontally)
        {
            if (tf._width == 0 || tf._height == 0 || tf.m_convertFn == null)
                return null;

            WriteableBitmap bmp = new WriteableBitmap(tf._width, tf._height, 72, 72, PixelFormats.Bgr32, null);
            bmp.Lock();

            if (tf.OffScreenRender)
                tf.m_convertFn(bmp.BackBuffer, bmp.BackBufferStride, pbScanline0, lStride, tf._width, tf._height, isFlipHorizontally);
            else
                MFExtern.MFCopyImage(bmp.BackBuffer, bmp.BackBufferStride, pbScanline0, lStride, tf._width * tf._offScreenCoeffN / tf._offScreenCoeffD, tf._height);

            bmp.Unlock();
            return bmp;
        }

        unsafe private static BitmapSource CreateEmptyBitmapSource(int width, int heigth, Color color)
        {
            ImageConversion.RGBQUAD empty = new ImageConversion.RGBQUAD(color);

            var po = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4,
            };

            WriteableBitmap bmp = new WriteableBitmap(width, heigth, 72, 72, PixelFormats.Bgr32, null);
            bmp.Lock();


            int lengthDst = width * heigth;
            var pDestPel = (ImageConversion.RGBQUAD*)bmp.BackBuffer;

            Parallel.For(0, lengthDst, po, idx => pDestPel[idx] = empty);

            bmp.Unlock();

            return bmp;
        }
    }
}
