using GTA_GXT_Editor.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace GTA_GXT_Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(args));
            }
            catch (Exception e)
            {
                File.WriteAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "error.txt"), $"{e.Message}");
            }
        }
    }
}
