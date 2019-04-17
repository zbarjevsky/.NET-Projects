using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace ClipboardManager
{
	public class CustomRichTextBox : System.Windows.Forms.RichTextBox
	{
        public CustomRichTextBox()
        {
            this.AutoWordSelection = false;
            //this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
        }//end constructor

        private bool _Selecting = false;
        private int _StartPosition = 0;

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
			System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
            int charPosition = this.GetTrueIndexPositionFromPoint(pt);
            
			if(_Selecting)
            {
                int length = 0;
                if(charPosition > _StartPosition)
                {
                    length = charPosition - _StartPosition + 1;
                    this.Select(_StartPosition, length);
                }
                else if(charPosition < _StartPosition)
                {
                    length = _StartPosition - charPosition;
                    this.Select(charPosition, length);
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this._Selecting = true;
            _StartPosition = this.GetTrueIndexPositionFromPoint(new System.Drawing.Point(e.X, e.Y));
            //this.Capture = true;
            base.OnMouseDown (e);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            this._Selecting = false;
            //this.Capture = false;
            base.OnMouseUp (e);
        }

		protected override void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)
		{
			this._Selecting = false;
            //this.Capture = false;
            base.OnDragEnter(drgevent);
		}
	}//end class CustomRichTextBox

    public static class RichTextBoxExtensions
    {
        private const int EM_CHARFROMPOS = 0x00D7;

        public static int GetTrueIndexPositionFromPoint(this RichTextBox rtb, Point pt)
        {
            POINT wpt = new POINT(pt.X, pt.Y);
            int index = (int)SendMessage(new HandleRef(rtb, rtb.Handle), EM_CHARFROMPOS, 0, wpt);

            return index;
        }

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, POINT lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class POINT
    {
        public int x;
        public int y;

        public POINT()
        {
        }

        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}//end namespace ClipboardListener
