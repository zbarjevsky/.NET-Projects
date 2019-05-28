using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardManager.Utils
{
    public partial class HotKeyEditorUserControl : UserControl
    {
        public HotKeyData HotKey { get; set; } = new HotKeyData();

        public HotKeyEditorUserControl()
        {
            InitializeComponent();
        }

        private void HotKeyEditorUserControl_Load(object sender, EventArgs e)
        {

        }

        public void UpdateData()
        {
            m_txtHotKey.Text = HotKey.ToString();
            m_chkUseHotKey.Checked = HotKey.UseHotKey;
        }

        private void m_chkUseHotKey_CheckedChanged(object sender, EventArgs e)
        {
            HotKey.UseHotKey = m_chkUseHotKey.Checked;
            m_txtHotKey.Enabled = HotKey.UseHotKey;
        }

        private void m_txtHotKey_TextChanged(object sender, EventArgs e)
        {
            m_txtHotKey.Text = HotKey.ToString();
        }

        private void m_txtHotKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode < Keys.A || e.KeyCode > Keys.Z)
                return;

            HotKey.KeyData = e.KeyData;
            e.Handled = true;
            m_txtHotKey.Text = HotKey.ToString();
        }
    }
}
