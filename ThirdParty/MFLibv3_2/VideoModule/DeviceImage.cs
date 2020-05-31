using MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

        private static int _length = 0;
        private static int _width = 0;
        private static int _height = 0;

        private static IntPtr _unmanagedPointer = IntPtr.Zero;

        public IntPtr Scan0 { get { return _unmanagedPointer; } }

        unsafe public DataBuffer(IntPtr src0, int pitch, int width, int height)
        {
            Stride = pitch * 2;
            if(_width != width || _height != height)
            {
                if (_unmanagedPointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(_unmanagedPointer);

                _width = width;
                _height = height;
                _length = Stride * _height; //in bytes
                _unmanagedPointer = Marshal.AllocHGlobal(_length+1);
            }

            int lengthSrc = pitch * height / 4;
            int lengthDst = _length / 4;

            var pSrcPel = (DrawDevice.YUYV*)src0;
            var pDestPel = (DrawDevice.RGBQUAD*)_unmanagedPointer;

            DrawDevice.RGBQUAD empty = new DrawDevice.RGBQUAD();

            var po = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 2,
            };

            Parallel.For(0, lengthDst/2, po, srcIdx => 
            {
                int buffIdx = srcIdx << 1;
                if (srcIdx < lengthSrc)
                {
                    DrawDevice.YUYV pt = pSrcPel[srcIdx];
                    pDestPel[buffIdx] = DrawDevice.ConvertYCrCbToRGB(pt.Y, pt.V, pt.U);
                    pDestPel[buffIdx + 1] = DrawDevice.ConvertYCrCbToRGB(pt.Y2, pt.V, pt.U);
                }
                else
                {
                    pDestPel[buffIdx] = empty;
                    pDestPel[buffIdx+1] = empty;
                }
            });
        }

        public IntPtr LockBuffer()
        {
            return _unmanagedPointer;
        }

        public void UnlockBuffer()
        {
        }
    }

    public class ImageWrapper
    {
        private ImageSource _source;

        public Action<ImageSource> OnUpdateVideoAction = (source) => { };

        public void UpdateSource(ImageSource source)
        {
            _source = source;
            _source.Freeze();
            WPFUtils.ExecuteOnUIThread(() =>
            {
                OnUpdateVideoAction(_source);
            });
        }
    }

    public class DeviceImage : IDisposable
    {
        private readonly ImageWrapper _image;
        private BitmapSource _bitmapSource;

        public bool DoubleBuffering { get; set; } = true;

        public DeviceImage(ImageWrapper image)
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
            _image.UpdateSource(_bitmapSource);
            return HResult.S_OK;
        }

        public void ColorFill(Color nullBackColor)
        {
            
        }

        internal void UpdateBuffer(IntPtr sourcePtr, int pitch, int width, int height)
        {
            int stride = pitch;// ((width * 32 + 31) & ~31) / 8;
            try
            {
                _bitmapSource = BitmapSource.Create(width, height, 72, 72, PixelFormats.Bgr32, null, sourcePtr, stride * height, stride);
            }
            catch (Exception err)
            {
                Debug.WriteLine(err);
            }        
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
