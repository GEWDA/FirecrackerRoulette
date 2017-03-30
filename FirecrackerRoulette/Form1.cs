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

        private void testButtons_Click(object sender, EventArgs e)
        {
            var testButtons = sender as Button;
            var testFirecracker = new Roulette.Firecracker(){IsDangerous = testButtons.Name == "testButton1"}; //todo: remove test line before final version
            testFirecracker.PlaySound(testFirecracker.IsDangerous);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnGo.Visible = true;
            cbxLethal.Visible = true;
            cbxFirecrackers.Visible = true;
            btnReset.Visible = false;
            cbxLethal.SelectedValue = "1";
            cbxFirecrackers.SelectedValue = "6";
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGo.Visible = false;
            cbxLethal.Visible = false;
            cbxFirecrackers.Visible = false;
            btnReset.Visible = true;
        }
    }
}
