using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MZ.WPF.MessageBox;

namespace VhdApiExample {
    public partial class VHD_MainForm : Form
    {
        Medo.IO.VirtualDisk _disk;

        public VHD_MainForm()
        {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._disk != null) { this._disk.Close(); }
        }

        private void mnuCreate_Click(object sender, EventArgs e)
        {
            if (this._disk != null) { this._disk.Close(); }
            using (var frm = new VHD_CreateForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    this._disk = frm.Disk;
                    UpdateData();
                }
            }
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (this._disk != null) { this._disk.Close(); }
                this._disk = new Medo.IO.VirtualDisk(openDialog.FileName);
                this._disk.Open();
                UpdateData();
            }
        }

        private void UpdateData()
        {
            if (this._disk != null)
            {
                textFileName.Text = this._disk.FileName;
                if (this._disk.IsOpen)
                {
                    try
                    {
                        textLocation.Text = this._disk.GetAttachedPath();
                        mnuAttach.Enabled = false;
                        mnuDetach.Enabled = true;
                    }
                    catch (IOException)
                    {
                        textLocation.Text = "";
                        mnuAttach.Enabled = true;
                        mnuDetach.Enabled = false;
                    }
                    mnuCreate.Enabled = false;
                    mnuOpen.Enabled = false;
                    mnuClose.Enabled = true;
                }
                else
                {
                    textLocation.Text = "";
                    mnuCreate.Enabled = false;
                    mnuOpen.Enabled = true;
                    mnuClose.Enabled = false;
                    mnuAttach.Enabled = false;
                    mnuDetach.Enabled = false;
                }
            }
            else
            {
                textFileName.Text = "";
                textLocation.Text = "";
                mnuCreate.Enabled = true;
                mnuOpen.Enabled = true;
                mnuClose.Enabled = false;
                mnuAttach.Enabled = false;
                mnuDetach.Enabled = false;
            }
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            if (this._disk != null)
            {
                this._disk.Close();
                this._disk = null;
                UpdateData();
            }
        }

        private void mnuAttach_Click(object sender, EventArgs e)
        {
            if (this._disk == null) { return; }

            try
            {
                this._disk.Attach(Medo.IO.VirtualDiskAttachOptions.None);
            }
            catch (Exception err)
            {
                PopUp.Error(err.ToString(), "Attach - ERROR");
            }
            UpdateData();
        }

        private void mnuDetach_Click(object sender, EventArgs e)
        {
            if (this._disk == null) { return; }
            try
            {
                this._disk.Detach();
            }
            catch (Exception err)
            {
                PopUp.Error(err.ToString(), "Detach - ERROR");
            }
            UpdateData();
        }

    }
}
