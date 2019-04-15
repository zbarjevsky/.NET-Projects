using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ClipboardManager
{
    public partial class FormSpyWindow
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();

                    if (_capturing)
                        this.CaptureMouse(false);
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpyWindow));
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_picFinder = new System.Windows.Forms.PictureBox();
            this.m_lblFinder = new System.Windows.Forms.Label();
            this.m_lblDescription = new System.Windows.Forms.Label();
            this.m_txtRect = new System.Windows.Forms.TextBox();
            this.m_txtStyle = new System.Windows.Forms.TextBox();
            this.m_lblRect = new System.Windows.Forms.Label();
            this.m_lblStyle = new System.Windows.Forms.Label();
            this.m_lblCaption = new System.Windows.Forms.Label();
            this.m_txtHandle = new System.Windows.Forms.TextBox();
            this.m_txtClass = new System.Windows.Forms.TextBox();
            this.m_lblHandle = new System.Windows.Forms.Label();
            this.m_txtCaption = new System.Windows.Forms.TextBox();
            this.m_lblClass = new System.Windows.Forms.Label();
            this.m_btnAdvanced = new System.Windows.Forms.Button();
            this.m_imageList_btnAdvanced = new System.Windows.Forms.ImageList(this.components);
            this.m_pnlAdvanced = new System.Windows.Forms.Panel();
            this.m_btBrowseProcess = new System.Windows.Forms.Button();
            this.m_txtProcess = new System.Windows.Forms.TextBox();
            this.m_lblProcess = new System.Windows.Forms.Label();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_sliderTransparency = new System.Windows.Forms.TrackBar();
            this.m_chkOnTop = new System.Windows.Forms.CheckBox();
            this.m_btnKillProcess = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_picFinder)).BeginInit();
            this.m_pnlAdvanced.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOK.Image")));
            this.m_btnOK.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_btnOK.Location = new System.Drawing.Point(166, 183);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(149, 23);
            this.m_btnOK.TabIndex = 7;
            this.m_btnOK.Text = "    Copy Text To Clipboard";
            this.m_btnOK.Click += new System.EventHandler(this.OnButtonOKClicked);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCancel.Location = new System.Drawing.Point(320, 183);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 8;
            this.m_btnCancel.Text = "    Cancel";
            this.m_btnCancel.Click += new System.EventHandler(this.OnButtonCancelClicked);
            // 
            // m_picFinder
            // 
            this.m_picFinder.Location = new System.Drawing.Point(85, 47);
            this.m_picFinder.Name = "m_picFinder";
            this.m_picFinder.Size = new System.Drawing.Size(32, 32);
            this.m_picFinder.TabIndex = 27;
            this.m_picFinder.TabStop = false;
            this.m_picFinder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnFinderToolMouseDown);
            // 
            // m_lblFinder
            // 
            this.m_lblFinder.Location = new System.Drawing.Point(9, 56);
            this.m_lblFinder.Name = "m_lblFinder";
            this.m_lblFinder.Size = new System.Drawing.Size(70, 20);
            this.m_lblFinder.TabIndex = 1;
            this.m_lblFinder.Text = "Finder Tool:";
            // 
            // m_lblDescription
            // 
            this.m_lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblDescription.Location = new System.Drawing.Point(10, 9);
            this.m_lblDescription.Name = "m_lblDescription";
            this.m_lblDescription.Size = new System.Drawing.Size(392, 42);
            this.m_lblDescription.TabIndex = 0;
            this.m_lblDescription.Text = "Drag the Finder Tool over a window to select it, then release the mouse button. O" +
    "r enter a window handle (in hexadecimal).";
            // 
            // m_txtRect
            // 
            this.m_txtRect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRect.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtRect.Location = new System.Drawing.Point(64, 114);
            this.m_txtRect.Name = "m_txtRect";
            this.m_txtRect.ReadOnly = true;
            this.m_txtRect.Size = new System.Drawing.Size(332, 20);
            this.m_txtRect.TabIndex = 7;
            // 
            // m_txtStyle
            // 
            this.m_txtStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtStyle.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtStyle.Location = new System.Drawing.Point(64, 89);
            this.m_txtStyle.Name = "m_txtStyle";
            this.m_txtStyle.ReadOnly = true;
            this.m_txtStyle.Size = new System.Drawing.Size(332, 20);
            this.m_txtStyle.TabIndex = 5;
            // 
            // m_lblRect
            // 
            this.m_lblRect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblRect.Location = new System.Drawing.Point(9, 116);
            this.m_lblRect.Name = "m_lblRect";
            this.m_lblRect.Size = new System.Drawing.Size(55, 20);
            this.m_lblRect.TabIndex = 6;
            this.m_lblRect.Text = "Rect:";
            // 
            // m_lblStyle
            // 
            this.m_lblStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblStyle.Location = new System.Drawing.Point(9, 91);
            this.m_lblStyle.Name = "m_lblStyle";
            this.m_lblStyle.Size = new System.Drawing.Size(55, 20);
            this.m_lblStyle.TabIndex = 4;
            this.m_lblStyle.Text = "Style:";
            // 
            // m_lblCaption
            // 
            this.m_lblCaption.Location = new System.Drawing.Point(9, 81);
            this.m_lblCaption.Name = "m_lblCaption";
            this.m_lblCaption.Size = new System.Drawing.Size(55, 20);
            this.m_lblCaption.TabIndex = 4;
            this.m_lblCaption.Text = "Text:";
            // 
            // m_txtHandle
            // 
            this.m_txtHandle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtHandle.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtHandle.Location = new System.Drawing.Point(64, 39);
            this.m_txtHandle.Name = "m_txtHandle";
            this.m_txtHandle.ReadOnly = true;
            this.m_txtHandle.Size = new System.Drawing.Size(332, 20);
            this.m_txtHandle.TabIndex = 1;
            // 
            // m_txtClass
            // 
            this.m_txtClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtClass.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtClass.Location = new System.Drawing.Point(64, 64);
            this.m_txtClass.Name = "m_txtClass";
            this.m_txtClass.ReadOnly = true;
            this.m_txtClass.Size = new System.Drawing.Size(332, 20);
            this.m_txtClass.TabIndex = 3;
            // 
            // m_lblHandle
            // 
            this.m_lblHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblHandle.Location = new System.Drawing.Point(9, 40);
            this.m_lblHandle.Name = "m_lblHandle";
            this.m_lblHandle.Size = new System.Drawing.Size(55, 20);
            this.m_lblHandle.TabIndex = 0;
            this.m_lblHandle.Text = "Handle:";
            // 
            // m_txtCaption
            // 
            this.m_txtCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCaption.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtCaption.Location = new System.Drawing.Point(12, 104);
            this.m_txtCaption.Multiline = true;
            this.m_txtCaption.Name = "m_txtCaption";
            this.m_txtCaption.ReadOnly = true;
            this.m_txtCaption.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtCaption.Size = new System.Drawing.Size(384, 73);
            this.m_txtCaption.TabIndex = 5;
            // 
            // m_lblClass
            // 
            this.m_lblClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblClass.Location = new System.Drawing.Point(9, 66);
            this.m_lblClass.Name = "m_lblClass";
            this.m_lblClass.Size = new System.Drawing.Size(55, 20);
            this.m_lblClass.TabIndex = 2;
            this.m_lblClass.Text = "Class:";
            // 
            // m_btnAdvanced
            // 
            this.m_btnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnAdvanced.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.m_btnAdvanced.ImageIndex = 1;
            this.m_btnAdvanced.ImageList = this.m_imageList_btnAdvanced;
            this.m_btnAdvanced.Location = new System.Drawing.Point(11, 183);
            this.m_btnAdvanced.Name = "m_btnAdvanced";
            this.m_btnAdvanced.Size = new System.Drawing.Size(91, 23);
            this.m_btnAdvanced.TabIndex = 6;
            this.m_btnAdvanced.Text = "Advanced    ";
            this.m_btnAdvanced.UseVisualStyleBackColor = true;
            this.m_btnAdvanced.Click += new System.EventHandler(this.m_btnAdvanced_Click);
            // 
            // m_imageList_btnAdvanced
            // 
            this.m_imageList_btnAdvanced.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList_btnAdvanced.ImageStream")));
            this.m_imageList_btnAdvanced.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageList_btnAdvanced.Images.SetKeyName(0, "arrow-down_16.ico");
            this.m_imageList_btnAdvanced.Images.SetKeyName(1, "arrow-up_16.ico");
            // 
            // m_pnlAdvanced
            // 
            this.m_pnlAdvanced.Controls.Add(this.m_btnKillProcess);
            this.m_pnlAdvanced.Controls.Add(this.m_btBrowseProcess);
            this.m_pnlAdvanced.Controls.Add(this.m_txtProcess);
            this.m_pnlAdvanced.Controls.Add(this.m_lblProcess);
            this.m_pnlAdvanced.Controls.Add(this.m_txtHandle);
            this.m_pnlAdvanced.Controls.Add(this.m_lblClass);
            this.m_pnlAdvanced.Controls.Add(this.m_lblHandle);
            this.m_pnlAdvanced.Controls.Add(this.m_txtClass);
            this.m_pnlAdvanced.Controls.Add(this.m_lblStyle);
            this.m_pnlAdvanced.Controls.Add(this.m_txtRect);
            this.m_pnlAdvanced.Controls.Add(this.m_lblRect);
            this.m_pnlAdvanced.Controls.Add(this.m_txtStyle);
            this.m_pnlAdvanced.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlAdvanced.Location = new System.Drawing.Point(0, 214);
            this.m_pnlAdvanced.Name = "m_pnlAdvanced";
            this.m_pnlAdvanced.Size = new System.Drawing.Size(409, 152);
            this.m_pnlAdvanced.TabIndex = 1;
            // 
            // m_btBrowseProcess
            // 
            this.m_btBrowseProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btBrowseProcess.Location = new System.Drawing.Point(348, 12);
            this.m_btBrowseProcess.Name = "m_btBrowseProcess";
            this.m_btBrowseProcess.Size = new System.Drawing.Size(23, 23);
            this.m_btBrowseProcess.TabIndex = 10;
            this.m_btBrowseProcess.Text = "...";
            this.toolTip1.SetToolTip(this.m_btBrowseProcess, "Open Process Location");
            this.m_btBrowseProcess.UseVisualStyleBackColor = true;
            this.m_btBrowseProcess.Click += new System.EventHandler(this.m_btBrowseProcess_Click);
            // 
            // m_txtProcess
            // 
            this.m_txtProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtProcess.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtProcess.Location = new System.Drawing.Point(64, 14);
            this.m_txtProcess.Name = "m_txtProcess";
            this.m_txtProcess.ReadOnly = true;
            this.m_txtProcess.Size = new System.Drawing.Size(281, 20);
            this.m_txtProcess.TabIndex = 9;
            // 
            // m_lblProcess
            // 
            this.m_lblProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lblProcess.Location = new System.Drawing.Point(8, 14);
            this.m_lblProcess.Name = "m_lblProcess";
            this.m_lblProcess.Size = new System.Drawing.Size(55, 20);
            this.m_lblProcess.TabIndex = 8;
            this.m_lblProcess.Text = "Process:";
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Controls.Add(this.m_sliderTransparency);
            this.m_pnlMain.Controls.Add(this.m_picFinder);
            this.m_pnlMain.Controls.Add(this.m_chkOnTop);
            this.m_pnlMain.Controls.Add(this.m_lblDescription);
            this.m_pnlMain.Controls.Add(this.m_lblFinder);
            this.m_pnlMain.Controls.Add(this.m_btnAdvanced);
            this.m_pnlMain.Controls.Add(this.m_txtCaption);
            this.m_pnlMain.Controls.Add(this.m_lblCaption);
            this.m_pnlMain.Controls.Add(this.m_btnOK);
            this.m_pnlMain.Controls.Add(this.m_btnCancel);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(409, 214);
            this.m_pnlMain.TabIndex = 0;
            // 
            // m_sliderTransparency
            // 
            this.m_sliderTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sliderTransparency.Location = new System.Drawing.Point(135, 49);
            this.m_sliderTransparency.Maximum = 99;
            this.m_sliderTransparency.Minimum = 50;
            this.m_sliderTransparency.Name = "m_sliderTransparency";
            this.m_sliderTransparency.Size = new System.Drawing.Size(155, 45);
            this.m_sliderTransparency.TabIndex = 2;
            this.m_sliderTransparency.TabStop = false;
            this.m_sliderTransparency.TickFrequency = 10;
            this.m_sliderTransparency.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.m_sliderTransparency.Value = 97;
            this.m_sliderTransparency.Scroll += new System.EventHandler(this.m_sliderTransparency_Scroll);
            // 
            // m_chkOnTop
            // 
            this.m_chkOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkOnTop.AutoSize = true;
            this.m_chkOnTop.Checked = true;
            this.m_chkOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkOnTop.Location = new System.Drawing.Point(311, 56);
            this.m_chkOnTop.Name = "m_chkOnTop";
            this.m_chkOnTop.Size = new System.Drawing.Size(84, 17);
            this.m_chkOnTop.TabIndex = 3;
            this.m_chkOnTop.Text = "Stay on Top";
            this.m_chkOnTop.UseVisualStyleBackColor = true;
            this.m_chkOnTop.CheckedChanged += new System.EventHandler(this.m_chkOnTop_CheckedChanged);
            // 
            // m_btnKillProcess
            // 
            this.m_btnKillProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnKillProcess.Image = ((System.Drawing.Image)(resources.GetObject("m_btnKillProcess.Image")));
            this.m_btnKillProcess.Location = new System.Drawing.Point(372, 12);
            this.m_btnKillProcess.Name = "m_btnKillProcess";
            this.m_btnKillProcess.Size = new System.Drawing.Size(23, 23);
            this.m_btnKillProcess.TabIndex = 11;
            this.toolTip1.SetToolTip(this.m_btnKillProcess, "Kill the Process");
            this.m_btnKillProcess.UseVisualStyleBackColor = true;
            this.m_btnKillProcess.Click += new System.EventHandler(this.m_btnKillProcess_Click);
            // 
            // FormSpyWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(409, 366);
            this.Controls.Add(this.m_pnlMain);
            this.Controls.Add(this.m_pnlAdvanced);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 50);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 350);
            this.Name = "FormSpyWindow";
            this.Opacity = 0.97D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Finder Tool";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormSpyWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_picFinder)).EndInit();
            this.m_pnlAdvanced.ResumeLayout(false);
            this.m_pnlAdvanced.PerformLayout();
            this.m_pnlMain.ResumeLayout(false);
            this.m_pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderTransparency)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Button m_btnOK;
        private Button m_btnCancel;
        private PictureBox m_picFinder;
        private Label m_lblFinder;
        private Label m_lblDescription;
        private TextBox m_txtRect;
        private TextBox m_txtStyle;
        private Label m_lblRect;
        private Label m_lblStyle;
        private Label m_lblCaption;
        private TextBox m_txtHandle;
        private TextBox m_txtClass;
        private Label m_lblHandle;
        private TextBox m_txtCaption;
        private Label m_lblClass;
        private Button m_btnAdvanced;
        private ImageList m_imageList_btnAdvanced;
        private Panel m_pnlAdvanced;
        private Panel m_pnlMain;
        private CheckBox m_chkOnTop;
        private TrackBar m_sliderTransparency;
        private System.Windows.Forms.TextBox m_txtProcess;
        private System.Windows.Forms.Label m_lblProcess;
        private System.Windows.Forms.Button m_btBrowseProcess;
        private Button m_btnKillProcess;
        private ToolTip toolTip1;
    }
}
