using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarConsole.ClassLib;
using CarConsole.Dev;
using CarConsole.Util;
using System.Threading;

namespace CarConsole
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 命令发送器
        /// </summary>
        CmdSender cmdSender;
        /// <summary>
        /// 图像接收器
        /// </summary>
        ImgReceiver imgReciever = new ImgReceiver();
        /// <summary>
        /// 获取图像线程（生产者）
        /// </summary>
        Thread imgProducer = new Thread(() => { });
        /// <summary>
        /// 显示图片窗口
        /// </summary>
        ImgForm imgForm = new ImgForm();
        SingleImgForm singleImgForm = new SingleImgForm();

        /// <summary>
        /// 方向按键标识,四位分别对应wsad
        /// </summary>
        byte _keyDirFlag = 0;
        /// <summary>
        /// 按键是否按下，是否向下位机发送命令
        /// </summary>
        bool keyCtl = false; 
        /// <summary>
        /// 用来接收图片的tcp端口
        /// </summary>
        string imgPort = "36889";

        public MainForm()
        {
            InitializeComponent();           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.cmdSender = new CmdSender();
            
            this.cmdSender.Serial.SetComboBox(ref comboBox_Port, ref comboBox_BaudRate, ref comboBox_Parity, ref comboBox_DataBits, ref comboBox_StopBit);//绑定控件
            this.cmdSender.Serial.DataReceived += new CarConsole.ClassLib.SerialPortUtil.UtilSerialDataReceivedEventHandler(_serial_DateReceive);//订阅事件

            this.cmdSender.Net.DataReceived += new CommandClient.DataReceivedEventHandler(_serial_DateReceive);

            this.comboBox_Port.SelectedIndex = this.comboBox_Port.Items.Count - 1;
            this.comboBox_Mode.SelectedIndex = 0;
        }

        /// <summary>
        /// 按下键
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {         
            switch (e.KeyCode)
            {
                case Keys.W:
                    {
                        this._keyDirFlag |= 0x8;//1000
                        this.checkBox_W.Checked = true;
                    }
                    break;
                case Keys.S:
                    {
                        this._keyDirFlag |= 0x4;//0100
                        this.checkBox_S.Checked = true;
                    }
                    break;
                case Keys.A:
                    {
                        this._keyDirFlag |= 0x2;//0010
                        this.checkBox_A.Checked = true;
                    }
                    break;
                case Keys.D:
                    {
                        this._keyDirFlag |= 0x1;//0001
                        this.checkBox_D.Checked = true;
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 放开键
        /// </summary>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    {
                        this._keyDirFlag &= 0x7;//0111
                        this.checkBox_W.Checked = false;
                    }
                    break;
                case Keys.S:
                    {
                        this._keyDirFlag &= 0xB;//1011
                        this.checkBox_S.Checked = false;
                    }
                    break;
                case Keys.A:
                    {
                        this._keyDirFlag &= 0xD;//1101
                        this.checkBox_A.Checked = false;
                    }
                    break;
                case Keys.D:
                    {
                        this._keyDirFlag &= 0xE;//1110
                        this.checkBox_D.Checked = false;
                    }
                    break;                
                default:
                    break;
            }

        }

        private void timer_key_Tick(object sender, EventArgs e)
        {
            this.label_ImgCount.Text = "图片数量：" + ImgReceiver.Imgtotal;
            this.label_ImgrestCount.Text = "剩下数量" + ImgReceiver.Imgrest;
            this.toolStripStatusLabel_ImgRecvState.Text = ImgReceiver.TellMainForm;
            
            if (KeyPreview)//按键事件有效时
            { 

                //改变电机数值
                byte cmd = (byte)(this._keyDirFlag & 0xF);
                this.cmdSender.DMControl.Command(cmd);              
            }

            //显示按键按下的2进制flag
            this.label_KeyDirFlag.Text = (checkBox_W.Checked ? "1" : "0") + (checkBox_S.Checked ? "1" : "0") + (checkBox_A.Checked ? "1" : "0") + (checkBox_D.Checked ? "1" : "0");

            //显示转速百分比
            if (this.cmdSender.DMControl.Motors[0].PerSpeed > 0) this.label_motorPer1.Text = "+";
            else this.label_motorPer1.Text = "";
            if (this.cmdSender.DMControl.Motors[1].PerSpeed > 0) this.label_motorPer2.Text = "+";
            else this.label_motorPer2.Text = "";
            this.label_motorPer1.Text += this.cmdSender.DMControl.Motors[0].PerSpeed.ToString();
            this.label_motorPer2.Text += this.cmdSender.DMControl.Motors[1].PerSpeed.ToString();
            this.label_CarState.Text = "状态：" + this.cmdSender.DMControl.CurState.ToString();

            this.toolStripStatusLabel_NowTime.Text = DateTime.Now.ToString();
        }

        private void timer_Send_Tick(object sender, EventArgs e)
        {
            if (KeyPreview)//按键事件有效时
                if (this.cmdSender.DMControl.Motors[0].SpeedChanged || this.cmdSender.DMControl.Motors[0].SpeedChanged)
                    this.cmdSender.ControlMotor();
        }

        /// <summary>
        /// 给SerialPortUtil类的DateReceive事件订阅的函数
        /// </summary>
        private void _serial_DateReceive(object sender, DataReceivedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.textBox_Receive.Text += (e.DataReceived + Environment.NewLine);
            }));           
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            if (button_Open.Text == "打开串口")
            {
                this.cmdSender.Serial.OpenPort();
                this.button_Open.Text = "关闭串口";
                this.toolStripStatusLabel_PostState.Text = "串口" + comboBox_Port.SelectedItem.ToString() + "已打开";

                this.comboBox_Port.Enabled = false;
                this.comboBox_BaudRate.Enabled = false;
                this.comboBox_Parity.Enabled = false;
                this.comboBox_DataBits.Enabled = false;
                this.comboBox_StopBit.Enabled = false;

                this.button_Refresh.Enabled = false;
                this.button_Dist0.Enabled = true;
                this.button_EnableKey.Enabled = true;

                

            }
            else
            {
                this.cmdSender.Serial.ClosePort();
                this.button_Open.Text = "打开串口";
                this.toolStripStatusLabel_PostState.Text = "就绪";

                this.comboBox_Port.Enabled = true;
                this.comboBox_BaudRate.Enabled = true;
                this.comboBox_Parity.Enabled = true;
                this.comboBox_DataBits.Enabled = true;
                this.comboBox_StopBit.Enabled = true;

                this.button_Refresh.Enabled = true;
                this.button_Dist0.Enabled = false;
                this.button_EnableKey.Enabled = false;

                //关闭控制板
                this.KeyPreview = false;
                this.groupBox_Control.Enabled = false;
                this.button_EnableKey.Text = "使能";
                
            }
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            this.cmdSender.Serial.DataReceived += new CarConsole.ClassLib.SerialPortUtil.UtilSerialDataReceivedEventHandler(_serial_DateReceive);

            this.comboBox_Port.SelectedIndex = this.comboBox_Port.Items.Count - 1;
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (this.button_Connect.Text == "连接")
            {
                this.cmdSender.Net.ConnectServer(this.textBox_IP.Text, int.Parse(this.textBox_PORT.Text));
                this.cmdSender.Net.ReadData();
                this.button_Connect.Text = "断开";
                this.toolStripStatusLabel_PostState.Text = "连接到服务器";

                this.button_Dist0.Enabled = true;
                this.button_EnableKey.Enabled = true;
                this.button_StartImg.Enabled = true;
                this.button_StopImg.Enabled = true;
                this.button_QueryMode.Enabled = true;
                this.button_ChangeMode.Enabled = true;
            }
            else
            {
                this.cmdSender.Net.DisConnect();
                this.button_Connect.Text = "连接";
                this.toolStripStatusLabel_PostState.Text = "就绪";

                this.button_Dist0.Enabled = false;
                this.button_EnableKey.Enabled = false;
                this.button_StartImg.Enabled = false;
                this.button_StopImg.Enabled = false;
                this.button_QueryMode.Enabled = false;
                this.button_ChangeMode.Enabled = false;

                //关闭控制板
                this.KeyPreview = false;
                this.groupBox_Control.Enabled = false;
                this.button_EnableKey.Text = "使能";


                this.textBox_Receive.Text = String.Empty;
            }
        }

        private void button_EnableKey_Click(object sender, EventArgs e)
        {
            if (this.button_EnableKey.Text == "使能")
            {
                this.KeyPreview = true;
                this.groupBox_Control.Enabled = true;
                this.button_EnableKey.Text = "取消";
            }
            else
            {
                this.KeyPreview = false;
                this.groupBox_Control.Enabled = false;
                this.button_EnableKey.Text = "使能";
            }
        }

        private void button_ReceiveClear_Click(object sender, EventArgs e)
        {
            this.textBox_Receive.Text = String.Empty;
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            this.cmdSender.SendStr(this.textBox_Send.Text);
        }

        private void button_SendClear_Click(object sender, EventArgs e)
        {
            this.textBox_Send.Text = String.Empty;
        }

        private void textBox_Send_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.KeyCode == (Keys.Control & Keys.Enter))//组合键换行
            {
                textBox_Send.Text += Environment.NewLine;
            }
        }

        private void textBox_Send_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;//Enter按键不换行
                textBox_Send.Focus();
                textBox_Send.SelectAll();
                //发送字符串
                this.cmdSender.Serial.WriteData(this.textBox_Send.Text);
            }
        }

        private void button_Dist0_Click(object sender, EventArgs e)
        {
            this.cmdSender.GetDistance(0);
        }

        /// <summary>
        /// 鼠标悬停时，显示所选取字符串的十六进制值
        /// </summary>
        private void textBox_Receive_MouseHover(object sender, EventArgs e)
        {
            string tgt = this.textBox_Receive.SelectedText;
            this.toolTip1.Show(HexConvert.StringToHexString(tgt), this.textBox_Receive);
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            //测试窗口消费接收到的图片
            /*if (!this.imgProducer.IsAlive)
            {
                this.button_Connect_Click(sender, e);
                this.button_SendPin1_Click(sender, e);
                this.cmdSender.ControlImgSender(this.imgPort, "^");
                //启动获取图像线程
                this.imgProducer = new Thread(() =>
                {
                    this.imgReciever.ProduceImgAfterRecv("127.0.0.1", 36888);
                });
                ImgReceiver.NeedStop = false;
                imgProducer.IsBackground = true;
                imgProducer.Start();

                ImgReceiver.Mode = 0;
                this.imgForm = new ImgForm();
                this.imgForm.Show();
                this.imgForm.ConsumeImg();
            }*/

            //测试本机ImgSender发送图片，本机在对应端口接收图片
            //this.imgReciever.ProduceImgAfterRecv("127.0.0.1", int.Parse(this.imgPort));

            //测试：小车连接360wifi并使用TCP连接电脑172.28.32.1，看是否被172.28.32.1拒绝
            //若使用127.0.0.1监听的话，本机监听的是202.193.9.83，并不是360wifi的地址
            //由于小车连接的是360wifi网关，似乎连不到上层设备，导致小车链接不到127.0.0.1即
            //202.193.9.83的端口。所以这里说明客户端应该监听正确的地址，否则连接不上
            //this.imgReciever.ProduceImgAfterRecv("127.0.0.1", int.Parse(this.imgPort));//wrong
            this.imgReciever.ProduceImgAfterRecv("172.28.32.1", int.Parse(this.imgPort));//right

            //测试本机连接小车上的服务器并接收图片
            /*this.cmdSender.Net.ConnectServer("172.28.32.3", int.Parse(this.textBox_PORT.Text));
            this.button_SendPin1_Click(sender, e);
            this.cmdSender.ControlImgSender(this.imgPort, "&");
            //测试小车发送图片后，此处接收图片
            this.imgReciever.ProduceImgAfterRecv("127.0.0.1", int.Parse(this.imgPort));*/
            
        }

        private void button_SendPin1_Click(object sender, EventArgs e)
        {
            this.cmdSender.SendStr("master.123456");
            this.cmdSender.SendCmd(new byte[1] { 0xfc });
        }

        private void button_SendPin2_Click(object sender, EventArgs e)
        {
            this.cmdSender.SendStr("guess.111111");
            this.cmdSender.SendCmd(new byte[1] { 0xfc });
        }

        /// <summary>
        /// 开启获取图像
        /// </summary>
        private void button_FetchImg_Click(object sender, EventArgs e)
        {
            if (!this.imgProducer.IsAlive)
            {               
                //启动接收图像线程
                this.imgProducer = new Thread(() =>
                {
                    //this.imgReciever.ProduceImgAfterRecv("127.0.0.1", int.Parse(this.imgPort));
                    this.imgReciever.ProduceImgAfterRecv("172.28.32.1", int.Parse(this.imgPort));
                });
                ImgReceiver.NeedStop = false;
                imgProducer.IsBackground = true;
                imgProducer.Start();

                System.Threading.Thread.Sleep(300);

                //注意，带宽不足时，第二个摄像可能会没数据
                ImgReceiver.Mode = this.comboBox_Mode.SelectedIndex;
                switch (ImgReceiver.Mode)
                {
                    case 0: this.cmdSender.ControlImgSender(this.imgPort, "^"); break;
                    case 1: this.cmdSender.ControlImgSender(this.imgPort, "&"); break;   //对应编号为1
                    case 2: this.cmdSender.ControlImgSender(this.imgPort, "*"); break;   //对应编号为0
                }

                this.singleImgForm = new SingleImgForm();
                this.singleImgForm.Show();
                if (!this.singleImgForm.imgConsumer.IsAlive)
                    this.singleImgForm.imgConsumer.Start();


            }
            else 
            {
                MessageBox.Show("图像已在获取并接收", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// 关闭获取图像
        /// </summary>
        private void button_StopImg_Click(object sender, EventArgs e)
        {
            this.cmdSender.ControlImgSender(this.imgPort, "$");
            ImgReceiver.NeedStop = true;
            ImgReceiver.Imgtotal = 0;
            ImgReceiver.TellMainForm = "";
            this.singleImgForm.Hide();
        }

        private void button_QueryMode_Click(object sender, EventArgs e)
        {
            this.cmdSender.ControlImgSender(this.imgPort, "?");
        }

        private void button_ChangeMode_Click(object sender, EventArgs e)
        {
            string text = String.Format("{0,-5}", this.textBox_FPS.Text);
            this.cmdSender.ControlImgSender(text, this.comboBox_Mode.SelectedItem.ToString());
            ImgReceiver.Mode = this.comboBox_Mode.SelectedIndex;
        }

        

        


        

        
    }
}
