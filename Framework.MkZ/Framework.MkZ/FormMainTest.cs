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
using MZ.WPF.MessageBox;
using System.IO;

namespace MZ
{
    public partial class FormMainTest : Form
    {
        public const string TITLE = "Common MZ Testing";

        Random _r = new Random();

        List<double> _values = new List<double>();


    public FormMainTest()
        {
            InitializeComponent();

            m_cmbListViewType.Items.AddRange(Enum.GetValues(typeof(View)).Cast<Object>().ToArray());
            m_cmbListViewType.SelectedIndex = 0;

            //string folder = @"C:\Dev_Mark\GitHub\zbarjevsky\.NET-Projects\CommonMZ\CommonMZ\Images\Shell32";
            //Shell32_Icons.SaveImages(folder, Shell32_Icons.SmallIconsList);
            //Shell32_Icons.SaveImages(folder, Shell32_Icons.LargeIconsList);

            //chart values
            for (int i = 0; i < trackBar1.Maximum; i++)
            {
                _values.Add(256 + 256 * Math.Sin(6 * Math.PI * i / 180.0));
            }
        }

        private void FormMainTest_Load(object sender, EventArgs e)
        {
            foldersTreeUserControl1.AfterSelectAction = (fullPath) =>
            {
                fileExplorerUserControl1.PopulateFiles(fullPath);
            };

            foldersTreeUserControl1.SelectFolder(@"D:\Temp");

            //listView1.SmallImageList = Shell32_Icons.SmallImageList;
            //listView1.LargeImageList = Shell32_Icons.LargeImageList;

            //for (int i = 0; i < Shell32_Icons.SmallImageList.Images.Count; i++)
            //{
            //    listView1.Items.Add("i" + i, i);
            //}

            //NonStickMouse.EnableMouseCorrection(true);
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
            if(trackBar1.Value <= colorBarsProgressBar1.Maximum)
                colorBarsProgressBar1.Value = (int)trackBar1.Value;
            if (trackBar1.Value <= colorBarsProgressBar2.Maximum)
                colorBarsProgressBar2.Value = (int)trackBar1.Value;
            colorBarsProgressBar3.Value = (int)trackBar1.Value;

            //chart values
            List<double> values = new List<double>();
            for (int i = 0; i < trackBar1.Value; i+=10)
            {
                values.Add(_values[i]);
            }
            chartProgressUserControl1.SetHistory(values, 10, "Trackbar Value "+ trackBar1.Value);
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

        private void m_btnOpenIconsFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "Icon Containers *.exe, *.dll|*.exe;*.dll|Images *.png, *.jpg | *.png; *.jpg| All Files (*.*)|*.*";
            dlg.FileName = @"C:\Windows\System32\Shell32.dll";
            dlg.CheckFileExists = false;
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                IconsExtractor extractor = new IconsExtractor(dlg.FileNames);

                listView1.SmallImageList = extractor.SmallImageList;
                listView1.LargeImageList = extractor.LargeImageList;

                if (extractor.SmallImageList.Images.Count == 0)
                    throw new Exception("No Images found in: " + Path.GetFileName(dlg.FileName));

                listView1.Items.Clear();
                for (int i = 0; i < extractor.SmallImageList.Images.Count; i++)
                {
                    listView1.Items.Add("i" + i, i);
                }
            }
            catch (Exception err)
            {
                this.MessageError(err.Message);
            }        
        }

        private void m_btnSaveIcons_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;
        }
    }
}
