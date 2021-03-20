using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenCvSharp;

namespace AutoPictureClicker
{
    public partial class Form1
    {
        private Thread clickThread = null;
        public bool clickThread_IsActive { get { return clickThread != null && clickThread.IsAlive; } }

        public struct ClickThreadArguments
        {
            public Screen screen;
            public string templatePath;
            public byte threshold;
            public int delay;
            public ClickThreadInfoSave clickThreadInfoSave;
        }
        public struct ClickThreadInfoSave
        {
            public TimeSpan timer;
            public TimeSpan totalRunningTime;
            public System.Drawing.Point lastLocation;
            public byte lastValue;
            public int clickCount;
            public int scanedCount;
        }

        #region ===ClickThread===
        private ClickThreadArguments clickThreadArguments = new ClickThreadArguments();
        private ClickThreadInfoSave? lastClickThreadInfoSave = null;
        private void ClickThread(object args)
        {
            //初始化
            ClickThreadArguments cta = new ClickThreadArguments();
            ClickThreadInfoSave ctis = new ClickThreadInfoSave();
            void UpdateAndGetClickThreadArguments()
            {
                cta = (ClickThreadArguments)this.Invoke((Func<object>)(() =>
                {
                    clickThreadArguments.screen = Screen.AllScreens[int.Parse(Config.Get(Config.Name_ScreenIndex))];
                    clickThreadArguments.templatePath = Config.Get(Config.Name_TemplatePath);
                    clickThreadArguments.threshold = byte.Parse(Config.Get(Config.Name_Threshold));
                    clickThreadArguments.delay = int.Parse(Config.Get(Config.Name_Delay));
                    return this.clickThreadArguments;
                }));
            }
            DateTime begin = new DateTime(0);
            DateTime timer = new DateTime(0);
            void WaitTimer()
            {
                TimeSpan subResult;
                TimeSpan delayTime;
                while (true)
                {
                    //更新配置
                    UpdateAndGetClickThreadArguments();
                    delayTime = new TimeSpan(0, 0, 0, 0, cta.delay);
                    //计算时差
                    subResult = DateTime.Now.Subtract(timer);
                    StatusStrip_Set_toolStripStatusLabel_TotalRunningTime(DateTime.Now.Subtract(begin));
                    if (subResult < delayTime)
                    {
                        StatusStrip_Set_toolStripProgressBar_Timer_Value((int)(100 * (subResult.TotalMilliseconds / delayTime.TotalMilliseconds)));//UI
                        StatusStrip_Set_toolStripLabel_Timer_Value((int)subResult.TotalMilliseconds, cta.delay);//UI
                    }
                    else
                    {
                        StatusStrip_Set_toolStripProgressBar_Timer_Value(100);
                        StatusStrip_Set_toolStripLabel_Timer_Value((int)delayTime.TotalMilliseconds, cta.delay);//UI
                        break;
                    }
                    //防卡停顿
                    Thread.Sleep(100);
                }
            }
            ScreenShots screenShots = new ScreenShots();
            Mat screenShot = new Mat();
            Mat template = new Mat();
            Mat result = new Mat();
            //恢复线程状态
            if (args != null)
            {
                ctis = (ClickThreadInfoSave)args;
                begin = DateTime.Now.Subtract(ctis.totalRunningTime);
                timer = DateTime.Now.Subtract(ctis.timer);
            }
            else
            {
                ctis.timer = new TimeSpan(0);
                ctis.totalRunningTime = new TimeSpan(0);
                ctis.lastLocation = new System.Drawing.Point(0, 0);
                ctis.lastValue = 0;
                ctis.clickCount = 0;
                ctis.scanedCount = 0;
                begin = DateTime.Now;
            }

            try
            {
                if (args != null)
                {
                    WaitTimer();
                }
                while (true)
                {
                    //获取配置
                    UpdateAndGetClickThreadArguments();

                    //开始计时
                    timer = DateTime.Now;
                    StatusStrip_Set_toolStripProgressBar_Timer_Value(0);//UI
                    StatusStrip_Set_toolStripLabel_Timer_Value(0, cta.delay);//UI

                    //获取并保存截屏
                    screenShots.ScreenShotDefault(cta.screen);
                    screenShots.LastScreenShot.Save(Config.ScreenShotPath);
                    //初始化图片
                    screenShot = new Mat(Config.ScreenShotPath);
                    template = new Mat(Config.Get(Config.Name_TemplatePath));
                    result = new Mat();//Mat(new OpenCvSharp.Size(screenShots.LastScreenShot.Width, screenShots.LastScreenShot.Height), MatType.CV_32SC1)

                    //比对
                    Cv2.MatchTemplate(InputArray.Create(screenShot), InputArray.Create(template), OutputArray.Create(result), TemplateMatchModes.CCoeffNormed, null);
                    //查找最佳匹配
                    OpenCvSharp.Point maxLoc = new OpenCvSharp.Point(0, 0);
                    double maxVal = 0;
                    Cv2.MinMaxLoc(InputArray.Create(result), out _, out maxVal, out _, out maxLoc);
                    //记录结果值
                    ctis.lastValue = (byte)(maxVal * 255);
                    StatusStrip_Set_toolStripLabel_LastValue_Value(ctis.lastValue);//UI
                    //记录扫描
                    ctis.scanedCount++;
                    StatusStrip_Set_toolStripStatusLabel_ScanedCount(ctis.scanedCount);//UI
                    //检查阀值
                    if (maxVal >= cta.threshold / (double)255)
                    {
                        //计算实际中心位置
                        int realX = cta.screen.WorkingArea.X + maxLoc.X + template.Width / 2;
                        int realY = cta.screen.WorkingArea.Y + maxLoc.Y + template.Height / 2;

                        //模拟点击
                        SimClick(realX, realY);
                        StatusStrip_Set_toolStripLabel_LastLocation_Value(realX, realY);//UI

                        //记录点击
                        ctis.lastLocation = new System.Drawing.Point(realX, realY);
                        ctis.clickCount++;
                        StatusStrip_Set_toolStripStatusLabel_ClickCount(ctis.clickCount);//UI
                    }

                    //清理内存
                    GC.Collect();
                    //延时
                    WaitTimer();
                }
            }
            catch (ThreadAbortException taEx)
            {
                //处理线程终止
                //清理
                result.Dispose();
                template.Dispose();
                screenShot.Dispose();
                screenShots.Clear();

                if (taEx.ExceptionState != null && (string)taEx.ExceptionState == "pause")
                {
                    if (timer != new DateTime(0))
                    {
                        ctis.timer = DateTime.Now.Subtract(timer);
                    }
                    if (begin != new DateTime(0))
                    {
                        ctis.totalRunningTime = DateTime.Now.Subtract(begin);
                    }

                    this.BeginInvoke((Action)(() =>
                    {
                        lastClickThreadInfoSave = ctis;
                    }));
                }
                //else
                //{
                //    //清除状态栏信息
                //    StatusStrip_Set_toolStripProgressBar_Timer_Value(0);//UI
                //    StatusStrip_Set_toolStripLabel_Timer_Value(0, cta.delay);//UI
                //}

                //阻塞UI，不可使用，请在调用终止代码后方添加状态调整
                //this.BeginInvoke((Action)(() =>
                //{
                //    SetThreadStatus(false);//UI
                //}));
            }
        }

