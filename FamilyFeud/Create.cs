using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FamilyFeud
{
    public partial class Create : Form
    {
        private readonly Form owner;
        private const float DesignW = 1917f;  
        private const float DesignH = 1077f;
        private Rectangle play;             
        private Size finishBaseSize, backBaseSize;
        private Font currentFont;        

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; 
                return cp;
            }
        }
        public Create()
        {
            InitializeComponent();
            SuspendLayout();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();

            finishBaseSize = btnFinishCreate.Size;
            backBaseSize = btnGoBackToAfterStart.Size;

            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;

            StyleImageButton(btnFinishCreate, Properties.Resources.CREATEBUTTON);
            StyleImageButton(btnGoBackToAfterStart, Properties.Resources.GOBACKBUTTON);
            StyleQuestionBox();
            StyleAnswerBoxes();

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Bounds = Screen.PrimaryScreen.Bounds;

            PrebuildBackground();       
            CenterLayout();
            FormHelper.EnableDoubleBuffer(this);

            ResumeLayout(true);
        }
        public Create(Form owner) : this()
        {
            this.owner = owner;
        }

        private void PrebuildBackground()
        {
            Image src = BackgroundImage;    
            if (src == null) return;

            var bmp = new Bitmap(Bounds.Width, Bounds.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float s = Math.Min((float)bmp.Width / src.Width, (float)bmp.Height / src.Height);
                int w = (int)(src.Width * s);
                int h = (int)(src.Height * s);
                g.DrawImage(src, (bmp.Width - w) / 2, (bmp.Height - h) / 2, w, h);
            }

            BackgroundImage = bmp;
            BackgroundImageLayout = ImageLayout.None;
        }
        private void Create_Load(object sender, EventArgs e)
        {

        }

        private void Create_Resize(object sender, EventArgs e)
        {
            CenterLayout();
        }

        private void btnFinishCreate_Click(object sender, EventArgs e)
        {
            var set = new QuestionSet
            {
                QuestionText = txtbCreateQuestionare.Text.Trim()
            };

            for (int i = 1; i <= 8; i++)
            {
                var questionBox = (TextBox)this.Controls.Find($"txtbCreateQuestion{i}", true)[0];
                var pointsBox = (TextBox)this.Controls.Find($"txtbCreatePoints{i}", true)[0];

                if (string.IsNullOrWhiteSpace(questionBox.Text))
                    continue;

                if (!int.TryParse(pointsBox.Text.Trim(), out int points))
                {
                    ConfirmPrompt.Notify(this, $"Points for Question {i} must be a whole number.");
                    return;
                }

                set.Answers.Add(new QuestionAnswer
                {
                    AnswerText = questionBox.Text.Trim(),
                    Points = points,
                    SlotNumber = i
                });
            }

            if (set.Answers.Count == 0)
            {
                ConfirmPrompt.Notify(this, "Enter at least one answer.");
                return;
            }
            try
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FeudDb"].ConnectionString;
                var repo = new QuestionRepository(connStr);
                int newSetId = repo.SaveQuestionSet(set);

                ConfirmPrompt.Notify(this, $"Saved! Question Set ID: {newSetId}");

                txtbCreateQuestionare.Clear();
                for (int i = 1; i <= 8; i++)
                {
                    ((TextBox)this.Controls.Find($"txtbCreateQuestion{i}", true)[0]).Clear();
                    ((TextBox)this.Controls.Find($"txtbCreatePoints{i}", true)[0]).Clear();
                }
            }
            catch (Exception ex)
            {
                ConfirmPrompt.Notify(this, "Save failed: " + ex.Message);
            }
        }

        private void btnGoBackToAfterStart_Click(object sender, EventArgs e)
        {
            if (owner != null)
                owner.Show();
            else
                new AfterStart().Show();

            this.Close();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private TextBox Box(string name)
        {
            return (TextBox)this.Controls.Find(name, true)[0];
        }

        private Rectangle D(double x, double y, double wd, double ht)
        {
            float s = play.Width / DesignW;
            return new Rectangle(
                play.Left + (int)(x * s),
                play.Top + (int)(y * s),
                (int)(wd * s),
                (int)(ht * s));
        }



        private void CenterLayout()
        {
                  
            if (ClientSize.Width == 0 || ClientSize.Height == 0) return;   

            this.SuspendLayout();

            float scale = Math.Min(ClientSize.Width / DesignW, ClientSize.Height / DesignH);
            int pw = (int)(DesignW * scale);
            int ph = (int)(DesignH * scale);
            play = new Rectangle((ClientSize.Width - pw) / 2, (ClientSize.Height - ph) / 2, pw, ph);

            ApplyFonts(scale);

            btnFinishCreate.Bounds = D(100, 910, 290, 90);
            btnGoBackToAfterStart.Bounds = D(1529, 910, 296, 100);

            pbQuestionLabel.SizeMode = PictureBoxSizeMode.Zoom;
            pbQuestionLabel.Bounds = D(180, 74, 247, 91);
            pbQuestionFrame.Bounds = D(449, 78, 1282, 98);

            int pad = (int)(play.Width * 0.0115);
            txtbCreateQuestionare.Left = pbQuestionFrame.Left + pad;
            txtbCreateQuestionare.Width = pbQuestionFrame.Width - pad * 2;
            txtbCreateQuestionare.Top = pbQuestionFrame.Top + (pbQuestionFrame.Height - txtbCreateQuestionare.Height) / 2;

            pbQuestionFrame.SendToBack();
            txtbCreateQuestionare.BringToFront();

            LayoutAnswerBoxes();

            this.ResumeLayout();
        }

        private PictureBox Pic(string name)
        {
            return (PictureBox) this.Controls.Find(name, true)[0];
        }
        private void LayoutAnswerBoxes()
        {

            pbAnswersFrame.Bounds = D(190, 215, 1537, 685);

            for (int i = 0; i < 8; i++)
            {
                int col = i / 4;  
                int row = i % 4;

                double rx = 218 + col * (725 + 31);  
                double ry = 243 + row * (142 + 20);  

                Pic($"pbRow{i + 1}").Bounds = D(rx, ry, 725, 142);

                Rectangle qArea = D(rx + 26, ry, 531, 142);
                Rectangle pArea = D(rx + 591, ry, 105, 142);

                TextBox q = Box($"txtbCreateQuestion{i + 1}");
                q.Left = qArea.Left;
                q.Width = qArea.Width;
                q.Top = qArea.Top + (qArea.Height - q.Height) / 2;

                TextBox p = Box($"txtbCreatePoints{i + 1}");
                p.Left = pArea.Left;
                p.Width = pArea.Width;
                p.Top = pArea.Top + (pArea.Height - p.Height) / 2;
            }

            for(int i = 1; i <= 8; i++) Pic($"pbRow{i}").SendToBack();
            pbAnswersFrame.SendToBack();
            for (int i = 1; i <= 8; i++)
            {
                Box($"txtbCreateQuestion{i}").BringToFront();
                Box($"txtbCreatePoints{i}").BringToFront();
            }
        }

        private void ApplyFonts(float scale)
        {
            float px = 44f * scale;   

            var newFont = new Font(txtbCreateQuestionare.Font.FontFamily, px, FontStyle.Bold, GraphicsUnit.Pixel);

            txtbCreateQuestionare.Font = newFont;
            for (int i = 1; i <= 8; i++)
            {
                Box($"txtbCreateQuestion{i}").Font = newFont;
                Box($"txtbCreatePoints{i}").Font = newFont;
            }

            currentFont?.Dispose(); 
            currentFont = newFont;
        }

        private void StyleQuestionBox()
        {
            txtbCreateQuestionare.Multiline = false;
            txtbCreateQuestionare.BorderStyle = BorderStyle.None;
            txtbCreateQuestionare.BackColor = Color.FromArgb(14, 40, 98);
            txtbCreateQuestionare.ForeColor = Color.FromArgb(255, 236, 170);
        }
        private void StyleAnswerBoxes()
        {
            for (int i = 1; i <= 8; i++)
            {
                TextBox q = Box($"txtbCreateQuestion{i}");
                TextBox p = Box($"txtbCreatePoints{i}");

                foreach (TextBox t in new[] { q, p })
                {
                    t.BorderStyle = BorderStyle.None;
                    t.BackColor = Color.FromArgb(14, 40, 98); 
                    t.ForeColor = Color.FromArgb(255, 236, 170); 
                }
            }
        }

        private void pbAnswersFrame_Click(object sender, EventArgs e)
        {

        }

        private void StyleImageButton(Button b, Image img)
        {
            b.Text = "";
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.Transparent;
            b.FlatAppearance.MouseDownBackColor = Color.Transparent;
            b.BackColor = Color.Transparent;
            b.UseVisualStyleBackColor = false;
            b.BackgroundImage = img;
            b.BackgroundImageLayout = ImageLayout.Zoom;
        }
    }
}