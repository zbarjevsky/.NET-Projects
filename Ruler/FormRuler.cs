using System;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Interop;

namespace RulerApplication {
	public class FormRuler : System.Windows.Forms.Form {

		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItemFlip;
		private System.Windows.Forms.MenuItem menuItemSeparator1;
		private System.Windows.Forms.MenuItem menuItemPixel;
		private System.Windows.Forms.MenuItem menuItemInch;
		private System.Windows.Forms.MenuItem menuItemCentimeter;
		private System.Windows.Forms.MenuItem menuItemSeparator2;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private PictureBox m_pic;
		private ToolTip m_ToolTip;
		private System.Windows.Forms.MenuItem menuItemExit;

		public FormRuler() {
			//
			// Required for Windows Form Designer support
			//
			this.InitializeComponent();

			this.size = new Size(this.Width, this.Width);
			this.pen = new Pen(Color.Black, float.Epsilon);
			this.format = new StringFormat(StringFormat.GenericTypographic);
			this.format.FormatFlags = StringFormatFlags.NoWrap;
			this.format.Trimming = StringTrimming.Character;

            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        }

		private void Ruler_Load(object sender, System.EventArgs e) {
			this.ContextMenu = this.contextMenu;
			this.Horizontal = true;
		}

		private IContainer components;

