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
using Timer = System.Timers.Timer;

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

        private void InvertControlsVisibility()
        {
            foreach (Control thing in Controls)
            {
                //I want to 'manually' set visibility of the picture boxes to true...
                if (!(thing is PictureBox))//can't use != operator to check type, so an extra set of brackets are used to invert the bool
                {
                    thing.Visible = !thing.Visible ;//another bool inversion
                }//...however, i want them to be set to false automatically
                if (thing is PictureBox && thing.Visible==true)
                {
                    thing.Visible = false;
                }
            }
            foreach (var picture in Controls.OfType<PictureBox>())//remove all borders from all picture boxes
            {
                picture.BorderStyle=BorderStyle.None;
                picture.Image = Properties.Resources.unlit_firecracker;
            }
            lblCurrentScore.Visible = true;//these should always be visible
            Game.CurrentScore = 0;
            lblHighScore.Visible = true;
            lblLosses.Visible = true;
            lblWins.Visible = true;
        }

        public void ShowFirecrackers(int howMany)
        {
            for (int i = 1; i <= howMany; i++)
            {
                foreach ( Control picture in this.Controls)
                {
                    if (picture.Name =="pictureBox"+i.ToString())
                    {
                        picture.Visible = true;
                    }
                }
            }
        }

        public void ChangeImage(object source, ElapsedEventArgs e)
        {
            foreach (var cracker in Game.FirecrackersArray)
            {
                var ID = cracker.ChangeFirecrackerState();
                foreach (var picture in this.Controls.OfType<PictureBox>())
                {
                    if (picture.Visible && picture.Name == "pictureBox" + (ID+1).ToString())//names do not start at 0, hence the +1
                    {
                        switch (Game.FirecrackersArray[ID].ElapsedEventCounter)
                        {
                            case 1:
                                picture.Image=Properties.Resources.lit_firecracker;
                                break;
                            case 2:
                                picture.Image = Properties.Resources.exploding_firecracker;
                                break;
                            case 3:
                                Game.FirecrackersArray[ID].PlaySound();
                                picture.Image = Properties.Resources.spark;
                                if (Game.FirecrackersArray[ID].IsDangerous)
                                {
                                    if (Game.FirecrackersArray[ID].VolumeModifier==5)
                                    {
                                        Game.CurrentScore += 1;
                                    }
                                    else
                                    {
                                        Game.hasLost = true;
                                        GameOver();
                                    }
                                }
                                break;
                            default://needed, due to timer reset
                                    cbxFirecrackers.Invoke(new MethodInvoker(delegate
                                    {
                                        Game.hasWon = ID == cbxFirecrackers.SelectedIndex;
                                    }));
                                if (Game.hasWon)
                                {
                                    GameOver(true);
                                }
                                break;
                        }
                    
                    }
                }
                Thread.Sleep(3000);//due to the multithreading, just using sleep does not stop user input

            }
            UpdateLabels();//triggers per firecracker exploding
            //if (Game.FirecrackersArray[Game.FirecrackersArray.Length - 1].ElapsedEventCounter == 4 && !Game.hasLost)//if the last cracker has exploded...
            //{
            //    GameOver(true);//...you win
            //    //todo: agressively stop all threads that reach here
            //}
        }

        private void UpdateLabels(bool all = false)
        {

            LabelInvokerMethod(lblCurrentScore,Game.CurrentScore);
            if (all)
            {
                LabelInvokerMethod(lblHighScore,Game.HighScore);
                LabelInvokerMethod(lblLosses,Game.Losses);
                LabelInvokerMethod(lblWins,Game.Wins);
            }
            
        }

        void LabelInvokerMethod(Control label,int value = 0)
        {
            //string text = "";
            if (label.InvokeRequired)
            {
                label.Invoke(new MethodInvoker(delegate
                {
                    label.Text = label.Text.Substring(0, label.Text.Length - (value > 10 ? 2 : 1))+value.ToString();
                }));
                return;
            }
            MessageBox.Show("This code should be unreachable/nLine 146 Form1");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            GC.Collect();//apparently this is a terrible idea, but i need to get rid of the timer
            InvertControlsVisibility();
            cbxLethal.SelectedIndex = 0;
            cbxFirecrackers.SelectedIndex = 5;
            foreach (var cracker in Game.FirecrackersArray)
            {
                cracker.ElapsedEventCounter = 0;
            }
            
        }

        private void GameOver(bool won = false)
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

        public void btnGo_Click(object sender, EventArgs e)
        {
            InvertControlsVisibility();
            lblCurrentScore.Text = "Score:  0";
            lblRemainingThrows.Text = "Remaining Throws:  0";
            Game.hasLost = false;
            ShowFirecrackers(cbxFirecrackers.SelectedIndex+1);
            Timer crackerCountdown = new Timer();

            crackerCountdown.Interval = Game.FirecrackersArray[0].STATE_CHANGE_TIME;//FirecrackersArray[0] always exists so I
            //                                                                      often use it to pass generic unchanging data
            crackerCountdown.Elapsed += ChangeImage;

            crackerCountdown.Enabled = true;
            Game.LightTheFuses(cbxFirecrackers.SelectedIndex+1,cbxLethal.SelectedIndex+1);
            lblRemainingThrows.Text="Remaining Throws:  "+Game.RemainingThrows.ToString();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            btnReset.Visible = false;
            lblRemainingThrows.Visible = false;
            cbxLethal.SelectedIndex = 0;//value is 1
            cbxFirecrackers.SelectedIndex = 5;//value is 6
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void debuggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testButton1.Visible = true;
            testButton2.Visible = true;
            const int theDebugWidth = 900;
            const int theNormalWidth = 800;
            this.Width = this.Width == theDebugWidth ? theNormalWidth : theDebugWidth;
            
        }

        private void ThrowingThisOne(object sender, EventArgs e)
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
            Game.FirecrackersArray[index].VolumeModifier = 5;//sound only reduces if they had a throw
            Game.RemainingThrows -= 1;
            lblRemainingThrows.Text = lblRemainingThrows.Text.Substring(0, 18) + Game.RemainingThrows.ToString();//shouldn't this crash it?
        }

        
    }
}
