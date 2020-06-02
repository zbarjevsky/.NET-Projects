using MZ.WinForms;

using MZ.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MZ.WPF;

namespace MZ
{
    public partial class FormMainTest : Form
    {
        public const string TITLE = "Common MZ Testing";

        public FormMainTest()
        {
            InitializeComponent();

            m_cmbListViewType.Items.AddRange(Enum.GetValues(typeof(View)).Cast<Object>().ToArray());
            m_cmbListViewType.SelectedIndex = 0;

            //string folder = @"C:\Dev_Mark\GitHub\zbarjevsky\.NET-Projects\CommonMZ\CommonMZ\Images\Shell32";
            //Shell32_Icons.SaveImages(folder, Shell32_Icons.SmallIconsList);
            //Shell32_Icons.SaveImages(folder, Shell32_Icons.LargeIconsList);
        }

        private void FormMainTest_Load(object sender, EventArgs e)
        {
            foldersTreeUserControl1.AfterSelectAction = (fullPath) =>
            {
                fileExplorerUserControl1.PopulateFiles(fullPath);
            };

            foldersTreeUserControl1.SelectFolder(@"D:\Temp");

            listView1.SmallImageList = Shell32_Icons.SmallImageList;
            listView1.LargeImageList = Shell32_Icons.LargeImageList;

            for (int i = 0; i < Shell32_Icons.SmallImageList.Images.Count; i++)
            {
                listView1.Items.Add("i" + i, i);
            }
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

        private void m_btnBrowseForFolder_Click(object sender, EventArgs e)
        {
            FormBrowseForFolder browse = new FormBrowseForFolder();
            browse.SelectedFolder = @"C:\Dev_Mark\Temp";
            browse.ShowDialog(this);
        }

        private void m_cmbListViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.View = (View)m_cmbListViewType.SelectedItem;
        }

        private void trackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            colorBarsProgressBar1.Value = (int)trackBar1.Value;
            colorBarsProgressBar2.Value = (int)trackBar1.Value;
            colorBarsProgressBar3.Value = (int)trackBar1.Value;
        }

        private void m_btnTestWPFMessageBox_Click(object sender, EventArgs e)
        {
            WPFMessageBoxTestWinForms.FormTestWpfMessageBox frm = new WPFMessageBoxTestWinForms.FormTestWpfMessageBox();
            frm.ShowDialog(this);
        }

        private void m_btnTestWPFMessageBoxWPF_Click(object sender, EventArgs e)
        {
            WPFMessageBoxTestWPF.WindowTestWpfMesageBox wnd = new WPFMessageBoxTestWPF.WindowTestWpfMesageBox();
            wnd.ShowDialog();
        }

        private void m_btnColorSlider_Click(object sender, EventArgs e)
        {
            TestColorSlider.FormColorSliderDemo frm = new TestColorSlider.FormColorSliderDemo();
            frm.ShowDialog(this);
        }

        private void m_btnGradientWpfProgress_Click(object sender, EventArgs e)
        {
            PopupInfoWindow wnd = new PopupInfoWindow(
                System.Windows.WindowStartupLocation.Manual, 
                new System.Windows.Point(100, 100), 
                this.Handle);
            wnd.ShowActivated = false;
            wnd.Show();
        }

        private void m_chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            colorBarsProgressBar1.Enabled = m_chkEnable.Checked;
            colorBarsProgressBar2.Enabled = m_chkEnable.Checked;
            colorBarsProgressBar3.Enabled = m_chkEnable.Checked;
        }
    }
}
