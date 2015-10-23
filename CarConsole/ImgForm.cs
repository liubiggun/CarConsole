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
using System.Drawing;

namespace CarConsole
{
    public partial class ImgForm : Form
    {
        public Thread imgConsumer;

        public ImgForm()
        {
            InitializeComponent();
        }

        private void ImgForm_Load(object sender, EventArgs e)
        {
            this.imgConsumer = new Thread(this.ConsumeImg);
            this.imgConsumer.IsBackground = true;          
        }

        /// <summary>
        /// 获取队列中的图片（消费）
        /// </summary>
        new public void ConsumeImg()
        {
            bool succeeded;
            Image frame;
            bool temp = true;
            while (true)
            {
                if (ImgReceiver.ImgQueue.Count == 0 && ImgReceiver.NeedStop == true)
                    break;
                succeeded = ImgReceiver.ImgQueue.TryDequeue(out frame);
                if (succeeded)
                {
                    switch (ImgReceiver.Mode)
                    {
                        case 0://双目
                            {
                                if (temp) { this.DisplayImg(frame, this.pictureBox1); temp = !temp; }
                                else { this.DisplayImg(frame, this.pictureBox2); temp = !temp; }
                            }break;
                        case 1:
                            {
                                this.DisplayImg(frame, this.pictureBox1);
                                this.HideImg(this.pictureBox2);
                            }
                            break;
                        case 2:
                            {
                                this.DisplayImg(frame, this.pictureBox2);
                                this.HideImg(this.pictureBox1);
                            }
                            break;
                    }    
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

        private delegate void delegateHideImg(PictureBox box);
        public void HideImg(PictureBox box)
        {
            try
            {
                if (!box.InvokeRequired)        //如果调用该函数的线程和控件位于同一个线程内
                {
                    box.Visible = false;
                    box.Size = new Size(1, 1);
                }
                else                                        //如果调用该函数的线程和控件不在同一个线程内
                {
                    this.Invoke(new delegateHideImg(HideImg), box);
                }
            }
            catch (Exception)
            {

            }
        }

        private void ImgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
