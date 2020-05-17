using MZ.Controls;
using MZ.ControlsWinForms;
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

            Point location = this.PointToScreen(ctrl.Location);
            location.Offset(0, ctrl.Height);
            string oldText = ctrl.Text;

            InPlaceTextBox.ShowTextBox(oldText, ctrl.Font, m_btnTestEdit, (text) => { m_btnTestEdit.Text = text; });

            //FormInPlaceEdit.ShowInPlaceEdit(ctrl.Text, ctrl.Font, ctrl.Location, this,
            //(text) => { ctrl.Text = text; },
            //(text) => { ctrl.Text = text; },
            //(text) => { ctrl.Text = oldText; });
        }
    }
}
