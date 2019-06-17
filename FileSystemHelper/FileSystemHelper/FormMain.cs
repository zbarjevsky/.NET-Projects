using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemHelper
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_txtOriginal_TextChanged(sender, e);
            m_btnStop.Enabled = false;
        }

        private void m_txtOriginal_TextChanged(object sender, EventArgs e)
        {
            m_txtDestination.Text = m_txtOriginal.Text;

            bool exists = File.Exists(m_txtOriginal.Text);

            m_btnDelete.Enabled = exists;
            m_btnRenameMove.Enabled = exists;
        }

        private void m_btnBrowse_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void m_brtnRenameMove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(m_txtOriginal.Text) || string.IsNullOrWhiteSpace(m_txtDestination.Text))
                return;

            RepeatButtonOperation(m_txtOriginal.Text, "rename/move", () =>
            {
                File.Move(m_txtOriginal.Text, m_txtDestination.Text);
                return File.Exists(m_txtDestination.Text);
            });
        }

        private void m_btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(m_txtOriginal.Text))
                return;

            RepeatButtonOperation(m_txtOriginal.Text, "delete", () => 
            {
                File.Delete(m_txtOriginal.Text);
                return !File.Exists(m_txtOriginal.Text);
            });
        }

        private void RepeatButtonOperation(string fileName, string operationName, Func<bool> op)
        {
            Cursor = Cursors.WaitCursor;
            EnableControls(false);

            const int RETRY = 1000;
            m_progressBar.Maximum = RETRY;
            if (File.Exists(fileName))
            {
                for (int i = 0; i < RETRY && !_stop; i++)
                {
                    try
                    {
                        if (op())
                        {
                            Cursor = Cursors.Arrow;
                            MessageBox.Show(this, operationName + " succeeded!\nFile: "+fileName,
                                operationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            EnableControls(true);
                            return;
                        }
                    }
                    catch (Exception err)
                    {
                        Thread.Sleep(220);
                        Application.DoEvents();
                    }

                    m_progressBar.Value = i;
                }
                Cursor = Cursors.Arrow;
                if(!_stop)
                    MessageBox.Show("Unable to "+operationName+", tries: " + RETRY + "\nFile: "+fileName,
                                    operationName, MessageBoxButtons.OK, MessageBoxIcon.Hand);

                EnableControls(true);
            }

        }

        private void EnableControls(bool bEnable)
        {
            m_progressBar.Value = 0;
            m_btnStop.Enabled = !bEnable;
            _stop = bEnable;

            m_btnBrowse.Enabled = bEnable;
            m_btnDelete.Enabled = bEnable;
            m_btnRenameMove.Enabled = bEnable;
            m_txtOriginal.Enabled = bEnable;
            m_txtDestination.Enabled = bEnable;
        }

        private void FormMain_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
                m_txtOriginal.Text = files[0];
        }

        private bool _stop = false;
        private void m_btnStop_Click(object sender, EventArgs e)
        {
            _stop = true;
        }
    }
}
