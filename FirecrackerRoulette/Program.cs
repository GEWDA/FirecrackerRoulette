using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirecrackerRoulette
{
    class Program
    {



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());//form 2 instead of form 1, as the main method glitched out and was causing errors for no reason
        }//                             this gave me a great opportunity for a welcome message though


    }
}
