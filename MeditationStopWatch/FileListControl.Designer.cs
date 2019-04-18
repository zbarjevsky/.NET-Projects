namespace MeditationStopWatch
{
    partial class FileListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileListControl));
            this.m_listFiles = new System.Windows.Forms.ListView();
            this.m_clmnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_clmnDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_clmnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuPause = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuNext = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripMenuSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolbarPlayer = new System.Windows.Forms.ToolStrip();
            this.m_toolStripButton_AddFiles = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_Remove = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_RemoveAll = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripButton_Up = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_Down = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_contextMenuStrip1.SuspendLayout();
            this.m_toolbarPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listFiles
            // 
            this.m_listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_clmnFileName,
            this.m_clmnDuration,
            this.m_clmnSize});
            this.m_listFiles.ContextMenuStrip = this.m_contextMenuStrip1;
            this.m_listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_listFiles.FullRowSelect = true;
            this.m_listFiles.GridLines = true;
            this.m_listFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listFiles.HideSelection = false;
            this.m_listFiles.Location = new System.Drawing.Point(0, 25);
            this.m_listFiles.Name = "m_listFiles";
            this.m_listFiles.ShowItemToolTips = true;
            this.m_listFiles.Size = new System.Drawing.Size(400, 124);
            this.m_listFiles.TabIndex = 5;
            this.m_listFiles.UseCompatibleStateImageBehavior = false;
            this.m_listFiles.View = System.Windows.Forms.View.Details;
            this.m_listFiles.SelectedIndexChanged += new System.EventHandler(this.m_listFiles_SelectedIndexChanged);
            this.m_listFiles.DoubleClick += new System.EventHandler(this.m_listFiles_DoubleClick);
            // 
            // m_clmnFileName
            // 
            this.m_clmnFileName.Text = "File name";
            this.m_clmnFileName.Width = 220;
            // 
            // m_clmnDuration
            // 
            this.m_clmnDuration.Text = "Duration";
            this.m_clmnDuration.Width = 70;
            // 
            // m_clmnSize
            // 
            this.m_clmnSize.Text = "Size";
            this.m_clmnSize.Width = 70;
            // 
            // m_contextMenuStrip1
            // 
            this.m_contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuPlay,
            this.m_mnuPause,
            this.m_mnuStop,
            this.m_toolStripMenuSep1,
            this.m_mnuPrev,
            this.m_mnuNext,
            this.m_toolStripMenuSep2,
            this.m_mnuAdd,
            this.m_mnuRemove,
            this.m_mnuRemoveAll,
            this.m_toolStripMenuSep3,
            this.m_mnuUp,
            this.m_mnuDown});
            this.m_contextMenuStrip1.Name = "m_contextMenuStrip1";
            this.m_contextMenuStrip1.Size = new System.Drawing.Size(135, 242);
            // 
            // m_mnuPlay
            // 
            this.m_mnuPlay.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuPlay.Image")));
            this.m_mnuPlay.Name = "m_mnuPlay";
            this.m_mnuPlay.Size = new System.Drawing.Size(134, 22);
            this.m_mnuPlay.Text = "Play";
            this.m_mnuPlay.Click += new System.EventHandler(this.m_mnuPlay_Click);
            // 
            // m_mnuPause
            // 
            this.m_mnuPause.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuPause.Image")));
            this.m_mnuPause.Name = "m_mnuPause";
            this.m_mnuPause.Size = new System.Drawing.Size(134, 22);
            this.m_mnuPause.Text = "Pause";
            this.m_mnuPause.Click += new System.EventHandler(this.m_mnuPause_Click);
            // 
            // m_mnuStop
            // 
            this.m_mnuStop.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuStop.Image")));
            this.m_mnuStop.Name = "m_mnuStop";
            this.m_mnuStop.Size = new System.Drawing.Size(134, 22);
            this.m_mnuStop.Text = "Stop";
            this.m_mnuStop.Click += new System.EventHandler(this.m_mnuStop_Click);
            // 
            // m_toolStripMenuSep1
            // 
            this.m_toolStripMenuSep1.Name = "m_toolStripMenuSep1";
            this.m_toolStripMenuSep1.Size = new System.Drawing.Size(131, 6);
            // 
            // m_mnuPrev
            // 
            this.m_mnuPrev.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuPrev.Image")));
            this.m_mnuPrev.Name = "m_mnuPrev";
            this.m_mnuPrev.Size = new System.Drawing.Size(134, 22);
            this.m_mnuPrev.Text = "Previous";
            this.m_mnuPrev.Click += new System.EventHandler(this.m_mnuPrev_Click);
            // 
            // m_mnuNext
            // 
            this.m_mnuNext.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuNext.Image")));
            this.m_mnuNext.Name = "m_mnuNext";
            this.m_mnuNext.Size = new System.Drawing.Size(134, 22);
            this.m_mnuNext.Text = "Next";
            this.m_mnuNext.Click += new System.EventHandler(this.m_mnuNext_Click);
            // 
            // m_toolStripMenuSep2
            // 
            this.m_toolStripMenuSep2.Name = "m_toolStripMenuSep2";
            this.m_toolStripMenuSep2.Size = new System.Drawing.Size(131, 6);
            // 
            // m_mnuAdd
            // 
            this.m_mnuAdd.Name = "m_mnuAdd";
            this.m_mnuAdd.Size = new System.Drawing.Size(134, 22);
            this.m_mnuAdd.Text = "Add Files";
            this.m_mnuAdd.Click += new System.EventHandler(this.m_toolStripButton_AddFiles_Click);
            // 
            // m_mnuRemove
            // 
            this.m_mnuRemove.Name = "m_mnuRemove";
            this.m_mnuRemove.Size = new System.Drawing.Size(134, 22);
            this.m_mnuRemove.Text = "Remove";
            this.m_mnuRemove.Click += new System.EventHandler(this.m_toolStripButton_Remove_Click);
            // 
            // m_mnuRemoveAll
            // 
            this.m_mnuRemoveAll.Name = "m_mnuRemoveAll";
            this.m_mnuRemoveAll.Size = new System.Drawing.Size(134, 22);
            this.m_mnuRemoveAll.Text = "Remove All";
            this.m_mnuRemoveAll.Click += new System.EventHandler(this.m_toolStripButton_RemoveAll_Click);
            // 
            // m_toolStripMenuSep3
            // 
            this.m_toolStripMenuSep3.Name = "m_toolStripMenuSep3";
            this.m_toolStripMenuSep3.Size = new System.Drawing.Size(131, 6);
            // 
            // m_mnuUp
            // 
            this.m_mnuUp.Name = "m_mnuUp";
            this.m_mnuUp.Size = new System.Drawing.Size(134, 22);
            this.m_mnuUp.Text = "Up";
            this.m_mnuUp.Click += new System.EventHandler(this.m_toolStripButton_Up_Click);
            // 
            // m_mnuDown
            // 
            this.m_mnuDown.Name = "m_mnuDown";
            this.m_mnuDown.Size = new System.Drawing.Size(134, 22);
            this.m_mnuDown.Text = "Down";
            this.m_mnuDown.Click += new System.EventHandler(this.m_toolStripButton_Down_Click);
            // 
            // m_toolbarPlayer
            // 
            this.m_toolbarPlayer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolbarPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripButton_AddFiles,
            this.m_toolStripButton_Remove,
            this.m_toolStripButton_RemoveAll,
            this.m_toolStripSeparator4,
            this.m_toolStripButton_Up,
            this.m_toolStripButton_Down});
            this.m_toolbarPlayer.Location = new System.Drawing.Point(0, 0);
            this.m_toolbarPlayer.Name = "m_toolbarPlayer";
            this.m_toolbarPlayer.Size = new System.Drawing.Size(400, 25);
            this.m_toolbarPlayer.TabIndex = 4;
            this.m_toolbarPlayer.Text = "toolStrip1";
            // 
            // m_toolStripButton_AddFiles
            // 
            this.m_toolStripButton_AddFiles.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_AddFiles.Image")));
            this.m_toolStripButton_AddFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_AddFiles.Name = "m_toolStripButton_AddFiles";
            this.m_toolStripButton_AddFiles.Size = new System.Drawing.Size(75, 22);
            this.m_toolStripButton_AddFiles.Text = "Add Files";
            this.m_toolStripButton_AddFiles.ToolTipText = "Add Files (Ins)";
            this.m_toolStripButton_AddFiles.Click += new System.EventHandler(this.m_toolStripButton_AddFiles_Click);
            // 
            // m_toolStripButton_Remove
            // 
            this.m_toolStripButton_Remove.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Remove.Image")));
            this.m_toolStripButton_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Remove.Name = "m_toolStripButton_Remove";
            this.m_toolStripButton_Remove.Size = new System.Drawing.Size(70, 22);
            this.m_toolStripButton_Remove.Text = "Remove";
            this.m_toolStripButton_Remove.ToolTipText = "Remove (Del)";
            this.m_toolStripButton_Remove.Click += new System.EventHandler(this.m_toolStripButton_Remove_Click);
            // 
            // m_toolStripButton_RemoveAll
            // 
            this.m_toolStripButton_RemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_RemoveAll.Image")));
            this.m_toolStripButton_RemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_RemoveAll.Name = "m_toolStripButton_RemoveAll";
            this.m_toolStripButton_RemoveAll.Size = new System.Drawing.Size(87, 22);
            this.m_toolStripButton_RemoveAll.Text = "Remove All";
            this.m_toolStripButton_RemoveAll.Click += new System.EventHandler(this.m_toolStripButton_RemoveAll_Click);
            // 
            // m_toolStripSeparator4
            // 
            this.m_toolStripSeparator4.Name = "m_toolStripSeparator4";
            this.m_toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // m_toolStripButton_Up
            // 
            this.m_toolStripButton_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Up.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Up.Image")));
            this.m_toolStripButton_Up.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Up.Name = "m_toolStripButton_Up";
            this.m_toolStripButton_Up.Size = new System.Drawing.Size(23, 22);
            this.m_toolStripButton_Up.Text = "Move file up in list";
            this.m_toolStripButton_Up.Click += new System.EventHandler(this.m_toolStripButton_Up_Click);
            // 
            // m_toolStripButton_Down
            // 
            this.m_toolStripButton_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Down.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Down.Image")));
            this.m_toolStripButton_Down.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Down.Name = "m_toolStripButton_Down";
            this.m_toolStripButton_Down.Size = new System.Drawing.Size(23, 22);
            this.m_toolStripButton_Down.Text = "Move file down in list";
            this.m_toolStripButton_Down.Click += new System.EventHandler(this.m_toolStripButton_Down_Click);
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.CheckFileExists = false;
            this.m_openFileDialog.DefaultExt = "mp3";
            this.m_openFileDialog.FileName = "*.mp3";
            this.m_openFileDialog.Filter = "Music Files(*.mp3)|*.mp3|All files|*.*";
            this.m_openFileDialog.Multiselect = true;
            this.m_openFileDialog.ValidateNames = false;
            // 
            // FileListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_listFiles);
            this.Controls.Add(this.m_toolbarPlayer);
            this.Name = "FileListControl";
            this.Size = new System.Drawing.Size(400, 149);
            this.Load += new System.EventHandler(this.FileListControl_Load);
            this.m_contextMenuStrip1.ResumeLayout(false);
            this.m_toolbarPlayer.ResumeLayout(false);
            this.m_toolbarPlayer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView m_listFiles;
        private System.Windows.Forms.ColumnHeader m_clmnFileName;
        private System.Windows.Forms.ColumnHeader m_clmnDuration;
        private System.Windows.Forms.ColumnHeader m_clmnSize;
        private System.Windows.Forms.ToolStrip m_toolbarPlayer;
        private System.Windows.Forms.ToolStripButton m_toolStripButton_AddFiles;
        private System.Windows.Forms.ToolStripButton m_toolStripButton_Remove;
        private System.Windows.Forms.ToolStripButton m_toolStripButton_RemoveAll;
        private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton m_toolStripButton_Up;
        private System.Windows.Forms.ToolStripButton m_toolStripButton_Down;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog m_openFileDialog;
        private System.Windows.Forms.ContextMenuStrip m_contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuPlay;
        private System.Windows.Forms.ToolStripMenuItem m_mnuPause;
        private System.Windows.Forms.ToolStripMenuItem m_mnuStop;
        private System.Windows.Forms.ToolStripSeparator m_toolStripMenuSep1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuPrev;
        private System.Windows.Forms.ToolStripMenuItem m_mnuNext;
        private System.Windows.Forms.ToolStripSeparator m_toolStripMenuSep2;
        private System.Windows.Forms.ToolStripMenuItem m_mnuAdd;
        private System.Windows.Forms.ToolStripMenuItem m_mnuRemove;
        private System.Windows.Forms.ToolStripMenuItem m_mnuRemoveAll;
        private System.Windows.Forms.ToolStripSeparator m_toolStripMenuSep3;
        private System.Windows.Forms.ToolStripMenuItem m_mnuUp;
        private System.Windows.Forms.ToolStripMenuItem m_mnuDown;
    }
}
