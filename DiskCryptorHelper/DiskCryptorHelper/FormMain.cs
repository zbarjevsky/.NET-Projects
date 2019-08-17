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
using DiskCryptorHelper.Utils;
using VhdApiExample;
using System.Reflection;
using MZ.WPF.MessageBox;
using DiskCryptorHelper.VHD;

namespace DiskCryptorHelper
{
    public partial class FormMain : Form
    {
        public const string TITLE = "DiskCryptor Commander";
        private string _selectedDriveLetter = "";
        private DiskCryptor _diskCryptor = new DiskCryptor();

        public FormMain(string [] cmd_line = null)
        {
            InitializeComponent();

            Text = TITLE;

            ProcessCommanLine(cmd_line);

            _diskCryptor.OnDataReceived = () =>
            {
                CommonUtils.ExecuteOnUIThread(() =>
                {
                    m_txtLog.Text = _diskCryptor.Log.ToString();
                    m_listDrives.Items.Clear();
                    //cache list for this operation
                    DiskCryptor.DriveInfo [] list = _diskCryptor.DriveList.ToArray();
                    foreach (DiskCryptor.DriveInfo info in list)
                    {
                        if (info.IsValidInfo)
                        {
                            ListViewItem item = m_listDrives.Items.Add(info.MountPoint);
                            item.Tag = info;

                            item.SubItems.Add(info.DriveLetter);
                            item.SubItems.Add(info.size);
                            item.SubItems.Add(info.status);
                        }
                    }
                    SmartSelectFirstAvailableDrive();
                }, this);
            };
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
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
            Log.WriteLine("FormMain_Load");

            _diskCryptor.ExecuteVersion();
            ReloadDriveData(0);
            m_listDrives_SelectedIndexChanged(sender, e);
            m_sysIcon.Text = TITLE;
            
            m_mnuFile.DropDown = m_sysIconMenu;
            m_mnuOptionsHideWhenMinimized.Checked = Settings.Default.HideWhenMinimized;

            m_txtPwd_TextChanged(sender, e); //check for error

            m_VHD_MountUnMountUserControl.Initialize(_diskCryptor, m_mnuFileAttachVHD);
            m_VHD_MountUnMountUserControl.GetPasswordFunc = () => m_txtPwd.Text;
            m_VHD_MountUnMountUserControl.UnmountDiscryptorDrive = (driveInfo) => Unmount(driveInfo);
            m_VHD_MountUnMountUserControl.ReloadDriveData = (delay) => ReloadDriveData(delay);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || 
                e.CloseReason == CloseReason.TaskManagerClosing)
            {
                Log.WriteLine("FormMain_FormClosing: " + e.CloseReason);
                try { m_VHD_MountUnMountUserControl.UnmountAndDetachAll(); }
                catch (Exception err) { Log.WriteLine("m_VHD_MountUnMountUserControl.UnmountAndDetachAll - Exception: {0}", err); }
                //_diskCryptor.ExecuteUnMountAll();
            }
            else if(e.CloseReason == CloseReason.UserClosing) //user clicked close button
            {
                PopUp.PopUpResult res = PopUp.MessageBox(
                    "Cancel(C), Exit(X) or Hide(H)?", "Exit Application",
                    System.Windows.MessageBoxImage.Question,
                    System.Windows.TextAlignment.Center,
                    new PopUp.PopUpButtons("_Cancel", "E_xit", "_Hide"));

                if (res != PopUp.PopUpResult.Btn2)
                {
                    e.Cancel = true;
                    if (res == PopUp.PopUpResult.Btn3)
                    {
                        this.Visible = false; //hide
                        this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                        this.ShowInTaskbar = false; //hide from ALT+TAB
                    }
                }
            }
            else if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                Log.WriteLine("FormMain_FormClosing: " + e.CloseReason);
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_sysIcon.Visible = false;
        }

        private void ProcessCommanLine(string[] cmd_line)
        {
            if (cmd_line == null || cmd_line.Length == 0)
                return;

            PopUp.MessageBox(cmd_line[0], "DiskCryptorHelper::ProcessCommanLine");

            string vhd = cmd_line[0];
            string exe = Path.GetFileName(Assembly.GetExecutingAssembly().Location).ToLower();
            string file = Path.GetFileName(vhd).ToLower();
            if (exe == file)
            {
                if (cmd_line.Length == 1) //only this exe file in command line
                    return;
                vhd = cmd_line[1];
            }

            if (!File.Exists(vhd))
                return;

            m_VHD_MountUnMountUserControl.OpenRecentVHD(vhd);
        }

        private void m_btnReload_Click(object sender, EventArgs e)
        {
            ReloadDriveData(0);
        }

