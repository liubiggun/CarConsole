namespace CarConsole
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button_Open = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_PostState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_NowTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBox_W = new System.Windows.Forms.CheckBox();
            this.checkBox_A = new System.Windows.Forms.CheckBox();
            this.checkBox_S = new System.Windows.Forms.CheckBox();
            this.checkBox_D = new System.Windows.Forms.CheckBox();
            this.timer_key = new System.Windows.Forms.Timer(this.components);
            this.label_KeyDirFlag = new System.Windows.Forms.Label();
            this.label_motorPer1 = new System.Windows.Forms.Label();
            this.label_motorPer2 = new System.Windows.Forms.Label();
            this.label_CarState = new System.Windows.Forms.Label();
            this.groupBox_Control = new System.Windows.Forms.GroupBox();
            this.button_EnableKey = new System.Windows.Forms.Button();
            this.textBox_Receive = new System.Windows.Forms.TextBox();
            this.groupBox_Receive = new System.Windows.Forms.GroupBox();
            this.button_ReceiveClear = new System.Windows.Forms.Button();
            this.groupBox_Send = new System.Windows.Forms.GroupBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_SendPin2 = new System.Windows.Forms.Button();
            this.button_SendPin1 = new System.Windows.Forms.Button();
            this.button_SendClear = new System.Windows.Forms.Button();
            this.textBox_Send = new System.Windows.Forms.TextBox();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.timer_Send = new System.Windows.Forms.Timer(this.components);
            this.button_Dist0 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_Test = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox_StopBit = new System.Windows.Forms.ComboBox();
            this.comboBox_DataBits = new System.Windows.Forms.ComboBox();
            this.comboBox_Parity = new System.Windows.Forms.ComboBox();
            this.comboBox_BaudRate = new System.Windows.Forms.ComboBox();
            this.comboBox_Port = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox_PORT = new System.Windows.Forms.TextBox();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_Connect = new System.Windows.Forms.Button();
            this.button_StartImg = new System.Windows.Forms.Button();
            this.button_StopImg = new System.Windows.Forms.Button();
            this.button_QueryMode = new System.Windows.Forms.Button();
            this.button_ChangeMode = new System.Windows.Forms.Button();
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.label_ImgCount = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBox_Control.SuspendLayout();
            this.groupBox_Receive.SuspendLayout();
            this.groupBox_Send.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Open
            // 
            this.button_Open.Location = new System.Drawing.Point(24, 340);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(75, 23);
            this.button_Open.TabIndex = 0;
            this.button_Open.Text = "打开串口";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_PostState,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel_NowTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 446);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(950, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_PostState
            // 
            this.toolStripStatusLabel_PostState.Name = "toolStripStatusLabel_PostState";
            this.toolStripStatusLabel_PostState.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel_PostState.Text = "就绪";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(755, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel_NowTime
            // 
            this.toolStripStatusLabel_NowTime.Name = "toolStripStatusLabel_NowTime";
            this.toolStripStatusLabel_NowTime.Size = new System.Drawing.Size(148, 17);
            this.toolStripStatusLabel_NowTime.Text = "当前时间为：                 ";
            // 
            // checkBox_W
            // 
            this.checkBox_W.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_W.AutoCheck = false;
            this.checkBox_W.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_W.Location = new System.Drawing.Point(123, 69);
            this.checkBox_W.Name = "checkBox_W";
            this.checkBox_W.Size = new System.Drawing.Size(70, 58);
            this.checkBox_W.TabIndex = 4;
            this.checkBox_W.TabStop = false;
            this.checkBox_W.Text = "W";
            this.checkBox_W.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_W.UseVisualStyleBackColor = true;
            // 
            // checkBox_A
            // 
            this.checkBox_A.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_A.AutoCheck = false;
            this.checkBox_A.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_A.Location = new System.Drawing.Point(31, 145);
            this.checkBox_A.Name = "checkBox_A";
            this.checkBox_A.Size = new System.Drawing.Size(70, 58);
            this.checkBox_A.TabIndex = 4;
            this.checkBox_A.TabStop = false;
            this.checkBox_A.Text = "A";
            this.checkBox_A.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_A.UseVisualStyleBackColor = true;
            // 
            // checkBox_S
            // 
            this.checkBox_S.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_S.AutoCheck = false;
            this.checkBox_S.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_S.Location = new System.Drawing.Point(123, 145);
            this.checkBox_S.Name = "checkBox_S";
            this.checkBox_S.Size = new System.Drawing.Size(70, 58);
            this.checkBox_S.TabIndex = 4;
            this.checkBox_S.TabStop = false;
            this.checkBox_S.Text = "S";
            this.checkBox_S.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_S.UseVisualStyleBackColor = true;
            // 
            // checkBox_D
            // 
            this.checkBox_D.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_D.AutoCheck = false;
            this.checkBox_D.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_D.Location = new System.Drawing.Point(215, 145);
            this.checkBox_D.Name = "checkBox_D";
            this.checkBox_D.Size = new System.Drawing.Size(70, 58);
            this.checkBox_D.TabIndex = 4;
            this.checkBox_D.TabStop = false;
            this.checkBox_D.Text = "D";
            this.checkBox_D.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_D.UseVisualStyleBackColor = true;
            // 
            // timer_key
            // 
            this.timer_key.Enabled = true;
            this.timer_key.Interval = 50;
            this.timer_key.Tick += new System.EventHandler(this.timer_key_Tick);
            // 
            // label_KeyDirFlag
            // 
            this.label_KeyDirFlag.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_KeyDirFlag.Location = new System.Drawing.Point(107, 206);
            this.label_KeyDirFlag.Name = "label_KeyDirFlag";
            this.label_KeyDirFlag.Size = new System.Drawing.Size(100, 23);
            this.label_KeyDirFlag.TabIndex = 5;
            this.label_KeyDirFlag.Text = "0000";
            this.label_KeyDirFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_motorPer1
            // 
            this.label_motorPer1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_motorPer1.Location = new System.Drawing.Point(17, 91);
            this.label_motorPer1.Name = "label_motorPer1";
            this.label_motorPer1.Size = new System.Drawing.Size(100, 23);
            this.label_motorPer1.TabIndex = 5;
            this.label_motorPer1.Text = "0";
            this.label_motorPer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_motorPer2
            // 
            this.label_motorPer2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_motorPer2.Location = new System.Drawing.Point(199, 91);
            this.label_motorPer2.Name = "label_motorPer2";
            this.label_motorPer2.Size = new System.Drawing.Size(100, 23);
            this.label_motorPer2.TabIndex = 5;
            this.label_motorPer2.Text = "0";
            this.label_motorPer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_CarState
            // 
            this.label_CarState.AutoSize = true;
            this.label_CarState.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_CarState.Location = new System.Drawing.Point(16, 33);
            this.label_CarState.Name = "label_CarState";
            this.label_CarState.Size = new System.Drawing.Size(58, 21);
            this.label_CarState.TabIndex = 6;
            this.label_CarState.Text = "状态：";
            // 
            // groupBox_Control
            // 
            this.groupBox_Control.Controls.Add(this.checkBox_S);
            this.groupBox_Control.Controls.Add(this.label_CarState);
            this.groupBox_Control.Controls.Add(this.checkBox_W);
            this.groupBox_Control.Controls.Add(this.label_motorPer2);
            this.groupBox_Control.Controls.Add(this.checkBox_A);
            this.groupBox_Control.Controls.Add(this.label_motorPer1);
            this.groupBox_Control.Controls.Add(this.checkBox_D);
            this.groupBox_Control.Controls.Add(this.label_KeyDirFlag);
            this.groupBox_Control.Enabled = false;
            this.groupBox_Control.Location = new System.Drawing.Point(242, 12);
            this.groupBox_Control.Name = "groupBox_Control";
            this.groupBox_Control.Size = new System.Drawing.Size(315, 259);
            this.groupBox_Control.TabIndex = 2;
            this.groupBox_Control.TabStop = false;
            this.groupBox_Control.Text = "控制板";
            // 
            // button_EnableKey
            // 
            this.button_EnableKey.Enabled = false;
            this.button_EnableKey.Location = new System.Drawing.Point(242, 277);
            this.button_EnableKey.Name = "button_EnableKey";
            this.button_EnableKey.Size = new System.Drawing.Size(75, 23);
            this.button_EnableKey.TabIndex = 2;
            this.button_EnableKey.Text = "使能";
            this.button_EnableKey.UseVisualStyleBackColor = true;
            this.button_EnableKey.Click += new System.EventHandler(this.button_EnableKey_Click);
            // 
            // textBox_Receive
            // 
            this.textBox_Receive.BackColor = System.Drawing.Color.Silver;
            this.textBox_Receive.Location = new System.Drawing.Point(16, 20);
            this.textBox_Receive.Multiline = true;
            this.textBox_Receive.Name = "textBox_Receive";
            this.textBox_Receive.ReadOnly = true;
            this.textBox_Receive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Receive.Size = new System.Drawing.Size(334, 150);
            this.textBox_Receive.TabIndex = 10;
            this.textBox_Receive.MouseHover += new System.EventHandler(this.textBox_Receive_MouseHover);
            // 
            // groupBox_Receive
            // 
            this.groupBox_Receive.Controls.Add(this.button_ReceiveClear);
            this.groupBox_Receive.Controls.Add(this.textBox_Receive);
            this.groupBox_Receive.Location = new System.Drawing.Point(572, 12);
            this.groupBox_Receive.Name = "groupBox_Receive";
            this.groupBox_Receive.Size = new System.Drawing.Size(366, 209);
            this.groupBox_Receive.TabIndex = 3;
            this.groupBox_Receive.TabStop = false;
            this.groupBox_Receive.Text = "数据接收";
            // 
            // button_ReceiveClear
            // 
            this.button_ReceiveClear.Location = new System.Drawing.Point(16, 180);
            this.button_ReceiveClear.Name = "button_ReceiveClear";
            this.button_ReceiveClear.Size = new System.Drawing.Size(75, 23);
            this.button_ReceiveClear.TabIndex = 11;
            this.button_ReceiveClear.Text = "清空";
            this.button_ReceiveClear.UseVisualStyleBackColor = true;
            this.button_ReceiveClear.Click += new System.EventHandler(this.button_ReceiveClear_Click);
            // 
            // groupBox_Send
            // 
            this.groupBox_Send.Controls.Add(this.button_Send);
            this.groupBox_Send.Controls.Add(this.button_SendPin2);
            this.groupBox_Send.Controls.Add(this.button_SendPin1);
            this.groupBox_Send.Controls.Add(this.button_SendClear);
            this.groupBox_Send.Controls.Add(this.textBox_Send);
            this.groupBox_Send.Location = new System.Drawing.Point(572, 227);
            this.groupBox_Send.Name = "groupBox_Send";
            this.groupBox_Send.Size = new System.Drawing.Size(366, 209);
            this.groupBox_Send.TabIndex = 4;
            this.groupBox_Send.TabStop = false;
            this.groupBox_Send.Text = "数据发送";
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(16, 180);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 21;
            this.button_Send.Text = "发送";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_SendPin2
            // 
            this.button_SendPin2.Location = new System.Drawing.Point(285, 180);
            this.button_SendPin2.Name = "button_SendPin2";
            this.button_SendPin2.Size = new System.Drawing.Size(75, 23);
            this.button_SendPin2.TabIndex = 24;
            this.button_SendPin2.Text = "发送密码2";
            this.button_SendPin2.UseVisualStyleBackColor = true;
            this.button_SendPin2.Click += new System.EventHandler(this.button_SendPin2_Click);
            // 
            // button_SendPin1
            // 
            this.button_SendPin1.Location = new System.Drawing.Point(196, 180);
            this.button_SendPin1.Name = "button_SendPin1";
            this.button_SendPin1.Size = new System.Drawing.Size(75, 23);
            this.button_SendPin1.TabIndex = 23;
            this.button_SendPin1.Text = "发送密码1";
            this.button_SendPin1.UseVisualStyleBackColor = true;
            this.button_SendPin1.Click += new System.EventHandler(this.button_SendPin1_Click);
            // 
            // button_SendClear
            // 
            this.button_SendClear.Location = new System.Drawing.Point(106, 180);
            this.button_SendClear.Name = "button_SendClear";
            this.button_SendClear.Size = new System.Drawing.Size(75, 23);
            this.button_SendClear.TabIndex = 22;
            this.button_SendClear.Text = "清空";
            this.button_SendClear.UseVisualStyleBackColor = true;
            this.button_SendClear.Click += new System.EventHandler(this.button_SendClear_Click);
            // 
            // textBox_Send
            // 
            this.textBox_Send.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox_Send.Location = new System.Drawing.Point(16, 20);
            this.textBox_Send.Multiline = true;
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Send.Size = new System.Drawing.Size(334, 150);
            this.textBox_Send.TabIndex = 20;
            this.textBox_Send.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Send_KeyDown);
            this.textBox_Send.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Send_KeyPress);
            // 
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(105, 340);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.button_Refresh.TabIndex = 1;
            this.button_Refresh.Text = "刷新";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // timer_Send
            // 
            this.timer_Send.Enabled = true;
            this.timer_Send.Tick += new System.EventHandler(this.timer_Send_Tick);
            // 
            // button_Dist0
            // 
            this.button_Dist0.Enabled = false;
            this.button_Dist0.Location = new System.Drawing.Point(242, 306);
            this.button_Dist0.Name = "button_Dist0";
            this.button_Dist0.Size = new System.Drawing.Size(75, 23);
            this.button_Dist0.TabIndex = 2;
            this.button_Dist0.Text = "测距0";
            this.button_Dist0.UseVisualStyleBackColor = true;
            this.button_Dist0.Click += new System.EventHandler(this.button_Dist0_Click);
            // 
            // button_Test
            // 
            this.button_Test.Location = new System.Drawing.Point(482, 277);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(75, 23);
            this.button_Test.TabIndex = 2;
            this.button_Test.Text = "测试";
            this.button_Test.UseVisualStyleBackColor = true;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(223, 418);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox_StopBit);
            this.tabPage1.Controls.Add(this.comboBox_DataBits);
            this.tabPage1.Controls.Add(this.comboBox_Parity);
            this.tabPage1.Controls.Add(this.comboBox_BaudRate);
            this.tabPage1.Controls.Add(this.comboBox_Port);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button_Open);
            this.tabPage1.Controls.Add(this.button_Refresh);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(215, 392);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "串口设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox_StopBit
            // 
            this.comboBox_StopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_StopBit.FormattingEnabled = true;
            this.comboBox_StopBit.Location = new System.Drawing.Point(76, 178);
            this.comboBox_StopBit.Name = "comboBox_StopBit";
            this.comboBox_StopBit.Size = new System.Drawing.Size(121, 20);
            this.comboBox_StopBit.TabIndex = 12;
            this.comboBox_StopBit.TabStop = false;
            // 
            // comboBox_DataBits
            // 
            this.comboBox_DataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DataBits.FormattingEnabled = true;
            this.comboBox_DataBits.Location = new System.Drawing.Point(76, 140);
            this.comboBox_DataBits.Name = "comboBox_DataBits";
            this.comboBox_DataBits.Size = new System.Drawing.Size(121, 20);
            this.comboBox_DataBits.TabIndex = 13;
            this.comboBox_DataBits.TabStop = false;
            // 
            // comboBox_Parity
            // 
            this.comboBox_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Parity.FormattingEnabled = true;
            this.comboBox_Parity.Location = new System.Drawing.Point(76, 102);
            this.comboBox_Parity.Name = "comboBox_Parity";
            this.comboBox_Parity.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Parity.TabIndex = 14;
            this.comboBox_Parity.TabStop = false;
            // 
            // comboBox_BaudRate
            // 
            this.comboBox_BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BaudRate.FormattingEnabled = true;
            this.comboBox_BaudRate.Location = new System.Drawing.Point(76, 64);
            this.comboBox_BaudRate.Name = "comboBox_BaudRate";
            this.comboBox_BaudRate.Size = new System.Drawing.Size(121, 20);
            this.comboBox_BaudRate.TabIndex = 15;
            this.comboBox_BaudRate.TabStop = false;
            // 
            // comboBox_Port
            // 
            this.comboBox_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Port.FormattingEnabled = true;
            this.comboBox_Port.Location = new System.Drawing.Point(76, 26);
            this.comboBox_Port.Name = "comboBox_Port";
            this.comboBox_Port.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Port.TabIndex = 16;
            this.comboBox_Port.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "停止位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "数据位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "奇偶校验";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "端口";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox_PORT);
            this.tabPage2.Controls.Add(this.textBox_IP);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.button_Connect);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(215, 392);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TCP设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_PORT
            // 
            this.textBox_PORT.Location = new System.Drawing.Point(54, 76);
            this.textBox_PORT.Name = "textBox_PORT";
            this.textBox_PORT.Size = new System.Drawing.Size(138, 21);
            this.textBox_PORT.TabIndex = 6;
            this.textBox_PORT.Text = "36888";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(54, 38);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(138, 21);
            this.textBox_IP.TabIndex = 6;
            this.textBox_IP.Text = "172.28.32.1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "IP";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "端口";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(65, 340);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(75, 23);
            this.button_Connect.TabIndex = 6;
            this.button_Connect.Text = "连接";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // button_StartImg
            // 
            this.button_StartImg.Enabled = false;
            this.button_StartImg.Location = new System.Drawing.Point(241, 335);
            this.button_StartImg.Name = "button_StartImg";
            this.button_StartImg.Size = new System.Drawing.Size(75, 23);
            this.button_StartImg.TabIndex = 2;
            this.button_StartImg.Text = "获取图像";
            this.button_StartImg.UseVisualStyleBackColor = true;
            this.button_StartImg.Click += new System.EventHandler(this.button_FetchImg_Click);
            // 
            // button_StopImg
            // 
            this.button_StopImg.Enabled = false;
            this.button_StopImg.Location = new System.Drawing.Point(241, 364);
            this.button_StopImg.Name = "button_StopImg";
            this.button_StopImg.Size = new System.Drawing.Size(75, 23);
            this.button_StopImg.TabIndex = 2;
            this.button_StopImg.Text = "关闭获取";
            this.button_StopImg.UseVisualStyleBackColor = true;
            this.button_StopImg.Click += new System.EventHandler(this.button_StopImg_Click);
            // 
            // button_QueryMode
            // 
            this.button_QueryMode.Enabled = false;
            this.button_QueryMode.Location = new System.Drawing.Point(242, 420);
            this.button_QueryMode.Name = "button_QueryMode";
            this.button_QueryMode.Size = new System.Drawing.Size(75, 23);
            this.button_QueryMode.TabIndex = 2;
            this.button_QueryMode.Text = "查询模式";
            this.button_QueryMode.UseVisualStyleBackColor = true;
            this.button_QueryMode.Click += new System.EventHandler(this.button_QueryMode_Click);
            // 
            // button_ChangeMode
            // 
            this.button_ChangeMode.Enabled = false;
            this.button_ChangeMode.Location = new System.Drawing.Point(242, 393);
            this.button_ChangeMode.Name = "button_ChangeMode";
            this.button_ChangeMode.Size = new System.Drawing.Size(75, 23);
            this.button_ChangeMode.TabIndex = 2;
            this.button_ChangeMode.Text = "改变模式";
            this.button_ChangeMode.UseVisualStyleBackColor = true;
            this.button_ChangeMode.Click += new System.EventHandler(this.button_ChangeMode_Click);
            // 
            // comboBox_Mode
            // 
            this.comboBox_Mode.FormattingEnabled = true;
            this.comboBox_Mode.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.comboBox_Mode.Location = new System.Drawing.Point(328, 395);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(31, 20);
            this.comboBox_Mode.TabIndex = 6;
            // 
            // label_ImgCount
            // 
            this.label_ImgCount.AutoSize = true;
            this.label_ImgCount.Location = new System.Drawing.Point(394, 398);
            this.label_ImgCount.Name = "label_ImgCount";
            this.label_ImgCount.Size = new System.Drawing.Size(65, 12);
            this.label_ImgCount.TabIndex = 8;
            this.label_ImgCount.Text = "图片数量：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 468);
            this.Controls.Add(this.label_ImgCount);
            this.Controls.Add(this.comboBox_Mode);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_Test);
            this.Controls.Add(this.button_ChangeMode);
            this.Controls.Add(this.button_QueryMode);
            this.Controls.Add(this.button_StopImg);
            this.Controls.Add(this.button_StartImg);
            this.Controls.Add(this.button_Dist0);
            this.Controls.Add(this.button_EnableKey);
            this.Controls.Add(this.groupBox_Send);
            this.Controls.Add(this.groupBox_Receive);
            this.Controls.Add(this.groupBox_Control);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小车控制台";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox_Control.ResumeLayout(false);
            this.groupBox_Control.PerformLayout();
            this.groupBox_Receive.ResumeLayout(false);
            this.groupBox_Receive.PerformLayout();
            this.groupBox_Send.ResumeLayout(false);
            this.groupBox_Send.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_PostState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_NowTime;
        private System.Windows.Forms.CheckBox checkBox_W;
        private System.Windows.Forms.CheckBox checkBox_A;
        private System.Windows.Forms.CheckBox checkBox_S;
        private System.Windows.Forms.CheckBox checkBox_D;
        private System.Windows.Forms.Timer timer_key;
        private System.Windows.Forms.Label label_KeyDirFlag;
        private System.Windows.Forms.Label label_motorPer1;
        private System.Windows.Forms.Label label_motorPer2;
        private System.Windows.Forms.Label label_CarState;
        private System.Windows.Forms.GroupBox groupBox_Control;
        private System.Windows.Forms.TextBox textBox_Receive;
        private System.Windows.Forms.GroupBox groupBox_Receive;
        private System.Windows.Forms.GroupBox groupBox_Send;
        private System.Windows.Forms.TextBox textBox_Send;
        private System.Windows.Forms.Button button_ReceiveClear;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_SendClear;
        private System.Windows.Forms.Button button_EnableKey;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Timer timer_Send;
        private System.Windows.Forms.Button button_Dist0;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Test;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox comboBox_StopBit;
        private System.Windows.Forms.ComboBox comboBox_DataBits;
        private System.Windows.Forms.ComboBox comboBox_Parity;
        private System.Windows.Forms.ComboBox comboBox_BaudRate;
        private System.Windows.Forms.ComboBox comboBox_Port;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_PORT;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Button button_SendPin1;
        private System.Windows.Forms.Button button_SendPin2;
        private System.Windows.Forms.Button button_StartImg;
        private System.Windows.Forms.Button button_StopImg;
        private System.Windows.Forms.Button button_QueryMode;
        private System.Windows.Forms.Button button_ChangeMode;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.Label label_ImgCount;
    }
}

