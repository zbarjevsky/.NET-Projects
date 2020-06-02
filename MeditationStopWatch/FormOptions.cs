using System;
using System.Windows.Forms;
using System.Drawing;
using MeditationStopWatch.Tools;
using MZ.WinForms;

namespace MeditationStopWatch
{
	/// <summary>
	/// Summary description for OptionForm.
	/// </summary>
	public partial class FormOptions : Form
	{
        private Action<string> OnPropertyChange = (propertyName) => { };

		public FormOptions(Options propclass, Action<string> onPropertyChange = null)
		{
			InitializeComponent();
			
			m_PropertyGrid.SelectedObject = propclass;
			m_PropertyGrid.HelpBackColor = propclass.Background;
            m_PropertyGrid.ExpandGridItem("AnalogClockSettings");

            if (onPropertyChange != null)
                OnPropertyChange = onPropertyChange;
		}//end constructor

		private void OptionForm_Load(object sender, EventArgs e)
		{
            m_PropertyGrid.HelpBackColor = ((Options)m_PropertyGrid.SelectedObject).Background;
            m_PropertyGrid.VerticalScroll.Value = m_PropertyGrid.VerticalScroll.Maximum;
            m_PropertyGrid.MoveSplitterTo(200);

        }//end OptionForm_Load

		private void m_PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			m_PropertyGrid.HelpBackColor = ((Options)m_PropertyGrid.SelectedObject).Background;
            string propertyName = e == null ? "" : e.ChangedItem.Label;
            OnPropertyChange(propertyName);
        }

		private void m_PropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
		{
			if (e != null)
				m_toolStripStatusLabel1.Text = e.NewSelection.Label;
		}
	}//end class FormProperties
}//end namespace DUMeterMZ
