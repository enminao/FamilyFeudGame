namespace FamilyFeud
{
    partial class frmStart
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.picClubLogo = new System.Windows.Forms.PictureBox();
            this.picMainLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClubLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMainLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImage = global::FamilyFeud.Properties.Resources.STARTGAME;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStart.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(573, 173);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(206, 72);
            this.btnStart.TabIndex = 0;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackgroundImage = global::FamilyFeud.Properties.Resources.QUITGAME;
            this.btnQuit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuit.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.Location = new System.Drawing.Point(475, 285);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(206, 72);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // picClubLogo
            // 
            this.picClubLogo.BackColor = System.Drawing.Color.Transparent;
            this.picClubLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picClubLogo.Image = global::FamilyFeud.Properties.Resources.ExplicitLogo;
            this.picClubLogo.Location = new System.Drawing.Point(58, 119);
            this.picClubLogo.Name = "picClubLogo";
            this.picClubLogo.Size = new System.Drawing.Size(338, 361);
            this.picClubLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClubLogo.TabIndex = 4;
            this.picClubLogo.TabStop = false;
            // 
            // picMainLogo
            // 
            this.picMainLogo.BackColor = System.Drawing.Color.Transparent;
            this.picMainLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMainLogo.Image = global::FamilyFeud.Properties.Resources.FAMILYFEUDLOGO;
            this.picMainLogo.Location = new System.Drawing.Point(-402, 519);
            this.picMainLogo.Name = "picMainLogo";
            this.picMainLogo.Size = new System.Drawing.Size(1407, 704);
            this.picMainLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMainLogo.TabIndex = 3;
            this.picMainLogo.TabStop = false;
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(896, 675);
            this.Controls.Add(this.picClubLogo);
            this.Controls.Add(this.picMainLogo);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnStart);
            this.KeyPreview = true;
            this.Name = "frmStart";
            this.Text = "frmStart";
            this.Load += new System.EventHandler(this.frmStart_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStart_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picClubLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMainLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.PictureBox picMainLogo;
        private System.Windows.Forms.PictureBox picClubLogo;
    }
}

