using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Tools
{
    public class WindowInfo
    {
        public IntPtr hWnd { get; set; }
        public string Title { get; set; }

        public WindowInfo(IntPtr hWnd, string title)
        {
            Title = title;
            this.hWnd = hWnd;
        }

        public IWin32Window Win32Window { get { return Control.FromHandle(hWnd); } }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Title))
                return Title;
            return "---";
        }
    }
}
