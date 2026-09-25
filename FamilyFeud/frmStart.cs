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
        public frmStart()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void btnEditQuestion_Click(object sender, EventArgs e)
        //{
        //    AfterStart edit = new AfterStart();

        //    edit.Show();

        //    this.Hide();
        //}

        private void frmStart_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            CenterButtons();
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
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            int spacing = 40;
            int totalHeight = btnStart.Height + btnQuit.Height + spacing;

            int startY = centerY - (totalHeight / 2);

            btnStart.Left = centerX - (btnStart.Width / 2);
            btnStart.Top = startY;

            btnQuit.Left = centerX - (btnQuit.Width / 2);
            btnQuit.Top = btnStart.Bottom + spacing;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            AfterStart edit = new AfterStart();

            edit.Show();

            this.Hide();
        }
    }
}
