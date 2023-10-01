using System.Windows.Forms;

namespace SColorPicker
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.BtnPick = new System.Windows.Forms.Button();
            this.GroupingBox = new System.Windows.Forms.GroupBox();
            this.TxtBScroll = new System.Windows.Forms.DomainUpDown();
            this.TxtGScroll = new System.Windows.Forms.DomainUpDown();
            this.TxtRScoll = new System.Windows.Forms.DomainUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCopy = new System.Windows.Forms.Button();
            this.LblXY = new System.Windows.Forms.Label();
            this.GbColor = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelSpanBottom = new System.Windows.Forms.Panel();
            this.panelSpanTop = new System.Windows.Forms.Panel();
            this.panelGroup = new System.Windows.Forms.Panel();
            this.gBColorWheel = new System.Windows.Forms.GroupBox();
            this.panelTip = new System.Windows.Forms.Panel();
            this.LabelTip = new System.Windows.Forms.Label();
            this.GroupingBox.SuspendLayout();
            this.GbColor.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelGroup.SuspendLayout();
            this.panelTip.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // BtnPick
            // 
            this.BtnPick.Location = new System.Drawing.Point(6, 19);
            this.BtnPick.Name = "BtnPick";
            this.BtnPick.Size = new System.Drawing.Size(56, 23);
            this.BtnPick.TabIndex = 0;
            this.BtnPick.Text = "&Pick";
            this.BtnPick.UseVisualStyleBackColor = true;
            this.BtnPick.Click += new System.EventHandler(this.BtnPick_Click);
            // 
            // GroupingBox
            // 
            this.GroupingBox.BackColor = System.Drawing.SystemColors.Control;
            this.GroupingBox.Controls.Add(this.TxtBScroll);
            this.GroupingBox.Controls.Add(this.TxtGScroll);
            this.GroupingBox.Controls.Add(this.TxtRScoll);
            this.GroupingBox.Controls.Add(this.label3);
            this.GroupingBox.Controls.Add(this.label2);
            this.GroupingBox.Controls.Add(this.label1);
            this.GroupingBox.Controls.Add(this.BtnCopy);
            this.GroupingBox.Controls.Add(this.LblXY);
            this.GroupingBox.Controls.Add(this.BtnPick);
            this.GroupingBox.Location = new System.Drawing.Point(0, 0);
            this.GroupingBox.Name = "GroupingBox";
            this.GroupingBox.Size = new System.Drawing.Size(200, 110);
            this.GroupingBox.TabIndex = 5;
            this.GroupingBox.TabStop = false;
            this.GroupingBox.Text = "[ Pick Color ]";
            this.GroupingBox.MouseEnter += new System.EventHandler(this.MouseEnter_Capture);
            // 
            // TxtBScroll
            // 
            this.TxtBScroll.Location = new System.Drawing.Point(138, 67);
            this.TxtBScroll.Name = "TxtBScroll";
            this.TxtBScroll.Size = new System.Drawing.Size(48, 20);
            this.TxtBScroll.TabIndex = 11;
            this.TxtBScroll.Text = "255";
            this.TxtBScroll.SelectedItemChanged += new System.EventHandler(this.TxtScoll_SelectedItemChanged);
            this.TxtBScroll.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtScroll_KeyUp);
            // 
            // TxtGScroll
            // 
            this.TxtGScroll.Location = new System.Drawing.Point(138, 44);
            this.TxtGScroll.Name = "TxtGScroll";
            this.TxtGScroll.Size = new System.Drawing.Size(48, 20);
            this.TxtGScroll.TabIndex = 10;
            this.TxtGScroll.Text = "255";
            this.TxtGScroll.SelectedItemChanged += new System.EventHandler(this.TxtScoll_SelectedItemChanged);
            this.TxtGScroll.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtScroll_KeyUp);
            // 
            // TxtRScoll
            // 
            this.TxtRScoll.Location = new System.Drawing.Point(138, 21);
            this.TxtRScoll.Name = "TxtRScoll";
            this.TxtRScoll.Size = new System.Drawing.Size(48, 20);
            this.TxtRScoll.TabIndex = 0;
            this.TxtRScoll.Text = "255";
            this.TxtRScoll.SelectedItemChanged += new System.EventHandler(this.TxtScoll_SelectedItemChanged);
            this.TxtRScoll.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtScroll_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Blue:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Green:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Red:";
            // 
            // BtnCopy
            // 
            this.BtnCopy.Location = new System.Drawing.Point(6, 81);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(56, 23);
            this.BtnCopy.TabIndex = 6;
            this.BtnCopy.Text = "&Copy";
            this.BtnCopy.UseVisualStyleBackColor = true;
            this.BtnCopy.Visible = false;
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // LblXY
            // 
            this.LblXY.AutoSize = true;
            this.LblXY.Location = new System.Drawing.Point(11, 47);
            this.LblXY.Name = "LblXY";
            this.LblXY.Size = new System.Drawing.Size(35, 13);
            this.LblXY.TabIndex = 5;
            this.LblXY.Text = "label1";
            // 
            // GbColor
            // 
            this.GbColor.BackColor = System.Drawing.SystemColors.Control;
            this.GbColor.Controls.Add(this.panel1);
            this.GbColor.Location = new System.Drawing.Point(0, 116);
            this.GbColor.Name = "GbColor";
            this.GbColor.Size = new System.Drawing.Size(200, 110);
            this.GbColor.TabIndex = 6;
            this.GbColor.TabStop = false;
            this.GbColor.Text = "[ Color Selector ]";
            this.GbColor.MouseEnter += new System.EventHandler(this.MouseEnter_Capture);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelSpanBottom);
            this.panel1.Controls.Add(this.panelSpanTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 57);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(194, 50);
            this.panel1.TabIndex = 0;
            // 
            // panelSpanBottom
            // 
            this.panelSpanBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSpanBottom.Location = new System.Drawing.Point(5, 25);
            this.panelSpanBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelSpanBottom.Name = "panelSpanBottom";
            this.panelSpanBottom.Size = new System.Drawing.Size(184, 20);
            this.panelSpanBottom.TabIndex = 2;
            // 
            // panelSpanTop
            // 
            this.panelSpanTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSpanTop.Location = new System.Drawing.Point(5, 5);
            this.panelSpanTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelSpanTop.Name = "panelSpanTop";
            this.panelSpanTop.Size = new System.Drawing.Size(184, 20);
            this.panelSpanTop.TabIndex = 1;
            // 
            // panelGroup
            // 
            this.panelGroup.BackColor = System.Drawing.Color.Transparent;
            this.panelGroup.Controls.Add(this.gBColorWheel);
            this.panelGroup.Controls.Add(this.GroupingBox);
            this.panelGroup.Controls.Add(this.GbColor);
            this.panelGroup.Location = new System.Drawing.Point(0, 0);
            this.panelGroup.Name = "panelGroup";
            this.panelGroup.Size = new System.Drawing.Size(404, 234);
            this.panelGroup.TabIndex = 7;
            // 
            // gBColorWheel
            // 
            this.gBColorWheel.BackColor = System.Drawing.SystemColors.Control;
            this.gBColorWheel.Location = new System.Drawing.Point(206, 0);
            this.gBColorWheel.Name = "gBColorWheel";
            this.gBColorWheel.Size = new System.Drawing.Size(190, 226);
            this.gBColorWheel.TabIndex = 7;
            this.gBColorWheel.TabStop = false;
            this.gBColorWheel.Text = "[ Color Wheel ]";
            this.gBColorWheel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseUp);
            this.gBColorWheel.Paint += new System.Windows.Forms.PaintEventHandler(this.gBColorWheel_Paint);
            // 
            // panelTip
            // 
            this.panelTip.BackColor = System.Drawing.Color.Black;
            this.panelTip.Controls.Add(this.LabelTip);
            this.panelTip.Location = new System.Drawing.Point(96, 294);
            this.panelTip.Name = "panelTip";
            this.panelTip.Padding = new System.Windows.Forms.Padding(4);
            this.panelTip.Size = new System.Drawing.Size(233, 37);
            this.panelTip.TabIndex = 8;
            // 
            // LabelTip
            // 
            this.LabelTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LabelTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelTip.Font = new System.Drawing.Font("Expo M", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LabelTip.Location = new System.Drawing.Point(4, 4);
            this.LabelTip.Name = "LabelTip";
            this.LabelTip.Size = new System.Drawing.Size(225, 29);
            this.LabelTip.TabIndex = 0;
            this.LabelTip.Text = "label4";
            this.LabelTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelTip.MouseEnter += new System.EventHandler(this.MouseEnter_Capture);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(651, 486);
            this.Controls.Add(this.panelTip);
            this.Controls.Add(this.panelGroup);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Color Picker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_Closing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseUp);
            this.GroupingBox.ResumeLayout(false);
            this.GroupingBox.PerformLayout();
            this.GbColor.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelGroup.ResumeLayout(false);
            this.panelTip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button BtnPick;
        private System.Windows.Forms.GroupBox GroupingBox;
        private System.Windows.Forms.Label LblXY;
        private System.Windows.Forms.GroupBox GbColor;
        private System.Windows.Forms.Button BtnCopy;
        private System.Windows.Forms.Panel panelGroup;
        private System.Windows.Forms.GroupBox gBColorWheel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSpanBottom;
        private System.Windows.Forms.Panel panelSpanTop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DomainUpDown TxtRScoll;
        private DomainUpDown TxtGScroll;
        private DomainUpDown TxtBScroll;
        private Panel panelTip;
        private Label LabelTip;
    }
}

