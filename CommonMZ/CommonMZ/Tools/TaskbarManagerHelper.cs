using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Tools
{
    //http://www.java2s.com/Open-Source/CSharp_Free_Code/API/Download_C4F_Managed_Taskbar_Sample.htm
    //https://rudigroblerwp.wordpress.com/2011/09/26/anatomy-of-the-windows-7-taskbar-thumbnailtoolbarbutton/
    public static class TaskbarManagerHelper
    {
        private const int COUNT = 7;
        private static ThumbnailToolbarButton[] _buttons = new ThumbnailToolbarButton[COUNT];

        static TaskbarManagerHelper()
        {
            for (int i = 0; i < COUNT; i++)
            {
                string txt = (i + 1).ToString();
                _buttons[i] = new ThumbnailToolbarButton(CreateIcon(txt), txt);
                _buttons[i].DismissOnClick = false;
                _buttons[i].IsInteractive = true;
                _buttons[i].Visible = true;
                _buttons[i].Enabled = true;
                _buttons[i].Click += TaskbarButton_Click;
            }
        }

        public static Action<string> ButtonClicked = (tooltip) => { };

        public static void Init(IntPtr hWnd)
        {
            TaskbarManager.Instance.ThumbnailToolbars.AddButtons(hWnd, _buttons);
        }

        public static void UpdateButtons(ListView.ListViewItemCollection items, string disableTooltip)
        {
            for (int i = 0; i < COUNT; i++)
            {
                if (i < items.Count)
                {
                    UpdateButtonText(i, items[i].Text, items[i].Text != disableTooltip, true);
                    UpdateButtonIcon(i, items[i]);
                }
                else //hide
                    UpdateButtonText(i, "", false, false);
            }
        }

        public static void UpdateButtons(List<string> tooltips, string disableTooltip)
        {
            for (int i = 0; i < COUNT; i++)
            {
                if (i < tooltips.Count)
                    UpdateButtonText(i, tooltips[i], tooltips[i] != disableTooltip, true);
                else //hide
                    UpdateButtonText(i, "", false, false);
            }
        }

        private static void UpdateButtonIcon(int idx, ListViewItem item)
        {
            Image img = item.ImageList.Images[item.ImageKey];
            Icon ico = Icon.FromHandle(((Bitmap)img).GetHicon());
            if (_buttons[idx].Icon != null)
                _buttons[idx].Icon.Dispose();
            _buttons[idx].Icon = ico;
        }

        public static void UpdateButtonText(int idx, string tooltip, bool enable, bool show = true)
        {
            if (idx >= 0 && idx < COUNT)
            {
                _buttons[idx].Tooltip = tooltip;
                _buttons[idx].Enabled = enable;
                _buttons[idx].Visible = show;
            }
        }

        private static void TaskbarButton_Click(object sender, ThumbnailButtonClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button clicked: "+e.ThumbnailButton.Tooltip);
            e.ThumbnailButton.Enabled = false; //avoid double click
            ButtonClicked(e.ThumbnailButton.Tooltip);
        }

        private static Icon CreateIcon(string text, int size = 16)
        {
            using (Bitmap bmp = new Bitmap(size, size))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    //"Engravers MT", "Goudy Stout", "Modern No. 20", "Mistral", "OCR A Extended"
                    //"Showcard Gothic" "Stencil" "Snap ITC" "Curlz MT"
                    Font _font = new Font("Stencil", size, FontStyle.Bold, GraphicsUnit.Pixel);
                    g.Clear(Color.Transparent);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                    g.DrawString(text, _font, Brushes.White, 0, -2);
                    IntPtr hIcon = (bmp.GetHicon());
                    Icon icon = System.Drawing.Icon.FromHandle(hIcon);
                    //DestroyIcon(hIcon.ToInt32);
                    return icon;
                }
            }
        }
    }
}
