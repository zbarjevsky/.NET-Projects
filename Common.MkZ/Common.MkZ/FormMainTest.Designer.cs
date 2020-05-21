namespace MZ
{
    partial class FormMainTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainTest));
            this.m_btnTestEdit = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_cmbListViewType = new System.Windows.Forms.ComboBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_btnTestWPFMessageBox = new System.Windows.Forms.Button();
            this.foldersTreeUserControl1 = new MZ.WinForms.FoldersTreeUserControl();
            this.fileExplorerUserControl1 = new MZ.WinForms.FileExplorerUserControl();
            this.colorBarsProgressBar3 = new MZ.WinForms.ColorBarsProgressBar();
            this.colorBarsProgressBar2 = new MZ.WinForms.ColorBarsProgressBar();
            this.colorBarsProgressBar1 = new MZ.WinForms.ColorBarsProgressBar();
            this.m_btnTestWPFMessageBoxWPF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnTestEdit
            // 
            this.m_btnTestEdit.Location = new System.Drawing.Point(160, 29);
            this.m_btnTestEdit.Name = "m_btnTestEdit";
            this.m_btnTestEdit.Size = new System.Drawing.Size(144, 68);
            this.m_btnTestEdit.TabIndex = 1;
            this.m_btnTestEdit.Text = "Test In-Place-Edit Box";
            this.m_btnTestEdit.UseVisualStyleBackColor = true;
            this.m_btnTestEdit.Click += new System.EventHandler(this.m_btnTestEdit_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(728, 389);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.foldersTreeUserControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileExplorerUserControl1);
            this.splitContainer1.Size = new System.Drawing.Size(736, 422);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.TabIndex = 6;
            // 
            // m_cmbListViewType
            // 
            this.m_cmbListViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbListViewType.FormattingEnabled = true;
            this.m_cmbListViewType.Location = new System.Drawing.Point(6, 6);
            this.m_cmbListViewType.Name = "m_cmbListViewType";
            this.m_cmbListViewType.Size = new System.Drawing.Size(121, 21);
            this.m_cmbListViewType.TabIndex = 7;
            this.m_cmbListViewType.SelectedIndexChanged += new System.EventHandler(this.m_cmbListViewType_SelectedIndexChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBar1.Location = new System.Drawing.Point(92, 29);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 369);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 60;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(750, 454);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.m_cmbListViewType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(742, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Shell32 icons";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(742, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Explorer Controls";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_btnTestWPFMessageBoxWPF);
            this.tabPage3.Controls.Add(this.m_btnTestWPFMessageBox);
            this.tabPage3.Controls.Add(this.m_btnTestEdit);
            this.tabPage3.Controls.Add(this.trackBar1);
            this.tabPage3.Controls.Add(this.colorBarsProgressBar3);
            this.tabPage3.Controls.Add(this.colorBarsProgressBar2);
            this.tabPage3.Controls.Add(this.colorBarsProgressBar1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(742, 428);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Other Controls";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // m_btnTestWPFMessageBox
            // 
            this.m_btnTestWPFMessageBox.Location = new System.Drawing.Point(160, 146);
            this.m_btnTestWPFMessageBox.Name = "m_btnTestWPFMessageBox";
            this.m_btnTestWPFMessageBox.Size = new System.Drawing.Size(144, 68);
            this.m_btnTestWPFMessageBox.TabIndex = 11;
            this.m_btnTestWPFMessageBox.Text = "Test WPFMessageBox (WinForms)";
            this.m_btnTestWPFMessageBox.UseVisualStyleBackColor = true;
            this.m_btnTestWPFMessageBox.Click += new System.EventHandler(this.m_btnTestWPFMessageBox_Click);
            // 
            // foldersTreeUserControl1
            // 
            this.foldersTreeUserControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.foldersTreeUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foldersTreeUserControl1.Location = new System.Drawing.Point(0, 0);
            this.foldersTreeUserControl1.Name = "foldersTreeUserControl1";
            this.foldersTreeUserControl1.Size = new System.Drawing.Size(191, 422);
            this.foldersTreeUserControl1.TabIndex = 2;
            // 
            // fileExplorerUserControl1
            // 
            this.fileExplorerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileExplorerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.fileExplorerUserControl1.Name = "fileExplorerUserControl1";
            this.fileExplorerUserControl1.Size = new System.Drawing.Size(541, 422);
            this.fileExplorerUserControl1.TabIndex = 3;
            // 
            // colorBarsProgressBar3
            // 
            this.colorBarsProgressBar3.ActiveColor = System.Drawing.Color.LimeGreen;
            this.colorBarsProgressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.colorBarsProgressBar3.ColorThemeType = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Speaker;
            this.colorBarsProgressBar3.InactiveBarsColorType = MZ.WinForms.ColorBarsProgressBar.InactiveColorType.Mono;
            this.colorBarsProgressBar3.InactiveColor = System.Drawing.Color.Honeydew;
            this.colorBarsProgressBar3.Location = new System.Drawing.Point(26, 29);
            this.colorBarsProgressBar3.Name = "colorBarsProgressBar3";
            this.colorBarsProgressBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar3.Size = new System.Drawing.Size(21, 369);
            this.colorBarsProgressBar3.TabIndex = 10;
            this.colorBarsProgressBar3.TabStop = false;
            this.colorBarsProgressBar3.Value = 60;
            // 
            // colorBarsProgressBar2
            // 
            this.colorBarsProgressBar2.ActiveColor = System.Drawing.Color.DarkBlue;
            this.colorBarsProgressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorBarsProgressBar2.ColorThemeType = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Regular;
            this.colorBarsProgressBar2.InactiveBarsColorType = MZ.WinForms.ColorBarsProgressBar.InactiveColorType.Mono;
            this.colorBarsProgressBar2.InactiveColor = System.Drawing.Color.Gainsboro;
            this.colorBarsProgressBar2.Location = new System.Drawing.Point(160, 385);
            this.colorBarsProgressBar2.Name = "colorBarsProgressBar2";
            this.colorBarsProgressBar2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.colorBarsProgressBar2.Size = new System.Drawing.Size(567, 27);
            this.colorBarsProgressBar2.TabIndex = 9;
            this.colorBarsProgressBar2.TabStop = false;
            this.colorBarsProgressBar2.Value = 60;
            // 
            // colorBarsProgressBar1
            // 
            this.colorBarsProgressBar1.ActiveColor = System.Drawing.Color.LimeGreen;
            this.colorBarsProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.colorBarsProgressBar1.ColorThemeType = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Microphone;
            this.colorBarsProgressBar1.InactiveBarsColorType = MZ.WinForms.ColorBarsProgressBar.InactiveColorType.Mono;
            this.colorBarsProgressBar1.InactiveColor = System.Drawing.Color.PaleGoldenrod;
            this.colorBarsProgressBar1.Location = new System.Drawing.Point(53, 29);
            this.colorBarsProgressBar1.Name = "colorBarsProgressBar1";
            this.colorBarsProgressBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar1.Size = new System.Drawing.Size(21, 369);
            this.colorBarsProgressBar1.TabIndex = 0;
            this.colorBarsProgressBar1.TabStop = false;
            this.colorBarsProgressBar1.Value = 60;
            // 
            // m_btnTestWPFMessageBoxWPF
            // 
            this.m_btnTestWPFMessageBoxWPF.Location = new System.Drawing.Point(160, 235);
            this.m_btnTestWPFMessageBoxWPF.Name = "m_btnTestWPFMessageBoxWPF";
            this.m_btnTestWPFMessageBoxWPF.Size = new System.Drawing.Size(144, 68);
            this.m_btnTestWPFMessageBoxWPF.TabIndex = 12;
            this.m_btnTestWPFMessageBoxWPF.Text = "Test WPFMessageBox(WPF)";
            this.m_btnTestWPFMessageBoxWPF.UseVisualStyleBackColor = true;
            this.m_btnTestWPFMessageBoxWPF.Click += new System.EventHandler(this.m_btnTestWPFMessageBoxWPF_Click);
            // 
            // FormMainTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 454);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormMainTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Main Test";
            this.Load += new System.EventHandler(this.FormMainTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WinForms.ColorBarsProgressBar colorBarsProgressBar1;
        private System.Windows.Forms.Button m_btnTestEdit;
        private WinForms.FoldersTreeUserControl foldersTreeUserControl1;
        private WinForms.FileExplorerUserControl fileExplorerUserControl1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox m_cmbListViewType;
        private System.Windows.Forms.TrackBar trackBar1;
        private WinForms.ColorBarsProgressBar colorBarsProgressBar2;
        private WinForms.ColorBarsProgressBar colorBarsProgressBar3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button m_btnTestWPFMessageBox;
        private System.Windows.Forms.Button m_btnTestWPFMessageBoxWPF;
    }
}