using System.Windows.Forms;

namespace DUMeterMZ
{
	public partial class FormProperties
	{
		private void InitializeComponent()
		{
            this.m_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_statusStrip.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PropertyGrid
            // 
            this.m_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.m_PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.m_PropertyGrid.Name = "m_PropertyGrid";
            this.m_PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.m_PropertyGrid.Size = new System.Drawing.Size(296, 470);
            this.m_PropertyGrid.TabIndex = 1;
            this.m_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.m_PropertyGrid_PropertyValueChanged);
            this.m_PropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.m_PropertyGrid_SelectedGridItemChanged);
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1,
            this.m_toolStripStatusLabel2,
            this.m_toolStripStatusLabel3});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 474);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Size = new System.Drawing.Size(300, 22);
            this.m_statusStrip.TabIndex = 2;
            this.m_statusStrip.Text = "statusStrip1";
            // 
            // m_toolStripStatusLabel1
            // 
            this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
            this.m_toolStripStatusLabel1.Size = new System.Drawing.Size(237, 17);
            this.m_toolStripStatusLabel1.Spring = true;
            this.m_toolStripStatusLabel1.Text = "Ready";
            this.m_toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_toolStripStatusLabel2
            // 
            this.m_toolStripStatusLabel2.AutoSize = false;
            this.m_toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.m_toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.m_toolStripStatusLabel2.Name = "m_toolStripStatusLabel2";
            this.m_toolStripStatusLabel2.Size = new System.Drawing.Size(24, 16);
            // 
            // m_toolStripStatusLabel3
            // 
            this.m_toolStripStatusLabel3.AutoSize = false;
            this.m_toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.m_toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.m_toolStripStatusLabel3.Name = "m_toolStripStatusLabel3";
            this.m_toolStripStatusLabel3.Size = new System.Drawing.Size(24, 16);
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlMain.Controls.Add(this.m_PropertyGrid);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(300, 474);
            this.m_pnlMain.TabIndex = 0;
            // 
            // FormProperties
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(300, 496);
            this.Controls.Add(this.m_pnlMain);
            this.Controls.Add(this.m_statusStrip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DU MeterMZ Properties";
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.m_pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}//end InitializeComponent

		private StatusStrip m_statusStrip;
		private ToolStripStatusLabel m_toolStripStatusLabel1;
		private ToolStripStatusLabel m_toolStripStatusLabel2;
		private ToolStripStatusLabel m_toolStripStatusLabel3;
		private Panel m_pnlMain;
		private System.Windows.Forms.PropertyGrid m_PropertyGrid;
	}
}