using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeditationStopWatch
{
    public partial class FormInPlaceEdit : Form
    {
        public Action<string> OkAction = (text) => { };

        public string EditText { get { return m_txtInput.Text; } set { m_txtInput.Text = value; } }

        public FormInPlaceEdit()
        {
            InitializeComponent();
        }

        private void FormInPlaceEdit_Load(object sender, EventArgs e)
        {
            this.ActiveControl = m_txtInput;
            m_txtInput.SelectAll();
        }

        private void FormInPlaceEdit_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            OkAction(EditText);
            this.Close();
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
