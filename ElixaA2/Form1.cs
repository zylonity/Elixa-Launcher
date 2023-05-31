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
using System.Runtime.InteropServices;
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
        string modPath = Environment.GetEnvironmentVariable("appdata") + "\\.Elixa\\mods";
        string sRepo = "https://github.com/zylonity/Elixa-Modpack";

        bool validateFiles = false;
        bool updating = false;

        Downloading downloadWindow;


        //Currently only checks for the initial download, if files are missing then download everything from the repo
        async Task<bool> CheckUpdates()
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


                string logMessage = "";
                using (var repo = new Repository(verPath))
                {
                    var remote = repo.Network.Remotes["origin"];
                    var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                    Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);

                    var updates = repo.Diff.Compare<TreeChanges>(repo.Head.Tip.Tree, repo.Branches[repo.Head.FriendlyName].TrackedBranch.Tip.Tree);

                    if (updates.Count > 0)
                    {
                        updating = true;
                        Console.WriteLine("Updates");

                        Downloading updateWindow = new Downloading();
                        updateWindow.Show();
                        updateWindow.Text = "Updating";
                        updateWindow.DownloadBigLabel.Text = "Updating";
                        updateWindow.progressBar1.Hide();
                        updateWindow.Activate();
                        updateWindow.Update();

                        if (System.IO.File.Exists(modPath + "\\ModList.txt"))
                        {
                            System.IO.File.Copy((modPath + "\\ModList.txt"), (modPath + "\\OldModList.txt"), true);
                        }

                        MergeResult mergeResult;
                        try
                        {
                            mergeResult = repo.Merge(repo.Branches[repo.Head.FriendlyName].TrackedBranch, repo.Config.BuildSignature(DateTimeOffset.Now));
                        }
                        catch (CheckoutConflictException)
                        {
                            // If there's a conflict, reset the repo and try the merge again
                            repo.Reset(ResetMode.Hard);
                            mergeResult = repo.Merge(repo.Branches[repo.Head.FriendlyName].TrackedBranch, repo.Config.BuildSignature(DateTimeOffset.Now));
                        }

                        // If there are conflicts, prioritize the incoming file
                        if (mergeResult.Status == MergeStatus.Conflicts)
                        {
                            foreach (var conflict in repo.Index.Conflicts)
                            {
                                // Directly checkout the remote's version of the file, effectively replacing the local one
                                Commands.Checkout(repo, conflict.Theirs.Path, new CheckoutOptions { CheckoutModifiers = CheckoutModifiers.Force });
                            }

                            Signature merger = repo.Config.BuildSignature(DateTimeOffset.Now);
                            Commit mergeCommit = repo.Commit("Merge commit", merger, merger);
                        }

                        if (mergeResult.Status != MergeStatus.UpToDate)
                        {
                            updateWindow.Close();
                            updateWindow.Hide();
                        }

                        //Deal with mods
                        string[] filePaths = Directory.GetFiles(modPath);

                        List<string> fileNames = filePaths.Select(path => Path.GetFileName(path)).ToList();
                        var downloads = new List<string>();
                        var oldDownloads = new List<string>();
                        var finalDownloads = new List<string>();

                        //Get a list of mods in the list
                        foreach (string line in System.IO.File.ReadAllLines(modPath + "\\ModList.txt"))
                        {
                            if (Uri.IsWellFormedUriString(line, UriKind.Absolute))
                            {
                                downloads.Add(line);
                            }
                        }

                        //Get a list of the old mods
                        foreach (string line in System.IO.File.ReadAllLines(modPath + "\\OldModList.txt"))
                        {
                            if (Uri.IsWellFormedUriString(line, UriKind.Absolute))
                            {
                                oldDownloads.Add(line);
                            }
                        }

                        //Get a list of all the new mods to download
                        foreach (string download in downloads)
                        {
                            Uri mod = new Uri(download);
                            if (!fileNames.Contains(System.IO.Path.GetFileName(mod.LocalPath)))
                            {
                                finalDownloads.Add(download);
                            }
                        }

                        //Get a list of all the old mods to delete
                        foreach (string olddownload in oldDownloads)
                        {

                            if (!downloads.Contains(olddownload))
                            {
                                Uri mod = new Uri(olddownload);
                                if (fileNames.Contains(System.IO.Path.GetFileName(mod.LocalPath)))
                                {
                                    System.IO.File.Delete(modPath + "\\" + System.IO.Path.GetFileName(mod.LocalPath));
                                }
                            }
                        }

                        if (finalDownloads.Count > 0)
                        {
                            downloadWindow = new Downloading();
                            downloadWindow.Activate();
                            downloadWindow.Show();
                            downloadWindow.Update();
                            await Task.Run(async () => await DownloadMods(finalDownloads));
                        }

                        updating = false;

                    }
                    else
                    {
                        Console.WriteLine("No updates");
                    }

                    foreach (TreeEntryChanges c in updates)
                    {
                        Console.WriteLine(c);
                    }
                }
                Console.WriteLine(logMessage);
                OffPlayButtonActive();
            }
            else
            {
                updating = true;
                downloadWindow = new Downloading();
                downloadWindow.Show();
                downloadWindow.Update();
                await InitialClone();
                downloadWindow.Close();
                updating = false;

                var downloads = new List<string>();

                foreach (string line in System.IO.File.ReadAllLines(modPath + "\\ModList.txt"))
                {
                    if (Uri.IsWellFormedUriString(line, UriKind.Absolute))
                    {
                        downloads.Add(line);
                    }
                }

                await Task.Run(async () => await DownloadMods(downloads));

                OffPlayButtonActive();
            }
            return false;
        }


        //Clones everything
        async Task InitialClone()
        {
            downloadWindow.TopMost = true;
            float percentDone;
            float previousPercentDone = 0;

            var cOptions = new CloneOptions
            {
                //Parameters to check for progress
                OnTransferProgress = progress =>
                {
                    percentDone = ((float)progress.IndexedObjects / (float)progress.TotalObjects * 100.0f);
                    if ((int)percentDone > previousPercentDone)
                    {
                        previousPercentDone = (int)percentDone;

                        // Update progress bar on UI thread
                        downloadWindow.Invoke((Action)(() =>
                        {
                            downloadWindow.progressBar1.Value = (int)percentDone;
                            downloadWindow.progressBar1.Update();
                        }));
                    }

                    return true;
                },
            };

            // Run clone in separate task
            await Task.Run(() => Repository.Clone(sRepo, verPath, cOptions));
        }


        async Task DownloadMods(List<string> downloads)
        {
            Console.WriteLine(downloads.Count);

            if (downloads.Count == 1)
            {
                Console.WriteLine(downloads[0]);
                Uri mod = new Uri(downloads[0]);
                string modName = System.IO.Path.GetFileName(mod.LocalPath); //get file name
                Console.WriteLine(downloads[0]);
                using (var client = new WebClient())
                {
                    int index = downloads.FindIndex(a => a.Contains(downloads[0]));

                    client.DownloadFileCompleted += (s, e) => Console.WriteLine("Download file completed.");

                    client.DownloadProgressChanged += (s, e) => downloadWindow.Invoke((MethodInvoker)delegate {
                        downloadWindow.DownloadBigLabel.Text = modName;
                        downloadWindow.progressBar1.Value = (int)e.ProgressPercentage;

                        downloadWindow.Update();
                        Application.DoEvents();

                        if (e.ProgressPercentage >= 100)
                        {
                            try
                            {
                                if (downloads[index + 1] == null) ;

                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                downloadWindow.Hide();
                                downloadWindow.Close();
                            }
                        }

                    });
                    await client.DownloadFileTaskAsync(mod, modPath + "\\" + modName);

                    if (downloads[index + 1] != null)
                    {
                        client.DownloadFileCompleted += (s, e) => Console.WriteLine("Download file completed.");
                    }


                }
            }

            if (downloads.Count > 1)
            {
                foreach (string download in downloads)
                {
                    Console.WriteLine(download);
                    Uri mod = new Uri(download);
                    string modName = System.IO.Path.GetFileName(mod.LocalPath); //get file name
                    Console.WriteLine(download);
                    using (var client = new WebClient())
                    {
                        int index = downloads.FindIndex(a => a.Contains(download));

                        client.DownloadFileCompleted += (s, e) => Console.WriteLine("Download file completed.");

                        client.DownloadProgressChanged += (s, e) => downloadWindow.Invoke((MethodInvoker)delegate {
                            downloadWindow.DownloadBigLabel.Text = modName;
                            downloadWindow.progressBar1.Value = (int)e.ProgressPercentage;

                            downloadWindow.Update();
                            Application.DoEvents();

                            if (e.ProgressPercentage >= 100)
                            {
                                try
                                {
                                    if (downloads[index + 1] == null) ;

                                }
                                catch (ArgumentOutOfRangeException ex)
                                {
                                    downloadWindow.Hide();
                                    downloadWindow.Close();
                                }
                            }

                        });
                        await client.DownloadFileTaskAsync(mod, modPath + "\\" + modName);

                        if (downloads[index + 1] != null)
                        {
                            client.DownloadFileCompleted += (s, e) => Console.WriteLine("Download file completed.");
                        }


                    }

                }
            }
            




        }

        //Checks if offline or microsoft
        void CheckGM() //Checks and sets the correct panel according to gamemode
        {


            if ((int)Properties.Settings.Default["GameMode"] == 0)
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

            if (updating == true)
            {
                usernameValid = false;
            }
            else
            {
                usernameValid = true;
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
