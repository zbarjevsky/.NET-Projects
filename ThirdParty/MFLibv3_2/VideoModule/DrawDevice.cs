using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MediaFoundation;
using MediaFoundation.Misc;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using VideoModule.Tools;
using System.Windows.Controls;

namespace VideoModule
{
    // ReSharper disable InconsistentNaming
    internal class DrawDevice : COMBase
    {
        #region Definitions

        private static readonly Color DefaultBackColor = Colors.Gray;
        private static readonly Color NullBackColor = Colors.DimGray;

        private const int NUM_BACK_BUFFERS = 2;

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

        private struct VideoFormatGUID
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
        private delegate void VideoConversion(
            IntPtr pDest, 
            int lDestStride, 
            IntPtr pSrc, 
            int lSrcStride, 
            int dwWidthInPixels, 
            int dwHeightInPixels);

        #endregion

        #region Private Members

        //private IntPtr hwnd;
        //private Device pDevice;
        //private SwapChain pSwapChain;
        //private Surface offScreenSurface;
        //private SlimDX.Direct3D9.Format offScreenFormat;
        //private PresentParameters[] d3dpp;

        private int _offScreenCoeffN = 4;
        private int _offScreenCoeffD = 1;

        NewFrameAvailableNotify _notify;
        DeviceImage _device;

        // Format information
        private SlimDX.Direct3D9.Format _format;
        private int _width;
        private int _height;
        private int _lDefaultStride;
        private MFRatio pixelAR;
        private Rect _rcDest;       // Destination Rect

        private readonly VideoFormatGUID[] VideoFormatDefs =
        {
            new VideoFormatGUID(MFMediaType.RGB32, TransformImage_RGB32, SlimDX.Direct3D9.Format.X8R8G8B8),
            new VideoFormatGUID(MFMediaType.RGB24, TransformImage_RGB24, SlimDX.Direct3D9.Format.R8G8B8),
            new VideoFormatGUID(MFMediaType.YUY2, TransformImage_YUY2, SlimDX.Direct3D9.Format.Yuy2),
            new VideoFormatGUID(MFMediaType.NV12, TransformImage_NV12, SlimDX.Direct3D9.Format.Unknown)
        };

        private VideoConversion m_convertFn;

        #endregion

        //-------------------------------------------------------------------
        // Constructor
        //-------------------------------------------------------------------
        public DrawDevice()
        {
            _notify = null;
            _device = null;

            _format = SlimDX.Direct3D9.Format.X8R8G8B8;
            _width = 0;
            _height = 0;
            _lDefaultStride = 0;
            pixelAR.Denominator = pixelAR.Numerator = 1;
            _rcDest = Rect.Empty;
            m_convertFn = null;
        }

        //-------------------------------------------------------------------
        // Destructor
        //-------------------------------------------------------------------
#if DEBUG
        ~DrawDevice()
        {
            //Debug.Assert(pSwapChain == null || pDevice == null);
            Debug.Assert(_device == null);
            DestroyDevice();
        }
#endif

        #region External

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int MulDiv(int nNumber, int nNumerator, int nDenominator);

        #endregion

        #region Private Methods

        private HResult TestCooperativeLevel()
        {
            if (_device == null)
            {
                return HResult.E_FAIL;
            }

            // Check the current status of Bitmap device.
            return _device.TestCooperativeLevel();
        }

        //-------------------------------------------------------------------
        // SetConversionFunction
        //
        // Set the conversion function for the specified video snapFormat.
        //-------------------------------------------------------------------

