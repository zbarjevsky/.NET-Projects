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
            this.foldersTreeUserControl1 = new MZ.ControlsWinForms.FoldersTreeUserControl();
            this.colorBarsProgressBar1 = new MZ.Controls.ColorBarsProgressBar();
            this.fileExplorerUserControl1 = new MZ.ControlsWinForms.FileExplorerUserControl();
            this.m_btnBrowseForFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.colorBarsProgressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnTestEdit
            // 
            this.m_btnTestEdit.Location = new System.Drawing.Point(99, 13);
            this.m_btnTestEdit.Name = "m_btnTestEdit";
            this.m_btnTestEdit.Size = new System.Drawing.Size(144, 116);
            this.m_btnTestEdit.TabIndex = 1;
            this.m_btnTestEdit.Text = "Test In-Place-Edit Box";
            this.m_btnTestEdit.UseVisualStyleBackColor = true;
            this.m_btnTestEdit.Click += new System.EventHandler(this.m_btnTestEdit_Click);
            // 
            // foldersTreeUserControl1
            // 
            this.foldersTreeUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.foldersTreeUserControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.foldersTreeUserControl1.Location = new System.Drawing.Point(267, 13);
            this.foldersTreeUserControl1.Name = "foldersTreeUserControl1";
            this.foldersTreeUserControl1.Size = new System.Drawing.Size(236, 407);
            this.foldersTreeUserControl1.TabIndex = 2;
            // 
            // colorBarsProgressBar1
            // 
            this.colorBarsProgressBar1.ActiveColor = System.Drawing.Color.LimeGreen;
            this.colorBarsProgressBar1.ActiveColorTheme = MZ.Controls.ColorBarsProgressBar.ActiveColorsTheme.Multicolor;
            this.colorBarsProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.colorBarsProgressBar1.InactiveColorTheme = MZ.Controls.ColorBarsProgressBar.InactiveColorsTheme.Pale;
            this.colorBarsProgressBar1.Location = new System.Drawing.Point(26, 34);
            this.colorBarsProgressBar1.Maximum = 100;
            this.colorBarsProgressBar1.Minimum = 0;
            this.colorBarsProgressBar1.Name = "colorBarsProgressBar1";
            this.colorBarsProgressBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar1.Size = new System.Drawing.Size(21, 353);
            this.colorBarsProgressBar1.TabIndex = 0;
            this.colorBarsProgressBar1.TabStop = false;
            this.colorBarsProgressBar1.Value = 65;
            // 
            // fileExplorerUserControl1
            // 
            this.fileExplorerUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileExplorerUserControl1.Location = new System.Drawing.Point(509, 13);
            this.fileExplorerUserControl1.Name = "fileExplorerUserControl1";
            this.fileExplorerUserControl1.Size = new System.Drawing.Size(534, 408);
            this.fileExplorerUserControl1.TabIndex = 3;
            // 
            // m_btnBrowseForFolder
            // 
            this.m_btnBrowseForFolder.Location = new System.Drawing.Point(99, 152);
            this.m_btnBrowseForFolder.Name = "m_btnBrowseForFolder";
            this.m_btnBrowseForFolder.Size = new System.Drawing.Size(144, 23);
            this.m_btnBrowseForFolder.TabIndex = 4;
            this.m_btnBrowseForFolder.Text = "Browse For Folder";
            this.m_btnBrowseForFolder.UseVisualStyleBackColor = true;
            this.m_btnBrowseForFolder.Click += new System.EventHandler(this.m_btnBrowseForFolder_Click);
            // 
            // FormMainTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 450);
            this.Controls.Add(this.m_btnBrowseForFolder);
            this.Controls.Add(this.fileExplorerUserControl1);
            this.Controls.Add(this.foldersTreeUserControl1);
            this.Controls.Add(this.m_btnTestEdit);
            this.Controls.Add(this.colorBarsProgressBar1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormMainTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Main Test";
            this.Load += new System.EventHandler(this.FormMainTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.colorBarsProgressBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ColorBarsProgressBar colorBarsProgressBar1;
        private System.Windows.Forms.Button m_btnTestEdit;
        private ControlsWinForms.FoldersTreeUserControl foldersTreeUserControl1;
        private ControlsWinForms.FileExplorerUserControl fileExplorerUserControl1;
        private System.Windows.Forms.Button m_btnBrowseForFolder;
    }
}