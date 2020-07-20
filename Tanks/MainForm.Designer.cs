namespace Tanks
{
    partial class MainForm
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
            this.MapPictureBox = new System.Windows.Forms.PictureBox();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.GameScoreLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MapPictureBox
            // 
            this.MapPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MapPictureBox.Location = new System.Drawing.Point(12, 41);
            this.MapPictureBox.Name = "MapPictureBox";
            this.MapPictureBox.Size = new System.Drawing.Size(735, 437);
            this.MapPictureBox.TabIndex = 0;
            this.MapPictureBox.TabStop = false;
            this.MapPictureBox.Visible = false;
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(510, 12);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(237, 23);
            this.StartGameButton.TabIndex = 1;
            this.StartGameButton.Text = "Start New Game";
            this.StartGameButton.UseVisualStyleBackColor = true;
            // 
            // GameScoreLabel
            // 
            this.GameScoreLabel.AutoSize = true;
            this.GameScoreLabel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.GameScoreLabel.Font = new System.Drawing.Font("Segoe Print", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameScoreLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.GameScoreLabel.Location = new System.Drawing.Point(12, 1);
            this.GameScoreLabel.Name = "GameScoreLabel";
            this.GameScoreLabel.Size = new System.Drawing.Size(172, 37);
            this.GameScoreLabel.TabIndex = 2;
            this.GameScoreLabel.Text = "Game Score: 0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 897);
            this.Controls.Add(this.GameScoreLabel);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.MapPictureBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tanks";
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox MapPictureBox;
        public System.Windows.Forms.Button StartGameButton;
        public System.Windows.Forms.Label GameScoreLabel;
    }
}

