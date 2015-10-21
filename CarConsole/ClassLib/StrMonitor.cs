using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConsole.ClassLib
{
    class StrMonitor
    {
        private string test;

        public string Text
        {
            get { return this.test; }
            set
            {
                if (this.test != value)
                    this.OnChanged(this, new TargetChangedEventArgs(value));//值改变则触发
                this.test = value;              
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="target"></param>
        public StrMonitor(string target)
        {
            this.test = target;
        }

        public delegate void ChangedEventHandler(object sender, TargetChangedEventArgs e);

        public event ChangedEventHandler ValueChanged;

        public class TargetChangedEventArgs : EventArgs
        {           
            public TargetChangedEventArgs(string text)
            {
                this.text = text;
            }

            public readonly string text;
        }
    

        protected virtual void OnChanged(object sender, TargetChangedEventArgs e)
        {
            if (this.ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
    }
}