        private HResult SetConversionFunction(Guid subtype)
        {
            var q = (from item in VideoFormatDefs  where item.SubType == subtype select item).FirstOrDefault();

            m_convertFn = q.VideoConvertFunction;
            SlimDX.Direct3D9.Format offScreenFormat = q.DxFormat;

            switch (offScreenFormat)
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
        // CreateSwapChains
        //
        // Create Direct3D swap chains.
        //-------------------------------------------------------------------

        //private HResult CreateSwapChains()
        //{
        //    if (pSwapChain != null)
        //    {
        //        pSwapChain.Dispose();
        //        pSwapChain = null;
        //    }

        //    var pp = new PresentParameters
        //    {
        //        EnableAutoDepthStencil = false,
        //        BackBufferWidth = _width,
        //        BackBufferHeight = _height,
        //        Windowed = true,
        //        SwapEffect = SwapEffect.Flip,
        //        DeviceWindowHandle = hwnd,
        //        BackBufferFormat = SlimDX.Direct3D9.Format.X8R8G8B8,
        //        PresentFlags = PresentFlags.DeviceClip | PresentFlags.LockableBackBuffer,
        //        PresentationInterval = PresentInterval.Immediate,
        //        BackBufferCount = NUM_BACK_BUFFERS
        //    };

        //    pSwapChain = new SwapChain(pDevice, pp);

        //    return S_Ok;
        //}

        //-------------------------------------------------------------------
        //  UpdateDestinationRect
        //
        //  Update the destination Rect for the current window size.
        //  The destination Rect is letterboxed to preserve the
        //  aspect ratio of the video image.
        //-------------------------------------------------------------------

        private void UpdateDestinationRect()
        {
            //Try to create off-screen surface
            //offScreenSurface?.Dispose();
            //if (offScreenFormat == SlimDX.Direct3D9.Format.Unknown || offScreenFormat == SlimDX.Direct3D9.Format.A8R8G8B8 ||
            //    offScreenFormat == SlimDX.Direct3D9.Format.X8R8G8B8)
            //    offScreenSurface = null;
            //else
            //    offScreenSurface = Surface.CreateOffscreenPlain(
            //        pDevice, _width, _height, offScreenFormat, Pool.Default);

            //var rcSrc = new Rect(0, 0, _width, _height);
            //var rcClient = User32.GetClientRect(hwnd);
            //var rectanClient = new Rect(rcClient.Left, rcClient.Top, rcClient.Right - rcClient.Left, rcClient.Bottom - rcClient.Top);

            //rcSrc = CorrectAspectRatio(rcSrc, pixelAR);

            //_rcDest = LetterBoxRect(rcSrc, rectanClient);
        }


        #endregion

        #region Public Methods

        //-------------------------------------------------------------------
        // CreateDevice
        //
        // Create the Direct3D device.
        //-------------------------------------------------------------------
        public HResult CreateDevice(NewFrameAvailableNotify notify)
        {
            if (_device != null)
            {
                return S_Ok;
            }

            _notify = notify;
            _device = new DeviceImage(notify);

            //var pp = new PresentParameters[1];

            //pp[0] = new PresentParameters
            //{
            //    BackBufferFormat = SlimDX.Direct3D9.Format.X8R8G8B8,
            //    SwapEffect = SwapEffect.Copy,
            //    PresentationInterval = PresentInterval.Immediate,
            //    Windowed = true,
            //    DeviceWindowHandle = hWindow,
            //    BackBufferHeight = 0,
            //    BackBufferWidth = 0,
            //    EnableAutoDepthStencil = false
            //};

            //using (var d = new Direct3D())
            //{
            //    pDevice = new Device(d, 0, DeviceType.Hardware, hWindow,
            //        CreateFlags.HardwareVertexProcessing | CreateFlags.FpuPreserve | CreateFlags.Multithreaded, pp);
            //}

            //hwnd = hWindow;
            //d3dpp = pp;

            return S_Ok;
        }

        //-------------------------------------------------------------------
        // ResetDevice
        //
        // Resets the Direct3D device.
        //-------------------------------------------------------------------
        public HResult ResetDevice()
        {
            HResult hr = S_Ok;

            if (_device != null)
            {
                //var d3dppClone = (PresentParameters[])d3dpp.Clone();

                try
                {
                    hr = _device.Reset();

                    if (Failed(hr))
                    {
                        DestroyDevice();
                    }
                }
                catch
                {
                    DestroyDevice();
                }
            }

            if (_device == null)
            {
                hr = CreateDevice(_notify);

                if (Failed(hr))
                {
                    return hr;
                }
            }

            if ((_format != SlimDX.Direct3D9.Format.Unknown))
            {
                UpdateDestinationRect();
            }

            return hr;
        }

        //-------------------------------------------------------------------
        // DestroyDevice
        //-------------------------------------------------------------------
        public void DestroyDevice()
        {
            _device?.Dispose();
            _device = null;
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
                if (Failed(hr))
                    throw new Exception();

                // Choose a conversion function.
                // (This also validates the snapFormat type.)

                hr = SetConversionFunction(subtype);
                if (Failed(hr))
                    throw new Exception();

                //
                // Get some video attributes.
                //

                // Get the frame size.
                hr = CProcess.MfGetAttributeSize(pType, out _width, out _height);
                if (Failed(hr))
                    throw new Exception();

                // Get the image stride.
                hr = GetDefaultStride(pType, out _lDefaultStride);
                if (Failed(hr))
                    throw new Exception();

                // Get the pixel aspect ratio. Default: Assume square pixels (1:1)
                hr = CProcess.MfGetAttributeRatio(pType, out PAR.Numerator, out PAR.Denominator);

                if (Succeeded(hr))
                {
                    pixelAR = PAR;
                }
                else
                {
                    pixelAR.Numerator = pixelAR.Denominator = 1;
                }

                var f = new FourCC(subtype);
                _format = (SlimDX.Direct3D9.Format) f.ToInt32();

                // Create Direct3D swap chains.

                //hr = CreateSwapChains();
                //if (Failed(hr))
                //    throw new Exception();

                // Update the destination Rect for the correct
                // aspect ratio.

                UpdateDestinationRect();

            }
            finally
            {
                if (Failed(hr))
                {
                    _format = SlimDX.Direct3D9.Format.Unknown;
                    m_convertFn = null;
                }
            }
            return hr;
        }

