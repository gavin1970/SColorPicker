using System;
using ColorPicker;
using Chizl.Utils;
using System.Drawing;
using SColorPicker.utils;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace SColorPicker
{
    public partial class FrmMain : Form
    {
        private class ColorSpan
        {
            const int m_spanDiff = 5;
            private int RED { get; } = -1;
            private int GREEN { get; } = -1;
            private int BLUE { get; } = -1;

            public ColorSpan(Color c)
            {
                RED = c.R; GREEN = c.G; BLUE = c.B;
            }
            public char GetPrime()
            {
                char retVal = 'R';
                int val = RED;
                if (GREEN > val) { val = GREEN; retVal = 'G'; }
                if (BLUE > val) { retVal = 'B'; }
                return retVal;
            }
            public Color[] GetUpperSpan(int span)
            {
                Color[] retVal = new Color[span];

                char prime = GetPrime();
                int r = RED;
                int g = GREEN;
                int b = BLUE;
                int spanDiff = (int)((double)span * m_spanDiff);
                int halfSpan = (int)(spanDiff / 2);

                for (int i = 0; i < span; i++)
                {
                    r = prime == 'R' ? r + halfSpan : r + span;
                    g = prime == 'G' ? g + halfSpan : g + span;
                    b = prime == 'B' ? b + halfSpan : b + span;
                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;

                    retVal[i] = Color.FromArgb(r, g, b);
                }

                return retVal;
            }
            public Color[] GetLowerSpan(int span)
            {
                Color[] retVal = new Color[span];

                char prime = GetPrime();
                int r = RED;
                int g = GREEN;
                int b = BLUE;
                int spanDiff = (int)((double)span * m_spanDiff);
                int halfSpan = (int)(spanDiff / 2);

                for (int i = 0; i < span; i++)
                {
                    r = prime == 'R' ? r - halfSpan : r - span;
                    g = prime == 'G' ? g - halfSpan : g - span;
                    b = prime == 'B' ? b - halfSpan : b - span;
                    
                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;

                    retVal[i] = Color.FromArgb(r, g, b);
                }

                return retVal;
            }
        }

        const int m_tipRotationWait = 10000;
        const int m_borderSize = 2;
        const double m_wheelLightness = 0.5;
        readonly List<PointF> m_path = new List<PointF>();
        readonly List<Color> m_colors = new List<Color>();

        private PickerTip Tips { get; set; }
        private FrmLens ZoomLens { get; set; } = null;
        private Point CursorPosition { get; set; }
        private Rectangle DefaultFormSetup { get; set; }
        private Size DefaultFormSize { get; set; } = new Size(220, 267);

        public FrmMain()
        {
            InitializeComponent();

            Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;

            //only need padding during Design, because I can't click on panel without removing DOCK on the labelTip.
            this.panelTip.Padding = new Padding(0);
            //align tool tip
            SetPanelTip(titleHeight);

            DefaultFormSize = new Size(this.panelGroup.Width, this.panelGroup.Height + (titleHeight * 2));
            this.Size = DefaultFormSize;
        }

        private void SetupTips()
        {
            Tips = new PickerTip(m_tipRotationWait);
            Tips.PickerTipEvent += (sender, e) =>
            {
                string msg = e.Tip;
                if (InvokeRequired) 
                {
                    LabelTip.Invoke(new Action(() => {
                        LabelTip.Text = msg;
                    }));
                }
                else
                    LabelTip.Text = msg;
            };
        }
        private void CenterTheApp()
        {
            int i = 0;
            int w = Screen.AllScreens[i++].WorkingArea.Width;
            while (this.Left > w)
                w += Screen.AllScreens[i++].WorkingArea.Width;

            int curWidth = Screen.AllScreens[--i].WorkingArea.Width;
            if (w != curWidth)
                curWidth = w - (curWidth / 2);
            else
                curWidth /= 2;

            this.Left = curWidth - (this.Width / 2);
            this.Top = (Screen.AllScreens[i].WorkingArea.Height / 2) - (this.Height / 2);
        }
        private void SelectColorClicked(object sender, EventArgs e)
        {
            CursorPosition = Cursor.Position;
            LblXY.Text = $"X:{CursorPosition.X}\nY:{CursorPosition.Y}";

            this.GbColor.BackColor = GetColorAt(CursorPosition.X, CursorPosition.Y);

            this.TxtRScoll.Text = this.GbColor.BackColor.R.ToString();
            this.TxtGScroll.Text = this.GbColor.BackColor.G.ToString();
            this.TxtBScroll.Text = this.GbColor.BackColor.B.ToString();

            SetSpan();
        }
        private void SetPanelTip(int tipHeight)
        {
            //while moving.
            this.panelTip.Visible = false;
            //int w = this.BackgroundImage == null ? this.panelGroup.Width : this.panelGroup.Width - 8;
            int w = this.panelGroup.Width;

            Point loc = new Point(this.panelGroup.Left, this.panelGroup.Top + (this.panelGroup.Height - 5));
            Size sz = new Size(w, tipHeight);

            this.panelTip.Location = loc;
            this.panelTip.Size = sz;

            //turn it back on
            this.panelTip.Visible = true;
        }
        private void StopFindColor()
        {
            this.timer.Enabled = false;
            if (this.FormBorderStyle != FormBorderStyle.FixedToolWindow)
            {
                this.Hide();
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Location = DefaultFormSetup.Location;
                this.Size = DefaultFormSize;
                this.panelGroup.Top = 0;

                this.BackgroundImage = null;
                this.Cursor = Cursors.Default;
                this.Show();

                this.BringToFront();
                this.Focus();
                this.BtnPick.Enabled = true;
                this.BtnCopy.Visible = true;

                this.SetPanelTip(this.panelTip.Height);
            }
        }
        private void SetSpan()
        {
            this.GbColor.Text = $"{GetFormatColor()}".Replace("Color [A=255, ", "").Replace("]", "");
            SetColorSpan(new ColorSpan(this.GbColor.BackColor));
        }
        private void SetColorSpan(ColorSpan org)
        {
            if (org == null)
                return;

            Color[] cu = org.GetUpperSpan(5);
            Color[] cl = org.GetLowerSpan(5);

            for (int i = 0; i < 5; i++)
            {
                //if (panelSpanBottom.Controls.Contains)
                Control[] tExists = this.Controls.Find($"tspan{i}", true);
                Control[] bExists = this.Controls.Find($"bspan{i}", true);
                if (tExists.Length == 0)
                {
                    Panel pu = new Panel
                    {
                        Size = new Size(46, 20),
                        Dock = DockStyle.Left,
                        Name = $"tspan{i}",
                        BackColor = cu[i],
                        Cursor = Cursors.Hand
                    };
                    pu.Click += SelectColorClicked;
                    panelSpanTop.Controls.Add(pu);
                }
                else
                    tExists[0].BackColor = cu[i];

                if (bExists.Length == 0)
                {
                    Panel pl = new Panel
                    {
                        Size = new Size(46, 20),
                        Dock = DockStyle.Left,
                        Name = $"bspan{i}",
                        BackColor = cl[i],
                        Cursor = Cursors.Hand
                    };
                    pl.Click += SelectColorClicked;
                    panelSpanBottom.Controls.Add(pl);
                }
                else
                    bExists[0].BackColor = cl[i];
            }
        }
        private Color GetFormatColor()
        {
            if (!int.TryParse(this.TxtRScoll.Text.ToString(), out int r))
                return Color.Empty;
            if (!int.TryParse(this.TxtGScroll.Text.ToString(), out int g))
                return Color.Empty;
            if (!int.TryParse(this.TxtBScroll.Text.ToString(), out int b))
                return Color.Empty;

            Color fg = Color.Black;

            //if ((r < 128 || b < 128 || r + b < 128) && g > 0)
            //if (r + g + b < ((255 * 3) / 2) && g < 125)
            if (((r + g + b) / 3) < 127 && g < 127)
                fg = Color.White;

            this.GbColor.ForeColor = fg;

            //if (r + g + b < ((255 * 3) / 2))
            //    this.GbColor.ForeColor = Color.White;
            //else
            //    this.GbColor.ForeColor = Color.Black;

            return Color.FromArgb((int)((byte)(r)), (int)((byte)(g)), (int)((byte)(b)));
        }
        private void SetupScollText()
        {
            for(int i=255; i>=0; i--)
            {
                this.TxtRScoll.Items.Add(i.ToString());
                this.TxtGScroll.Items.Add(i.ToString());
                this.TxtBScroll.Items.Add(i.ToString());
            }

            this.TxtRScoll.SelectedIndex = 0;
            this.TxtGScroll.SelectedIndex = 0;
            this.TxtBScroll.SelectedIndex = 0;
        }
        private void CalcWheelPoints()
        {
            m_path.Clear();
            m_colors.Clear();

            PointF center = Center(ColorWheelRectangle);
            float radius = Radius(ColorWheelRectangle);
            double angle = 0;
            double fullcircle = 360;
            double step = 5;
            while (angle < fullcircle)
            {
                double angleR = angle * (Math.PI / 180);
                double x = center.X + Math.Cos(angleR) * radius;
                double y = center.Y - Math.Sin(angleR) * radius;
                m_path.Add(new PointF((float)x, (float)y));
                m_colors.Add(new HSLColor(angle, 1, m_wheelLightness).Color);
                angle += step;
            }
        }
        private Rectangle CwClientRectangle
        {
            get
            {
                int tPadding = 50;
                int lPadding = 20;
                int deduct = -50;
                Rectangle r = gBColorWheel.ClientRectangle;
                int sizeWithMargin = r.Width < r.Height ? r.Width + deduct : r.Height + deduct;

                r.Y += tPadding;
                r.X += lPadding;
                r.Width = sizeWithMargin;
                r.Height = sizeWithMargin;

                return r;
            }
        }
        RectangleF WheelRectangle
        {
            get
            {
                Rectangle r = CwClientRectangle;
                r.Width -= 1;
                r.Height -= 1;
                return r;
            }
        }
        RectangleF ColorWheelRectangle
        {
            get
            {
                RectangleF r = WheelRectangle;
                r.Inflate(-5, -5);
                return r;
            }
        }
        private float Radius(RectangleF r)
        {
            return Math.Min((r.Width / 2), (r.Height / 2));
        }
        private Rectangle Rect(RectangleF rf)
        {
            Rectangle r = new Rectangle
            {
                X = (int)rf.X,
                Y = (int)rf.Y,
                Width = (int)rf.Width,
                Height = (int)rf.Height
            };
            return r;
        }
        private PointF Center(RectangleF r)
        {
            PointF center = r.Location;
            center.X += r.Width / 2;
            center.Y += r.Height / 2;
            return center;
        }
        private void SetTitlebar()
        {
            this.Text = $"{AppInfo.Title} v{AppInfo.FileVersion}";
        }
        private void SetTxtScrollTextColor(DomainUpDown dUpDown)
        {
            if(string.IsNullOrWhiteSpace(dUpDown.Text))
            {
                MessageBox.Show($"Value is required.   Only enter: 0 - 255", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(dUpDown.Text, out int c) || c < 0 || c > 255)
            {
                MessageBox.Show($"{c} is an invalid entry.   Only enter: 0 - 255", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int r = dUpDown.Name.Contains("R") ? 255 : 255 - c;
            int g = dUpDown.Name.Contains("G") ? 255 : 255 - c;
            int b = dUpDown.Name.Contains("B") ? 255 : 255 - c;

            dUpDown.BackColor = Color.FromArgb(r, g, b);

            //int rgb = r + g + b;

            Color fg = Color.Black;
            //if (!dUpDown.Name.Contains("G") && rgb <((255*3)/2))
            if (!dUpDown.Name.Contains("G") && c > 128)
                fg = Color.White;
            dUpDown.ForeColor = fg;

            int cR = int.Parse(this.TxtRScoll.Text.ToString());
            int cG = int.Parse(this.TxtGScroll.Text.ToString());
            int cB = int.Parse(this.TxtBScroll.Text.ToString());
            
            this.GbColor.BackColor = Color.FromArgb(cR, cG, cB);
            this.TxtHex.Text = $"#{cR:X2}{cG:X2}{cB:X2}";
            SetSpan();
        }

        private void GBColorWheel_Paint(object sender, PaintEventArgs e)
        {
            RectangleF wheelrect = WheelRectangle;
            PointF center = Center(wheelrect);

            using (PathGradientBrush brush = new PathGradientBrush(m_path.ToArray(), WrapMode.Clamp))
            {
                brush.CenterPoint = center;
                brush.CenterColor = Color.White;
                brush.SurroundColors = m_colors.ToArray();

                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                e.Graphics.FillPie(brush, Rect(wheelrect), 0, 360);
            }

            using (SolidBrush b = new SolidBrush(Color.Black))
                e.Graphics.DrawEllipse(new Pen(b, m_borderSize), wheelrect);
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            CenterTheApp();
            CalcWheelPoints();
            SetupScollText();
            SetupTips();
            SetTitlebar();
        }
        private void FrmMain_Closing(object sender, FormClosingEventArgs e)
        {
            Tips.Dispose();
            if (timer.Enabled)
                StopFindColor();
        }
        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                StopFindColor();
            else
            {
                if (ZoomLens == null || !ZoomLens.Visible)
                {
                    ZoomLens = new FrmLens()
                    {
                        Size = new Size(150, 150),
                        AutoClose = true,
                        ZoomFactor = 2,
                        NearestNeighborInterpolation = false
                    };
                    ZoomLens.ShowDialog(this);
                    if (ZoomLens != null && ZoomLens.ColorPicked != Color.Transparent)
                    {
                        StopFindColor();
                        this.GbColor.BackColor = ZoomLens.ColorPicked;
                        ZoomLens?.Dispose();

                        this.TxtRScoll.Text = this.GbColor.BackColor.R.ToString();
                        this.TxtGScroll.Text = this.GbColor.BackColor.G.ToString();
                        this.TxtBScroll.Text = this.GbColor.BackColor.B.ToString();

                        SetSpan();
                    }
                }
                else
                {
                    ZoomLens.Close();
                    ZoomLens.Dispose();
                }
            }
        }
        private void TxtScroll_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                StopFindColor();
                return;
            }
        }
        private void MouseEnter_Capture(object sender, EventArgs e)
        {
            if (this.Size.Width > DefaultFormSize.Width)
            {
                int tipHeight = this.panelTip.Height;
                this.panelGroup.Visible = false;

                this.panelGroup.Top = this.panelGroup.Top == 0 ? this.Height - this.panelGroup.Height - tipHeight : 0;

                SetPanelTip(tipHeight);

                this.panelTip.Visible = true;
                this.panelGroup.Visible = true;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            SelectColorClicked(sender, e);
        }
        private void BtnPick_Click(object sender, EventArgs e)
        {
            this.BtnCopy.BackColor = SystemColors.Control;
            this.BtnCopy.ForeColor = Color.Black;
            this.BtnPick.Enabled = false;
            this.BtnCopy.Visible = false;
            this.DefaultFormSetup = new Rectangle(this.Location, this.ClientSize);
            this.FormBorderStyle = FormBorderStyle.None;

            this.Hide();
            this.BackgroundImage = GetScreenCap(out Size sz);
            this.Location = new Point(0, 0);
            this.Size = sz;
            
            this.SetPanelTip(this.panelTip.Height);

            this.Show();
            this.Cursor = Cursors.Cross;
            this.timer.Enabled = true;
            this.BringToFront();
            this.Focus();
            this.TxtRScoll.Focus();
        }
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            this.BtnCopy.BackColor = Color.Green;
            this.BtnCopy.ForeColor = Color.White;
            Clipboard.Clear();

            Color color = GetFormatColor();
            this.TxtRScoll.Focus();

            if (color.IsEmpty)
                return;

            string col = color.ToString().Replace("Color [", "").Replace("]", "");
            col += $"{Environment.NewLine}{this.TxtHex.Text}";

            Clipboard.SetText($"{col}");
        }
        private void TxtScoll_SelectedItemChanged(object sender, EventArgs e)
        {
            DomainUpDown domainUpDown = (DomainUpDown)sender;
            SetTxtScrollTextColor(domainUpDown);
        }

        public Image GetScreenCap(out Size sz)
        {
            sz = this.Size;

            foreach (Screen scr in Screen.AllScreens)
            {
                if (scr.Bounds.Height > sz.Height)
                    sz.Height = scr.Bounds.Height;

                sz.Width += scr.Bounds.Width;
            }

            Image bmp = new Bitmap(sz.Width, sz.Height);
            Rectangle bounds = new Rectangle(0, 0, sz.Width, sz.Height);

            using (Graphics g = Graphics.FromImage(bmp))
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);

            return bmp;
        }
        public Color GetColorAt(int x, int y)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Rectangle bounds = new Rectangle(x, y, 1, 1);
            using (Graphics g = Graphics.FromImage(bmp))
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);

            return bmp.GetPixel(0, 0);
        }
    }
}
