using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElixaA2
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            RamBox.SelectedIndex = (int)Properties.Settings.Default["RamIndex"];
        }

        private void RamBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["RamIndex"] = RamBox.SelectedIndex;
            Properties.Settings.Default["Ram"] = (RamBox.SelectedIndex + 2) * 1024;
            Properties.Settings.Default.Save();

        }
    }
}