        //[DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        //private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        public HResult DrawNullFrame()
        {
            // Color fill the back buffer.
            _device.ColorFill(NullBackColor);

            // Present the frame.
            return _device.Present();
        }

        //-------------------------------------------------------------------
        // DrawFrame
        //
        // Draw the video frame.
        //-------------------------------------------------------------------
        public HResult DrawFrame(IMFMediaBuffer pCaptureDeviceBuffer, bool snap, string snapFormat=null)
        {
            if (m_convertFn == null)
            {
                return HResult.MF_E_INVALIDREQUEST;
            }            

            if (_device == null)
            {
                return S_Ok;
            }

            HResult hr = HResult.S_OK;

            Rect r = new Rect(0, 0, _width, _height);

            // Helper object to lock the video buffer.
            using (var xbuffer = new VideoBufferLock(pCaptureDeviceBuffer))
            {
                IntPtr pbScanline0;
                int lStride;
                try
                {
                    hr = TestCooperativeLevel();
                    if (Failed(hr))
                        throw new InvalidOperationException();

                    // Lock the video buffer. This method returns a pointer to the first scan
                    // line in the image, and the stride in bytes.

                    hr = xbuffer.LockBuffer(_lDefaultStride, _height, out pbScanline0, out lStride);
                    if (Failed(hr))
                        throw new InvalidOperationException();
                }
                catch (InvalidOperationException)
                {
                    return hr;
                }

                //DataBuffer dr = new DataBuffer(pbScanline0, lStride, _width, _height);
                try
                {
                    //IntPtr scan0 = dr.LockBuffer();
                    //if (pDevice.DoubleBuffering)
                    //{
                    //    // Convert the frame. This also copies it to the Direct3D surface.
                    //    m_convertFn(scan0, dr.Stride, pbScanline0, lStride, _width, _height);
                    //}
                    //else
                    //{
                    //    MFExtern.MFCopyImage(scan0, dr.Stride, pbScanline0, lStride, _width * offScreenCoeffN / offScreenCoeffD, _height);
                    //}

                    //pDevice.UpdateBuffer(dr);
                    DataBuffer dr = _device.UpdateBuffer(pbScanline0, lStride, _width, _height);

                    if (snap)
                            ImageHelper.SnapShot(dr.Scan0, dr.Stride, _width, _height, snapFormat);
                }
                finally
                {
                    //dr.UnlockBuffer();
                }
            }

            if (true)
            {
                // Present the frame.
                hr = _device.Present();
            }

            return hr;
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
                return S_Ok;
            }

            pSubtype = Guid.Empty;
            return HResult.MF_E_NO_MORE_TYPES;
        }

        #endregion

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
        private static unsafe void TransformImage_RGB24(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride,
            int dwWidthInPixels, int dwHeightInPixels)
        {
            var source = (RGB24*) pSrc;
            var dest = (RGBQUAD*) pDest;

            lSrcStride /= 3;
            lDestStride /= 4;

            var po = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 32,
            };

