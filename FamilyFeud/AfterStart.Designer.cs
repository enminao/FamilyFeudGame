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
            this.QuestionsInListView = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnGoBackToStart
            // 
            this.btnGoBackToStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoBackToStart.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBackToStart.Location = new System.Drawing.Point(152, 334);
            this.btnGoBackToStart.Name = "btnGoBackToStart";
            this.btnGoBackToStart.Size = new System.Drawing.Size(181, 62);
            this.btnGoBackToStart.TabIndex = 1;
            this.btnGoBackToStart.Text = "Go Back";
            this.btnGoBackToStart.UseVisualStyleBackColor = true;
            this.btnGoBackToStart.Click += new System.EventHandler(this.btnGoBackToStart_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(552, 309);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(189, 62);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lvQuestions
            // 
            this.lvQuestions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.QuestionsInListView});
            this.lvQuestions.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvQuestions.GridLines = true;
            this.lvQuestions.HideSelection = false;
            this.lvQuestions.Location = new System.Drawing.Point(271, 12);
            this.lvQuestions.Name = "lvQuestions";
            this.lvQuestions.Size = new System.Drawing.Size(386, 240);
            this.lvQuestions.TabIndex = 3;
            this.lvQuestions.UseCompatibleStateImageBehavior = false;
            this.lvQuestions.View = System.Windows.Forms.View.Details;
            this.lvQuestions.SelectedIndexChanged += new System.EventHandler(this.lvQuestions_SelectedIndexChanged_1);
            this.lvQuestions.DoubleClick += new System.EventHandler(this.lvQuestions_DoubleClick);
            // 
            // QuestionsInListView
            // 
            this.QuestionsInListView.Text = "----------Questions----------";
            this.QuestionsInListView.Width = 450;
            // 
            // AfterStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 578);
            this.Controls.Add(this.lvQuestions);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnGoBackToStart);
            this.Name = "AfterStart";
            this.Text = "AfterStart";
            this.Load += new System.EventHandler(this.AfterStart_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGoBackToStart;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ListView lvQuestions;
        private System.Windows.Forms.ColumnHeader QuestionsInListView;
    }
}