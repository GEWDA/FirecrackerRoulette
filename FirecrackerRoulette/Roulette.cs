using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace FirecrackerRoulette
{
    class Roulette
    {

        public class Firecracker
        {
                     
            //properties
            private bool _isDangerous = false;
            public bool IsDangerous
            {
                get { return _isDangerous; }
                set { _isDangerous = value; } //if true, is the 'bullet'
            }





            //methods



            //loading the sounds
            public WindowsMediaPlayer[] ChooseSound(bool explosion = false)
            {
                WindowsMediaPlayer[] player = new WindowsMediaPlayer[1];
                WindowsMediaPlayer TheSound = new WindowsMediaPlayer();
                if (explosion)
                {
                    TheSound.URL = AppDomain.CurrentDomain.BaseDirectory+"TheExplosion.mp3";
                }
                else
                {
                    TheSound.URL = AppDomain.CurrentDomain.BaseDirectory + "NormalFirecracker.mp3";
                }
                player[0] = TheSound;
                return player;
            }
                
            public void PlaySound(bool explosion = false)
            {
                var sound = ChooseSound(explosion)[0].controls;
                sound.play();
            }






        }//end of Firecracker class

        public Firecracker[] FirecrackersArray = { new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker() };


        //public Firecracker[] FirecrackersArray(int size)                      //todo:Replace the above array with this method
        //{
        //    Firecracker[] theArrayofFirecrackers = new Firecracker[size]
        //    for (int i = 0; i<size; i++)
        //    {
        //        theArrayofFirecrackers[i] = new Firecracker();
        //    }
        //}



        //methods
        private int MakeOneLethal()
        {
            var randNum = new Random();
            return randNum.Next(0, FirecrackersArray.Length);
        }

        private void LightTheFuses()
        {
            FirecrackersArray[MakeOneLethal()].IsDangerous = true;
        }

        private void FusesBurning()
        {
            
        }

    }
}
