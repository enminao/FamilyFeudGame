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
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(325, 152);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(206, 72);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.Location = new System.Drawing.Point(325, 252);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(206, 72);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 675);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnStart);
            this.KeyPreview = true;
            this.Name = "frmStart";
            this.Text = "frmStart";
            this.Load += new System.EventHandler(this.frmStart_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStart_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnQuit;
    }
}

