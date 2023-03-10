namespace Elixa_Launcher_v0._2
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.OfflineUsername = new System.Windows.Forms.TextBox();
            this.MicrosoftLogout = new System.Windows.Forms.Button();
            this.SaveOfflineUsername = new System.Windows.Forms.CheckBox();
            this.MicrosoftUsername = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(647, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Offline",
            "Microsoft Login"});
            this.comboBox1.Location = new System.Drawing.Point(48, 395);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // OfflineUsername
            // 
            this.OfflineUsername.AccessibleName = "";
            this.OfflineUsername.Enabled = false;
            this.OfflineUsername.Location = new System.Drawing.Point(508, 397);
            this.OfflineUsername.Name = "OfflineUsername";
            this.OfflineUsername.Size = new System.Drawing.Size(133, 20);
            this.OfflineUsername.TabIndex = 2;
            this.OfflineUsername.Visible = false;
            this.OfflineUsername.TextChanged += new System.EventHandler(this.OfflineUsername_TextChanged);
            // 
            // MicrosoftLogout
            // 
            this.MicrosoftLogout.Enabled = false;
            this.MicrosoftLogout.Location = new System.Drawing.Point(329, 395);
            this.MicrosoftLogout.Name = "MicrosoftLogout";
            this.MicrosoftLogout.Size = new System.Drawing.Size(173, 23);
            this.MicrosoftLogout.TabIndex = 7;
            this.MicrosoftLogout.Text = "Microsoft Logout";
            this.MicrosoftLogout.UseVisualStyleBackColor = true;
            this.MicrosoftLogout.Visible = false;
            this.MicrosoftLogout.Click += new System.EventHandler(this.MicrosoftLogout_Click);
            // 
            // SaveOfflineUsername
            // 
            this.SaveOfflineUsername.AutoSize = true;
            this.SaveOfflineUsername.Enabled = false;
            this.SaveOfflineUsername.Location = new System.Drawing.Point(351, 399);
            this.SaveOfflineUsername.Name = "SaveOfflineUsername";
            this.SaveOfflineUsername.Size = new System.Drawing.Size(135, 17);
            this.SaveOfflineUsername.TabIndex = 9;
            this.SaveOfflineUsername.Text = "Save Offline Username";
            this.SaveOfflineUsername.UseVisualStyleBackColor = true;
            this.SaveOfflineUsername.Visible = false;
            this.SaveOfflineUsername.CheckedChanged += new System.EventHandler(this.SaveOfflineUsername_CheckedChanged);
            // 
            // MicrosoftUsername
            // 
            this.MicrosoftUsername.AutoSize = true;
            this.MicrosoftUsername.Enabled = false;
            this.MicrosoftUsername.Location = new System.Drawing.Point(505, 402);
            this.MicrosoftUsername.Name = "MicrosoftUsername";
            this.MicrosoftUsername.Size = new System.Drawing.Size(133, 13);
            this.MicrosoftUsername.TabIndex = 10;
            this.MicrosoftUsername.Text = "Please press Play to log in!";
            this.MicrosoftUsername.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MicrosoftUsername);
            this.Controls.Add(this.SaveOfflineUsername);
            this.Controls.Add(this.MicrosoftLogout);
            this.Controls.Add(this.OfflineUsername);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox OfflineUsername;
        private System.Windows.Forms.Button MicrosoftLogout;
        private System.Windows.Forms.CheckBox SaveOfflineUsername;
        private System.Windows.Forms.Label MicrosoftUsername;
    }
}

