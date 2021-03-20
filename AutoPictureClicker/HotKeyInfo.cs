using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPictureClicker
{
    public struct HotKeyInfo
    {
        public string Name;
        public User32.KeyModifiers KeyModifiers;
        public Keys Key;
        public Control InvokeHandleControl;
        public HotKeyHandle HotKeyHandle;
    }
}
