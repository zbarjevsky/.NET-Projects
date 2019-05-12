namespace MeditationStopWatch.Tools
{
    partial class LabelWithTimeout
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
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 330;
            this.m_Timer.Tick += new System.EventHandler(this.m_Timer_Tick);
            // 
            // LabelWithTimeout
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Maroon;
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(100, 23);
            this.VisibleChanged += new System.EventHandler(this.LabelWithTimeout_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer m_Timer;
    }
}