        private void SimClick(int x, int y)
        {
            Cursor.Position = new System.Drawing.Point(x, y);
            clicker.Invoke((Action)(() =>
            {
                clicker.Hide();
            }));
            User32.mouse_event(User32.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            User32.mouse_event(User32.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            clicker.BeginInvoke((Action)(() =>
            {
                clicker.MoveAndShow(x, y);
            }));
        }
        #endregion

        #region ===ControlClickThread===
        private void button_Boot_Click(object sender, EventArgs e)
        {
            StartClickThread();
        }
        private void StartClickThread(bool checkInvalidOperation = true)
        {
            if (clickThread == null)
            {
                clickThread = new Thread(new ParameterizedThreadStart(ClickThread));
                clickThread.IsBackground = true;

                //int screenIndex = int.Parse(Config.Get(Config.Name_ScreenIndex));
                //try
                //{
                //    clickThreadArguments.screen = Screen.AllScreens[int.Parse(Config.Get(Config.Name_ScreenIndex))];
                //}
                //catch (IndexOutOfRangeException)
                //{
                //    ShowError("找不到序号为" + screenIndex.ToString() + "的屏幕");
                //    return;
                //}

                clickThread.Start(null);
                SetThreadStatus(true);
            }
            else
            {
                if (checkInvalidOperation)
                {
                    ShowMessage("线程正在运行。"
                        , "Information 信息", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void button_Abort_Click(object sender, EventArgs e)
        {
            AbortClickThread();
        }
        private void AbortClickThread()
        {
            AbortClickThread(true);
        }
        private void AbortClickThread(bool checkInvalidOperation = true)
        {
            if (clickThread != null)
            {
                clickThread.Abort();
                while (clickThread.IsAlive) ;
                clickThread = null;
                SetThreadStatus(false);

                StatusStrip_Set_toolStripProgressBar_Timer_Value(0);
                StatusStrip_Set_toolStripLabel_Timer_Value(0, int.Parse(Config.Get(Config.Name_Delay)));
            }
            else
            {
                if (checkInvalidOperation)
                {
                    MessageBox.Show("线程已经终止。"
                    , "Information 信息", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void PauseClickThread()
        {
            if (clickThread != null)
            {
                clickThread.Abort("pause");
                while (clickThread.IsAlive) ;
                clickThread = null;
                SetThreadStatus(false);
            }
            else
            {
                throw new InvalidOperationException("The thread is not running.");
            }
        }
        private void ResumeClickThread()
        {
            if (clickThread == null)
            {
                if (lastClickThreadInfoSave == null)
                {
                    throw new InvalidOperationException("No paused thread.");
                }
                clickThread = new Thread(new ParameterizedThreadStart(ClickThread));
                clickThread.IsBackground = true;

                clickThread.Start(lastClickThreadInfoSave);
                SetThreadStatus(true);
            }
            else
            {
                throw new InvalidOperationException("The thread has started.");
            }
        }
        #endregion

        #region ===ClickThreadStstus====
        private bool threadStatus = false;
        /// <summary>
        /// 设置线程状态。（只是记录，不对线程操作）
        /// </summary>
        /// <param name="status">true: 运行; false: 终止。</param>
        private void SetThreadStatus(bool status)
        {
            threadStatus = status;
            radioButton_ThreadStatus.Checked = status;
        }
        private void radioButton_ThreadStatus_Click(object sender, EventArgs e)
        {
            radioButton_ThreadStatus.Checked = threadStatus;
        }
        #endregion
    }
}
