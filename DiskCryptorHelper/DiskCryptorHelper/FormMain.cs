using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using DiskCryptorHelper.Properties;
using sD.WPF.MessageBox;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DiskCryptorHelper
{
    public partial class FormMain : Form
    {
        public const string TITLE = "DiskCryptor Commands";

        private DiskCryptor _diskCryptor = new DiskCryptor();

        public FormMain()
        {
            InitializeComponent();

            Text = TITLE;

            _diskCryptor.OnDataReceived = () =>
            {
                Utils.ExecuteOnUIThread(() =>
                {
                    m_txtLog.Text = _diskCryptor.Log.ToString();
                    m_listDrives.Items.Clear();
                    //cache list for this operation
                    DiskCryptor.DriveInfo [] list = _diskCryptor.DriveList.ToArray();
                    foreach (DiskCryptor.DriveInfo info in list)
                    {
                        if (info.IsValidInfo)
                        {
                            ListViewItem item = m_listDrives.Items.Add(info.volume);
                            item.Tag = info;

                            item.SubItems.Add(info.mount_point);
                            item.SubItems.Add(info.size);
                            item.SubItems.Add(info.status);
                        }
                    }
                    SmartSelectFirstAvailableDrive();
                }, this);
            };

            SystemEvents.SessionEnding += SessionEndingEvtHandler;
        }

        const int WM_DEVICECHANGE = 0x0219;
        const int DBT_DEVICEARRIVAL = 0x8000; // system detected a new device
        const int DBT_DEVICEREMOVECOMPLETE = 0x8004; //device was removed
        const int DBT_DEVNODES_CHANGED = 0x0007; //device changed
        const int DBT_DEVTYP_VOLUME = 0x00000002; // logical volume

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                int op = m.WParam.ToInt32();
                if (op == DBT_DEVICEARRIVAL || op == DBT_DEVICEREMOVECOMPLETE || op == DBT_DEVNODES_CHANGED)
                {
                    if (op != DBT_DEVNODES_CHANGED)
                    {
                        int devType = Marshal.ReadInt32(m.LParam, 4);
                        if (devType == DBT_DEVTYP_VOLUME)
                        {
                            //Poll drives
                            Debug.WriteLine("WndProc: WM_DEVICECHANGE");
                            ReloadDriveData(0);
                        }
                    }
                    else
                    {
                        //Poll drives
                        Debug.WriteLine("WndProc: DBT_DEVNODES_CHANGED");
                        ReloadDriveData(0);
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _diskCryptor.ExecuteVersion();
            ReloadDriveData(0);
            m_listDrives_SelectedIndexChanged(sender, e);
            m_sysIcon.Text = "XaXa";

            m_mnuFile.DropDown = m_sysIconMenu;
            m_mnuOptionsHideWhenMinimized.Checked = Settings.Default.HideWhenMinimized;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.WindowsShutDown || 
                e.CloseReason == CloseReason.TaskManagerClosing)
            {
                _diskCryptor.ExecuteUnMountAll();

                File.AppendAllText("C:\\Temp\\Log11.txt", DateTime.Now.ToString("u") + " - Close for: " + e.CloseReason + "\r\n");
            }
            else if(e.CloseReason == CloseReason.UserClosing) //user clicked close button
            {
                //System.Windows.MessageBoxResult res = PopUp.MessageBox(
                //    "Cancel(C), Hide(H) or Exit(X)?", "Close Application",
                //    System.Windows.MessageBoxImage.Question, 
                //    System.Windows.TextAlignment.Center, 
                //    System.Windows.MessageBoxButton.YesNoCancel,
                //    "E_xit", "_Hide");

                //if (res != System.Windows.MessageBoxResult.Yes)
                //{
                //    e.Cancel = true;
                //    if(res == System.Windows.MessageBoxResult.No)
                //        this.Visible = false; //hide
                //}
            }
            else if (e.CloseReason == CloseReason.ApplicationExitCall)
            {

            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_sysIcon.Visible = false;
        }

        private void SessionEndingEvtHandler(object sender, SessionEndingEventArgs e)
        {
            _diskCryptor.ExecuteUnMountAll();

            File.AppendAllText("C:\\Temp\\Log11.txt", DateTime.Now.ToString("u") + " - Session end: " + e.Reason + "\n");
        }

        private void m_btnReload_Click(object sender, EventArgs e)
        {
            ReloadDriveData(0);
        }

        private void ReloadDriveData(int millisecodsTimeout = 800)
        {
            m_btnReload.Enabled = false;

            Task.Factory.StartNew(() =>
            {
                lock (this)
                {
                    Thread.Sleep(millisecodsTimeout);

                    ReloadDriveList();
                    ReloadAvailableDriveLetters();
                    _diskCryptor.ExecuteEnum();
                }

                Utils.ExecuteOnUIThread(() => m_btnReload.Enabled = true, this);
            });
        }

        private void ReloadDriveList()
        {
            List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
            List<UsbEject.Library.Volume> removable_volumes = new List<UsbEject.Library.Volume>();

            Utils.ExecuteOnUIThread(() =>
            {
                m_treeDrives.Nodes.Clear();
                m_mnuEject.DropDownItems.Clear();
                m_btnEject.Enabled = false;

                foreach (UsbEject.Library.Device drive in list)
                {
                    if (drive.RemovableDevices.Count == 0)
                        continue;

                    UsbEject.Library.Volume vol = drive as UsbEject.Library.Volume;
                    if (vol == null || vol.Disks.Count == 0)
                        continue;

                    //if already has this volume
                    if (removable_volumes.FirstOrDefault(v => v.Disks[0].FriendlyName == vol.Disks[0].FriendlyName) != null)
                        continue;

                    removable_volumes.Add(vol);
                    TreeNode node = m_treeDrives.Nodes.Add(vol.Disks[0].FriendlyName);
                    node.Tag = vol;

                    DriveInfo[] driveInfos = System.IO.DriveInfo.GetDrives();
                    List<string> driveLetters = DriveTools.GetAllDriveLettersForDevice(list, vol.Disks[0].FriendlyName);
                    string mnuText = "";
                    foreach (string dr in driveLetters)
                    {
                        mnuText += (dr) + ", ";
                        TreeNode sub = node.Nodes.Add(GetDriveDescription(dr, driveInfos));
                        sub.Tag = vol;
                    }
                    mnuText += "[" + vol.Disks[0].FriendlyName + "]";

                    ToolStripItem mnu = new ToolStripMenuItem(mnuText);
                    mnu.Tag = vol;
                    mnu.Click += mnuEjectDisk_Click;

                    m_mnuEject.DropDownItems.Add(mnu);
                }

                m_mnuEject.Enabled = m_mnuEject.DropDownItems.Count > 0;
                m_treeDrives.ExpandAll();
                if (m_treeDrives.Nodes.Count > 0)
                    m_treeDrives.SelectedNode = m_treeDrives.Nodes[0];
            }, this);
        }

        private string GetDriveDescription(string driveLetter, DriveInfo [] driveInfoList)
        {
            foreach (DriveInfo ifo in driveInfoList)
            {
                if(ifo.Name.Trim('\\') == driveLetter)
                {
                    try
                    {
                        return ifo.Name + " [" + ifo.VolumeLabel + "] Size:"+ ifo.TotalSize.ToString("###,###");
                    }
                    catch (Exception err)
                    {
                        return ifo.Name + " [" + err.Message + "]";
                    }
                }
            }

            return driveLetter;
        }

        private void mnuEjectDisk_Click(object sender, EventArgs e)
        {
            ToolStripItem mnu = sender as ToolStripItem;
            if (mnu == null)
                return;

            UsbEject.Library.Volume vol = mnu.Tag as UsbEject.Library.Volume;
            if (vol == null)
                return;

            ExecuteClickAction(() => UnmountAndEjectDevice(vol), sender);
        }

        private void ReloadAvailableDriveLetters()
        {
            ArrayList driveLetters = new ArrayList(26); // Allocate space for alphabet
            for (int i = 65; i < 91; i++) // increment from ASCII values for A-Z
                driveLetters.Add(Convert.ToChar(i)); // Add uppercase letters to possible drive letters

            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
                driveLetters.Remove(drive[0]); // removed used drive letters from possible drive letters

            Utils.ExecuteOnUIThread(() =>
            {
                m_cmbAvailableDriveLetters.Items.Clear();

                foreach (char drive in driveLetters)
                {
                    m_cmbAvailableDriveLetters.Items.Add(drive + ":"); // add unused drive letters to the combo box
                }

                //default select T or M
                int idx = m_cmbAvailableDriveLetters.Items.IndexOf("T:");
                if (idx < 0)
                    idx = m_cmbAvailableDriveLetters.Items.IndexOf("M:");
                if (idx >= 0)
                    m_cmbAvailableDriveLetters.SelectedIndex = idx;
                else
                    m_cmbAvailableDriveLetters.SelectedIndex = 0;
            }, this);
        }

        private void m_btnMountAll_Click(object sender, EventArgs e)
        {
            ExecuteClickAction(() =>
            {
                _diskCryptor.ExecuteMountAll(m_txtPwd.Text);
                ReloadDriveData();
            }, sender);
        }

        private void m_btnUnmoutAll_Click(object sender, EventArgs e)
        {
            ExecuteClickAction(() =>
            {
                _diskCryptor.ExecuteUnMountAll();
                ReloadDriveData(1200);
            }, sender);
        }

        private void m_btnMount_Click(object sender, EventArgs e)
        {
            ExecuteClickAction(() =>
            {
                m_lblSelected.Text = "Selected: ???";
                if (m_listDrives.SelectedIndices.Count <= 0)
                    return;

                if (string.IsNullOrWhiteSpace(m_txtPwd.Text))
                {
                    MessageBox.Show(this, "Password is empty");
                    return;
                }

                DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;

                _diskCryptor.ExecuteMount(drive, m_cmbAvailableDriveLetters.SelectedItem.ToString(), m_txtPwd.Text);

                ReloadDriveData();
            }, sender);
        }

        private void m_btnUnmount_Click(object sender, EventArgs e)
        {
            ExecuteClickAction(() =>
            {
                m_lblSelected.Text = "Selected: ???";
                if (m_listDrives.SelectedIndices.Count <= 0)
                    return;

                DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;
                Unmount(drive);

                ReloadDriveData();
            }, sender);
        }

        private void Unmount(DiskCryptor.DriveInfo drive)
        {
            _diskCryptor.ExecuteUnMount(drive);
            ReloadDriveData();
        }

        private void m_listDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_lblSelected.Text = "Selected: ???";
            m_btnMount.Enabled = false;
            m_btnUnmount.Enabled = false;
            if (m_listDrives.SelectedIndices.Count <= 0)
                return;

            m_cmbAvailableDriveLetters_SelectedIndexChanged(sender, e);
            DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;

            m_lblSelected.Text = "Selected: " + drive.volume + ", MP: " + drive.mount_point;
            m_btnMount.Enabled = drive.volume.StartsWith("pt") && drive.status.StartsWith("unmounted");
            m_btnUnmount.Enabled = drive.volume.StartsWith("pt") && drive.status.StartsWith("mounted");
        }

        private void SmartSelectFirstAvailableDrive()
        {
            //select last mounted drive
            for (int i = 0; i < m_listDrives.Items.Count; i++)
            {
                DiskCryptor.DriveInfo drive = m_listDrives.Items[i].Tag as DiskCryptor.DriveInfo;
                if (drive.status.StartsWith("mounted"))
                    m_listDrives.Items[i].Selected = true;
            }

            if (m_listDrives.SelectedItems.Count != 0)
                return;

            //if no munted drives
            //select last unmounted, but not system
            for (int i = 0; i < m_listDrives.Items.Count; i++)
            {
                DiskCryptor.DriveInfo drive = m_listDrives.Items[i].Tag as DiskCryptor.DriveInfo;
                if (string.IsNullOrWhiteSpace(drive.mount_point) && drive.status.Equals("unmounted"))
                    m_listDrives.Items[i].Selected = true;
            }
        }

        private void m_mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void m_mnuUnmountAll_Click(object sender, EventArgs e)
        {
            m_btnUnmoutAll_Click(sender, e);
        }

        private void m_btnUnmountAllandBSOD_Click(object sender, EventArgs e)
        {
            if (PopUp.Question("Are you sure?", "BSOD") != MessageBoxResult.OK)
                return;

            m_btnUnmoutAll_Click(sender, e);
            _diskCryptor.ExecuteBSOD(); //Erase all keys in memory and generate BSOD
        }

        private void m_cmbAvailableDriveLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_btnMount.Text = "Mount As: " + m_cmbAvailableDriveLetters.SelectedItem;
            if (m_listDrives.SelectedIndices.Count <= 0)
                return;

            DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;
            if (!string.IsNullOrWhiteSpace(drive.mount_point))
                m_btnMount.Text = "Mount As " + drive.mount_point;
        }

        private void m_treeDrives_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_btnEject.Enabled = false;
            if (m_treeDrives.SelectedNode == null)
                return;

            UsbEject.Library.Device device = m_treeDrives.SelectedNode.Tag as UsbEject.Library.Device;
            if (device == null)
                return;

            m_btnEject.Enabled = device.RemovableDevices.Count > 0;
        }

        private void m_btnEject_Click(object sender, EventArgs e)
        {
            ExecuteClickAction(() =>
            {
                UsbEject.Library.Volume device = m_treeDrives.SelectedNode.Tag as UsbEject.Library.Volume;
                if (device == null || device.Disks.Count == 0)
                    return;

                UnmountAndEjectDevice(device);
            }, sender);
        }

        private void UnmountAndEjectDevice(UsbEject.Library.Volume device)
        {
            List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
            List<string> driveLetters = DriveTools.GetAllDriveLettersForDevice(list, device.Disks[0].FriendlyName);
            foreach (string dr in driveLetters)
            {
                _diskCryptor.ExecuteUnMount(dr);
            }

            Thread.Sleep(800);
            device.Eject(true);

            ReloadDriveData();
        }

        private void ExecuteClickAction(Action action, object sender, [CallerMemberName] string propertyName = null)
        {
            this.Cursor = Cursors.WaitCursor;
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Enabled = false;
            }

            try
            {
                Application.DoEvents();
                action.Invoke();
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message, "Error in: " + propertyName);
            }

            this.Cursor = Cursors.Arrow;
            if (btn != null)
            {
                btn.Enabled = true;
                btn.Focus();
            }
        }

        private void m_mnuShow_Click(object sender, EventArgs e)
        {
            SingleInstanceHelper.GlobalShowWindow(TITLE);
        }

        private void m_sysIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_mnuShow_Click(sender, e);
        }

        private void m_mnuOptionsHideWhenMinimized_Click(object sender, EventArgs e)
        {
            m_mnuOptionsHideWhenMinimized.Checked = !m_mnuOptionsHideWhenMinimized.Checked;
            Settings.Default.HideWhenMinimized = m_mnuOptionsHideWhenMinimized.Checked;
            Settings.Default.Save();
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && Settings.Default.HideWhenMinimized)
            {
                Visible = false; //hide
            }
        }
    }
}
