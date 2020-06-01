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
        public int Stride { get; private set; }

        unsafe public BitmapSource ConvertToBitmapSourceBgr32(IntPtr src0, int pitch, TransformInformation formatInformation, bool isFlipHorizontally)
        {
            if (formatInformation._width == 0 || formatInformation._height == 0)
                return null;

            Stride = pitch * 2; //for YUV2

            int _width = formatInformation._width;
            int _height = formatInformation._height;
            int _length = Stride * _height; //in bytes

            WriteableBitmap bmp = new WriteableBitmap(_width, _height, 72, 72, PixelFormats.Bgr32, null);
            Debug.Assert(Stride == bmp.BackBufferStride);
            bmp.Lock();
            YUV2BGRQManagedFast((byte*)src0, (byte*)bmp.BackBuffer, _width, _height, isFlipHorizontally);
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

        private static unsafe void YUV2BGRQManagedFast(byte* YUVData, byte* BGRQData, int width, int height, bool flipHorizontally = true)
        {

            //returned pixel format is 2yuv - i.e. luminance, y, is represented for every pixel and the u and v are alternated
            //like this (where Cb = u , Cr = y)
            //Y0 Cb Y1 Cr Y2 Cb Y3 

            /*http://msdn.microsoft.com/en-us/library/ms893078.aspx
             * 
             * C = Y - 16
             D = U - 128
             E = V - 128
             R = clip(( 298 * C           + 409 * E + 128) >> 8)
             G = clip(( 298 * C - 100 * D - 208 * E + 128) >> 8)
             B = clip(( 298 * C + 516 * D           + 128) >> 8)

             * here are a whole bunch more formats for doing this...
             * http://stackoverflow.com/questions/3943779/converting-to-yuv-ycbcr-colour-space-many-versions
             */

            int i0 = flipHorizontally ? 4 : 0;
            int i1 = flipHorizontally ? 5 : 1;
            int i2 = flipHorizontally ? 6 : 2;
            int i3 = flipHorizontally ? 7 : 3;
            int i4 = flipHorizontally ? 0 : 4;
            int i5 = flipHorizontally ? 1 : 5;
            int i6 = flipHorizontally ? 2 : 6;
            int i7 = flipHorizontally ? 3 : 7;

            byte* pBGRQs = BGRQData, pYUVs = YUVData;
            {
                for (int row = 0; row < height; row++)
                {
                    byte* pBGRQ = pBGRQs + row * width * 4;
                    byte* pYUV = pYUVs + row * width * 2;

                    if(flipHorizontally)
                    {
                        pBGRQ += width * 4 - 8;
                    }

                    //process two pixels at a time
                    for (int col = 0; col < width; col += 2)
                    {
                        int C1 = pYUV[0] - 16;
                        int C2 = pYUV[2] - 16;
                        int D = pYUV[1] - 128;
                        int E = pYUV[3] - 128;

                        int R1 = (298 * C1 + 409 * E + 128) >> 8;
                        int G1 = (298 * C1 - 100 * D - 208 * E + 128) >> 8;
                        int B1 = (298 * C1 + 516 * D + 128) >> 8;

                        int R2 = (298 * C2 + 409 * E + 128) >> 8;
                        int G2 = (298 * C2 - 100 * D - 208 * E + 128) >> 8;
                        int B2 = (298 * C2 + 516 * D + 128) >> 8;
#if true
                        //check for overflow
                        //unsurprisingly this takes the bulk of the time.
                        pBGRQ[i3] = 255; //opacity
                        pBGRQ[i2] = (byte)(R1 < 0 ? 0 : R1 > 255 ? 255 : R1);
                        pBGRQ[i1] = (byte)(G1 < 0 ? 0 : G1 > 255 ? 255 : G1);
                        pBGRQ[i0] = (byte)(B1 < 0 ? 0 : B1 > 255 ? 255 : B1);

                        pBGRQ[i7] = 255; //opacity
                        pBGRQ[i6] = (byte)(R2 < 0 ? 0 : R2 > 255 ? 255 : R2);
                        pBGRQ[i5] = (byte)(G2 < 0 ? 0 : G2 > 255 ? 255 : G2);
                        pBGRQ[i4] = (byte)(B2 < 0 ? 0 : B2 > 255 ? 255 : B2);
#else
                        pBGRQ[2] = (byte)(R1);
                        pBGRQ[1] = (byte)(G1);
                        pBGRQ[0] = (byte)(B1);

                        pBGRQ[6] = (byte)(R2);
                        pBGRQ[5] = (byte)(G2);
                        pBGRQ[4] = (byte)(B2);
#endif
                        pYUV += 4;
                        if (flipHorizontally)
                        {
                            pBGRQ -= 8;
                        }
                        else
                        {
                            pBGRQ += 8;
                        }
                    }
                }
            }
        }

        //unsafe public void UpdateBufferOld(IntPtr src0, int pitch, int width, int height, bool isFlipHorizontally)
        //{
        //    IntPtr _unmanagedPointer = IntPtr.Zero;

        //    Stride = pitch * 2;
        //    if (_width != width || _height != height)
        //    {
        //        if (_unmanagedPointer != IntPtr.Zero)
        //            Marshal.FreeHGlobal(_unmanagedPointer);

        //        _width = width;
        //        _height = height;
        //        _length = Stride * _height; //in bytes
        //        _unmanagedPointer = Marshal.AllocHGlobal(_length+1);
        //    }

        //    int lengthSrc = pitch * height / 4;
        //    int lengthDst = _length / 4;

        //    var pSrcPel = (TransformInformation.YUYV*)src0;
        //    var pDestPel = (TransformInformation.RGBQUAD*)_unmanagedPointer;

        //    TransformInformation.RGBQUAD empty = new TransformInformation.RGBQUAD();

        //    var po = new ParallelOptions()
        //    {
        //        MaxDegreeOfParallelism = 1,
        //    };

        //    Parallel.For(0, lengthDst / 2, po, srcIdx =>
        //      {
        //          int buffIdx = srcIdx << 1;
        //          if (srcIdx < lengthSrc)
        //          {
        //              TransformInformation.YUYV pt = pSrcPel[srcIdx];
        //              pDestPel[buffIdx] = TransformInformation.ConvertYCrCbToRGB(pt.Y, pt.V, pt.U);
        //              pDestPel[buffIdx + 1] = TransformInformation.ConvertYCrCbToRGB(pt.Y2, pt.V, pt.U);
        //          }
        //          else
        //          {
        //              pDestPel[buffIdx] = empty;
        //              pDestPel[buffIdx + 1] = empty;
        //          }
        //      });
        //}

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
