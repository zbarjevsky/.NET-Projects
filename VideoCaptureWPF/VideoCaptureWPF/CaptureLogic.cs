using DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace MkZ.WPF.VideoCapture
{
    public class CaptureLogic : IDisposable
    {
        //IntPtr hWnd = IntPtr.Zero;
        //IntPtr hWndChild = IntPtr.Zero;
        //private Microsoft.UI.Windowing.AppWindow _apw;

        IVideoWindow g_pVW = null;
        IMediaControl g_pMC = null;
        IMediaEventEx g_pME = null;
        IGraphBuilder g_pGraph = null;
        ICaptureGraphBuilder2 g_pCapture = null;
        private enum PLAYSTATE { Stopped, Paused, Running, Init };
        //PLAYSTATE g_psCurrent = PLAYSTATE.Stopped;
        private const int WM_GRAPHNOTIFY = Win32.WM_APP + 1;
        IVMRMixerBitmap9 g_pMP = null;
        bool g_bOverLay = false;

        private Win32.SUBCLASSPROC SubClassDelegate;

        private const int _nXCaptureWindow = 10, _nYCaptureWindow = 10, _nWidthCaptureWindow = 640, _nHeightCaptureWindow = 480;
        IVMRWindowlessControl9 g_pWC = null;

        //IntPtr m_hWndContainer = IntPtr.Zero;
        ControlHost _wndContainer = null;
        Grid _parent = null;

        public CaptureLogic(Grid parent, string deviceName)
        {
            _parent = parent;

            //hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            //var wih = new WindowInteropHelper(this);
            //hWnd = wih.Handle;

            // For 1.1.0 release (layered child)
            // hWndChild = FindWindowEx(hWnd, IntPtr.Zero, "Microsoft.UI.Content.ContentWindowSiteBridge", null);
            // hWnd = hWndChild;
            // m_hWndContainer = CreateWindowEx(WS_EX_TRANSPARENT | WS_EX_LAYERED, "Static", "", WS_VISIBLE | WS_CHILD, _nXCaptureWindow, _nYCaptureWindow, _nWidthCaptureWindow, _nHeightCaptureWindow, hWnd, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            //m_hWndContainer = Win32.CreateWindowEx(Win32.WS_EX_LAYERED, "Static", "",
            //    Win32.WS_VISIBLE | Win32.WS_CHILD, 
            //    _nXCaptureWindow, _nYCaptureWindow, _nWidthCaptureWindow, _nHeightCaptureWindow, 
            //    hWnd, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            //uint error = Win32.GetLastError();

            _wndContainer = new ControlHost(_nWidthCaptureWindow, _nHeightCaptureWindow);
            _parent.Children.Add(_wndContainer);

            Win32.SetOpacity(_wndContainer.Handle, 100);
            //RECT rect = new RECT(_nXCaptureWindow, _nYCaptureWindow, _nWidthCaptureWindow, _nHeightCaptureWindow);
            //SetRegion(hWndChild, true, ref rect);

            HRESULT hr = CaptureVideo(_wndContainer.Handle, deviceName);

            SubClassDelegate = new Win32.SUBCLASSPROC(Win32.WindowSubClass);
            bool bRet = Win32.SetWindowSubclass(_wndContainer.Handle, SubClassDelegate, 0, 0);

            //Microsoft.UI.WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            //_apw = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
            //_apw.Resize(new Windows.Graphics.SizeInt32(_nWidthCaptureWindow + 190, _nHeightCaptureWindow * 2 + 60));
            //CenterToScreen(hWnd);
        }

        private HRESULT CaptureVideo(IntPtr hWnd, string deviceName)
        {
            HRESULT hr = HRESULT.E_FAIL;
            IBaseFilter pSrcFilter = null;

            hr = GetInterfaces(hWnd);
            if (hr == HRESULT.S_OK)
            {
                hr = g_pCapture.SetFiltergraph(g_pGraph);
                if (hr == HRESULT.S_OK)
                {
                    hr = ConnectCaptureDevice(deviceName, ref pSrcFilter);
                    if (hr == HRESULT.S_OK && pSrcFilter != null)
                    {
                        IntPtr pSrcFilterPtr = Marshal.GetIUnknownForObject(pSrcFilter);
                        hr = g_pGraph.AddFilter(pSrcFilter, "Video Capture");
                        if (hr == HRESULT.S_OK)
                        {
                            IBaseFilter pVideoMixingRenderer9 = (IBaseFilter)Activator.CreateInstance(Type.GetTypeFromCLSID(DirectShowTools.CLSID_VideoMixingRenderer9));
                            hr = g_pGraph.AddFilter(pVideoMixingRenderer9, "Video Mixing Renderer 9");
                            if (hr == HRESULT.S_OK)
                            {
                                IVMRFilterConfig9 pConfig = null;
                                pConfig = (IVMRFilterConfig9)pVideoMixingRenderer9;
                                hr = pConfig.SetRenderingMode((uint)VMR9Mode.VMR9Mode_Windowless);
                                //IVMRWindowlessControl9 pWC = null;
                                g_pWC = (IVMRWindowlessControl9)pVideoMixingRenderer9;
                                if (g_pWC != null)
                                {
                                    hr = g_pWC.SetVideoClippingWindow(_wndContainer.Handle);

                                    //hr = pWC.SetBorderColor((uint)ColorTranslator.ToWin32(System.Drawing.Color.Red));
                                    //RECT rcSrc = new RECT(0, 0, 0, 0);
                                    //RECT rcDest = new RECT(_nXCaptureWindow, _nYCaptureWindow, _nWidthCaptureWindow + _nXCaptureWindow, _nHeightCaptureWindow + _nYCaptureWindow);
                                    RECT rcDest = new RECT(0, 0, _nWidthCaptureWindow, _nHeightCaptureWindow);
                                    //RECT rcDest = new RECT(0, 0, 0, 0);
                                    //hr = pWC.SetVideoPosition(ref rcSrc, ref rcDest);
                                    hr = g_pWC.SetVideoPosition(IntPtr.Zero, ref rcDest);
                                    //hr = pWC.SetVideoPosition(IntPtr.Zero, IntPtr.Zero);
                                    //
                                    //hr = g_pWC.GetNativeVideoSize(out int lpWidth, out int lpHeight, out int lpARWidth, out int lpARHeight);
                                }

                                g_pMP = (IVMRMixerBitmap9)pVideoMixingRenderer9;
                                hr = g_pCapture.RenderStream(DirectShowTools.PIN_CATEGORY_PREVIEW, DirectShowTools.MEDIATYPE_Video, pSrcFilterPtr, null, pVideoMixingRenderer9);
                                //Marshal.ReleaseComObject(pVideoMixingRenderer9);
                            }
                            hr = g_pMC.Run();
                            //g_psCurrent = PLAYSTATE.Running;
                        }
                        Marshal.ReleaseComObject(pSrcFilter);
                    }
                }
            }
            return hr;
        }

        private HRESULT GetInterfaces(IntPtr hWnd)
        {
            HRESULT hr = HRESULT.E_FAIL;
            Type FilterGraphType = Type.GetTypeFromCLSID(DirectShowTools.CLSID_FilterGraph, true);
            object FilterGraph = Activator.CreateInstance(FilterGraphType);
            g_pGraph = (IGraphBuilder)FilterGraph;
            g_pMC = (IMediaControl)FilterGraph;
            g_pVW = (IVideoWindow)FilterGraph;
            g_pME = (IMediaEventEx)FilterGraph;
            if (g_pME != null)
                hr = g_pME.SetNotifyWindow(hWnd, WM_GRAPHNOTIFY, IntPtr.Zero);
            if (hr == HRESULT.S_OK)
            {
                Type CaptureGraphBuilder2Type = Type.GetTypeFromCLSID(DirectShowTools.CLSID_CaptureGraphBuilder2, true);
                object CaptureGraphBuilder2 = Activator.CreateInstance(CaptureGraphBuilder2Type);
                g_pCapture = (ICaptureGraphBuilder2)CaptureGraphBuilder2;
                hr = (g_pCapture != null) ? HRESULT.S_OK : HRESULT.E_FAIL;
            }
            return hr;
        }

        public static HRESULT ConnectCaptureDevice(string deviceName, ref IBaseFilter ppSrcFilter)
        {
            HRESULT hr = HRESULT.E_FAIL;
            IBaseFilter pSrc = null;

            Type CreateDevEnumType = Type.GetTypeFromCLSID(DirectShowTools.CLSID_SystemDeviceEnum, true);
            object CreateDevEnum = Activator.CreateInstance(CreateDevEnumType);
            ICreateDevEnum pCreateDevEnum = (ICreateDevEnum)CreateDevEnum;
            IEnumMoniker pEm;
            hr = pCreateDevEnum.CreateClassEnumerator(DirectShowTools.CLSID_VideoInputDeviceCategory, out pEm, 0);
            if (hr == HRESULT.S_OK && pEm != null)
            {
                uint cFetched;
                IMoniker pMoniker = null;
                while ((hr = pEm.Next(1, out pMoniker, out cFetched)) == HRESULT.S_OK && cFetched > 0)
                {
                    IntPtr pBag = IntPtr.Zero;
                    if (pMoniker != null)
                    {
                        hr = pMoniker.BindToStorage(IntPtr.Zero, null, typeof(IPropertyBag).GUID, out pBag);
                        if (hr == HRESULT.S_OK)
                        {
                            IPropertyBag pPropertyBag = Marshal.GetObjectForIUnknown(pBag) as IPropertyBag;
                            PROPVARIANT var;
                            var.varType = (ushort)VARENUM.VT_BSTR;
                            // ou CLSID
                            hr = pPropertyBag.Read("FriendlyName", out var, null);
                            string sString = Marshal.PtrToStringUni(var.pwszVal);
                            Marshal.Release(pBag);

                            System.Diagnostics.Debug.WriteLine("Video Capture device name : " + sString);

                            if (deviceName == sString)
                            {
                                IntPtr pBaseFilterPtr = IntPtr.Zero;
                                hr = pMoniker.BindToObject(IntPtr.Zero, null, typeof(IBaseFilter).GUID, ref pBaseFilterPtr);
                                if (hr == HRESULT.S_OK)
                                {
                                    pSrc = Marshal.GetObjectForIUnknown(pBaseFilterPtr) as IBaseFilter;
                                    ppSrcFilter = pSrc;
                                    Marshal.ReleaseComObject(pMoniker);
                                    break;
                                }
                            }
                        }
                        Marshal.ReleaseComObject(pMoniker);
                    }
                    else break;
                }
                Marshal.ReleaseComObject(pEm);
            }
            return hr;
        }

        public static List<string> EnumCaptureDevices()
        {
            HRESULT hr = HRESULT.E_FAIL;
            IBaseFilter pSrc = null;
            List<string> devices = new List<string>();

            Type CreateDevEnumType = Type.GetTypeFromCLSID(DirectShowTools.CLSID_SystemDeviceEnum, true);
            object CreateDevEnum = Activator.CreateInstance(CreateDevEnumType);
            ICreateDevEnum pCreateDevEnum = (ICreateDevEnum)CreateDevEnum;
            IEnumMoniker pEm;
            hr = pCreateDevEnum.CreateClassEnumerator(DirectShowTools.CLSID_VideoInputDeviceCategory, out pEm, 0);
            if (hr == HRESULT.S_OK && pEm != null)
            {
                uint cFetched;
                IMoniker pMoniker = null;
                while ((hr = pEm.Next(1, out pMoniker, out cFetched)) == HRESULT.S_OK && cFetched > 0)
                {
                    IntPtr pBag = IntPtr.Zero;
                    if (pMoniker != null)
                    {
                        hr = pMoniker.BindToStorage(IntPtr.Zero, null, typeof(IPropertyBag).GUID, out pBag);
                        if (hr == HRESULT.S_OK)
                        {
                            IPropertyBag pPropertyBag = Marshal.GetObjectForIUnknown(pBag) as IPropertyBag;
                            PROPVARIANT var;
                            var.varType = (ushort)VARENUM.VT_BSTR;
                            // ou CLSID
                            hr = pPropertyBag.Read("FriendlyName", out var, null);
                            string sString = Marshal.PtrToStringUni(var.pwszVal);
                            Marshal.Release(pBag);

                            System.Diagnostics.Debug.WriteLine("Video Capture device name : " + sString);
                            devices.Add(sString);
                        }
                        Marshal.ReleaseComObject(pMoniker);
                    }
                    else break;
                }
                Marshal.ReleaseComObject(pEm);
            }
            return devices;
        }

        public bool ToggleOverlay()
        {
            HRESULT hr = HRESULT.E_FAIL;
            if (!g_bOverLay)
            {
                // Draw Red rectangle
                int nWidth = 200;
                int nHeight = 100;
                IntPtr hDC = Win32.GetDC(IntPtr.Zero);
                IntPtr hDCMem = Win32.CreateCompatibleDC(hDC);
                IntPtr hBitmap = Win32.CreateCompatibleBitmap(hDC, nWidth, nHeight);
                IntPtr hBitmapOld = Win32.SelectObject(hDCMem, hBitmap);
                RECT rc = new RECT(0, 0, nWidth, nHeight);
                IntPtr hBrush = Win32.CreateSolidBrush(System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Red));
                Win32.FillRect(hDCMem, ref rc, hBrush);
                Win32.DeleteObject(hBrush);

                VMR9AlphaBitmap alphaBitmap = new VMR9AlphaBitmap();
                alphaBitmap.dwFlags = (uint)VMRBITMAP.VMRBITMAP_HDC;
                alphaBitmap.hdc = hDCMem;
                alphaBitmap.rSrc = new RECT(0, 0, nWidth, nHeight);
                alphaBitmap.rDest.left = 0.3F;
                alphaBitmap.rDest.right = 0.7F;
                alphaBitmap.rDest.top = 0.3F;
                alphaBitmap.rDest.bottom = 0.7F;
                alphaBitmap.clrSrcKey = System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.White);
                alphaBitmap.dwFlags |= (uint)VMRBITMAP.VMRBITMAP_SRCCOLORKEY;
                alphaBitmap.fAlpha = 0.6F;
                hr = g_pMP.SetAlphaBitmap(alphaBitmap);

                Win32.SelectObject(hDCMem, hBitmapOld);
                Win32.DeleteObject(hBitmap);
                Win32.DeleteDC(hDCMem);
                Win32.ReleaseDC(IntPtr.Zero, hDC);
                g_bOverLay = true;
            }
            else
            {
                VMR9AlphaBitmap alphaBitmap = new VMR9AlphaBitmap();
                alphaBitmap.dwFlags = (uint)VMRBITMAP.VMRBITMAP_DISABLE;
                hr = g_pMP.SetAlphaBitmap(alphaBitmap);
                g_bOverLay = false;
            }

            return g_bOverLay;
        }

        public ImageSource GrabImage()
        {
            IntPtr pDIB = IntPtr.Zero;
            HRESULT hr = g_pWC.GetCurrentImage(out pDIB);
            if (hr == HRESULT.S_OK)
            {
                Win32.BITMAPINFOHEADER bih = new Win32.BITMAPINFOHEADER();
                Marshal.PtrToStructure(pDIB, bih);

                int nSize = bih.biWidth * bih.biHeight * 4;
                byte[] pManagedArray = new byte[nSize];
                long start = pDIB.ToInt64() + bih.biSize;
                Marshal.Copy(new IntPtr(start), pManagedArray, 0, nSize);

                System.Windows.Media.Imaging.WriteableBitmap wb =
                    new System.Windows.Media.Imaging.WriteableBitmap(bih.biWidth, bih.biHeight, 96, 96, PixelFormats.Bgr32, BitmapPalettes.WebPalette);
                wb.WritePixels(new Int32Rect(0, 0, bih.biWidth, bih.biHeight), pManagedArray, bih.biWidth * 4, 0);
                //await wb.PixelBuffer.AsStream().WriteAsync(pManagedArray, 0, pManagedArray.Length);

                hr = g_pWC.GetNativeVideoSize(out int lpWidth, out int lpHeight, out int lpARWidth, out int lpARHeight);


                return wb;
            }

            return null;
        }

        public Int32Rect GetVideoSize()
        {
            if (g_pWC == null)
            {
                return new Int32Rect();
            }

            IntPtr pDIB = IntPtr.Zero;
            HRESULT hr = g_pWC.GetCurrentImage(out pDIB);
            if (hr == HRESULT.S_OK)
            {
                Win32.BITMAPINFOHEADER bih = new Win32.BITMAPINFOHEADER();
                Marshal.PtrToStructure(pDIB, bih);

                int nSize = bih.biWidth * bih.biHeight * 4;

                return new Int32Rect(0, 0, bih.biWidth, bih.biHeight);
            }

            return new Int32Rect();
        }

        public void Dispose()
        {
            g_pMC.Stop();
            Marshal.ReleaseComObject(g_pCapture);
            g_pCapture = null;
            Marshal.ReleaseComObject(g_pWC);
            g_pWC = null;

            if (_parent != null && _wndContainer != null)
            {
                _parent.Children.Remove(_wndContainer);
                _wndContainer.Dispose();
                _wndContainer = null;
            }
        }
    }
    
}
