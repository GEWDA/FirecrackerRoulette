using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirecrackerRoulette
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        ////public string LethalValue => cbxLethal.SelectedText;              //this is what I would do, and it would have worked
        //public string GetLethalValue() { return cbxLethal.SelectedText; }   //this is what I have to do instead
        //public string GetFirecrackerValue() { return cbxFirecrackers.SelectedText; }//except this wouldn't work without completely restructuring my code, because controls can't be static

        private void testButtons_Click(object sender, EventArgs e)//manual debugging
        {
            //testing sound
            var testButtons = sender as Button;
            var testFirecracker = new Roulette.Firecracker(){IsDangerous = testButtons.Name == "testButton1"}; //todo: remove test line before final version
            testFirecracker.PlaySound(testFirecracker.IsDangerous);
        }

        private void InvertControlsVisibility()
        {
            foreach (Control thing in this.Controls)
            {
                thing.Visible = thing.Visible != true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            InvertControlsVisibility();
            cbxLethal.SelectedIndex = 0;
            cbxFirecrackers.SelectedIndex = 5;
        }

        public void btnGo_Click(object sender, EventArgs e)
        {
            InvertControlsVisibility();
            Roulette Game = new Roulette();
            Game.LightTheFuses(cbxFirecrackers.SelectedIndex+1,cbxLethal.SelectedIndex+1);
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            btnReset.Visible = false;
            btnSpeedUp.Visible = false;
            btnAvoid.Visible = false;
            cbxLethal.SelectedIndex = 0;
            cbxFirecrackers.SelectedIndex = 5;
        }
    }
}
