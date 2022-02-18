using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            m_txtAbouLevels.Rtf = Properties.Resources.How_much_is_dangerous;
        }

        private void FormAboutLevels_Load(object sender, EventArgs e)
        {
            m_lnkRadiationMap_LinkClicked(sender, null);
        }

        private const string _radonUrl = "https://www.radon.com/maps/";
        private const string _radiationUrl = "https://www.epa.gov/radnet/near-real-time-and-laboratory-data-state";

        private void m_lnlRadonLevelMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Navigate(_radonUrl, 200);
        }

        private void m_lnkRadiationMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Navigate(_radiationUrl, 600);
        }

        private void m_tabAbout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tabAbout.SelectedIndex == 1) //radiation map - web browser
            {
                const int WIDTH = 1200;
                if (this.Width < WIDTH)
                {
                    this.Left = this.Left - (WIDTH - this.Width) / 2; //center
                    this.Width = WIDTH;

                    this.Top = this.Top - (900 - this.Height) / 2;
                    this.Height = 900;
                }
            }
        }

        private void Navigate(string url, int scrollToY)
        {
            this.Cursor = Cursors.WaitCursor;
            _scrollToY = scrollToY;

            try
            {
                m_webBrowser.Navigate(new Uri(url));
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show(this, err.ToString(), "Navigate to ERROR", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int _scrollToY = 0;
        private void m_webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //int y = m_webBrowser.Document.Window.Size.Height;
            m_webBrowser.Document.Window.ScrollTo(0, _scrollToY);
            this.Cursor = Cursors.Arrow;
        }

        private void m_mnuCopy_Click(object sender, EventArgs e)
        {
            string url = GetCtxMenuUrl(sender);
            Clipboard.SetText(url);
        }

        private void m_mnuOpen_Click(object sender, EventArgs e)
        {
            string url = GetCtxMenuUrl(sender);
            Process.Start(url);
        }

        private string GetCtxMenuUrl(object sender)
        {
            Control ctrl = CtxMenuParent(sender);
            if(ctrl != null)
            {
                if (ctrl == m_lnkRadiationMap)
                    return _radiationUrl;
                return _radonUrl;
            }
            return _radiationUrl;
        }

        private Control CtxMenuParent(object sender)
        {
            ToolStripItem ctrl = sender as ToolStripItem;
            if (ctrl != null && ctrl.Owner != null)
            {
                ContextMenuStrip mnu = ctrl.Owner as ContextMenuStrip;
                return mnu.SourceControl;
            }
            return null;
        }
    }
}
