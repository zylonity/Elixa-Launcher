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


        Downloading downloadWindow;


        //Currently only checks for the initial download, if files are missing then download everything from the repo
        async Task<bool> CheckUpdates()
        {

            if (!Directory.Exists(verPath))
            {
                downloadWindow = new Downloading();
                //downloadWindow.Activate();
                downloadWindow.Show();
                //downloadWindow.Update();
                InitialClone();
            }
            return false;
        }


        //Clones everything
        void InitialClone()
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
                        downloadWindow.progressBar1.Value = (int)percentDone;
                        //downloadWindow.Focus();
                        downloadWindow.progressBar1.Update();

                    }

                    return true;
                },
            };

            Repository.Clone(sRepo, verPath, cOptions);


        }


        //Initialises everything
        public Form1()
        {
            Task.Run(async () => await CheckUpdates());
            InitializeComponent();

        }


        
    }
}
