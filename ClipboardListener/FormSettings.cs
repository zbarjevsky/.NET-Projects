using ClipboardManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace ClipboardManager
{
	public partial class FormSettings : Form
	{
		private Settings m_Settings = null;
		private HotKeyData m_HotKey = null;

        public FormSettings(Settings settings)
		{
			this.m_Settings = settings;
			
			//remember old settings - to reset on cancel
			m_HotKey = m_Settings.I.HotKey.Clone();
			
			InitializeComponent();
		}//end constructor

		private void m_txtHotKey_TextChanged(object sender, EventArgs e)
		{
			m_txtHotKey.Text = m_HotKey.ToString();
		}//end m_txtHotKey_TextChanged

		private void m_txtHotKey_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode < Keys.A || e.KeyCode > Keys.Z )
				return;

			m_HotKey.SetHotKey(e);
			e.Handled = true;
			m_txtHotKey.Text = m_HotKey.ToString();
		}//end m_txtHotKey_KeyUp

		private void FormSettings_Load(object sender, EventArgs e)
		{
			m_txtHotKey.Text				= m_HotKey.ToString();
			m_chkUseHotKey.Checked			= m_HotKey.m_bUseHotKey;

			m_numHistoryLen.Value			= m_Settings.I.m_iMenuMaxLen;
            m_numHistoryMax.Value           = m_Settings.I.m_iBufferMaxLen;
			m_chkStartWithWindows.Checked	= LoadWithWindows;
		    m_chkAutoUAC.Checked            = m_Settings.I.m_bAutoUAC;
            m_chkAbortShutdown.Checked      = m_Settings.I.m_bAbortShutdown;
            m_chkStopServices.Checked       = m_Settings.I.m_bStopServices;


            m_HotKey.UnregisterHotKey(this.Parent as Form); //to allow change or reset

            m_chkReconnect.Checked			= m_Settings.I.m_AutoReconnect;
            m_chkLog.Checked    			= m_Settings.WriteLogFile;

            propertyGrid1.SelectedObject    = m_Settings.I;
        }//end FormSettings_Load

		private void m_btnOK_Click(object sender, EventArgs e)
		{
			LoadWithWindows = m_chkStartWithWindows.Checked;

			m_Settings.I.m_iMenuMaxLen = (int)m_numHistoryLen.Value;
			m_Settings.I.m_iBufferMaxLen = (int)m_numHistoryMax.Value;

			m_Settings.I.HotKey = m_HotKey; //set new values
			m_Settings.I.HotKey.RegisterHotKey(this.Parent as Form); //register if needed

			m_Settings.I.m_AutoReconnect = m_chkReconnect.Checked;
            m_Settings.WriteLogFile  = m_chkLog.Checked;
		    m_Settings.I.m_bAutoUAC = m_chkAutoUAC.Checked;
		    m_Settings.I.m_bAbortShutdown = m_chkAbortShutdown.Checked;
		    m_Settings.I.m_bStopServices = m_chkStopServices.Checked;
		}//end m_btnOK_Click

		private void m_btnCancel_Click(object sender, EventArgs e)
		{
			m_Settings.I.HotKey.RegisterHotKey(this.Parent as Form); //register old key if needed
		}//end m_btnCancel_Click

        private const string _strAppKey = @"ClipboardHistoryMZ";
        private Microsoft.Win32.RegistryKey OpenRegKey(bool writable)
        {
            //const string REG_KEY = @"Software\Microsoft\Windows\CurrentVersion\Run";
            const string REG_KEY = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run";
            try
            {
                return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY, writable);
            }
            catch (Exception err)
            {
                CenteredMessageBox.MsgBoxErr(err.Message);
                return null;
            }
        }

		[Category("Windows Options")]
		[Description("Load application when windows starts")]
		[DisplayName("Load with Windows")]
		public bool LoadWithWindows
		{
			get
			{
                var key = OpenRegKey(writable: false);
                if (key == null)
                    return false;
                bool run = (key.GetValue(_strAppKey) != null);
                key.Close();
                return run;
			}//end get
			
            set
			{
                var key = OpenRegKey(writable: true);
                if (key == null)
                    return;

                if (value)
					key.SetValue(_strAppKey, "\""+Application.ExecutablePath+"\"");
				else
					key.DeleteValue(_strAppKey, false);
                key.Close();
			}//end set
		}//end LoadWithWindows

		private void m_chkUseHotKey_CheckedChanged(object sender, EventArgs e)
		{
			m_txtHotKey.Enabled = m_chkUseHotKey.Checked;
			m_HotKey.m_bUseHotKey = m_chkUseHotKey.Checked;
		}
	}//end class FormSettings
}//end namespace ClipboardListener