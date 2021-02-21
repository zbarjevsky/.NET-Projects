using MkZ.Windows;
using MkZ.Windows.Win32API;
using MkZ.WPF;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace MkZ.Tools
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
        //single instance
        private static MouseHook _instance = new MouseHook();

        private User32_MouseHook.HookProcDelegate _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public event EventHandler<MouseEventArgs32> OnMouseMessage = delegate { };

        /// <summary>
        /// Use this to hook/unhook mose messages
        /// </summary>
        public bool Enabled
        {
            get { return _hookID != IntPtr.Zero; }
            set
            {
                if(value)
                {
                    if(_hookID == IntPtr.Zero)
                        _hookID = SetHook(_proc);
                }
                else //Dispose
                {
                    UnHook(ref _hookID);
                }
            }
        }

        public static MouseHook Hook { get { return _instance; } }

        private MouseHook()
        {
            _proc = LowLevelMouseProc;
            MessageUtil.Init();
        }

        ~MouseHook()
        {
            UnHook(ref _hookID);
        }

        public void Dispose()
        {
            UnHook(ref _hookID);
        }

        //https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)
        private IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                User32_MouseHook.MouseMessages msg = (User32_MouseHook.MouseMessages)wParam;
                User32_MouseHook.MSLLHOOKSTRUCT hookStruct = (User32_MouseHook.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(User32_MouseHook.MSLLHOOKSTRUCT));
                MouseEventArgs32 e = new MouseEventArgs32(msg, ref hookStruct);

                MessageUtil.BeginInvoke(() => OnMouseMessage(null, e));
            }

            return User32_MouseHook.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static IntPtr SetHook(User32_MouseHook.HookProcDelegate proc)
        {
            IntPtr hook = User32_MouseHook.SetWindowsHookEx(User32_MouseHook.HookType.WH_MOUSE_LL, proc, User32.GetModuleHandle("user32"), 0);
            if (hook == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception();
            }
            return hook;
        }

        private static void UnHook(ref IntPtr hookID)
        {
            if (hookID != IntPtr.Zero)
                User32_MouseHook.UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }

        public class MouseEventArgs32 : EventArgs
        {
            public User32_MouseHook.MouseMessages Message { get; }
            public User32_MouseHook.MSLLHOOKSTRUCT MessageData { get; }
            public MouseEventArgs32(User32_MouseHook.MouseMessages msg, ref User32_MouseHook.MSLLHOOKSTRUCT hookStruct)
            {
                Message = msg;
                MessageData = hookStruct;
            }
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
    }
}