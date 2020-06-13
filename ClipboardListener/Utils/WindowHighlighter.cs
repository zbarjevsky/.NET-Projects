using MZ.Tools;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ClipboardManager
{
	/// <summary>
	/// Summary description for WindowHighlighter.
	/// </summary>
	public class WindowHighlighter
	{				
		/// <summary>
		/// Highlights the specified window just like Spy++
		/// </summary>
		/// <param name="hWnd"></param>
		public static void Highlight(IntPtr hWnd)
		{
			const float penWidth = 3;
			User32.GetWindowRect(hWnd, out User32.RECT rc);

			IntPtr hDC = User32.GetWindowDC(hWnd);
			if (hDC != IntPtr.Zero)
			{
				using (Pen pen = new Pen(Color.Lime, penWidth))
				{
					using (Graphics g = Graphics.FromHdc(hDC))
					{
						g.DrawRectangle(pen, 0, 0, rc.Right - rc.Left - (int)penWidth, rc.Bottom - rc.Top - (int)penWidth);
					}
				}
			}
			User32.ReleaseDC(hWnd, hDC);
		}

		/// <summary>
		/// Forces a window to refresh, to eliminate our funky highlighted border
		/// </summary>
		/// <param name="hWnd"></param>
		public static void Refresh(IntPtr hWnd)
		{
			User32.InvalidateRect(hWnd, IntPtr.Zero, 1 /* TRUE */);
			User32.UpdateWindow(hWnd);
			User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 
				User32.RDW.RDW_FRAME | User32.RDW.RDW_INVALIDATE | User32.RDW.RDW_UPDATENOW | User32.RDW.RDW_ALLCHILDREN);		
		}
	}
}
