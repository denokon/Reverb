using System;
using System.Windows.Forms;
using Un4seen.Bass;

namespace Reverb
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static NetBackend Net;
        
        [STAThread]
        static void Main()
        {
            Net = new NetBackend();
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
