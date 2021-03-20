using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoPictureClicker
{
    public static class Config
    {
        //public struct ConfigValue
        //{
        //    public string Value;
        //    public Type DefaultType;
        //    public bool IsConst;
        //}
        private static SortedDictionary<string, string> configs = new SortedDictionary<string, string>();
        public static SortedDictionary<string, string> Configs { get { return configs; } }

        public static string Get(string name)
        {
            var sameName = from val in configs
                           where val.Key == name
                           select val;
            if (sameName.Count() > 0)
            {
                return sameName.Single().Value;
            }
            else
            {
                return null;
            }
        }
        public static void Set(string name, string value, bool flushAfterDone)
        {
            if (configs.ContainsKey(name))
            {
                configs.Remove(name);
                configs.Add(name, value);
            }
            else
            {
                throw new ArgumentException("The key name doesn't exist.", "name");
            }

            if (flushAfterDone)
            {
                Flush();
            }
        }
        public static void Flush()
        {
            WriteConfig();
        }

        private static bool hasInitialized = false;

        //初始值
        //public static readonly string DefalutConfigPath = "Config.cfg";
        //public static readonly string DefalutScreenShotPath = "ScreenShot.png";
        //public static readonly string DefalutResultPath = "Result.png";
        public static readonly int DefalutScreenIndex = 0;
        public static readonly string DefalutTemplatePath = "Template.png";
        public static readonly byte DefalutThreshold = 245;
        private static readonly int DefalutDelay = 5000;
        //常量
        public static readonly string ConfigPath = "config.config";
        public static readonly string ScreenShotPath = "ScreenShot.png";
        public static readonly string ResultPath = "Result.png";
        public static readonly string OutputDirectory = "./output";
        //表
        public static readonly string Name_ScreenIndex = "ScreenIndex";
        public static readonly string Name_TemplatePath = "TemplatePath";
        public static readonly string Name_Threshold = "Threshold";
        public static readonly string Name_Delay = "Delay";

        public static void Init()
        {
            if (!hasInitialized)
            {
                //hasInitialized = true;

                //初始化参数表
                configs.Add(Name_ScreenIndex, DefalutScreenIndex.ToString());
                configs.Add(Name_TemplatePath, DefalutTemplatePath);
                configs.Add(Name_Threshold, DefalutThreshold.ToString());
                configs.Add(Name_Delay, DefalutDelay.ToString());

                //hasInitialized = true;

                FileInfo fi = new FileInfo(ConfigPath);
                if (fi.Exists)
                {
                    ReadConfig(false);
                }
                else
                {
                    WriteConfig();
                }

                hasInitialized = true;
            }
        }

        private static void ReadConfig(bool CanAdd = false)
        {
            FileStream fs = new FileStream(ConfigPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
            StreamReader sr = new StreamReader(fs);

            
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                KeyValuePair<string, string> kvp;
                try
                {
                    kvp = FindKeyAndValue(line);
                }
                catch(Exception ex)
                {
                    fs.Close();
                    throw ex;
                }
                if (kvp.Key == null)
                {
                    continue;
                }

                bool KeyContained = configs.Keys.Contains(kvp.Key);
                if (!CanAdd)
                {
                    if (!KeyContained)
                    {
                        fs.Close();
                        throw new ArgumentException("Unknown option\"" + kvp.Key + "\".");
                    }
                }

                if (KeyContained)
                {
                    configs.Remove(kvp.Key);
                }
                configs.Add(kvp.Key, kvp.Value);
            }

            fs.Close();
        }
        
        private static KeyValuePair<string,string> FindKeyAndValue(string oneLine)
        {
            string key = null;
            string value = null;

            int i = 0;

            //其实可以用Linq的SkipWhile
            void SkipWhitSpace()
            {
                while (true)
                {
                    if (i < oneLine.Length && Char.IsWhiteSpace(oneLine[i]))
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //<space>keyname<space>=<space>value<sapce>

            //跳过如空格，横向制表符之类的字符
            SkipWhitSpace();
            if (i >= oneLine.Length)
            {
                return new KeyValuePair<string, string>(null, null);//空行
            }
            if (oneLine[i] == '#')
            {
                return new KeyValuePair<string, string>(null, null);//注释行
            }

            //获取键名
            key = new string(oneLine.Substring(i).TakeWhile((char ch) => { return (!Char.IsWhiteSpace(ch)) && (ch != '=') && (ch != '#'); }).ToArray());
            //无需检测键名是否符合命名规则，直接检测键名是否存在
            if (!configs.Keys.Contains(key))
            {
                throw new Exception("Unknow key name\"" + key + "\".");
            }
            i += key.Length;
            if (oneLine[i] == '#')
            {
                throw new FormatException("Lost key word \"=\".");
            }

            //跳过空格
            SkipWhitSpace();
            if (i >= oneLine.Length)
            {
                throw new FormatException("Lost key word \"=\".");
            }

            //获取符号=
            if (oneLine[i++] != '=')
            {
                throw new FormatException("Lost key word \"=\".");
            }

            //跳过空格
            SkipWhitSpace();
            if (i >= oneLine.Length || oneLine[i] == '#')
            {
                throw new FormatException("Lost value.");
            }

            //获取值
            if (oneLine[i] == '"')
            {
                //简单算法
                StringBuilder sb = new StringBuilder(oneLine.Substring(++i));
                int j;
                for (j = 0; j < sb.Length; j++)
                {
                    if (sb[j] == '\\')
                    {
                        j++;

                        if (j >= sb.Length)
                        {
                            throw new Exception("Lost second double quote.");
                        }

                        if ("abfnrtv\\'\"?".Contains(sb[j]) ||
                            (j + 2 < sb.Length && (!Regex.Match(sb.ToString().Substring(j, 3), "^[0-9]{3}$").Success)))
                        {
                            char temp = '\0';
                            switch (sb[j])
                            {
                                case '0':
                                    temp = '\0';
                                    break;
                                case 'a':
                                    temp = '\a';
                                    break;
                                case 'b':
                                    temp = '\b';
                                    break;
                                case 'f':
                                    temp = '\f';
                                    break;
                                case 'n':
                                    temp = '\n';
                                    break;
                                case 'r':
                                    temp = '\r';
                                    break;
                                case 't':
                                    temp = '\t';
                                    break;
                                case 'v':
                                    temp = '\v';
                                    break;
                                case '\\':
                                case '\'':
                                case '"':
                                case '?':
                                    temp = sb[j];
                                    break;
                            }
                            sb.Remove(j - 1, 2);
                            sb.Insert(j - 1, temp);
                            j--;
                        }
                        else if(j + 2 < sb.Length && Regex.Match(sb.ToString().Substring(j, 3), "^[0-9]{3}$").Success)
                        {
                            int temp = Convert.ToInt32(sb.ToString().Substring(j,3), 8);
                            if (temp > byte.MaxValue)
                            {
                                throw new FormatException("The ESC code is too big \"\\" + sb.ToString().Substring(j, j + 1) + "\".");
                            }
                            sb.Remove(j - 1, 4);
                            sb.Insert(j - 1, (char)temp);

                            j -= 3;
                        }
                        else if (
                            j + 2 < sb.Length && Regex.Match(sb.ToString().Substring(j,3),"^x[A-Fa-f0-9]{2}$").Success)
                        {
                            int temp = Convert.ToInt32(sb.ToString().Substring(j + 1, 2), 16);
                            sb.Remove(j - 1, 4);
                            sb.Insert(j - 1, (char)temp);

                            j -= 3;
                        }
                        else
                        {
                            throw new FormatException("Unrecognized ESC \"\\" + sb[j] + "\".");
                        }
                    }
                    else if (sb[j] == '"')
                    {
                        break;
                    }
                }
                if (j >= sb.Length)
                {
                    throw new Exception("Lost second double quote.");
                }
                value = sb.ToString().Substring(0, j);
                oneLine = oneLine.Substring(0, i) + sb.ToString() + "";
                i += value.Length + 1;
            }
            else
            {
                value = new string(oneLine.Substring(i).TakeWhile((char ch) => { return (!Char.IsWhiteSpace(ch)); }).ToArray());//无视注释
                i += value.Length;
            }

            //检查行末
            SkipWhitSpace();
            if (i < oneLine.Length && oneLine[i] != '#')
            {
                //不是空白或注释结尾
                throw new FormatException("The \"" + oneLine.Substring(i) + "\" is expletory part.");
            }

            return new KeyValuePair<string, string>(key, value);
        }

        private static void WriteConfig()
        {
            StreamWriter sw = new StreamWriter(ConfigPath, false, Encoding.UTF8);

            sw.Write("#Picture Clicker的配置：\n" +
                "#格式: 行注释：<键名> = 值。\n" +
                "#      行末注释：<键名> = \"字符串值\" (双引号内支持转义字符，并且要注意在#前留出空车才会被识别)。\n" +
                "#注释: 以字符#开头，或者在值后用字符#。(暂时不支持修改注释，请不要尝试在此处用注释记录，以免丢失)\n" +
                "#如果配置出错，可以尝试删除配置来恢复出厂设置。\n" +
                "\n" +
                "#使用的屏幕序号。\n" +
                "#这个序号从0开始，一般主屏幕序号为0。\n" +
                Name_ScreenIndex + " = " + Escape(Get(Name_ScreenIndex), false) + "\n" +
                "\n" +
                "#模板图片路径。\n" +
                Name_TemplatePath + " = " + Escape(Get(Name_TemplatePath), true) + "\n" +
                "\n" +
                "#图像相似度阀值。\n" +
                "#可以设置为0~255，越大表示越相似。\n" +
                "#当屏幕中与模板图片最相似的一个区域的相似度>=该值时，触发点击。\n" +
                Name_Threshold + " = " + Escape(Get(Name_Threshold), false) + "\n" +
                "\n" +
                "#每次检测周期间隔。\n" +
                "#为了避免卡顿，实际精度大于100ms。\n" +
                Name_Delay + " = " + Escape(Get(Name_Delay), false) + " #ms\n");
            sw.Flush();

            sw.Close();
        }
        private static string Escape(string source, bool inDoubleQuotationMarks)
        {
            StringBuilder sb = new StringBuilder("");

            if (inDoubleQuotationMarks)
            {
                sb.Append("\"");
            }

            for (int i = 0; i < source.Length; i++)
            {
                if ("\\'\"?".Contains(source[i]))
                {
                    sb.Append("\\" + source[i].ToString());
                }
                //不对其他字符做转义，以免发生非ASCII字符的格式问题
                else
                {
                    sb.Append(source[i].ToString());
                }
            }

            if (inDoubleQuotationMarks)
            {
                sb.Append("\"");
            }

            return sb.ToString();
        }
    }
}
