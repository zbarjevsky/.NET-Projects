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
using System.Windows;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace DiskCryptorHelper.VHD
{
    public partial class VHD_MountUnMountUserControl : UserControl
    {
        private DiskCryptor _diskCryptor;
        private VirtualDiskService _virtualDiskService;
        private ToolStripMenuItem m_mnuFileAttachVHD;
        private RecentFilesList _recentFiles = new RecentFilesList();

        public Func<string> GetPasswordFunc = () => "";
        public Action<DiskCryptor.DriveInfo> UnmountDiscryptorDrive = (drive) => { };
        public Action<int> ReloadDriveData = (delay) => { };

        public VHD_MountUnMountUserControl()
        {
            InitializeComponent();

            _virtualDiskService = new VirtualDiskService();
        }

        private void VHD_MountUnMountUserControl_Load(object sender, EventArgs e)
        {

        }

        public void Initialize(DiskCryptor diskCryptor, ToolStripMenuItem mnuFileAttachVHD)
        {
            _diskCryptor = diskCryptor;
            m_mnuFileAttachVHD = mnuFileAttachVHD;

            _recentFiles.Update(m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);
        }

        public void BrowseForVHD()
        {
            m_btnOpenVHD_Click(this, null);
        }

        public void OpenRecentVHD(string vhdFileName)
        {
            _recentFiles.AddRecent(vhdFileName, m_mnuFileAttachVHD.DropDown, m_cmbVHD_FileName);
        }

        private void m_btnOpenVHD_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog()
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

        private Medo.IO.VirtualDisk _virtualDisk;
        private List<DiskCryptor.DriveInfo> _cachedDriveInfo;
        private string _selectedDriveLetterForMount;
        private void m_btnAttachVHDandMount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GetPasswordFunc()))
            {
                PopUp.Error("Password is empty", "Password");
                return;
            }

            FileInfo fi = new FileInfo(m_cmbVHD_FileName.Text);
            if (!fi.Exists)
            {
                if (PopUp.Question("File not found: " + fi.FullName + "\nRemove from Recent Files List?", "Mount VHD - ERROR",
                    MessageBoxImage.Asterisk, TextAlignment.Center, PopUp.PopUpButtonsType.NoYes) == PopUp.PopUpResult.Yes)
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
                try { _virtualDisk.Detach(); } catch (Exception err) { Debug.WriteLine("Cannot detach: " + err.Message); }

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

                _diskCryptor.ExecuteMount(driveInfo, _selectedDriveLetterForMount, GetPasswordFunc());

                ReloadDriveData(1000);
            }
        }

        private string _selectedDriveLetter = "";
        private void m_cmbAvailableDriveLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedDriveLetter = m_cmbAvailableDriveLetters.SelectedItem.ToString();
            m_btnAttachVHD.Text = "Attach && Mount As: " + _selectedDriveLetter;
        }

        private void m_btnDetachAll_Click(object sender, EventArgs e)
        {
            ExecuteClickAction(() => UnmountAndDetachAll(), sender);
        }

        private void m_btnUnmountAndDetach_Click(object sender, EventArgs e)
        {
            string vhdFileName = m_cmbVHD_FileName.Text;
            if (!File.Exists(vhdFileName))
            {
                PopUp.MessageBox("File does not exist: " + vhdFileName, "Detach Error", MessageBoxImage.Asterisk);
                return;
            }

            ExecuteClickAction(() => UnmountAndDetach(vhdFileName), sender);
        }

        public void UnmountAndDetachAll()
        {
            Log.WriteLine("UnmountAndDetachAll(1)");
            List<string> paths = _virtualDiskService.GetVirtualDisksImagePaths();

            for (int i = 0; i < paths.Count; i++)
            {
                Log.WriteLine("UnmountAndDetachAll({0}:{1})", i, paths[i]);
                UnmountAndDetach(paths[i]);
            }
        }

        private void UnmountAndDetach(string vhdFileName)
        {
            FileInfo fi = new FileInfo(vhdFileName);
            if (!fi.Exists)
                return;

            Log.WriteLine("UnmountAndDetach({0})", vhdFileName);
            fi.IsReadOnly = false; //unlock file to enable load

            try
            {
                if (_virtualDisk != null) // && _virtualDisk.FileName.CompareTo(vhdFileName) != 0)
                {
                    _virtualDisk.Close();
                    _virtualDisk = null;
                }

                if (_virtualDisk == null || !_virtualDisk.IsOpen)
                {
                    _virtualDisk = new Medo.IO.VirtualDisk(vhdFileName);
                    _virtualDisk.Open();
                }

                string diskPath = _virtualDisk.GetAttachedPath();
                if (!string.IsNullOrWhiteSpace(diskPath)) //if device is attached
                {
                    DiskCryptor.DriveInfo drive = FindDriveInfo(diskPath);
                    if (drive != null)
                    {
                        try { UnmountDiscryptorDrive(drive); }
                        catch (Exception err)
                        {
                            Log.WriteLine("UnmountDiscryptorDrive({0}) - Error: {1}", diskPath, err);
                        }
                    }

                    _virtualDisk.Detach();
                }
            }
            catch (Exception err)
            {
                Log.WriteLine("UnmountAndDetach({0}): {1}", vhdFileName, err.ToString());
                throw;
            }
            finally
            {
                if (_virtualDisk != null)
                {
                    _virtualDisk.Close();
                    _virtualDisk = null;
                }

                fi.IsReadOnly = true; //lock file to improve security
            }
        }

        private DiskCryptor.DriveInfo FindDriveInfo(string attachedPath)
        {
            List<UsbEject.Library.Device> list = DriveTools.GetUsbDriveList();
            List<UsbEject.Library.Volume> removable_volumes = DriveTools.GetRemovableDriveList(list);

            const string PHYSICALDRIVE = "PHYSICALDRIVE";
            int pos = attachedPath.IndexOf(PHYSICALDRIVE) + PHYSICALDRIVE.Length;
            int diskNumber = int.Parse(attachedPath.Substring(pos));

            UsbEject.Library.Volume vol = null;
            foreach (UsbEject.Library.Volume device in list)
            {
                if (device.Disks[0].DiskNumber == diskNumber)
                {
                    vol = device;
                    break;
                }
            }

            if (vol == null)
                return null;

            foreach (DiskCryptor.DriveInfo drive in _diskCryptor.DriveList)
            {
                if (drive != null && !string.IsNullOrWhiteSpace(drive.DriveLetter) && drive.DriveLetter[0] == vol.LogicalDrive[0])
                    return drive;
            }
            return null;
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

        public void ReloadAvailableDriveLetters(Form owner)
        {
            ReloadAvailableDriveLetters(m_cmbAvailableDriveLetters, owner);
        }

        public static void ReloadAvailableDriveLetters(ComboBox cmbAvailableDriveLetters, Form owner)
        {
            List<char> driveLetters = new List<char>(26); // Allocate space for alphabet
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
                cmbAvailableDriveLetters.Items.Clear();

                foreach (char drive in driveLetters)
                {
                    cmbAvailableDriveLetters.Items.Add(drive + ":"); // add unused drive letters to the combo box
                }

                //default select T or M
                int idx = cmbAvailableDriveLetters.Items.IndexOf("T:");
                if (idx < 0)
                    idx = cmbAvailableDriveLetters.Items.IndexOf("M:");
                if (idx >= 0)
                    cmbAvailableDriveLetters.SelectedIndex = idx;
                else
                    cmbAvailableDriveLetters.SelectedIndex = 0;

            }, owner);
        }
    }
}
