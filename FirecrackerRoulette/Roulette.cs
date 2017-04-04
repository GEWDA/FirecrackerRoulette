using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.CoreAudio;
using WMPLib;

namespace FirecrackerRoulette
{
    public class Roulette
    {

        public class Firecracker
        {


            //fields and constants
            private const int EXPLOSION_VOLUME = 100;
            private const int NORMAL_VOLUME = 50;
                     
            //properties
            private bool _isDangerous = false;
            public bool IsDangerous
            {
                get { return _isDangerous; }
                set { _isDangerous = value; } //if true, is the 'bullet'
            }


            //private WindowsMediaPlayer _player = {new WindowsMediaPlayer()};

            //public WindowsMediaPlayer TheSound
            //{
            //    get { return _player; }
            //    set { _player = value; }
            //}



            //methods



            //loading the sounds
            public WindowsMediaPlayer ChooseSound(bool explosion = false)
            {
                WindowsMediaPlayer theSound = new WindowsMediaPlayer();
                if (explosion)
                {
                    theSound.URL = AppDomain.CurrentDomain.BaseDirectory+"TheExplosion.wav";
                }
                else
                {
                    theSound.URL = AppDomain.CurrentDomain.BaseDirectory + "NormalFirecracker.wav";
                }
                return theSound;
            }
                
            public void PlaySound(bool explosion = false)
            {
                var sound = ChooseSound(explosion);
                SetVolume(explosion ? EXPLOSION_VOLUME : NORMAL_VOLUME);                                                        
                (new Task(() =>sound.controls.play())).RunSynchronously();//run syncronously to avoid sound clipping
            }

            [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]//because ReSharper doesn't like this method
            private void SetVolume(int number)//unmutes the speakers and sets the volume
            {
                //installed NuGet AudioSwitcher.AudioApi.CoreAudio 3.0.0.1 for this method
                CoreAudioDevice speakers = new CoreAudioController().DefaultPlaybackDevice;
                if (speakers.IsMuted)
                {
                    speakers.ToggleMute();
                }
                speakers.Volume = number;
            }





        }//end of Firecracker class

        //properties
        private Firecracker[] _FirecrackersArray = { new Firecracker() };//needed just so it's not null, gets overwritten later
        public Firecracker[] FirecrackersArray
        {
            get { return _FirecrackersArray; }
            set { _FirecrackersArray = value; }
        }






        //methods
        public Firecracker[] GenerateFirecrackers(int size)
        {
            Firecracker[] LocalFirecrackerArray = new Firecracker[size];
            for (int i = 0; i < size; i++)
            {
                LocalFirecrackerArray[i] = new Firecracker();
            }
            return LocalFirecrackerArray;
        }

        private int[] GenerateLethalNumbers(int lethal, int limit)
        {
            int[] theArray = new int[lethal];
            var randNum = new Random();
            for (int i = 0; i < lethal; i++)
            {
                theArray[i] = randNum.Next(0, limit);
            }
            return theArray;
        }

        private void MakeSomeLethal(int lethal)
        {
            int[] lethalNumbersArray = GenerateLethalNumbers(lethal,FirecrackersArray.Length) ;
            foreach (var explosive in lethalNumbersArray)
            {
                FirecrackersArray[explosive].IsDangerous = true;
            }
        }


        /// <summary>
        /// Starts when the player presses go. The actual 'game start' method
        /// </summary>
        public void LightTheFuses(int firecrackerValue = 6, int lethalValue = 1 )
        {
            //setup

            FirecrackersArray = GenerateFirecrackers(Convert.ToInt16(firecrackerValue));
            MakeSomeLethal(Convert.ToInt16(lethalValue));

            //run
            FusesBurning();
            //potentially high scores next???
            //todo:reset properly, then highscores?
        }

        private void FusesBurning()
        {
            
            //todo: set up timer based firecracker countdown with button to speed up to next firecracker
        }

    }
}
