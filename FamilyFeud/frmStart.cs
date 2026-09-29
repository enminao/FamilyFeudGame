using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilyFeud
{
    public partial class frmStart : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public frmStart()
        {
            InitializeComponent();
            SuspendLayout();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Bounds = Screen.PrimaryScreen.Bounds;
            BackColor = Color.FromArgb(14, 40, 98);  

            picMainLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picClubLogo.SizeMode = PictureBoxSizeMode.Zoom;

            StyleButton(btnStart, Properties.Resources.STARTGAME);
            StyleButton(btnQuit, Properties.Resources.QUITGAME);

            CenterButtons();
            ResumeLayout(true);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmStart_Load(object sender, EventArgs e)
        {

        }
        private void StyleButton(Button btn, Image img)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.BackColor = Color.Transparent;
            btn.Text = "";
            BackgroundImage = ScaleTo(Properties.Resources.STARTBACKGROUND, Bounds.Size);
            BackgroundImageLayout = ImageLayout.None;
            btn.TabStop = false; 
        }

        private static Bitmap ScaleTo(Image src, Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, 0, 0, size.Width, size.Height);
            }
            return bmp;
        }

        private void frmStart_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmStart_Resize(object sender, EventArgs e)
        {
            CenterButtons();
        }

        private void CenterButtons()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            int centerX = w / 2;
            int centerY = h / 2;

            float logoAspect = 1317f / 692f;
            picMainLogo.Width = (int)(w * 0.55);
            picMainLogo.Height = (int)(picMainLogo.Width / logoAspect);
            picMainLogo.Left = centerX - (picMainLogo.Width / 2);
            picMainLogo.Top = (int)(h * 0.055);

            picClubLogo.Width = (int)(w * 0.25);
            picClubLogo.Height = (int)(h * 0.35);
            picClubLogo.Left = (int)(w * 0.04);
            picClubLogo.Top = (int)(h * 0.60);

            float btnAspect = 1000f / 280f;
            btnStart.Width = (int)(w * 0.18);
            btnStart.Height = (int)(btnStart.Width / btnAspect);
            btnQuit.Width = btnStart.Width;
            btnQuit.Height = btnStart.Height;

            int spacing = 30;
            int totalHeight = btnStart.Height + btnQuit.Height + spacing;
            int verticalOffset = (int)(h * 0.25);
            int startY = centerY - (totalHeight / 2) + verticalOffset;

            btnStart.Left = centerX - (btnStart.Width / 2);
            btnStart.Top = startY;

            btnQuit.Left = centerX - (btnQuit.Width / 2);
            btnQuit.Top = btnStart.Bottom + spacing;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            AfterStart edit = new AfterStart(this);
            edit.Show();
            this.Hide();
        }
    }
}
