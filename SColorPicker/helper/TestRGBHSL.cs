using System.Drawing;
using System.Windows.Forms;
using xRay.Toolkit.Utilities;
using System;

namespace rgbhsltest
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
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
            //
            // Form1
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 349);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);

        }
        #endregion

/*        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }
*/
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Color[] rainbow = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
            int n = 0;
            foreach (Color c in rainbow)
            {
                for (double b = 0; b <= 1; b += 0.1)
                {
                    SolidBrush br = new SolidBrush(RGBHSL.SetBrightness(c, b));
                    e.Graphics.FillRectangle(br, (float)(this.ClientSize.Width * b), (float)(this.ClientSize.Height / 7 * n), (float)(this.ClientSize.Width / 10), (float)(this.ClientSize.Height / 6));
                    br.Dispose();
                }
                n++;
            }
        }
    }
}