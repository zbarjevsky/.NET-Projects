using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFFAvi
{
  public partial class FormPreferences : Form
  {
    public FormPreferences(ProgramPreferences pref)
    {
      InitializeComponent();

      m_PropertyGrid.SelectedObject = pref;
      m_PropertyGrid.HelpBackColor = SystemColors.Window;
    }

    private void FormPreferences_Load(object sender, EventArgs e)
    {
      m_PropertyGrid_PropertyValueChanged(null, null);
      m_PropertyGrid.VerticalScroll.Value = m_PropertyGrid.VerticalScroll.Maximum;
    }

    private void m_PropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
    {
      if (e != null)
        m_toolStripStatusLabel1.Text = e.NewSelection.Label;
    }

    private void m_PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {

    }
  }
}
