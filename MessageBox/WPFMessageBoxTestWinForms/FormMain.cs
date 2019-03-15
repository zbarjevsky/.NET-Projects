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
    public partial class FormMain : Form
    {
        private Bitmap _imgError = System.Drawing.SystemIcons.Hand.ToBitmap();
        private Bitmap _imgInfo = System.Drawing.SystemIcons.Information.ToBitmap();
        private Bitmap _imgExcl = System.Drawing.SystemIcons.Exclamation.ToBitmap();
        private Bitmap _imgQuest = System.Drawing.SystemIcons.Question.ToBitmap();

        public FormMain()
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

        }

        private void AddIcon(Icon icon, string name)
        {
            m_imageListIcons.Images.Add(icon);
            listView1.Items.Add(name, m_imageListIcons.Images.Count-1);
        }

        private void m_btnInfo_Click(object sender, EventArgs e)
        {
            sD.WPF.MessageBox.PopUp.Information("Information.");
        }

        private void m_btnWarn_Click(object sender, EventArgs e)
        {
            sD.WPF.MessageBox.PopUp.Exclamation("Exclamation!");
        }

        private void m_btnError_Click(object sender, EventArgs e)
        {
            sD.WPF.MessageBox.PopUp.Error("Error!!!");
        }

        private void m_btnQuestion_Click(object sender, EventArgs e)
        {
            sD.WPF.MessageBox.PopUp.Question("Question?");
        }

        private void m_btnQuestionYNC_Click(object sender, EventArgs e)
        {
            sD.WPF.MessageBox.PopUp.Question("Question?", "Title", MessageBoxImage.Question, TextAlignment.Center, MessageBoxButton.YesNoCancel);
        }

        private void m_btnSpecial_Click(object sender, EventArgs e)
        {
            sD.WPF.MessageBox.PopUp.MessageBox("Question?", "Title", MessageBoxImage.Question, TextAlignment.Center, MessageBoxButton.YesNoCancel,
                "Yes - Long Text", "No - Long Text", "Cancel - Long Text");
        }
    }
}
