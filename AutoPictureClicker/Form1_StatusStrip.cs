using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPictureClicker
{
    public partial class Form1
    {
        public void StatusStrip_Set_toolStripProgressBar_Timer_Value(int value)
        {
            this.BeginInvoke((Action)(() =>
            {
                this.toolStripProgressBar_Timer.Value = value;
            }));
        }
        public void StatusStrip_Set_toolStripLabel_Timer_Value(int value, int max)
        {
            this.BeginInvoke((Action)(() =>
            {
                this.toolStripStatusLabel_Timer.Text = String.Format("{0}/{1} ms", value, max);
            }));
        }
        public void StatusStrip_Set_toolStripStatusLabel_TotalRunningTime(TimeSpan timer)
        {
            int temp = (int)Math.Floor(timer.Subtract(new TimeSpan(0, timer.Minutes, 0)).Subtract(new TimeSpan(0, 0, timer.Seconds)).TotalHours);
            this.BeginInvoke((Action)(() =>
            {
                this.toolStripStatusLabel_TotalRunningTime.Text = String.Format("{0}:{1}:{2}", temp, timer.Minutes, timer.Seconds);
            }));
        }
        public void StatusStrip_Set_toolStripLabel_LastLocation_Value(int x, int y)
        {
            this.BeginInvoke((Action)(() =>
            {
                this.toolStripStatusLabel_LastLocation.Text = String.Format("x = {0}, y = {1}", x, y);
            }));
        }
        public void StatusStrip_Set_toolStripLabel_LastValue_Value(int value)
        {
            double temp = 100 * (value / 255.0);
            this.BeginInvoke((Action)(() =>
            {
                this.toolStripStatusLabel_LastValue.Text = String.Format("{0}/255 ({1}%)", value, temp);
            }));
        }
        public void StatusStrip_Set_toolStripStatusLabel_ClickCount(int value)
        {
            this.BeginInvoke((Action)(() =>
            {
                this.toolStripStatusLabel_ClickCount.Text = String.Format("Clicked {0} time(s)", value);
            }));
        }
        public void StatusStrip_Set_toolStripStatusLabel_ScanedCount(int value)
        {
            this.BeginInvoke((Action)(() =>
            {
                this.toolStripStatusLabel_ScanedCount.Text = String.Format("Scaned {0} time(s)", value);
            }));
        }
    }
}
