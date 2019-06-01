using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MZ.WPF.MessageBox;

namespace DiskCryptorHelper
{
    public partial class HideDriveLetterControl : UserControl
    {
        public const string DRIVE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public void ReloadList()
        {
            try
            {
                m_listDriveLetters.Items.Clear();

                DriveInfo[] driveInfos = System.IO.DriveInfo.GetDrives();

                foreach (char c in DRIVE_LETTERS)
                {
                    bool isHidden = HideDriveLetter.IsDriveHidden(c);
                    ListViewItem itm = m_listDriveLetters.Items.Add(c.ToString());
                    itm.SubItems.Add(GetDriveDescription(c, driveInfos));
                    itm.Checked = isHidden;
                }
            }
            catch (Exception err)
            {
                PopUp.MessageBox(err.Message, "HideDriveLetter: Load");
            }
        }

        public HideDriveLetterControl()
        {
            InitializeComponent();
        }

        private void HideDriveLetterControl_Load(object sender, EventArgs e)
        {
            ReloadList();
        }

        private string GetDriveDescription(char driveLetter, DriveInfo[] driveInfos)
        {
            foreach (DriveInfo drive in driveInfos)
            {
                if (drive.Name[0] == driveLetter)
                {
                    if (drive.DriveType == DriveType.Network)
                        return "Network Drive";

                    if (drive.IsReady)
                        return string.Format("{0} ({1}) : {2} {3}", 
                            drive.VolumeLabel, drive.Name, drive.DriveType, drive.DriveFormat);

                    return string.Format("{0} ({1}) : {2}",
                            "? Not Ready ?", drive.Name, drive.DriveType);
                }
            }
            return "--:--";
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
