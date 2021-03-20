namespace AutoPictureClicker
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Abort = new System.Windows.Forms.Button();
            this.button_Boot = new System.Windows.Forms.Button();
            this.button_SetsWitchHotKey = new System.Windows.Forms.Button();
            this.textBox_SetSwitchHotKey = new System.Windows.Forms.TextBox();
            this.radioButton_ThreadStatus = new System.Windows.Forms.RadioButton();
            this.comboBox_SelectScreen = new System.Windows.Forms.ComboBox();
            this.label_SelectScreen = new System.Windows.Forms.Label();
            this.button_SelectTemplate = new System.Windows.Forms.Button();
            this.label_TemplatePath = new System.Windows.Forms.Label();
            this.groupBox_SelectTemplate = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar_Timer = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel_Timer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_TotalRunningTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_LastLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_LastValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ClickCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ScanedCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.trackBar_SetThreshold = new System.Windows.Forms.TrackBar();
            this.trackBar_SetTimer = new System.Windows.Forms.TrackBar();
            this.textBox_SetThreshold = new System.Windows.Forms.TextBox();
            this.textBox_SetTimer = new System.Windows.Forms.TextBox();
            this.label_SetThresholdUnit = new System.Windows.Forms.Label();
            this.label_SetThreshold = new System.Windows.Forms.Label();
            this.label_SetTimer = new System.Windows.Forms.Label();
            this.label_SetTimerUnit = new System.Windows.Forms.Label();
            this.openFileDialog_SelectTemplate = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_SelectTemplate.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SetThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SetTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Abort
            // 
            this.button_Abort.Location = new System.Drawing.Point(713, 12);
            this.button_Abort.Name = "button_Abort";
            this.button_Abort.Size = new System.Drawing.Size(75, 21);
            this.button_Abort.TabIndex = 3;
            this.button_Abort.Text = "Abort 终止";
            this.button_Abort.UseVisualStyleBackColor = true;
            this.button_Abort.Click += new System.EventHandler(this.button_Abort_Click);
            // 
            // button_Boot
            // 
            this.button_Boot.Location = new System.Drawing.Point(632, 12);
            this.button_Boot.Name = "button_Boot";
            this.button_Boot.Size = new System.Drawing.Size(75, 21);
            this.button_Boot.TabIndex = 2;
            this.button_Boot.Text = "Boot 启动";
            this.button_Boot.UseVisualStyleBackColor = true;
            this.button_Boot.Click += new System.EventHandler(this.button_Boot_Click);
            // 
            // button_SetsWitchHotKey
            // 
            this.button_SetsWitchHotKey.Location = new System.Drawing.Point(274, 12);
            this.button_SetsWitchHotKey.Name = "button_SetsWitchHotKey";
            this.button_SetsWitchHotKey.Size = new System.Drawing.Size(210, 21);
            this.button_SetsWitchHotKey.TabIndex = 1;
            this.button_SetsWitchHotKey.Text = "Set switch hot key 设置开关热键";
            this.button_SetsWitchHotKey.UseVisualStyleBackColor = true;
            this.button_SetsWitchHotKey.Click += new System.EventHandler(this.button_SetSwitchHotKey_Click);
            // 
            // textBox_SetSwitchHotKey
            // 
            this.textBox_SetSwitchHotKey.Font = new System.Drawing.Font("宋体", 9F);
            this.textBox_SetSwitchHotKey.Location = new System.Drawing.Point(12, 12);
            this.textBox_SetSwitchHotKey.Name = "textBox_SetSwitchHotKey";
            this.textBox_SetSwitchHotKey.Size = new System.Drawing.Size(256, 21);
            this.textBox_SetSwitchHotKey.TabIndex = 0;
            this.textBox_SetSwitchHotKey.TextChanged += new System.EventHandler(this.textBox_SetSwitchHotKey_TextChanged);
            this.textBox_SetSwitchHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_SetSwitchHotKey_KeyDown);
            // 
            // radioButton_ThreadStatus
            // 
            this.radioButton_ThreadStatus.AutoSize = true;
            this.radioButton_ThreadStatus.Location = new System.Drawing.Point(632, 39);
            this.radioButton_ThreadStatus.Name = "radioButton_ThreadStatus";
            this.radioButton_ThreadStatus.Size = new System.Drawing.Size(155, 16);
            this.radioButton_ThreadStatus.TabIndex = 4;
            this.radioButton_ThreadStatus.Text = "Thread status 线程状态";
            this.radioButton_ThreadStatus.UseVisualStyleBackColor = true;
            this.radioButton_ThreadStatus.Click += new System.EventHandler(this.radioButton_ThreadStatus_Click);
            // 
            // comboBox_SelectScreen
            // 
            this.comboBox_SelectScreen.FormattingEnabled = true;
            this.comboBox_SelectScreen.Location = new System.Drawing.Point(263, 208);
            this.comboBox_SelectScreen.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox_SelectScreen.Name = "comboBox_SelectScreen";
            this.comboBox_SelectScreen.Size = new System.Drawing.Size(121, 20);
            this.comboBox_SelectScreen.TabIndex = 17;
            this.comboBox_SelectScreen.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectScreen_SelectedIndexChanged);
            // 
            // label_SelectScreen
            // 
            this.label_SelectScreen.AutoSize = true;
            this.label_SelectScreen.Location = new System.Drawing.Point(10, 208);
            this.label_SelectScreen.Margin = new System.Windows.Forms.Padding(3);
            this.label_SelectScreen.Name = "label_SelectScreen";
            this.label_SelectScreen.Size = new System.Drawing.Size(137, 12);
            this.label_SelectScreen.TabIndex = 16;
            this.label_SelectScreen.Text = "Select screen 选择屏幕";
            // 
            // button_SelectTemplate
            // 
            this.button_SelectTemplate.Location = new System.Drawing.Point(701, 76);
            this.button_SelectTemplate.Name = "button_SelectTemplate";
            this.button_SelectTemplate.Size = new System.Drawing.Size(86, 23);
            this.button_SelectTemplate.TabIndex = 7;
            this.button_SelectTemplate.Text = "Browse 浏览";
            this.button_SelectTemplate.UseVisualStyleBackColor = true;
            this.button_SelectTemplate.Click += new System.EventHandler(this.button_SelectTemplate_Click);
            // 
            // label_TemplatePath
            // 
            this.label_TemplatePath.AutoSize = true;
            this.label_TemplatePath.Location = new System.Drawing.Point(6, 20);
            this.label_TemplatePath.Margin = new System.Windows.Forms.Padding(3);
            this.label_TemplatePath.Name = "label_TemplatePath";
            this.label_TemplatePath.Size = new System.Drawing.Size(77, 12);
            this.label_TemplatePath.TabIndex = 6;
            this.label_TemplatePath.Text = "Template.png";
            // 
            // groupBox_SelectTemplate
            // 
            this.groupBox_SelectTemplate.Controls.Add(this.label_TemplatePath);
            this.groupBox_SelectTemplate.Location = new System.Drawing.Point(12, 61);
            this.groupBox_SelectTemplate.Name = "groupBox_SelectTemplate";
            this.groupBox_SelectTemplate.Size = new System.Drawing.Size(683, 42);
            this.groupBox_SelectTemplate.TabIndex = 5;
            this.groupBox_SelectTemplate.TabStop = false;
            this.groupBox_SelectTemplate.Text = "Template path 模板路径";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar_Timer,
            this.toolStripStatusLabel_Timer,
            this.toolStripStatusLabel_TotalRunningTime,
            this.toolStripStatusLabel_LastLocation,
            this.toolStripStatusLabel_LastValue,
            this.toolStripStatusLabel_ClickCount,
            this.toolStripStatusLabel_ScanedCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 241);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar_Timer
            // 
            this.toolStripProgressBar_Timer.Name = "toolStripProgressBar_Timer";
            this.toolStripProgressBar_Timer.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar_Timer.Step = 1;
            this.toolStripProgressBar_Timer.ToolTipText = "Timer progress bar 计时器进度条";
            // 
            // toolStripStatusLabel_Timer
            // 
            this.toolStripStatusLabel_Timer.Name = "toolStripStatusLabel_Timer";
            this.toolStripStatusLabel_Timer.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel_Timer.Text = "0/0 ms";
            this.toolStripStatusLabel_Timer.ToolTipText = "Timer progress 计时器进度";
            // 
            // toolStripStatusLabel_TotalRunningTime
            // 
            this.toolStripStatusLabel_TotalRunningTime.Name = "toolStripStatusLabel_TotalRunningTime";
            this.toolStripStatusLabel_TotalRunningTime.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel_TotalRunningTime.Text = "0:0:0";
            this.toolStripStatusLabel_TotalRunningTime.ToolTipText = "Total running time 总运行时长";
            // 
            // toolStripStatusLabel_LastLocation
            // 
            this.toolStripStatusLabel_LastLocation.Name = "toolStripStatusLabel_LastLocation";
            this.toolStripStatusLabel_LastLocation.Size = new System.Drawing.Size(75, 17);
            this.toolStripStatusLabel_LastLocation.Text = "x = 0, y = 0";
            this.toolStripStatusLabel_LastLocation.ToolTipText = "Last Location 上次位置";
            // 
            // toolStripStatusLabel_LastValue
            // 
            this.toolStripStatusLabel_LastValue.Name = "toolStripStatusLabel_LastValue";
            this.toolStripStatusLabel_LastValue.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel_LastValue.Text = "0/255 (0%)";
            this.toolStripStatusLabel_LastValue.ToolTipText = "Last Similarity 上次相似度";
            // 
            // toolStripStatusLabel_ClickCount
            // 
            this.toolStripStatusLabel_ClickCount.Name = "toolStripStatusLabel_ClickCount";
            this.toolStripStatusLabel_ClickCount.Size = new System.Drawing.Size(104, 17);
            this.toolStripStatusLabel_ClickCount.Text = "Clicked 0 time(s)";
            this.toolStripStatusLabel_ClickCount.ToolTipText = "Click count 点击计数";
            // 
            // toolStripStatusLabel_ScanedCount
            // 
            this.toolStripStatusLabel_ScanedCount.Name = "toolStripStatusLabel_ScanedCount";
            this.toolStripStatusLabel_ScanedCount.Size = new System.Drawing.Size(104, 17);
            this.toolStripStatusLabel_ScanedCount.Text = "Scaned 0 time(s)";
            this.toolStripStatusLabel_ScanedCount.ToolTipText = "Scaning count 扫描计数";
            // 
            // trackBar_SetThreshold
            // 
            this.trackBar_SetThreshold.Location = new System.Drawing.Point(153, 109);
            this.trackBar_SetThreshold.Maximum = 255;
            this.trackBar_SetThreshold.Name = "trackBar_SetThreshold";
            this.trackBar_SetThreshold.Size = new System.Drawing.Size(104, 45);
            this.trackBar_SetThreshold.TabIndex = 9;
            this.trackBar_SetThreshold.ValueChanged += new System.EventHandler(this.trackBar_SetThreshold_ValueChanged);
            // 
            // trackBar_SetTimer
            // 
            this.trackBar_SetTimer.Location = new System.Drawing.Point(153, 160);
            this.trackBar_SetTimer.Maximum = 143;
            this.trackBar_SetTimer.Name = "trackBar_SetTimer";
            this.trackBar_SetTimer.Size = new System.Drawing.Size(104, 45);
            this.trackBar_SetTimer.TabIndex = 13;
            this.trackBar_SetTimer.ValueChanged += new System.EventHandler(this.trackBar_SetTimer_ValueChanged);
            // 
            // textBox_SetThreshold
            // 
            this.textBox_SetThreshold.Location = new System.Drawing.Point(263, 109);
            this.textBox_SetThreshold.Name = "textBox_SetThreshold";
            this.textBox_SetThreshold.Size = new System.Drawing.Size(121, 21);
            this.textBox_SetThreshold.TabIndex = 10;
            this.textBox_SetThreshold.Enter += new System.EventHandler(this.textBox_SetThreshold_Enter);
            this.textBox_SetThreshold.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_SetThreshold_KeyDown);
            this.textBox_SetThreshold.Leave += new System.EventHandler(this.textBox_SetThreshold_Leave);
            // 
            // textBox_SetTimer
            // 
            this.textBox_SetTimer.Location = new System.Drawing.Point(263, 160);
            this.textBox_SetTimer.Name = "textBox_SetTimer";
            this.textBox_SetTimer.Size = new System.Drawing.Size(121, 21);
            this.textBox_SetTimer.TabIndex = 14;
            this.textBox_SetTimer.Enter += new System.EventHandler(this.textBox_SetTimer_Enter);
            this.textBox_SetTimer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_SetTimer_KeyDown);
            this.textBox_SetTimer.Leave += new System.EventHandler(this.textBox_SetTimer_Leave);
            // 
            // label_SetThresholdUnit
            // 
            this.label_SetThresholdUnit.AutoSize = true;
            this.label_SetThresholdUnit.Location = new System.Drawing.Point(390, 112);
            this.label_SetThresholdUnit.Margin = new System.Windows.Forms.Padding(3);
            this.label_SetThresholdUnit.Name = "label_SetThresholdUnit";
            this.label_SetThresholdUnit.Size = new System.Drawing.Size(113, 12);
            this.label_SetThresholdUnit.TabIndex = 11;
            this.label_SetThresholdUnit.Text = "Similarity 相似度%";
            // 
            // label_SetThreshold
            // 
            this.label_SetThreshold.AutoSize = true;
            this.label_SetThreshold.Location = new System.Drawing.Point(10, 109);
            this.label_SetThreshold.Margin = new System.Windows.Forms.Padding(3);
            this.label_SetThreshold.Name = "label_SetThreshold";
            this.label_SetThreshold.Size = new System.Drawing.Size(137, 12);
            this.label_SetThreshold.TabIndex = 8;
            this.label_SetThreshold.Text = "Set threshold 设置阈值";
            // 
            // label_SetTimer
            // 
            this.label_SetTimer.AutoSize = true;
            this.label_SetTimer.Location = new System.Drawing.Point(10, 157);
            this.label_SetTimer.Margin = new System.Windows.Forms.Padding(3);
            this.label_SetTimer.Name = "label_SetTimer";
            this.label_SetTimer.Size = new System.Drawing.Size(125, 12);
            this.label_SetTimer.TabIndex = 12;
            this.label_SetTimer.Text = "Set timer 设置计时器";
            // 
            // label_SetTimerUnit
            // 
            this.label_SetTimerUnit.AutoSize = true;
            this.label_SetTimerUnit.Location = new System.Drawing.Point(390, 163);
            this.label_SetTimerUnit.Margin = new System.Windows.Forms.Padding(3);
            this.label_SetTimerUnit.Name = "label_SetTimerUnit";
            this.label_SetTimerUnit.Size = new System.Drawing.Size(137, 12);
            this.label_SetTimerUnit.TabIndex = 15;
            this.label_SetTimerUnit.Text = "ms 毫秒 ( = 0.001s 秒)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 263);
            this.Controls.Add(this.label_SetTimerUnit);
            this.Controls.Add(this.label_SetTimer);
            this.Controls.Add(this.label_SetThreshold);
            this.Controls.Add(this.label_SetThresholdUnit);
            this.Controls.Add(this.textBox_SetTimer);
            this.Controls.Add(this.textBox_SetThreshold);
            this.Controls.Add(this.trackBar_SetTimer);
            this.Controls.Add(this.trackBar_SetThreshold);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox_SelectTemplate);
            this.Controls.Add(this.button_SelectTemplate);
            this.Controls.Add(this.label_SelectScreen);
            this.Controls.Add(this.comboBox_SelectScreen);
            this.Controls.Add(this.radioButton_ThreadStatus);
            this.Controls.Add(this.textBox_SetSwitchHotKey);
            this.Controls.Add(this.button_SetsWitchHotKey);
            this.Controls.Add(this.button_Boot);
            this.Controls.Add(this.button_Abort);
            this.MinimumSize = new System.Drawing.Size(816, 302);
            this.Name = "Form1";
            this.Text = "AutoPictureClicker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.groupBox_SelectTemplate.ResumeLayout(false);
            this.groupBox_SelectTemplate.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SetThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SetTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Abort;
        private System.Windows.Forms.Button button_Boot;
        private System.Windows.Forms.Button button_SetsWitchHotKey;
        private System.Windows.Forms.TextBox textBox_SetSwitchHotKey;
        private System.Windows.Forms.RadioButton radioButton_ThreadStatus;
        private System.Windows.Forms.ComboBox comboBox_SelectScreen;
        private System.Windows.Forms.Label label_SelectScreen;
        private System.Windows.Forms.Button button_SelectTemplate;
        private System.Windows.Forms.Label label_TemplatePath;
        private System.Windows.Forms.GroupBox groupBox_SelectTemplate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar_Timer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Timer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_LastLocation;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_LastValue;
        private System.Windows.Forms.TrackBar trackBar_SetThreshold;
        private System.Windows.Forms.TrackBar trackBar_SetTimer;
        private System.Windows.Forms.TextBox textBox_SetThreshold;
        private System.Windows.Forms.TextBox textBox_SetTimer;
        private System.Windows.Forms.Label label_SetThresholdUnit;
        private System.Windows.Forms.Label label_SetThreshold;
        private System.Windows.Forms.Label label_SetTimer;
        private System.Windows.Forms.Label label_SetTimerUnit;
        private System.Windows.Forms.OpenFileDialog openFileDialog_SelectTemplate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_TotalRunningTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ClickCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ScanedCount;
    }
}

