using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.Auth.Microsoft.UI.WinForm;
using CmlLib.Core.VersionLoader;

namespace Elixa_Launcher_v0._2
{


    public partial class Form1 : Form
    {

        bool runOffline = false;
        bool runMicrosoft = false;

        bool loggedInMicrosoft = (bool)Properties.Settings.Default["MicrosoftLoggedIn"];

        string strOfflineUsername;

        string mcVer = "1.19.2-forge-43.2.0";


        private async Task signOut()
        {

            //creates log out form
            MicrosoftLoginForm loginForm = new MicrosoftLoginForm();
            loginForm.ShowLogoutDialog();


            //shitty way of fixing a problem, since I can't detect if they user closes the log out dialog box
            //Try logging in again, if it's unsuccessful, the user did log out, if it's successful then they didn't log out before.
            try
            {
                MSession microSesh = await loginForm.ShowLoginDialog();
            }
            catch
            {
                Properties.Settings.Default["MicrosoftLoggedIn"] = false;
                Properties.Settings.Default["MicroUsername"] = "AAAAAA";
                Properties.Settings.Default.Save();

                MicrosoftUsername.Text = Properties.Settings.Default["MicroUsername"].ToString();

                MicrosoftLogout.Visible = false;
                MicrosoftLogout.Enabled = false;

            }

        }


        //Task to run the game
        private async Task runMC()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 256;

            var elixaPath = new MinecraftPath(Environment.GetEnvironmentVariable("appdata") + "\\.Elixa");

            var launcher = new CMLauncher(elixaPath);

            //prints stuff to output
            launcher.FileChanged += (e) =>
            {
                Console.WriteLine("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount);
            };
            launcher.ProgressChanged += (s, e) =>
            {
                Console.WriteLine("{0}%", e.ProgressPercentage);
            };

            //get all MC versions
            var versions = await launcher.GetAllVersionsAsync();
            foreach (var v in versions)
            {
                Console.WriteLine(v.Name);
            }

            //check if running in offline mode
            if (runOffline)
            {
                var process = await launcher.CreateProcessAsync(mcVer, new MLaunchOption
                {
                    MaximumRamMb = 2048,
                    Session = MSession.GetOfflineSession(strOfflineUsername),
                });

                process.Start();
            }

            //check if running in microsoft mode
            if (runMicrosoft)
            {
                MicrosoftLoginForm loginForm = new MicrosoftLoginForm();
                MSession microSesh = await loginForm.ShowLoginDialog();

                if (microSesh.CheckIsValid())
                {
                    Properties.Settings.Default["MicrosoftLoggedIn"] = true;
                    Properties.Settings.Default["MicroUsername"] = microSesh.Username;
                    Properties.Settings.Default.Save();

                    MicrosoftUsername.Text = Properties.Settings.Default["MicroUsername"].ToString();

                    MicrosoftLogout.Visible = true;
                    MicrosoftLogout.Enabled = true;

                }

                var process = await launcher.CreateProcessAsync(mcVer, new MLaunchOption
                {
                    MaximumRamMb = 2048,
                    Session = microSesh,
                });

                process.Start();
            }


        }

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs a)
        {
            runMC();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if selection is in offline mode
            if (comboBox1.SelectedIndex == 0)
            {
                //save username tickbox
                SaveOfflineUsername.Enabled = true;
                SaveOfflineUsername.Visible = true;

                //username box
                OfflineUsername.Enabled = true;
                OfflineUsername.Visible = true;

                //if the username was saved
                if ((bool)Properties.Settings.Default["SaveUsername"] == true)
                {
                    strOfflineUsername = Properties.Settings.Default["OfflineUsername"].ToString();
                    SaveOfflineUsername.Checked = (bool)Properties.Settings.Default["SaveUsername"];
                    OfflineUsername.Text = strOfflineUsername;
                }

                runOffline = true;
            }
            else
            {
                SaveOfflineUsername.Enabled = false;
                SaveOfflineUsername.Visible = false;

                OfflineUsername.Enabled = false;
                OfflineUsername.Visible = false;
                runOffline = false;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                //text label with the username and with the log in text
                MicrosoftUsername.Visible = true;
                MicrosoftUsername.Enabled = true;

                //if its logged in, then show the log out 
                if (loggedInMicrosoft)
                {
                    MicrosoftLogout.Visible = true;
                    MicrosoftLogout.Enabled = true;

                    MicrosoftUsername.Text = Properties.Settings.Default["MicroUsername"].ToString();

                }

                runMicrosoft = true;

            }
            else
            {

                MicrosoftLogout.Visible = false;
                MicrosoftLogout.Enabled = false;


                MicrosoftUsername.Visible = false;
                MicrosoftUsername.Enabled = false;

                runMicrosoft = false;
            }

        }

        //add offline username to variables and settings
        private void OfflineUsername_TextChanged(object sender, EventArgs e)
        {
            strOfflineUsername = OfflineUsername.Text;

            if ((bool)Properties.Settings.Default["SaveUsername"] == true)
            {
                Properties.Settings.Default["OfflineUsername"] = strOfflineUsername;
                Properties.Settings.Default.Save();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //signMicrosoft();
        }

        private void MicrosoftLogout_Click(object sender, EventArgs e)
        {
            signOut();

        }

        private void SaveOfflineUsername_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveOfflineUsername.Checked)
            {
                if (strOfflineUsername != "")
                {
                    Properties.Settings.Default["OfflineUsername"] = strOfflineUsername;
                    Properties.Settings.Default["SaveUsername"] = true;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                Properties.Settings.Default["OfflineUsername"] = null;
                Properties.Settings.Default["SaveUsername"] = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
