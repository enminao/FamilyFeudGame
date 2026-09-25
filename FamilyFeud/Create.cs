using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace FamilyFeud
{
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }

        private void Create_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            CenterLayout();
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
                    MessageBox.Show($"Points for Question {i} must be a whole number.");
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
                MessageBox.Show("Enter at least one answer.");
                return;
            }
            try
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FeudDb"].ConnectionString;
                var repo = new QuestionRepository(connStr);
                int newSetId = repo.SaveQuestionSet(set);

                MessageBox.Show($"Saved! Question Set ID: {newSetId}");

                
                txtbCreateQuestionare.Clear();
                for (int i = 1; i <= 8; i++)
                {
                    ((TextBox)this.Controls.Find($"txtbCreateQuestion{i}", true)[0]).Clear();
                    ((TextBox)this.Controls.Find($"txtbCreatePoints{i}", true)[0]).Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed: " + ex.Message);
            }

        }

        private void btnGoBackToAfterStart_Click(object sender, EventArgs e)
        {
            AfterStart backToAfterStart = new AfterStart();

            backToAfterStart.Show();

            this.Hide();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //    this.Close();
        }

        private void CenterLayout()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            int centerX = w / 2;

            // ----- Top Question row (blue) -----
            int qTotalWidth = lblTopQuestion.Width + 10 + txtbCreateQuestionare.Width;
            lblTopQuestion.Left = centerX - (qTotalWidth / 2);
            lblTopQuestion.Top = (int)(h * 0.05);
            txtbCreateQuestionare.Left = lblTopQuestion.Right + 10;
            txtbCreateQuestionare.Top = lblTopQuestion.Top - 3;

            // ----- Answer grid (red) -----
            int topMargin = (int)(h * 0.20);
            int rowGap = (int)(h * 0.08);
            int labelTextboxGap = 10;
            int pairGap = 20;

            // Left column: Questions 1-4
            int leftColLeft = (int)(w * 0.10);
            Label[] leftLabels = { lblQ1, lblQ2, lblQ3, lblQ4 };
            TextBox[] leftBoxes = { txtbCreateQuestion1, txtbCreateQuestion2, txtbCreateQuestion3, txtbCreateQuestion4 };
            Label[] leftPLabels = { lblP1, lblP2, lblP3, lblP4 };
            TextBox[] leftPBoxes = { txtbCreatePoints1, txtbCreatePoints2, txtbCreatePoints3, txtbCreatePoints4 };

            for (int i = 0; i < 4; i++)
            {
                int rowTop = topMargin + (rowGap * i);
                leftLabels[i].Left = leftColLeft;
                leftLabels[i].Top = rowTop;
                leftBoxes[i].Left = leftLabels[i].Right + labelTextboxGap;
                leftBoxes[i].Top = rowTop - 3;
                leftPLabels[i].Left = leftBoxes[i].Right + pairGap;
                leftPLabels[i].Top = rowTop;
                leftPBoxes[i].Left = leftPLabels[i].Right + labelTextboxGap;
                leftPBoxes[i].Top = rowTop - 3;
            }

            // Right column: Questions 5-8
            int rightColLeft = (int)(w * 0.52);
            Label[] rightLabels = { lblQ5, lblQ6, lblQ7, lblQ8 };
            TextBox[] rightBoxes = { txtbCreateQuestion5, txtbCreateQuestion6, txtbCreateQuestion7, txtbCreateQuestion8 };
            Label[] rightPLabels = { lblP5, lblP6, lblP7, lblP8 };
            TextBox[] rightPBoxes = { txtbCreatePoints5, txtbCreatePoints6, txtbCreatePoints7, txtbCreatePoints8 };

            for (int i = 0; i < 4; i++)
            {
                int rowTop = topMargin + (rowGap * i);
                rightLabels[i].Left = rightColLeft;
                rightLabels[i].Top = rowTop;
                rightBoxes[i].Left = rightLabels[i].Right + labelTextboxGap;
                rightBoxes[i].Top = rowTop - 3;
                rightPLabels[i].Left = rightBoxes[i].Right + pairGap;
                rightPLabels[i].Top = rowTop;
                rightPBoxes[i].Left = rightPLabels[i].Right + labelTextboxGap;
                rightPBoxes[i].Top = rowTop - 3;
            }

            // ----- Bottom buttons (yellow) -----
            int sideInset = (int)(w * 0.08);
            int bottomGap = (int)(h * 0.04);

            btnFinishCreate.Left = sideInset;
            btnFinishCreate.Top = h - bottomGap - btnFinishCreate.Height;

            btnGoBackToAfterStart.Left = w - sideInset - btnGoBackToAfterStart.Width;
            btnGoBackToAfterStart.Top = h - bottomGap - btnGoBackToAfterStart.Height;
        }
    }
}
