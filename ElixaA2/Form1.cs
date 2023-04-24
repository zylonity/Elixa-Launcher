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
        string sRepo = "https://github.com/zylonity/Elixa-Modpack";

        bool validateFiles = false;


        //Currently only checks for the initial download, if files are missing then download everything from the repo
        void CheckUpdates()
        {
            
            if (Directory.Exists(verPath))
            {
                if (validateFiles)
                {
                    using (var repo = new Repository(verPath))
                    {
                        // Fetch the remote branch and its tree
                        var remoteBranch = repo.Branches["main"];
                        var remoteCommit = repo.Lookup<Commit>(remoteBranch.Tip.Sha);
                        var remoteTree = remoteCommit.Tree;

                        // Get the directories and files in the local directory and its subdirectories
                        var localDirectories = Directory.GetDirectories(verPath, "*", SearchOption.AllDirectories)
                            .Select(path => GetRelativePath(verPath, path))
                            .ToList();
                        var localFiles = Directory.GetFiles(verPath, "*", SearchOption.AllDirectories)
                            .Select(path => GetRelativePath(verPath, path))
                            .ToList();

                        // Traverse the remote tree recursively, checking for missing directories and files
                        var missingDirectories = new List<string>();
                        var missingFiles = new List<string>();
                        TraverseTree(remoteTree, verPath, localDirectories, localFiles, ref missingDirectories, ref missingFiles);

                        // Print out the missing directories and files
                        foreach (var directory in missingDirectories)
                        {
                            Console.WriteLine($"Missing directory: {directory}");
                        }
                        foreach (var file in missingFiles)
                        {
                            Console.WriteLine($"Missing file: {file}");
                        }
                    }
                }

                using (var repo = new Repository(verPath))
                {
                    var branch = repo.Head;

                    var remote = repo.Network.Remotes["origin"];
                    var localTip = branch.Tip;
                    var remoteTip = remote.FetchRefSpecs;



                    if (remoteTip.ToList().Count() == 0)
                    {
                        Console.WriteLine("You are on the latest commit.");
                    }
                    else
                    {
                        var rremote = repo.Network.Remotes["origin"];

                        var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                        Console.WriteLine("You are not on the latest commit.");


                    }
                }

            }
            else
            {

                InitialClone();
                
            }
        }


        async void UpdateMods()
        {
            Downloading downloadWindow = new Downloading();
            downloadWindow.Activate();
            downloadWindow.Show();
            downloadWindow.Update();
            downloadWindow.TopMost = true;
            float percentDone;

            var cOptions = new CloneOptions
            {
                //Parameters to check for progress
                OnTransferProgress = progress =>
                {
                    percentDone = ((float)progress.IndexedObjects / (float)progress.TotalObjects * 100.0f);
                    downloadWindow.progressBar1.Value = (int)percentDone;
                    return true;
                },
            };
            Repository.Clone(sRepo, verPath, cOptions);
            downloadWindow.Close();
        }

        //Clones everything
        async void InitialClone()
        {
            Downloading downloadWindow = new Downloading();
            downloadWindow.Activate();
            downloadWindow.Show();
            downloadWindow.Update();
            downloadWindow.TopMost = true;
            float percentDone;

            var cOptions = new CloneOptions
            {
                //Parameters to check for progress
                OnTransferProgress = progress =>
                {
                    percentDone = ((float)progress.IndexedObjects / (float)progress.TotalObjects * 100.0f);
                    downloadWindow.progressBar1.Value = (int)percentDone;
                    return true;
                },
            };
            Repository.Clone(sRepo, verPath, cOptions);
            downloadWindow.Close();
        }


        //Checks if offline or microsoft
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

        //Checks offline username
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


        //Saves offline username
        void CheckOffUsername()
        {
            offlineUsername = (string)Properties.Settings.Default["OfflineUsername"];
            OfflineUsernameBox.Text = offlineUsername;

            checkBox1.Checked = (bool)Properties.Settings.Default["SaveOfflineUsername"];

        }


        //Resets the ram per play
        void resetRam()
        {
            ram = (int)Properties.Settings.Default["Ram"];

            if (ram <= 0)
            {
                OfflinePlay.Enabled = false;
            }
            else
            {
                OfflinePlay.Enabled = true;
                OffPlayButtonActive();
            }

        }

        //Initialises everything
        public Form1()
        {
            CheckUpdates();
            InitializeComponent();
            CheckGM();
            CheckOffUsername();
            OffPlayButtonActive();
            resetRam();
        }

        //Deals with the box for microsoft/offline
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
        } 



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

        //Pressing play in offline mode
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


        //Save offline username checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["SaveOfflineUsername"] = checkBox1.Checked;

            if ((bool)Properties.Settings.Default["SaveOfflineUsername"] == true)
                Properties.Settings.Default["OfflineUsername"] = offlineUsername;
            else
                Properties.Settings.Default["OfflineUsername"] = "";

            Properties.Settings.Default.Save();
        }

        //Replaces relativePath thing that .net framwork doesnt have
        private static string GetRelativePath(string rootPath, string fullPath)
        {
            var rootUri = new Uri(rootPath.EndsWith("\\") ? rootPath : rootPath + "\\");
            var fullUri = new Uri(fullPath);
            var relativeUri = rootUri.MakeRelativeUri(fullUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        //Looks through directories and compares the files.
        private static void TraverseTree(Tree tree, string currentPath, List<string> localDirectories, List<string> localFiles, ref List<string> missingDirectories, ref List<string> missingFiles)
        {
            foreach (var entry in tree)
            {
                if (entry.TargetType == TreeEntryTargetType.Blob)
                {
                    // Check for missing files
                    var fullPath = Path.Combine(currentPath, entry.Path);
                    if (!localFiles.Contains(entry.Path))
                    {
                        missingFiles.Add(entry.Path);
                    }
                }
                else if (entry.TargetType == TreeEntryTargetType.Tree)
                {
                    // Check for missing directories
                    var fullPath = Path.Combine(currentPath, entry.Path);
                    if (!localDirectories.Contains(entry.Path))
                    {
                        missingDirectories.Add(entry.Path);
                    }

                    // Recursively traverse the subdirectory
                    var subTree = (Tree)entry.Target;
                    TraverseTree(subTree, fullPath, localDirectories, localFiles, ref missingDirectories, ref missingFiles);
                }
            }
        }

        private async void settingsButton_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Activate();
            settings.Show();
            settings.Update();
        }
    }
}
