using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConsole.ClassLib;
using CarConsole.Dev;

namespace CarConsole.Util
{
    class CmdSender
    {
        /// <summary>
        /// 串口工具类，初始化的一部分工作（绑定控件，订阅接收后的事件）需要在窗体界面完成
        /// </summary>
        public SerialPortUtil Serial;

        /// <summary>
        /// 小车命令行客户端工具类
        /// </summary>
        public CommandClient Net;

        public DualMotors DMControl = new DualMotors();
        /// <summary>
        /// 指令头字节
        /// </summary>
        byte[] HeadBytes;
        /// <summary>
        /// 指令结束符
        /// </summary>
        byte EndByte;
        /// <summary>
        /// 指令头
        /// </summary>
        string HeadStr;
        /// <summary>
        /// 指令结束符字符串
        /// </summary>
        string EndStr;

        public CmdSender ()
	    {
            this.Serial = new SerialPortUtil();
            this.Net = new CommandClient();
            this.HeadBytes = new byte[2] { 0x66, 0xaa };
            this.EndByte = 0xfc;
            this.HeadStr = Encoding.UTF8.GetString(HeadBytes);
            this.EndStr = ((char)EndByte).ToString();           
	    }

        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="msg"></param>
        public void SendStr(string msg)
        {
            if (this.Serial.IsOpen)
                this.Serial.WriteData(msg);
            else if (this.Net.IsConnect)
            {
                this.Net.WriteData(Encoding.ASCII.GetBytes(msg));
                this.Net.ReadData();
            }
                
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        public void SendCmd(byte[] msg)
        {
            if (this.Serial.IsOpen)
                this.Serial.WriteData(msg);
            else if (this.Net.IsConnect)
            {
                this.Net.WriteData(msg);
                this.Net.ReadData();
            }
        }

        /// <summary>
        /// 获取超声波测距的距离
        /// </summary>
        /// <param name="index">第几个超声波模块</param>
        public void GetDistance(int index)
        {
            //   字头       栈长度    命令字          数据        校验码    结束字节
            //0x66  0xaa     0x01      0x01           "0"        0xcc     0xfc   
            /*
            string cmd = this.HeadStr +  + index;
            cmd += this.Char_CheckSum(cmd.Substring(2, 3));
            cmd += EndStr;
            this.Serial.WriteData(cmd);
            */

            //栈长度 + 命令字 + 数据(用string来构造，因为其中没有byte大于127的字符）
            //字头字尾和校验码用字节传输
            List<byte> cmd = new List<byte>(this.HeadBytes);
            string data = Encoding.ASCII.GetString(new byte[2] { 0x01, 0x01 }) + index;

            cmd.AddRange(Encoding.ASCII.GetBytes(data));
            cmd.AddRange(new byte[2] { this.Char_CheckSum(data), this.EndByte });

            this.SendCmd(cmd.ToArray());
            
        }

        /// <summary>
        /// 向下位机发送电机控制指令
        /// </summary>
        public void ControlMotor()
        {
            //   字头       栈长度    命令字          数据        校验码    结束字节
            //0x66  0xaa     0x08      0x02        "+100-100"    0xcc     0xfc 

            //栈长度 + 命令字 + 数据(用string来构造，因为其中没有byte大于127的字符）
            //字头字尾和校验码用字节传输
            List<byte> cmd = new List<byte>(this.HeadBytes);
            string data = Encoding.ASCII.GetString(new byte[2] { 0x08, 0x02 });
            string pwd = String.Empty;

            if (DMControl.Motors[0].PwdValue >= 0)
            {
                data += "+";
                pwd = DMControl.Motors[0].PwdValue.ToString();
                switch (pwd.Length)
                {
                    case 1: data += "00" + pwd;
                        break;
                    case 2: data += "0" + pwd;
                        break;
                    case 3: data += pwd;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                data += "-";
                pwd = DMControl.Motors[0].PwdValue.ToString().Substring(1);
                switch (pwd.Length)
                {
                    case 1: data += "00" + pwd;
                        break;
                    case 2: data += "0" + pwd;
                        break;
                    case 3: data += pwd;
                        break;
                    default:
                        break;
                }
            }

            if (DMControl.Motors[1].PwdValue >= 0)
            {
                data += "+";
                pwd = DMControl.Motors[1].PwdValue.ToString();
                switch (pwd.Length)
                {
                    case 1: data += "00" + pwd;
                        break;
                    case 2: data += "0" + pwd;
                        break;
                    case 3: data += pwd;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                data += "-";
                pwd = DMControl.Motors[1].PwdValue.ToString().Substring(1);
                switch (pwd.Length)
                {
                    case 1: data += "00" + pwd;
                        break;
                    case 2: data += "0" + pwd;
                        break;
                    case 3: data += pwd;
                        break;
                    default:
                        break;
                }
            }
          
            byte[] e = new byte[2] { this.Char_CheckSum(data), this.EndByte };

            cmd.AddRange(Encoding.ASCII.GetBytes(data));
            cmd.AddRange(new byte[2] { this.Char_CheckSum(data), this.EndByte });

            this.SendCmd(cmd.ToArray());
        }

        /// <summary>
        /// 控制图像输出模式
        /// </summary>
        /// <param name="tag">控制获取图像数据，第一个参数是客户端要接收数据的TCP端口,
        /// 数字字符串长度不足5则补零，^打开图像传输，$关闭图像传输，0是获取双目数据，
        /// 1是左边摄像头，2则右边？查询状态，服务器返回客户端0 1 2 或 $</param>
        public void ControlImgSender(string imgport, string tag)
        { 
            //   字头       栈长度    命令字          数据        校验码    结束字节
            //0x66  0xaa     0x07      0xff         "65535;0"    0xcc     0xfc   
            
            List<byte> cmd = new List<byte>(this.HeadBytes);
            string data = Encoding.ASCII.GetString(new byte[2] { 0x07, 0x7f }) + imgport + ';' + tag;

            cmd.AddRange(Encoding.ASCII.GetBytes(data));
            cmd.AddRange(new byte[2] { this.Char_CheckSum(data), this.EndByte });

            this.SendCmd(cmd.ToArray());
        }

        /// <summary>
        /// 计算校验和(数据字符串中每个字符转换成有符号字节进行求和，运算过程中按有符号字节存储，结果返回相应的无符号字节表示)
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <returns>字节类型</returns>
        private byte Char_CheckSum(string data)
        {
            sbyte rs = 0;
            sbyte[] datas = Array.ConvertAll(Encoding.ASCII.GetBytes(data), (a) => (sbyte)a);//byte数组转换sbyte数组
            for (int i = 0; i < datas.Length; i++)
            {
                rs += datas[i];
            }
            return (byte)rs;
        }

        /// <summary>
        /// 计算校验和(数据字符串中每个字符转换成有符号字节进行求和，运算过程中按有符号字节存储，结果返回相应的无符号字节表示)
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <returns>字节类型</returns>
        private byte Char_CheckSum(byte[] data)
        {
            sbyte rs = 0;
            sbyte[] datas = Array.ConvertAll(data, (a) => (sbyte)a);//byte数组转换sbyte数组
            for (int i = 0; i < datas.Length; i++)
            {
                rs += datas[i];
            }
            return (byte)rs;
        }
    }
}