		#region Windows Form Designer generated code


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRuler));
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemFlip = new System.Windows.Forms.MenuItem();
            this.menuItemSeparator1 = new System.Windows.Forms.MenuItem();
            this.menuItemPixel = new System.Windows.Forms.MenuItem();
            this.menuItemInch = new System.Windows.Forms.MenuItem();
            this.menuItemCentimeter = new System.Windows.Forms.MenuItem();
            this.menuItemSeparator2 = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.m_pic = new System.Windows.Forms.PictureBox();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFlip,
            this.menuItemSeparator1,
            this.menuItemPixel,
            this.menuItemInch,
            this.menuItemCentimeter,
            this.menuItemSeparator2,
            this.menuItemAbout,
            this.menuItemExit});
            // 
            // menuItemFlip
            // 
            this.menuItemFlip.Index = 0;
            this.menuItemFlip.Text = "Flip Ruler";
            this.menuItemFlip.Click += new System.EventHandler(this.menuItemFlip_Click);
            // 
            // menuItemSeparator1
            // 
            this.menuItemSeparator1.Index = 1;
            this.menuItemSeparator1.Text = "-";
            // 
            // menuItemPixel
            // 
            this.menuItemPixel.Checked = true;
            this.menuItemPixel.Index = 2;
            this.menuItemPixel.Text = "Pixels";
            this.menuItemPixel.Click += new System.EventHandler(this.menuItemPixel_Click);
            // 
            // menuItemInch
            // 
            this.menuItemInch.Index = 3;
            this.menuItemInch.Text = "Inches";
            this.menuItemInch.Click += new System.EventHandler(this.menuItemInch_Click);
            // 
            // menuItemCentimeter
            // 
            this.menuItemCentimeter.Index = 4;
            this.menuItemCentimeter.Text = "Centimeters";
            this.menuItemCentimeter.Click += new System.EventHandler(this.menuItemCentimeter_Click);
            // 
            // menuItemSeparator2
            // 
            this.menuItemSeparator2.Index = 5;
            this.menuItemSeparator2.Text = "-";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 6;
            this.menuItemAbout.Text = "About Ruler...";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 7;
            this.menuItemExit.Text = "Exit Ruler";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // m_pic
            // 
            this.m_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pic.Location = new System.Drawing.Point(0, 0);
            this.m_pic.Name = "m_pic";
            this.m_pic.Size = new System.Drawing.Size(400, 45);
            this.m_pic.TabIndex = 0;
            this.m_pic.TabStop = false;
            this.m_pic.Paint += new System.Windows.Forms.PaintEventHandler(this.Ruler_Paint);
            this.m_pic.DoubleClick += new System.EventHandler(this.menuItemFlip_Click);
            this.m_pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ruler_MouseDown);
            this.m_pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ruler_MouseMove);
            this.m_pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Ruler_MouseUp);
            // 
            // FormRuler
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(400, 45);
            this.Controls.Add(this.m_pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(45, 45);
            this.Name = "FormRuler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ruler";
            this.m_ToolTip.SetToolTip(this, "Double click to change orientation");
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Ruler_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ruler_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.m_pic)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.Run(new FormRuler());
		}

		//---------------------------------------------------------------------

		private Size size;

		private void EnsureVisible() {
			Rectangle screen = Screen.FromControl(this).Bounds;
			Rectangle ruler = this.Bounds;
			Rectangle r = Rectangle.Intersect(screen, ruler);
			int w = this.MinimumSize.Width / 2;
			if(r.Width < w) {
				this.Location = new Point(
					Math.Max(screen.X - ruler.Width + w, Math.Min(ruler.X, screen.Right - w)),
					this.Location.Y
				);
			}
			int h = this.MinimumSize.Height / 2;
			if(r.Height < h) {
				this.Location = new Point(
					this.Location.X,
					Math.Max(screen.Y - ruler.Height + h, Math.Min(ruler.Y, screen.Bottom - h))
				);
			}
		}

		private bool horizontal;
		private bool Horizontal {
			get { return this.horizontal; }
			set {
				this.horizontal = value;
				if(this.horizontal) {
					this.Size = new Size(this.size.Width, this.MinimumSize.Height);
				} else {
					this.Size = new Size(this.MinimumSize.Width, this.size.Height);
				}
				this.EnsureVisible();
			}
		}

		//---------------------------------------------------------------------

		private Point movePoint;
		private bool isMoving = false;
		private bool isLeftSizing = false;
		private bool isRightSizing = false;
		private bool isTopSizing = false;
		private bool isBottomSizing = false;

		private void Ruler_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Clicks <= 1 && e.Button == MouseButtons.Left) {
				if(this.Horizontal) {
					if(e.X <= 3) {
						this.isLeftSizing = m_pic.Capture = true;
					} else if(e.X >= this.Width - 3) {
						this.isRightSizing = m_pic.Capture = true;
					} else {
						this.isMoving = m_pic.Capture = true;
					}
				} else {
					if(e.Y <= 3) {
						this.isTopSizing = m_pic.Capture = true;
					} else if(e.Y >= this.Height - 3) {
						this.isBottomSizing = m_pic.Capture = true;
					} else {
						this.isMoving = m_pic.Capture = true;
					}
				}
				this.movePoint = this.PointToScreen(new Point(e.X, e.Y));
			}
		}
		private void Ruler_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (m_pic.Capture && e.Button == MouseButtons.Left)
			{
				this.isMoving =
				this.isLeftSizing =
				this.isRightSizing =
				this.isTopSizing =
				this.isBottomSizing =
				m_pic.Capture = false;
				this.EnsureVisible();
				this.Invalidate();
			}
		}
		private void Ruler_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (m_pic.Capture)
			{
				Point p = this.PointToScreen(new Point(e.X, e.Y));
				Rectangle ruler = this.Bounds;
				Size min = this.MinimumSize;
				if(this.isMoving) {
					this.Location = new Point(
						ruler.X + p.X - this.movePoint.X,
						ruler.Y + p.Y - this.movePoint.Y
					);
				} else if(this.isLeftSizing) {
					this.Bounds = new Rectangle(
						ruler.X + p.X - this.movePoint.X,
						ruler.Y,
						ruler.Width - p.X + this.movePoint.X,
						min.Height
					);
					this.size.Width = this.Width;
				} else if(this.isRightSizing) {
					this.Size = new Size(ruler.Width + p.X - this.movePoint.X, ruler.Height);
					this.size.Width = this.Width;
				} else if(this.isTopSizing) {
					this.Bounds = new Rectangle(
						ruler.X,
						ruler.Y + p.Y - this.movePoint.Y,
						min.Width,
						ruler.Height - p.Y + this.movePoint.Y
					);
					this.size.Height = this.Height;
				} else if(this.isBottomSizing) {
					this.Size = new Size(min.Width, ruler.Height + p.Y - this.movePoint.Y);
					this.size.Height = this.Height;
				}
				this.movePoint = p;
				m_pic.Refresh();
			} else {
				if(this.Horizontal) {
					if(e.X <= 3 || e.X >= this.Width - 3) {
						this.Cursor = Cursors.SizeWE;
					} else {
						this.Cursor = Cursors.Default;
					}
				} else {
					if(e.Y <= 3 || e.Y >= this.Height - 3) {
						this.Cursor = Cursors.SizeNS;
					} else {
						this.Cursor = Cursors.Default;
					}
				}
			}
		}

		private void Ruler_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			int step = e.Shift ? 10 : 1;
			if(e.KeyCode == Keys.Left) {
				if(e.Control && this.Horizontal) {
					this.Width -= step;
					this.size.Width = this.Width;
				} else {
					this.Location = new Point(this.Location.X - step, this.Location.Y);
				}
				this.EnsureVisible();
				this.Invalidate();
			} else if(e.KeyCode == Keys.Right) {
				if(e.Control && this.Horizontal) {
					this.Width += step;
					this.size.Width = this.Width;
				} else {
					this.Location = new Point(this.Location.X + step, this.Location.Y);
				}
				this.EnsureVisible();
				this.Invalidate();
			} else if(e.KeyCode == Keys.Up) {
				if(e.Control && !this.Horizontal) {
					this.Height -= step;
					this.size.Height = this.Height;
				} else {
					this.Location = new Point(this.Location.X, this.Location.Y - step);
				}
				this.EnsureVisible();
				this.Invalidate();
			} else if(e.KeyCode == Keys.Down) {
				if(e.Control && !this.Horizontal) {
					this.Height += step;
					this.size.Height = this.Height;
				} else {
					this.Location = new Point(this.Location.X, this.Location.Y + step);
				}
				this.EnsureVisible();
				this.Invalidate();
			}
		}

		//---------------------------------------------------------------------

		private void menuItemFlip_Click(object sender, System.EventArgs e) {
			this.Horizontal = !this.Horizontal;
			m_pic.Invalidate();
		}
		private void menuItemPixel_Click(object sender, System.EventArgs e) {
			this.menuItemPixel.Checked = true;
			this.menuItemInch.Checked = false;
			this.menuItemCentimeter.Checked = false;
			m_pic.Invalidate();
		}
		private void menuItemInch_Click(object sender, System.EventArgs e) {
			this.menuItemPixel.Checked = false;
			this.menuItemInch.Checked = true;
			this.menuItemCentimeter.Checked = false;
			m_pic.Invalidate();
		}
		private void menuItemCentimeter_Click(object sender, System.EventArgs e) {
			this.menuItemPixel.Checked = false;
			this.menuItemInch.Checked = false;
			this.menuItemCentimeter.Checked = true;
			m_pic.Invalidate();
		}
		private void menuItemAbout_Click(object sender, System.EventArgs e) {
			using(About dlg = new About()) {
				dlg.ShowDialog(this);
			}
		}
		private void menuItemExit_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		//---------------------------------------------------------------------

		private Pen pen;
		private StringFormat format;
		private void Ruler_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
			int scale;
			int step;
			int small;
			int big;
			int number;
			string unit;
			if(this.menuItemPixel.Checked) {
				step = 5;
				small = 10;
				big = 50;
				number = 100;
				scale = 1;
				unit = " Pixels";
			} else if(this.menuItemInch.Checked) {
				g.PageUnit = GraphicsUnit.Inch;
				g.PageScale = 1f / 12f;
				step = 1;
				small = 2;
				big = 8;
				number = 16;
				scale = 16;
				unit = "\"";
			} else { //Cm.
				g.PageUnit = GraphicsUnit.Millimeter;
				g.PageScale = 1f;
				step = 1;
				small = 5;
				big = 10;
				number = 10;
				scale = 10;
				unit = " Cm.";
			}
			PointF[] point = new PointF[] {
				new PointF(2, 2), new PointF(5, 5), new Point(this.Size), this.Location
			};
			g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, point);
			float infoDelta = this.Horizontal ? point[0].Y : point[0].X;
			float stroke = this.Horizontal ? point[1].Y : point[1].X;
			int length = (int)(point[2].X + point[2].Y);

			if(!this.Horizontal) {
				g.RotateTransform(90, MatrixOrder.Prepend);
				g.TranslateTransform(point[2].X, 0, MatrixOrder.Append);
			}

			for(int i = 0; i < length; i += step) {
				float d = 1;
				if(i % small == 0) {
					if(i % big == 0) {
						d = 3;
					} else {
						d = 2;
					}
				}
				g.DrawLine(this.pen, i, 0f, i, d * stroke);
				if((i % number) == 0) {
					string text = (i / scale).ToString(CultureInfo.InvariantCulture);
					SizeF size = g.MeasureString(text, this.Font, length, this.format);
					g.DrawString(text, this.Font, Brushes.Black, i - size.Width / 2, d * stroke, this.format);
				}
			}
			string info = string.Format(CultureInfo.InvariantCulture,
				"X={0} Y={1} Length={2}{3}",
				Math.Round(point[3].X / scale, 1),
				Math.Round(point[3].Y / scale, 1),
				Math.Round((float)(this.Horizontal ? point[2].X : point[2].Y) / scale, 1),
				unit
			);
			SizeF infoSize = g.MeasureString(info, this.Font, length, this.format);
			float y = (float)(this.Horizontal ? point[2].Y : point[2].X);
			float x = (float)(this.Horizontal ? point[2].X : point[2].Y);
			float txtX = (y - infoSize.Height)/2;
			float txtY = y - infoSize.Height - infoDelta;
			g.DrawString(info, this.Font, Brushes.Black, txtX, txtY, this.format);

			if (infoSize.Width + 10 > x)
				this.m_ToolTip.SetToolTip(m_pic, info);
			else
				this.m_ToolTip.SetToolTip(m_pic, "");
            this.Text = info + " - Ruler";

			//System.Diagnostics.Debug.WriteLine(info);
		}
	}
}
