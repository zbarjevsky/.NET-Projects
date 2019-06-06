using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RulerWPF.Tools
{
    public class GraphicsHelper
    {
        private static double _dpi = GetSystemDpi();
        public static double DPI { get { return _dpi; } }

        public static double UpdateSystemDpi()
        {
            _dpi = GetSystemDpi();
            return _dpi;
        }

        private static double GetSystemDpi()
        {
            using (System.Drawing.Graphics screen = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr hdc = screen.GetHdc();

                int virtualWidth = GetDeviceCaps(hdc, DeviceCaps.HORZRES);
                int physicalWidth = GetDeviceCaps(hdc, DeviceCaps.DESKTOPHORZRES);
                screen.ReleaseHdc(hdc);

                return (double)(96f * physicalWidth / virtualWidth);
            }
        }

        private enum DeviceCaps
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,

            /// <summary>
            /// Horizontal width in pixels
            /// </summary>
            HORZRES = 8,

            /// <summary>
            /// Horizontal width of entire desktop in pixels
            /// </summary>
            DESKTOPHORZRES = 118
        }

        /// <summary>
        /// Retrieves device-specific information for the specified device.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="nIndex">The item to be returned.</param>
        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, DeviceCaps nIndex);
    }
}
