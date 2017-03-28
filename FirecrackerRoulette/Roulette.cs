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
            protected SoundPlayer TheExplosion = new SoundPlayer(MediaFiles.TheExplosion);
            protected SoundPlayer NormalFirecracker = new SoundPlayer(MediaFiles.NormalFirecracker);
            
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

        public Firecracker[] FirecrackersArray = { new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker() };
    }
}
