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
using System.Media;

namespace FamilyFeud
{

    public partial class Game : Form
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

        private readonly Form afterStartForm;
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
                {
                    Panel cover = answerCovers[i - 1];
                    bool hasAnswer = (match != null) && !string.IsNullOrWhiteSpace(match.AnswerText);

                    cover.Visible = true;
                    cover.Enabled = hasAnswer;
                    cover.Cursor = hasAnswer ? Cursors.Hand : Cursors.Default;
                    cover.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(
                        hasAnswer ? $"cover{i}" : "cover_blank");
                    cover.BackgroundImage = Res(hasAnswer ? $"cover{i}" : "cover_blank");
                }
            }
        }

        public Game()
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

            this.Paint += Game_Paint;
            CreateCovers();
            CreateAnswerFrame();
            CreateTeamFrames();
            CreateStrikeFlash();

            this.Resize += (s, e) =>
            {
                CenterQuestion();
                ArrangeTeamBoxes();
                ArrangeCovers();
                ArrangeAnswerFrame();
                ArrangeBottomButtons();
                ArrangeStrikeFlash();
            };

            FormHelper.EnableDoubleBuffer(this); 
            ResumeLayout(true);
        }
        public Game(Form afterStartForm) : this()
        {
            this.afterStartForm = afterStartForm;
        }

        private void Game_Load(object sender, EventArgs e)
        {

            this.KeyPreview = true;
            this.KeyDown -= Game_KeyDown;
            this.KeyDown += Game_KeyDown;

            this.KeyUp -= Game_KeyUp;
            this.KeyUp += Game_KeyUp;

            lbQuestionare.AutoSize = false;
            lbQuestionare.BackColor = Color.FromArgb(14, 40, 98);
            lbQuestionare.ForeColor = Color.FromArgb(255, 236, 170);
            lbQuestionare.TextAlign = ContentAlignment.MiddleLeft;
            lbQuestionare.Font = new Font(lbQuestionare.Font.FontFamily, 24f, FontStyle.Bold);


            foreach (Button b in new[] { btnFinish, btnGoBackToStart })
            {
                b.UseVisualStyleBackColor = false;
                b.BackColor = Color.Transparent;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                b.TabStop = false;                    
                b.BackgroundImageLayout = ImageLayout.Zoom;
                b.Size = new Size(200, 56);           
            }

            CenterQuestion();
            ArrangeTeamBoxes();     
            SetupStrikeLists();
            lvTeamOneWrong.Visible = false;
            lvTeamTwoWrong.Visible = false;
            ArrangeTeamBoxes();    
            ArrangeCovers();
            ArrangeAnswerFrame();
            ArrangeBottomButtons();
            ArrangeStrikeFlash();

            UpdateScoreLabels();
            
            label1.Click += (s, ev) => SetActiveTeam(1);
            label2.Click += (s, ev) => SetActiveTeam(2);

            this.BeginInvoke(new Action(() =>
            {
                int firstTeam = TeamPrompt.Ask(this);
                SetActiveTeam(firstTeam);
            }));
        }

        private static readonly Dictionary<string, Image> imageCache = new Dictionary<string, Image>();

        private static Image Res(string name)
        {
            Image img;
            if (!imageCache.TryGetValue(name, out img))
            {
                img = (Image)Properties.Resources.ResourceManager.GetObject(name);
                imageCache[name] = img;
            }
            return img;
        }

        private void btnGoBackToStart_Click(object sender, EventArgs e)
        {
            if (ConfirmPrompt.Ask(this, "Are you sure you want to go back?"))
            {
                if (afterStartForm != null) afterStartForm.Show();
                else new AfterStart().Show();
                this.Close();
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
                p.BackColor = Color.FromArgb(14, 40, 98);
                p.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject($"cover{i + 1}");
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.BorderStyle = BorderStyle.None;
                p.Cursor = Cursors.Hand;
                p.Click += Cover_Click;
                p.BackgroundImage = Res($"cover{i + 1}");

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

            PushUndo();                                 

            if (isStealMode)
            {
                if (activeTeam == 1) { team1Score += team2Score + points; team2Score = 0; }
                else { team2Score += team1Score + points; team1Score = 0; }

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



        private void GetRowLayout(out int[] rowTops, out int rowHeight)
        {
            int h = this.ClientSize.Height;

            rowHeight = (int)(h * 0.1207);      
            int firstTop = (int)(h * 0.278);    
            int pitch = (int)(h * 0.1467);     

            rowTops = new int[4];
            for (int i = 0; i < 4; i++)
                rowTops[i] = firstTop + i * pitch;
        }

        private void ArrangeCovers()
        {
            if (answerCovers == null) return;

            int w = this.ClientSize.Width;

            int[] rowTops;
            int rowHeight;
            GetRowLayout(out rowTops, out rowHeight);

            int leftColLeft = (int)(w * 0.206);
            int leftColRight = (int)(w * 0.490);
            int rightColLeft = (int)(w * 0.510);
            int rightColRight = (int)(w * 0.794);

            for (int i = 0; i < 8; i++)
            {
                bool isLeft = i < 4;
                int left = isLeft ? leftColLeft : rightColLeft;
                int right = isLeft ? leftColRight : rightColRight;

                answerCovers[i].SetBounds(left, rowTops[i % 4], right - left, rowHeight);
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

            Color gold = Color.FromArgb(255, 214, 102);

            bool t1 = (team == 1);
            bool t2 = (team == 2);

            label1.ForeColor = t1 ? gold : Color.White;
            label2.ForeColor = t2 ? gold : Color.White;

            if (pbTeam1Frame != null) pbTeam1Frame.Image = t1 ? teamFrameActive : teamFrameNormal;
            if (pbTeam2Frame != null) pbTeam2Frame.Image = t2 ? teamFrameActive : teamFrameNormal;
        }


        private void CenterQuestion()
        {
            int formWidth = this.ClientSize.Width;
            int centerX = formWidth / 2;

            int h = this.ClientSize.Height;

            int picW = (int)(formWidth * 0.1289);
            int picH = (int)(h * 0.1);
            int picLeft = (int)(formWidth * 0.0939);
            int picTop = (int)(h * 0.0687);
            pbQuestionLabel.SetBounds(picLeft, picTop, picW, picH);

            int frameLeft = (int)(formWidth * 0.2342);
            int frameTop = (int)(h * 0.0724);
            int frameW = (int)(formWidth * 0.6688);
            int frameH = (int)(h * 0.1);
            pbQuestionFrame.SetBounds(frameLeft, frameTop, frameW, frameH);

            int pad = (int)(formWidth * 0.0115);
            lbQuestionare.AutoSize = false;
            lbQuestionare.Left = pbQuestionFrame.Left + pad;
            lbQuestionare.Width = pbQuestionFrame.Width - pad * 2;

            int padV = (int)(h * 0.025);
            lbQuestionare.Top = pbQuestionFrame.Top + padV;
            lbQuestionare.Height = pbQuestionFrame.Height - padV * 2;

            pbQuestionFrame.SendToBack();
            lbQuestionare.BringToFront();

            int[] rowTops;
            int rowHeight;
            GetRowLayout(out rowTops, out rowHeight);

            int leftMargin = (int)(formWidth * 0.22);
            int leftMarginp = (int)(formWidth * 0.40);
            Label[] leftAnswers = { lblQuestion1, lblQuestion2, lblQuestion3, lblQuestion4 };
            Label[] leftPoints = { lblQuestionPoints1, lblQuestionPoints2, lblQuestionPoints3, lblQuestionPoints4 };

            for (int i = 0; i < 4; i++)
            {
                int y = rowTops[i] + (rowHeight - leftAnswers[i].Font.Height) / 2;
                leftAnswers[i].Left = leftMargin;
                leftAnswers[i].Top = y;
                leftPoints[i].Left = leftMarginp;
                leftPoints[i].Top = y;
            }

            int rightColumnLeft = (int)(formWidth * 0.524);
            int rightMarginp = (int)(formWidth * 0.225);
            Label[] rightAnswers = { lblQuestion5, lblQuestion6, lblQuestion7, lblQuestion8 };
            Label[] rightPoints = { lblQuestionPoints5, lblQuestionPoints6, lblQuestionPoints7, lblQuestionPoints8 };

            for (int i = 0; i < 4; i++)
            {
                int y = rowTops[i] + (rowHeight - rightAnswers[i].Font.Height) / 2;

                rightAnswers[i].AutoSize = true;
                rightAnswers[i].TextAlign = ContentAlignment.TopLeft;
                rightAnswers[i].Left = rightColumnLeft;
                rightAnswers[i].Top = y;

                rightPoints[i].Left = formWidth - rightMarginp - rightPoints[i].Width;
                rightPoints[i].Top = y;
            }
        }

        private void ArrangeTeamBoxes()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;


            int boxWidth = (int)(w * 0.14);
            int boxTop = (int)(h * 0.37);
            int boxHeight = (int)(h * 0.31);

            int leftBoxLeft = (int)(w * 0.02);
            int rightBoxLeft = w - leftBoxLeft - boxWidth - ListExtraWidth;

            PlaceTeamInBox(label1, lblTeamOnePoints, lvTeamOneWrong,
                           rightBoxLeft, boxTop, boxWidth, boxHeight);

            PlaceTeamInBox(label2, lblTeamTwoPoints, lvTeamTwoWrong,
                           leftBoxLeft, boxTop, boxWidth, boxHeight);

            ArrangeTeamFrames();
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

        private void PlayBuzzer()
        {
            using (SoundPlayer player = new SoundPlayer(Properties.Resources.BuzzerSound))
            {
                player.Play();
            }
        }

        private bool isStealMode = false;
        private bool roundLocked = false;
        private void AddStrike(int team)
        {
            if (roundLocked) return;

            if (isStealMode)
            {
                PushUndo();                                     
                ListView stealList = (team == 1) ? lvTeamOneWrong : lvTeamTwoWrong;
                stealList.Items.Add(new ListViewItem("", "x"));
                PlayBuzzer();

                using (SoundPlayer player = new SoundPlayer(Properties.Resources.BuzzerSound))
                {
                    player.Play();
                }

                isStealMode = false;
                roundLocked = true;
                return;
            }

            ListView lv = (team == 1) ? lvTeamOneWrong : lvTeamTwoWrong;
            if (lv.Items.Count >= MaxStrikes) return;

            PushUndo();
            lv.Items.Add(new ListViewItem("", "x"));
            PlayBuzzer();

            using (SoundPlayer player = new SoundPlayer(Properties.Resources.BuzzerSound))
            {
                player.Play();
            }

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
            undoStack.Clear();
            RefreshStrikes();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
            {
                if (!xKeyHeld)
                {
                    xKeyHeld = true;
                    AddStrike(activeTeam);
                    RefreshStrikes();
                    FlashStrike();
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.C)
            {
                ClearStrikes();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.U)                        
            {
                if (!uKeyHeld)
                {
                    uKeyHeld = true;
                    Undo();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
                xKeyHeld = false;
            if (e.KeyCode == Keys.U)                              
                uKeyHeld = false;
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

            int centerY = (int)(h * 0.919);
            int finishCenterX = (int)(w * 0.102);
            int goBackCenterX = (int)(w * 0.892);

            btnFinish.Left = finishCenterX - btnFinish.Width / 2;
            btnFinish.Top = centerY - btnFinish.Height / 2;

            btnGoBackToStart.Left = goBackCenterX - btnGoBackToStart.Width / 2;
            btnGoBackToStart.Top = centerY - btnGoBackToStart.Height / 2;
        }

        private Stack<GameState> undoStack = new Stack<GameState>();
        private bool uKeyHeld = false;

        private class GameState
        {
            public int Team1Score;
            public int Team2Score;
            public int ActiveTeam;
            public bool IsStealMode;
            public bool RoundLocked;
            public int Team1StrikeCount;
            public int Team2StrikeCount;
            public bool[] CoverVisible;
        }

        private GameState CaptureState()
        {
            return new GameState
            {
                Team1Score = team1Score,
                Team2Score = team2Score,
                ActiveTeam = activeTeam,
                IsStealMode = isStealMode,
                RoundLocked = roundLocked,
                Team1StrikeCount = lvTeamOneWrong.Items.Count,
                Team2StrikeCount = lvTeamTwoWrong.Items.Count,
                CoverVisible = answerCovers.Select(p => p.Visible).ToArray()
            };
        }

        private void PushUndo()
        {
            undoStack.Push(CaptureState());
        }

        private void Undo()
        {
            if (undoStack.Count == 0) return;

            GameState s = undoStack.Pop();

            team1Score = s.Team1Score;
            team2Score = s.Team2Score;
            isStealMode = s.IsStealMode;
            roundLocked = s.RoundLocked;

            SetActiveTeam(s.ActiveTeam);
            UpdateScoreLabels();

            lvTeamOneWrong.Items.Clear();
            for (int i = 0; i < s.Team1StrikeCount; i++)
                lvTeamOneWrong.Items.Add(new ListViewItem("", "x"));

            lvTeamTwoWrong.Items.Clear();
            for (int i = 0; i < s.Team2StrikeCount; i++)
                lvTeamTwoWrong.Items.Add(new ListViewItem("", "x"));

            for (int i = 0; i < answerCovers.Length; i++)
                answerCovers[i].Visible = s.CoverVisible[i];

            RefreshStrikes();

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

        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (ConfirmPrompt.Ask(this, "Finish the game?"))
            {
                var afterStart = afterStartForm as AfterStart;
                Form start = afterStart != null ? afterStart.StartForm : null; 

                if (start != null) start.Show();
                else new frmStart().Show();

                if (afterStartForm != null) afterStartForm.Close();
                this.Close();
            }
        }

        private FlashOverlay flashOverlay;
        private Timer strikeFlashTimer;

        private void CreateStrikeFlash()
        {
            strikePic = RemoveWhiteBackground(Properties.Resources.WrongSymbol);
            flashOverlay = new FlashOverlay(strikePic);

            strikeFlashTimer = new Timer();
            strikeFlashTimer.Interval = 1500;  
            strikeFlashTimer.Tick += (s, e) =>
            {
                strikeFlashTimer.Stop();
                flashOverlay.Hide();
            };
        }

        private void ArrangeStrikeFlash()
        {

        }

        private void FlashStrike()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            Point p = this.PointToScreen(new Point((int)(w * 0.283), (int)(h * 0.198)));
            flashOverlay.Bounds = new Rectangle(p, new Size((int)(w * 0.445), (int)(h * 0.668)));

            strikeFlashTimer.Stop();
            if (!flashOverlay.Visible) flashOverlay.Show(this);
            strikeFlashTimer.Start();
        }

        private static Bitmap RemoveWhiteBackground(Image source, int tolerance = 40)
        {
            Bitmap bmp = new Bitmap(source);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (c.R >= 255 - tolerance && c.G >= 255 - tolerance && c.B >= 255 - tolerance)
                        bmp.SetPixel(x, y, Color.Transparent);
                }
            }
            return bmp;
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (flashOverlay != null) flashOverlay.Dispose();
            base.OnFormClosed(e);
        }

        private PictureBox pbAnswerFrame;

        private PictureBox[] rowFrames;
        private Label[] answerLabels;
        private Label[] pointsLabels;

        private void CreateAnswerFrame()
        {
            pbAnswerFrame = new PictureBox();
            pbAnswerFrame.Name = "pbAnswerFrame";
            pbAnswerFrame.Image = Properties.Resources.FRAMEREALOUTER;
            pbAnswerFrame.SizeMode = PictureBoxSizeMode.StretchImage;
            pbAnswerFrame.BackColor = Color.FromArgb(14, 40, 98);
            this.Controls.Add(pbAnswerFrame);
            pbAnswerFrame.SendToBack();

            answerLabels = new Label[] { lblQuestion1, lblQuestion2, lblQuestion3, lblQuestion4,
                                 lblQuestion5, lblQuestion6, lblQuestion7, lblQuestion8 };
            pointsLabels = new Label[] { lblQuestionPoints1, lblQuestionPoints2, lblQuestionPoints3, lblQuestionPoints4,
                                 lblQuestionPoints5, lblQuestionPoints6, lblQuestionPoints7, lblQuestionPoints8 };

            rowFrames = new PictureBox[8];
            for (int i = 0; i < 8; i++)
            {
                int index = i;

                var pb = new PictureBox();
                pb.Name = $"pbRowFrame{i + 1}";
                pb.Image = Properties.Resources.FRAMEROW;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.BackColor = Color.FromArgb(14, 40, 98);
                pb.Paint += (s, e) => DrawRowText(e.Graphics, pb, answerLabels[index].Text, pointsLabels[index].Text);

                rowFrames[i] = pb;
                this.Controls.Add(pb);

                answerLabels[i].Visible = false;
                pointsLabels[i].Visible = false;
                answerLabels[i].TextChanged += (s, e) => rowFrames[index].Invalidate();
                pointsLabels[i].TextChanged += (s, e) => rowFrames[index].Invalidate();
            }
        }

        private void DrawRowText(Graphics g, PictureBox pb, string answer, string points)
        {
            int w = pb.Width;
            int h = pb.Height;

            using (Font font = new Font("Comic Sans MS", h * 0.30f, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix;

                Rectangle answerRect = new Rectangle((int)(w * 0.035), 0, (int)(w * 0.74), h);
                Rectangle pointsRect = new Rectangle((int)(w * 0.80), 0, (int)(w * 0.185), h);

                DrawShadowText(g, answer, font, answerRect, flags | TextFormatFlags.Left);
                DrawShadowText(g, points, font, pointsRect, flags | TextFormatFlags.HorizontalCenter);
            }
        }

        private void DrawShadowText(Graphics g, string text, Font font, Rectangle r, TextFormatFlags flags)
        {
            if (string.IsNullOrEmpty(text)) return;

            Rectangle shadow = r;
            shadow.Offset(2, 2);
            TextRenderer.DrawText(g, text, font, shadow, Color.FromArgb(5, 15, 50), flags);
            TextRenderer.DrawText(g, text, font, r, Color.White, flags);
        }

        private void ArrangeAnswerFrame()
        {
            if (pbAnswerFrame == null) return;

            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            int[] rowTops;
            int rowHeight;
            GetRowLayout(out rowTops, out rowHeight);

            int pad = (int)(h * 0.026);  

            int left = (int)(w * 0.206) - pad;
            int right = (int)(w * 0.794) + pad;
            int top = rowTops[0] - pad;
            int bottom = rowTops[3] + rowHeight + pad;

            pbAnswerFrame.SetBounds(left, top, right - left, bottom - top);
            pbAnswerFrame.SendToBack();

            for (int i = 0; i < 8; i++)
            {
                rowFrames[i].Bounds = answerCovers[i].Bounds;
                rowFrames[i].BringToFront();
            }

            for (int i = 0; i < 8; i++)
                answerCovers[i].BringToFront();
        }

        private PictureBox pbTeam1Frame;
        private PictureBox pbTeam2Frame;

        private Image teamFrameNormal;
        private Image teamFrameActive;


        private void CreateTeamFrames()
        {
            teamFrameNormal = Properties.Resources.FRAMETEAM;
            teamFrameActive = Properties.Resources.FRAMETEAM_ACTIVE;
            pbTeam1Frame = MakeTeamFrame("pbTeam1Frame");
            pbTeam2Frame = MakeTeamFrame("pbTeam2Frame");

            Color teamNavy = Color.FromArgb(14, 40, 98);
            Label[] teamLabels = { label1, label2, lblTeamOnePoints, lblTeamTwoPoints };
            foreach (Label lbl in teamLabels)
            {
                lbl.BackColor = teamNavy;    
                lbl.BringToFront();
            }
            lblTeamOnePoints.ForeColor = Color.White;
            lblTeamTwoPoints.ForeColor = Color.White;

            label1.ForeColor = Color.White;                        
            label2.ForeColor = Color.White;
        }

        private PictureBox MakeTeamFrame(string name)
        {
            PictureBox pb = new PictureBox();
            pb.Name = name;
            pb.Image = teamFrameNormal;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BackColor = Color.Transparent;
            this.Controls.Add(pb);
            pb.SendToBack();
            return pb;
        }

        private void ArrangeTeamFrames()
        {
            if (pbTeam1Frame == null) return;

            FitTeamFrame(pbTeam1Frame, label1, lblTeamOnePoints, lvTeamOneWrong);
            FitTeamFrame(pbTeam2Frame, label2, lblTeamTwoPoints, lvTeamTwoWrong);
        }

        private void FitTeamFrame(PictureBox frame, Label nameLabel, Label pointsLabel, ListView strikeList)
        {
            int padV = 12;     
            int inset = 12;        
            int pointsRaise = 10;  

            int top = nameLabel.Top - padV;
            int bottom = pointsLabel.Bottom + padV;  

            frame.SetBounds(strikeList.Left, top, strikeList.Width, bottom - top);

            nameLabel.SetBounds(frame.Left + inset, nameLabel.Top, frame.Width - inset * 2, nameLabel.Height);
            pointsLabel.SetBounds(frame.Left + inset, pointsLabel.Top - pointsRaise, frame.Width - inset * 2, pointsLabel.Height);

            frame.SendToBack();
        }

        private Bitmap strikePic;

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            if (strikePic == null || strikeImages == null) return;

            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            DrawStrikes(g, lvTeamOneWrong);
            DrawStrikes(g, lvTeamTwoWrong);
        }

        private void DrawStrikes(Graphics g, ListView lv)
        {
            int count = Math.Min(lv.Items.Count, MaxStrikes);
            int iconW = strikeImages.ImageSize.Width;
            int iconH = strikeImages.ImageSize.Height;

            for (int i = 0; i < count; i++)
            {
                int x = lv.Left + i * strikeSlotWidth + (strikeSlotWidth - iconW) / 2;
                int y = lv.Top + 2;
                g.DrawImage(strikePic, new Rectangle(x, y, iconW, iconH));
            }
        }

        private void RefreshStrikes()
        {
            this.Invalidate(Rectangle.Inflate(lvTeamOneWrong.Bounds, 4, 4));
            this.Invalidate(Rectangle.Inflate(lvTeamTwoWrong.Bounds, 4, 4));
        }
    }

    public class FlashOverlay : Form
    {
        public FlashOverlay(Image img)
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.Magenta;
            TransparencyKey = Color.Magenta;
            BackgroundImage = img;
            BackgroundImageLayout = ImageLayout.Zoom;
        }
        protected override bool ShowWithoutActivation { get { return true; } }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x08000000; 
                cp.ExStyle |= 0x00000080; 
                return cp;
            }
        }

    }
}
