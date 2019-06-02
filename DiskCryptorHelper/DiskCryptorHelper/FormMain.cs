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
using VhdApiExample;
using System.Reflection;
using MZ.WPF.MessageBox;

namespace DiskCryptorHelper
{
    public partial class FormMain : Form
    {
        public const string TITLE = "DiskCryptor Commander";
        private string _selectedDriveLetter = "";
        private RecentFilesList _recentFiles = new RecentFilesList();
        private DiskCryptor _diskCryptor = new DiskCryptor();

        public FormMain(string [] cmd_line = null)
        {
            InitializeComponent();

            Text = TITLE;

            ProcessCommanLine(cmd_line);

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

            _recentFiles.Update(m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || 
                e.CloseReason == CloseReason.TaskManagerClosing)
            {
                Log.WriteLine("FormMain_FormClosing: " + e.CloseReason);
                _diskCryptor.ExecuteUnMountAll();
            }
            else if(e.CloseReason == CloseReason.UserClosing) //user clicked close button
            {
                System.Windows.MessageBoxResult res = PopUp.MessageBox(
                    "Cancel(C), Hide(H) or Exit(X)?", "Exit Application",
                    System.Windows.MessageBoxImage.Question,
                    System.Windows.TextAlignment.Center,
                    System.Windows.MessageBoxButton.YesNoCancel,
                    "E_xit", "_Hide");

                if (res != System.Windows.MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                    if (res == System.Windows.MessageBoxResult.No)
                        this.Visible = false; //hide
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

            _recentFiles.AddRecent(vhd, m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);
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
            List<UsbEject.Library.Volume> removable_volumes = DriveTools.GetRemovableDriveList(list);

            Utils.ExecuteOnUIThread(() =>
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

        private bool DetachVHDDrive(string driveLetter)
        {
            List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
            List<UsbEject.Library.Volume> removable_volumes = DriveTools.GetRemovableDriveList(list);

            foreach (UsbEject.Library.Volume vol in removable_volumes)
            {
                if(vol.LogicalDrive == driveLetter)
                {

                    return true;
                }
            }

            return false;
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

            var res = NetResourceEnumerator.WNetResource(); //find disconnected/remembered network drives
            foreach (string key in res.Keys)
                driveLetters.Remove(key[0]);

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
            if (m_listDrives.SelectedIndices.Count <= 0)
                return;

            m_cmbAvailableDriveLetters_SelectedIndexChanged(sender, e);
            DiskCryptor.DriveInfo drive = m_listDrives.SelectedItems[0].Tag as DiskCryptor.DriveInfo;

            m_lblSelected.Text = "Selected: " + drive.MountPoint + ", MP: " + drive.DriveLetter;
            m_btnMount.Enabled = drive.MountPoint.StartsWith("pt") && drive.status.StartsWith("unmounted");
            m_btnUnmount.Enabled = drive.MountPoint.StartsWith("pt") && drive.status.StartsWith("mounted");
            m_btnUnmount.Text = "UnMount: " + drive.MountPoint;
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
            if (PopUp.Question("Are you sure?", "BSOD") != MessageBoxResult.OK)
                return;

            m_btnUnmoutAll_Click(sender, e);
            _diskCryptor.ExecuteBSOD(); //Erase all keys in memory and generate BSOD
        }

        private void m_cmbAvailableDriveLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedDriveLetter = m_cmbAvailableDriveLetters.SelectedItem.ToString();

            m_btnMount.Text = "Mount As: " + _selectedDriveLetter;
            m_btnAttachVHD.Text = "Attach && Mount As: " + _selectedDriveLetter;
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
            using (var frm = new VHD_CreateForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    //this._disk = frm.Disk;
                    //UpdateData();
                }
            }
        }

        private void m_mnuFileOpenVHD_File_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog()
            {
                RestoreDirectory = true,
                Multiselect = false,
                Filter = "Virtual Disks(*.vhd)|*.vhd|All files (*.*)|*.*"
            };

            if (File.Exists(m_cmbVHD_FileName.Text))
            {
                open.InitialDirectory = Path.GetDirectoryName(m_cmbVHD_FileName.Text);
                open.FileName = m_cmbVHD_FileName.Text;
            }

            if (open.ShowDialog(this) != DialogResult.OK)
                return;

            _recentFiles.AddRecent(open.FileName, m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);
        }

        private void m_btnOpenVHD_Click(object sender, EventArgs e)
        {
            m_mnuFileOpenVHD_File_Click(sender, e);
        }

        private void OpenRecentVHD_Click(object sender, EventArgs e)
        {
            string fileName = (sender as ToolStripMenuItem).Text;
            _recentFiles.AddRecent(fileName, m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);
        }

        private Medo.IO.VirtualDisk _virtualDisk;
        private List<DiskCryptor.DriveInfo> _cachedDriveInfo;
        private string _selectedDriveLetterForMount;
        private void m_btnAttachVHDandMount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(m_txtPwd.Text))
            {
                PopUp.Error("Password is empty", "Password");
                return;
            }

