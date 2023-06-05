namespace ElixaA2
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OfflinePlay = new System.Windows.Forms.Button();
            this.OfflineUsernameBox = new System.Windows.Forms.TextBox();
            this.settingsButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // OfflinePlay
            // 
            this.OfflinePlay.BackColor = System.Drawing.Color.Transparent;
            this.OfflinePlay.Enabled = false;
            this.OfflinePlay.FlatAppearance.BorderSize = 0;
            this.OfflinePlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.OfflinePlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.OfflinePlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OfflinePlay.Image = ((System.Drawing.Image)(resources.GetObject("OfflinePlay.Image")));
            this.OfflinePlay.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.OfflinePlay.Location = new System.Drawing.Point(322, 368);
            this.OfflinePlay.Name = "OfflinePlay";
            this.OfflinePlay.Size = new System.Drawing.Size(176, 75);
            this.OfflinePlay.TabIndex = 2;
            this.OfflinePlay.UseVisualStyleBackColor = false;
            this.OfflinePlay.Click += new System.EventHandler(this.OfflinePlay_Click);
            this.OfflinePlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OfflinePlay_MouseDown);
            this.OfflinePlay.MouseEnter += new System.EventHandler(this.OfflinePlay_MouseEnter);
            this.OfflinePlay.MouseLeave += new System.EventHandler(this.OfflinePlay_MouseLeave);
            this.OfflinePlay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OfflinePlay_MouseUp);
            // 
            // OfflineUsernameBox
            // 
            this.OfflineUsernameBox.Location = new System.Drawing.Point(339, 293);
            this.OfflineUsernameBox.Name = "OfflineUsernameBox";
            this.OfflineUsernameBox.Size = new System.Drawing.Size(159, 20);
            this.OfflineUsernameBox.TabIndex = 3;
            this.OfflineUsernameBox.TextChanged += new System.EventHandler(this.OfflineUsernameBox_TextChanged);
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.Transparent;
            this.settingsButton.FlatAppearance.BorderSize = 0;
            this.settingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.settingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.settingsButton.Location = new System.Drawing.Point(640, 359);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(74, 75);
            this.settingsButton.TabIndex = 12;
            this.settingsButton.UseVisualStyleBackColor = false;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            this.settingsButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.settingsButton_MouseDown);
            this.settingsButton.MouseEnter += new System.EventHandler(this.settingsButton_MouseEnter);
            this.settingsButton.MouseLeave += new System.EventHandler(this.settingsButton_MouseLeave);
            this.settingsButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.settingsButton_MouseUp);
            // 
            // closeButton
            // 
            this.closeButton.AutoSize = true;
            this.closeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Image = global::ElixaA2.Properties.Resources.cerrar;
            this.closeButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.closeButton.Location = new System.Drawing.Point(753, 12);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(51, 51);
            this.closeButton.TabIndex = 13;
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.closeButton_MouseDown);
            this.closeButton.MouseEnter += new System.EventHandler(this.closeButton_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            this.closeButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.closeButton_MouseUp);
            // 
            // minimizeButton
            // 
            this.minimizeButton.AutoSize = true;
            this.minimizeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.minimizeButton.BackColor = System.Drawing.Color.Transparent;
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Image = global::ElixaA2.Properties.Resources.minimizar;
            this.minimizeButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.minimizeButton.Location = new System.Drawing.Point(684, 12);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(51, 51);
            this.minimizeButton.TabIndex = 14;
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            this.minimizeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.minimizeButton_MouseDown);
            this.minimizeButton.MouseEnter += new System.EventHandler(this.minimizeButton_MouseEnter);
            this.minimizeButton.MouseLeave += new System.EventHandler(this.minimizeButton_MouseLeave);
            this.minimizeButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.minimizeButton_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::ElixaA2.Properties.Resources.logo_como_tiene_que_ser;
            this.pictureBox1.Location = new System.Drawing.Point(271, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(277, 171);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(816, 490);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.OfflinePlay);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.OfflineUsernameBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elixa Launcher";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OfflinePlay;
        private System.Windows.Forms.TextBox OfflineUsernameBox;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

