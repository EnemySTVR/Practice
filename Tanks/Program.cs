using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] { "621", "369", "5", "5" };
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var width = Int32.Parse(args[0]);
            var height = Int32.Parse(args[1]);
            var tanksValue = Int32.Parse(args[2]);
            var appleValue = Int32.Parse(args[3]);
            Application.Run(new GameForm(width, height, tanksValue, appleValue));
        }
    }
}
