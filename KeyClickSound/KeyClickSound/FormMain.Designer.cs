namespace KeyClickSound
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            m_chkSoundOn = new CheckBox();
            m_btnBrowse = new Button();
            m_splitMain = new SplitContainer();
            m_btnClear = new Button();
            m_btnPlay = new Button();
            m_listKeys = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)m_splitMain).BeginInit();
            m_splitMain.Panel1.SuspendLayout();
            m_splitMain.Panel2.SuspendLayout();
            m_splitMain.SuspendLayout();
            SuspendLayout();
            // 
            // m_chkSoundOn
            // 
            m_chkSoundOn.AutoSize = true;
            m_chkSoundOn.Location = new Point(24, 15);
            m_chkSoundOn.Name = "m_chkSoundOn";
            m_chkSoundOn.Size = new Size(108, 19);
            m_chkSoundOn.TabIndex = 0;
            m_chkSoundOn.Text = "Sound On Click";
            m_chkSoundOn.UseVisualStyleBackColor = true;
            m_chkSoundOn.CheckedChanged += m_chkSoundOn_CheckedChanged;
            // 
            // m_btnBrowse
            // 
            m_btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            m_btnBrowse.Image = (Image)resources.GetObject("m_btnBrowse.Image");
            m_btnBrowse.ImageAlign = ContentAlignment.MiddleLeft;
            m_btnBrowse.Location = new Point(456, 12);
            m_btnBrowse.Name = "m_btnBrowse";
            m_btnBrowse.Size = new Size(170, 23);
            m_btnBrowse.TabIndex = 1;
            m_btnBrowse.Text = "Select Sound File...";
            m_btnBrowse.UseVisualStyleBackColor = true;
            m_btnBrowse.Click += m_btnBrowse_Click;
            // 
            // m_splitMain
            // 
            m_splitMain.Dock = DockStyle.Fill;
            m_splitMain.Location = new Point(0, 0);
            m_splitMain.Name = "m_splitMain";
            m_splitMain.Orientation = Orientation.Horizontal;
            // 
            // m_splitMain.Panel1
            // 
            m_splitMain.Panel1.Controls.Add(m_btnClear);
            m_splitMain.Panel1.Controls.Add(m_btnPlay);
            m_splitMain.Panel1.Controls.Add(m_chkSoundOn);
            m_splitMain.Panel1.Controls.Add(m_btnBrowse);
            // 
            // m_splitMain.Panel2
            // 
            m_splitMain.Panel2.Controls.Add(m_listKeys);
            m_splitMain.Size = new Size(800, 450);
            m_splitMain.SplitterDistance = 45;
            m_splitMain.TabIndex = 2;
            // 
            // m_btnClear
            // 
            m_btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            m_btnClear.Image = (Image)resources.GetObject("m_btnClear.Image");
            m_btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            m_btnClear.Location = new Point(713, 12);
            m_btnClear.Name = "m_btnClear";
            m_btnClear.Size = new Size(75, 23);
            m_btnClear.TabIndex = 3;
            m_btnClear.Text = "Clear";
            m_btnClear.UseVisualStyleBackColor = true;
            m_btnClear.Click += m_btnClear_Click;
            // 
            // m_btnPlay
            // 
            m_btnPlay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            m_btnPlay.Image = (Image)resources.GetObject("m_btnPlay.Image");
            m_btnPlay.ImageAlign = ContentAlignment.MiddleLeft;
            m_btnPlay.Location = new Point(632, 12);
            m_btnPlay.Name = "m_btnPlay";
            m_btnPlay.Size = new Size(75, 23);
            m_btnPlay.TabIndex = 2;
            m_btnPlay.Text = "Play";
            m_btnPlay.UseVisualStyleBackColor = true;
            m_btnPlay.Click += m_btnPlay_Click;
            // 
            // m_listKeys
            // 
            m_listKeys.AllowDrop = true;
            m_listKeys.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            m_listKeys.Dock = DockStyle.Fill;
            m_listKeys.FullRowSelect = true;
            m_listKeys.GridLines = true;
            m_listKeys.Location = new Point(0, 0);
            m_listKeys.Name = "m_listKeys";
            m_listKeys.Size = new Size(800, 401);
            m_listKeys.TabIndex = 0;
            m_listKeys.UseCompatibleStateImageBehavior = false;
            m_listKeys.View = View.Details;
            m_listKeys.SelectedIndexChanged += m_listKeys_SelectedIndexChanged;
            m_listKeys.DragDrop += m_listKeys_DragDrop;
            m_listKeys.DragEnter += m_listKeys_DragEnter;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Key";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Sound File Name";
            columnHeader2.Width = 736;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(m_splitMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(640, 480);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Select Sound On-Click";
            FormClosed += FormMain_FormClosed;
            Load += FormMain_Load;
            m_splitMain.Panel1.ResumeLayout(false);
            m_splitMain.Panel1.PerformLayout();
            m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_splitMain).EndInit();
            m_splitMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CheckBox m_chkSoundOn;
        private Button m_btnBrowse;
        private SplitContainer m_splitMain;
        private ListView m_listKeys;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button m_btnClear;
        private Button m_btnPlay;
    }
}
