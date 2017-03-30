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
    internal class Roulette
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





            //methods



            //loading the sounds
            public WindowsMediaPlayer[] ChooseSound(bool explosion = false)
            {
                WindowsMediaPlayer[] player = new WindowsMediaPlayer[1];
                WindowsMediaPlayer theSound = new WindowsMediaPlayer();
                if (explosion)
                {
                    theSound.URL = AppDomain.CurrentDomain.BaseDirectory+"TheExplosion.wav";
                }
                else
                {
                    theSound.URL = AppDomain.CurrentDomain.BaseDirectory + "NormalFirecracker.wav";
                }
                player[0] = theSound;
                return player;
            }
                
            public void PlaySound(bool explosion = false)
            {
                var sound = ChooseSound(explosion)[0];
                SetVolume(explosion ? EXPLOSION_VOLUME : NORMAL_VOLUME);
                //                                                          todo:unmute sound here
                sound.controls.play();
            }

            [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]//because ReSharper doesn't like this method
            private void SetVolume(int number)
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

        public Firecracker[] FirecrackersArray = { new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker(), new Firecracker() };


        //public void GenerateFirecrackers(int size)                      //todo:Replace the above array with this method
        //{
        //    Firecracker[] FirecrackerArray = new Firecracker[size];
        //    for (int i = 0; i<size; i++)
        //    {
        //        FirecrackerArray[i] = new Firecracker();
        //    }
        //}



        //methods
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
            int[] LethalNumbersArray = GenerateLethalNumbers(lethal,FirecrackersArray.Length) ;
            foreach (var explosive in LethalNumbersArray)
            {
                FirecrackersArray[explosive].IsDangerous = true;
            }
        }


        /// <summary>
        /// Starts when the player presses go. The actual 'game start' method
        /// </summary>
        public void LightTheFuses()
        {
            //setup
            //GenerateFirecrackers(6);
            MakeSomeLethal(1);//todo: change '1' to a choice by user
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
