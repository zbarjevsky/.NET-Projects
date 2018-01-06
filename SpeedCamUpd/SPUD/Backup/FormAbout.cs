using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SPUD
{
  public partial class FormAbout : Form
  {
    public FormAbout()
    {
      InitializeComponent();
    }

    private void m_lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("mailto:zbarjevsky@hotmail.com");
      //FormMain.OpenUrl("mailto:zbarjevsky@hotmail.com");
    }
  }
}