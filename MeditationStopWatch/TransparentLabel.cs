using System;
using System.Windows.Forms;
using System.Drawing;

namespace MeditationStopWatch
{
	public class TransparentLabel : Label
	{
		public TransparentLabel() : base()
		{
			TabStop = false;
			//this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			//BackColor = Color.Transparent;
		}

		////Override Creation
		//protected override CreateParams CreateParams
		//{
		//    get
		//    {
		//        CreateParams cp = base.CreateParams;
		//        cp.ExStyle |= 0x20;
		//        return cp;
		//    }
		//}

		//protected override void OnPaintBackground(PaintEventArgs e)
		//{
		//    // do nothing
		//    //base.OnPaintBackground(e);
		//}


		////Paint It
		//protected override void OnPaint(PaintEventArgs e)
		//{
		//    using (SolidBrush TBrush = new SolidBrush(ForeColor))
		//    {
		//        e.Graphics.DrawString(Text, Font, TBrush, -1, 0);
		//    }
		//}
	}
}
