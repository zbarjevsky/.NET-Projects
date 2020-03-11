using DesktopManagerUX.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace DesktopManagerUX
{
    public class Logic
    {

        public static List<AppInfo> ListTasks()
        {
            Process[] processes = Process.GetProcesses();
            List<Process> pp = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList();

            List<AppInfo> apps = new List<AppInfo>();
            apps.Add(AppInfo.GetEmptyAppInfo());

            foreach (Process p in pp)
                apps.Add(new AppInfo(p));

            return apps;
        }

        public static List<DisplayInfo> GetDisplays()
        {
            List<DisplayInfo> list = new List<DisplayInfo>();
            foreach (WpfScreen screen in WpfScreen.AllScreens())
            {
                list.Add(new DisplayInfo(screen));
            }
            return list;
        }

        public static void MoveWindow(Process p, double X, double Y, double nWidth, double nHeight)
        {
            if (p != null && !p.HasExited)
            {
                User32.ShowWindow(p.MainWindowHandle, User32.SWP_SHOWWINDOW);
                User32.SetForegroundWindow(p.MainWindowHandle);
                User32.MoveWindow(p.MainWindowHandle, (int)X, (int)Y, (int)nWidth, (int)nHeight, false);
            }
        }

        public static double SystemScale
        {
            get { return SystemParameters.VirtualScreenHeight / SystemParameters.PrimaryScreenHeight; }
        }


        public static BitmapSource CaptureApplication(Process process, System.Windows.Point DPI)
        {
            User32.RECT rect = User32.RestoreWindow(process.MainWindowHandle);

            //User32.PROCESS_DPI_AWARENESS value;
            //int result = User32.GetProcessDpiAwareness(process.Handle, out value);

            //var rect = new User32.RECT();
            //User32.GetWindowRect(process.MainWindowHandle, ref rect);

            int width = (int)((rect.right - rect.left));// / DPI.X);
            int height = (int)((rect.bottom - rect.top));// / DPI.Y);

            using (Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.Clear(Color.Wheat);

                    //graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
                    IntPtr hdc = graphics.GetHdc();
                    if (!User32.PrintWindow(process.MainWindowHandle, hdc, User32.PW_RENDERFULLCONTENT))// User32.PW_CLIENTONLY))
                    {
                        int error = Marshal.GetLastWin32Error();
                        var exception = new System.ComponentModel.Win32Exception(error);
                        Debug.WriteLine("ERROR: " + error + ": " + exception.Message);
                        // TODO: Throw the exception?
                        return null;
                    }
                    graphics.ReleaseHdc(hdc);
                    graphics.Dispose();
                }

                //bmp.Save("c:\\temp\\test.png", ImageFormat.Png);
                BitmapSource b = GetImageSourceFromBitmap(bmp);
                return b;
            }

        }

        public static BitmapSource GetImageSourceFromBitmap(Bitmap bmp)
        {
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                  bmp.GetHbitmap(),
                                  IntPtr.Zero,
                                  System.Windows.Int32Rect.Empty,
                                  BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
            return bs;
        }
    }
}
