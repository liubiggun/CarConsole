using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace CarConsole.ClassLib
{
    /// <summary>
    /// 小车控制的命令行客户端
    /// </summary>
    class CommandClient
    {
        TcpClient tcpClient;

        /// <summary>
        /// 是否连接到客户端
        /// </summary>
        public bool IsConnect
        {
            get { return this.tcpClient.Connected; }
        }

        public delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);
        /// <summary>
        /// 接收数据处理事件(comPort.DataReceived 的后续的处理)
        /// </summary>
        public event DataReceivedEventHandler DataReceived;

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void ConnectServer(string host, int port)
        {
            this.tcpClient = new TcpClient();
            try
            {
                this.tcpClient.Connect(host, port);
            }
            catch (Exception)
            {
                MessageBox.Show("连接失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        public void DisConnect()
        {
            if (this.IsConnect)
            {
                this.tcpClient.Close();
            }
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="msg"></param>
        public void WriteData(byte[] msg)
        {
            if (tcpClient.Connected)
                tcpClient.Client.Send(msg);
        }

        /// <summary>
        /// 获取服务器返回的信息
        /// </summary>
        public void ReadData()
        {
            try
            {
                if (tcpClient.Connected)
                {
                    ReceiveSate state = new ReceiveSate();
                    tcpClient.Client.BeginReceive(state.datas, 0, 128, SocketFlags.None, OnReceiveComplete, state);
                }
            }
            catch (Exception)
            {
                
            }            
        }

        private void OnReceiveComplete(IAsyncResult asyncResult)
        {
            ReceiveSate state = (ReceiveSate)asyncResult.AsyncState;

            //触发事件
            if (DataReceived != null)
            {
                DataReceived(this, new DataReceivedEventArgs(Encoding.ASCII.GetString(state.datas)));
            }
            try
            {
                tcpClient.Client.EndReceive(asyncResult);  
            }
            catch (Exception)
            {                
                
            }
                     
        }

        /// <summary>
        /// 传给异步回调函数的信息
        /// </summary>
        internal class ReceiveSate
        { 
            public byte[] datas=new byte[128];
        }
    }
}
