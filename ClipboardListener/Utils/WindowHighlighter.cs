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
			NativeWIN32.RECT rc = NativeWIN32.GetWindowRect(hWnd);

			IntPtr hDC = NativeWIN32.GetWindowDC(hWnd);
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
			NativeWIN32.ReleaseDC(hWnd, hDC);
		}

		/// <summary>
		/// Forces a window to refresh, to eliminate our funky highlighted border
		/// </summary>
		/// <param name="hWnd"></param>
		public static void Refresh(IntPtr hWnd)
		{
			NativeWIN32.InvalidateRect(hWnd, IntPtr.Zero, 1 /* TRUE */);
			NativeWIN32.UpdateWindow(hWnd);
			NativeWIN32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, NativeWIN32.RDW_FRAME | NativeWIN32.RDW_INVALIDATE | NativeWIN32.RDW_UPDATENOW | NativeWIN32.RDW_ALLCHILDREN);		
		}
	}
}
