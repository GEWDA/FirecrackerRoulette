using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                                        lblCurrentScore.Text = lblCurrentScore.Text.Substring(0, 8) + Game.CurrentScore;
                                    }
                                    else
                                    {
                                        Lose();
                                    }
                                }
                                break;
                            default:
                                try { picture.Visible = false; } //once ElapsedEventCounter reaches 4 (i.e. it has exploded), stop loading it
                                catch { }//todo: remove the underlying problem (messy multi-threading)
                                break;
                        }
                    
                    }
                }
                //todo: add some non-click-stopping 2 second delay here
            }



        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            InvertControlsVisibility();
            cbxLethal.SelectedIndex = 0;
            cbxFirecrackers.SelectedIndex = 5;
        }

        private void Lose()
        {
            Game.HighScore = Game.CurrentScore > Game.HighScore ? Game.CurrentScore : Game.HighScore;
            MessageBox.Show("You have perished");
            btnReset_Click(this,new EventArgs());
        }

        public void btnGo_Click(object sender, EventArgs e)
        {
            InvertControlsVisibility();
            lblCurrentScore.Text = "Score:  0";
            ShowFirecrackers(cbxFirecrackers.SelectedIndex+1);
            Timer crackerCountdown = new Timer();

            crackerCountdown.Interval = 5000;

            crackerCountdown.Elapsed += ChangeImage;

            crackerCountdown.Enabled = true;
            Game.LightTheFuses(cbxFirecrackers.SelectedIndex+1,cbxLethal.SelectedIndex+1);
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
            if (Game.FirecrackersArray[index].IsDangerous)
            {
                Game.CurrentScore += 1;
            }
            Game.FirecrackersArray[index].VolumeModifier = 5;//sound only reduces if they had a throw
            Game.RemainingThrows -= 1;
        }

        
    }
}
