using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FamilyFeud
{
    public partial class Game : Form
    {

        private Panel[] answerCovers;   
        private int team1Score = 0;
        private int team2Score = 0;
        private int activeTeam = 1;    

        public void DisplayQuestionSet(QuestionSet set)
        {
            lbQuestionare.Text = set.QuestionText;
            ClearStrikes();

            for (int i = 1; i <= 8; i++)
            {
                var answerLabel = (Label)this.Controls.Find($"lblQuestion{i}", true)[0];
                var pointsLabel = (Label)this.Controls.Find($"lblQuestionPoints{i}", true)[0];

                var match = set.Answers.FirstOrDefault(a => a.SlotNumber == i);
                if (match != null)
                {
                    answerLabel.Text = match.AnswerText;
                    pointsLabel.Text = match.Points.ToString();
                }
                else
                {
                    answerLabel.Text = "";
                    pointsLabel.Text = "";
                }


                if (answerCovers != null)
                    answerCovers[i - 1].Visible = (match != null);
            }
        }

        public Game()
        {
            InitializeComponent();
            CreateCovers();


            this.Resize += (s, e) =>
            {
                CenterQuestion();
                ArrangeTeamBoxes();
                ArrangeCovers();
                ArrangeBottomButtons();
            };
        }

        private void Game_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            this.KeyPreview = true;
            this.KeyDown -= Game_KeyDown;
            this.KeyDown += Game_KeyDown;

            this.KeyUp -= Game_KeyUp;
            this.KeyUp += Game_KeyUp;

            CenterQuestion();
            ArrangeTeamBoxes();     
            SetupStrikeLists();     
            ArrangeTeamBoxes();    
            ArrangeCovers();
            ArrangeBottomButtons();

            UpdateScoreLabels();
            
            label1.Click += (s, ev) => SetActiveTeam(1);
            label2.Click += (s, ev) => SetActiveTeam(2);

            this.BeginInvoke(new Action(() =>
            {
                int firstTeam = TeamPrompt.Ask(this);
                SetActiveTeam(firstTeam);
            }));
        }

        private void btnGoBackToStart_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to go back?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AfterStart bbackToAfterStart = new AfterStart();
                bbackToAfterStart.Show();
                this.Hide();
            }
        }


        private void CreateCovers()
        {
            answerCovers = new Panel[8];
            for (int i = 0; i < 8; i++)
            {
                var p = new Panel();
                p.Name = $"pnlCover{i + 1}";
                p.Tag = i + 1;                      
                p.BackColor = Color.RoyalBlue;
                p.BorderStyle = BorderStyle.FixedSingle;
                p.Cursor = Cursors.Hand;
                p.Click += Cover_Click;
                p.Paint += Cover_Paint;

                answerCovers[i] = p;
                this.Controls.Add(p);
                p.BringToFront();
            }
        }

        private void Cover_Click(object sender, EventArgs e)
        {
            var panel = (Panel)sender;
            int slot = (int)panel.Tag;

            var pointsLabel = (Label)this.Controls.Find($"lblQuestionPoints{slot}", true)[0];
            int.TryParse(pointsLabel.Text, out int points);

            if (isStealMode)
            {

                if (activeTeam == 1)
                {
                    team1Score += team2Score + points;
                    team2Score = 0;
                }
                else
                {
                    team2Score += team1Score + points;
                    team1Score = 0;
                }

                isStealMode = false;
                roundLocked = true;
            }
            else
            {

                if (activeTeam == 1) team1Score += points;
                else team2Score += points;
            }

            UpdateScoreLabels();
            panel.Visible = false;
        }

        private void Cover_Paint(object sender, PaintEventArgs e)
        {
            var p = (Panel)sender;
            using (var font = new Font("Comic Sans MS", 28, FontStyle.Bold))
            {
                TextRenderer.DrawText(e.Graphics, p.Tag.ToString(), font, p.ClientRectangle,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void ArrangeCovers()
        {
            if (answerCovers == null) return;

            int w = this.ClientSize.Width;

            Label[] answers = { lblQuestion1, lblQuestion2, lblQuestion3, lblQuestion4,
                                lblQuestion5, lblQuestion6, lblQuestion7, lblQuestion8 };


            int leftColLeft = (int)(w * 0.19);
            int leftColRight = (int)(w * 0.47);
            int rightColLeft = (int)(w * 0.54);
            int rightColRight = (int)(w * 0.81);

            int rowHeight = lblQuestion2.Top - lblQuestion1.Top - 15; 

            for (int i = 0; i < 8; i++)
            {
                bool isLeft = i < 4;
                int left = isLeft ? leftColLeft : rightColLeft;
                int right = isLeft ? leftColRight : rightColRight;

                answerCovers[i].SetBounds(left, answers[i].Top - 10, right - left, rowHeight);
                answerCovers[i].BringToFront();
            }
        }

        private void UpdateScoreLabels()
        {
            lblTeamOnePoints.Text = team1Score.ToString();
            lblTeamTwoPoints.Text = team2Score.ToString();
        }

        private void SetActiveTeam(int team)
        {
            activeTeam = team;
            label1.ForeColor = (team == 1) ? Color.Red : Color.Black;
            label2.ForeColor = (team == 2) ? Color.Red : Color.Black;
        }


        private void CenterQuestion()
        {
            int formWidth = this.ClientSize.Width;
            int centerX = formWidth / 2;


            int qTotalWidth = label3.Width + 10 + lbQuestionare.Width;
            label3.Left = centerX - (qTotalWidth / 2);
            label3.Top = 67;
            lbQuestionare.Left = label3.Right + 10;
            lbQuestionare.Top = 67;

            int topMargin = 250; 
            int rowGap = 100;


            int leftMargin = (int)(formWidth * 0.20); 
            lblQuestion1.Left = leftMargin;
            lblQuestion2.Left = leftMargin;
            lblQuestion3.Left = leftMargin;
            lblQuestion4.Left = leftMargin;

            lblQuestion1.Top = topMargin;
            lblQuestion2.Top = topMargin + rowGap;
            lblQuestion3.Top = topMargin + (rowGap * 2);
            lblQuestion4.Top = topMargin + (rowGap * 3);


            int leftMarginp = (int)(formWidth * 0.40);  
            lblQuestionPoints1.Left = leftMarginp;
            lblQuestionPoints2.Left = leftMarginp;
            lblQuestionPoints3.Left = leftMarginp;
            lblQuestionPoints4.Left = leftMarginp;

            lblQuestionPoints1.Top = lblQuestion1.Top;
            lblQuestionPoints2.Top = lblQuestion2.Top;
            lblQuestionPoints3.Top = lblQuestion3.Top;
            lblQuestionPoints4.Top = lblQuestion4.Top;


            int rightColumnLeft = (int)(formWidth * 0.55); 

            Label[] rightAnswers = { lblQuestion5, lblQuestion6, lblQuestion7, lblQuestion8 };
            foreach (Label lbl in rightAnswers)
            {
                lbl.AutoSize = true;                       
                lbl.TextAlign = ContentAlignment.TopLeft;
                lbl.Left = rightColumnLeft;
            }

            lblQuestion5.Top = topMargin;
            lblQuestion6.Top = topMargin + rowGap;
            lblQuestion7.Top = topMargin + (rowGap * 2);
            lblQuestion8.Top = topMargin + (rowGap * 3);


            int rightMarginp = (int)(formWidth * 0.20);  
            lblQuestionPoints5.Left = formWidth - rightMarginp - lblQuestionPoints5.Width;
            lblQuestionPoints6.Left = formWidth - rightMarginp - lblQuestionPoints6.Width;
            lblQuestionPoints7.Left = formWidth - rightMarginp - lblQuestionPoints7.Width;
            lblQuestionPoints8.Left = formWidth - rightMarginp - lblQuestionPoints8.Width;

            lblQuestionPoints5.Top = lblQuestion5.Top;
            lblQuestionPoints6.Top = lblQuestion6.Top;
            lblQuestionPoints7.Top = lblQuestion7.Top;
            lblQuestionPoints8.Top = lblQuestion8.Top;
        }

        private void ArrangeTeamBoxes()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;


            int boxWidth = (int)(w * 0.14);
            int boxTop = (int)(h * 0.37);
            int boxHeight = (int)(h * 0.31);

            int leftBoxLeft = (int)(w * 0.02);
            int rightBoxLeft = (int)(w * 0.84);

            PlaceTeamInBox(label1, lblTeamOnePoints, lvTeamOneWrong,
                           rightBoxLeft, boxTop, boxWidth, boxHeight);

            PlaceTeamInBox(label2, lblTeamTwoPoints, lvTeamTwoWrong,
                           leftBoxLeft, boxTop, boxWidth, boxHeight);
        }

        private void PlaceTeamInBox(Label teamLabel, Label pointsLabel, ListView wrongList,
                                    int boxLeft, int boxTop, int boxWidth, int boxHeight)
        {
            int pad = 10;   
            int gap = 25;   
            int innerWidth = boxWidth - (pad * 2);
            int y = boxTop + pad;


            teamLabel.AutoSize = false;
            teamLabel.TextAlign = ContentAlignment.MiddleCenter;
            teamLabel.Width = innerWidth;
            teamLabel.Left = boxLeft + pad;
            teamLabel.Top = y;
            y += teamLabel.Height + gap;


            pointsLabel.AutoSize = false;
            pointsLabel.TextAlign = ContentAlignment.MiddleCenter;
            pointsLabel.Width = innerWidth;
            pointsLabel.Left = boxLeft + pad;
            pointsLabel.Top = y;
            y += pointsLabel.Height + gap;


            wrongList.Left = boxLeft + pad;
            wrongList.Top = y;
            wrongList.Width = innerWidth;
            wrongList.Height = (strikeListHeight > 0 ? strikeListHeight : 110) + ListExtraHeight;
            wrongList.Width = innerWidth + ListExtraWidth;

 
            if (strikeImages != null)
                SetIconSpacing(wrongList, strikeSlotWidth, strikeIconH + 6);
        }
        private const int ListExtraWidth = 21;
        private const int ListExtraHeight = 7;
        private const int MaxStrikes = 3;
        private ImageList strikeImages;
        private int strikeIconH = 0;
        private int strikeListHeight = 0;
        private int strikeSlotWidth = 0;


        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int LVM_SETICONSPACING = 0x1035;

        private static void SetIconSpacing(ListView lv, int cx, int cy)
        {
            SendMessage(lv.Handle, LVM_SETICONSPACING, IntPtr.Zero, (IntPtr)((cy << 16) | (cx & 0xFFFF)));
        }

        private void SetupStrikeLists()
        {

            foreach (ListView lv in new[] { lvTeamOneWrong, lvTeamTwoWrong })
            {
                lv.View = View.LargeIcon;
                lv.Alignment = ListViewAlignment.Top; 
                lv.Scrollable = false;
                lv.MultiSelect = false;
                lv.HideSelection = true;
                lv.BorderStyle = BorderStyle.None;


                lv.ItemSelectionChanged += (s, ev) => { if (ev.IsSelected) ev.Item.Selected = false; };
            }

           
            int listW = lvTeamOneWrong.ClientSize.Width - ListExtraWidth;
            strikeSlotWidth = listW / MaxStrikes;
            int iconW = strikeSlotWidth;

            while (iconW > 24)
            {
                ApplyStrikeIconSize(iconW);
                if (StrikesFit(lvTeamOneWrong, iconW)) break;
                iconW -= 2;
            }

            strikeListHeight = strikeIconH + 16;
        }

        private void ApplyStrikeIconSize(int iconW)
        {
            strikeIconH = (int)(iconW * 400.0 / 324.0);     

            var newImages = new ImageList();
            newImages.ColorDepth = ColorDepth.Depth32Bit;
            newImages.ImageSize = new Size(iconW, strikeIconH); 
            newImages.Images.Add("x", Properties.Resources.WrongSymbol);

            lvTeamOneWrong.LargeImageList = newImages;
            lvTeamTwoWrong.LargeImageList = newImages;
            SetIconSpacing(lvTeamOneWrong, strikeSlotWidth, strikeIconH + 6);
            SetIconSpacing(lvTeamTwoWrong, strikeSlotWidth, strikeIconH + 6);

            if (strikeImages != null) strikeImages.Dispose();
            strikeImages = newImages;
        }


        private bool StrikesFit(ListView lv, int iconW)
        {
            lv.Items.Clear();
            for (int i = 0; i < MaxStrikes; i++)
                lv.Items.Add(new ListViewItem("", "x"));

            Point first = lv.Items[0].Position;
            Point last = lv.Items[MaxStrikes - 1].Position;

            bool sameRow = (last.Y == first.Y);
            bool insideList = (last.X + iconW + 8 <= lv.ClientSize.Width);   

            lv.Items.Clear();
            return sameRow && insideList;
        }
        private bool isStealMode = false;
        private bool roundLocked = false;
        private void AddStrike(int team)
        {
            if (roundLocked) return;   

            if (isStealMode)
            {
                ListView stealList = (team == 1) ? lvTeamOneWrong : lvTeamTwoWrong;
                stealList.Items.Add(new ListViewItem("", "x"));
                isStealMode = false;
                roundLocked = true;   
                return;
            }

            ListView lv = (team == 1) ? lvTeamOneWrong : lvTeamTwoWrong;
            if (lv.Items.Count >= MaxStrikes) return;

            lv.Items.Add(new ListViewItem("", "x"));

            if (lv.Items.Count >= MaxStrikes)
            {
                int otherTeam = (team == 1) ? 2 : 1;
                SetActiveTeam(otherTeam);
                isStealMode = true;
            }
        }

        private void ClearStrikes()
        {
            lvTeamOneWrong.Items.Clear();
            lvTeamTwoWrong.Items.Clear();
            roundLocked = false;
            isStealMode = false;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
            {
                if (!xKeyHeld)
                {
                    xKeyHeld = true;
                    AddStrike(activeTeam);
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.C)
            {
                ClearStrikes();
                e.SuppressKeyPress = true;
            }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
                xKeyHeld = false;
        }
        public void SetFirstTeam(int team)
        {
            SetActiveTeam(team);
        }

        private bool xKeyHeld = false;

        private void ArrangeBottomButtons()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            int margin = (int)(w * 0.02);      
            int bottomGap = (int)(h * 0.04);   

            btnFinish.Left = margin;
            btnFinish.Top = h - bottomGap - btnFinish.Height;

            btnGoBackToStart.Left = w - margin - btnGoBackToStart.Width;
            btnGoBackToStart.Top = h - bottomGap - btnGoBackToStart.Height;
        }


        private void lblQuestionPoints8_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestionPoints7_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestionPoints6_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestionPoints5_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion5_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion6_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion7_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion8_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestionPoints4_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion4_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestionPoints3_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion3_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestionPoints2_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion2_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestionPoints1_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion1_Click(object sender, EventArgs e)
        {
            frmStart goStart = new frmStart();

            goStart.Show();

            this.Hide();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
            "Finish the game?",
            "Confirm",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                frmStart goStart = new frmStart();

                goStart.Show();

                this.Hide();
            }


        }
    }
}
