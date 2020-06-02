using System;
using System.Windows.Forms;
using System.Drawing;

namespace DUMeterMZ
{
	/// <summary>
	/// Summary description for OptionForm.
	/// </summary>
	public partial class FormProperties : Form
	{
		public Action<string> OnPropertyChangedAction = (propertyName) => { };
		
		public FormProperties(Options propclass)
		{
			InitializeComponent();

			m_PropertyGrid.SelectedObject = propclass;
			m_PropertyGrid.HelpBackColor = propclass.Background;
		}//end constructor

		private void OptionForm_Load(object sender, EventArgs e)
		{
			m_PropertyGrid_PropertyValueChanged(null, null);
			m_PropertyGrid.VerticalScroll.Value = m_PropertyGrid.VerticalScroll.Maximum;
		}//end OptionForm_Load

		private void m_PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			m_PropertyGrid.HelpBackColor = ((Options)m_PropertyGrid.SelectedObject).Background;
			if(e != null)
				OnPropertyChangedAction(e.ChangedItem.PropertyDescriptor.Name);
		}

		private void m_PropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
		{
			if (e != null)
				m_toolStripStatusLabel1.Text = e.NewSelection.Label;
		}
	}//end class FormProperties
}//end namespace DUMeterMZ
