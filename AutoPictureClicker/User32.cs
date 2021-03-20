using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPictureClicker
{
    public static class User32
    {
        //https://www.cnblogs.com/blackice/p/3418414.html
        //{
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标 
        public static readonly int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        public static readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        public static readonly int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        public static readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        public static readonly int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        public static readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        public static readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        public static readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //}



        //https://blog.csdn.net/qq_15505341/article/details/79165834
        //{
        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);
        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        /// <summary>
        /// 辅助键名称。
        /// Alt, Ctrl, Shift, WindowsKey
        /// </summary>
        [Flags()]
        public enum KeyModifiers { None = 0, Alt = 1, Ctrl = 2, Shift = 4, WindowsKey = 8 }
        ///// <summary>
        ///// 注册热键
        ///// </summary>
        ///// <param name="hwnd">窗口句柄</param>
        ///// <param name="hotKey_id">热键ID</param>
        ///// <param name="keyModifiers">组合键</param>
        ///// <param name="key">热键</param>
        //public static void RegHotKey(IntPtr hwnd, int hotKeyId, KeyModifiers keyModifiers, Keys key)
        //{
        //    if (!RegisterHotKey(hwnd, hotKeyId, keyModifiers, key))
        //    {
        //        int errorCode = Marshal.GetLastWin32Error();
        //        if (errorCode == 1409)
        //        {
        //            MessageBox.Show("热键被占用 ！");
        //        }
        //        else
        //        {
        //            MessageBox.Show("注册热键失败！错误代码：" + errorCode);
        //        }
        //    }
        //}
        //}
    }
}
