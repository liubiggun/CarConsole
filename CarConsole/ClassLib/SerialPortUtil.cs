using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace CarConsole.ClassLib
{
    /// <summary>
    /// 串口开发辅助类
    /// </summary>
    public class SerialPortUtil
    {
        /// <summary>
        /// 接收事件是否有效 false表示有效
        /// </summary>
        public bool ReceiveEventFlag = false;
        /// <summary>
        /// 结束符字节  
        /// </summary>
        public byte EndByte = 0x0A;//0x0A \n   0x23  "#";
        /// <summary>
        /// 串口数据位列表（5,6,7,8）
        /// </summary>
        public List<string> SerialPortDatabits = new List<string>(new string[] { "5", "6", "7", "8" });
        /// <summary>
        /// 串口波特率列表。
        /// 75,110,150,300,600,1200,2400,4800,9600,14400,19200,28800,38400,56000,57600,
        /// 115200,128000,230400,256000
        /// </summary>
        public List<string> SerialPortBaudRates = new List<string>(new string[] { "75", "110", "150", "300", "600", "1200", "2400", "4800", "9600",
            "14400", "19200", "28800", "38400", "56000", "57600", "115200", "128000", "230400", "256000" });


        public delegate void UtilSerialDataReceivedEventHandler(object sender, DataReceivedEventArgs e);
        /// <summary>
        /// 接收数据处理事件(comPort.DataReceived 的后续的处理)
        /// </summary>
        public event UtilSerialDataReceivedEventHandler DataReceived;
        public event SerialErrorReceivedEventHandler ErrorReceived;

        #region 变量属性
        private string _portName = "COM1";//串口号，默认COM1
        private string _baudRate = "9600";//波特率
        private Parity _parity = Parity.None;//校验位
        private StopBits _stopBits = StopBits.One;//停止位
        private string _dataBits = "8";//数据位

        private SerialPort comPort = new SerialPort();

        /// <summary>
        /// 串口号
        /// </summary>
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public string BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public Parity Parity
        {
            get { return _parity; }
            set { _parity = value; } 
        }

        /// <summary>
        /// 数据位
        /// </summary>
        public string DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        #endregion

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="baud">波特率</param>
        /// <param name="par">奇偶校验位</param>
        /// <param name="sBits">停止位</param>
        /// <param name="dBits">数据位</param>
        /// <param name="name">串口号</param>
        public SerialPortUtil(string name, string baud, Parity par, string dBits, StopBits sBits)
        {
            _portName = name;
            _baudRate = baud;
            _parity = par;
            _dataBits = dBits;
            _stopBits = sBits;

            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            comPort.ErrorReceived += new SerialErrorReceivedEventHandler(comPort_ErrorReceived);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SerialPortUtil()
        {
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            comPort.ErrorReceived += new SerialErrorReceivedEventHandler(comPort_ErrorReceived);
        } 


        /// <summary>
        /// 端口是否已经打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return comPort.IsOpen;
            }
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <returns></returns>
        public void OpenPort()
        {
            if (comPort.IsOpen) comPort.Close();

            comPort.PortName = _portName;
            comPort.BaudRate = int.Parse(_baudRate);
            comPort.Parity = _parity;
            comPort.DataBits = int.Parse(_dataBits);
            comPort.StopBits = _stopBits;

            try
            {
                comPort.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("打开串口失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);              
            }
            
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        public void ClosePort()
        {
            if (comPort.IsOpen) comPort.Close();
        }

        /// <summary>
        /// 丢弃来自串行驱动程序的接收和发送缓冲区的数据
        /// </summary>
        public void DiscardBuffer()
        {
            comPort.DiscardInBuffer();
            comPort.DiscardOutBuffer();
        }

        /// <summary>
        /// 数据接收处理函数
        /// </summary>
        void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //禁止接收事件时直接退出
            if (ReceiveEventFlag) return;

            #region 根据结束字节来判断是否全部获取完成
            List<byte> _byteData = new List<byte>();
            bool found = false;//是否检测到结束符号
            while (comPort.BytesToRead > 0 || !found)
            {
                if (!comPort.IsOpen) break;

                byte[] readBuffer = new byte[comPort.ReadBufferSize + 1];
                int count = comPort.Read(readBuffer, 0, comPort.ReadBufferSize);
                for (int i = 0; i < count; i++)
                {
                    _byteData.Add(readBuffer[i]);

                    if (readBuffer[i] == EndByte)
                    {
                        found = true;
                    }
                }
            } 
            #endregion
            
            //字符转换
            string readString = System.Text.Encoding.ASCII.GetString(_byteData.ToArray(), 0, _byteData.Count);
            
            //触发整条记录的后续处理
            if (DataReceived != null)
            {
                DataReceived(this, new DataReceivedEventArgs(readString));//触发事件,在外部需要些触发事件时进行的函数并订阅
            }
        }

        /// <summary>
        /// 数据接收错误处理函数
        /// </summary>
        void comPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (ErrorReceived != null)
            {
                ErrorReceived(sender, e);
            }
        }

        #region 数据写入操作

        /// <summary>
        /// 写入数据（类SerialPort的方法Write(string text)默认将text编码用ascall编码方式，使用0到127，其他的都将用0x63 即？表示）
        /// </summary>
        /// <param name="msg"></param>
        public void WriteData(string msg)
        {
            if (!(comPort.IsOpen)) comPort.Open();
            
            comPort.Write(msg);
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="msg">写入端口的字节数组</param>
        public void WriteData(byte[] msg)
        {
            if (!(comPort.IsOpen)) comPort.Open();

            comPort.Write(msg, 0, msg.Length);
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="msg">包含要写入端口的字节数组</param>
        /// <param name="offset">参数从0字节开始的字节偏移量</param>
        /// <param name="count">要写入的字节数</param>
        public void WriteData(byte[] msg, int offset, int count)
        {
            if (!(comPort.IsOpen)) comPort.Open();

            comPort.Write(msg, offset, count);
        }

        /// <summary>
        /// 发送串口命令
        /// </summary>
        /// <param name="SendData">发送数据</param>
        /// <param name="ReceiveData">接收数据</param>
        /// <param name="Overtime">重复次数</param>
        /// <returns></returns>
        public int SendCommand(byte[] SendData, ref  byte[] ReceiveData, int Overtime)
        {
            if (!(comPort.IsOpen)) comPort.Open();

            ReceiveEventFlag = true;        //关闭接收事件
            comPort.DiscardInBuffer();      //清空接收缓冲区                 
            comPort.Write(SendData, 0, SendData.Length);
            
            int num = 0, ret = 0;
            while (num++ < Overtime)
            {
                if (comPort.BytesToRead >= ReceiveData.Length) break;
                System.Threading.Thread.Sleep(1);
            }

            if (comPort.BytesToRead >= ReceiveData.Length)
            {
                ret = comPort.Read(ReceiveData, 0, ReceiveData.Length);
            }

            ReceiveEventFlag = false;       //打开事件
            return ret;
        }

        #endregion

        #region 设置combobox

        /// <summary>
        /// 若窗体需要使用combobox来设定串口时调用该函数
        /// </summary>
        /// <param name="comboBoxPort"></param>
        /// <param name="comboBoxBaudRate"></param>
        /// <param name="comboBoxParity"></param>
        /// <param name="comboBoxDataBits"></param>
        /// <param name="comboBoxStopBit"></param>
        public void SetComboBox(ref ComboBox comboBoxPort, ref ComboBox comboBoxBaudRate, ref ComboBox comboBoxParity,
            ref ComboBox comboBoxDataBits, ref ComboBox comboBoxStopBit)
        {
            comboBoxPort.Items.Clear();
            comboBoxBaudRate.Items.Clear();
            comboBoxParity.Items.Clear();
            comboBoxDataBits.Items.Clear();
            comboBoxStopBit.Items.Clear();
           
            comboBoxPort.Items.AddRange(SerialPort.GetPortNames());
            comboBoxBaudRate.Items.AddRange(SerialPortBaudRates.ToArray());
            comboBoxParity.Items.AddRange(new string[] { "无", "奇校验", "偶校验", "保留1", "保留0" });
            comboBoxDataBits.Items.AddRange(SerialPortDatabits.ToArray());
            comboBoxStopBit.Items.AddRange(new string[] { "1", "1.5", "2" });

            comboBoxPort.SelectedIndex = 0;
            comboBoxBaudRate.SelectedIndex = 15;
            comboBoxParity.SelectedIndex = 0;
            comboBoxDataBits.SelectedIndex = 3;
            comboBoxStopBit.SelectedIndex = 0;

            _portName = comboBoxPort.SelectedItem.ToString();
            _baudRate = comboBoxBaudRate.SelectedItem.ToString();
            switch (comboBoxParity.SelectedItem.ToString())
            {
                case "无": _parity = Parity.None; break;
                case "奇校验": _parity = Parity.Odd; break;
                case "偶校验": _parity = Parity.Even; break;
                case "保留1": _parity = Parity.Mark; break;
                case "保留0": _parity = Parity.Space; break;
                default:
                    break;
            }
            _dataBits = comboBoxDataBits.SelectedItem.ToString();
            switch (comboBoxStopBit.SelectedItem.ToString())
            {
                case "1": _stopBits = StopBits.One; break;
                case "1.5": _stopBits = StopBits.OnePointFive; break;
                case "2": _stopBits = StopBits.Two; break;
                default:
                    break;
            }
            

            comboBoxPort.SelectedIndexChanged += new EventHandler(this.comboBoxPort_SelectedIndexChanged);
            comboBoxBaudRate.SelectedIndexChanged += new EventHandler(this.comboBoxBaudRate_SelectedIndexChanged);
            comboBoxParity.SelectedIndexChanged += new EventHandler(this.comboBoxParity_SelectedIndexChanged);
            comboBoxDataBits.SelectedIndexChanged += new EventHandler(this.comboBoxDataBits_SelectedIndexChanged);
            comboBoxStopBit.SelectedIndexChanged += new EventHandler(this.comboBoxStopBit_SelectedIndexChanged);
        }

        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            _portName = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void comboBoxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _baudRate = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void comboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).SelectedItem.ToString())
            {
                case "无": _parity = Parity.None; break;
                case "奇校验": _parity = Parity.Odd; break;
                case "偶校验": _parity = Parity.Even; break;
                case "保留1": _parity = Parity.Mark; break;
                case "保留0": _parity = Parity.Space; break;
                default:
                    break;
            }
        }

        private void comboBoxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dataBits = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void comboBoxStopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).SelectedItem.ToString())
            {
                case "1": _stopBits = StopBits.One; break;
                case "1.5": _stopBits = StopBits.OnePointFive; break;
                case "2": _stopBits = StopBits.Two; break;
                default:
                    break;
            }
        }

        #endregion

        #region 格式转换
        /// <summary>
        /// 转换十六进制字符串到字节数组
        /// </summary>
        /// <param name="msg">待转换字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");//移除空格

            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                //convert each set of 2 characters to a byte and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            }

            return comBuffer;
        }

        /// <summary>
        /// 转换字节数组到十六进制字符串
        /// </summary>
        /// <param name="comByte">待转换字节数组</param>
        /// <returns>十六进制字符串</returns>
        public static string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            }

            return builder.ToString().ToUpper();
        }
        #endregion

        /// <summary>
        /// 检查端口名称是否存在
        /// </summary>
        /// <param name="port_name"></param>
        /// <returns></returns>
        public static bool Exists(string port_name)
        {
            foreach (string port in SerialPort.GetPortNames()) if (port == port_name) return true;
            return false;
        }

        /// <summary>
        /// 格式化端口相关属性
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string Format(SerialPort port)
        {
            return String.Format("{0} ({1},{2},{3},{4},{5})", 
                port.PortName, port.BaudRate, port.DataBits, port.StopBits, port.Parity, port.Handshake);
        }
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public string DataReceived;
        public DataReceivedEventArgs(string dataReceived)
        {
            this.DataReceived = dataReceived;
        }
    }
    
    


    
    
    

    

    
}
