using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FamilyFeud
{
    public class ConfirmPrompt : Form
    {
        private static readonly Color Navy = Color.FromArgb(14, 40, 98);
        private static readonly Color Cream = Color.FromArgb(255, 236, 170);
        private static readonly Color DarkBrown = Color.FromArgb(58, 34, 5);

        private readonly string message;
        private bool result = false;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; 
                return cp;
            }
        }

        public static bool Ask(Form owner, string message)
        {
            using (var dlg = new ConfirmPrompt(message, false))
            {
                dlg.ShowDialog(owner);
                return dlg.result;
            }
        }

        public static void Notify(Form owner, string message)
        {
            using (var dlg = new ConfirmPrompt(message, true))
            {
                dlg.ShowDialog(owner);
            }
        }

        private ConfirmPrompt(string message, bool okOnly)
        {

            this.message = message;

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            ClientSize = new Size(560, 327);
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
            KeyPreview = true;
            BackgroundImage = GetFrame(ClientSize);
            BackgroundImageLayout = ImageLayout.None;

            BackColor = Navy;
            BackgroundImage = Properties.Resources.FRAMETEAM;
            BackgroundImageLayout = ImageLayout.Stretch;

            Region = new Region(RoundedRect(new Rectangle(0, 0, Width, Height), 16));

            if (okOnly)
            {
                Controls.Add(MakeButton("OK", true, (ClientSize.Width - 215) / 2)); 
            }
            else
            {
                Controls.Add(MakeButton("YES", true, 60));
                Controls.Add(MakeButton("NO", false, 285));
            }

            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) { result = false; Close(); }
            };
        }

        private Button MakeButton(string text, bool answer, int left)
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
                result = answer;
                Close();
            };
            return b;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var font = new Font("Comic Sans MS", 24f, FontStyle.Bold))
            {
                var area = new Rectangle(40, 40, Width - 80, 130);
                TextRenderer.DrawText(e.Graphics, message, font, area, Cream,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
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

        private static Bitmap cachedFrame;

        private static Bitmap GetFrame(Size size)
        {
            if (cachedFrame == null || cachedFrame.Size != size)
            {
                cachedFrame?.Dispose();
                cachedFrame = new Bitmap(size.Width, size.Height);
                using (var g = Graphics.FromImage(cachedFrame))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(Properties.Resources.FRAMETEAM, 0, 0, size.Width, size.Height);
                }
            }
            return cachedFrame;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "ConfirmPrompt";
            this.Load += new System.EventHandler(this.ConfirmPrompt_Load);
            this.ResumeLayout(false);

        }

        private void ConfirmPrompt_Load(object sender, EventArgs e)
        {

        }
    }
}