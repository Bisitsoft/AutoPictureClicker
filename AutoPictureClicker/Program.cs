using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPictureClicker
{
    public static class Program
    {
        private static Form1 mainWindow;
        #region ===MainWindow Invoke===
        public static IAsyncResult BeginInvoke(Delegate method)
        {
            return mainWindow.BeginInvoke(method);
        }
        public static IAsyncResult BeginInvoke(Delegate method, params object[] args)
        {
            return mainWindow.BeginInvoke(method, args);
        }
        public static object EndInvoke(IAsyncResult asyncResult)
        {
            return mainWindow.EndInvoke(asyncResult);
        }
        public static object Invoke(Delegate method)
        {
            return mainWindow.Invoke(method);
        }
        public static object Invoke(Delegate method, params object[] args)
        {
            return mainWindow.Invoke(method, args);
        }
        public static bool InvokeRequired
        {
            get { return mainWindow.InvokeRequired; }
        }
        #endregion

        //public static readonly bool IsDebug = true;
        public static ProgramArguments ProgramArguments = null;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>

        [STAThread]
        static void Main(string[] args)
        {
            Config.Init();

            if (ArgReader(args, out ProgramArguments))
            {
                ;
            }
            else
            {

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainWindow = new Form1();
            Application.Run(mainWindow);

            #region 申请管理权限（暂时不用）
            ////https://www.cnblogs.com/mq0036/p/11647724.html
            ///**
            // * 当前用户是管理员的时候，直接启动应用程序
            // * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
            // */
            ////获得当前登录的Windows用户标示
            //System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            ////判断当前登录用户是否为管理员
            //if (IsDebug || principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            //{
            //    //如果是管理员，则直接运行
            //    Application.Run(mainWindow);
            //}
            //else
            //{
            //    MessageBox.Show("本程序需要用到WinIO进行输入输出，需要给予管理员权限来使用，谢谢！",
            //            "PictureClicker: 请求管理员权限", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //
            //    //创建启动对象
            //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //    startInfo.UseShellExecute = true;
            //    startInfo.WorkingDirectory = Environment.CurrentDirectory;
            //    startInfo.FileName = Application.ExecutablePath;
            //    //设置启动动作,确保以管理员身份运行
            //    startInfo.Verb = "runas";
            //    try
            //    {
            //        System.Diagnostics.Process.Start(startInfo);
            //    }
            //    catch
            //    {
            //        MessageBox.Show(
            //            //"启动失败，请确保已赋予管理员权限，或检查应用程序文件是否还存在（可能被杀毒软件清除或阻止）。若无，请将文件夹下的dump.log递交给开发者，谢谢！",
            //            "启动失败，请确保已赋予管理员权限。若已赋予仍然启动失败，请将文件夹下的crash.report递交给开发者，谢谢！",
            //            "PictureClicker: 启动失败", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //        Environment.Exit(-1);
            //        return;
            //    }
            //    //退出
            //    Environment.Exit(0);
            //}
            #endregion
        }

        private static bool ArgReader(string[] args, out ProgramArguments programArguments)
        {
            programArguments = new ProgramArguments();

            //--reset-config 恢复出厂设置
            //--no-delete-temp 不要删除缓存文件
            //--save-result 保存结果
            //--save-result <config> 有计划地保存结果
            //-d --debug 调试模式

            if (args.Length > 0)
            {
                var HelpMode=from val in args
                               where val=="--help" || val=="-h"
                               select val;
                if (HelpMode.Count() > 0)
                {
                    Console.WriteLine("Sorry, no console help now. Please check help.txt in the program root path.");
                    Console.WriteLine("If you don't have help.txt, Get full program on https://github.com/Orange23333/AutoPictureClicker/issues .");
                    Environment.Exit(0);
                }

                var CleanMode = from val in args
                                where val == "--clean"
                                select val;
                if (CleanMode.Count() > 0)
                {
                    Clean();
                    Environment.Exit(0);
                }

                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        case "--skip-startup-info":
                            programArguments.SkipStartupInfo = true;
                            break;
                        case "--start-thread-directly":
                            //programArguments.SkipStartupInfo = true;
                            programArguments.StartThreadDirectly = true;
                            break;
                        case "--output-working-data":

                            break;
                        case "--clean":

                            break;
                        case "-h":
                        case "--help":
                        default:
                            break;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Clean()
        {
            FileInfo screenShotFile = new FileInfo(Config.ScreenShotPath);
            if (screenShotFile.Exists)
            {
                screenShotFile.Delete();
            }
            FileInfo resultFile = new FileInfo(Config.ResultPath);
            if (resultFile.Exists)
            {
                resultFile.Delete();
            }
        }
    }
}
