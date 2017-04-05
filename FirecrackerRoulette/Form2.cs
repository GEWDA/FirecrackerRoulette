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

        private void button1_Click(object sender, EventArgs e)
        {
            Second();
        }

        public Form1 myForm = new Form1();

        public void Second()
        {
            this.Hide();
            myForm.ShowDialog();
            this.Close();

        }
    }
}
