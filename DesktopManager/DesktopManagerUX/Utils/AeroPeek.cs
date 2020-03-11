using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopManagerUX.Utils
{
    //https://stackoverflow.com/questions/6450223/windows-aero-peek-api
    //InvokeAeroPeek(enterPeekMode, target, caller, pType, new IntPtr(32), 0x3244);

    class AeroPeek
    {
        ///<summary>
        /// These flags are used in conjunction with the Aero Peek API.
        /// </summary>
        public enum PeekTypes : long
        {
            /// <summary>
            /// This flag is here only for completeness and is not used
            /// </summary>
            NotUsed = 0,
            /// <summary>
            /// Denotes that the Peek API is to operate on the desktop
            /// </summary>
            Desktop = 1,
            /// <summary>
            /// Denotes that the Peek API is to operate on a window.
            /// </summary>
            Window = 3
        }

        /// <summary>
        /// This is the *Almighty* Aero Peek API!
        /// </summary>
        /// <param name="EM">True if we're going into peek mode; False if we're coming out of it.</param>
        /// <param name="PH">The handle of the window we want to put into peek mode; 
        /// IntPtr.Zero if we're coming out of peek mode or peeking on the desktop.</param>
        /// <param name="C">The handle of the window calling the API method.</param>
        /// <param name="pT">One of the <see cref="PeekTypes"/> enum members. 
        /// Pass <see cref="PeekTypes.Desktop"/> if you want to peek on the desktop and <see cref="PeekTypes.Window"/> if you want to peek on a window. <see cref="PeekTypes.None"/> is unused but, there for completeness.</param>
        /// <param name="hPN0">When going into or out of peek mode, always pass new IntPtr(32) for this parameter.</param>
        /// <param name="iFI0">When going into or out of peek mode, always pass 0x3244 for this parameter.</param>
        /// <returns></returns>
        [DllImport("dwmapi.dll", EntryPoint = "#113", CharSet = CharSet.Auto, PreserveSig = true, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        static extern int InvokeAeroPeek(bool EM, IntPtr PH, IntPtr C, PeekTypes pT, IntPtr hPN0, int x3244);
    }
}
