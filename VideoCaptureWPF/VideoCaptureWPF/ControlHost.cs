using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace MkZ.WPF.VideoCapture
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/walkthrough-hosting-a-win32-control-in-wpf?view=netframeworkdesktop-4.8
    /// </summary>
    public class ControlHost : HwndHost
    {
        internal const int
            WS_CHILD = 0x40000000,
            WS_VISIBLE = 0x10000000,
            LBS_NOTIFY = 0x00000001,
            HOST_ID = 0x00000002,
            LISTBOX_ID = 0x00000001,
            WS_VSCROLL = 0x00200000,
            WS_BORDER = 0x00800000;
        
        IntPtr hwndControl;
        IntPtr hwndHost;
        int hostHeight, hostWidth;

        public ControlHost(double height, double width)
        {
            hostHeight = (int)height;
            hostWidth = (int)width;
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            hwndControl = IntPtr.Zero;
            hwndHost = IntPtr.Zero;

            hwndHost = Win32.CreateWindowEx(0, "static", "",
                                      WS_CHILD | WS_VISIBLE,
                                      0, 0,
                                      hostWidth, hostHeight,
                                      hwndParent.Handle,
                                      (IntPtr)HOST_ID,
                                      IntPtr.Zero,
                                      0);

            //hwndControl = Win32.CreateWindowEx(0, "listbox", "",
            //                              WS_CHILD | WS_VISIBLE | LBS_NOTIFY
            //                                | WS_VSCROLL | WS_BORDER,
            //                              0, 0,
            //                              hostWidth, hostHeight,
            //                              hwndHost,
            //                              (IntPtr)LISTBOX_ID,
            //                              IntPtr.Zero,
            //                              0);

            return new HandleRef(this, hwndHost);
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            handled = false;
            return IntPtr.Zero;
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            Win32.DestroyWindow(hwnd.Handle);
        }

        //private IntPtr ControlMsgFilter(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    int textLength;

        //    handled = false;
        //    if (msg == WM_COMMAND)
        //    {
        //        switch ((uint)wParam.ToInt32() >> 16 & 0xFFFF) //extract the HIWORD
        //        {
        //            case LBN_SELCHANGE: //Get the item text and display it
        //                selectedItem = SendMessage(listControl.hwndListBox, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
        //                textLength = SendMessage(listControl.hwndListBox, LB_GETTEXTLEN, IntPtr.Zero, IntPtr.Zero);
        //                StringBuilder itemText = new StringBuilder();
        //                SendMessage(hwndListBox, LB_GETTEXT, selectedItem, itemText);
        //                selectedText.Text = itemText.ToString();
        //                handled = true;
        //                break;
        //        }
        //    }
        //    return IntPtr.Zero;
        //}

        //internal const int
        //  LBN_SELCHANGE = 0x00000001,
        //  WM_COMMAND = 0x00000111,
        //  LB_GETCURSEL = 0x00000188,
        //  LB_GETTEXTLEN = 0x0000018A,
        //  LB_ADDSTRING = 0x00000180,
        //  LB_GETTEXT = 0x00000189,
        //  LB_DELETESTRING = 0x00000182,
        //  LB_GETCOUNT = 0x0000018B;

        //[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        //internal static extern int SendMessage(IntPtr hwnd,
        //                                       int msg,
        //                                       IntPtr wParam,
        //                                       IntPtr lParam);

        //[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        //internal static extern int SendMessage(IntPtr hwnd,
        //                                       int msg,
        //                                       int wParam,
        //                                       [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lParam);

        //[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        //internal static extern IntPtr SendMessage(IntPtr hwnd,
        //                                          int msg,
        //                                          IntPtr wParam,
        //                                          String lParam);
    }
}
