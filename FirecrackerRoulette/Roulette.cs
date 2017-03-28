using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace FirecrackerRoulette
{
    class Roulette
    {
        public class Firecracker
        {
            //loading the sounds
            protected System.Media.SoundPlayer TheExplosion = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\TheExplosion.wav");//the '@' makes the slash be interpreted as a slash instead of the start of a command (e.g. \n , \t)
            protected System.Media.SoundPlayer NormalFirecracker = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\NormalFirecracker.wav");//otherwise two slashes would be required

            //properties

            private bool _isDangerous = false;

            public bool IsDangerous
            {
                get { return _isDangerous; }
                set { _isDangerous = value; } //if true, is the 'bullet'
            }




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
        }//end of Firecracker class



        Firecracker[] FirecrackersArray = new Firecracker[6];//consider future proofing here, replace 6 with a property

        //FirecrackersArray[0] = Firecracker Firecracker1 = new Firecracker();
        Firecracker Firecracker1 = new Firecracker();
        Firecracker Firecracker2 = new Firecracker();
        Firecracker Firecracker3 = new Firecracker();
        Firecracker Firecracker4 = new Firecracker();
        Firecracker Firecracker5 = new Firecracker();
        Firecracker Firecracker6 = new Firecracker();
    }
}
