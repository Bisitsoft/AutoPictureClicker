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
        public static readonly User32.KeyModifiers DefaultKeyModifiers_SwitchClickThread = User32.KeyModifiers.Ctrl | User32.KeyModifiers.Shift;
        public static readonly Keys DefaultHotKey_SwitchClickThread = Keys.F12;

        private User32.KeyModifiers hotKeyModifiers_SwitchClickThread;
        private Keys hotKey_SwitchClickThread;
        private string hotKeyName_SwitchClickThread;

        private static readonly string HotKey_AbortClickThreadName = "SwitchClickThread";
        private Message HotKey_SwitchClickThread(Message message)
        {
            if(clickThread != null && clickThread.IsAlive)
            {
                AbortClickThread(true);
            }
            else
            {
                StartClickThread(true);
            }
            return message;
        }

        public static string GetHotKeyName()
        {
            return GetHotKeyName(User32.KeyModifiers.None, Keys.None);
        }
        public static string GetHotKeyName(User32.KeyModifiers keyModifier, Keys key)
        {
            if (key == Keys.None)
            {
                return "<null>";
            }

            StringBuilder name = new StringBuilder("");
            if ((keyModifier & User32.KeyModifiers.Alt) == User32.KeyModifiers.Alt)
            {
                name.Append("Alt + ");
            }
            if ((keyModifier & User32.KeyModifiers.Ctrl) == User32.KeyModifiers.Ctrl)
            {
                name.Append("Ctrl + ");
            }
            if ((keyModifier & User32.KeyModifiers.Shift) == User32.KeyModifiers.Shift)
            {
                name.Append("Shift + ");
            }

            name.Append(Enum.GetName(typeof(Keys), key));

            return name.ToString();
        }

        private void CleanHotKeyInfo_SwitchClickThread()
        {
            hotKeyModifiers_SwitchClickThread = User32.KeyModifiers.None;
            hotKey_SwitchClickThread = Keys.None;
            Change_textBox_SetSwitchHotKey_Text(GetHotKeyName());
        }

        private void InitHotKey()
        {
            try
            {
                HotKeyManager.Add(HotKey_AbortClickThreadName, DefaultKeyModifiers_SwitchClickThread, DefaultHotKey_SwitchClickThread, this, HotKey_SwitchClickThread);
                Change_textBox_SetSwitchHotKey_Text(GetHotKeyName(DefaultKeyModifiers_SwitchClickThread, DefaultHotKey_SwitchClickThread));
            }
            catch (Exception)
            {
                ShowMessage(
                    "默认热键注册失败。可能会影响使用，请更改热键设置。",
                    "Warning 警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                CleanHotKeyInfo_SwitchClickThread();
            }
        }

        private void button_SetSwitchHotKey_Click(object sender, EventArgs e)
        {
            var sameName = from val in HotKeyManager.HotKeyInfos
                           where val.Name == HotKey_AbortClickThreadName
                           select val;
            if (sameName.Count() > 0)
            {
                if (hotKey_SwitchClickThread == Keys.None)
                {
                    HotKeyManager.Remove(HotKey_AbortClickThreadName);
                }
                else
                {
                    try
                    {
                        HotKeyManager.Change(HotKey_AbortClickThreadName, hotKeyModifiers_SwitchClickThread, hotKey_SwitchClickThread, this, HotKey_SwitchClickThread);
                    }
                    catch (Exception)
                    {
                        HotKeyInfo last = sameName.Single();
                        textBox_SetSwitchHotKey.Text = GetHotKeyName(last.KeyModifiers, last.Key);

                        ShowWarning("注册热键失败。");
                        return;
                    }
                }
            }
            else
            {
                if (hotKey_SwitchClickThread != Keys.None)
                {
                    try
                    {
                        HotKeyManager.Add(HotKey_AbortClickThreadName, hotKeyModifiers_SwitchClickThread, hotKey_SwitchClickThread, this, HotKey_SwitchClickThread);
                    }
                    catch (Exception)
                    {
                        CleanHotKeyInfo_SwitchClickThread();

                        ShowWarning("注册热键失败。");
                        return;
                    }
                }
            }
            Change_textBox_SetSwitchHotKey_Text(GetHotKeyName(hotKeyModifiers_SwitchClickThread, hotKey_SwitchClickThread));
        }

        private void textBox_SetSwitchHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                CleanHotKeyInfo_SwitchClickThread();
                e.Handled = true;
                return;
            }

            hotKeyModifiers_SwitchClickThread = User32.KeyModifiers.None;
            if (e.Alt && e.KeyCode != Keys.Menu)
            {
                hotKeyModifiers_SwitchClickThread |= User32.KeyModifiers.Alt;
            }
            if (e.Control && e.KeyCode != Keys.ControlKey)
            {
                hotKeyModifiers_SwitchClickThread |= User32.KeyModifiers.Ctrl;
            }
            if (e.Shift && e.KeyCode != Keys.ShiftKey)
            {
                hotKeyModifiers_SwitchClickThread |= User32.KeyModifiers.Shift;
            }
            hotKey_SwitchClickThread = e.KeyCode;

            Change_textBox_SetSwitchHotKey_Text(GetHotKeyName(hotKeyModifiers_SwitchClickThread, hotKey_SwitchClickThread));

            e.Handled = true;
        }

        private void Change_textBox_SetSwitchHotKey_Text(string text)
        {
            textBox_SetSwitchHotKey.Text = hotKeyName_SwitchClickThread = text;
        }
        private void textBox_SetSwitchHotKey_TextChanged(object sender, EventArgs e)
        {
            textBox_SetSwitchHotKey.Text = hotKeyName_SwitchClickThread;
        }
    }
}
