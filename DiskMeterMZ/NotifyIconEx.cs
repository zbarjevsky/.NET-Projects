namespace MZ
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(MZ.NotifyIconEx), "MZ.NotifyIcon.bmp"), DefaultEvent("MouseDown"), DefaultProperty("Text"), ToolboxItemFilter("DUMeterMZ")]
    public sealed class NotifyIconEx : Component
    {
        // Events
        [Category("Action"), Description("Occurs when the balloon is dismissed because of a mouse click.")]
        public event MZ.NotifyIconEx.BalloonClickEventHandler BalloonClick;
        [Category("Behavior"), Description("Occurs when the balloon disappears\u2014for example, when the icon is deleted. This message is not sent if the balloon is dismissed because of a timeout or a mouse click.")]
        public event MZ.NotifyIconEx.BalloonHideEventHandler BalloonHide;
        [Description("Occurs when the balloon is shown (balloons are queued)."), Category("Behavior")]
        public event MZ.NotifyIconEx.BalloonShowEventHandler BalloonShow;
        [Category("Behavior"), Description("Occurs when the balloon is dismissed because of a timeout.")]
        public event MZ.NotifyIconEx.BalloonTimeoutEventHandler BalloonTimeout;
        [Description("Occurs when the user clicks the icon in the status area."), Category("Action")]
        public event MZ.NotifyIconEx.ClickEventHandler Click;
        [Description("Occurs when the user double-clicks the icon in the status notification area of the taskbar."), Category("Action")]
        public event MZ.NotifyIconEx.DoubleClickEventHandler DoubleClick;
        [Description("Occurs when the user presses the mouse button while the pointer is over the icon in the status notification area of the taskbar."), Category("Mouse")]
        public event MZ.NotifyIconEx.MouseDownEventHandler MouseDown;
        [Category("Mouse"), Description("Occurs when the user moves the mouse while the pointer is over the icon in the status notification area of the taskbar.")]
        public event MZ.NotifyIconEx.MouseMoveEventHandler MouseMove;
        [Description("Occurs when the user releases the mouse button while the pointer is over the icon in the status notification area of the taskbar."), Category("Mouse")]
        public event MZ.NotifyIconEx.MouseUpEventHandler MouseUp;

        // Methods
        public NotifyIconEx()
        {
            this.Messages = new MessageHandler();
            this.InitializeComponent();
            this.Initialize();
        }

        public NotifyIconEx(IContainer Container) : this()
        {
            Container.Add(this);
            this.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Visible = false;
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void Initialize()
        {
            this.NID.hwnd = this.Messages.Handle;
            this.NID.cbSize = Marshal.SizeOf(typeof(MZ.NotifyIconEx.NOTIFYICONDATA));
            this.NID.uFlags = 7;
            this.NID.uCallbackMessage = 0x401;
            this.NID.uVersion = 5;
            this.NID.szTip = "";
            this.NID.uID = 0;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
        }

        private void Messages_BalloonClick(object sender)
        {
            if (!this.m_VisibleBeforeBalloon)
            {
                this.Visible = false;
            }
            if (this.BalloonClick != null)
            {
                this.BalloonClick(this);
            }
        }

        private void Messages_BalloonHide(object sender)
        {
            if (this.BalloonHide != null)
            {
                this.BalloonHide(this);
            }
        }

        private void Messages_BalloonShow(object sender)
        {
            if (this.BalloonShow != null)
            {
                this.BalloonShow(this);
            }
        }

        private void Messages_BalloonTimeout(object sender)
        {
            if (!this.m_VisibleBeforeBalloon)
            {
                this.Visible = false;
            }
            if (this.BalloonTimeout != null)
            {
                this.BalloonTimeout(this);
            }
        }

        private void Messages_Click(object sender, EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        private void Messages_DoubleClick(object sender, EventArgs e)
        {
            if (this.DoubleClick != null)
            {
                this.DoubleClick(this, e);
            }
        }

        private void Messages_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.MouseDown != null)
            {
                this.MouseDown(this, e);
            }
        }

        private void Messages_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.MouseMove != null)
            {
                this.MouseMove(this, e);
            }
        }

        private void Messages_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.MouseUp != null)
            {
                this.MouseUp(this, e);
            }
            if (e.Button == MouseButtons.Right)
            {
                this.Messages.Activate();
                Point point7 = new Point(0, 0);
                Point point6 = this.Messages.PointToScreen(point7);
                Point point4 = new Point(0, 0);
                Point point3 = this.Messages.PointToScreen(point4);
                Point point2 = new Point(Cursor.Position.X - point6.X, Cursor.Position.Y - point3.Y);
                Point point1 = point2;
                if (this.m_ContextMenu != null)
                {
                    this.m_ContextMenu.Show(this.Messages, point1);
                }
            }
        }

        private void Messages_Reload()
        {
            if (this.Visible)
            {
                this.Visible = true;
            }
        }

        [DllImport("Shell32", CharSet=CharSet.Auto)]
        private static extern bool Shell_NotifyIcon(int dwMessage, ref MZ.NotifyIconEx.NOTIFYICONDATA lpData);

        public void ShowBalloon(EBalloonIcon Icon, string Text, string Title, [Optional] int Timeout /* = 0x3a98 */)
        {
            this.m_VisibleBeforeBalloon = this.m_Visible;
            this.NID.uFlags |= 0x10;
            this.NID.uVersion = Timeout;
            this.NID.szInfo = Text;
            this.NID.szInfoTitle = Title;
            this.NID.dwInfoFlags = Convert.ToInt32((int) Icon);
            if (!this.Visible)
            {
                this.Visible = true;
            }
            else
            {
                MZ.NotifyIconEx.Shell_NotifyIcon(1, ref this.NID);
            }
            this.NID.uFlags &= -17;
        }


        // Properties
        [Category("Behavior"), Description("The pop-up menu to show when the user right-clicks the icon."), DefaultValue("")]
        public System.Windows.Forms.ContextMenu ContextMenu
        {
            get
            {
                return this.m_ContextMenu;
            }
            set
            {
                this.m_ContextMenu = value;
            }
        }

        [Category("Appearance"), Description("The icon to display in the system tray."), DefaultValue("")]
        public System.Drawing.Icon Icon
        {
            get
            {
                return this.m_Icon;
            }
            set
            {
                this.m_Icon = value;
                this.NID.uFlags |= 2;
                this.NID.hIcon = this.Icon.Handle;
                if (this.Visible)
                {
                    MZ.NotifyIconEx.Shell_NotifyIcon(1, ref this.NID);
                }
            }
        }

        private MessageHandler Messages
        {
            get
            {
                return this._Messages;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (this._Messages != null)
                {
                    this._Messages.BalloonClick -= new MessageHandler.BalloonClickEventHandler(this.Messages_BalloonClick);
                    this._Messages.BalloonTimeout -= new MessageHandler.BalloonTimeoutEventHandler(this.Messages_BalloonTimeout);
                    this._Messages.BalloonHide -= new MessageHandler.BalloonHideEventHandler(this.Messages_BalloonHide);
                    this._Messages.BalloonShow -= new MessageHandler.BalloonShowEventHandler(this.Messages_BalloonShow);
                    this._Messages.Reload -= new MessageHandler.ReloadEventHandler(this.Messages_Reload);
                    this._Messages.MouseUp -= new MessageHandler.MouseUpEventHandler(this.Messages_MouseUp);
                    this._Messages.MouseMove -= new MessageHandler.MouseMoveEventHandler(this.Messages_MouseMove);
                    this._Messages.MouseDown -= new MessageHandler.MouseDownEventHandler(this.Messages_MouseDown);
                    this._Messages.DoubleClick -= new MessageHandler.DoubleClickEventHandler(this.Messages_DoubleClick);
                    this._Messages.Click -= new MessageHandler.ClickEventHandler(this.Messages_Click);
                }
                this._Messages = value;
                if (this._Messages != null)
                {
                    this._Messages.BalloonClick += new MessageHandler.BalloonClickEventHandler(this.Messages_BalloonClick);
                    this._Messages.BalloonTimeout += new MessageHandler.BalloonTimeoutEventHandler(this.Messages_BalloonTimeout);
                    this._Messages.BalloonHide += new MessageHandler.BalloonHideEventHandler(this.Messages_BalloonHide);
                    this._Messages.BalloonShow += new MessageHandler.BalloonShowEventHandler(this.Messages_BalloonShow);
                    this._Messages.Reload += new MessageHandler.ReloadEventHandler(this.Messages_Reload);
                    this._Messages.MouseUp += new MessageHandler.MouseUpEventHandler(this.Messages_MouseUp);
                    this._Messages.MouseMove += new MessageHandler.MouseMoveEventHandler(this.Messages_MouseMove);
                    this._Messages.MouseDown += new MessageHandler.MouseDownEventHandler(this.Messages_MouseDown);
                    this._Messages.DoubleClick += new MessageHandler.DoubleClickEventHandler(this.Messages_DoubleClick);
                    this._Messages.Click += new MessageHandler.ClickEventHandler(this.Messages_Click);
                }
            }
        }

        [Description("The text that will be displayed when the mouse hovers over the icon."), Category("Appearance")]
        public string Text
        {
            get
            {
                return this.NID.szTip;
            }
            set
            {
                this.NID.szTip = value;
                if (this.Visible)
                {
                    this.NID.uFlags |= 4;
                    MZ.NotifyIconEx.Shell_NotifyIcon(1, ref this.NID);
                }
            }
        }

        [DefaultValue(false), Description("Determines whether the control is visible or hidden."), Category("Behavior")]
        public bool Visible
        {
            get
            {
                return this.m_Visible;
            }
            set
            {
                this.m_Visible = value;
                if (!this.DesignMode)
                {
                    if (this.m_Visible)
                    {
                        MZ.NotifyIconEx.Shell_NotifyIcon(0, ref this.NID);
                    }
                    else
                    {
                        MZ.NotifyIconEx.Shell_NotifyIcon(2, ref this.NID);
                    }
                }
            }
        }


        // Fields
        [AccessedThroughProperty("Messages")]
        private MessageHandler _Messages;
        private IContainer components;
        private System.Windows.Forms.ContextMenu m_ContextMenu;
        private System.Drawing.Icon m_Icon;
        private bool m_Visible;
        private bool m_VisibleBeforeBalloon;
        private MZ.NotifyIconEx.NOTIFYICONDATA NID;
        private const int NIF_ICON = 2;
        private const int NIF_INFO = 0x10;
        private const int NIF_MESSAGE = 1;
        private const int NIF_STATE = 8;
        private const int NIF_TIP = 4;
        private const int NIM_ADD = 0;
        private const int NIM_DELETE = 2;
        private const int NIM_MODIFY = 1;
        private const int NIM_SETVERSION = 4;
        private const int NOTIFYICON_VERSION = 5;
        private const int WM_USER = 0x400;
        private const int WM_USER_TRAY = 0x401;

        // Nested Types
        public delegate void BalloonClickEventHandler(object sender);


        public delegate void BalloonHideEventHandler(object sender);


        public delegate void BalloonShowEventHandler(object sender);


        public delegate void BalloonTimeoutEventHandler(object sender);


        public delegate void ClickEventHandler(object sender, EventArgs e);


        public delegate void DoubleClickEventHandler(object sender, EventArgs e);


        public enum EBalloonIcon
        {
            None,
            Info,
            Warning,
            Error
        }

        private class MessageHandler : Form
        {
            // Events
            public event MZ.NotifyIconEx.MessageHandler.BalloonClickEventHandler BalloonClick;
            public event MZ.NotifyIconEx.MessageHandler.BalloonHideEventHandler BalloonHide;
            public event MZ.NotifyIconEx.MessageHandler.BalloonShowEventHandler BalloonShow;
            public event MZ.NotifyIconEx.MessageHandler.BalloonTimeoutEventHandler BalloonTimeout;
            public new event MZ.NotifyIconEx.MessageHandler.ClickEventHandler Click;
            public new event MZ.NotifyIconEx.MessageHandler.DoubleClickEventHandler DoubleClick;
            public event MZ.NotifyIconEx.MessageHandler.MouseDownEventHandler MouseDown;
            public event MZ.NotifyIconEx.MessageHandler.MouseMoveEventHandler MouseMove;
            public event MZ.NotifyIconEx.MessageHandler.MouseUpEventHandler MouseUp;
            public event ReloadEventHandler Reload;

            // Methods
            public MessageHandler()
            {
                this.WM_TASKBARCREATED = MZ.NotifyIconEx.MessageHandler.RegisterWindowMessage("TaskbarCreated");
                this.ShowInTaskbar = false;
                this.StartPosition = FormStartPosition.Manual;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                Size size1 = new Size(100, 100);
                this.Size = size1;
                Point point1 = new Point(-500, -500);
                this.Location = point1;
                this.Show();
            }

            [DllImport("User32", CharSet=CharSet.Auto)]
            private static extern int RegisterWindowMessage(string lpString);

            protected override void WndProc(ref Message m)
            {
                int num2 = m.Msg;
                if (num2 == 0x401)
                {
                    int num1 = m.LParam.ToInt32();
                    if (((num1 == 0x203) || (num1 == 0x206)) || (num1 == 0x209))
                    {
                        if (this.DoubleClick != null)
                        {
                            this.DoubleClick(this, new MouseEventArgs(Control.MouseButtons, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                    }
                    else if (((num1 == 0x201) || (num1 == 0x204)) || (num1 == 0x207))
                    {
                        if (this.MouseDown != null)
                        {
                            this.MouseDown(this, new MouseEventArgs(Control.MouseButtons, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                    }
                    else if (num1 == 0x200)
                    {
                        if (this.MouseMove != null)
                        {
                            this.MouseMove(this, new MouseEventArgs(Control.MouseButtons, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                    }
                    else if (num1 == 0x202)
                    {
                        if (this.MouseUp != null)
                        {
                            this.MouseUp(this, new MouseEventArgs(MouseButtons.Left, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                        if (this.Click != null)
                        {
                            this.Click(this, new MouseEventArgs(MouseButtons.Left, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                    }
                    else if (num1 == 0x205)
                    {
                        if (this.MouseUp != null)
                        {
                            this.MouseUp(this, new MouseEventArgs(MouseButtons.Right, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                        if (this.Click != null)
                        {
                            this.Click(this, new MouseEventArgs(MouseButtons.Right, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                    }
                    else if (num1 == 520)
                    {
                        if (this.MouseUp != null)
                        {
                            this.MouseUp(this, new MouseEventArgs(MouseButtons.Middle, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                        if (this.Click != null)
                        {
                            this.Click(this, new MouseEventArgs(MouseButtons.Middle, 0, Control.MousePosition.X, Control.MousePosition.Y, 0));
                        }
                    }
                    else if (num1 == 0x402)
                    {
                        if (this.BalloonShow != null)
                        {
                            this.BalloonShow(this);
                        }
                    }
                    else if (num1 == 0x403)
                    {
                        if (this.BalloonHide != null)
                        {
                            this.BalloonHide(this);
                        }
                    }
                    else if (num1 == 0x404)
                    {
                        if (this.BalloonTimeout != null)
                        {
                            this.BalloonTimeout(this);
                        }
                    }
                    else if ((num1 == 0x405) && (this.BalloonClick != null))
                    {
                        this.BalloonClick(this);
                    }
                }
                else if ((num2 == this.WM_TASKBARCREATED) && (this.Reload != null))
                {
                    this.Reload();
                }
                base.WndProc(ref m);
            }


            // Fields
            private const int NIN_BALLOONHIDE = 0x403;
            private const int NIN_BALLOONSHOW = 0x402;
            private const int NIN_BALLOONTIMEOUT = 0x404;
            private const int NIN_BALLOONUSERCLICK = 0x405;
            private const int WM_LBUTTONDBLCLK = 0x203;
            private const int WM_LBUTTONDOWN = 0x201;
            private const int WM_LBUTTONUP = 0x202;
            private const int WM_MBUTTONDBLCLK = 0x209;
            private const int WM_MBUTTONDOWN = 0x207;
            private const int WM_MBUTTONUP = 520;
            private const int WM_MOUSEMOVE = 0x200;
            private const int WM_RBUTTONDBLCLK = 0x206;
            private const int WM_RBUTTONDOWN = 0x204;
            private const int WM_RBUTTONUP = 0x205;
            private int WM_TASKBARCREATED;
            private const int WM_USER = 0x400;
            private const int WM_USER_TRAY = 0x401;

            // Nested Types
            public delegate void BalloonClickEventHandler(object sender);


            public delegate void BalloonHideEventHandler(object sender);


            public delegate void BalloonShowEventHandler(object sender);


            public delegate void BalloonTimeoutEventHandler(object sender);


            public delegate void ClickEventHandler(object sender, EventArgs e);


            public delegate void DoubleClickEventHandler(object sender, EventArgs e);


            public delegate void MouseDownEventHandler(object sender, MouseEventArgs e);


            public delegate void MouseMoveEventHandler(object sender, MouseEventArgs e);


            public delegate void MouseUpEventHandler(object sender, MouseEventArgs e);


            public delegate void ReloadEventHandler();

        }//end class MessageHandler

        public delegate void MouseDownEventHandler(object sender, MouseEventArgs e);


        public delegate void MouseMoveEventHandler(object sender, MouseEventArgs e);


        public delegate void MouseUpEventHandler(object sender, MouseEventArgs e);


        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        private struct NOTIFYICONDATA
        {
            public int cbSize;
            public IntPtr hwnd;
            public int uID;
            public int uFlags;
            public int uCallbackMessage;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x80)]
            public string szTip;
            public int dwState;
            public int dwStateMask;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x100)]
            public string szInfo;
            public int uVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x40)]
            public string szInfoTitle;
            public int dwInfoFlags;
        }//end struct NOTIFYICONDATA
    }//end class NotifyIcon
}//end namespace MZ

