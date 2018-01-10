using System;
using System.Collections.Generic;
using System.Text;

namespace ClipboardManager
{
	class CustomRichTextBox : System.Windows.Forms.RichTextBox
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
            int charPosition = base.GetCharIndexFromPosition(pt);
            
			if(_Selecting)
            {
                int length = 0;
                if(charPosition >= _StartPosition)
                {
                    length = charPosition - _StartPosition + 1;
                    this.Select(_StartPosition, length);
                }
                else
                {
                    length = _StartPosition - charPosition;
                    this.Select(charPosition, length);
                }
            }            
            base.OnMouseMove (e);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this._Selecting = true;
            _StartPosition = base.GetCharIndexFromPosition(new System.Drawing.Point(e.X, e.Y));
            base.OnMouseDown (e);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            this._Selecting = false;
            base.OnMouseUp (e);
        }

		protected override void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)
		{
			this._Selecting = false;
			base.OnDragEnter(drgevent);
		}
	}//end class CustomRichTextBox
}//end namespace ClipboardListener
