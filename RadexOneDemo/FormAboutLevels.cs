using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadexOneDemo
{
    public partial class FormAboutLevels : Form
    {
        public FormAboutLevels()
        {
            InitializeComponent();

            richTextBox1.Rtf = Properties.Resources.How_much_is_dangerous;
        }

        private void FormAboutLevels_Load(object sender, EventArgs e)
        {

        }
    }
}
