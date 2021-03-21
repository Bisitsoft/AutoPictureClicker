using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPictureClicker
{
    public class ProgramArguments
    {
        public static readonly bool IsDebug = true;

        public int ExitClickCount = -1;
        public string ExitFlagPath = null;

        public bool NoLogo = false;
        public bool StartThreadDirectly = false;
    }
}
