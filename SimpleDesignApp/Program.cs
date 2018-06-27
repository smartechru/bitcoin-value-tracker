using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SimpleDesignApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process[] process = Process.GetProcessesByName("SimpleDesignApp");
            if (process.Length > 1)
                return;
            Mainform mf = new Mainform();
            Application.Run(mf);
        }
    }
}
