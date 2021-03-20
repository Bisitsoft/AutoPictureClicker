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
        private void trackBar_SetTimer_ValueChanged(object sender, EventArgs e)
        {
            if (textBox_SetTimer_ChangedFlag)
            {
                textBox_SetTimer_ChangedFlag = false;
                return;
            }

            if (trackBar_SetTimer.Value > 142)
            {
                trackBar_SetTimer.Value = 142;
            }
            string text = SetTimerPanel_ValueToText(trackBar_SetTimer.Value);
            Config.Set(Config.Name_Delay, text.ToString(), true);
            textBox_SetTimer.Text = text.ToString();
        }

        private void textBox_SetTimer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                MoveFocusToAEmptyControl();
            }
            e.Handled = true;
        }

        private string textBox_SetTimer_Last = null;
        private void textBox_SetTimer_Enter(object sender, EventArgs e)
        {
            textBox_SetTimer_Last = textBox_SetTimer.Text;
        }

        private bool textBox_SetTimer_ChangedFlag = false;
        private void textBox_SetTimer_Leave(object sender, EventArgs e)
        {
            int value;
            try
            {
                value = SetTimerPanel_TextToValue(textBox_SetTimer.Text);
            }
            catch (FormatException)
            {
                textBox_SetTimer.Text = textBox_SetTimer_Last;
                return;
            }
            
            Config.Set(Config.Name_Delay, int.Parse(textBox_SetTimer.Text).ToString(), true);
            textBox_SetTimer_ChangedFlag = true;
            trackBar_SetTimer.Value = value;
        }

        public string SetTimerPanel_ValueToText(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("It must be >= 0.", "value");
            }
            if (value > 143)
            {
                throw new ArgumentException("It must be <= 143.", "value");
            }

            if (0 <= value && value <= 60)
            {
                return (value * 1000).ToString();
            }
            else if (61 <= value && value <= 119)
            {
                return ((value - 59) * 60 * 1000).ToString();
            }
            else if (120 <= value && value <= 142)
            {
                return ((value - 118) * 3600 * 1000).ToString();
            }
            else
            {
                throw new Exception("SetTimerPanel_ValueToText: Unknown error.");
            }
        }
        public int SetTimerPanel_TextToValue(string text)
        {
            int value = int.Parse(text);
            if (value < 0)
            {
                throw new ArgumentException("It must be >= 0.", "value");
            }

            if (value <= 60 * 1000)
            {
                return (int)Math.Ceiling(value / 1000.0);
            }
            else if (value <= (119 - 59) * 60 * 1000)
            {
                return (int)Math.Ceiling(value / 60.0 / 1000.0 + 59);
            }
            else if (value <= (142 - 118) * 3600 * 1000)
            {
                return (int)Math.Ceiling(value /3600.0 / 1000.0 + 118);
            }
            else
            {
                return 143;
            }
        }
    }
}
