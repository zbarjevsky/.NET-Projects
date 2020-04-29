using System.Windows.Forms;

namespace DUMeterMZ
{
    public partial class ReportForm
    {
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				m_penGrid.Dispose();
				m_penRecv.Dispose();
				m_penBoth.Dispose();
				m_penSend.Dispose();
				m_penLineSpeed.Dispose();
				m_brushBkText.Dispose();

				if (components != null)
				{
					components.Dispose();
				}//end if
			}//end if
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.pictureBoxReport = new System.Windows.Forms.PictureBox();
            this.m_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ctxMenu_ShowSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctxMenu_Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctxMenu_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ctxMenu_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_statusStripLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_statusStripLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_statusStripLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReport)).BeginInit();
            this.m_contextMenuStrip.SuspendLayout();
            this.m_statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxReport
            // 
            this.pictureBoxReport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxReport.ContextMenuStrip = this.m_contextMenuStrip;
            this.pictureBoxReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxReport.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxReport.Name = "pictureBoxReport";
            this.pictureBoxReport.Size = new System.Drawing.Size(716, 200);
            this.pictureBoxReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxReport.TabIndex = 0;
            this.pictureBoxReport.TabStop = false;
            this.pictureBoxReport.Click += new System.EventHandler(this.pictureBoxReport_Click);
            this.pictureBoxReport.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxReport_Paint);
            this.pictureBoxReport.DoubleClick += new System.EventHandler(this.pictureBoxReport_DoubleClick);
            this.pictureBoxReport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxReport_MouseDown);
            this.pictureBoxReport.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxReport_MouseMove);
            this.pictureBoxReport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxReport_MouseUp);
            // 
            // m_contextMenuStrip
            // 
            this.m_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ctxMenu_ShowSelection,
            this.m_ctxMenu_Restore,
            this.m_ctxMenu_Sep1,
            this.m_ctxMenu_Close});
            this.m_contextMenuStrip.Name = "m_contextMenuStrip";
            this.m_contextMenuStrip.Size = new System.Drawing.Size(197, 76);
            // 
            // m_ctxMenu_ShowSelection
            // 
            this.m_ctxMenu_ShowSelection.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_ctxMenu_ShowSelection.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxMenu_ShowSelection.Image")));
            this.m_ctxMenu_ShowSelection.Name = "m_ctxMenu_ShowSelection";
            this.m_ctxMenu_ShowSelection.Size = new System.Drawing.Size(196, 22);
            this.m_ctxMenu_ShowSelection.Text = "&Show Selection";
            this.m_ctxMenu_ShowSelection.Click += new System.EventHandler(this.m_ctxMenu_ShowSelection_Click);
            // 
            // m_ctxMenu_Restore
            // 
            this.m_ctxMenu_Restore.Enabled = false;
            this.m_ctxMenu_Restore.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxMenu_Restore.Image")));
            this.m_ctxMenu_Restore.Name = "m_ctxMenu_Restore";
            this.m_ctxMenu_Restore.Size = new System.Drawing.Size(196, 22);
            this.m_ctxMenu_Restore.Text = "&Restore Original Report";
            this.m_ctxMenu_Restore.Click += new System.EventHandler(this.m_ctxMenu_Restore_Click);
            // 
            // m_ctxMenu_Sep1
            // 
            this.m_ctxMenu_Sep1.Name = "m_ctxMenu_Sep1";
            this.m_ctxMenu_Sep1.Size = new System.Drawing.Size(193, 6);
            // 
            // m_ctxMenu_Close
            // 
            this.m_ctxMenu_Close.Image = ((System.Drawing.Image)(resources.GetObject("m_ctxMenu_Close.Image")));
            this.m_ctxMenu_Close.Name = "m_ctxMenu_Close";
            this.m_ctxMenu_Close.Size = new System.Drawing.Size(196, 22);
            this.m_ctxMenu_Close.Text = "&Close Report";
            this.m_ctxMenu_Close.Click += new System.EventHandler(this.m_ctxMenu_Close_Click);
            // 
            // m_toolTip
            // 
            this.m_toolTip.AutomaticDelay = 0;
            this.m_toolTip.AutoPopDelay = 30000;
            this.m_toolTip.InitialDelay = 0;
            this.m_toolTip.ReshowDelay = 10;
            this.m_toolTip.ShowAlways = true;
            this.m_toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_statusStripLabel1,
            this.m_statusStripLabel2,
            this.m_statusStripLabel3});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 200);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Size = new System.Drawing.Size(716, 24);
            this.m_statusStrip.TabIndex = 1;
            this.m_statusStrip.Text = "Ready";
            // 
            // m_statusStripLabel1
            // 
            this.m_statusStripLabel1.Name = "m_statusStripLabel1";
            this.m_statusStripLabel1.Size = new System.Drawing.Size(673, 19);
            this.m_statusStripLabel1.Spring = true;
            this.m_statusStripLabel1.Text = "Ready";
            this.m_statusStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_statusStripLabel2
            // 
            this.m_statusStripLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_statusStripLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.m_statusStripLabel2.Name = "m_statusStripLabel2";
            this.m_statusStripLabel2.Size = new System.Drawing.Size(14, 19);
            this.m_statusStripLabel2.Text = " ";
            // 
            // m_statusStripLabel3
            // 
            this.m_statusStripLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_statusStripLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.m_statusStripLabel3.Name = "m_statusStripLabel3";
            this.m_statusStripLabel3.Size = new System.Drawing.Size(14, 19);
            this.m_statusStripLabel3.Text = " ";
            // 
            // ReportForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(716, 224);
            this.Controls.Add(this.pictureBoxReport);
            this.Controls.Add(this.m_statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 60);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DUMeterMZ - Report";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.Resize += new System.EventHandler(this.ReportForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReport)).EndInit();
            this.m_contextMenuStrip.ResumeLayout(false);
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.PictureBox pictureBoxReport;
		private StatusStrip m_statusStrip;
		private ToolStripStatusLabel m_statusStripLabel1;
		private ToolStripStatusLabel m_statusStripLabel2;
		private ToolStripStatusLabel m_statusStripLabel3;
		private ContextMenuStrip m_contextMenuStrip;
		private ToolStripMenuItem m_ctxMenu_Restore;
		private ToolStripMenuItem m_ctxMenu_ShowSelection;
		private ToolStripSeparator m_ctxMenu_Sep1;
		private ToolStripMenuItem m_ctxMenu_Close;
		private System.Windows.Forms.ToolTip m_toolTip;
	}
}