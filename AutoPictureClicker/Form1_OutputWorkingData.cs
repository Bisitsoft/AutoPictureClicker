using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace AutoPictureClicker
{
    public partial class OutputWorkingData
    {
        public string FileName { get { return Path.Combine(Config.OutputDirectory, "workdata" /*+ threadId.ToString()*/ + ".out"); } }

        [JsonProperty("Timer")]
        public (int value, int max) Timer { get; set; }

        [JsonProperty("TotalRunningTime")]
        public TimeSpan TotalRunningTime { get; set; }

        [JsonProperty("LastLocation")]
        public Point LastLocation { get; set; }

        [JsonProperty("ClickCount")]
        public int ClickCount { get; set; }

        [JsonProperty("ScanedCount")]
        public int ScanedCount { get; set; }

        public void Output()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                //DefaultValueHandling=DefaultValueHandling.Include,
                //NullValueHandling=NullValueHandling.Include,
            };

            System.IO.File.WriteAllText(FileName, JsonConvert.SerializeObject(this, jsonSerializerSettings), Encoding.UTF8);
        }

        public void Load(string path)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                //DefaultValueHandling=DefaultValueHandling.Include,
                //NullValueHandling=NullValueHandling.Include,
            };

            OutputWorkingData outputWorkingData = new OutputWorkingData();
            string str = System.IO.File.ReadAllText(path, Encoding.UTF8);

            outputWorkingData = JsonConvert.DeserializeObject<OutputWorkingData>(str, jsonSerializerSettings);

            AllCopy.CopyTo(outputWorkingData, this);
        }

        public OutputWorkingData(int timer_value, int timer_max, TimeSpan totalRunningTime, Point lastLocation, int clickCount, int scanedCount)
        {
            this.Timer = (timer_value, timer_max);
            this.TotalRunningTime = totalRunningTime;
            this.LastLocation = lastLocation;
            this.ClickCount = clickCount;
            this.ScanedCount = scanedCount;
        }
        public OutputWorkingData() : this(0, 0, TimeSpan.Zero, Point.Empty, 0, 0) {; }
    }

    public partial class Form1
    {
        private void Output_WorkingData()
        {
#warning unfinished
            OutputWorkingData outputWorkingData = new OutputWorkingData()
            {
            };
        }
    }
}
