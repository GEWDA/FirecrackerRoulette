using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using AudioSwitcher.AudioApi.CoreAudio;
using WMPLib;
using Timer = System.Timers.Timer;

namespace FirecrackerRoulette
{
    public class Roulette
    {

        public class Firecracker
        {
            private Roulette parent;//this is probably bad practice, passing the entire parent to the child class
            public Firecracker(Roulette parent)
            {//I needed to ensure the sound player actually loads when the firecracker is generated, 
                //otherwise there's a huge lag spike upon cracker detonation
                TheSound.URL = AppDomain.CurrentDomain.BaseDirectory + "LitFuseLong.wav";

                 this.parent = parent;//i'm sure i will realize how bad of an idea this is in future

                VolumeModifier = 1;
                ElapsedEventCounter = 0;
            }

            public Firecracker()//necessary due to the FirecrackersArray property in Roulette class, and that can't change
            {
                VolumeModifier = 1;
                ElapsedEventCounter = 0;
            }

            
            public int ChangeFirecrackerState()
            {
                ElapsedEventCounter += 1;
                return parent.ChangePicture(ElapsedEventCounter,this);
            }

            //fields
            public int VolumeModifier;

            //constants
            private const int EXPLOSION_VOLUME = 100;
            private const int NORMAL_VOLUME = 50;
            public int STATE_CHANGE_TIME = 5000;
                     
            //properties
            private bool _isDangerous = false;
            public bool IsDangerous
            {
                get { return _isDangerous; }
                set { _isDangerous = value; } //if true, is the 'bullet'
            }

            public int ElapsedEventCounter { get; set; }

            private WindowsMediaPlayer _player = new WindowsMediaPlayer();
                public WindowsMediaPlayer TheSound
                {
                    get { return _player; }
                    set { _player = value; }
                }



            //methods


            //loading the sounds
            public void ChooseSound()
            {
                if (IsDangerous)
                {
                    TheSound.URL = AppDomain.CurrentDomain.BaseDirectory+ "TheExplosion.wav";
                }
                else
                {
                    TheSound.URL = AppDomain.CurrentDomain.BaseDirectory + "NormalFirecracker.wav";
                }
            }
                
            public void PlaySound()
            {
                
                
                SetVolume((IsDangerous ? EXPLOSION_VOLUME : NORMAL_VOLUME)/VolumeModifier);
                ChooseSound();//turns out selecting a sound plays it, if it is different from the currently selected sound
            }

            [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]//because ReSharper doesn't like this method
            private void SetVolume(decimal number)//unmutes the speakers and sets the volume
            {
                //installed NuGet AudioSwitcher.AudioApi.CoreAudio 3.0.0.1 for this method
                CoreAudioDevice speakers = new CoreAudioController().DefaultPlaybackDevice;
                if (speakers.IsMuted)
                {
                    speakers.ToggleMute();
                }
                speakers.Volume = (double) number;//so that's what casting is... handy
            }





        }//end of Firecracker class


        //fields
        public int CurrentScore;
        public int HighScore;
        public int Wins;
        public int Losses;
        public int RemainingThrows;
        public int changeID;
        public bool hasLost;
        public bool hasWon;

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
                LocalFirecrackerArray[i] = new Firecracker(this);
            }
            return LocalFirecrackerArray;
        }

        public int ChangePicture(int eventCounter,Firecracker theSender)
        {
            int ID = 0;
            foreach (var cracker in FirecrackersArray)
            {
                if (Equals(cracker, theSender))
                {
                    return ID;
                }
                ID += 1;
            }
            return -1;//will never happen (famous last words...)
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
            RemainingThrows = 1;
            FirecrackersArray = GenerateFirecrackers(Convert.ToInt16(firecrackerValue));
            MakeSomeLethal(Convert.ToInt16(lethalValue));
            foreach (Firecracker thing in FirecrackersArray)
            {
                RemainingThrows += thing.IsDangerous ? 1 : 0;//this means you will always have exactly one spare throw
            }//this means you can win without getting the high score

            //run
            FusesBurning();
            //potentially high scores next???
            //todo:reset properly, then highscores?
        }

        private void FusesBurning()
        {
            //i think the actual visual prompts will have to be on form, as the game can't access the form
            
            //todo: set up timer based firecracker countdown with button to speed up to next firecracker
        }

    }
}
