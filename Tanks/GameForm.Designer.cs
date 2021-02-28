namespace Tanks
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.MenuImage = new System.Windows.Forms.PictureBox();
            this.GameMapPictureBox = new System.Windows.Forms.PictureBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.ScoresLabel = new System.Windows.Forms.Label();
            this.OpenInfoBoxLabel = new System.Windows.Forms.Label();
            this.mock = new System.Windows.Forms.Button();
            this.GameResultLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MenuImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GameMapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuImage
            // 
            this.MenuImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MenuImage.BackColor = System.Drawing.Color.Black;
            this.MenuImage.Image = ((System.Drawing.Image)(resources.GetObject("MenuImage.Image")));
            this.MenuImage.Location = new System.Drawing.Point(0, 65);
            this.MenuImage.Name = "MenuImage";
            this.MenuImage.Size = new System.Drawing.Size(212, 276);
            this.MenuImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MenuImage.TabIndex = 3;
            this.MenuImage.TabStop = false;
            // 
            // GameMapPictureBox
            // 
            this.GameMapPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GameMapPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GameMapPictureBox.Location = new System.Drawing.Point(0, 30);
            this.GameMapPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.GameMapPictureBox.Name = "GameMapPictureBox";
            this.GameMapPictureBox.Size = new System.Drawing.Size(622, 373);
            this.GameMapPictureBox.TabIndex = 2;
            this.GameMapPictureBox.TabStop = false;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartButton.Location = new System.Drawing.Point(268, 255);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(100, 25);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Играть";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // ScoresLabel
            // 
            this.ScoresLabel.AutoSize = true;
            this.ScoresLabel.Font = new System.Drawing.Font("Showcard Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ScoresLabel.Location = new System.Drawing.Point(13, 3);
            this.ScoresLabel.Name = "ScoresLabel";
            this.ScoresLabel.Size = new System.Drawing.Size(198, 23);
            this.ScoresLabel.TabIndex = 5;
            this.ScoresLabel.Text = "tanks: 0    apples: 0";
            // 
            // OpenInfoBoxLabel
            // 
            this.OpenInfoBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenInfoBoxLabel.AutoSize = true;
            this.OpenInfoBoxLabel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.OpenInfoBoxLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OpenInfoBoxLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenInfoBoxLabel.Font = new System.Drawing.Font("Showcard Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenInfoBoxLabel.Location = new System.Drawing.Point(511, 7);
            this.OpenInfoBoxLabel.Name = "OpenInfoBoxLabel";
            this.OpenInfoBoxLabel.Size = new System.Drawing.Size(100, 17);
            this.OpenInfoBoxLabel.TabIndex = 6;
            this.OpenInfoBoxLabel.Text = "open infobox";
            this.OpenInfoBoxLabel.Click += new System.EventHandler(this.OpenInfoPanel_Click);
            // 
            // mock
            // 
            this.mock.Location = new System.Drawing.Point(283, 3);
            this.mock.Name = "mock";
            this.mock.Size = new System.Drawing.Size(75, 23);
            this.mock.TabIndex = 7;
            this.mock.Text = "this is mock";
            this.mock.UseVisualStyleBackColor = true;
            this.mock.Visible = false;
            // 
            // GameResultLabel
            // 
            this.GameResultLabel.AutoSize = true;
            this.GameResultLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GameResultLabel.Font = new System.Drawing.Font("Arial Black", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GameResultLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.GameResultLabel.Location = new System.Drawing.Point(268, 124);
            this.GameResultLabel.Name = "GameResultLabel";
            this.GameResultLabel.Size = new System.Drawing.Size(272, 52);
            this.GameResultLabel.TabIndex = 8;
            this.GameResultLabel.Text = "Ты победил";
            this.GameResultLabel.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(621, 399);
            this.Controls.Add(this.GameResultLabel);
            this.Controls.Add(this.mock);
            this.Controls.Add(this.OpenInfoBoxLabel);
            this.Controls.Add(this.ScoresLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.MenuImage);
            this.Controls.Add(this.GameMapPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.Text = "Tanks LOL";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MenuImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GameMapPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox MenuImage;
        private System.Windows.Forms.PictureBox GameMapPictureBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label ScoresLabel;
        private System.Windows.Forms.Label OpenInfoBoxLabel;
        private System.Windows.Forms.Button mock;
        private System.Windows.Forms.Label GameResultLabel;
    }
}

