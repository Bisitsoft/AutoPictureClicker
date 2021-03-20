using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPictureClicker
{
    public partial class Form1 : Form
    {
        private Clicker clicker = new Clicker();
        private Control lastTabIndexControl = new Control();
        public void MoveFocusToAEmptyControl() { lastTabIndexControl.Focus(); }

        public Form1()
        {
            InitializeComponent();

            //初始化空控件
            lastTabIndexControl.Visible = true;
            lastTabIndexControl.TabIndex = int.MaxValue;
            this.Controls.Add(lastTabIndexControl);

            //初始化点击窗口
            clicker.Show();
            clicker.Visible = false;

            //初始化设置面板数据
            //模板路径
            //FileInfo templateFile = new FileInfo(Config.Get(Config.Name_TemplatePath));
            //if (!templateFile.Exists)
            //{
            //    Config.Set(Config.Name_TemplatePath, templateFile.FullName, true);
            //    templateFile = new FileInfo(Config.Get(Config.Name_TemplatePath));
            //}
            if (!Utility.CheckMatSource(Config.Get(Config.Name_TemplatePath)))
            {
                ShowError("当前模板图片不存在或无法打开或不支持。\n" +
                    Config.Get(Config.Name_TemplatePath));
            }
            label_TemplatePath.Text = Config.Get(Config.Name_TemplatePath);
            //阈值
            trackBar_SetThreshold.Value = byte.Parse(Config.Get(Config.Name_Threshold));
            textBox_SetThreshold.Text = byte.Parse(Config.Get(Config.Name_Threshold)).ToString();
            //计时器
            trackBar_SetTimer.Value= SetTimerPanel_TextToValue(Config.Get(Config.Name_Delay));
            textBox_SetTimer.Text = int.Parse(Config.Get(Config.Name_Delay)).ToString();
            //屏幕序号
            int screenNumber = ScreenShots.GetScreens().Length;
            for(int i = 0; i < screenNumber; i++)
            {
                comboBox_SelectScreen.Items.Add(i.ToString());
            }
            int screenIndex = int.Parse(Config.Get(Config.Name_ScreenIndex));
            if (screenIndex >= screenNumber)
            {
                Config.Set(Config.Name_ScreenIndex, Config.DefalutScreenIndex.ToString(), true);
                comboBox_SelectScreen.SelectedIndex = Config.DefalutScreenIndex;
            }
            else
            {
                comboBox_SelectScreen.SelectedIndex = screenIndex;
            }
            StartCheckScreenThread(screenNumber);

            //启动初始化线程
            Thread InitThread = new Thread((ThreadStart)(() =>
            {
                while (!this.IsHandleCreated) ;

                this.BeginInvoke((Action)(() =>
                {
                    InitHotKey();

                    this.MinimumSize = this.Size;
                    this.MaximumSize = this.Size;

                    if (Program.ProgramArguments.StartThreadDirectly)
                    {
                        StartClickThread();
                    }
                }));
            }));
            InitThread.IsBackground = true;
            InitThread.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AbortClickThread(false);
            AbortCheckScreenThread();

            HotKeyManager.Clear();

            Program.Clean();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!(Program.ProgramArguments.SkipStartupInfo || Program.ProgramArguments.StartThreadDirectly))
            {
                ShowMessage(
                    "欢迎使用Picture Clicker，这是一款能够自动点击指定图像的小工具。\n" +
                    //"Auth 作者: Orange233\n" +
                    "Copyright © 2020 Bisitsoft\n" +
                    "Official website 官方网站: https://www.ourorangenet.com/project/software/PictureClicker/\n" +
                    "Feedback here 反馈地址: https://github.com/Orange23333/AutoPictureClicker/issues\n" +
                    "Version 版本: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    "About 关于", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1
                );
            }

            MoveFocusToAEmptyControl();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
           MoveFocusToAEmptyControl();
        }
    }
}
