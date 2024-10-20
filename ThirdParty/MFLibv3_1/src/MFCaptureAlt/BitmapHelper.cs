﻿using MediaFoundation;
using MediaFoundation.Misc;
using MFCaptureAlt.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFCaptureAlt
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPV5HEADER
    {
        public uint bV5Size;
        public long bV5Width;
        public long bV5Height;
        public int bV5Planes;
        public int bV5BitCount;
        public uint bV5Compression;
        public uint bV5SizeImage;
        public long bV5XPelsPerMeter;
        public long bV5YPelsPerMeter;
        public uint bV5ClrUsed;
        public uint bV5ClrImportant;
        public uint bV5RedMask;
        public uint bV5GreenMask;
        public uint bV5BlueMask;
        public uint bV5AlphaMask;
        public uint bV5CSType;
        public IntPtr bV5Endpoints;
        public uint bV5GammaRed;
        public uint bV5GammaGreen;
        public uint bV5GammaBlue;
        public uint bV5Intent;
        public uint bV5ProfileData;
        public uint bV5ProfileSize;
        public uint bV5Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBQUAD
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;
    }

    public class BitmapHelper :COMBase
    {
        /// <summary>
        /// A struct that describes a YUYV pixel
        /// </summary>
        private struct YUYV
        {
            public byte Y;
            public byte U;
            public byte Y2;
            public byte V;

            public YUYV(byte y, byte u, byte y2, byte v)
            {
                Y = y;
                U = u;
                Y2 = y2;
                V = v;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RGBQUAD
        {
            public byte B;
            public byte G;
            public byte R;
            public byte A;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RGB24
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
        }        // Function to convert the video to RGB32
        private struct VideoFormatGUID
        {
            public Guid SubType;
            public VideoConversion VideoConvertFunction;
            public BitmapConversion BitmapConvertFunction;

            public VideoFormatGUID(Guid FormatGuid, VideoConversion cvt, BitmapConversion bmpcvt)
            {
                SubType = FormatGuid;
                VideoConvertFunction = cvt;
                BitmapConvertFunction = bmpcvt;
            }
        }

        private delegate void VideoConversion(
            IntPtr pDest,
            int lDestStride,
            IntPtr pSrc,
            int lSrcStride,
            int dwWidthInPixels,
            int dwHeightInPixels);

        // Function to convert the bitmap to video data type
        private delegate int BitmapConversion(
            int dwWidthInPixels,
            int dwHeightInPixels,
            int dwDestStride,
            IntPtr ipSrc,
            IntPtr ipDest);

        bool FAILED(bool res) { return res; }
        void BREAK_ON_FAIL(Func<HResult> func) { if (func() != HResult.S_OK) { throw new Exception(); } }

        PictureBox _pic;
        Bitmap _bmp;
        int m_width, m_height;
        int m_lDefaultStride;
        private MFRatio m_PixelAR;
        private Rectangle m_rcDest;       // Destination rectangle
        D3DFORMAT m_format = D3DFORMAT.D3DFMT_X8R8G8B8;
        private IntPtr m_hwnd;

        private VideoFormatGUID[] VideoFormatDefs;
        private VideoConversion m_convertFn;
        private BitmapConversion m_bmpconvertFn;

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        unsafe private static extern void CopyMemory(byte* Destination, byte* Source, [MarshalAs(UnmanagedType.U4)] uint Length);

        public BitmapHelper(PictureBox pic)
        {
            _pic = pic;
            //_bmp = new Bitmap(640, 480, PixelFormat.Format32bppRgb);
            m_hwnd = pic.Handle;

            VideoFormatDefs = new VideoFormatGUID[] {
                            new VideoFormatGUID(MFMediaType.RGB32, TransformImage_RGB32, ARGB32_To_RGB32),
                            new VideoFormatGUID(MFMediaType.RGB24, TransformImage_RGB24, ARGB32_To_RGB24),
                            new VideoFormatGUID(MFMediaType.YUY2, TransformImage_YUY2, ARGB32_To_YUY2),
                            new VideoFormatGUID(MFMediaType.NV12, TransformImage_NV12, ARGB32_To_NV12)
                        };
        }

        public HResult CreateDevice(IntPtr hVideo)
        {
            if(m_width == 0)
            m_width = 640; 
            if(m_height==0)
            m_height = 480;

            _bmp = new Bitmap(m_width, m_height, PixelFormat.Format32bppRgb);
            _pic.Image = _bmp;

            return HResult.S_OK;
        }

        //-------------------------------------------------------------------
        // SetVideoType
        //
        // Set the video format.
        //-------------------------------------------------------------------
        public HResult SetVideoType(IMFMediaType pType)
        {
            HResult hr = HResult.S_OK;
            Guid subtype;
            MFRatio PAR = new MFRatio();

            // Find the video subtype.
            hr = pType.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out subtype);
            if (Failed(hr)) { goto done; }

            // Choose a conversion function.
            // (This also validates the format type.)

            hr = SetConversionFunction(subtype);
            if (Failed(hr)) { goto done; }

            //
            // Get some video attributes.
            //

            // Get the frame size.
            hr = MFExtern.MFGetAttributeSize(pType, MFAttributesClsid.MF_MT_FRAME_SIZE, out m_width, out m_height);
            if (Failed(hr)) { goto done; }

            // Get the image stride.
            hr = GetDefaultStride(pType, out m_lDefaultStride);
            if (Failed(hr)) { goto done; }

            // Get the pixel aspect ratio. Default: Assume square pixels (1:1)
            hr = MFExtern.MFGetAttributeRatio(pType, MFAttributesClsid.MF_MT_PIXEL_ASPECT_RATIO, out PAR.Numerator, out PAR.Denominator);

            if (Succeeded(hr))
            {
                m_PixelAR = PAR;
            }
            else
            {
                m_PixelAR.Numerator = m_PixelAR.Denominator = 1;
            }

            FourCC f = new FourCC(subtype);
            m_format = (D3DFORMAT)f.ToInt32();

            // Update the destination rectangle for the correct
            // aspect ratio.

            UpdateDestinationRect();

        done:
            if (Failed(hr))
            {
                m_format = D3DFORMAT.D3DFMT_UNKNOWN;
                m_convertFn = null;
                m_bmpconvertFn = null;
            }

            return hr;
        }

        //-------------------------------------------------------------------
        //  UpdateDestinationRect
        //
        //  Update the destination rectangle for the current window size.
        //  The destination rectangle is letterboxed to preserve the
        //  aspect ratio of the video image.
        //-------------------------------------------------------------------

        private void UpdateDestinationRect()
        {
            Rectangle rcSrc = new Rectangle(0, 0, m_width, m_height);
            Rectangle rcClient = GetClientRect(m_hwnd);
            Rectangle rectanClient = new Rectangle(rcClient.Left, rcClient.Top, rcClient.Right - rcClient.Left, rcClient.Bottom - rcClient.Top);

            rcSrc = CorrectAspectRatio(rcSrc, m_PixelAR);

            m_rcDest = LetterBoxRect(rcSrc, rectanClient);

            CreateDevice(IntPtr.Zero);
        }

        //-------------------------------------------------------------------
        // LetterBoxDstRect
        //
        // Takes a src rectangle and constructs the largest possible
        // destination rectangle within the specifed destination rectangle
        // such that the video maintains its current shape.
        //
        // This function assumes that pels are the same shape within both the
        // source and destination rectangles.
        //
        //-------------------------------------------------------------------
        private static Rectangle LetterBoxRect(Rectangle rcSrc, Rectangle rcDst)
        {
            int iDstLBWidth;
            int iDstLBHeight;

            if (Gdi32.MulDiv(rcSrc.Width, rcDst.Height, rcSrc.Height) <= rcDst.Width)
            {
                // Column letter boxing ("pillar box")

                iDstLBWidth = Gdi32.MulDiv(rcDst.Height, rcSrc.Width, rcSrc.Height);
                iDstLBHeight = rcDst.Height;
            }
            else
            {
                // Row letter boxing.

                iDstLBWidth = rcDst.Width;
                iDstLBHeight = Gdi32.MulDiv(rcDst.Width, rcSrc.Height, rcSrc.Width);
            }

            // Create a centered rectangle within the current destination rect

            int left = rcDst.Left + ((rcDst.Width - iDstLBWidth) / 2);
            int top = rcDst.Top + ((rcDst.Height - iDstLBHeight) / 2);

            Rectangle rc = new Rectangle(left, top, iDstLBWidth, iDstLBHeight);

            return rc;
        }

        //-----------------------------------------------------------------------------
        // CorrectAspectRatio
        //
        // Converts a rectangle from the source's pixel aspect ratio (PAR) to 1:1 PAR.
        // Returns the corrected rectangle.
        //
        // For example, a 720 x 486 rect with a PAR of 9:10, when converted to 1x1 PAR,
        // is stretched to 720 x 540.
        //-----------------------------------------------------------------------------
        private static Rectangle CorrectAspectRatio(Rectangle src, MFRatio srcPAR)
        {
            // Start with a rectangle the same size as src, but offset to the origin (0,0).
            Rectangle rc = new Rectangle(0, 0, src.Right - src.Left, src.Bottom - src.Top);
            int rcNewWidth = rc.Right;
            int rcNewHeight = rc.Bottom;

            if ((srcPAR.Numerator != 1) || (srcPAR.Denominator != 1))
            {
                // Correct for the source's PAR.

                if (srcPAR.Numerator > srcPAR.Denominator)
                {
                    // The source has "wide" pixels, so stretch the width.
                    rcNewWidth = Gdi32.MulDiv(rc.Right, srcPAR.Numerator, srcPAR.Denominator);
                }
                else if (srcPAR.Numerator < srcPAR.Denominator)
                {
                    // The source has "tall" pixels, so stretch the height.
                    rcNewHeight = Gdi32.MulDiv(rc.Bottom, srcPAR.Denominator, srcPAR.Numerator);
                }
                // else: PAR is 1:1, which is a no-op.
            }

            rc = new Rectangle(0, 0, rcNewWidth, rcNewHeight);
            return rc;
        }

        public static Rectangle GetClientRect(IntPtr hWnd)
        {
            Gdi32.GetClientRect(hWnd, out Rectangle result);
            return result;
        }

        //-----------------------------------------------------------------------------
        // GetDefaultStride
        //
        // Gets the default stride for a video frame, assuming no extra padding bytes.
        //
        //-----------------------------------------------------------------------------
        private static HResult GetDefaultStride(IMFMediaType pType, out int plStride)
        {
            int lStride = 0;
            plStride = 0;

            // Try to get the default stride from the media type.
            HResult hr = pType.GetUINT32(MFAttributesClsid.MF_MT_DEFAULT_STRIDE, out lStride);

            if (Failed(hr))
            {
                // Attribute not set. Try to calculate the default stride.
                Guid subtype = Guid.Empty;

                int width = 0;
                int height = 0;

                // Get the subtype and the image size.
                hr = pType.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out subtype);
                if (Succeeded(hr))
                {
                    hr = MFExtern.MFGetAttributeSize(pType, MFAttributesClsid.MF_MT_FRAME_SIZE, out width, out height);
                }

                if (Succeeded(hr))
                {
                    FourCC f = new FourCC(subtype);

                    hr = MediaFoundation.MFExtern.MFGetStrideForBitmapInfoHeader(f.ToInt32(), width, out lStride);
                }

                // Set the attribute for later reference.
                if (Succeeded(hr))
                {
                    hr = pType.SetUINT32(MFAttributesClsid.MF_MT_DEFAULT_STRIDE, lStride);
                }
            }

            if (Succeeded(hr))
            {
                plStride = lStride;
            }

            return hr;
        }
        //-------------------------------------------------------------------
        // SetConversionFunction
        //
        // Set the conversion function for the specified video format.
        //-------------------------------------------------------------------

        private HResult SetConversionFunction(Guid subtype)
        {
            HResult hr = HResult.MF_E_INVALIDMEDIATYPE;
            m_convertFn = null;
            m_bmpconvertFn = null;

            for (int i = 0; i < VideoFormatDefs.Length; i++)
            {
                if (VideoFormatDefs[i].SubType == subtype)
                {
                    m_convertFn = VideoFormatDefs[i].VideoConvertFunction;
                    m_bmpconvertFn = VideoFormatDefs[i].BitmapConvertFunction;
                    hr = HResult.S_OK;
                    break;
                }
            }

            return hr;
        }

        internal void DestroyDevice()
        {
            
        }

        internal HResult ResetDevice()
        {
            return HResult.S_OK;
        }
        
        public HResult Write(IMFSample sample)
        {
            IMFMediaBuffer pCaptureDeviceBuffer = null;
            IntPtr pbScanline0 = IntPtr.Zero;
            BitmapData data = null;

            try
            {
                unsafe
                {
                    data = _bmp.LockBits(new Rectangle(Point.Empty, _bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

                    HResult hr = sample.ConvertToContiguousBuffer(out pCaptureDeviceBuffer);

                    //using (VideoBufferLock xbuffer = new VideoBufferLock(pCaptureDeviceBuffer))
                    {
                        hr = pCaptureDeviceBuffer.Lock(out pbScanline0, out int pcbMaxLength, out int pcbCurrentLength);
                        //hr = TestCooperativeLevel();
                        if (Failed(hr)) { goto done; }

                        // Lock the video buffer. This method returns a pointer to the first scan
                        // line in the image, and the stride in bytes.

                        //hr = xbuffer.LockBuffer(m_lDefaultStride, m_height, out IntPtr pbScanline0, out int lStride);
                        //if (Failed(hr)) { goto done; }


                        //copy every line - to ensure stride is enforced
                        //int bpp = 3;// fmt == PixelFormat.Format24bppRgb ? 3 : 4;
                        //int stride = _bmp.Width * bpp;

                        //byte* src = (byte*)pbScanline0;
                        //byte* dst = (byte*)data.Scan0;
                        //for (int i = 0; i < pcbCurrentLength; i++)
                        //{
                        //    dst[i] = src[i/2];
                        //}

                        MFExtern.MFCopyImage(data.Scan0, data.Stride, pbScanline0, m_lDefaultStride, m_width * 2, m_height);


                        //m_convertFn(data.Scan0, data.Stride, pbScanline0, m_lDefaultStride, m_width, m_height);
                        //TransformImage_YUY2(data.Scan0, data.Stride, pData, m_lDefaultStride, m_width, m_height);
                        //int[] buffer = new int[pcbCurrentLength/4];
                        //Marshal.Copy(pData, buffer, 0, buffer.Length);
                        //Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);

                        //for (int y = 0; y < _bmp.Height; y++)
                        //{
                        //    int destIdx = m_lDefaultStride * y;
                        //    IntPtr dest = data.Scan0 + destIdx;

                        //    Marshal.Copy(pData, buffer, destIdx, m_lDefaultStride);
                        //    Marshal.Copy(buffer, destIdx, dest, m_lDefaultStride);
                        //}
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err);
            }
done:
            _bmp.UnlockBits(data);
            if (pCaptureDeviceBuffer != null)
                pCaptureDeviceBuffer.Unlock();

            ExecuteOnUIThread(() => 
            {
                Debug.Write(".");
                _pic.Image = _bmp;
                _pic.Refresh();
            }, _pic);

            return HResult.S_OK;
        }

        public static void ExecuteOnUIThread(Action action, Control owner)
        {
             owner.Invoke(action);
        }


        //-------------------------------------------------------------------
        // GetFormat
        //
        // Get a supported output format by index.
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

        public bool IsFormatSupported(Guid subtype)
        {
            for (int i = 0; i < VideoFormatDefs.Length; i++)
            {
                if (subtype == VideoFormatDefs[i].SubType)
                {
                    return true;
                }
            }

            return false;
        }
        #region Static Methods

        //-------------------------------------------------------------------
        //
        // Conversion functions
        //
        //-------------------------------------------------------------------

        private static byte Clip(int clr)
        {
            return (byte)(clr < 0 ? 0 : (clr > 255 ? 255 : clr));
        }

        private static RGBQUAD ConvertYCrCbToRGB(byte y, byte cr, byte cb)
        {
            RGBQUAD rgbq = new RGBQUAD();

            int c = y - 16;
            int d = cb - 128;
            int e = cr - 128;

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
        unsafe private static void TransformImage_RGB24(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride, int dwWidthInPixels, int dwHeightInPixels)
        {
            RGB24* source = (RGB24*)pSrc;
            RGBQUAD* dest = (RGBQUAD*)pDest;

            lSrcStride /= 3;
            lDestStride /= 4;

            for (int y = 0; y < dwHeightInPixels; y++)
            {
                for (int x = 0; x < dwWidthInPixels; x++)
                {
                    dest[x].R = source[x].rgbRed;
                    dest[x].G = source[x].rgbGreen;
                    dest[x].B = source[x].rgbBlue;
                    dest[x].A = 0;
                }

                source += lSrcStride;
                dest += lDestStride;
            }
        }

        //-------------------------------------------------------------------
        // TransformImage_RGB32
        //
        // RGB-32 to RGB-32
        //
        // Note: This function is needed to copy the image from system
        // memory to the Direct3D surface.
        //-------------------------------------------------------------------
        private static void TransformImage_RGB32(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride, int dwWidthInPixels, int dwHeightInPixels)
        {
            MediaFoundation.MFExtern.MFCopyImage(pDest, lDestStride, pSrc, lSrcStride, dwWidthInPixels * 4, dwHeightInPixels);
        }

        //-------------------------------------------------------------------
        // TransformImage_YUY2
        //
        // YUY2 to RGB-32
        //-------------------------------------------------------------------
        unsafe private static void TransformImage_YUY2(
            IntPtr pDest,
            int lDestStride,
            IntPtr pSrc,
            int lSrcStride,
            int dwWidthInPixels,
            int dwHeightInPixels)
        {
            YUYV* pSrcPel = (YUYV*)pSrc;
            RGBQUAD* pDestPel = (RGBQUAD*)pDest;

            lSrcStride /= 4; // convert lSrcStride to YUYV
            lDestStride /= 4; // convert lDestStride to RGBQUAD

            for (int y = 0; y < dwHeightInPixels; y++)
            {
                for (int x = 0; x < dwWidthInPixels / 2; x++)
                {
                    pDestPel[x * 2] = ConvertYCrCbToRGB(pSrcPel[x].Y, pSrcPel[x].V, pSrcPel[x].U);
                    pDestPel[(x * 2) + 1] = ConvertYCrCbToRGB(pSrcPel[x].Y2, pSrcPel[x].V, pSrcPel[x].U);
                }

                pSrcPel += lSrcStride;
                pDestPel += lDestStride;
            }
        }

        //-------------------------------------------------------------------
        // TransformImage_NV12
        //
        // NV12 to RGB-32
        //-------------------------------------------------------------------
        unsafe private static void TransformImage_NV12(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride, int dwWidthInPixels, int dwHeightInPixels)
        {
            Byte* lpBitsY = (byte*)pSrc;
            Byte* lpBitsCb = lpBitsY + (dwHeightInPixels * lSrcStride);
            Byte* lpBitsCr = lpBitsCb + 1;

            Byte* lpLineY1;
            Byte* lpLineY2;
            Byte* lpLineCr;
            Byte* lpLineCb;

            Byte* lpDibLine1 = (Byte*)pDest;
            for (UInt32 y = 0; y < dwHeightInPixels; y += 2)
            {
                lpLineY1 = lpBitsY;
                lpLineY2 = lpBitsY + lSrcStride;
                lpLineCr = lpBitsCr;
                lpLineCb = lpBitsCb;

                Byte* lpDibLine2 = lpDibLine1 + lDestStride;

                for (UInt32 x = 0; x < dwWidthInPixels; x += 2)
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

        unsafe static private int ARGB32_To_NV12(
            int dwWidthInPixels,
            int dwHeightInPixels,
            int dwDestStride,
            IntPtr ipSrc,
            IntPtr ipDest
            )
        {
            Debug.Assert(dwWidthInPixels % 2 == 0);
            Debug.Assert(dwHeightInPixels % 2 == 0);
            Debug.Assert(dwDestStride >= dwWidthInPixels);

            RGBQUAD* pSrcRow = (RGBQUAD*)ipSrc;
            byte* pDestY = (byte*)ipDest;

            // Convert the Y plane.
            for (int y = 0; y < dwHeightInPixels; y++)
            {
                RGBQUAD* pSrcPixel = (RGBQUAD*)pSrcRow;

                for (int x = 0; x < dwWidthInPixels; x++)
                {
                    // Y0
                    pDestY[x] = (byte)(((66 * pSrcPixel[x].R + 129 * pSrcPixel[x].G + 25 * pSrcPixel[x].B + 128) >> 8) + 16);
                }
                pDestY += dwDestStride;
                pSrcRow += dwWidthInPixels;
            }

            // Calculate the offsets for the V and U planes.
            // In NV12, each chroma plane has equal stride and half the height as the Y plane.
            byte* pDestUV = (byte*)ipDest;
            pDestUV += (dwDestStride * dwHeightInPixels);

            // Convert the V and U planes.
            // NV12 is a 4:2:0 format, so each chroma sample is derived from four RGB pixels.
            // The chroma samples are packed in one plane (U,V)
            pSrcRow = (RGBQUAD*)ipSrc;
            for (int y = 0; y < dwHeightInPixels; y += 2)
            {
                RGBQUAD* pSrcPixel = (RGBQUAD*)pSrcRow;
                RGBQUAD* pNextSrcRow = (RGBQUAD*)(pSrcRow + dwWidthInPixels);

                byte* pbUV = pDestUV;

                for (int x = 0; x < dwWidthInPixels; x += 2)
                {
                    // Use a simple average to downsample the chroma.

                    // U
                    *pbUV++ = (byte)((
                        (byte)(((-38 * pSrcPixel[x].R - 74 * pSrcPixel[x].G + 112 * pSrcPixel[x].B + 128) >> 8) + 128) +
                        (byte)(((-38 * pSrcPixel[x + 1].R - 74 * pSrcPixel[x + 1].G + 112 * pSrcPixel[x + 1].B + 128) >> 8) + 128) +
                        (byte)(((-38 * pNextSrcRow[x].R - 74 * pNextSrcRow[x].G + 112 * pNextSrcRow[x].B + 128) >> 8) + 128) +
                        (byte)(((-38 * pNextSrcRow[x + 1].R - 74 * pNextSrcRow[x + 1].G + 112 * pNextSrcRow[x + 1].B + 128) >> 8) + 128)
                               ) / 4);

                    // V
                    *pbUV++ = (byte)((
                        (byte)(((112 * pSrcPixel[x].R - 94 * pSrcPixel[x].G - 18 * pSrcPixel[x].B + 128) >> 8) + 128) +
                        (byte)(((112 * pSrcPixel[x + 1].R - 94 * pSrcPixel[x + 1].G - 18 * pSrcPixel[x + 1].B + 128) >> 8) + 128) +
                        (byte)(((112 * pNextSrcRow[x].R - 94 * pNextSrcRow[x].G - 18 * pNextSrcRow[x].B + 128) >> 8) + 128) +
                        (byte)(((112 * pNextSrcRow[x + 1].R - 94 * pNextSrcRow[x + 1].G - 18 * pNextSrcRow[x + 1].B + 128) >> 8) + 128)
                               ) / 4);
                }
                pDestUV += dwDestStride;

                // Skip two lines on the source image.
                pSrcRow += (dwWidthInPixels * 2);
            }

            return (int)(pDestUV - (byte*)ipDest);
        }

        unsafe static private int ARGB32_To_RGB32(
            int dwWidthInPixels,
            int dwHeightInPixels,
            int dwDestStride,
            IntPtr ipSrc,
            IntPtr ipDest
            )
        {
            byte* pSrc = (byte*)ipSrc;
            byte* pDest = (byte*)ipDest;

            int iBufsize = dwDestStride * dwHeightInPixels;

            CopyMemory(pDest, pSrc, (uint)iBufsize);

            return iBufsize;
        }

        unsafe static private int ARGB32_To_RGB24(
            int dwWidthInPixels,
            int dwHeightInPixels,
            int dwDestStride,
            IntPtr ipSrc,
            IntPtr ipDest
            )
        {
            RGBQUAD* pSrc = (RGBQUAD*)ipSrc;
            RGB24* pDest = (RGB24*)ipDest;

            for (int y = 0; y < dwHeightInPixels; y++)
            {
                for (int x = 0; x < dwWidthInPixels; x++)
                {
                    pDest[x].rgbRed = pSrc[x].R;
                    pDest[x].rgbGreen = pSrc[x].G;
                    pDest[x].rgbBlue = pSrc[x].B;
                }

                pSrc += dwWidthInPixels * 3;
                pDest += dwDestStride;
            }

            return dwDestStride * dwHeightInPixels;
        }

        unsafe static private int ARGB32_To_YUY2(
            int dwWidthInPixels,
            int dwHeightInPixels,
            int dwDestStride,
            IntPtr ipSrc,
            IntPtr ipDest
            )
        {
            Debug.Assert(dwDestStride >= (dwWidthInPixels * 2));

            RGBQUAD* pSrcPixel = (RGBQUAD*)ipSrc;
            YUYV* pDestPixel = (YUYV*)ipDest;
            int dwUseWidth = dwWidthInPixels / 2;

            // invert
            pSrcPixel += (dwHeightInPixels - 1) * dwWidthInPixels;

            for (int y = 0; y < dwHeightInPixels; y++)
            {
                for (int x = 0; x < dwUseWidth; x++)
                {
                    pDestPixel[x].Y = (byte)(((66 * pSrcPixel->R + 129 * pSrcPixel->G + 25 * pSrcPixel->B + 128) >> 8) + 16);
                    pDestPixel[x].U = (byte)(((-38 * pSrcPixel->R - 74 * pSrcPixel->G + 112 * pSrcPixel->B + 128) >> 8) + 128);

                    pSrcPixel++;

                    pDestPixel[x].Y2 = (byte)(((66 * pSrcPixel->R + 129 * pSrcPixel->G + 25 * pSrcPixel->B + 128) >> 8) + 16);
                    pDestPixel[x].V = (byte)(((112 * pSrcPixel->R - 94 * pSrcPixel->G - 18 * pSrcPixel->B + 128) >> 8) + 128);

                    pSrcPixel++;
                }
                pDestPixel = &pDestPixel[dwDestStride / sizeof(YUYV)];

                // Invert
                pSrcPixel -= dwWidthInPixels * 2;
            }

            return (int)((byte*)pDestPixel - (byte*)ipDest);
        }

        #endregion
    }
    }
