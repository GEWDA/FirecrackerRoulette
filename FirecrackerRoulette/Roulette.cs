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
        //classes
        public class Firecracker
        {
            private Roulette parent;//this passes the entire parent to the child class
            public Firecracker(Roulette parent)
            {//I needed to ensure the sound player actually loads when the firecracker is generated, 
                //otherwise there's a huge lag spike upon cracker detonation
                TheSound.URL = AppDomain.CurrentDomain.BaseDirectory + "LitFuseLong.wav";

                 this.parent = parent;//i'm sure this has huge potential to go wrong

                VolumeModifier = 1;
                ElapsedEventCounter = 0;//without these two lines, the resets between games would be very buggy
            }

            public Firecracker()//second constructor needed due to the FirecrackersArray property in Roulette class
            {//                 Also necessary due to the test buttons in debug mode (right click on main form)
                VolumeModifier = 1;
                ElapsedEventCounter = 0;
            }

            
            public int ChangeFirecrackerState()
            {
                ElapsedEventCounter += 1;//counts how many times the current firecracker has changed image
                return parent.ChangePicture(this);//finds its position in the firecracker array and returns it
            }

            //fields
            public int VolumeModifier;

            //constants
            private const int EXPLOSION_VOLUME = 100;
            private const int NORMAL_VOLUME = 50;
            public int STATE_CHANGE_TIME = 5000;//used as a constant, but can not be a constant
                     
            //properties
            private bool _isDangerous = false;
            public bool IsDangerous
            {
                get { return _isDangerous; }
                set { _isDangerous = value; } //if true, is the 'bullet'
            }

            public int ElapsedEventCounter { get; set; }//autoproperty, use described in ChangeFirecrackerState

            private WindowsMediaPlayer _player = new WindowsMediaPlayer();//my '.wav' files weren't true '.wav's and wouldn't work
                public WindowsMediaPlayer TheSound//                    so i have used an alternate library to play sound,
                {//                                                     namely, Windows Media Player
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
                }//changing sounds actually plays it in windows media player
            }
                
            public void PlaySound()
            {
                
                //below line must be before selecting sound, as VolumeModifier is used to check if you are dead
                SetVolume((IsDangerous ? EXPLOSION_VOLUME : NORMAL_VOLUME)/VolumeModifier);//If you throw a firecracker, the sound is reduced to 1/5 of what it would be
                ChooseSound();//turns out selecting a sound plays it, if it is different from the currently selected sound
            }

            [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]//because ReSharper doesn't like this method
            private void SetVolume(decimal number)//unmutes the speakers and sets the volume
            {
                //installed NuGet AudioSwitcher.AudioApi.CoreAudio 3.0.0.1 for this method
                CoreAudioDevice speakers = new CoreAudioController().DefaultPlaybackDevice;
                if (speakers.IsMuted)
                {
                    speakers.ToggleMute();//there's no muting this program
                }
                speakers.Volume = (double) number;//so that's what casting is... handy
            }





        }//end of Firecracker class


        //fields
        public int CurrentScore;//resets every game
        public int HighScore;//stays or gets increased between games
        public int Wins;//stays or gets increased between games
        public int Losses;//stays or gets increased between games
        public int RemainingThrows;//initial value changes every game, decreases per firecracker clicked
        public bool hasLost;//these bools prevent messagebox spamming
        public bool hasWon;//both bools are required if you want proper resets and proper scoring at the same time

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
            Firecracker[] LocalFirecrackerArray = new Firecracker[size];//allows any size game up to an arbitrary limit set by me (20)
            for (int i = 0; i < size; i++)
            {
                LocalFirecrackerArray[i] = new Firecracker(this);//every firecracker made as part of the game gets passed the parent
            }
            return LocalFirecrackerArray;
        }

        public int ChangePicture(Firecracker theSender)//doesn't actually change the picture,
        {//                                             returns the index position in the array
            int ID = 0;//                               of the cracker, so you know which picture to change
            foreach (var cracker in FirecrackersArray)
            {
                if (Equals(cracker, theSender))//I was pleasantly surprised when this worked, then again, why wouldn't it?
                {
                    return ID;//the loop cycles through the array, checking if the sender is the object it is looking at
                }
                ID += 1;//if it's not, then clearly it must be in a later position
            }
            return -1;//will never happen (famous last words...)
        }// above line required, as 'not all code paths return a value' otherwise



        private int[] GenerateLethalNumbers(int lethal, int limit)//pretty self explainatory
        {
            int[] theArray = new int[lethal];//lethal = how many times to randomly swap dynamte with a firecracker
            var randNum = new Random();
            for (int i = 0; i < lethal; i++)//limit = the number of firecrackers
            {
                theArray[i] = randNum.Next(0, limit);//returns an array of length lethal (thenumber of potential 'bullets' in the game), 
            }//                                         with no value greater than limit (the last cracker's position)
            return theArray;
        }

        private void MakeSomeLethal(int lethal)//uses the array of random numbers created above to edit the property IsDangerous
        {//                                     of some firecrackers
            int[] lethalNumbersArray = GenerateLethalNumbers(lethal,FirecrackersArray.Length) ;
            foreach (var explosive in lethalNumbersArray)//the value of the random number dictates which cracker is made dangerous.
            {//                                             This means a cracker can be swapped for dynamite, then the dynamite 
                FirecrackersArray[explosive].IsDangerous = true;//can also be swapped for dynamite. This is intentional, as it
            }//                                             allows games where there are more swaps than there are crackers, while
        }//                                                 still not always being an auto-win scenario


        /// <summary>
        /// Starts when the player presses go. The actual 'game setup' method
        /// </summary>
        public void LightTheFuses(int firecrackerValue = 6, int lethalValue = 1 )//defaults for a normal roulette game
        {
            //setup
            RemainingThrows = 1;//needed a default value, and you will always start with at least 2 throws (I add at least 1 in a few lines)
            FirecrackersArray = GenerateFirecrackers(Convert.ToInt16(firecrackerValue));
            MakeSomeLethal(Convert.ToInt16(lethalValue));
            foreach (Firecracker thing in FirecrackersArray)
            {
                RemainingThrows += thing.IsDangerous ? 1 : 0;//this loop means you will always have exactly one spare throw, as this
            }//                                              relies on how many are dangerous, rather than how many swaps occurred
        }
    }
}
