using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoPictureClicker
{
    public partial class Form1
    {
        private Thread checkScreenThread = null;

        private void comboBox_SelectScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.Set(Config.Name_ScreenIndex, comboBox_SelectScreen.SelectedIndex.ToString(), true);
        }

        private void StartCheckScreenThread(int nowScreenNumber)
        {
            if(checkScreenThread == null || (!checkScreenThread.IsAlive))
            {
                checkScreenThread = new Thread((ParameterizedThreadStart)CheckScreenThread);
                checkScreenThread.IsBackground = true;
                checkScreenThread.Start(nowScreenNumber);
            }
            
        }

        private void AbortCheckScreenThread()
        {
            if (checkScreenThread != null && checkScreenThread.IsAlive)
            {
                checkScreenThread.Abort();
                while (checkScreenThread.IsAlive) ;
                checkScreenThread = null;
            }
        }

        private void CheckScreenThread(object args)
        {
            int n, last_n, cb_n;
            last_n = (int)args;

            while (true)
            {
                n = ScreenShots.GetScreens().Length;
                if (n != last_n)
                {
                    last_n = n;
                    cb_n = (int)this.Invoke((Func<object>)(() => {
                        return comboBox_SelectScreen.Items.Count;
                    }));
                    if (n > cb_n)
                    {
                        this.Invoke((Action)(() => {
                            for(int i= comboBox_SelectScreen.Items.Count; i < n; i++)
                            {
                                comboBox_SelectScreen.Items.Add(i.ToString());
                            }
                        }));
                    }
                    else if(n < cb_n)
                    {
                        this.Invoke((Action)(() => {
                            int temp = -1;
                            bool errFlag = false;
                            bool stoppedThread = false;
                            if (int.Parse(Config.Get(Config.Name_ScreenIndex)) >= n)
                            {
                                Config.Set(Config.Name_ScreenIndex, Config.DefalutScreenIndex.ToString(), true);
                                temp = comboBox_SelectScreen.SelectedIndex;
                                comboBox_SelectScreen.SelectedIndex = Config.DefalutScreenIndex;
                                if(clickThread != null && clickThread.IsAlive)
                                {
                                    AbortClickThread();
                                    stoppedThread = true;
                                }
                                errFlag = true;
                            }
                            for (int i = comboBox_SelectScreen.Items.Count - 1; i >= n; i--)
                            {
                                comboBox_SelectScreen.Items.Remove(i.ToString());
                            }
                            if (errFlag)
                            {
                                ShowError("已丢失序号为" + temp.ToString() + "的屏幕。" +
                                    (stoppedThread ? "线程已经被终止。" : ""));
                            }
                        }));
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
