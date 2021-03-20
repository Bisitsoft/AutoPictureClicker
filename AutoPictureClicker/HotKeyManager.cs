using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPictureClicker
{
    /// <summary>
    /// 一个被热键管理器调用来出发热键事件的
    /// </summary>
    /// <param name="message">WndProc Message。</param>
    /// <returns>可能发生改变的message。（因为无法在这里传递ref参数，所以请将更改用return的方式传回来；传回null将定义为未改变message）</returns>
    public delegate Message HotKeyHandle(Message message);

    public sealed class HotKeyManager
    {
        private static SortedDictionary<int, HotKeyInfo> hotKeys = new SortedDictionary<int, HotKeyInfo>();
        public static List<HotKeyInfo> HotKeyInfos { get { return hotKeys.Values.ToList(); } }
        private class InnerWindowHandleBody : Control
        {
            private delegate void ObejectsMethod(params object[] args);

            protected override void WndProc(ref Message m)
            {
                const int WM_HOTKEY = 0x0312;

                switch (m.Msg)
                {
                    case WM_HOTKEY:
                        Message message = m;
                        var sameId = from val in hotKeys
                                     where val.Key == message.WParam.ToInt32()
                                     select val;
                        if (sameId.Count() == 0)
                        {
                            Program.BeginInvoke((Action)(() =>
                            {
                                throw new Exception("A unknow error from HotKeyManager.InnerWindowHandleBody.WndProc.");
                            }));
                        }
                        Message r = (Message)(sameId.Single().Value.InvokeHandleControl.Invoke((
                            Func<Message>)(() =>
                            {
                                return sameId.Single().Value.HotKeyHandle(message);
                            }
                            )));
                        if (r != null)
                        {
                            m = r;
                        }
                        break;
                }

                base.WndProc(ref m);
            }
        }
        private static InnerWindowHandleBody innerWindowHandleBody = new InnerWindowHandleBody();

        public static void Add(string name, User32.KeyModifiers keyModifier, Keys key, Control invokeHandleControl, HotKeyHandle hotKeyHandle)
        {
            HotKeyInfo hotKeyInfo = new HotKeyInfo();
            hotKeyInfo.Name = name;
            hotKeyInfo.KeyModifiers = keyModifier;
            hotKeyInfo.Key = key;
            hotKeyInfo.InvokeHandleControl = invokeHandleControl;
            hotKeyInfo.HotKeyHandle = hotKeyHandle;
            Add(hotKeyInfo);
        }
        public static void Add(HotKeyInfo hotKeyInfo)
        {
            if (hotKeyInfo.InvokeHandleControl == null)
            {
                throw new ArgumentException("The invoke handle control can't be null.", "hotKeyInfo");
            }
            if (hotKeyInfo.HotKeyHandle == null)
            {
                throw new ArgumentException("The hot key handle can't be null.", "hotKeyInfo");
            }
            var sameHotKey = from val in hotKeys.Values
                             where val.Key == hotKeyInfo.Key && val.KeyModifiers == hotKeyInfo.KeyModifiers
                             select val;
            if (sameHotKey.Count() > 0)
            {
                throw new ArgumentException("The hot key has been existed.", "hotKeyInfo");
            }
            var sameName = from val in hotKeys
                           where val.Value.Name == hotKeyInfo.Name
                           select val;
            if (sameName.Count() > 0)
            {
                throw new ArgumentException("The name has been existed.", "hotKeyInfo");
            }
            if (hotKeyInfo.Key == Keys.None)
            {
                throw new ArgumentException("The key can't be none, if you want remove it, please call Change().", "hotKeyInfo");
            }

            int newId;
            try
            {
                newId = Register(hotKeyInfo.KeyModifiers, hotKeyInfo.Key);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            hotKeys.Add(newId, hotKeyInfo);
        }

        /// <remarks>
        /// 如果名相同且和热键相同，则不会引发异常。
        /// </remarks>
        public static void Change(string name, User32.KeyModifiers keyModifier, Keys key, Control invokeHandleControl, HotKeyHandle hotKeyHandle)
        {
            HotKeyInfo hotKeyInfo = new HotKeyInfo();
            hotKeyInfo.Name = name;
            hotKeyInfo.KeyModifiers = keyModifier;
            hotKeyInfo.Key = key;
            hotKeyInfo.InvokeHandleControl = invokeHandleControl;
            hotKeyInfo.HotKeyHandle = hotKeyHandle;
            Change(hotKeyInfo);
        }
        /// <remarks>
        /// 如果名相同且和热键相同，则不会引发异常。
        /// </remarks>
        public static void Change(HotKeyInfo hotKeyInfo)
        {
            var sameName = from val in hotKeys
                           where val.Value.Name == hotKeyInfo.Name
                           select val;
            if (sameName.Count() == 0)
            {
                throw new ArgumentException("The name doesn't exist.", "hotKeyInfo");
            }else if (sameName.Count() > 1)
            {
                throw new Exception("A unknow error from HotKeyManager.Change.");
            }
            if (hotKeyInfo.InvokeHandleControl == null)
            {
                throw new ArgumentException("The invoke handle control can't be null.", "hotKeyInfo");
            }
            if (hotKeyInfo.HotKeyHandle == null)
            {
                throw new ArgumentException("The hot key handle can't be null.", "hotKeyInfo");
            }
            var sameHotKey = from val in hotKeys.Values
                             where val.Key == hotKeyInfo.Key && val.KeyModifiers == hotKeyInfo.KeyModifiers
                             select val;
            if (sameHotKey.Count() > 0)
            {
                throw new ArgumentException("The hot key has been existed.", "hotKeyInfo");
            }
            if (hotKeyInfo.Key == Keys.None)
            {
                throw new ArgumentException("The key can't be none, if you want remove it, please call Change().", "hotKeyInfo");
            }

            int newId;
            try
            {
                newId = Register(hotKeyInfo.KeyModifiers, hotKeyInfo.Key);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            hotKeys.Remove(sameName.Single().Key);
            hotKeys.Add(newId, hotKeyInfo);
        }

        private static int Register(User32.KeyModifiers keyModifiers, Keys key)
        {
            int newId = 0;
            Random random = new Random();
            while(newId == 0 || hotKeys.Keys.Contains(newId))
            {
                newId = random.Next();
            }

            if (!User32.RegisterHotKey(innerWindowHandleBody.Handle, newId, keyModifiers, key))
            {
                throw new Exception("Register hot key failed.");
            }

            return newId;
        }

        public static HotKeyInfo Remove(string name)
        {
            var sameName = from val in hotKeys
                           where val.Value.Name == name
                           select val;
            if (sameName.Count() != 1)
            {
                Program.BeginInvoke((Action)(() =>
                {
                    throw new Exception("A unknow error from HotKeyManager.Remove.");
                }));
            }

            KeyValuePair<int, HotKeyInfo> target = sameName.Single();

            Unregister(target.Key);
            hotKeys.Remove(target.Key);

            return target.Value;
        }

        private static void Unregister(int id)
        {
            //不考虑错误的注销
            User32.UnregisterHotKey(innerWindowHandleBody.Handle, id);
        }

        public static void Clear()
        {
            foreach(KeyValuePair<int, HotKeyInfo> temp in hotKeys)
            {
                Unregister(temp.Key);
            }
        }
    }
}
