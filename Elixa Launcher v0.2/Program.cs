using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace Elixa_Launcher_v0._2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {

            using (var mgr = new UpdateManager("C:\\Users\\kkhal\\source\\repos\\Elixa Launcher v0.2\\RELEASES"))
            {
                await mgr.UpdateApp();
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}
