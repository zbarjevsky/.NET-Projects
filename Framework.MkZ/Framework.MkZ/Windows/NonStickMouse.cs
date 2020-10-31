using Microsoft.VisualBasic.ApplicationServices;
using MZ.Tools;
using MZ.Windows;
using MZ.WPF;
using MZ.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MZ.Tools
{
    public class NonStickMouse : IDisposable
    {
        private class BorderBetweenDisplays
        {
            public int Index = -1;
            public double FromDisplayToDisplay_Top = -1;
            public double FromDisplayToDisplay_Bottom = -1;
            public double DisplayToDisplay_X = -1;

            public Rect Bounds 
            { 
                get 
                { 
                    return new Rect(DisplayToDisplay_X, FromDisplayToDisplay_Top, 0, FromDisplayToDisplay_Bottom - FromDisplayToDisplay_Top); 
                } 
            }
        }

        private List<WpfScreen> _screens = WpfScreen.AllScreens();
        private bool _lBtnDown, _rBtnDown;
        private BorderBetweenDisplays[] _borders = null;
        private User32.POINT _prev = new User32.POINT();

        private bool IsButtonDown {  get { return _lBtnDown || _rBtnDown; } }

        public static NonStickMouse Instance { get; private set; }

        static NonStickMouse()
        {
            Instance = new NonStickMouse();
            Instance.Init();
        }

        public NonStickMouse()
        {
            MouseHook.Hook.OnMouseMessage += mouseHook_OnMouseMessage;
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        }

        public void Dispose()
        {
            MouseHook.Hook.Enabled = false;
            MouseHook.Hook.OnMouseMessage -= mouseHook_OnMouseMessage;
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
            Instance = null;
        }


        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
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
                    Index = i - 1,
                    DisplayToDisplay_X = _screens[i - 1].DeviceBounds.Right,
                    FromDisplayToDisplay_Top = Math.Max(_screens[i].DeviceBounds.Top, _screens[i-1].DeviceBounds.Top),
                    FromDisplayToDisplay_Bottom = Math.Min(_screens[i].DeviceBounds.Bottom, _screens[i - 1].DeviceBounds.Bottom)
                };
            }
        }

        private void mouseHook_OnMouseMessage(object sender, MouseHook.MouseEventArgs32 e)
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

        private void OnMouseMove(MouseHook.MouseEventArgs32 e)
        {
            User32.POINT ptCurr = e.MessageData.pt;
            if (ptCurr.Equals(_prev))
            {
                //Debug.WriteLine("SKIP => Mouse Position:{0}", _prev);
                return; //already been there
            }

            User32.POINT ptPrev = _prev;
            _prev = e.MessageData.pt;

            if (IsButtonDown || _screens.Count == 1 || !MouseHook.Hook.Enabled)
                return; //ignore dragging or if one sceen only or if not enabled

            int screenSrcIndex = WpfScreen.ScreenIndexFromPoint(ptPrev.X, ptPrev.Y);
            if (screenSrcIndex < 0) //point out of screens
                return;

            int screenDstIndex = WpfScreen.ScreenIndexFromPoint(ptCurr.X, ptCurr.Y);
            if (screenSrcIndex == screenDstIndex)
                return;

            User32.POINT ptCorrected = CorrectIntoOtherScreenProportionally(ptPrev, ptCurr, screenSrcIndex);
            if (ptCorrected.X != 0 || ptCorrected.Y != 0)
            {
                _prev = ptCorrected; //already handled
                User32.SetCursorPos(ptCorrected.X, ptCorrected.Y);
            }
        }

        //if screens are different height - move mouse vertical proportionally to border
        private User32.POINT CorrectIntoOtherScreenProportionally(User32.POINT ptPrev, User32.POINT ptCurr, int screenSrcIndex)
        {
            User32.POINT delta = ptCurr - ptPrev;
            Rect rFrom = _screens[screenSrcIndex].DeviceBounds;
            Rect rTo;

            int maxDistanceFromBorder = 1 + Math.Abs(delta.X);

            if (Math.Abs(ptCurr.X - _screens[screenSrcIndex].DeviceBounds.Left) < maxDistanceFromBorder) //close to left side
            {
                if (screenSrcIndex == 0 || delta.X >= 0) //left most or move away
                    return new User32.POINT();
                rTo = _screens[screenSrcIndex - 1].DeviceBounds;
                Debug.WriteLine("LEFT => Mouse Position:{0} Delta:{1}", ptCurr, delta);
            }
            else if (Math.Abs(ptCurr.X - _screens[screenSrcIndex].DeviceBounds.Right) < maxDistanceFromBorder) //close to right side
            {
                if (screenSrcIndex == _screens.Count - 1 || delta.X <= 0) //right most or move away
                    return new User32.POINT();
                rTo = _screens[screenSrcIndex + 1].DeviceBounds;
                Debug.WriteLine("RIGHT => Mouse Position:{0} Delta:{1}", ptCurr, delta);
            }
            else
            {
                //Debug.WriteLine("MIDDLE => Mouse Position:{0} Delta:{1}", ptCurr, delta);
                return new User32.POINT();
            }

            int dstY = CalcProportionalY(ptCurr, rFrom, rTo, delta);
            //int dstY = CorrectYIfNeeded(ptCurr, rFrom, rTo, delta);

            User32.POINT corrected = new User32.POINT(ptCurr.X + delta.X, dstY);
            Debug.WriteLine("Corrected Mouse Position: {0} Delta:{1}", corrected, delta);
            return corrected;
        }

        private int CalcProportionalY(User32.POINT ptCurr, Rect rFrom, Rect rTo, User32.POINT delta)
        {
            Debug.WriteLine("Current Point: {0}", ptCurr);
            double srcRatioFromTop = (ptCurr.Y - rFrom.Top) / rFrom.Height;
            if (srcRatioFromTop < 0 || srcRatioFromTop >= 1)
                srcRatioFromTop = 0.5;

            double dstY = rTo.Top + srcRatioFromTop * rTo.Height;
            Debug.WriteLine("SrcRatio: {0:0.00} DstHeight:{1:0.0} DstY: {2:0.0}", srcRatioFromTop, rTo.Height, dstY);

            return (int)dstY;
        }

        private int CorrectYIfNeeded(User32.POINT ptCurr, Rect rFrom, Rect rTo, User32.POINT delta)
        {
            if (ptCurr.Y > rTo.Top && ptCurr.Y < rTo.Bottom)
                return ptCurr.Y + delta.Y; //no correction needed

            const int offsetY = 80;
            int y = (int)rTo.Top + offsetY;
            if (ptCurr.Y >= rTo.Bottom)
                y = (int)(rTo.Bottom - offsetY);

            return y;
        }

        /// <summary>
        /// correct mouse movement if stuck between monitors
        /// </summary>
        /// <param name="enable"></param>
        public void EnableMouseCorrection(bool enable)
        {
            MouseHook.Hook.Enabled = enable;
        }
    }
}
