using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FamilyFeud
{
    public class TeamPrompt : Form
    {
        private static readonly Color Navy = Color.FromArgb(14, 40, 98);
        private static readonly Color Gold = Color.FromArgb(243, 169, 31);
        private static readonly Color GoldLight = Color.FromArgb(255, 214, 102);
        private static readonly Color Cream = Color.FromArgb(255, 236, 170);
        private static readonly Color DarkBrown = Color.FromArgb(58, 34, 5);

        private int result = 1;

        public static int Ask(Form owner)
        {
            using (var dlg = new TeamPrompt())
            {
                dlg.ShowDialog(owner);
                return dlg.result;
            }
        }

        private TeamPrompt()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            ClientSize = new Size(560, 327);       
            DoubleBuffered = true;

            BackColor = Navy;
            BackgroundImage = Properties.Resources.FRAMETEAM;
            BackgroundImageLayout = ImageLayout.Stretch;

            Region = new Region(RoundedRect(new Rectangle(0, 0, Width, Height), 16));

            Controls.Add(MakeButton("TEAM A", 2, 60));
            Controls.Add(MakeButton("TEAM B", 1, 285));
        }

        private Button MakeButton(string text, int team, int left)
        {
            var b = new PillButton();
            b.Text = text;
            b.Font = new Font("Comic Sans MS", 16f, FontStyle.Bold);
            b.SetBounds(left, 200, 215, 60);
            b.ForeColor = DarkBrown;
            b.Cursor = Cursors.Hand;
            b.TabStop = false;
            b.Click += (s, e) =>
            {
                result = team;
                Close();
            };
            return b;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);  

            using (var font = new Font("Comic Sans MS", 24f, FontStyle.Bold))
            {
                var area = new Rectangle(0, 45, Width, 120);
                TextRenderer.DrawText(e.Graphics, "Which team will go first?", font, area, Cream,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "TeamPrompt";
            this.Load += new System.EventHandler(this.TeamPrompt_Load);
            this.ResumeLayout(false);

        }

        private void TeamPrompt_Load(object sender, EventArgs e)
        {

        }
    }
    public class PillButton : Button
    {
        private bool hover, down;

        public PillButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
        }

        protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hover = false; down = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { down = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { down = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaintBackground(e); 

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = new Rectangle(2, 2, Width - 5, Height - 5);

            Color top = down ? Color.FromArgb(215, 140, 15)
                      : hover ? Color.FromArgb(255, 214, 102)
                      : Color.FromArgb(255, 196, 60);
            Color bottom = down ? Color.FromArgb(190, 120, 10)
                         : hover ? Color.FromArgb(243, 169, 31)
                         : Color.FromArgb(230, 150, 20);

            using (var path = Rounded(r, r.Height / 2))
            {
                using (var fill = new LinearGradientBrush(r, top, bottom, 90f))
                    g.FillPath(fill, path);

                using (var border = new Pen(Color.FromArgb(255, 236, 170), 3)) 
                    g.DrawPath(border, path);
            }

            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
        }

        private static GraphicsPath Rounded(Rectangle r, int radius)
        {
            int d = radius * 2;
            var p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}