            FileInfo fi = new FileInfo(m_cmbVHD_FileName.Text);
            if (!fi.Exists)
            {
                if (PopUp.Question("File not found: " + fi.FullName + "\nRemove from Recent Files List?", "Mount VHD - ERROR", 
                    MessageBoxImage.Asterisk, TextAlignment.Center, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _recentFiles.RemoveFromList(m_cmbVHD_FileName.Text, m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);
                }
                return;
            }

            ExecuteClickAction(() =>
            {
                _recentFiles.AddRecent(m_cmbVHD_FileName.Text, m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);

                fi.IsReadOnly = false; //unlock file to enable load

                _cachedDriveInfo = new List<DiskCryptor.DriveInfo>(_diskCryptor.DriveList);

                if (_virtualDisk != null)
                {
                    _virtualDisk.Close();
                }

                _virtualDisk = new Medo.IO.VirtualDisk(fi.FullName);
                _virtualDisk.Open();
                try { _virtualDisk.Detach(); } catch (Exception err) { Debug.WriteLine("Cannot detach: "+err.Message); }

                _selectedDriveLetterForMount = _selectedDriveLetter;
                _diskCryptor.OnDisksAdded += OnDiskAddedMountDiskCryptorDisk; //mount DiskCryptor disk

                Medo.IO.VirtualDiskAttachOptions options = Medo.IO.VirtualDiskAttachOptions.NoDriveLetter;
                if (m_chkPermanent.Checked)
                    options |= Medo.IO.VirtualDiskAttachOptions.PermanentLifetime;
                _virtualDisk.Attach(options);

                fi.IsReadOnly = true; //lock file to improve security
            }, sender);
        }

        private void OnDiskAddedMountDiskCryptorDisk(DiskCryptor.DriveInfo driveInfo)
        {
            if (_cachedDriveInfo == null)
                return;

            //find newly added drive
            var found = _cachedDriveInfo.FirstOrDefault(d => d.ToString() == driveInfo.ToString());
            if (found == null)
            {
                _diskCryptor.OnDisksAdded -= OnDiskAddedMountDiskCryptorDisk;
                _cachedDriveInfo = null;

                Debug.WriteLine("On Disk Added: " + driveInfo.Description());

                _diskCryptor.ExecuteMount(driveInfo, _selectedDriveLetterForMount, m_txtPwd.Text);

                Utils.ExecuteOnUIThread(() => { ReloadDriveData(1000); } , this);
            }
        }

        private void m_btnUnmountAndDetach_Click(object sender, EventArgs e)
        {
            string vhdFileName = m_cmbVHD_FileName.Text;
            if (!File.Exists(vhdFileName))
            {
                PopUp.MessageBox("File does not exist: "+ vhdFileName, "Detach Error", MessageBoxImage.Asterisk);
                return;
            }

            ExecuteClickAction(() =>
            {
                if (_virtualDisk != null && _virtualDisk.FileName.CompareTo(vhdFileName) != 0)
                { 
                    _virtualDisk.Close();
                    _virtualDisk = null;
                }

                if(_virtualDisk == null || !_virtualDisk.IsOpen)
                {
                    _virtualDisk = new Medo.IO.VirtualDisk(vhdFileName);
                    _virtualDisk.Open();
                }

                string path = _virtualDisk.GetAttachedPath();
                DiskCryptor.DriveInfo drive = FindDriveInfo(path);
                if(drive != null)
                    Unmount(drive);

                _virtualDisk.Detach();
                _virtualDisk.Close();
                _virtualDisk = null;
            }, sender);
        }

        private DiskCryptor.DriveInfo FindDriveInfo(string attachedPath)
        {
            List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
            List<UsbEject.Library.Volume> removable_volumes = DriveTools.GetRemovableDriveList(list);
            DriveInfo[] driveInfos = System.IO.DriveInfo.GetDrives();

            const string PHYSICALDRIVE = "PHYSICALDRIVE";
            int pos = attachedPath.IndexOf(PHYSICALDRIVE) + PHYSICALDRIVE.Length;
            int diskNumber = int.Parse(attachedPath.Substring(pos));

            UsbEject.Library.Volume vol = null;
            foreach (UsbEject.Library.Volume device in removable_volumes)
            {
                if (device.Disks[0].DiskNumber == diskNumber)
                {
                    vol = device;
                    break;
                }
            }

            if (vol == null)
                return null;

            foreach (ListViewItem item in m_listDrives.Items)
            {
                DiskCryptor.DriveInfo drive = item.Tag as DiskCryptor.DriveInfo;
                if (drive != null && !string.IsNullOrWhiteSpace(drive.DriveLetter) && drive.DriveLetter[0] == vol.LogicalDrive[0])
                    return drive;
            }
            return null;
        }

        private void m_mnuOpenDiskCryptor_Click(object sender, EventArgs e)
        {
            Process.Start(DiskCryptor.DiskCryptorPath);
        }
    }
}
