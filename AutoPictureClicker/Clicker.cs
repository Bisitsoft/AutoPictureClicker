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

namespace AutoPictureClicker
{
    public partial class Clicker : Form
    {
        public Clicker()
        {
            InitializeComponent();
            this.Visible = false;
        }

        Thread timer = null;
        public void MoveAndShow(int x, int y)
        {
            if (timer != null)
            {
                timer.Abort();
                while (timer.IsAlive) ;
            }
            timer = new Thread((ThreadStart)(() =>
            {
                Thread.Sleep(500);
                this.Invoke((Action)(() =>
                {
                    this.Visible = false;
                    timer = null;
                }));
            }));
            timer.IsBackground = true;
            
            this.Enabled = true;
            this.SetDesktopLocation(x, y);
            this.Enabled = false;
            this.Visible = true;
            this.TopMost = true;

            timer.Start();
        }
    }
}
