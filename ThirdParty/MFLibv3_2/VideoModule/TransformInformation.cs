using MediaFoundation;
using MediaFoundation.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VideoModule
{
    public class TransformInformation
    {
        public struct VideoFormatGUID
        {
            public readonly Guid SubType;
            public readonly VideoConversion VideoConvertFunction;
            public readonly SlimDX.Direct3D9.Format DxFormat;

            public VideoFormatGUID(Guid FormatGuid, VideoConversion cvt, SlimDX.Direct3D9.Format format)
            {
                SubType = FormatGuid;
                VideoConvertFunction = cvt;
                DxFormat = format;
            }
        }

        // Function to convert the video to RGB32
        public delegate void VideoConversion(
            IntPtr pDest,
            int lDestStride,
            IntPtr pSrc,
            int lSrcStride,
            int dwWidthInPixels,
            int dwHeightInPixels);

        public readonly VideoFormatGUID[] VideoFormatDefs =
        {
            new VideoFormatGUID(MFMediaType.RGB32, ImageConversion.TransformImage_RGB32, SlimDX.Direct3D9.Format.X8R8G8B8),
            new VideoFormatGUID(MFMediaType.RGB24, ImageConversion.TransformImage_RGB24, SlimDX.Direct3D9.Format.R8G8B8),
            new VideoFormatGUID(MFMediaType.YUY2, ImageConversion.TransformImage_YUY2, SlimDX.Direct3D9.Format.Yuy2),
            new VideoFormatGUID(MFMediaType.NV12, ImageConversion.TransformImage_NV12, SlimDX.Direct3D9.Format.Unknown)
        };

        public VideoConversion m_convertFn;

        public int _width = 0;
        public int _height = 0;
        public int _lDefaultStride = 0;
        
        private MFRatio pixelAR = new MFRatio() {Denominator = 1, Numerator = 1 };
        
        private int _offScreenCoeffN = 4;
        private int _offScreenCoeffD = 1;
        private SlimDX.Direct3D9.Format _offScreenFormat = SlimDX.Direct3D9.Format.X8R8G8B8;
        
        public SlimDX.Direct3D9.Format _format = SlimDX.Direct3D9.Format.X8R8G8B8;

        public TransformInformation()
        {
        }

        //-------------------------------------------------------------------
        // SetVideoType
        //
        // Set the video snapFormat.
        //-------------------------------------------------------------------
        public HResult SetVideoType(IMFMediaType pType)
        {
            Guid subtype;
            var PAR = new MFRatio();

            // Find the video subtype.
            var hr = pType.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out subtype);

            try
            {
                if (COMBase.Failed(hr))
                    throw new Exception();

                // Choose a conversion function.
                // (This also validates the snapFormat type.)

                hr = SetConversionFunction(subtype);
                if (COMBase.Failed(hr))
                    throw new Exception();

                //
                // Get some video attributes.
                //

                // Get the frame size.
                hr = CProcess.MfGetAttributeSize(pType, out _width, out _height);
                if (COMBase.Failed(hr))
                    throw new Exception();

                // Get the image stride.
                hr = GetDefaultStride(pType, out _lDefaultStride);
                if (COMBase.Failed(hr))
                    throw new Exception();

                // Get the pixel aspect ratio. Default: Assume square pixels (1:1)
                hr = CProcess.MfGetAttributeRatio(pType, out PAR.Numerator, out PAR.Denominator);

                if (COMBase.Succeeded(hr))
                {
                    pixelAR = PAR;
                }
                else
                {
                    pixelAR.Numerator = pixelAR.Denominator = 1;
                }

                var f = new FourCC(subtype);
                _format = (SlimDX.Direct3D9.Format)f.ToInt32();

            }
            finally
            {
                if (COMBase.Failed(hr))
                {
                    _format = SlimDX.Direct3D9.Format.Unknown;
                    m_convertFn = null;
                }
            }
            return hr;
        }

        //-----------------------------------------------------------------------------
        // GetDefaultStride
        //
        // Gets the default stride for a video frame, assuming no extra padding bytes.
        //
        //-----------------------------------------------------------------------------
        private static HResult GetDefaultStride(IMFMediaType pType, out int plStride)
        {
            int lStride;
            plStride = 0;

            // Try to get the default stride from the media type.
            var hr = pType.GetUINT32(MFAttributesClsid.MF_MT_DEFAULT_STRIDE, out lStride);

            if (COMBase.Failed(hr))
            {
                // Attribute not set. Try to calculate the default stride.
                Guid subtype;

                var width = 0;
                // ReSharper disable once TooWideLocalVariableScope
                // ReSharper disable once RedundantAssignment
                var height = 0;

                // Get the subtype and the image size.
                hr = pType.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out subtype);
                if (COMBase.Succeeded(hr))
                {
                    hr = CProcess.MfGetAttributeSize(pType, out width, out height);
                }

                if (COMBase.Succeeded(hr))
                {
                    var f = new FourCC(subtype);

                    hr = MFExtern.MFGetStrideForBitmapInfoHeader(f.ToInt32(), width, out lStride);
                }

                // Set the attribute for later reference.
                if (COMBase.Succeeded(hr))
                {
                    hr = pType.SetUINT32(MFAttributesClsid.MF_MT_DEFAULT_STRIDE, lStride);
                }
            }

            if (COMBase.Succeeded(hr))
            {
                plStride = lStride;
            }

            return hr;
        }

        //-------------------------------------------------------------------
        // SetConversionFunction
        //
        // Set the conversion function for the specified video snapFormat.
        //-------------------------------------------------------------------

        public HResult SetConversionFunction(Guid subtype)
        {
            var q = (from item in VideoFormatDefs where item.SubType == subtype select item).FirstOrDefault();

            m_convertFn = q.VideoConvertFunction;
            _offScreenFormat = q.DxFormat;

            switch (_offScreenFormat)
            {
                case SlimDX.Direct3D9.Format.R8G8B8:
                    _offScreenCoeffN = 3;
                    _offScreenCoeffD = 1;
                    break;
                case SlimDX.Direct3D9.Format.Yuy2:
                    _offScreenCoeffN = 2;
                    _offScreenCoeffD = 1;
                    break;
                case SlimDX.Direct3D9.Format.X8B8G8R8:
                case SlimDX.Direct3D9.Format.A8B8G8R8:
                case SlimDX.Direct3D9.Format.X8R8G8B8:
                case SlimDX.Direct3D9.Format.A8R8G8B8:
                    _offScreenCoeffN = 4;
                    _offScreenCoeffD = 1;
                    break;
            }
            return (m_convertFn == null) ? HResult.MF_E_INVALIDMEDIATYPE : HResult.S_OK;
        }

        //-------------------------------------------------------------------
        //  IsFormatSupported
        //
        //  Query if a snapFormat is supported.
        //-------------------------------------------------------------------
        public bool IsFormatSupported(Guid subtype)
        {
            return VideoFormatDefs.Any(item => item.SubType == subtype);
        }

        //-------------------------------------------------------------------
        // GetFormat
        //
        // Get a supported output snapFormat by index.
        //-------------------------------------------------------------------
        public HResult GetFormat(int index, out Guid pSubtype)
        {
            if (index < VideoFormatDefs.Length)
            {
                pSubtype = VideoFormatDefs[index].SubType;
                return HResult.S_OK;
            }

            pSubtype = Guid.Empty;
            return HResult.MF_E_NO_MORE_TYPES;
        }
    }

    public static class ImageConversion
    { 
        /// <summary>
        /// A struct that describes a YUYV pixel
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct YUYV
        {
            public byte Y;
            public byte U;
            public byte Y2;
            public byte V;

            //// ReSharper disable once UnusedMember.Local
            //public YUYV(byte y, byte u, byte y2, byte v)
            //{
            //    Y = y;
            //    U = u;
            //    Y2 = y2;
            //    V = v;
            //}
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RGBQUAD
        {
            public byte B;
            public byte G;
            public byte R;
            public byte A;

            public RGBQUAD(Color color) : this()
            {
                R = color.R;
                G = color.G;
                B = color.B;
                A = color.A;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RGB24
        {
            public byte B;
            public byte G;
            public byte R;
        }

        //-------------------------------------------------------------------
        //
        // Conversion functions
        //
        //-------------------------------------------------------------------

        private static byte Clip(int clr)
        {
            return (byte)(clr < 0 ? 0 : (clr > 255 ? 255 : clr));
        }

        public static RGBQUAD ConvertYCrCbToRGB(byte y, byte cr, byte cb)
        {
            var rgbq = new RGBQUAD();

            var c = y - 16;
            var d = cb - 128;
            var e = cr - 128;

            rgbq.R = Clip((298 * c + 409 * e + 128) >> 8);
            rgbq.G = Clip((298 * c - 100 * d - 208 * e + 128) >> 8);
            rgbq.B = Clip((298 * c + 516 * d + 128) >> 8);

            return rgbq;
        }

        //-------------------------------------------------------------------
        // TransformImage_RGB24
        //
        // RGB-24 to RGB-32
        //-------------------------------------------------------------------
        public static unsafe void TransformImage_RGB24(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride,
            int dwWidthInPixels, int dwHeightInPixels)
        {
            var source = (RGB24*)pSrc;
            var dest = (RGBQUAD*)pDest;

            lSrcStride /= 3;
            lDestStride /= 4;

            var po = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 32,
            };

            Parallel.For(0, dwHeightInPixels, po, y =>
            {
                var destY = dest + y * lDestStride;
                var sourceY = source + y * lSrcStride;
                Parallel.For(0, dwWidthInPixels, po, x =>
                {
                    destY[x].R = sourceY[x].R;
                    destY[x].G = sourceY[x].G;
                    destY[x].B = sourceY[x].B;
                    destY[x].A = 0;
                });
            });
        }

        //-------------------------------------------------------------------
        // TransformImage_RGB32
        //
        // RGB-32 to RGB-32
        //
        // Note: This function is needed to copy the image from system
        // memory to the Direct3D surface.
        //-------------------------------------------------------------------
        public static void TransformImage_RGB32(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride, int dwWidthInPixels, int dwHeightInPixels)
        {
            MFExtern.MFCopyImage(pDest, lDestStride, pSrc, lSrcStride, dwWidthInPixels * 4, dwHeightInPixels);
        }

        //-------------------------------------------------------------------
        // TransformImage_YUY2
        //
        // YUY2 to RGB-32
        //-------------------------------------------------------------------
        unsafe public static void TransformImage_YUY2(
            IntPtr pDest,
            int lDestStride,
            IntPtr pSrc,
            int lSrcStride,
            int dwWidthInPixels,
            int dwHeightInPixels)
        {
            var pSrcPel = (YUYV*)pSrc;
            var pDestPel = (RGBQUAD*)pDest;

            lSrcStride /= 4; // convert lSrcStride to YUYV
            lDestStride /= 4; // convert lDestStride to RGBQUAD

            var po = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 1,
            };

            Parallel.For(0, dwHeightInPixels, po, y =>
            {
                Parallel.For(0, dwWidthInPixels >> 1, po, x =>
                {
                    var dIndex = y * lDestStride + (x << 1);
                    var sIndex = y * lSrcStride + x;
                    YUYV src = pSrcPel[sIndex];
                    pDestPel[dIndex] = ConvertYCrCbToRGB(src.Y, src.V, src.U);
                    pDestPel[dIndex + 1] = ConvertYCrCbToRGB(src.Y2, src.V, src.U);
                });
            });
        }

        //-------------------------------------------------------------------
        // TransformImage_NV12
        //
        // NV12 to RGB-32
        //-------------------------------------------------------------------
        unsafe public static void TransformImage_NV12(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride, int dwWidthInPixels, int dwHeightInPixels)
        {
            byte* lpBitsY = (byte*)pSrc;
            byte* lpBitsCb = lpBitsY + (dwHeightInPixels * lSrcStride);
            byte* lpBitsCr = lpBitsCb + 1;

            // ReSharper disable TooWideLocalVariableScope
            byte* lpLineY1;
            byte* lpLineY2;
            byte* lpLineCr;
            byte* lpLineCb;
            // ReSharper enable TooWideLocalVariableScope

            byte* lpDibLine1 = (byte*)pDest;
            for (var y = 0; y < dwHeightInPixels; y += 2)
            {
                lpLineY1 = lpBitsY;
                lpLineY2 = lpBitsY + lSrcStride;
                lpLineCr = lpBitsCr;
                lpLineCb = lpBitsCb;

                byte* lpDibLine2 = lpDibLine1 + lDestStride;

                for (var x = 0; x < dwWidthInPixels; x += 2)
                {
                    byte y0 = lpLineY1[0];
                    byte y1 = lpLineY1[1];
                    byte y2 = lpLineY2[0];
                    byte y3 = lpLineY2[1];
                    byte cb = lpLineCb[0];
                    byte cr = lpLineCr[0];

                    RGBQUAD r = ConvertYCrCbToRGB(y0, cr, cb);
                    lpDibLine1[0] = r.B;
                    lpDibLine1[1] = r.G;
                    lpDibLine1[2] = r.R;
                    lpDibLine1[3] = 0; // Alpha

                    r = ConvertYCrCbToRGB(y1, cr, cb);
                    lpDibLine1[4] = r.B;
                    lpDibLine1[5] = r.G;
                    lpDibLine1[6] = r.R;
                    lpDibLine1[7] = 0; // Alpha

                    r = ConvertYCrCbToRGB(y2, cr, cb);
                    lpDibLine2[0] = r.B;
                    lpDibLine2[1] = r.G;
                    lpDibLine2[2] = r.R;
                    lpDibLine2[3] = 0; // Alpha

                    r = ConvertYCrCbToRGB(y3, cr, cb);
                    lpDibLine2[4] = r.B;
                    lpDibLine2[5] = r.G;
                    lpDibLine2[6] = r.R;
                    lpDibLine2[7] = 0; // Alpha

                    lpLineY1 += 2;
                    lpLineY2 += 2;
                    lpLineCr += 2;
                    lpLineCb += 2;

                    lpDibLine1 += 8;
                    lpDibLine2 += 8;
                }

                pDest += (2 * lDestStride);
                lpBitsY += (2 * lSrcStride);
                lpBitsCr += lSrcStride;
                lpBitsCb += lSrcStride;
            }
        }
    }
}
