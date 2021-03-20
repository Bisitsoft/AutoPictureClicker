using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPictureClicker
{
    public partial class Form1
    {
        private void trackBar_SetThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (textBox_SetThreshold_ChangedFlag)
            {
                textBox_SetThreshold_ChangedFlag = false;
                return;
            }

            string text = trackBar_SetThreshold.Value.ToString();
            Config.Set(Config.Name_Threshold, text, true);
            textBox_SetThreshold.Text = text;
        }

        private void textBox_SetThreshold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                MoveFocusToAEmptyControl();
            }
            e.Handled = true;
        }

        private string textBox_SetThreshold_Last = null;
        private void textBox_SetThreshold_Enter(object sender, EventArgs e)
        {
            textBox_SetThreshold_Last = textBox_SetThreshold.Text;
        }

        private bool textBox_SetThreshold_ChangedFlag = false;
        private void textBox_SetThreshold_Leave(object sender, EventArgs e)
        {
            int value;
            try
            {
                value = byte.Parse(textBox_SetThreshold.Text);
            }
            catch (FormatException)
            {
                textBox_SetThreshold.Text = textBox_SetThreshold_Last;
                return;
            }

            Config.Set(Config.Name_Threshold, value.ToString(), true);
            textBox_SetThreshold_ChangedFlag = true;
            trackBar_SetThreshold.Value = value;
        }
    }
}
