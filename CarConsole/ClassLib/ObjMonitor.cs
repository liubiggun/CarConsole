using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConsole.ClassLib
{
    /// <summary>
    /// 常用数据类型（string int double等）监控事件
    /// </summary>
    class ObjMonitor
    {
        //声明一个委托          
        public delegate void ChangedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// 监控事件
        /// </summary>       
        public event ChangedEventHandler OnValueChange;

        private object target;

        /// <summary>
        /// 监控的数据类型
        /// </summary>
        public object Target
        {
            get
            {
                return target;
            }
            set
            {
                if (target != value)
                {
                    target = value;
                    OnValueChange(this, new EventArgs());//值改变则触发，之后只需要为事件订阅事件函数即可
                }
            }
        }
    }
}
