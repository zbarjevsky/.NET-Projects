using MZ.WPF;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace MZ.Tools
{
    /// <summary>
    /// https://stackoverflow.com/questions/11607133/global-mouse-event-handler
    /// Usage:
    /// MouseHook _mouseHook = new MouseHook();
    /// _mouseHook.MouseAction += EventAction;
    /// _mouseHook.MouseMove += EventMove;
    ///   private void EventMove(object sender, MouseHook.MouseEventArgs32 e)
    ///   {
    ///       var pt = e._hookStruct.pt;
    ///       Console.WriteLine("Mouse Move: " + pt.ToString());
    ///       if (pt.x == 3840)
    ///       {
    ///           Application.Current.Dispatcher.BeginInvoke(new Action(() =>
    ///                       {
    ///               Debug.WriteLine("SetCursorPos(3850, 540)");
    ///               MouseHook.SetCursorPos(3841, 540);
    ///           }));
    ///       }
    ///   }
    /// </summary>
    public class MouseHook : IDisposable
    {
        private Win32.HookProcDelegate _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public class MouseEventArgs32 : EventArgs
        {
            public Win32.MouseMessages Message { get; }
            public Win32.MSLLHOOKSTRUCT MessageData { get; }
            public MouseEventArgs32(Win32.MouseMessages msg, ref Win32.MSLLHOOKSTRUCT hookStruct)
            {
                Message = msg;
                MessageData = hookStruct;
            }
        }

        public event EventHandler<MouseEventArgs32> OnMouseMessage = delegate { };

        public MouseHook()
        {
            MessageUtil.Init();

            _proc = LowLevelMouseProc;
            _hookID = SetHook();
        }

        ~MouseHook()
        {
            Dispose();
        }

        public static void SetMousePos(int x, int y)
        {
            Win32.SetCursorPos(x, y);
        }

        public void Dispose()
        {
            if (_hookID != IntPtr.Zero)
                Win32.UnhookWindowsHookEx(_hookID);
            _hookID = IntPtr.Zero;
        }

        private IntPtr SetHook()
        {
            IntPtr hook = Win32.SetWindowsHookEx(Win32.HookType.WH_MOUSE_LL, _proc, Win32.GetModuleHandle("user32"), 0);
            if (hook == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception();
            }
            return hook;
        }

        //https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)
        private IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                Win32.MouseMessages msg = (Win32.MouseMessages)wParam;
                Win32.MSLLHOOKSTRUCT hookStruct = (Win32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MSLLHOOKSTRUCT));
                MouseEventArgs32 e = new MouseEventArgs32(msg, ref hookStruct);

                MessageUtil.BeginInvoke(() => OnMouseMessage(null, e));
            }

            return Win32.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public class MessageUtil
        {
            static System.Windows.Forms.Form MainForm = null;
            static System.Windows.Threading.Dispatcher CurrentDispatcher = null;

            static MessageUtil()
            {
                Init();
            }

            public static void Init()
            {
                MainForm = WPF_Helper.TopmostForm();
                if (Application.Current != null)
                    CurrentDispatcher = Application.Current.Dispatcher;
            }

            public static void BeginInvoke(Action action)
            {
                if (CurrentDispatcher != null)
                    CurrentDispatcher.BeginInvoke(action);
                else if (MainForm != null)
                    MainForm.BeginInvoke(action);
                else
                    action();
            }

            public static void ExecuteOnUIThreadWPF(Action action)
            {
                CurrentDispatcher.BeginInvoke(action);
            }

            public static void ExecuteOnUIThreadForm(Action action)
            {
                MainForm.BeginInvoke(action);
            }
        }

        public class Win32
        {
            public delegate IntPtr LowLevelMouseProcDelegate(int nCode, MouseMessages message, MSLLHOOKSTRUCT lParam);
            public delegate IntPtr HookProcDelegate(int code, IntPtr wParam, IntPtr lParam);

            //The WH_MOUSE_LL hook enables you to monitor mouse input events about to be posted in a thread input queue.
            //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa?redirectedfrom=MSDN
            public enum HookType
            {
                WH_MSGFILTER = -1,
                WH_KEYBOARD = 2,
                WH_MOUSE = 7,
                WH_KEYBOARD_LL = 13,
                WH_MOUSE_LL = 14,
            }

            public enum MouseMessages
            {
                WM_LBUTTONDOWN = 0x0201,
                WM_LBUTTONUP = 0x0202,
                WM_MOUSEMOVE = 0x0200,
                WM_MOUSEWHEEL = 0x020A,
                WM_RBUTTONDOWN = 0x0204,
                WM_RBUTTONUP = 0x0205
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct Win32Point
            {
                public Int32 X;
                public Int32 Y;

                public override string ToString()
                {
                    return string.Format("X = {0}, Y = {1}", X, Y);
                }
            }

            //https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msllhookstruct?redirectedfrom=MSDN
            [StructLayout(LayoutKind.Sequential)]
            public struct MSLLHOOKSTRUCT
            {
                public Win32Point pt;
                public uint mouseData;
                public uint flags;
                public uint time;
                public IntPtr dwExtraInfo;
            }

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(HookType hookType, HookProcDelegate lpfn, IntPtr hModule, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetCursorPos(int x, int y);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);
        }
    }
}