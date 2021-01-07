using MkZ.WPF.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WPFMessageBoxTestWinForms
{
    public partial class FormTestWpfMessageBox : Form
    {
        private Bitmap _imgError = System.Drawing.SystemIcons.Hand.ToBitmap();
        private Bitmap _imgInfo = System.Drawing.SystemIcons.Information.ToBitmap();
        private Bitmap _imgExcl = System.Drawing.SystemIcons.Exclamation.ToBitmap();
        private Bitmap _imgQuest = System.Drawing.SystemIcons.Question.ToBitmap();

        public FormTestWpfMessageBox()
        {
            InitializeComponent();

            m_btnInfo.Image = _imgInfo;
            m_btnError.Image = _imgError;
            m_btnWarn.Image = _imgExcl;
            m_btnQuestion.Image = _imgQuest;
            m_btnQuestionYNC.Image = _imgQuest;

            AddIcon(SystemIcons.Application, "Application");
            AddIcon(SystemIcons.Asterisk,    "Asterisk");
            AddIcon(SystemIcons.Error,       "Error");
            AddIcon(SystemIcons.Exclamation, "Exclamation");
            AddIcon(SystemIcons.Hand,        "Hand");
            AddIcon(SystemIcons.Information, "Information");
            AddIcon(SystemIcons.Question,    "Question");
            AddIcon(SystemIcons.Shield,      "Shield");
            AddIcon(SystemIcons.Warning,     "Warning");
            AddIcon(SystemIcons.WinLogo,     "WinLogo");

            this.Icon = SystemIcons.Information;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_cmbIconStyle.DataSource = Enum.GetNames(typeof(MkZ.WPF.MessageBox.PopUp.IconStyle));
            m_cmbIconStyle.Text = MkZ.WPF.MessageBox.PopUp.IconType.ToString();
        }

        private void m_cmbIconStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MkZ.WPF.MessageBox.PopUp.IconType = (MkZ.WPF.MessageBox.PopUp.IconStyle)Enum.Parse(typeof(MkZ.WPF.MessageBox.PopUp.IconStyle), m_cmbIconStyle.SelectedItem.ToString());
        }

        private void AddIcon(Icon icon, string name)
        {
            m_imageListIcons.Images.Add(icon);
            listView1.Items.Add(name, m_imageListIcons.Images.Count-1);
        }

        private void m_btnInfo_Click(object sender, EventArgs e)
        {
            //MZ.WPF.MessageBox.PopUp.Information(richTextBox1.Text);
            this.MessageInfo(richTextBox1.Text);
        }

        private void m_btnWarn_Click(object sender, EventArgs e)
        {
            //MZ.WPF.MessageBox.PopUp.Exclamation(richTextBox1.Text);
            this.MessageWarning(richTextBox1.Text);
        }

        private void m_btnError_Click(object sender, EventArgs e)
        {
            //MZ.WPF.MessageBox.PopUp.Error(richTextBox1.Text);
            this.MessageError(richTextBox1.Text);
        }

        private void m_btnQuestion_Click(object sender, EventArgs e)
        {
            PopUp.PopUpResult res = this.MessageQuestion(richTextBox1.Text);
            //MZ.WPF.MessageBox.PopUp.Question(richTextBox1.Text);

            this.MessageInfo("Result = " + res);
        }

        private void m_btnQuestionYNC_Click(object sender, EventArgs e)
        {
            PopUp.PopUpResult res = this.MessageQuestion(richTextBox1.Text, "Question", PopUp.PopUpButtonsType.CancelNoYes);
            //PopUpResult res = MZ.WPF.MessageBox.PopUp.Question(richTextBox1.Text, "Title", MessageBoxImage.Question, TextAlignment.Center, MZ.WPF.MessageBox.PopUp.PopUpButtonsType.CancelNoYes);

            this.MessageInfo("Result = " + res);
        }

        private void m_btnSpecial_Click(object sender, EventArgs e)
        {
            PopUp.PopUpResult res = this.MessageBoxEx(richTextBox1.Text, "Title", 
                MessageBoxImage.Question, TextAlignment.Center, 
                new MkZ.WPF.MessageBox.PopUp.PopUpButtons(
                    "Cancel - Da Da Da Da Long Text", "No - Long Long Long Text", "Yes - Long Text", 
                    PopUp.PopUpResult.Btn2));

            this.MessageInfo("Result = "+res);
        }

        private void m_btnInput_Click(object sender, EventArgs e)
        {
            string inputText = "Type Here...";
            if(MkZ.WPF.MessageBox.PopUp.InputBox(ref inputText, "Title") == MkZ.WPF.MessageBox.PopUp.PopUpResult.OK)
                this.MessageInfo(inputText, Text, TextAlignment.Justify);
        }

        private void m_btnTimeout_Click(object sender, EventArgs e)
        {
            MkZ.WPF.MessageBox.PopUp.PopUpResult res = this.MessageBoxEx(richTextBox1.Text, "Title", 
                MessageBoxImage.Question, TextAlignment.Center,
                new MkZ.WPF.MessageBox.PopUp.PopUpButtons("Cancel ", "No", "Yes", MkZ.WPF.MessageBox.PopUp.PopUpResult.Btn2),
                timeout: (int)m_numTimeout.Value);

            this.MessageInfo("Result = " + res);
        }

        private void m_numTimeout_ValueChanged(object sender, EventArgs e)
        {
            m_btnTimeout.Text = "Timeout: " + (m_numTimeout.Value/1000) + " sec...";
        }
    }
}
