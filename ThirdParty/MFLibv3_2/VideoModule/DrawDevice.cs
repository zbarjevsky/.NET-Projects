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

        #endregion

        #region Private Members

        ImageDisplayData _notify;
        DeviceImage _device;

        // Format information
        private TransformInformation _formatInformation = new TransformInformation();
        private Rect _rcDest;       // Destination Rect

        #endregion

        //-------------------------------------------------------------------
        // Constructor
        //-------------------------------------------------------------------
        public DrawDevice()
        {
            _notify = null;
            _device = null;
            _rcDest = Rect.Empty;
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

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern int MulDiv(int nNumber, int nNumerator, int nDenominator);

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

        #endregion

        #region Public Methods

        //-------------------------------------------------------------------
        // CreateDevice
        //
        // Create the Direct3D device.
        //-------------------------------------------------------------------
        public HResult CreateDevice(ImageDisplayData notify)
        {
            if (_device != null)
            {
                return S_Ok;
            }

            _notify = notify;
            _device = new DeviceImage(notify);

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

            if ((_formatInformation._format != SlimDX.Direct3D9.Format.Unknown))
            {
                
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
            if (_formatInformation.m_convertFn == null)
            {
                return HResult.MF_E_INVALIDREQUEST;
            }            

            if (_device == null)
            {
                return S_Ok;
            }

            HResult hr = HResult.S_OK;

            //Rect r = new Rect(0, 0, _width, _height);

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

                    hr = xbuffer.LockBuffer(_formatInformation._lDefaultStride, _formatInformation._height, out pbScanline0, out lStride);
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
                    BitmapSource bmp = _device.Convert(pbScanline0, lStride, _formatInformation);

                    if (snap)
                            ImageHelper.SnapShot(bmp, snapFormat);
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

        #endregion

        #region Format Methods

        public HResult GetFormat(int index, out Guid subtype)
        {
            return _formatInformation.GetFormat(index, out subtype);
        }

        public bool IsFormatSupported(Guid subtype)
        {
            return _formatInformation.IsFormatSupported(subtype);
        }

        internal HResult SetVideoType(IMFMediaType pType)
        {
            return _formatInformation.SetVideoType(pType);
        }

        #endregion
        // ReSharper enable InconsistentNaming
    }
}
