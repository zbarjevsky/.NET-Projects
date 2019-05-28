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
		private SettingsData m_SettingsData = null;
        private HotKeyData HotKey { get; set; } //for undo

        public FormSettings(Settings settings)
		{
			this.m_SettingsData = settings.I;
            HotKey = settings.I.HotKeyInfo.Clone();

            InitializeComponent();
		}//end constructor

		private void FormSettings_Load(object sender, EventArgs e)
		{
            m_SettingsData.HotKeyInfo.UnregisterHotKey(this.Owner); //to allow change or reset
            m_HotKeyEditor.HotKey = m_SettingsData.HotKeyInfo;
            m_gridSettings.SelectedObject    = m_SettingsData;
        }//end FormSettings_Load

		private void m_btnOK_Click(object sender, EventArgs e)
		{
			m_SettingsData.HotKeyInfo.RegisterHotKey(this.Owner); //register if needed
		}//end m_btnOK_Click

		private void m_btnCancel_Click(object sender, EventArgs e)
		{
            m_SettingsData.HotKeyInfo = HotKey; //undo
            m_SettingsData.HotKeyInfo.RegisterHotKey(this.Owner); //register old key if needed
		}//end m_btnCancel_Click

        private void m_gridSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_HotKeyEditor.UpdateData();
        }
    }//end class FormSettings
}//end namespace ClipboardListener