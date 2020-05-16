using MZ.Controls;
using MZ.Tools.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ
{
    public partial class FormMainTest : Form
    {
        public const string TITLE = "Common MZ Testing";

        public FormMainTest()
        {
            InitializeComponent();
        }

        private void FormMainTest_Load(object sender, EventArgs e)
        {

        }

        private void m_btnTestEdit_Click(object sender, EventArgs e)
        {
            Control ctrl = m_btnTestEdit;
            FormInPlaceEdit frm = new FormInPlaceEdit()
            {
                //Font = ctrl.Font,
                EditText = ctrl.Text
            };
            frm.Draggable(true);

            Point location = this.PointToScreen(ctrl.Location);
            location.Offset(0, ctrl.Height);
            frm.Location = location;
            frm.OkAction = (text) =>
            {
                ctrl.Text = text;
            };
            string oldText = ctrl.Text;
            frm.CancelAction = (text) =>
            {
                ctrl.Text = oldText;
            };

            frm.EditTextChangedAction = (text) =>
            {
                ctrl.Text = text;
            };

            frm.Show(this);
        }
    }
}
