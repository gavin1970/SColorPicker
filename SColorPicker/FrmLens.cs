using System;
using SColorPicker;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

public class FrmLens : Form
{
    private bool ControlKeyPressed = false;
    public List<Color> ColorPickedList { get; set; } = new List<Color>() {  };
    public Color ColorPicked { get; set; } = Color.Transparent;
    readonly FrmMain formMain = new FrmMain();
    private readonly Timer timer;
    private Image scrBmp;
    private Graphics scrGrp;
    private bool mouseDown;

    public FrmLens() : base()
    {
        SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.Opaque |
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint, true);

        UpdateStyles();

        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        TopMost = true;
        Width = 150;
        Height = 150;

        timer = new Timer() { Interval = 55, Enabled = true };
        timer.Tick += (s, e) => Invalidate();
    }

    public int ZoomFactor { get; set; } = 2;
    public bool HideCursor { get; set; } = true;
    public bool AutoClose { get; set; } = true;
    public bool NearestNeighborInterpolation { get; set; }

    private void CopyScreen()
    {
        if (scrBmp == null)
        {
            scrBmp = formMain.GetScreenCap(out Size _);
            scrGrp = Graphics.FromImage(scrBmp);
        }

        scrGrp.CopyFromScreen(Point.Empty, Point.Empty, scrBmp.Size);
    }
    private void SetLocation()
    {
        var p = Cursor.Position;

        Left = p.X - Width / 2;
        Top = p.Y - Height / 2;
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        if (e.Delta > 0 && ZoomFactor < 10)
            ZoomFactor++;
        else if (e.Delta < 0 && ZoomFactor > 1)
            ZoomFactor--;
    }
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        var gp = new GraphicsPath();
        gp.AddEllipse(0, 0, Width, Height);
        Region = new Region(gp);

        CopyScreen();
        SetLocation();

        Capture = true;
        mouseDown = true;
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Right)
            mouseDown = true;
        else if (e.Button == MouseButtons.Left)
        {
            var clr = formMain.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
            if (ControlKeyPressed)
                ColorPickedList.Add(clr);   //append
            ColorPicked = clr;              //always set the last
        }
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        Invalidate();
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (!ControlKeyPressed)
        {
            if (mouseDown)
            {
                mouseDown = false;

                if (ColorPickedList.Count > 0)
                    this.Close();
                else if (AutoClose)
                    Dispose();
            }
            else
                this.Close();
        }

        base.OnMouseUp(e);
    }
    protected override void OnKeyUp(KeyEventArgs e)
    {
        ControlKeyPressed = e.Modifiers == Keys.Control;
        base.OnKeyUp(e);
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        Point loc = Cursor.Position;
        if (e.KeyCode == Keys.Right)
        {
            Cursor.Position = new Point(loc.X + 1, loc.Y);
        }
        else if (e.KeyCode == Keys.Left && loc.X >= 1)
        {
            Cursor.Position = new Point(loc.X - 1, loc.Y);
        }
        else if (e.KeyCode == Keys.Up && loc.Y >= 1)
        {
            Cursor.Position = new Point(loc.X, loc.Y - 1);
        }
        else if (e.KeyCode == Keys.Down)
        {
            Cursor.Position = new Point(loc.X, loc.Y + 1);
        }

        ControlKeyPressed = e.Modifiers == Keys.Control;

        if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var clr = formMain.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
                if (ControlKeyPressed)
                    ColorPickedList.Add(clr);   //append
                ColorPicked = clr;              //always set the last
            }

            if (!ControlKeyPressed)
            {
                if (ColorPickedList.Count > 0)
                    this.Close();
                else if (AutoClose)
                    Dispose();
            }
        }

        base.OnKeyDown(e);
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        if (mouseDown) SetLocation();
        else CopyScreen();

        var pos = Cursor.Position;
        var cr = RectangleToScreen(ClientRectangle);
        var dY = cr.Top - Top;
        var dX = cr.Left - Left;

        e.Graphics.TranslateTransform(Width / 2, Height / 2);
        e.Graphics.ScaleTransform(ZoomFactor, ZoomFactor);
        e.Graphics.TranslateTransform(-pos.X - dX, -pos.Y - dY);
        e.Graphics.Clear(BackColor);

        if (NearestNeighborInterpolation)
        {
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
        }

        if (scrBmp != null)
            e.Graphics.DrawImage(scrBmp, 0, 0);
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            timer.Dispose();
            scrBmp?.Dispose();
            scrGrp?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // FrmLens
            // 
            this.ClientSize = new Size(284, 261);
            this.Name = "FrmLens";
            this.Load += new EventHandler(this.FrmLens_Load);
            this.ResumeLayout(false);

    }

    private void FrmLens_Load(object sender, EventArgs e)
    {

    }
}