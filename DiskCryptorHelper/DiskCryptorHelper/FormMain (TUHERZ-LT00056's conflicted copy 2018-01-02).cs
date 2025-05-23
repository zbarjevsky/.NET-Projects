﻿using Microsoft.Win32;
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
using System.Windows.Forms;

namespace WinCryptMount
{
    public partial class FormMain : Form
    {
        private DiskCryptor _diskCryptor = new DiskCryptor();

        public FormMain()
        {
            InitializeComponent();

            _diskCryptor.OnDataReceived = () =>
            {
                Utils.ExecuteOnUIThread<int>(() =>
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
                    m_listDrives_SelectedIndexChanged(this, null);
                    return 0;
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
                            Debug.WriteLine("WM_DEVICECHANGE");
                            ReloadDriveData(0);
                        }
                    }
                    else
                    {
                        //Poll drives
                        Debug.WriteLine("DBT_DEVNODES_CHANGED");
                        ReloadDriveData(0);
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ReloadDriveData(0);
            m_listDrives_SelectedIndexChanged(sender, e);
            m_sysIcon.Text = "XaXa";

            m_mnuFile.DropDown = m_sysIconMenu;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.WindowsShutDown || 
                e.CloseReason == CloseReason.TaskManagerClosing)
            {
                _diskCryptor.ExecuteUnMountAll();

                File.AppendAllText("C:\\Temp\\Log11.txt", DateTime.Now.ToString() + " - Close for: " + e.CloseReason + "\n");
            }
            else if(e.CloseReason == CloseReason.UserClosing) //user clicked close button
            {
                DialogResult res = MessageBox.Show(this, "Exit(Yes) or Hide(No)?", "Close",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (res != DialogResult.Yes)
                {
                    e.Cancel = true;
                    if(res == DialogResult.No)
                        this.Visible = false; //hide
                }
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

            File.AppendAllText("C:\\Temp\\Log11.txt", DateTime.Now.ToString() + " - Session end: " + e.Reason + "\n");
        }

        private void m_btnReload_Click(object sender, EventArgs e)
        {
            ExecuteClickAction(() =>
            {
                ReloadDriveData(0);
            }, sender);
        }

        private void ReloadDriveData(int millisecodsTimeout = 800)
        {
            Thread.Sleep(millisecodsTimeout);

            ReloadAvailableDriveLetters();
            ReloadDriveList();
            _diskCryptor.ExecuteEnum();
        }

        private void ReloadDriveList()
        {
            m_treeDrives.Nodes.Clear();
            m_mnuEject.DropDownItems.Clear();

            List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
            List<UsbEject.Library.Volume> removable_volumes = new List<UsbEject.Library.Volume>();

            foreach (UsbEject.Library.Device drive in list)
            {
                if (drive.RemovableDevices.Count == 0)
                    continue;

                UsbEject.Library.Volume vol = drive as UsbEject.Library.Volume;
                if (vol == null || vol.Disks.Count == 0)
                    continue;

                if (removable_volumes.FirstOrDefault(v => v.Disks[0].FriendlyName == vol.Disks[0].FriendlyName) != null)
                    continue;

                removable_volumes.Add(vol);
                TreeNode node = m_treeDrives.Nodes.Add(vol.Disks[0].FriendlyName);
                node.Tag = vol;

                List<string> driveLetters = DriveTools.GetUsbDriveList(vol);
                string mnuText = "";
                foreach (string dr in driveLetters)
                {
                    mnuText += (dr) + ", ";
                    TreeNode sub = node.Nodes.Add(GetDriveDescription(dr));
                    sub.Tag = vol;
                }
                mnuText += "[" + vol.Disks[0].FriendlyName + "]";

                ToolStripItem mnu = new ToolStripMenuItem(mnuText);
                mnu.Tag = vol;
                mnu.Click += mnuEjectDisk_Click; ;
                m_mnuEject.DropDownItems.Add(mnu);
            }

            m_mnuEject.Enabled = m_mnuEject.DropDownItems.Count > 0;
            m_treeDrives.ExpandAll();
            if (m_treeDrives.Nodes.Count > 0)
                m_treeDrives.SelectedNode = m_treeDrives.Nodes[0];
        }

        private string GetDriveDescription(string driveLetter)
        {
            DriveInfo[] list = System.IO.DriveInfo.GetDrives();
            foreach (DriveInfo ifo in list)
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
            m_cmbAvailableDriveLetters.Items.Clear();

            ArrayList driveLetters = new ArrayList(26); // Allocate space for alphabet
            for (int i = 65; i < 91; i++) // increment from ASCII values for A-Z
            {
                driveLetters.Add(Convert.ToChar(i)); // Add uppercase letters to possible drive letters
            }

            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                driveLetters.Remove(drive[0]); // removed used drive letters from possible drive letters
            }

            foreach (char drive in driveLetters)
            {
                m_cmbAvailableDriveLetters.Items.Add(drive+":"); // add unused drive letters to the combo box
            }

            //default select T or M
            int idx = m_cmbAvailableDriveLetters.Items.IndexOf("T:");
            if (idx < 0)
                idx = m_cmbAvailableDriveLetters.Items.IndexOf("M:");
            if (idx >= 0)
                m_cmbAvailableDriveLetters.SelectedIndex = idx;
            else
                m_cmbAvailableDriveLetters.SelectedIndex = 0;
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

            DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;

            m_lblSelected.Text = "Selected: " + drive.volume + ", MP: " + drive.mount_point;
            m_btnMount.Enabled = drive.volume.StartsWith("pt") && drive.status.StartsWith("unmounted");
            m_btnUnmount.Enabled = drive.volume.StartsWith("pt") && drive.status.StartsWith("mounted");
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
            if (MessageBox.Show(this, "Are you sure?", "BSOD", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            m_btnUnmoutAll_Click(sender, e);
            _diskCryptor.ExecuteBSOD(); //Erase all keys in memory and generate BSOD
        }

        private void m_cmbAvailableDriveLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_btnMount.Text = "Mount As: " + m_cmbAvailableDriveLetters.SelectedItem.ToString();
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
                UsbEject.Library.Device device = m_treeDrives.SelectedNode.Tag as UsbEject.Library.Device;
                if (device == null || device.RemovableDevices.Count == 0)
                    return;

                UnmountAndEjectDevice(device);
            }, sender);
        }

        private void UnmountAndEjectDevice(UsbEject.Library.Device device)
        {
            List<string> driveLetters = DriveTools.GetUsbDriveList(device);
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
            this.Visible = true;
        }

        private void m_sysIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_mnuShow_Click(sender, e);
        }
    }
}
