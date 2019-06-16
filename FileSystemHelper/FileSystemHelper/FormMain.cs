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

            const int RETRY = 1000;
            if (File.Exists(fileName))
            {
                for (int i = 0; i < RETRY; i++)
                {
                    try
                    {
                        if (op())
                        {
                            Cursor = Cursors.Arrow;
                            MessageBox.Show(operationName + " succeeded!");
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(220);
                        Application.DoEvents();
                    }
                }
                Cursor = Cursors.Arrow;
                MessageBox.Show("Unable to "+operationName+", tries: " + RETRY);
            }

        }
    }
}