            Parallel.For(0, dwHeightInPixels, po, y =>
            {
                var destY = dest + y*lDestStride;
                var sourceY = source + y*lSrcStride;
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
        private static void TransformImage_RGB32(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride, int dwWidthInPixels, int dwHeightInPixels)
        {
            MFExtern.MFCopyImage(pDest, lDestStride, pSrc, lSrcStride, dwWidthInPixels * 4, dwHeightInPixels);
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
                Parallel.For(0, dwWidthInPixels>>1, po, x =>
                {
                    var dIndex = y*lDestStride + (x<<1);
                    var sIndex = y*lSrcStride + x;
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
    unsafe private static void TransformImage_NV12(IntPtr pDest, int lDestStride, IntPtr pSrc, int lSrcStride, int dwWidthInPixels, int dwHeightInPixels)
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

    //-------------------------------------------------------------------
    // LetterBoxDstRect
    //
    // Takes a src Rect and constructs the largest possible
    // destination Rect within the specified destination Rect
    // such that the video maintains its current shape.
    //
    // This function assumes that pels are the same shape within both the
    // source and destination Rects.
    //
    //-------------------------------------------------------------------
    private static Rect LetterBoxRect(Rect rcSrc, Rect rcDst)
        {
            int iDstLBWidth;
            int iDstLBHeight;

            if (MulDiv((int)rcSrc.Width, (int)rcDst.Height, (int)rcSrc.Height) <= rcDst.Width)
            {
                // Column letter boxing ("pillar box")

                iDstLBWidth = MulDiv((int)rcDst.Height, (int)rcSrc.Width, (int)rcSrc.Height);
                iDstLBHeight = (int)rcDst.Height;
            }
            else
            {
                // Row letter boxing.

                iDstLBWidth = (int)rcDst.Width;
                iDstLBHeight = MulDiv((int)rcDst.Width, (int)rcSrc.Height, (int)rcSrc.Width);
            }

            // Create a centered Rect within the current destination rect

            var left = rcDst.Left + ((rcDst.Width - iDstLBWidth) / 2);
            var top = rcDst.Top + ((rcDst.Height - iDstLBHeight) / 2);

            var rc = new Rect(left, top, iDstLBWidth, iDstLBHeight);

            return rc;
        }

        //-----------------------------------------------------------------------------
        // CorrectAspectRatio
        //
        // Converts a Rect from the source's pixel aspect ratio (PAR) to 1:1 PAR.
        // Returns the corrected Rect.
        //
        // For example, a 720 x 486 rect with a PAR of 9:10, when converted to 1x1 PAR,
        // is stretched to 720 x 540.
        //-----------------------------------------------------------------------------
        private static Rect CorrectAspectRatio(Rect src, MFRatio srcPAR)
        {
            // Start with a Rect the same size as src, but offset to the origin (0,0).
            var rc = new Rect(0, 0, src.Right - src.Left, src.Bottom - src.Top);
            var rcNewWidth = rc.Right;
            var rcNewHeight = rc.Bottom;

            if ((srcPAR.Numerator != 1) || (srcPAR.Denominator != 1))
            {
                // Correct for the source's PAR.

                if (srcPAR.Numerator > srcPAR.Denominator)
                {
                    // The source has "wide" pixels, so stretch the width.
                    rcNewWidth = MulDiv((int)rc.Right, srcPAR.Numerator, srcPAR.Denominator);
                }
                else if (srcPAR.Numerator < srcPAR.Denominator)
                {
                    // The source has "tall" pixels, so stretch the height.
                    rcNewHeight = MulDiv((int)rc.Bottom, srcPAR.Denominator, srcPAR.Numerator);
                }
                // else: PAR is 1:1, which is a no-op.
            }

            rc = new Rect(0, 0, rcNewWidth, rcNewHeight);
            return rc;
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

            if (Failed(hr))
            {
                // Attribute not set. Try to calculate the default stride.
                Guid subtype;

                var width = 0;
                // ReSharper disable once TooWideLocalVariableScope
                // ReSharper disable once RedundantAssignment
                var height = 0;

                // Get the subtype and the image size.
                hr = pType.GetGUID(MFAttributesClsid.MF_MT_SUBTYPE, out subtype);
                if (Succeeded(hr))
                {
                    hr = CProcess.MfGetAttributeSize(pType, out width, out height);
                }

                if (Succeeded(hr))
                {
                    var f = new FourCC(subtype);

                    hr = MFExtern.MFGetStrideForBitmapInfoHeader(f.ToInt32(), width, out lStride);
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

        #endregion
        // ReSharper enable InconsistentNaming
    }
}
