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
            this.m_btnTestEdit = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_cmbListViewType = new System.Windows.Forms.ComboBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.colorBarsProgressBar2 = new MZ.Controls.ColorBarsProgressBar();
            this.foldersTreeUserControl1 = new MZ.ControlsWinForms.FoldersTreeUserControl();
            this.fileExplorerUserControl1 = new MZ.ControlsWinForms.FileExplorerUserControl();
            this.colorBarsProgressBar1 = new MZ.Controls.ColorBarsProgressBar();
            this.colorBarsProgressBar3 = new MZ.Controls.ColorBarsProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnTestEdit
            // 
            this.m_btnTestEdit.Location = new System.Drawing.Point(235, 21);
            this.m_btnTestEdit.Name = "m_btnTestEdit";
            this.m_btnTestEdit.Size = new System.Drawing.Size(144, 116);
            this.m_btnTestEdit.TabIndex = 1;
            this.m_btnTestEdit.Text = "Test In-Place-Edit Box";
            this.m_btnTestEdit.UseVisualStyleBackColor = true;
            this.m_btnTestEdit.Click += new System.EventHandler(this.m_btnTestEdit_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(64, 181);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(326, 236);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(396, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.foldersTreeUserControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileExplorerUserControl1);
            this.splitContainer1.Size = new System.Drawing.Size(654, 405);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 6;
            // 
            // m_cmbListViewType
            // 
            this.m_cmbListViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbListViewType.FormattingEnabled = true;
            this.m_cmbListViewType.Location = new System.Drawing.Point(64, 154);
            this.m_cmbListViewType.Name = "m_cmbListViewType";
            this.m_cmbListViewType.Size = new System.Drawing.Size(121, 21);
            this.m_cmbListViewType.TabIndex = 7;
            this.m_cmbListViewType.SelectedIndexChanged += new System.EventHandler(this.m_cmbListViewType_SelectedIndexChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(64, 34);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 104);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 60;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // colorBarsProgressBar2
            // 
            this.colorBarsProgressBar2.ActiveColor = System.Drawing.Color.DarkBlue;
            this.colorBarsProgressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorBarsProgressBar2.ColorThemeType = MZ.Controls.ColorBarsProgressBar.ColorsThemeType.Regular;
            this.colorBarsProgressBar2.InactiveBarsColorType = MZ.Controls.ColorBarsProgressBar.InactiveColorType.Mono;
            this.colorBarsProgressBar2.InactiveColor = System.Drawing.Color.Gainsboro;
            this.colorBarsProgressBar2.Location = new System.Drawing.Point(26, 427);
            this.colorBarsProgressBar2.Name = "colorBarsProgressBar2";
            this.colorBarsProgressBar2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.colorBarsProgressBar2.Size = new System.Drawing.Size(1024, 27);
            this.colorBarsProgressBar2.TabIndex = 9;
            this.colorBarsProgressBar2.TabStop = false;
            this.colorBarsProgressBar2.Value = 60;
            // 
            // foldersTreeUserControl1
            // 
            this.foldersTreeUserControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.foldersTreeUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foldersTreeUserControl1.Location = new System.Drawing.Point(0, 0);
            this.foldersTreeUserControl1.Name = "foldersTreeUserControl1";
            this.foldersTreeUserControl1.Size = new System.Drawing.Size(218, 405);
            this.foldersTreeUserControl1.TabIndex = 2;
            // 
            // fileExplorerUserControl1
            // 
            this.fileExplorerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileExplorerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.fileExplorerUserControl1.Name = "fileExplorerUserControl1";
            this.fileExplorerUserControl1.Size = new System.Drawing.Size(432, 405);
            this.fileExplorerUserControl1.TabIndex = 3;
            // 
            // colorBarsProgressBar1
            // 
            this.colorBarsProgressBar1.ActiveColor = System.Drawing.Color.LimeGreen;
            this.colorBarsProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.colorBarsProgressBar1.ColorThemeType = MZ.Controls.ColorBarsProgressBar.ColorsThemeType.Microphone;
            this.colorBarsProgressBar1.InactiveBarsColorType = MZ.Controls.ColorBarsProgressBar.InactiveColorType.Mono;
            this.colorBarsProgressBar1.InactiveColor = System.Drawing.Color.PaleGoldenrod;
            this.colorBarsProgressBar1.Location = new System.Drawing.Point(32, 34);
            this.colorBarsProgressBar1.Name = "colorBarsProgressBar1";
            this.colorBarsProgressBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar1.Size = new System.Drawing.Size(21, 367);
            this.colorBarsProgressBar1.TabIndex = 0;
            this.colorBarsProgressBar1.TabStop = false;
            this.colorBarsProgressBar1.Value = 60;
            // 
            // colorBarsProgressBar3
            // 
            this.colorBarsProgressBar3.ActiveColor = System.Drawing.Color.LimeGreen;
            this.colorBarsProgressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.colorBarsProgressBar3.ColorThemeType = MZ.Controls.ColorBarsProgressBar.ColorsThemeType.Speaker;
            this.colorBarsProgressBar3.InactiveBarsColorType = MZ.Controls.ColorBarsProgressBar.InactiveColorType.Mono;
            this.colorBarsProgressBar3.InactiveColor = System.Drawing.Color.Honeydew;
            this.colorBarsProgressBar3.Location = new System.Drawing.Point(5, 34);
            this.colorBarsProgressBar3.Name = "colorBarsProgressBar3";
            this.colorBarsProgressBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar3.Size = new System.Drawing.Size(21, 367);
            this.colorBarsProgressBar3.TabIndex = 10;
            this.colorBarsProgressBar3.TabStop = false;
            this.colorBarsProgressBar3.Value = 60;
            // 
            // FormMainTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 464);
            this.Controls.Add(this.colorBarsProgressBar3);
            this.Controls.Add(this.colorBarsProgressBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.m_cmbListViewType);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.m_btnTestEdit);
            this.Controls.Add(this.colorBarsProgressBar1);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ColorBarsProgressBar colorBarsProgressBar1;
        private System.Windows.Forms.Button m_btnTestEdit;
        private ControlsWinForms.FoldersTreeUserControl foldersTreeUserControl1;
        private ControlsWinForms.FileExplorerUserControl fileExplorerUserControl1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox m_cmbListViewType;
        private System.Windows.Forms.TrackBar trackBar1;
        private Controls.ColorBarsProgressBar colorBarsProgressBar2;
        private Controls.ColorBarsProgressBar colorBarsProgressBar3;
    }
}