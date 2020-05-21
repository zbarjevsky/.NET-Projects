using MZ.Controls;
using MZ.ControlsWinForms;
using MZ.Tools;
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

        private void foldersTreeUserControl1_Load(object sender, EventArgs e)
        {

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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            colorBarsProgressBar1.Value = trackBar1.Value;
            colorBarsProgressBar2.Value = trackBar1.Value;
            colorBarsProgressBar3.Value = trackBar1.Value;
        }
    }
}
