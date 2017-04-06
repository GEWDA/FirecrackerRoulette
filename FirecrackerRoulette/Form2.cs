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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//this is a pretty simple form
        {
            Second();//it literally displays a message, opens another form, and closes itself
        }

        public Form1 myForm = new Form1();

        public void Second()
        {
            this.Hide();//hides the form without actually closing it
            myForm.ShowDialog();//opens the other form
            this.Close();//note the form can only be closed AFTER the other form is opened

        }
    }
}
