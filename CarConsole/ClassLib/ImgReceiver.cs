using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Drawing;

namespace CarConsole.ClassLib
{
    class ImgReceiver
    {
        private string host;
        private int port;
        private TcpListener listener;

        /// <summary>
        /// 图片队列
        /// </summary>
        public static ConcurrentQueue<Image> ImgQueue = new ConcurrentQueue<Image>(); 
        /// <summary>
        /// 生产图片循环是否需要结束
        /// </summary>
        public static bool NeedStop = false;
        /// <summary>
        /// 获取图片的模式
        /// </summary>
        public static int Mode = 0;
        /// <summary>
        /// 获取的图片总数
        /// </summary>
        public static int Imgtotal = 0;
        /// <summary>
        /// 图片队列剩下的图片总数
        /// </summary>
        public static int Imgrest = 0;
        /// <summary>
        /// 简单通知主窗体接收图片的状态
        /// </summary>
        public static string TellMainForm = "";

        /// <summary>
        /// 一个生产者函数，接收对端传过来的图像并将其放入队列中
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void ProduceImgAfterRecv(string host, int port)
        {
            this.host = host;
            this.port = port;
            this.listener = new TcpListener(System.Net.IPAddress.Parse(host), port);
            listener.Start();
            List<byte> lenbuffer = new List<byte>(16);
            TellMainForm = "正在监听服务器的连接!";
            TcpClient client = listener.AcceptTcpClient();        //接受客户端的连接，利用client保存连接的客户端   
            TellMainForm = "接收到服务器的连接，开始接收图像!";
            NetworkStream clientStream = client.GetStream();      //获取客户端的流stream
            
            int recvlen = 0;                                      //接收数据的长度
            int len = 0;                                          //当前接收图片的长度
            //clientStream.ReadTimeout = 10;

            //循环接收一帧帧的图片（一应一答接收）
            while (true)
            {
                byte[] temp = new byte[16];
                
                if (NeedStop) break;
                try
                {
                    client.Client.Send(new byte[1] { 0xaa });
                    while (recvlen != 16)
                    {
                        recvlen += clientStream.Read(temp, 0, 16);                      //先读取图片长度
                        lenbuffer.AddRange(temp);
                    }
                }
                catch (System.IO.IOException)                                           //捕获对端关闭异常
                {
                    break;
                }
                recvlen = 0;//清零计数器
                string lenstr = Encoding.ASCII.GetString(lenbuffer.ToArray()).Trim();
                len = int.Parse(lenstr);                                                //图片长度                
                List<byte> imgbuffer = new List<byte>(len);

                try
                {
                    while (recvlen != len)
                    {
                        recvlen += clientStream.Read(temp, 0, 16);                      //读取图片
                        imgbuffer.AddRange(temp);
                    }
                }
                catch (System.IO.IOException)                                           //捕获对端关闭异常
                {
                    break;
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imgbuffer.ToArray());
                Image img = System.Drawing.Bitmap.FromStream(ms);
                ImgQueue.Enqueue(img);
                
                Imgtotal += 1;
                Imgrest = ImgQueue.Count;

                recvlen = 0;//清除接收数据的长度
                len = 0;//清除图片的长度
                lenbuffer.Clear();//清除长度接收寄存器
            }
            //关闭客户端流
            clientStream.Close();
            client.Close();
            this.listener.Stop();
        }

    }
}
