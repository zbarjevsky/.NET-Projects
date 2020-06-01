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
        public Action<ImageSource> OnUpdateVideoAction = (source) => { };

        public bool IsFlipHorizontally { get; set; } = true;

        public void Notify(ImageSource source)
        {
            source.Freeze();
            WPFUtils.ExecuteOnUIThreadBeginInvoke(() =>
            {
                OnUpdateVideoAction(source);
            });
        }
    }

   public class ImageCreateTransform : IDisposable
    {
        public bool OffScreenRender { get; set; } = true;

        unsafe public BitmapSource ConvertToBitmapSourceBgr32(IntPtr pbScanline0, int lStride, TransformInformation tf, bool isFlipHorizontally)
        {
            if (tf._width == 0 || tf._height == 0 || tf.m_convertFn == null)
                return null;

            WriteableBitmap bmp = new WriteableBitmap(tf._width, tf._height, 72, 72, PixelFormats.Bgr32, null);
            bmp.Lock();

            if(OffScreenRender)
                tf.m_convertFn(bmp.BackBuffer, bmp.BackBufferStride, pbScanline0, lStride, tf._width, tf._height, isFlipHorizontally);
            else
                MFExtern.MFCopyImage(bmp.BackBuffer, bmp.BackBufferStride, pbScanline0, lStride, tf._width * tf._offScreenCoeffN / tf._offScreenCoeffD, tf._height);

            bmp.Unlock();
            return bmp;
        }

        public unsafe BitmapSource ColorFill(Color color)
        {
            ImageConversion.RGBQUAD empty = new ImageConversion.RGBQUAD(color);

            var po = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4,
            };

            WriteableBitmap bmp = new WriteableBitmap(640, 480, 72, 72, PixelFormats.Bgr32, null);
            bmp.Lock();


            int lengthDst = 640 * 480;
            var pDestPel = (ImageConversion.RGBQUAD*)bmp.BackBuffer;

            Parallel.For(0, lengthDst, po, idx => pDestPel[idx] = empty);
            
            bmp.Unlock();

            return bmp;
        }

        public void Dispose()
        {
        }
    }

    public class DeviceImage : IDisposable
    {
        private readonly ImageDisplayData _imageData;
        private readonly ImageCreateTransform _dataTransform = new ImageCreateTransform();

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
                _dataTransform.Dispose();
            }
        }

        public HResult Present()
        {
            lock (LockObj)
            {
                if (_bitmapSource != null)
                    _imageData.Notify(_bitmapSource);
            }
            return HResult.S_OK;
        }

        public void ColorFill(Color nullBackColor)
        {
            lock (LockObj)
            {
                _bitmapSource = _dataTransform.ColorFill(nullBackColor);
            }
        }

        internal BitmapSource Convert(IntPtr sourcePtr, int pitch, TransformInformation formatInformation)
        {
            lock (LockObj)
            {
                if (formatInformation.m_convertFn == null) //not set
                    return null;

                _bitmapSource = _dataTransform.ConvertToBitmapSourceBgr32(sourcePtr, pitch, formatInformation, _imageData.IsFlipHorizontally);

                return _bitmapSource;
            }
        }
    }
}