        private void ReloadDriveData(int millisecodsTimeout = 800)
        {
            CommonUtils.ExecuteOnUIThread(() => m_btnReload.Enabled = false, this);

            Task.Factory.StartNew(() =>
            {
                lock (this)
                {
                    Thread.Sleep(millisecodsTimeout);

                    ReloadDriveList();
                    VHD_MountUnMountUserControl.ReloadAvailableDriveLetters(m_cmbAvailableDriveLetters, this);
                    m_VHD_MountUnMountUserControl.ReloadAvailableDriveLetters(this);
                    _diskCryptor.ExecuteEnum();
                }

                CommonUtils.ExecuteOnUIThread(() => m_btnReload.Enabled = true, this);
            });
        }

        private void ReloadDriveList()
        {
            List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
            List<UsbEject.Library.Volume> removable_volumes = DriveTools.GetRemovableDriveList(list);

            CommonUtils.ExecuteOnUIThread(() =>
            {
                m_treeDrives.Nodes.Clear();
                m_mnuEject.DropDownItems.Clear();
                m_btnEject.Enabled = false;

                hideDriveLetterControl1.ReloadList();

                foreach (UsbEject.Library.Volume vol in removable_volumes)
                {
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

        //private bool DetachVHDDrive(string driveLetter)
        //{
        //    List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
        //    List<UsbEject.Library.Volume> removable_volumes = DriveTools.GetRemovableDriveList(list);

        //    foreach (UsbEject.Library.Volume vol in removable_volumes)
        //    {
        //        if(vol.LogicalDrive == driveLetter)
        //        {

        //            return true;
        //        }
        //    }

        //    return false;
        //}

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
                    PopUp.MessageBox("Password is empty", "Error");
                    return;
                }

                DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;

                _diskCryptor.ExecuteMount(drive, _selectedDriveLetter, m_txtPwd.Text);

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

        private void m_btnBrowseDisk_Click(object sender, EventArgs e)
        {
            DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;
            if (string.IsNullOrWhiteSpace(drive.DriveLetter))
                return;

            Process.Start(drive.DriveLetter);
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
            m_btnUnmount.Text = "UnMount";
            m_btnBrowseDisk.Enabled = false;
            if (m_listDrives.SelectedIndices.Count <= 0)
                return;

            m_cmbAvailableDriveLetters_SelectedIndexChanged(sender, e);
            DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;

            m_lblSelected.Text = "Selected: " + drive.MountPoint + ", MP: " + drive.DriveLetter;
            m_btnMount.Enabled = drive.MountPoint.StartsWith("pt") && drive.status.StartsWith("unmounted");
            m_btnUnmount.Enabled = drive.MountPoint.StartsWith("pt") && drive.status.StartsWith("mounted");
            m_btnUnmount.Text = "UnMount: " + drive.MountPoint;
            m_btnBrowseDisk.Enabled = string.IsNullOrWhiteSpace(drive.DriveLetter) ? false : true;
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
                if (string.IsNullOrWhiteSpace(drive.DriveLetter) && drive.status.Equals("unmounted"))
                    m_listDrives.Items[i].Selected = true;
            }
        }

        private void m_mnuExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void m_mnuUnmountAll_Click(object sender, EventArgs e)
        {
            m_btnUnmoutAll_Click(sender, e);
        }

        private void m_btnUnmountAllandBSOD_Click(object sender, EventArgs e)
        {
            if (PopUp.Question("Are you sure?", "BSOD") != PopUp.PopUpResult.OK)
                return;

            m_btnUnmoutAll_Click(sender, e);
            _diskCryptor.ExecuteBSOD(); //Erase all keys in memory and generate BSOD
        }

        private void m_cmbAvailableDriveLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedDriveLetter = m_cmbAvailableDriveLetters.SelectedItem.ToString();

            m_btnMount.Text = "Mount As: " + _selectedDriveLetter;
            if (m_listDrives.SelectedIndices.Count <= 0)
                return;

            DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;
            if (!string.IsNullOrWhiteSpace(drive.DriveLetter))
                m_btnMount.Text = "Mount As " + drive.DriveLetter;
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
                System.Windows.Forms.Application.DoEvents();
                action.Invoke();
            }
            catch (Exception err)
            {
                PopUp.MessageBox(err.Message, "Error in: " + propertyName);
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
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ShowInTaskbar = true;
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

        private void m_mnuOptionsVHD_Click(object sender, EventArgs e)
        {
            VHD_MainForm frm = new VHD_MainForm();
            frm.ShowDialog(this);
        }

        private void m_mnuFileCreateVHD_Click(object sender, EventArgs e)
        {
            using (var frm = new VHD_MainForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void m_mnuFileOpenVHD_File_Click(object sender, EventArgs e)
        {
            m_VHD_MountUnMountUserControl.BrowseForVHD();
        }

        private void m_mnuOpenDiskCryptor_Click(object sender, EventArgs e)
        {
            Process.Start(DiskCryptor.DiskCryptorAppPath);
        }

        private void m_txtPwd_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetIconAlignment(m_txtPwd, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconPadding(m_txtPwd, 2);

            if (string.IsNullOrWhiteSpace(m_txtPwd.Text))
                errorProvider1.SetError(m_txtPwd, "Password is Empty");
            else
                errorProvider1.SetError(m_txtPwd, "");
        }
    }
}
