﻿namespace ElixaA2
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
            this.SelectMC = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OfflinePlay = new System.Windows.Forms.Button();
            this.OfflineUsernameBox = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.OfflinePanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.verNum = new System.Windows.Forms.Label();
            this.settingsButton = new System.Windows.Forms.Button();
            this.OfflinePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectMC
            // 
            this.SelectMC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectMC.FormattingEnabled = true;
            this.SelectMC.Items.AddRange(new object[] {
            "Offline",
            "Microsoft"});
            this.SelectMC.Location = new System.Drawing.Point(74, 390);
            this.SelectMC.Name = "SelectMC";
            this.SelectMC.Size = new System.Drawing.Size(121, 21);
            this.SelectMC.TabIndex = 0;
            this.SelectMC.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 369);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select offline or online";
            // 
            // OfflinePlay
            // 
            this.OfflinePlay.Enabled = false;
            this.OfflinePlay.Location = new System.Drawing.Point(197, 33);
            this.OfflinePlay.Name = "OfflinePlay";
            this.OfflinePlay.Size = new System.Drawing.Size(146, 27);
            this.OfflinePlay.TabIndex = 2;
            this.OfflinePlay.Text = "PLAY";
            this.OfflinePlay.UseVisualStyleBackColor = true;
            this.OfflinePlay.Click += new System.EventHandler(this.OfflinePlay_Click);
            // 
            // OfflineUsernameBox
            // 
            this.OfflineUsernameBox.Location = new System.Drawing.Point(11, 37);
            this.OfflineUsernameBox.Name = "OfflineUsernameBox";
            this.OfflineUsernameBox.Size = new System.Drawing.Size(159, 20);
            this.OfflineUsernameBox.TabIndex = 3;
            this.OfflineUsernameBox.TextChanged += new System.EventHandler(this.OfflineUsernameBox_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(218, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Save username";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // OfflinePanel
            // 
            this.OfflinePanel.Controls.Add(this.checkBox1);
            this.OfflinePanel.Controls.Add(this.OfflineUsernameBox);
            this.OfflinePanel.Controls.Add(this.OfflinePlay);
            this.OfflinePanel.Location = new System.Drawing.Point(396, 353);
            this.OfflinePanel.Name = "OfflinePanel";
            this.OfflinePanel.Size = new System.Drawing.Size(354, 71);
            this.OfflinePanel.TabIndex = 5;
            this.OfflinePanel.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 430);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ver: ";
            // 
            // verNum
            // 
            this.verNum.AutoSize = true;
            this.verNum.Location = new System.Drawing.Point(35, 430);
            this.verNum.Name = "verNum";
            this.verNum.Size = new System.Drawing.Size(10, 13);
            this.verNum.TabIndex = 11;
            this.verNum.Text = "-";
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(8, 8);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(30, 34);
            this.settingsButton.TabIndex = 12;
            this.settingsButton.Text = "S";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.verNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OfflinePanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectMC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elixa Launcher";
            this.OfflinePanel.ResumeLayout(false);
            this.OfflinePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SelectMC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OfflinePlay;
        private System.Windows.Forms.TextBox OfflineUsernameBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel OfflinePanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label verNum;
        private System.Windows.Forms.Button settingsButton;
    }
}

