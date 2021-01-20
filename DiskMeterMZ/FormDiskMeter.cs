using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestFileBrowser
{
	/// <summary>
	/// Summary description for FormDiskMeter.
	/// </summary>
	public class FormDiskMeter : System.Windows.Forms.Form
	{
		private System.Timers.Timer timer1;
		private System.Diagnostics.PerformanceCounter performanceCounterRead;
		private System.Diagnostics.PerformanceCounter performanceCounterWrite;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripMenuItem meterToolStripMenuItem;
		private System.ComponentModel.IContainer components;

		public FormDiskMeter()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			string [] svIcons = {
				"TestFileBrowser.res.Disk.ico",
				"TestFileBrowser.res.DiskRead.ico",
				"TestFileBrowser.res.DiskWrite.ico",
				"TestFileBrowser.res.DiskReadWrite.ico",
			};
			m_IconList = new MkZ.IconList(svIcons);
		}//end constructor

		private MkZ.IconList m_IconList = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiskMeter));
			this.performanceCounterRead = new System.Diagnostics.PerformanceCounter();
			this.timer1 = new System.Timers.Timer();
			this.performanceCounterWrite = new System.Diagnostics.PerformanceCounter();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.meterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounterRead)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounterWrite)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// performanceCounterRead
			// 
			this.performanceCounterRead.CategoryName = "PhysicalDisk";
			this.performanceCounterRead.CounterName = "Disk Read Bytes/sec";
			this.performanceCounterRead.InstanceName = "_Total";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			// 
			// performanceCounterWrite
			// 
			this.performanceCounterWrite.CategoryName = "PhysicalDisk";
			this.performanceCounterWrite.CounterName = "Disk Write Bytes/sec";
			this.performanceCounterWrite.InstanceName = "_Total";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meterToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem,
            this.toolStripTextBox1,
            this.toolStripComboBox1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(182, 102);
			// 
			// meterToolStripMenuItem
			// 
			this.meterToolStripMenuItem.Image = global::TestFileBrowser.Properties.Resources.Disk;
			this.meterToolStripMenuItem.Name = "meterToolStripMenuItem";
			this.meterToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.meterToolStripMenuItem.Text = "Meter";
			this.meterToolStripMenuItem.Click += new System.EventHandler(this.meterToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = global::TestFileBrowser.Properties.Resources.DiskWrite;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 21);
			// 
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBox1.Items.AddRange(new object[] {
            "first",
            "second",
            "third",
            "3333"});
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(121, 21);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(140, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem1
			// 
			this.exitToolStripMenuItem1.Image = global::TestFileBrowser.Properties.Resources.Disk;
			this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
			this.exitToolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem1.Text = "Exit";
			this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
			// 
			// label1
			// 
			this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label1.ImageIndex = 0;
			this.label1.ImageList = this.imageList1;
			this.label1.Location = new System.Drawing.Point(12, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Initializing...";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormDiskMeter
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(140, 65);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(600, 400);
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "FormDiskMeter";
			this.Opacity = 0.85;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Disk Meter";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FormDiskMeter_Load);
			((System.ComponentModel.ISupportInitialize)(this.performanceCounterRead)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounterWrite)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormDiskMeter());
		}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			float f1 = performanceCounterRead.NextValue();
			float f2 = performanceCounterWrite.NextValue();
			float fMin = 300000.0F;
			if ( f1 > fMin && f2 < fMin )
			{
				if ( label1.ImageIndex != 1 )
				{
					label1.Text = "Read";
					label1.ImageIndex = 1;
					this.Icon = m_IconList[label1.ImageIndex];
				}//end if
			}//end if
			else if ( f1 < fMin && f2 > fMin )
			{
				if ( label1.ImageIndex != 2 )
				{
					label1.Text = "Write";
					label1.ImageIndex = 2;
					this.Icon = m_IconList[label1.ImageIndex];
				}//end if
			}//end else if
			else if ( f1 < fMin && f2 < fMin )
			{
				if ( label1.ImageIndex != 0 )
				{
					label1.Text = "Idle";
					label1.ImageIndex = 0;
					this.Icon = m_IconList[label1.ImageIndex];
				}//end if
			}//end else if
			else if ( f1 > fMin && f2 > fMin )
			{
				if ( label1.ImageIndex != 3 )
				{
					label1.Text = "Read+Write";
					label1.ImageIndex = 3;
					this.Icon = m_IconList[label1.ImageIndex];
				}//end if
			}//end else if

			this.Text = label1.Text + " - Disk Meter";
			this.notifyIcon1.Text = this.Text;
			this.notifyIcon1.Icon = this.Icon;
			System.Diagnostics.Trace.WriteLine("Disk counter: "+f1+" --- "+f2);
		}

		private void menu_Exit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menu_ShowHide_Click(object sender, System.EventArgs e)
		{
		}

		private void FormDiskMeter_Load(object sender, System.EventArgs e)
		{
            //Size sz = System.Windows.Forms.SystemInformation..MaxWindowTrackSize;
            Rectangle r = System.Windows.Forms.SystemInformation.VirtualScreen;
            this.Location = new Point(r.Width - this.Width, r.Height - this.Height-100);
            this.Visible = false;
            meterToolStripMenuItem.Checked = this.Visible;
			this.Refresh();
		}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void meterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
            meterToolStripMenuItem.Checked = this.Visible;
        }
	}
}
