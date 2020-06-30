using MediaFoundation;
using MediaFoundation.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFCaptureAlt
{
    public class DrawBmpDevice : COMBase
    {
        PictureBox _pic;
        Bitmap _bmp;
        int m_width, m_height;

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        unsafe private static extern void CopyMemory(byte* Destination, byte* Source, [MarshalAs(UnmanagedType.U4)] uint Length);

        public DrawBmpDevice(PictureBox pic)
        {
            _pic = pic;
            _bmp = new Bitmap(640, 480);
        }

        public void CreateDevice()
        {
            m_width = 640; m_height = 480;

            _bmp = new Bitmap(m_width, m_height);
            _pic.Image = _bmp;
        }

        public void DrawFrame(IMFMediaBuffer pCaptureDeviceBuffer)
        {
            HResult hr = HResult.S_OK;
            IntPtr pbScanline0;
            int lStride = 0;

            using (VideoBufferLock xbuffer = new VideoBufferLock(pCaptureDeviceBuffer))
            {
                hr = TestCooperativeLevel();
                if (Failed(hr)) { goto done; }

                // Lock the video buffer. This method returns a pointer to the first scan
                // line in the image, and the stride in bytes.

                hr = xbuffer.LockBuffer(m_lDefaultStride, m_height, out pbScanline0, out lStride);
                if (Failed(hr)) { goto done; }

                // Copy in bitmap
                if (m_Data != IntPtr.Zero)
                {
                    unsafe
                    {
                        byte* ipSource = (byte*)m_Data;
                        byte* ipDest = (byte*)pbScanline0;

                        for (int x = 0; x < m_LogoHeight; x++)
                        {
                            CopyMemory(ipDest, ipSource, (uint)m_LogoStride);
                            ipDest += lStride;
                            ipSource += m_LogoStride;
                        }
                    }
                }

                // Get the swap-chain surface.
                pSurf = m_pSwapChain.GetBackBuffer(0);

                // Lock the swap-chain surface and get Graphic stream object.
                DataRectangle dr = pSurf.LockRectangle(LockFlags.NoSystemLock);

                try
                {
                    using (dr.Data)
                    {
                        // Convert the frame. This also copies it to the Direct3D surface.
                        m_convertFn(dr.Data.DataPointer, dr.Pitch, pbScanline0, lStride, m_width, m_height);
                    }
                }
                finally
                {
                    res = pSurf.UnlockRectangle();
                    MFError.ThrowExceptionForHR(res.Code);
                }
            }

            if (res.IsSuccess)
            {
                // Present the frame.
                res = m_pDevice.Present();
                hr = (HResult)res.Code;
            }

        done:
            SafeRelease(pBB);
            SafeRelease(pSurf);

            return hr;
        }

        private HResult TestCooperativeLevel()
        {
            return HResult.S_OK;
        }
    }
}
