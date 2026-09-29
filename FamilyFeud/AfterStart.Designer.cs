namespace FamilyFeud
{
    partial class AfterStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGoBackToStart = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lvQuestions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbQuestions = new System.Windows.Forms.PictureBox();
            this.pbListFrame = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuestions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbListFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGoBackToStart
            // 
            this.btnGoBackToStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoBackToStart.BackColor = System.Drawing.Color.Transparent;
            this.btnGoBackToStart.BackgroundImage = global::FamilyFeud.Properties.Resources.GOBACKBUTTON;
            this.btnGoBackToStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoBackToStart.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBackToStart.Location = new System.Drawing.Point(299, 428);
            this.btnGoBackToStart.Name = "btnGoBackToStart";
            this.btnGoBackToStart.Size = new System.Drawing.Size(181, 62);
            this.btnGoBackToStart.TabIndex = 1;
            this.btnGoBackToStart.UseVisualStyleBackColor = false;
            this.btnGoBackToStart.Click += new System.EventHandler(this.btnGoBackToStart_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.BackColor = System.Drawing.Color.Transparent;
            this.btnCreate.BackgroundImage = global::FamilyFeud.Properties.Resources.CREATEBUTTON;
            this.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCreate.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(567, 428);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(189, 62);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lvQuestions
            // 
            this.lvQuestions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lvQuestions.BackgroundImageTiled = true;
            this.lvQuestions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvQuestions.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvQuestions.GridLines = true;
            this.lvQuestions.HideSelection = false;
            this.lvQuestions.Location = new System.Drawing.Point(330, 151);
            this.lvQuestions.Name = "lvQuestions";
            this.lvQuestions.Size = new System.Drawing.Size(386, 240);
            this.lvQuestions.TabIndex = 3;
            this.lvQuestions.UseCompatibleStateImageBehavior = false;
            this.lvQuestions.View = System.Windows.Forms.View.Details;
            this.lvQuestions.SelectedIndexChanged += new System.EventHandler(this.lvQuestions_SelectedIndexChanged_1);
            this.lvQuestions.DoubleClick += new System.EventHandler(this.lvQuestions_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 286;
            // 
            // pbQuestions
            // 
            this.pbQuestions.BackColor = System.Drawing.Color.Transparent;
            this.pbQuestions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbQuestions.Image = global::FamilyFeud.Properties.Resources.QUESTIONSTEXT;
            this.pbQuestions.Location = new System.Drawing.Point(432, 86);
            this.pbQuestions.Name = "pbQuestions";
            this.pbQuestions.Size = new System.Drawing.Size(193, 50);
            this.pbQuestions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbQuestions.TabIndex = 5;
            this.pbQuestions.TabStop = false;
            // 
            // pbListFrame
            // 
            this.pbListFrame.BackColor = System.Drawing.Color.Transparent;
            this.pbListFrame.Image = global::FamilyFeud.Properties.Resources.LISTFRAME;
            this.pbListFrame.Location = new System.Drawing.Point(152, 221);
            this.pbListFrame.Name = "pbListFrame";
            this.pbListFrame.Size = new System.Drawing.Size(172, 148);
            this.pbListFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbListFrame.TabIndex = 6;
            this.pbListFrame.TabStop = false;
            // 
            // AfterStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FamilyFeud.Properties.Resources.STARTBACKGROUND;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1030, 578);
            this.Controls.Add(this.pbListFrame);
            this.Controls.Add(this.pbQuestions);
            this.Controls.Add(this.lvQuestions);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnGoBackToStart);
            this.Name = "AfterStart";
            this.Text = "AfterStart";
            this.Load += new System.EventHandler(this.AfterStart_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbQuestions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbListFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGoBackToStart;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ListView lvQuestions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.PictureBox pbQuestions;
        private System.Windows.Forms.PictureBox pbListFrame;
    }
}