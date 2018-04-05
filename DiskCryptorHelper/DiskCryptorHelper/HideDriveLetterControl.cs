using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sD.WPF.MessageBox;

namespace DiskCryptorHelper
{
    public partial class HideDriveLetterControl : UserControl
    {
        public const string driveLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public HideDriveLetterControl()
        {
            InitializeComponent();
        }

        private void HideDriveLetterControl_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (char c in driveLetters)
                {
                    bool isHidden = HideDriveLetter.IsDriveHidden(c);
                    ListViewItem itm = m_listDriveLetters.Items.Add(c.ToString());
                    itm.Checked = isHidden;
                }
            }
            catch (Exception err)
            {
                PopUp.MessageBox(err.Message, "HideDriveLetter: Load");
            }
        }

        private void m_listDriveLetters_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                HideDriveLetter.Hide(e.Item.Text[0], e.Item.Checked);
            }
            catch (Exception err)
            {
                PopUp.MessageBox(err.Message, "HideDriveLetter: ItemChecked");
            }
        }
    }
}
