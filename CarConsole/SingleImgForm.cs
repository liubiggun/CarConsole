using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CarConsole.ClassLib;

namespace CarConsole
{
    public partial class SingleImgForm : Form
    {
        public Thread imgConsumer;

        public SingleImgForm()
        {
            InitializeComponent();
        }

        private void SingleImgForm_Load(object sender, EventArgs e)
        {
            this.imgConsumer = new Thread(this.ConsumeImg);
            this.imgConsumer.IsBackground = true;
        }

        /// <summary>
        /// 获取队列中的图片（消费）
        /// </summary>
        public void ConsumeImg()
        {
            bool succeeded;
            Image frame;
            while (true)
            {
                if (ImgReceiver.NeedStop == true && ImgReceiver.ImgQueue.Count == 0)
                    break;
                succeeded = ImgReceiver.ImgQueue.TryDequeue(out frame);
                              
                if (succeeded)
                {                   
                    this.DisplayImg(frame, this.pictureBox1);
                }
            }
        }

        private delegate void delegateDisplayImg(Image img, PictureBox box);
        public void DisplayImg(Image img, PictureBox box)
        {
            try
            {
                if (!box.InvokeRequired)        //如果调用该函数的线程和控件位于同一个线程内
                {
                    box.Image = img;
                }
                else                                        //如果调用该函数的线程和控件不在同一个线程内
                {
                    this.Invoke(new delegateDisplayImg(DisplayImg), img, box);
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
