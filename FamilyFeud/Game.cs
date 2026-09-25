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
        // ----- Game state -----
        private Panel[] answerCovers;   // pnlCover1 .. pnlCover8 (created in code)
        private int team1Score = 0;
        private int team2Score = 0;
        private int activeTeam = 1;     // which team gets the points when a cover is clicked



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

                // Covers come back for a new question; empty slots stay uncovered
                if (answerCovers != null)
                    answerCovers[i - 1].Visible = (match != null);
            }
        }

        public Game()
        {
            InitializeComponent();
            CreateCovers();

            // Re-run the layout if the window size ever changes
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

            // Key presses: the form must see them before the controls do
            this.KeyPreview = true;
            this.KeyDown -= Game_KeyDown;   // avoids double-firing if the designer already hooked it
            this.KeyDown += Game_KeyDown;

            this.KeyUp -= Game_KeyUp;
            this.KeyUp += Game_KeyUp;

            CenterQuestion();
            ArrangeTeamBoxes();     // 1st pass: places the lists so they have a real width
            SetupStrikeLists();     // measures that width and builds the X icons
            ArrangeTeamBoxes();     // 2nd pass: applies the list height and icon spacing
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

        // =====================================================
        //  COVER PANELS + SCORING
        // =====================================================
        private void CreateCovers()
        {
            answerCovers = new Panel[8];
            for (int i = 0; i < 8; i++)
            {
                var p = new Panel();
                p.Name = $"pnlCover{i + 1}";
                p.Tag = i + 1;                       // slot number, drawn on the cover
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
                // Stealing team takes the other team's ENTIRE score, plus this panel's points
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
                // Normal play — just add this panel's points to whoever's active
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

            // Horizontal span of each column (as a fraction of the form width)
            int leftColLeft = (int)(w * 0.19);
            int leftColRight = (int)(w * 0.47);
            int rightColLeft = (int)(w * 0.54);
            int rightColRight = (int)(w * 0.81);

            int rowHeight = lblQuestion2.Top - lblQuestion1.Top - 15;  // leaves a small gap between rows

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

        // =====================================================
        //  LAYOUT
        // =====================================================
        private void CenterQuestion()
        {
            int formWidth = this.ClientSize.Width;
            int centerX = formWidth / 2;

            // Center the question row near the top
            int qTotalWidth = label3.Width + 10 + lbQuestionare.Width;
            label3.Left = centerX - (qTotalWidth / 2);
            label3.Top = 67;
            lbQuestionare.Left = label3.Right + 10;
            lbQuestionare.Top = 67;

            int topMargin = 250; // vertical position
            int rowGap = 100;

            // ----- LEFT COLUMN (Questions 1-4) -----
            int leftMargin = (int)(formWidth * 0.20);   // 20% in from the left edge
            lblQuestion1.Left = leftMargin;
            lblQuestion2.Left = leftMargin;
            lblQuestion3.Left = leftMargin;
            lblQuestion4.Left = leftMargin;

            lblQuestion1.Top = topMargin;
            lblQuestion2.Top = topMargin + rowGap;
            lblQuestion3.Top = topMargin + (rowGap * 2);
            lblQuestion4.Top = topMargin + (rowGap * 3);

            // Points 1-4
            int leftMarginp = (int)(formWidth * 0.40);  // 40% in from the left edge
            lblQuestionPoints1.Left = leftMarginp;
            lblQuestionPoints2.Left = leftMarginp;
            lblQuestionPoints3.Left = leftMarginp;
            lblQuestionPoints4.Left = leftMarginp;

            lblQuestionPoints1.Top = lblQuestion1.Top;
            lblQuestionPoints2.Top = lblQuestion2.Top;
            lblQuestionPoints3.Top = lblQuestion3.Top;
            lblQuestionPoints4.Top = lblQuestion4.Top;

            // ----- RIGHT COLUMN (Questions 5-8) -----
            // Every answer starts at the same X and grows to the RIGHT.
            int rightColumnLeft = (int)(formWidth * 0.55);   // where the first letter of each answer starts

            Label[] rightAnswers = { lblQuestion5, lblQuestion6, lblQuestion7, lblQuestion8 };
            foreach (Label lbl in rightAnswers)
            {
                lbl.AutoSize = true;                          // label grows with the text
                lbl.TextAlign = ContentAlignment.TopLeft;
                lbl.Left = rightColumnLeft;
            }

            lblQuestion5.Top = topMargin;
            lblQuestion6.Top = topMargin + rowGap;
            lblQuestion7.Top = topMargin + (rowGap * 2);
            lblQuestion8.Top = topMargin + (rowGap * 3);

            // Points 5-8
            int rightMarginp = (int)(formWidth * 0.20);  // 20% in from the right edge
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

            // Box size/position (tuned to match your green boxes)
            int boxWidth = (int)(w * 0.14);
            int boxTop = (int)(h * 0.37);
            int boxHeight = (int)(h * 0.31);

            int leftBoxLeft = (int)(w * 0.02);
            int rightBoxLeft = (int)(w * 0.84);

            // Team 1 -> RIGHT box, Team 2 -> LEFT box
            PlaceTeamInBox(label1, lblTeamOnePoints, lvTeamOneWrong,
                           rightBoxLeft, boxTop, boxWidth, boxHeight);

            PlaceTeamInBox(label2, lblTeamTwoPoints, lvTeamTwoWrong,
                           leftBoxLeft, boxTop, boxWidth, boxHeight);
        }

        private void PlaceTeamInBox(Label teamLabel, Label pointsLabel, ListView wrongList,
                                    int boxLeft, int boxTop, int boxWidth, int boxHeight)
        {
            int pad = 10;   // space between the box edge and the controls
            int gap = 25;   // vertical space between team label, points label, and listview
            int innerWidth = boxWidth - (pad * 2);
            int y = boxTop + pad;

            // Team name (centered in the box)
            teamLabel.AutoSize = false;
            teamLabel.TextAlign = ContentAlignment.MiddleCenter;
            teamLabel.Width = innerWidth;
            teamLabel.Left = boxLeft + pad;
            teamLabel.Top = y;
            y += teamLabel.Height + gap;

            // Team points (centered in the box)
            pointsLabel.AutoSize = false;
            pointsLabel.TextAlign = ContentAlignment.MiddleCenter;
            pointsLabel.Width = innerWidth;
            pointsLabel.Left = boxLeft + pad;
            pointsLabel.Top = y;
            y += pointsLabel.Height + gap;

            // Wrong-answers (X) listview
            wrongList.Left = boxLeft + pad;
            wrongList.Top = y;
            wrongList.Width = innerWidth;
            wrongList.Height = (strikeListHeight > 0 ? strikeListHeight : 110) + ListExtraHeight;
            wrongList.Width = innerWidth + ListExtraWidth;

            // Exactly 3 X's per row: each slot is 1/3 of the list width
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

        // Lets us control the exact spacing between the X icons in a ListView
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int LVM_SETICONSPACING = 0x1035;

        private static void SetIconSpacing(ListView lv, int cx, int cy)
        {
            SendMessage(lv.Handle, LVM_SETICONSPACING, IntPtr.Zero, (IntPtr)((cy << 16) | (cx & 0xFFFF)));
        }

        private void SetupStrikeLists()
        {
            // 1) Configure the lists first (some of these recreate the control, so do them before measuring)
            foreach (ListView lv in new[] { lvTeamOneWrong, lvTeamTwoWrong })
            {
                lv.View = View.LargeIcon;
                lv.Alignment = ListViewAlignment.Top;   // items fill left -> right
                lv.Scrollable = false;
                lv.MultiSelect = false;
                lv.HideSelection = true;
                lv.BorderStyle = BorderStyle.None;

                // No blue highlight when clicked
                lv.ItemSelectionChanged += (s, ev) => { if (ev.IsSelected) ev.Item.Selected = false; };
            }

            // 2) Start with big icons and shrink them until all 3 really fit in the list
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
            strikeIconH = (int)(iconW * 400.0 / 324.0);     // keeps the image's proportions

            var newImages = new ImageList();
            newImages.ColorDepth = ColorDepth.Depth32Bit;
            newImages.ImageSize = new Size(iconW, strikeIconH);   // set the size BEFORE adding images
            newImages.Images.Add("x", Properties.Resources.WrongSymbol);

            lvTeamOneWrong.LargeImageList = newImages;
            lvTeamTwoWrong.LargeImageList = newImages;
            SetIconSpacing(lvTeamOneWrong, strikeSlotWidth, strikeIconH + 6);
            SetIconSpacing(lvTeamTwoWrong, strikeSlotWidth, strikeIconH + 6);

            if (strikeImages != null) strikeImages.Dispose();
            strikeImages = newImages;
        }

        // Adds 3 test X's, checks where Windows put the last one, then removes them
        private bool StrikesFit(ListView lv, int iconW)
        {
            lv.Items.Clear();
            for (int i = 0; i < MaxStrikes; i++)
                lv.Items.Add(new ListViewItem("", "x"));

            Point first = lv.Items[0].Position;
            Point last = lv.Items[MaxStrikes - 1].Position;

            bool sameRow = (last.Y == first.Y);
            bool insideList = (last.X + iconW + 8 <= lv.ClientSize.Width);   // 8px safety margin

            lv.Items.Clear();
            return sameRow && insideList;
        }
        private bool isStealMode = false;
        private bool roundLocked = false;
        private void AddStrike(int team)
        {
            if (roundLocked) return;   // round is over — no more strikes count for anyone

            if (isStealMode)
            {
                ListView stealList = (team == 1) ? lvTeamOneWrong : lvTeamTwoWrong;
                stealList.Items.Add(new ListViewItem("", "x"));
                isStealMode = false;
                roundLocked = true;    // steal attempt is used up — lock the round completely
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

            int margin = (int)(w * 0.02);      // distance from left/right edges
            int bottomGap = (int)(h * 0.04);   // distance from the bottom edge

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
