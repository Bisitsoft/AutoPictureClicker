using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPictureClicker
{
    public class ScreenShots
    {
        private Bitmap lastScreenShot = null;
        public Bitmap LastScreenShot { get { return lastScreenShot; } }

        //http://www.voidcn.com/article/p-ycvuxmrq-byd.html -> https://stackoverflow.com/questions/10233055/how-to-get-screenshot-to-include-the-invoking-window-on-xp/10234693
        public Bitmap ScreenShotDefault(Screen screen)
        {
            Size sz = screen.Bounds.Size;
            IntPtr hDesk = GetDesktopWindow();
            IntPtr hSrce = GetWindowDC(hDesk);
            IntPtr hDest = CreateCompatibleDC(hSrce);
            IntPtr hBmp = CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
            IntPtr hOldBmp = SelectObject(hDest, hBmp);
            bool b = BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, screen.Bounds.X, screen.Bounds.Y, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
            Bitmap bmp = Bitmap.FromHbitmap(hBmp);
            SelectObject(hDest, hOldBmp);
            DeleteObject(hBmp);
            DeleteDC(hDest);
            ReleaseDC(hDesk, hSrce);

            lastScreenShot?.Dispose();
            lastScreenShot = bmp;
            return bmp;
        }
        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int
        wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);
        [DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("gdi32.dll")]
        static extern IntPtr DeleteDC(IntPtr hDc);
        [DllImport("gdi32.dll")]
        static extern IntPtr DeleteObject(IntPtr hDc);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr ptr);

        ////https://www.cnblogs.com/yufun/archive/2009/01/20/1378812.html
        ////Uncompleted cannot use
        //public Bitmap ScreenShotUsingPrintScreenKey(Screen screen)
        //{
        //    const uint KEYEVENTF_EXTENDEDKEY = 0x1;
        //    const uint KEYEVENTF_KEYUP = 0x2;
        //    const byte VK_SNAPSHOT = 0x2C;
        //
        //    IDataObject pri_ido = GetClipboardFromMainThread();
        //    byte[] pri = null;
        //    if (pri_ido != null)
        //    {
        //        pri = Utility.Serialize(GetClipboardFromMainThread());
        //    }
        //    void RestoreClipboard()
        //    {
        //        if (pri != null)
        //        {
        //            SetClipboardToMainThread((IDataObject)Utility.Deserialize(pri));
        //        }
        //        else
        //        {
        //            SetClipboardToMainThread(new DataObject((object)""));
        //        }
        //    }
        //    keybd_event(VK_SNAPSHOT, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
        //    keybd_event(VK_SNAPSHOT, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        //    IDataObject data;
        //    DateTime begin = DateTime.Now;
        //    TimeSpan timeout = new TimeSpan(0, 0, 1);
        //    while (true)
        //    {
        //        data = GetClipboardFromMainThread();
        //        if(data.GetDataPresent(DataFormats.Bitmap, true))
        //        {
        //            break;
        //        }
        //        if (DateTime.Now - begin > timeout)
        //        {
        //            RestoreClipboard();
        //            throw new TimeoutException("Get screenshot from clipboard timeout.");
        //        }
        //    }
        //
        //    Bitmap bmpScreen = (Bitmap)data.GetData(DataFormats.Bitmap, true);
        //    Bitmap bmpOutput = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
        //    Graphics g = Graphics.FromImage(bmpOutput);
        //    Rectangle destRectangle = new Rectangle(0, 0, screen.Bounds.Width, screen.Bounds.Height);
        //    g.DrawImage(bmpScreen, destRectangle, screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height, GraphicsUnit.Pixel);
        //
        //    RestoreClipboard();
        //
        //    lastScreenShot?.Dispose();
        //    lastScreenShot = bmpOutput;
        //    return bmpOutput;
        //}
        ////https://zhidao.baidu.com/question/88918730.html
        //[DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        //private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        //
        //private static IDataObject GetClipboardFromMainThread()
        //{
        //    return (IDataObject)Program.Invoke((Func<object>)(() =>
        //    {
        //        return Clipboard.GetDataObject();
        //    }));
        //}
        //
        //private static void SetClipboardToMainThread(IDataObject data)
        //{
        //    Program.Invoke((Action)(() =>
        //    {
        //        Clipboard.SetDataObject(data);
        //    }));
        //}
        //
        //public static Bitmap ScreenShotUsingDirectX(Screen screen)
        //{
        //    throw new NotImplementedException("Unrealized.");
        //}

        public void Clear()
        {
            lastScreenShot?.Dispose();
            lastScreenShot = null;
        }

        public static Screen[] GetScreens()
        {
            return Screen.AllScreens;
        }

        //Old, 在截图的时候存在bug
        ////?
        //public Bitmap ScreenShotUsingGraphics(Screen screen)
        //{
        //    Bitmap image = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
        //    using (Graphics g = Graphics.FromImage(image))
        //    {
        //        g.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, image.Size);
        //        g.Dispose();
        //    }
        //
        //    lastScreenShot?.Dispose();
        //    lastScreenShot = image;
        //
        //    return image;
        //}
        //
        ////https://blog.csdn.net/bruce135lee/article/details/81565702
        //[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        //private static extern IntPtr CreateDC(
        //    string lpszDriver,//驱动名称
        //    string lpszDevice,//设备名称
        //    string lpszOutput,//无用，设为null
        //    IntPtr lpInitData//任意的打印机数据
        //    );
        ////https://blog.csdn.net/qq_36439293/article/details/80482172
        //[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        //private static extern bool BitBlt(
        //    IntPtr hdcDest, // 目标 DC的句柄 
        //    int nXDest,
        //    int nYDest,
        //    int nWidth,
        //    int nHeight,
        //    IntPtr hdcSrc, // 源DC的句柄 
        //    int nXSrc,
        //    int nYSrc,
        //    System.Int32 dwRop // 光栅的处理数值 
        //);
        ////dwRop详细{
        ////  https://blog.csdn.net/weixin_33974433/article/details/93277696
        ////}
        ////https://blog.csdn.net/bruce135lee/article/details/81565702
        //public Bitmap ScreenShotUsingDC(Screen screen)
        //{
        //    //创建显示器的DC
        //    IntPtr dc = CreateDC(screen.DeviceName, null, null, (IntPtr)null);
        //    //由一个指定设备的句柄创建一个新的Graphics对象
        //    Graphics g_scr = Graphics.FromHdc(dc);
        //    //根据屏幕大小创建一个与之相同大小的Bitmap对象
        //    Image image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, g_scr);
        //    Graphics g_img = Graphics.FromImage(image);
        //
        //
        //    //获得屏幕的句柄
        //    IntPtr dc_scr = g_scr.GetHdc();
        //    //获得位图的句柄
        //    IntPtr dc_bmp = g_img.GetHdc();
        //
        //    const int SRCCOPY = 0xCC0020;
        //    BitBlt(dc_bmp, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, dc_scr, 0, 0, SRCCOPY);
        //    //释放屏幕句柄
        //    g_scr.ReleaseHdc(dc_scr);
        //    //释放位图句柄
        //    g_img.ReleaseHdc(dc_bmp);
        //
        //    Bitmap bitmap = new Bitmap(image);
        //    lastScreenShot?.Dispose();
        //    lastScreenShot = bitmap;
        //    return bitmap;
        //}
    }
}
