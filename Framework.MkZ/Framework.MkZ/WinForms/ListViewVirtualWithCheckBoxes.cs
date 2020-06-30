using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;

namespace ListViewVirtualMode
{
	/// <summary>
	/// https://www.codeproject.com/Articles/18115/ListView-in-VirtualMode-and-checkboxes
	/// Must have List<ListViewItem> as virtual collection, or deriverd from ListViewItem
	/// </summary>
	public class ListViewVirtualWithCheckBoxes : ListView
	{
		public ListViewVirtualWithCheckBoxes()
		{
			VirtualMode = true;

			// This is what you need, for drawing unchecked checkboxes
			this.OwnerDraw = true;
			this.DrawItem += new DrawListViewItemEventHandler(listView_DrawItem);
            this.DrawColumnHeader += ListViewVirtualWithCheckBoxes_DrawColumnHeader;

			// Redraw when checked or doubleclicked
			this.MouseClick += new MouseEventHandler(listView_MouseClick);
			this.MouseDoubleClick += new MouseEventHandler(listView_MouseDoubleClick);

			this.DoubleBuffered = true;
		}

        private void ListViewVirtualWithCheckBoxes_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			e.DrawDefault = true;
			if (!CheckBoxes)
				return;

			if (!e.Item.Checked)
			{
				e.Item.Checked = true;
				e.Item.Checked = false;
			}
		}

		private void listView_MouseClick(object sender, MouseEventArgs e)
		{
			if (!CheckBoxes)
				return;

			ListView lv = (ListView)sender;
			ListViewItem lvi = lv.GetItemAt(e.X, e.Y);
			if (lvi != null)
			{
				if (e.X < (lvi.Bounds.Left + 16))
				{
					CheckState newState = lvi.Checked ? CheckState.Unchecked : CheckState.Checked;
					CheckState state = lvi.Checked ? CheckState.Checked : CheckState.Unchecked;
					ItemCheckEventArgs evt = new ItemCheckEventArgs(lvi.Index, newState, state);
					this.OnItemCheck(evt);

					lvi.Checked = !lvi.Checked;
					lv.Invalidate(lvi.Bounds);
					this.OnItemChecked(new ItemCheckedEventArgs(lvi));
				}
			}
		}

		private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (!CheckBoxes)
				return;

			ListView lv = (ListView)sender;
			ListViewItem lvi = lv.GetItemAt(e.X, e.Y);
			if (lvi != null)
				lv.Invalidate(lvi.Bounds);
		}
	}
}
