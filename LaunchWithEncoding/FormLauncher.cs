using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace LaunchWithEncoding
{
    public partial class FormLauncher : Form
    {
        public FormLauncher()
        {
            InitializeComponent();
        }

        private void FormLauncher_Load(object sender, EventArgs e)
        {
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                int idx = m_cmbEncoding.Items.Add(new Language(ei));
            }//end foreach
        }

        private void m_btnBrowse_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != m_openFileDialog.ShowDialog())
                return;
            m_txtProgram.Text = m_openFileDialog.FileName;
        }

        private void m_btnLaunch_Click(object sender, EventArgs e)
        {
            Language lan = m_cmbEncoding.SelectedItem as Language;
            Encoding en = Encoding.GetEncoding(lan.CodePage);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("he");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("he");
            ProcessStartInfo startInfo = new ProcessStartInfo(m_txtProgram.Text);
            //startInfo.StandardOutputEncoding = en;
            Process.Start(startInfo);
        }

        class Language
        {
            public int CodePage;
            public string Name, DisplayName;

            public Language(EncodingInfo ei)
            {
                CodePage = ei.CodePage;
                Name = ei.Name;
                DisplayName = ei.DisplayName;
            }
            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}
