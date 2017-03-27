using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirecrackerRoulette
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
            Application.Run(new Form1());
        }
    }

    public class Roulette
    {

        public class Firecracker
        {
            //loading the sounds
            protected System.Media.SoundPlayer TheExplosion = new System.Media.SoundPlayer(Environment.CurrentDirectory+@"\theExplosion.wav");//the '@' makes the slash be interpreted as a slash instead of the start of a command (e.g. \n , \t)
            protected System.Media.SoundPlayer NormalFirecracker = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\normalFirecracker.wav");//otherwise two slashes would be required

            //properties
            public bool IsDangerous { get; set; }   //if true, is the 'bullet'




            //methods
            private void PlaySound(bool explosion)
            {
                if (explosion == true)
                {
                    TheExplosion.Play();
                }
                else
                {
                    NormalFirecracker.Play();
                }
            }
        }
            
    }
}
