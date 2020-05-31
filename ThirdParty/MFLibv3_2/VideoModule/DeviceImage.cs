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

            YUV2BGRQManaged((byte*)src0, (byte*)_unmanagedPointer, _width, _height);
            Thread.Sleep(10);

            //int lengthSrc = pitch * height / 4;
            //int lengthDst = _length / 4;

            //var pSrcPel = (DrawDevice.YUYV*)src0;
            //var pDestPel = (DrawDevice.RGBQUAD*)_unmanagedPointer;

            //DrawDevice.RGBQUAD empty = new DrawDevice.RGBQUAD();

            //var po = new ParallelOptions()
            //{
            //    MaxDegreeOfParallelism = 1,
            //};

            //Parallel.For(0, lengthDst / 2, po, srcIdx =>
            //  {
            //      int buffIdx = srcIdx << 1;
            //      if (srcIdx < lengthSrc)
            //      {
            //          DrawDevice.YUYV pt = pSrcPel[srcIdx];
            //          pDestPel[buffIdx] = DrawDevice.ConvertYCrCbToRGB(pt.Y, pt.V, pt.U);
            //          pDestPel[buffIdx + 1] = DrawDevice.ConvertYCrCbToRGB(pt.Y2, pt.V, pt.U);
            //      }
            //      else
            //      {
            //          pDestPel[buffIdx] = empty;
            //          pDestPel[buffIdx + 1] = empty;
            //      }
            //  });
        }

        public IntPtr LockBuffer()
        {
            return _unmanagedPointer;
        }

        public void UnlockBuffer()
        {
        }

        private static unsafe void YUV2BGRQManaged(byte* YUVData, byte* BGRQData, int width, int height, bool flipHorizontally = false)
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


            byte * pBGRQs = BGRQData, pYUVs = YUVData;
            {
                for (int row = 0; row < height; row++)
                {
                    byte* pBGRQ = pBGRQs + row * width * 4;
                    byte* pYUV = pYUVs + row * width * 2;

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
                        ////unsurprisingly this takes the bulk of the time.
                        pBGRQ[2] = (byte)(R1 < 0 ? 0 : R1 > 255 ? 255 : R1);
                        pBGRQ[1] = (byte)(G1 < 0 ? 0 : G1 > 255 ? 255 : G1);
                        pBGRQ[0] = (byte)(B1 < 0 ? 0 : B1 > 255 ? 255 : B1);

                        pBGRQ[6] = (byte)(R2 < 0 ? 0 : R2 > 255 ? 255 : R2);
                        pBGRQ[5] = (byte)(G2 < 0 ? 0 : G2 > 255 ? 255 : G2);
                        pBGRQ[4] = (byte)(B2 < 0 ? 0 : B2 > 255 ? 255 : B2);
#else
                        pBGRQ[2] = (byte)(R1);
                        pBGRQ[1] = (byte)(G1);
                        pBGRQ[0] = (byte)(B1);

                        pBGRQ[6] = (byte)(R2);
                        pBGRQ[5] = (byte)(G2);
                        pBGRQ[4] = (byte)(B2);
#endif
                        pBGRQ += 8;
                        pYUV += 4;
                    }
                }
            }
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
