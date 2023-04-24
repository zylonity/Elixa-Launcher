namespace ElixaA2
{
    partial class Settings
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
            this.ramLabel = new System.Windows.Forms.Label();
            this.RamBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ramLabel
            // 
            this.ramLabel.AutoSize = true;
            this.ramLabel.Location = new System.Drawing.Point(209, 131);
            this.ramLabel.Name = "ramLabel";
            this.ramLabel.Size = new System.Drawing.Size(34, 13);
            this.ramLabel.TabIndex = 1;
            this.ramLabel.Text = "RAM:";
            // 
            // RamBox
            // 
            this.RamBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.RamBox.DisplayMember = "1";
            this.RamBox.FormattingEnabled = true;
            this.RamBox.Items.AddRange(new object[] {
            "2048 Mb",
            "3072 Mb",
            "4096 Mb",
            "5120 Mb",
            "6144 Mb",
            "7168 Mb",
            "8192 Mb",
            "9216 Mb",
            "10240 Mb",
            "11264 Mb",
            "12288 Mb",
            "13312 Mb",
            "14336 Mb",
            "15360 Mb",
            "16384 Mb",
            "17408 Mb",
            "18432 Mb",
            "19456 Mb",
            "20480 Mb"});
            this.RamBox.Location = new System.Drawing.Point(249, 128);
            this.RamBox.Name = "RamBox";
            this.RamBox.Size = new System.Drawing.Size(85, 21);
            this.RamBox.TabIndex = 2;
            this.RamBox.Text = "2048 Mb";
            this.RamBox.SelectedIndexChanged += new System.EventHandler(this.RamBox_SelectedIndexChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 306);
            this.Controls.Add(this.RamBox);
            this.Controls.Add(this.ramLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ramLabel;
        private System.Windows.Forms.ComboBox RamBox;
    }
}