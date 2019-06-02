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

namespace DiskCryptorHelper.VHD
{
    public partial class VHD_MountUnMountUserControl : UserControl
    {
        public VHD_MountUnMountUserControl()
        {
            InitializeComponent();
        }

        private void VHD_MountUnMountUserControl_Load(object sender, EventArgs e)
        {

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

                _diskCryptor.ExecuteMount(driveInfo, _selectedDriveLetterForMount, m_txtPwd.Text);

                Utils.ExecuteOnUIThread(() => { ReloadDriveData(1000); }, this);
            }
        }

        private void m_cmbAvailableDriveLetters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void m_btnUnmountAndDetach_Click(object sender, EventArgs e)
        {
            string vhdFileName = m_cmbVHD_FileName.Text;
            if (!File.Exists(vhdFileName))
            {
                PopUp.MessageBox("File does not exist: " + vhdFileName, "Detach Error", MessageBoxImage.Asterisk);
                return;
            }

            ExecuteClickAction(() =>
            {
                if (_virtualDisk != null && _virtualDisk.FileName.CompareTo(vhdFileName) != 0)
                {
                    _virtualDisk.Close();
                    _virtualDisk = null;
                }

                if (_virtualDisk == null || !_virtualDisk.IsOpen)
                {
                    _virtualDisk = new Medo.IO.VirtualDisk(vhdFileName);
                    _virtualDisk.Open();
                }

                string path = _virtualDisk.GetAttachedPath();
                DiskCryptor.DriveInfo drive = FindDriveInfo(path);
                if (drive != null)
                    Unmount(drive);

                _virtualDisk.Detach();
                _virtualDisk.Close();
                _virtualDisk = null;
            }, sender);
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
    }
}
