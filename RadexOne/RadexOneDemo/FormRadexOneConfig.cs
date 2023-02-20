using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MkZ.RadexOneLib;

namespace RadexOneDemo
{
    public partial class FormRadexOneConfig : Form
    {
        private readonly RadexOneConnection _radexDevice;

        public bool Sound { get { return m_chkSnd.Checked; } set { m_chkSnd.Checked = value; } }
        public bool Vibrate { get { return m_chkVib.Checked; } set { m_chkVib.Checked = value; } }
        public double Threshold { get { return (double)m_numLimit.Value; } set { m_numLimit.Value = (decimal)value; } }
        public string Dose { set { m_lblDose.Text = value; } get { return m_lblDose.Text; } }
        public string SerialNumber { get { return m_lblSN.Text; } set { m_lblSN.Text = value; } }

        public static DialogResult ShowConfig(Form parent, RadexOneConnection radexDevice, RadexOneDeviceInfo radexConfig)
        {
            FormRadexOneConfig frm = new FormRadexOneConfig(radexDevice, radexConfig);
            return frm.ShowDialog(parent);
        }

        public FormRadexOneConfig(RadexOneConnection radexDevice, RadexOneDeviceInfo radexConfig)
        {
            _radexDevice = radexDevice;

            InitializeComponent();

            SerialNumber = radexConfig.SerialNumber.ToString();

            Sound = radexConfig.Sound;
            Vibrate = radexConfig.Vibrate;
            Threshold = radexConfig.Threshold;
            Dose = radexConfig.DoseToString();
        }

        private void FormRadexOneConfig_Load(object sender, EventArgs e)
        {
            m_btnOk.Enabled = false; //enable when changed something
        }

        private void m_btnResetDose_Click(object sender, EventArgs e)
        {
            _radexDevice.RequestResetDose();
            Dose = "...";
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _radexDevice.SendRequestSetSettings(m_chkSnd.Checked, m_chkVib.Checked, (double)(m_numLimit.Value));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestSetSettings()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void m_numLimit_ValueChanged(object sender, EventArgs e)
        {
            m_btnOk.Enabled = true;
        }
    }
}
