using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Net;
using System.IO;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.VersionLoader;
using LibGit2Sharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.WebRequestMethods;
using System.Diagnostics;

namespace ElixaA2
{
    public partial class Form1 : Form
    {
        string offlineUsername;
        int ram;
        string mcVer = "1.19.2-forge-43.2.0";
        string verPath = Environment.GetEnvironmentVariable("appdata") + "\\.Elixa";
        IObserver<string> observer;

        void CheckUpdates()
        {
            
            if (Directory.Exists(verPath))
            {
                try
                {
                    string fileVer = System.IO.File.ReadAllText(verPath + "\\ElixaVer.txt");
                    verNum.Text = fileVer;
                }
                catch
                {
                    verNum.Text = "VerFile not Found";
                    
                }

            }
            else
            {
                var updated = Task.Run(async () => await InitialClone());
            }
        }

        void nameProgress(string test)
        {
            downloadText.Text = test;
        }

        async Task<bool> InitialClone()
        {
            var cOptions = new CloneOptions
            {
                Checkout = true,
                CredentialsProvider = null,
                OnTransferProgress = progress =>
                {
                    var p = (100 * progress.ReceivedObjects) / progress.TotalObjects;
                    var receivingMessage = String.Format("Receiving objects:  {0}% ({1}/{2})", p, progress.ReceivedObjects, progress.TotalObjects);
                    observer.OnNext(receivingMessage);
                    return true;
                },
                IsBare = false,
            };
            Repository.Clone("https://github.com/zylonity/Elixa-Modpack", verPath, cOptions);
            string progress = observer;
            Debug.Log(observer)
            return true;
        }

        void CheckGM() //Checks and sets the correct panel according to gamemode
        {

            if((int)Properties.Settings.Default["GameMode"] == 0)
            {
                OfflinePanel.Visible = false;
            }

            if ((int)Properties.Settings.Default["GameMode"] == 1)
            {
                SelectMC.SelectedIndex = 0;
                OfflinePanel.Visible = true;
            }

            if ((int)Properties.Settings.Default["GameMode"] == 2)
            {
                SelectMC.SelectedIndex = 1;
                OfflinePanel.Visible = false;
            }

        }

        void OffPlayButtonActive()
        {
            bool usernameValid = false;


            if (offlineUsername.Length >= 4 && offlineUsername.Contains(" ") == false && offlineUsername != "")
            {
                usernameValid = true;
            }
            else
            {
                usernameValid = false;
            }

            OfflinePlay.Enabled = usernameValid;
        }


        void CheckOffUsername()
        {
            offlineUsername = (string)Properties.Settings.Default["OfflineUsername"];
            OfflineUsernameBox.Text = offlineUsername;

            checkBox1.Checked = (bool)Properties.Settings.Default["SaveOfflineUsername"];

        }


        void resetRam()
        {
            ram = (int)Properties.Settings.Default["Ram"];
            ramBox.Text = ram.ToString();

            if (ram <= 0)
            {
                ramError.Visible = true;
                OfflinePlay.Enabled = false;
            }
            else
            {
                ramError.Visible = false;
                OfflinePlay.Enabled = true;
                OffPlayButtonActive();
            }

        }

        public Form1()
        {
            InitializeComponent();
            CheckUpdates();
            CheckGM();
            CheckOffUsername();
            OffPlayButtonActive();
            resetRam();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SelectMC.SelectedIndex == 0)
            {
                Properties.Settings.Default["GameMode"] = 1;
                Properties.Settings.Default.Save();
                CheckGM();
            }
            else if(SelectMC.SelectedIndex == 1)
            {
                Properties.Settings.Default["GameMode"] = 2;
                Properties.Settings.Default.Save();
                CheckGM();
            }
            else
            {
                Properties.Settings.Default["GameMode"] = 0;
                Properties.Settings.Default.Save();
                CheckGM();
            }
        } //Deals with offline/online box



        private void OfflineUsernameBox_TextChanged(object sender, EventArgs e)
        {
            offlineUsername = OfflineUsernameBox.Text;
            if ((bool)Properties.Settings.Default["SaveOfflineUsername"] == true)
            {
                Properties.Settings.Default["OfflineUsername"] = offlineUsername;
                Properties.Settings.Default.Save();
            }
            OffPlayButtonActive();
        }

        private async void OfflinePlay_Click(object sender, EventArgs a)
        {
            OfflinePlay.Enabled = false;

            System.Net.ServicePointManager.DefaultConnectionLimit = 256;

            var elixaPath = new MinecraftPath(Environment.GetEnvironmentVariable("appdata") + "\\.Elixa");

            var launcher = new CMLauncher(elixaPath);

            var process = await launcher.CreateProcessAsync(mcVer, new MLaunchOption
            {
                MaximumRamMb = ram,
                Session = MSession.GetOfflineSession(offlineUsername),
            });

            process.Start();
            this.WindowState = FormWindowState.Minimized;

            process.WaitForExit();
            OfflinePlay.Enabled = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["SaveOfflineUsername"] = checkBox1.Checked;

            if ((bool)Properties.Settings.Default["SaveOfflineUsername"] == true)
                Properties.Settings.Default["OfflineUsername"] = offlineUsername;
            else
                Properties.Settings.Default["OfflineUsername"] = "";

            Properties.Settings.Default.Save();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ram = Int32.Parse(ramBox.Text);
                if (ram <= 0)
                {
                    ramError.Visible = true;
                    OfflinePlay.Enabled = false;
                }
                else
                {
                    ramError.Visible = false;
                    OfflinePlay.Enabled = true;
                    Properties.Settings.Default["Ram"] = ram;
                    Properties.Settings.Default.Save();
                }
            }
            catch
            {
                ramError.Visible = true;
                OfflinePlay.Enabled = false;
            }


            
        }

    }
}
