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
    public partial class AfterStart : Form
    {
        private readonly Form startForm;
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; 
                return cp;
            }
        }
        public AfterStart()
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

            lvQuestions.BeginUpdate();
            LoadQuestionSetList();
            StyleButton(btnCreate, Properties.Resources.CREATEBUTTON);
            StyleButton(btnGoBackToStart, Properties.Resources.GOBACKBUTTON);
            StyleList();
            SetupScrollBar();
            CenterLayout();
            SetupContextMenu();
            lvQuestions.EndUpdate();

            FormHelper.EnableDoubleBuffer(this);  

            VisibleChanged += (s, e) =>
            {
                if (!Visible) return;
                lvQuestions.BeginUpdate();
                LoadQuestionSetList();
                lvQuestions.EndUpdate();
            };

            ResumeLayout(true);
        }
        public Form StartForm { get { return startForm; } }

        public AfterStart(Form startForm) : this()
        {
            this.startForm = startForm;
        }

        private void btnGoBackToStart_Click(object sender, EventArgs e)
        {
            startForm.Show();
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Create create = new Create(this);
            create.Show();
            this.Hide();
        }

        private void lvQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AfterStart_Load(object sender, EventArgs e)
        {

        }
        private void AfterStart_Resize(object sender, EventArgs e)
        {
            CenterLayout();
        }

        private void CenterLayout()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            int centerX = w / 2;

            pbQuestions.SizeMode = PictureBoxSizeMode.Zoom;
            pbQuestions.Width = (int)(w * 0.30);
            pbQuestions.Height = (int)(h * 0.10);
            pbQuestions.Left = centerX - (pbQuestions.Width / 2);
            pbQuestions.Top = (int)(h * 0.13);

            lvQuestions.Width = (int)(w * 0.42);
            lvQuestions.Height = (int)(h * 0.50);
            lvQuestions.Left = centerX - (lvQuestions.Width / 2);
            lvQuestions.Top = (int)(h * 0.285);

            int pad = (int)(w * 0.02);
            pbListFrame.Left = lvQuestions.Left - pad;
            pbListFrame.Top = lvQuestions.Top - pad;
            pbListFrame.Width = lvQuestions.Width + pad * 2;
            pbListFrame.Height = lvQuestions.Height + pad * 2;
            pbListFrame.SendToBack();
            lvQuestions.BringToFront();

            if (lvQuestions.Columns.Count > 0)
                lvQuestions.Columns[0].Width = lvQuestions.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

            float btnAspect = 1000f / 280f;
            btnCreate.Width = (int)(w * 0.16);
            btnCreate.Height = (int)(btnCreate.Width / btnAspect);
            btnGoBackToStart.Width = btnCreate.Width;
            btnGoBackToStart.Height = btnCreate.Height;

            int buttonsY = (int)(h * 0.84);
            int offset = (int)(w * 0.21);

            btnCreate.Left = centerX - offset - (btnCreate.Width / 2);
            btnCreate.Top = buttonsY;
            btnCreate.BringToFront();

            btnGoBackToStart.Left = centerX + offset - (btnGoBackToStart.Width / 2);
            btnGoBackToStart.Top = buttonsY;
            btnGoBackToStart.BringToFront();

            if (navyBar != null)
            {
                int sbW = SystemInformation.VerticalScrollBarWidth;
                navyBar.Width = sbW;
                navyBar.Height = lvQuestions.ClientSize.Height;
                navyBar.Left = lvQuestions.Right - sbW;
                navyBar.Top = lvQuestions.Top;
                navyBar.BringToFront();
            }
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

            Game gameForm = new Game(this);
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
        private ContextMenuStrip questionMenu;

        private QuestionRepository GetRepo()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["FeudDb"].ConnectionString;
            return new QuestionRepository(connStr);
        }

        private void SetupContextMenu()
        {
            questionMenu = new ContextMenuStrip();

            var renameItem = new ToolStripMenuItem("Rename");
            var deleteItem = new ToolStripMenuItem("Delete");
            renameItem.Click += RenameQuestion_Click;
            deleteItem.Click += DeleteQuestion_Click;

            questionMenu.Items.Add(renameItem);
            questionMenu.Items.Add(deleteItem);

            questionMenu.Opening += (s, e) =>
            {
                if (lvQuestions.SelectedItems.Count == 0)
                    e.Cancel = true;
            };

            lvQuestions.ContextMenuStrip = questionMenu;
            lvQuestions.AfterLabelEdit += lvQuestions_AfterLabelEdit;
        }

        private void RenameQuestion_Click(object sender, EventArgs e)
        {
            if (lvQuestions.SelectedItems.Count == 0) return;

            lvQuestions.LabelEdit = true;            
            lvQuestions.SelectedItems[0].BeginEdit(); 
        }

        private void lvQuestions_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            lvQuestions.LabelEdit = false;

            string newText = e.Label == null ? "" : e.Label.Trim();  
            if (newText == "")
            {
                e.CancelEdit = true;
                return;
            }

            try
            {
                int id = (int)lvQuestions.Items[e.Item].Tag;
                GetRepo().RenameQuestionSet(id, newText);
            }
            catch (Exception ex)
            {
                e.CancelEdit = true;
                MessageBox.Show("Rename failed: " + ex.Message);
            }
        }

        private void DeleteQuestion_Click(object sender, EventArgs e)
        {
            if (lvQuestions.SelectedItems.Count == 0) return;

            ListViewItem item = lvQuestions.SelectedItems[0];

            DialogResult result = MessageBox.Show(
                $"Delete \"{item.Text}\"? This can't be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                int id = (int)item.Tag;
                GetRepo().DeleteQuestionSet(id);
                lvQuestions.Items.Remove(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed: " + ex.Message);
            }
        }
        private void StyleButton(Button btn, Image img)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.BackColor = Color.Transparent;
            btn.Text = "";
            btn.BackgroundImage = img;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
            btn.TabStop = false;
        }
        private void StyleList()
        {
            lvQuestions.HeaderStyle = ColumnHeaderStyle.None;
            lvQuestions.BorderStyle = BorderStyle.None;
            lvQuestions.BackColor = Color.FromArgb(14, 40, 98);   
            lvQuestions.ForeColor = Color.FromArgb(255, 236, 170);
            lvQuestions.GridLines = false;
            lvQuestions.FullRowSelect = true;
            lvQuestions.OwnerDraw = true;
            lvQuestions.DrawItem += lvQuestions_DrawItem;
            lvQuestions.DrawColumnHeader += (s, e) => e.DrawDefault = true;
        }

        private void lvQuestions_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Rectangle r = e.Bounds;
            bool selected = e.Item.Selected;

            using (var bg = new SolidBrush(selected ? Color.FromArgb(243, 169, 31) : lvQuestions.BackColor))
                e.Graphics.FillRectangle(bg, r);

            using (var pen = new Pen(Color.FromArgb(40, 90, 170)))
                e.Graphics.DrawLine(pen, r.Left + 8, r.Bottom - 1, r.Right - 8, r.Bottom - 1);

            Color textColor = selected ? Color.FromArgb(58, 34, 5) : Color.FromArgb(255, 236, 170);
            TextRenderer.DrawText(e.Graphics, e.Item.Text, lvQuestions.Font,
                new Rectangle(r.Left + 14, r.Top, r.Width - 20, r.Height), textColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
        }
        private NavyScrollBar navyBar;
        private Timer scrollSync;

        private void SetupScrollBar()
        {
            navyBar = new NavyScrollBar();
            this.Controls.Add(navyBar);

            navyBar.Scrolled += (s, e) =>
            {
                if (navyBar.Value < lvQuestions.Items.Count)
                    lvQuestions.TopItem = lvQuestions.Items[navyBar.Value];
            };

            scrollSync = new Timer { Interval = 40 };
            scrollSync.Tick += (s, e) =>
            {
                if (lvQuestions.Items.Count == 0) { navyBar.Visible = false; return; }

                int itemH = lvQuestions.Items[0].Bounds.Height;
                int newMax = lvQuestions.Items.Count;
                int newPage = Math.Max(1, lvQuestions.ClientSize.Height / itemH);

                bool changed = navyBar.Maximum != newMax || navyBar.PageSize != newPage;
                navyBar.Maximum = newMax;
                navyBar.PageSize = newPage;

                bool shouldShow = navyBar.Maximum > navyBar.PageSize;
                if (navyBar.Visible != shouldShow) navyBar.Visible = shouldShow;

                if (!navyBar.IsDragging && lvQuestions.TopItem != null)
                    navyBar.Value = lvQuestions.TopItem.Index; 

                if (changed) navyBar.Invalidate();
            };
            scrollSync.Start();
        }
    }

    public class NavyScrollBar : Control
    {
        public int Maximum = 0; 
        public int PageSize = 1; 
        public event EventHandler Scrolled;
        public bool IsDragging { get; private set; }

        private int _value;
        private int _dragOffset;

        public int Value
        {
            get { return _value; }
            set
            {
                int max = Math.Max(0, Maximum - PageSize);
                value = Math.Max(0, Math.Min(value, max));
                if (value == _value) return;
                _value = value;
                Invalidate();
            }
        }

        public NavyScrollBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        private int Range { get { return Math.Max(1, Maximum - PageSize); } }

        private Rectangle ThumbRect()
        {
            int thumbH = (int)((float)Height * PageSize / Math.Max(Maximum, 1));
            thumbH = Math.Min(Math.Max(thumbH, 30), Height - 4);
            int space = Height - 4 - thumbH;
            int y = 2 + (int)((float)_value / Range * space);
            return new Rectangle(3, y, Width - 6, thumbH);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(14, 40, 98));
            if (Maximum <= PageSize) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle t = ThumbRect();
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            using (var brush = new SolidBrush(Color.FromArgb(243, 169, 31)))  
            {
                int d = t.Width;
                path.AddArc(t.X, t.Y, d, d, 180, 180);
                path.AddArc(t.X, t.Bottom - d, d, d, 0, 180);
                path.CloseFigure();
                e.Graphics.FillPath(brush, path);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Rectangle t = ThumbRect();
            if (t.Contains(e.Location))
            {
                IsDragging = true;
                _dragOffset = e.Y - t.Y;
            }
            else
            {
                Value += (e.Y < t.Y ? -PageSize : PageSize); 
                if (Scrolled != null) Scrolled(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!IsDragging) return;
            int space = Height - 4 - ThumbRect().Height;
            if (space <= 0) return;
            Value = (int)Math.Round((e.Y - _dragOffset - 2) / (float)space * Range);
            if (Scrolled != null) Scrolled(this, EventArgs.Empty);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsDragging = false;
     
        }
        

    }

}
