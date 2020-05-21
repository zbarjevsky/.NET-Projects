using Microsoft.WindowsAPICodePack.Taskbar;
using MZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.WinForms
{
    public class VerticalProgressBar : ProgressBar
    {
        private const int WM_USER = 0x400;
        private const int PBM_SETSTATE = 16;
        //private const int PBST_PAUSED = 0x0003;
        //private const int PBST_ERROR = 0x0002;
        //private const int PBST_NORMAL = 0x0001;

        private ProgressState _state = ProgressState.PBST_NORMAL;

        public enum ProgressState : int
        {
            PBST_NORMAL = 0x0001,
            PBST_ERROR = 0x0002,
            PBST_PAUSED = 0x0003
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                return cp;
            }
        }

        public void SetColorGreen()
        {
            SetState(ProgressState.PBST_NORMAL);
        }

        public void SetColorYellow()
        {
            SetState(ProgressState.PBST_PAUSED);
        }

        public void SetColorRed()
        {
            SetState(ProgressState.PBST_ERROR);
        }

        public void SetState(ProgressState state)
        {
            if (_state == state)
                return;
            _state = state;

            User32.SendMessage(this.Handle, WM_USER + PBM_SETSTATE, (uint)state, 0);
        }
    }
}
