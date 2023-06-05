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
using System.Runtime.InteropServices;

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

        }


        //sets ram
        public void setRam()
        {
            ram = (int)Properties.Settings.Default["Ram"];

        }

        //Initialises everything
        public Form1()
        {
            //CheckUpdates();
            InitializeComponent();
            CheckOffUsername();
            OffPlayButtonActive();
            setRam();
        }



        private void OfflineUsernameBox_TextChanged(object sender, EventArgs e)
        {
            offlineUsername = OfflineUsernameBox.Text;
            Properties.Settings.Default["OfflineUsername"] = offlineUsername;
            Properties.Settings.Default.Save();
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



        private void OfflinePlay_MouseEnter(object sender, EventArgs e)
        {
            OfflinePlay.Image = Properties.Resources.jugar_cursor_encima;
        }
        private void OfflinePlay_MouseLeave(object sender, EventArgs e)
        {
            OfflinePlay.Image = Properties.Resources.jugar;
        }

        private void OfflinePlay_MouseDown(object sender, MouseEventArgs e)
        {
            OfflinePlay.Image = Properties.Resources.jugar2;
        }

        private void OfflinePlay_MouseUp(object sender, MouseEventArgs e)
        {
            OfflinePlay.Image = Properties.Resources.jugar;
        }

        private void settingsButton_MouseDown(object sender, MouseEventArgs e)
        {
            settingsButton.Image = Properties.Resources.config2;
        }

        private void settingsButton_MouseUp(object sender, MouseEventArgs e)
        {
            settingsButton.Image = Properties.Resources.config;
        }

        private void settingsButton_MouseEnter(object sender, EventArgs e)
        {
            settingsButton.Image = Properties.Resources.config_cursor_encima;
        }

        private void settingsButton_MouseLeave(object sender, EventArgs e)
        {
            settingsButton.Image = Properties.Resources.config;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void minimizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            minimizeButton.Image = Properties.Resources.minimizar2;
        }

        private void minimizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            minimizeButton.Image = Properties.Resources.minimizar;
        }

        private void minimizeButton_MouseEnter(object sender, EventArgs e)
        {
            minimizeButton.Image = Properties.Resources.minimizar_cursor_encima;
        }

        private void minimizeButton_MouseLeave(object sender, EventArgs e)
        {
            minimizeButton.Image = Properties.Resources.minimizar;
        }

        private void closeButton_MouseDown(object sender, MouseEventArgs e)
        {
            closeButton.Image = Properties.Resources.cerrar2;
        }

        private void closeButton_MouseUp(object sender, MouseEventArgs e)
        {
            closeButton.Image = Properties.Resources.cerrar;
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.Image = Properties.Resources.cerrar_cursor_encima;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.Image = Properties.Resources.cerrar;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
