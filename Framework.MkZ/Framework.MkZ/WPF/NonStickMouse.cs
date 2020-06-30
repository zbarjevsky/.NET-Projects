using MZ.Tools;
using MZ.Windows;
using MZ.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MZ.WPF
{
    public class NonStickMouse
    {
        private class BorderBetweenDisplays
        {
            public double fromDisplaytoDisplay_Top = -1;
            public double fromDisplaytoDisplay_Bottom = -1;
            public double DisplaytoDisplay_X = -1;
        }

        static List<WpfScreen> _screens = WpfScreen.AllScreens();
        static bool _lBtnDown, _rBtnDown;
        static BorderBetweenDisplays[] _borders = null;
        static int _prevX = -1;
        static bool _enableCorrection = true;

        static bool IsButtonDown {  get { return _lBtnDown || _rBtnDown; } }

        static NonStickMouse()
        {
            MouseHook.Hook.OnMouseMessage += mouseHook_OnMouseMessage;
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            Init();
        }

        private static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            Init();
        }

        private static void Init()
        {
            _screens = WpfScreen.AllScreens();
            //arrange displays by X
            _screens.Sort((s1, s2) => (int)(s1.DeviceBounds.Left - s2.DeviceBounds.Left));

            foreach (var scr in _screens)
                Debug.WriteLine(scr);

            _borders = new BorderBetweenDisplays[_screens.Count - 1];

            var bounds = WPF_Helper.VirtualScreenBounds();

            for (int i = 1; i < _screens.Count; i++)
            {
                _borders[i - 1] = new BorderBetweenDisplays() 
                {
                    DisplaytoDisplay_X = _screens[i-1].DeviceBounds.Right,
                    fromDisplaytoDisplay_Top = _screens[i].DeviceBounds.Top,
                };
            }
        }

        private static void mouseHook_OnMouseMessage(object sender, MouseHook.MouseEventArgs32 e)
        {
            switch (e.Message)
            {
                case User32_MouseHook.MouseMessages.WM_LBUTTONDOWN:
                    _lBtnDown = true;
                    break;
                case User32_MouseHook.MouseMessages.WM_LBUTTONUP:
                    _lBtnDown = false;
                    break;
                case User32_MouseHook.MouseMessages.WM_MOUSEMOVE:
                    OnMouseMove(e);
                    break;
                case User32_MouseHook.MouseMessages.WM_MOUSEWHEEL:
                    break;
                case User32_MouseHook.MouseMessages.WM_RBUTTONDOWN:
                    _rBtnDown = true;
                    break;
                case User32_MouseHook.MouseMessages.WM_RBUTTONUP:
                    _rBtnDown = false;
                    break;
                default:
                    break;
            }
        }

        private static void OnMouseMove(MouseHook.MouseEventArgs32 e)
        {
            var pt = e.MessageData.pt;
            int delta = pt.X - _prevX;
            _prevX = pt.X;

            if (IsButtonDown || _screens.Count == 1 || !_enableCorrection)
                return; //ignore dragging or if one sceen only or if not enabled

            int currentScreenIndex = ScreenFromPoint(pt);
            if (currentScreenIndex < 0) //point out of screen
                return;

            Rect rFrom = _screens[currentScreenIndex].DeviceBounds;
            Rect rTo = _screens[currentScreenIndex].DeviceBounds;

            if (Math.Abs(pt.X - _screens[currentScreenIndex].DeviceBounds.Left) < 2) //close to left side
            {
                if (currentScreenIndex == 0 || delta >= 0) //left most or move away
                    return;
                rTo = _screens[currentScreenIndex - 1].DeviceBounds;
            }
            else if(Math.Abs(pt.X - _screens[currentScreenIndex].DeviceBounds.Right) < 2) //close to right side
            {
                if (currentScreenIndex == _screens.Count - 1 || delta <= 0) //right most or move away
                    return;
                rTo = _screens[currentScreenIndex + 1].DeviceBounds;

            }
            else
            {
                return;
            }

            if (pt.Y > rTo.Top && pt.Y < rTo.Bottom)
                return; //no correction needed

            const int offsetY = 80;
            int y = (int)rTo.Top + offsetY;
            if (pt.Y >= rTo.Bottom)
                y = (int)(rTo.Bottom - offsetY);

            Debug.WriteLine("Corrected Mouse Position: X:{0} Y:{1}", pt.X - delta, y);
            User32.SetCursorPos(pt.X - delta, y);
        }

        private static int ScreenFromPoint(User32.POINT pt)
        {
            for (int i = 0; i < _screens.Count; i++)
            {
                if (_screens[i].DeviceBounds.Contains(pt.X, pt.Y))
                    return i;
            };
            return -1;
        }

        /// <summary>
        /// correct mouse movement if stuck between monitors
        /// </summary>
        /// <param name="enable"></param>
        public static void EnableMouseCorrection(bool enable)
        {
            MouseHook.Hook.Enabled = _enableCorrection = enable;
        }
    }
}
