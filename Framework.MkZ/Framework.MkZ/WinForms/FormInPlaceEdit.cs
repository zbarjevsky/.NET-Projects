using MkZ.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MkZ.WinForms
{
    public partial class FormInPlaceEdit : Form
    {
        public Action<string> OkAction = (text) => { };
        public Action<string> CancelAction = (text) => { };
        public Action<string> EditTextChangedAction = (text) => { };

        public string EditText { get { return m_txtInput.Text; } set { m_txtInput.Text = value; } }
        public Font EditFont { get { return m_txtInput.Font; } set { m_txtInput.Font = value; } }

        public static void ShowInPlaceEdit(string text, Font font, Point location, IWin32Window owner, 
            Action<string> OkAction, 
            Action<string> TextEditAction = null, 
            Action<string> CancelAction = null)
        {
            FormInPlaceEdit frm = new FormInPlaceEdit()
            {
                EditFont = font,
                EditText = text
            };

            frm.Location = location;
            
            frm.OkAction = OkAction;
            if (TextEditAction != null)
                frm.EditTextChangedAction = TextEditAction;
            if (CancelAction != null)
                frm.CancelAction = CancelAction;

            frm.Show(owner);
        }

        public FormInPlaceEdit()
        {
            InitializeComponent();
        }

        private void FormInPlaceEdit_Load(object sender, EventArgs e)
        {
            this.Draggable(true);
            this.ActiveControl = m_txtInput;
            m_txtInput.SelectAll();
        }

        private void m_txtInput_TextChanged(object sender, EventArgs e)
        {
            UpdateSizeCorrespondingToTextLength();
            EditTextChangedAction(EditText);
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
            CancelAction(EditText);
            this.Close();
        }

        private void UpdateSizeCorrespondingToTextLength()
        {
            Size size = TextRenderer.MeasureText(m_txtInput.Text, m_txtInput.Font);
            int delta = size.Width - m_txtInput.Width;
            this.Width += delta;
        }
    }
}
