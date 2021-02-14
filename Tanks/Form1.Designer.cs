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
            ((System.ComponentModel.ISupportInitialize)(this.MenuImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GameMapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuImage
            // 
            this.MenuImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MenuImage.BackColor = System.Drawing.Color.Black;
            this.MenuImage.Image = ((System.Drawing.Image)(resources.GetObject("MenuImage.Image")));
            this.MenuImage.Location = new System.Drawing.Point(0, 21);
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
            this.GameMapPictureBox.Location = new System.Drawing.Point(0, 0);
            this.GameMapPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.GameMapPictureBox.Name = "GameMapPictureBox";
            this.GameMapPictureBox.Size = new System.Drawing.Size(626, 373);
            this.GameMapPictureBox.TabIndex = 2;
            this.GameMapPictureBox.TabStop = false;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartButton.Location = new System.Drawing.Point(268, 213);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(100, 25);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Играть";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(621, 369);
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

        }

        #endregion
        private System.Windows.Forms.PictureBox MenuImage;
        private System.Windows.Forms.PictureBox GameMapPictureBox;
        private System.Windows.Forms.Button StartButton;
    }
}

