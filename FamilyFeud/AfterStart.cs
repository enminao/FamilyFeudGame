using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FamilyFeud
{
    public partial class AfterStart : Form
    {
        public AfterStart()
        {
            InitializeComponent();
        }

        private void btnGoBackToStart_Click(object sender, EventArgs e)
        {          
            frmStart backToStart = new frmStart();
          
            backToStart.Show();

            this.Hide();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Create create = new Create();

            create.Show();

            this.Hide();
        }

        private void lvQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AfterStart_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            LoadQuestionSetList();
            CenterLayout();
            
        }
        private void AfterStart_Resize(object sender, EventArgs e)
        {
            CenterLayout();
        }

        private void CenterLayout()
        {
            int centerX = this.ClientSize.Width / 2;



            lvQuestions.Width = 500;
            lvQuestions.Height = 250;

            lvQuestions.Left = centerX - (lvQuestions.Width / 2);
            lvQuestions.Top = 250;


            int buttonGap = 100; 
            int totalWidth = btnCreate.Width + buttonGap + btnGoBackToStart.Width; 

            int startX = centerX - (totalWidth / 2);
            int buttonsY = lvQuestions.Bottom + 20;

            btnCreate.Left = startX;
            btnCreate.Top = buttonsY;
            btnCreate.BringToFront();

            btnGoBackToStart.Left = startX + btnCreate.Width + buttonGap;
            btnGoBackToStart.Top = buttonsY;
            btnGoBackToStart.BringToFront();
        }

        private void LoadQuestionSetList()
        {
            lvQuestions.Items.Clear();

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FeudDb"].ConnectionString;
            var repo = new QuestionRepository(connStr);
            List<QuestionSetSummary> sets = repo.GetAllQuestionSets();


            foreach (var set in sets)
            {
                var item = new ListViewItem(set.QuestionText);
                item.Tag = set.QuestionSetId;
                lvQuestions.Items.Add(item);
            }
        }

        private void lvQuestions_DoubleClick(object sender, EventArgs e)
        {
            if (lvQuestions.SelectedItems.Count == 0)
                return;

            int selectedId = (int)lvQuestions.SelectedItems[0].Tag;

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FeudDb"].ConnectionString;
            var repo = new QuestionRepository(connStr);
            QuestionSet loadedSet = repo.LoadQuestionSet(selectedId);

            Game gameForm = new Game();
            gameForm.DisplayQuestionSet(loadedSet);
            gameForm.Show();
            this.Hide();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void lvQuestions_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
