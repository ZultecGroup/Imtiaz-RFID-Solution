using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZulLabel
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            timer1.Tick += new EventHandler(timer_Tick);
            timer1.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {

            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
            timer1.Stop();
        }
    }
}
