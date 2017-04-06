using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;//used to cause the firecrackers to change image

namespace FirecrackerRoulette
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }
        public Roulette Game = new Roulette();


        private void testButtons_Click(object sender, EventArgs e)//manual debugging/testing
        {
            //testing sound
            var testButtons = sender as Button;
            var testFirecracker = new Roulette.Firecracker() { IsDangerous = testButtons.Name == "testButton1" || testButtons.Name == "testButton3"};
            testFirecracker.VolumeModifier = testButtons.Name == "testButton1" || testButtons.Name == "testButton2" ? 1 : 5;
            testFirecracker.PlaySound();
        }

        private void InvertControlsVisibility()//inverts the visibility of MOST controls, and sets the visibility of some
        {
            foreach (Control thing in Controls)
            {
                //I want to 'manually' set visibility of the picture boxes to true...
                if (!(thing is PictureBox))//can't use != operator to check type, so an extra set of brackets are used to invert the bool
                {
                    thing.Visible = !thing.Visible ;//another bool inversion
                }//...however, i want them to be set to false 'automatically'
                if (thing is PictureBox && thing.Visible==true)
                {
                    thing.Visible = false;
                }
            }
            foreach (var picture in Controls.OfType<PictureBox>())//remove all borders from all picture boxes
            {
                picture.BorderStyle=BorderStyle.None;
                picture.Image = Properties.Resources.unlit_firecracker;//and return to the default picture
            }
            lblCurrentScore.Visible = true;//these should always be visible
            Game.CurrentScore = 0;//this gets reset whenever the game stops or starts 
            lblHighScore.Visible = true;
            lblLosses.Visible = true;
            lblWins.Visible = true;
        }

        public void ShowFirecrackers(int howMany)
        {
            for (int i = 1; i <= howMany; i++)
            {
                foreach ( Control picture in this.Controls)//technically fetches all of the labels and buttons too, but they aren't called "PictureBox'n'"
                {
                    if (picture.Name =="pictureBox"+i.ToString())//this is why i left the picture boxes with the default names
                    {
                        picture.Visible = true;//this way, only the picture boxes that are needed are visible
                    }
                }
            }
        }

        public void ChangeImage(object source, ElapsedEventArgs e)//this event changes the image, and also evaluates whether the 
        {//                                                         changed image means the cracker explodes
            foreach (var cracker in Game.FirecrackersArray)
            {
                var ID = cracker.ChangeFirecrackerState();//adds one to the selected firecracker's ElapsedEventCounter, and returns its position in the array
                foreach (var picture in this.Controls.OfType<PictureBox>())//OfType is necessary here, as I use picture.Image in this loop
                {
                    if (picture.Visible && picture.Name == "pictureBox" + (ID+1).ToString())//names do not start at 0, hence the +1
                    {
                        switch (Game.FirecrackersArray[ID].ElapsedEventCounter)//indexes do start at 0, hence the lack of a +1
                        {
                            case 1:
                                picture.Image=Properties.Resources.lit_firecracker;//if cracker was unlit, it's now lit
                                break;
                            case 2:
                                picture.Image = Properties.Resources.exploding_firecracker;//if cracker was lit, it now has almost no fuse
                                break;
                            case 3:
                                Game.FirecrackersArray[ID].PlaySound();//if cracker was almost exploded, it now IS exploded
                                picture.Image = Properties.Resources.spark;
                                if (Game.FirecrackersArray[ID].IsDangerous)//if it would explode and kill you...
                                {
                                    if (Game.FirecrackersArray[ID].VolumeModifier==5)//...but you threw it...
                                    {
                                        Game.CurrentScore += 1;//...you get a point!
                                    }
                                    else if(!Game.hasLost)//this if statement means you can only lose once per reset
                                    {//                     which is very important if there is more than one explosive.
                                        Game.hasLost = true;
                                        GameOver();
                                    }
                                }
                                break;
                            default://                                  If the cracker has already exploded...
                                if (!Game.hasWon && !Game.hasLost)//... and you're not dead, and haven't already won...
                                {//
                                    cbxFirecrackers.Invoke(new MethodInvoker(delegate
                                    {
                                        Game.hasWon = ID == cbxFirecrackers.SelectedIndex;//...and this is the last cracker...
                                    }));
                                    if (Game.hasWon)
                                    {
                                        GameOver(true);//...then you win!
                                    }
                                }                                    
                                break;
                        }
                    
                    }
                }
                Thread.Sleep(3000);//due to the multithreading, just using sleep does not stop user input
            }
            UpdateLabels();//triggers per firecracker exploding
        }

        private void UpdateLabels(bool all = false)
        {
            LabelInvokerMethod(lblCurrentScore,Game.CurrentScore);//I will always want to update the current score
            if (all)//I will only want to update the other labels when the game ends
            {
                LabelInvokerMethod(lblHighScore,Game.HighScore);
                LabelInvokerMethod(lblLosses,Game.Losses);
                LabelInvokerMethod(lblWins,Game.Wins);
            }            
        }

        void LabelInvokerMethod(Control label,int value = 0)//due to the multithreading, this is how change the labels
        {
            label.Invoke(new MethodInvoker(delegate
            {
                label.Text = label.Text.Substring(0, label.Text.Length - (value > 10 ? 2 : 1))+value.ToString();//this will work for any label that changes
                label.Update();//forces the result to actually show
            }));
            return;            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            GC.Collect();//apparently this is a terrible idea in large programs, but i need to get rid of the timer
            InvertControlsVisibility();
            cbxLethal.SelectedIndex = 0;//value of that index is 1
            cbxFirecrackers.SelectedIndex = 5;//value of that index is 6, so these are the defaults for a classic game of roulette
            foreach (var cracker in Game.FirecrackersArray)
            {
                cracker.ElapsedEventCounter = 0;//puts all of the used firecrackers back to their original states
            }
            
        }

        private void GameOver(bool won = false)//triggers when you win or lose
        {
            Game.HighScore = Game.CurrentScore > Game.HighScore ? Game.CurrentScore : Game.HighScore;
            if (won)
            {
                MessageBox.Show("You have survived");
                Game.Wins += 1;
            }
            else
            {
                MessageBox.Show("You have perished");
                Game.Losses += 1;
            }
            UpdateLabels(true);
            btnReset.Invoke(new MethodInvoker(delegate { btnReset.PerformClick(); }));//multithreading error avoidance
        }

        public void btnGo_Click(object sender, EventArgs e)//sets some things to their default values, and begins game
        {
            InvertControlsVisibility();
            lblCurrentScore.Text = "Score:  0";
            lblRemainingThrows.Text = "Remaining Throws:  0";
            Game.hasLost = false;
            Game.hasWon = false;
            ShowFirecrackers(cbxFirecrackers.SelectedIndex+1);
            Timer crackerCountdown = new Timer();//is what actually sets off the changing pictures

            crackerCountdown.Interval = Game.FirecrackersArray[0].STATE_CHANGE_TIME;//FirecrackersArray[0] always exists so I
            //                                                                      can use it to pass generic unchanging data
            crackerCountdown.Elapsed += ChangeImage;//                              like this technically-not-constant constant

            crackerCountdown.Enabled = true;
            Game.LightTheFuses(cbxFirecrackers.SelectedIndex+1,cbxLethal.SelectedIndex+1);
            lblRemainingThrows.Text="Remaining Throws:  "+Game.RemainingThrows.ToString();//allows the user to see how many 
        }//                                                                                 throws they will have before they do any

        public void Form1_Load(object sender, EventArgs e)
        {
            btnReset.Visible = false;
            lblRemainingThrows.Visible = false;//these two items are invisible until the game starts
            cbxLethal.SelectedIndex = 0;//value is 1
            cbxFirecrackers.SelectedIndex = 5;//value is 6, defaults for a classic roulette game
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;//prevents the user from 'accidentally' seeing debug mode
        }//                                                     by resizing the form

        private void debuggingToolStripMenuItem_Click(object sender, EventArgs e)//settings for test buttons
        {
            testButton1.Visible = true;
            testButton2.Visible = true;
            testButton3.Visible = true;
            testButton4.Visible = true;
            const int theDebugWidth = 900;
            const int theNormalWidth = 800;
            this.Width = this.Width == theDebugWidth ? theNormalWidth : theDebugWidth;//makes them all visible, then extends the
        }//                                                                             form to make them visible

        private void ThrowingThisOne(object sender, EventArgs e)//happens when a picture box is clicked
        {
            var isItLethal = sender as PictureBox;
            if (Game.RemainingThrows <=0)
            {
                return;//if they can't throw, nothing happens
            }
            isItLethal.BorderStyle = BorderStyle.Fixed3D;//you can see which ones you have thrown
            int index=Convert.ToInt16(isItLethal.Name.Substring(10))-1;//takes the 'pictureBox' out of 'pitureBox1', and then adjusts it to reflect the correct index
            if (Game.FirecrackersArray[index].VolumeModifier==5)
            {
                return;//if they have already thrown, nothing happens
            }
            Game.FirecrackersArray[index].VolumeModifier = 5;//sound will reduce when playing the sound from a thrown cracker
            Game.RemainingThrows -= 1;
            lblRemainingThrows.Text = lblRemainingThrows.Text.Substring(0, 18) + Game.RemainingThrows.ToString();//18 happens to be the length of "Remaining throws:  "
        }        
    }
}
