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
        #region ===ShowMessage===
        public void ShowError(string text)
        {
            ShowMessage(text, "Error 错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        public void ShowWarning(string text)
        {
            ShowMessage(text, "Warning 警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }
        public void ShowInformation(string text)
        {
            ShowMessage(text, "Information 信息", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        public void ShowMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            if (clickThread != null && clickThread.IsAlive)
            {
                PauseClickThread();
                MessageBox.Show(text, caption + "（注意：为了能够正常读取该消息，自动点击已暂停，并持续到该消息被关闭为止）", buttons, icon, defaultButton);
                ResumeClickThread();
            }
            else
            {
                MessageBox.Show(text, caption, buttons, icon, defaultButton);
            }
        }
        #endregion
    }
}
