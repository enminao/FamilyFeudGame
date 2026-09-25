namespace FamilyFeud
{
    partial class Game
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbQuestionare = new System.Windows.Forms.Label();
            this.lblQuestion1 = new System.Windows.Forms.Label();
            this.lblQuestion2 = new System.Windows.Forms.Label();
            this.lblQuestion3 = new System.Windows.Forms.Label();
            this.lblQuestion4 = new System.Windows.Forms.Label();
            this.lblQuestion5 = new System.Windows.Forms.Label();
            this.lblQuestion6 = new System.Windows.Forms.Label();
            this.lblQuestion7 = new System.Windows.Forms.Label();
            this.lblQuestion8 = new System.Windows.Forms.Label();
            this.lblQuestionPoints4 = new System.Windows.Forms.Label();
            this.lblQuestionPoints1 = new System.Windows.Forms.Label();
            this.lblQuestionPoints2 = new System.Windows.Forms.Label();
            this.lblQuestionPoints3 = new System.Windows.Forms.Label();
            this.lblQuestionPoints8 = new System.Windows.Forms.Label();
            this.lblQuestionPoints5 = new System.Windows.Forms.Label();
            this.lblQuestionPoints6 = new System.Windows.Forms.Label();
            this.lblQuestionPoints7 = new System.Windows.Forms.Label();
            this.btnGoBackToStart = new System.Windows.Forms.Button();
            this.lblTeamOnePoints = new System.Windows.Forms.Label();
            this.lblTeamTwoPoints = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lvTeamOneWrong = new System.Windows.Forms.ListView();
            this.lvTeamTwoWrong = new System.Windows.Forms.ListView();
            this.btnFinish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 657);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Team 1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1580, 668);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 45);
            this.label2.TabIndex = 1;
            this.label2.Text = "Team 2";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(316, 84);
            this.label3.TabIndex = 2;
            this.label3.Text = "Question:";
            // 
            // lbQuestionare
            // 
            this.lbQuestionare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbQuestionare.AutoSize = true;
            this.lbQuestionare.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuestionare.Location = new System.Drawing.Point(345, 38);
            this.lbQuestionare.Name = "lbQuestionare";
            this.lbQuestionare.Size = new System.Drawing.Size(839, 84);
            this.lbQuestionare.TabIndex = 3;
            this.lbQuestionare.Text = "Where the Question will go";
            // 
            // lblQuestion1
            // 
            this.lblQuestion1.AutoSize = true;
            this.lblQuestion1.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion1.Location = new System.Drawing.Point(129, 173);
            this.lblQuestion1.Name = "lblQuestion1";
            this.lblQuestion1.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion1.TabIndex = 4;
            this.lblQuestion1.Text = "Question 1";
            this.lblQuestion1.Click += new System.EventHandler(this.lblQuestion1_Click);
            // 
            // lblQuestion2
            // 
            this.lblQuestion2.AutoSize = true;
            this.lblQuestion2.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion2.Location = new System.Drawing.Point(129, 281);
            this.lblQuestion2.Name = "lblQuestion2";
            this.lblQuestion2.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion2.TabIndex = 5;
            this.lblQuestion2.Text = "Question 2";
            this.lblQuestion2.Click += new System.EventHandler(this.lblQuestion2_Click);
            // 
            // lblQuestion3
            // 
            this.lblQuestion3.AutoSize = true;
            this.lblQuestion3.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion3.Location = new System.Drawing.Point(129, 402);
            this.lblQuestion3.Name = "lblQuestion3";
            this.lblQuestion3.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion3.TabIndex = 6;
            this.lblQuestion3.Text = "Question 3";
            this.lblQuestion3.Click += new System.EventHandler(this.lblQuestion3_Click);
            // 
            // lblQuestion4
            // 
            this.lblQuestion4.AutoSize = true;
            this.lblQuestion4.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion4.Location = new System.Drawing.Point(129, 518);
            this.lblQuestion4.Name = "lblQuestion4";
            this.lblQuestion4.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion4.TabIndex = 7;
            this.lblQuestion4.Text = "Question 4";
            this.lblQuestion4.Click += new System.EventHandler(this.lblQuestion4_Click);
            // 
            // lblQuestion5
            // 
            this.lblQuestion5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestion5.AutoSize = true;
            this.lblQuestion5.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion5.Location = new System.Drawing.Point(762, 159);
            this.lblQuestion5.Name = "lblQuestion5";
            this.lblQuestion5.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion5.TabIndex = 8;
            this.lblQuestion5.Text = "Question 5";
            this.lblQuestion5.Click += new System.EventHandler(this.lblQuestion5_Click);
            // 
            // lblQuestion6
            // 
            this.lblQuestion6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestion6.AutoSize = true;
            this.lblQuestion6.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion6.Location = new System.Drawing.Point(762, 269);
            this.lblQuestion6.Name = "lblQuestion6";
            this.lblQuestion6.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion6.TabIndex = 9;
            this.lblQuestion6.Text = "Question 6";
            this.lblQuestion6.Click += new System.EventHandler(this.lblQuestion6_Click);
            // 
            // lblQuestion7
            // 
            this.lblQuestion7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestion7.AutoSize = true;
            this.lblQuestion7.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion7.Location = new System.Drawing.Point(777, 380);
            this.lblQuestion7.Name = "lblQuestion7";
            this.lblQuestion7.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion7.TabIndex = 10;
            this.lblQuestion7.Text = "Question 7";
            this.lblQuestion7.Click += new System.EventHandler(this.lblQuestion7_Click);
            // 
            // lblQuestion8
            // 
            this.lblQuestion8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestion8.AutoSize = true;
            this.lblQuestion8.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion8.Location = new System.Drawing.Point(777, 500);
            this.lblQuestion8.Name = "lblQuestion8";
            this.lblQuestion8.Size = new System.Drawing.Size(276, 66);
            this.lblQuestion8.TabIndex = 11;
            this.lblQuestion8.Text = "Question 8";
            this.lblQuestion8.Click += new System.EventHandler(this.lblQuestion8_Click);
            // 
            // lblQuestionPoints4
            // 
            this.lblQuestionPoints4.AutoSize = true;
            this.lblQuestionPoints4.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints4.Location = new System.Drawing.Point(488, 518);
            this.lblQuestionPoints4.Name = "lblQuestionPoints4";
            this.lblQuestionPoints4.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints4.TabIndex = 12;
            this.lblQuestionPoints4.Text = "Points 4";
            this.lblQuestionPoints4.Click += new System.EventHandler(this.lblQuestionPoints4_Click);
            // 
            // lblQuestionPoints1
            // 
            this.lblQuestionPoints1.AutoSize = true;
            this.lblQuestionPoints1.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints1.Location = new System.Drawing.Point(488, 173);
            this.lblQuestionPoints1.Name = "lblQuestionPoints1";
            this.lblQuestionPoints1.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints1.TabIndex = 12;
            this.lblQuestionPoints1.Text = "Points 1";
            this.lblQuestionPoints1.Click += new System.EventHandler(this.lblQuestionPoints1_Click);
            // 
            // lblQuestionPoints2
            // 
            this.lblQuestionPoints2.AutoSize = true;
            this.lblQuestionPoints2.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints2.Location = new System.Drawing.Point(488, 281);
            this.lblQuestionPoints2.Name = "lblQuestionPoints2";
            this.lblQuestionPoints2.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints2.TabIndex = 13;
            this.lblQuestionPoints2.Text = "Points 2";
            this.lblQuestionPoints2.Click += new System.EventHandler(this.lblQuestionPoints2_Click);
            // 
            // lblQuestionPoints3
            // 
            this.lblQuestionPoints3.AutoSize = true;
            this.lblQuestionPoints3.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints3.Location = new System.Drawing.Point(488, 402);
            this.lblQuestionPoints3.Name = "lblQuestionPoints3";
            this.lblQuestionPoints3.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints3.TabIndex = 14;
            this.lblQuestionPoints3.Text = "Points 3";
            this.lblQuestionPoints3.Click += new System.EventHandler(this.lblQuestionPoints3_Click);
            // 
            // lblQuestionPoints8
            // 
            this.lblQuestionPoints8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestionPoints8.AutoSize = true;
            this.lblQuestionPoints8.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints8.Location = new System.Drawing.Point(1212, 500);
            this.lblQuestionPoints8.Name = "lblQuestionPoints8";
            this.lblQuestionPoints8.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints8.TabIndex = 15;
            this.lblQuestionPoints8.Text = "Points 8";
            this.lblQuestionPoints8.Click += new System.EventHandler(this.lblQuestionPoints8_Click);
            // 
            // lblQuestionPoints5
            // 
            this.lblQuestionPoints5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestionPoints5.AutoSize = true;
            this.lblQuestionPoints5.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints5.Location = new System.Drawing.Point(1177, 160);
            this.lblQuestionPoints5.Name = "lblQuestionPoints5";
            this.lblQuestionPoints5.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints5.TabIndex = 15;
            this.lblQuestionPoints5.Text = "Points 5";
            this.lblQuestionPoints5.Click += new System.EventHandler(this.lblQuestionPoints5_Click);
            // 
            // lblQuestionPoints6
            // 
            this.lblQuestionPoints6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestionPoints6.AutoSize = true;
            this.lblQuestionPoints6.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints6.Location = new System.Drawing.Point(1212, 269);
            this.lblQuestionPoints6.Name = "lblQuestionPoints6";
            this.lblQuestionPoints6.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints6.TabIndex = 16;
            this.lblQuestionPoints6.Text = "Points 6";
            this.lblQuestionPoints6.Click += new System.EventHandler(this.lblQuestionPoints6_Click);
            // 
            // lblQuestionPoints7
            // 
            this.lblQuestionPoints7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestionPoints7.AutoSize = true;
            this.lblQuestionPoints7.Font = new System.Drawing.Font("Comic Sans MS", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionPoints7.Location = new System.Drawing.Point(1212, 380);
            this.lblQuestionPoints7.Name = "lblQuestionPoints7";
            this.lblQuestionPoints7.Size = new System.Drawing.Size(210, 66);
            this.lblQuestionPoints7.TabIndex = 17;
            this.lblQuestionPoints7.Text = "Points 7";
            this.lblQuestionPoints7.Click += new System.EventHandler(this.lblQuestionPoints7_Click);
            // 
            // btnGoBackToStart
            // 
            this.btnGoBackToStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoBackToStart.Font = new System.Drawing.Font("Comic Sans MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBackToStart.Location = new System.Drawing.Point(920, 890);
            this.btnGoBackToStart.Name = "btnGoBackToStart";
            this.btnGoBackToStart.Size = new System.Drawing.Size(174, 60);
            this.btnGoBackToStart.TabIndex = 19;
            this.btnGoBackToStart.Text = "Go Back";
            this.btnGoBackToStart.UseVisualStyleBackColor = true;
            this.btnGoBackToStart.Click += new System.EventHandler(this.btnGoBackToStart_Click);
            // 
            // lblTeamOnePoints
            // 
            this.lblTeamOnePoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTeamOnePoints.AutoSize = true;
            this.lblTeamOnePoints.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeamOnePoints.Location = new System.Drawing.Point(12, 687);
            this.lblTeamOnePoints.Name = "lblTeamOnePoints";
            this.lblTeamOnePoints.Size = new System.Drawing.Size(246, 45);
            this.lblTeamOnePoints.TabIndex = 20;
            this.lblTeamOnePoints.Text = "Team 1 Points";
            // 
            // lblTeamTwoPoints
            // 
            this.lblTeamTwoPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTeamTwoPoints.AutoSize = true;
            this.lblTeamTwoPoints.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeamTwoPoints.Location = new System.Drawing.Point(1528, 739);
            this.lblTeamTwoPoints.Name = "lblTeamTwoPoints";
            this.lblTeamTwoPoints.Size = new System.Drawing.Size(246, 45);
            this.lblTeamTwoPoints.TabIndex = 21;
            this.lblTeamTwoPoints.Text = "Team 2 Points";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lvTeamOneWrong
            // 
            this.lvTeamOneWrong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lvTeamOneWrong.HideSelection = false;
            this.lvTeamOneWrong.Location = new System.Drawing.Point(12, 829);
            this.lvTeamOneWrong.Name = "lvTeamOneWrong";
            this.lvTeamOneWrong.Size = new System.Drawing.Size(166, 48);
            this.lvTeamOneWrong.TabIndex = 22;
            this.lvTeamOneWrong.UseCompatibleStateImageBehavior = false;
            // 
            // lvTeamTwoWrong
            // 
            this.lvTeamTwoWrong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTeamTwoWrong.HideSelection = false;
            this.lvTeamTwoWrong.Location = new System.Drawing.Point(1558, 829);
            this.lvTeamTwoWrong.Name = "lvTeamTwoWrong";
            this.lvTeamTwoWrong.Size = new System.Drawing.Size(166, 48);
            this.lvTeamTwoWrong.TabIndex = 23;
            this.lvTeamTwoWrong.UseCompatibleStateImageBehavior = false;
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFinish.Font = new System.Drawing.Font("Comic Sans MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinish.Location = new System.Drawing.Point(71, 922);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(174, 60);
            this.btnFinish.TabIndex = 18;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1757, 1017);
            this.Controls.Add(this.lvTeamTwoWrong);
            this.Controls.Add(this.lvTeamOneWrong);
            this.Controls.Add(this.lblTeamTwoPoints);
            this.Controls.Add(this.lblTeamOnePoints);
            this.Controls.Add(this.btnGoBackToStart);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.lblQuestionPoints7);
            this.Controls.Add(this.lblQuestionPoints6);
            this.Controls.Add(this.lblQuestionPoints5);
            this.Controls.Add(this.lblQuestionPoints8);
            this.Controls.Add(this.lblQuestionPoints3);
            this.Controls.Add(this.lblQuestionPoints2);
            this.Controls.Add(this.lblQuestionPoints1);
            this.Controls.Add(this.lblQuestionPoints4);
            this.Controls.Add(this.lblQuestion8);
            this.Controls.Add(this.lblQuestion7);
            this.Controls.Add(this.lblQuestion6);
            this.Controls.Add(this.lblQuestion5);
            this.Controls.Add(this.lblQuestion4);
            this.Controls.Add(this.lblQuestion3);
            this.Controls.Add(this.lblQuestion2);
            this.Controls.Add(this.lblQuestion1);
            this.Controls.Add(this.lbQuestionare);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "Game";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbQuestionare;
        private System.Windows.Forms.Label lblQuestion1;
        private System.Windows.Forms.Label lblQuestion2;
        private System.Windows.Forms.Label lblQuestion3;
        private System.Windows.Forms.Label lblQuestion4;
        private System.Windows.Forms.Label lblQuestion5;
        private System.Windows.Forms.Label lblQuestion6;
        private System.Windows.Forms.Label lblQuestion7;
        private System.Windows.Forms.Label lblQuestion8;
        private System.Windows.Forms.Label lblQuestionPoints4;
        private System.Windows.Forms.Label lblQuestionPoints1;
        private System.Windows.Forms.Label lblQuestionPoints2;
        private System.Windows.Forms.Label lblQuestionPoints3;
        private System.Windows.Forms.Label lblQuestionPoints8;
        private System.Windows.Forms.Label lblQuestionPoints5;
        private System.Windows.Forms.Label lblQuestionPoints6;
        private System.Windows.Forms.Label lblQuestionPoints7;
        private System.Windows.Forms.Button btnGoBackToStart;
        private System.Windows.Forms.Label lblTeamOnePoints;
        private System.Windows.Forms.Label lblTeamTwoPoints;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvTeamOneWrong;
        private System.Windows.Forms.ListView lvTeamTwoWrong;
        private System.Windows.Forms.Button btnFinish;
    }
}