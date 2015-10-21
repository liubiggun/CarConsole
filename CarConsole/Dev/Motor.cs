using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConsole.Dev
{
    /// <summary>
    /// 电机类
    /// </summary>
    class Motor
    {
        /// <summary>
        /// 电机转速百分比控制
        /// </summary>
        /// <param name="accelerating">是否加速</param>
        /// <param name="step">变化步长</param>
        /// <param name="forward">该参数处理前一刻电机静止时的状况，PerSpeed为0时且accelerating为true时有效。正转加速true，反转加速false</param>
        public void Control(bool accelerating, int step, bool forward = true)
        {
            if (PerSpeed > 0)//正转
            {
                if (accelerating)//加速
                {
                    if (PerSpeed + step > 100)//越界处理
                        PerSpeed = 100;
                    else
                        PerSpeed += step;
                }
                else//减速
                {
                    if (PerSpeed - step < 0)//越界处理
                        PerSpeed = 0;
                    else
                        PerSpeed -= step;
                }
            }
            else if (PerSpeed < 0)//反转
            {
                if (accelerating)//加速
                {
                    if (PerSpeed - step < -100)//越界处理
                        PerSpeed = -100;
                    else
                        PerSpeed -= step;
                }
                else//减速
                {
                    if (PerSpeed + step > 0)//越界处理
                        PerSpeed = 0;
                    else
                        PerSpeed += step;
                }
            }
            else//此时PerSpeed为0，需要根据参数3确定：1正转加速或-1反转加速或者0静止
            {
                if (accelerating)
                {
                    if (forward) PerSpeed = 1;
                    else PerSpeed = -1;
                }
            }
        }

        /// <summary>
        /// PerSpeed监控的事件函数
        /// </summary>
        private void perSpeed_OnVauleChanged(object sender, EventArgs e)
        {
            double per = PerSpeed;
            pwmValue = (int)(per / 100 * maxPwmValue);
        }

        public Motor(int id, int maxPwm)
        {
            this.index = id;
            this.maxPwmValue = maxPwm;
            this.OnValueChange += new ChangedEventHandler(perSpeed_OnVauleChanged);
        }

        //声明一个委托和事件，用来监控perSpeed的改变      
        private delegate void ChangedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// 监控事件
        /// </summary>       
        private event ChangedEventHandler OnValueChange;

        private int index;
        private bool speedChanged;
        private int perSpeed;        
        private int pwmValue;
        private int maxPwmValue;     

        /// <summary>
        /// ID
        /// </summary>
        public int Index
        {
            get { return index; }
        }

        /// <summary>
        /// 速度较之前是否改变,需要对PerSpeed赋值才能触发
        /// </summary>
        public bool SpeedChanged 
        { 
            get { return speedChanged; }
        }

        /// <summary>
        /// 百分比转速
        /// </summary>
        public int PerSpeed
        {
            get { return perSpeed; }
            set 
            {
                if (perSpeed != value)
                {
                    speedChanged = true;
                    perSpeed = value;
                    OnValueChange(this, new EventArgs());//值改变则触发，之后只需要为事件订阅事件函数即可
                }
                else speedChanged = false;
            }
        }

        /// <summary>
        /// PWD调速的值（传给下位机）
        /// </summary>
        public int PwdValue
        {
            get { return pwmValue; }
        }

        /// <summary>
        /// PWD调速的最大值
        /// </summary>
        public int MaxPwdValue
        {
            get { return maxPwmValue; }
        }


        
    }

    /// <summary>
    /// 双电机小车控制类
    /// </summary>
    class DualMotors
    {
        /// <summary>
        /// 控制小车（此处控制模仿惯性，小车停止，回正或转向有惯性，但小车移动方向相反时则立刻静止并变向）
        /// </summary>
        /// <param name="cmd">方向按键标识,四位分别对应wsad</param>
        public void Command(byte cmd)
        {
            this.preState = curState;//状态更新
            switch (cmd)
            {
                case 0://0000 静止
                    {
                        switch (preState)//根据前一刻确定减速的的快慢
                        {
                            case State.Stop: Stop();
                                break;
                            case State.Forward: DualDec(8, 8);
                                break;
                            case State.ForwardAndTurnRight: DualDec(8, 5);//转弯
                                break;
                            case State.ForwardAndTurnLeft: DualDec(5, 8);
                                break;
                            case State.Back: DualDec(8, 8);
                                break;
                            case State.BackAndTurnRight: DualDec(8, 5);//转弯
                                break;
                            case State.BackAndTurnLeft: DualDec(5, 8);
                                break;
                            case State.ForwardRightCircle: DualDec(8, 5);//打转减速的方式与转弯相同
                                break;
                            case State.ForwardLeftCircle: DualDec(5, 8);
                                break;
                            case State.BackRightCircle: DualDec(8, 5);
                                break;
                            case State.BackdLeftCircle: DualDec(5, 8);
                                break;
                            case State.CWCircle: DualDec(8, 8);
                                break;
                            case State.CCWCircle: DualDec(8, 8);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case 1://0001 右,(仅仅按左右键，不按前进和后退时，两个电机的速度应该下降，但差值应逐步增大)
                    {
                        switch (preState)
                        {
                            case State.Stop: Stop();//前一刻静止，则此刻仍静止
                                break;
                            case State.Forward: Dec(2, 5);//前一刻前进，则此刻前进减速，右向转弯
                                break;
                            case State.ForwardAndTurnRight: Dec(2, 5);//前一刻前进右转弯，则此刻前进减速，右向转弯
                                break;
                            case State.ForwardAndTurnLeft: Dec(2, 6);//前一刻前进左转弯，则此刻前进减速，右向转弯，右电机减速多一些，使回正效果明显
                                break;
                            case State.Back: Dec(2, 5);//前一刻后退，则此刻后退减速，右向转弯
                                break;
                            case State.BackAndTurnRight: Dec(2, 5);//前一刻后退，则此刻后退减速，右向转弯
                                break;
                            case State.BackAndTurnLeft: Dec(2, 6);//前一刻后退，则此刻后退减速，右向转弯,右电机减速多一些，使回正效果明显
                                break;
                            case State.ForwardRightCircle: Dec(2, 5);//前一刻前进右打转，则此刻前进减速，右向转弯
                                break;
                            case State.ForwardLeftCircle: Dec(2, 6);//前一刻前进左打转，则此刻前进减速，右向转弯,右电机减速多一些，使回正效果明显
                                break;
                            case State.BackRightCircle: Dec(2, 5);//前一刻后退右打转，则此刻后退减速，右向转弯
                                break;
                            case State.BackdLeftCircle: Dec(2, 6);//前一刻后退左打转，则此刻后退减速，右向转弯,右电机减速多一些，使回正效果明显
                                break;
                            case State.CWCircle: Dec(2, 5);//前一刻原地顺时针打转，则此时减速，右向转弯
                                break;
                            case State.CCWCircle: Dec(2, 5);//前一刻原地逆时针打转，则此时减速，右向转弯
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case 2://0010 左
                    {
                        switch (preState)
                        {
                            case State.Stop: Stop();//前一刻静止，则此刻仍静止
                                break;
                            case State.Forward: Dec(5, 2);//前一刻前进，则此刻前进减速，左向转弯
                                break;
                            case State.ForwardAndTurnRight: Dec(6, 2);//前一刻前进右转弯，则此刻前进减速，左向转弯，左电机减速多一些，使回正效果明显
                                break;
                            case State.ForwardAndTurnLeft: Dec(5, 2);//前一刻前进左转弯，则此刻前进减速，左向转弯
                                break;
                            case State.Back: Dec(5, 2);//前一刻后退，则此刻后退减速，左向转弯
                                break;
                            case State.BackAndTurnRight: Dec(6, 2);//前一刻后退，则此刻后退减速，左向转弯,左电机减速多一些，使回正效果明显
                                break;
                            case State.BackAndTurnLeft: Dec(5, 2);//前一刻后退，则此刻后退减速，左向转弯
                                break;
                            case State.ForwardRightCircle: Dec(6, 2);//前一刻前进右打转，则此刻前进减速，左向转弯,左电机减速多一些，使回正效果明显
                                break;
                            case State.ForwardLeftCircle: Dec(5, 2);//前一刻前进左打转，则此刻前进减速，左向转弯
                                break;
                            case State.BackRightCircle: Dec(6, 2);//前一刻后退右打转，则此刻后退减速，右向转弯,左电机减速多一些，使回正效果明显
                                break;
                            case State.BackdLeftCircle: Dec(5, 2);//前一刻后退左打转，则此刻后退减速，左向转弯
                                break;
                            case State.CWCircle: Dec(5, 2);//前一刻原地顺时针打转，则此时减速，左向转弯
                                break;
                            case State.CCWCircle: Dec(5, 2);//前一刻原地逆时针打转，则此时减速，左向转弯
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case 3://0011 左右 按键冲突，视为静止
                    {
                        goto case 0;
                    }
                case 4://0100 下
                    {

                        switch (preState)
                        {
                            case State.Stop: DualAcc(3, 3, false); //前一刻停止，则此刻直接双轮反向加速
                                break;
                            case State.Forward: Stop();//前一刻前进，则此刻立刻停止后直接双轮反向加速
                                break;
                            case State.ForwardAndTurnRight: Stop();//前一刻前进向右，则此刻立刻停止后直接双轮反向加速
                                break;
                            case State.ForwardAndTurnLeft: Stop();//前一刻前进向左，则此刻立刻停止后直接双轮反向加速
                                break;
                            case State.Back: DualAcc(3, 3, false); //前一刻后退，则此刻直接双轮反向加速
                                break;
                            case State.BackAndTurnRight: DualAcc(3, 5, false);//前一刻后退向右转，则此刻双轮反向加速，但右电机加速应更快，需回正
                                break;
                            case State.BackAndTurnLeft: DualAcc(5, 3, false);//前一刻后退向左转，则此刻双轮反向加速，但左电机加速应更快，需回正
                                break;
                            case State.ForwardRightCircle: Stop();//前一刻前进右打转，则此刻立刻停止后直接双轮反向加速
                                break;
                            case State.ForwardLeftCircle: Stop();//前一刻前进左打转，则此刻立刻停止后直接双轮反向加速
                                break;
                            case State.BackRightCircle: motors[1].PerSpeed = 0;//前一刻后退右打转，左电机必定反向满速，右电机正向，则此刻立刻归零右电机
                                break;
                            case State.BackdLeftCircle: motors[0].PerSpeed = 0;//前一刻后退左打转，右电机必定反向满速，左电机正向，则此刻立刻归零左电机
                                break;
                            case State.CWCircle: Stop();//前一刻顺时针原地打转，则此刻立刻停止后直接双轮反向加速
                                break;
                            case State.CCWCircle: Stop();//前一刻逆时针原地打转，则此刻立刻停止后直接双轮反向加速
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case 5://0101 下右
                    {
                        switch (preState)
                        {
                            case State.Stop: Acc(5, 3, false);//前一刻静止，则此刻立刻反向加速，但左电机加的较快，状态将变为后退向右
                                break;
                            case State.Forward: Stop();
                                break;
                            case State.ForwardAndTurnRight: Stop();
                                break;
                            case State.ForwardAndTurnLeft: Stop();
                                break;
                            case State.Back://前一刻后退,下一刻状态将变为后退向右
                                {
                                    if (motors[0].PerSpeed == -100)//若已满速
                                        motors[1].Control(false, 3);//电机不能再加速，只需要右电机减速
                                    else
                                        Acc(5, 3, false);
                                }
                                break;
                            case State.BackAndTurnRight:
                                {
                                    if (motors[0].PerSpeed == -100)//若已满速
                                    {
                                        //左电机不能再加速，只需要右电机减速(需要考虑为0的状况)
                                        if (motors[1].PerSpeed < 0)
                                            motors[1].Control(false, 5);
                                        else if (motors[1].PerSpeed == 0)
                                            motors[1].Control(true, 5, true);//处理PerSpeed为0的情况，令状态进入BackRightCircle
                                    }
                                    else
                                        Acc(5, 2, false);
                                }
                                break;
                            case State.BackAndTurnLeft:
                                {
                                    if (motors[1].PerSpeed == -100)//若已满速
                                        motors[0].Control(true, 7, false);//右电机不能再加速，只需要左电机加速(第三个参数处理PerSpeed为0的状况,且回正速度应更快)
                                    else
                                        Acc(6, 3, false);
                                }
                                break;
                            case State.ForwardRightCircle: Stop();
                                break;
                            case State.ForwardLeftCircle: Stop();
                                break;
                            case State.BackRightCircle: motors[1].Control(true, 5);//已进入后退向右打转状态，此时左电机一定是反向满速状态，右电机正向，只需加速右电机
                                break;
                            case State.BackdLeftCircle: Stop();
                                break;
                            case State.CWCircle: Stop();
                                break;
                            case State.CCWCircle: //BackRightCircle状态持续到两个电机都满速则进入该状态
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case 6://0110 下左
                    {
                        switch (preState)
                        {
                            case State.Stop: Acc(3, 5, false);//前一刻静止，则此刻立刻反向加速，但右电机加的较快，状态将变为后退向左
                                break;
                            case State.Forward: Stop();
                                break;
                            case State.ForwardAndTurnRight: Stop();
                                break;
                            case State.ForwardAndTurnLeft: Stop();
                                break;
                            case State.Back://前一刻后退,下一刻状态将变为后退向左
                                {
                                    if (motors[0].PerSpeed == -100)//若已满速
                                        motors[0].Control(false, 3);//电机不能再加速，只需要左电机减速
                                    else
                                        Acc(3, 5, false);
                                }
                                break;
                            case State.BackAndTurnRight:
                                {
                                    if (motors[0].PerSpeed == -100)//若已满速
                                        motors[1].Control(true, 7, false);//左电机不能再加速，只需要右电机加速(第三个参数处理PerSpeed为0的状况,且回正速度应更快)
                                    else
                                        Acc(3, 6, false);
                                }
                                break;
                            case State.BackAndTurnLeft:
                                {
                                    if (motors[1].PerSpeed == -100)//若已满速
                                    {
                                        //右电机不能再加速，只需要左电机减速(需要考虑为0的状况)
                                        if (motors[0].PerSpeed < 0)
                                            motors[0].Control(false, 5);
                                        else if (motors[0].PerSpeed == 0)
                                            motors[0].Control(true, 5, true);//处理PerSpeed为0的情况，令状态进入BackLeftCircle
                                    }
                                    else
                                        Acc(2, 5, false);

                                }
                                break;
                            case State.ForwardRightCircle: Stop();
                                break;
                            case State.ForwardLeftCircle: Stop();
                                break;
                            case State.BackRightCircle: Stop();
                                break;
                            case State.BackdLeftCircle: motors[0].Control(true, 5);//已进入后退向左打转状态，此时右电机一定是反向满速状态，左电机正向，只需加速左电机
                                break;
                            case State.CWCircle: //BackLeftCircle状态持续到两个电机都满速则进入该状态
                                break;
                            case State.CCWCircle: Stop();
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case 7://0111 下左右 按键冲突，视为下
                    {
                        goto case 4;

                    }
                case 8://1000 上
                    {
                        switch (preState)
                        {
                            case State.Stop: DualAcc(3, 3, true); //前一刻停止，则此刻直接双轮正向加速
                                break;
                            case State.Forward: DualAcc(3, 3, true); //前一刻前进，则此刻直接双轮正向加速
                                break;
                            case State.ForwardAndTurnRight: DualAcc(3, 5, true); //前一刻前进向右，则此刻双轮正向加速，但右电机加速应更快，需回正
                                break;
                            case State.ForwardAndTurnLeft: DualAcc(5, 3, true); //前一刻前进向左，则此刻双轮正向加速，但左电机加速应更快，需回正
                                break;
                            case State.Back: Stop(); //前一刻后退，则此刻立刻停止后直接双轮正向加速
                                break;
                            case State.BackAndTurnRight: Stop();//前一刻后退向右转，则此刻立刻停止后直接双轮正向加速
                                break;
                            case State.BackAndTurnLeft: Stop();//前一刻后退向左转，则此刻立刻停止后直接双轮正向加速
                                break;
                            case State.ForwardRightCircle: motors[1].PerSpeed = 0;//前一刻前进右打转，左电机必定正向满速，右电机反向，则此刻立刻归零右电机
                                break;
                            case State.ForwardLeftCircle: motors[0].PerSpeed = 0;//前一刻前进左打转，右电机必定正向满速，左电机反向，则此刻立刻归零左电机
                                break;
                            case State.BackRightCircle: Stop();//前一刻后退右打转，则此刻立刻停止后直接双轮正向加速
                                break;
                            case State.BackdLeftCircle: Stop();//前一刻后退左打转，则此刻立刻停止后直接双轮正向加速
                                break;
                            case State.CWCircle: Stop();//前一刻顺时针原地打转，则此刻立刻停止后直接双轮正向加速
                                break;
                            case State.CCWCircle: Stop();//前一刻逆时针原地打转，则此刻立刻停止后直接双轮正向加速
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case 9://1001 上右
                    {
                        switch (preState)
                        {
                            case State.Stop: Acc(5, 3, true);//前一刻静止，则此刻立刻正向加速，但左电机加的较快，状态将变为前进向右
                                break;
                            case State.Forward: //前一刻前进,下一刻状态将变为前进向右
                                {
                                    if (motors[0].PerSpeed == 100)//若已满速
                                        motors[1].Control(false, 3);//电机不能再加速，只需要右电机减速
                                    else
                                        Acc(5, 3, true);
                                }
                                break;
                            case State.ForwardAndTurnRight:
                                {
                                    if (motors[0].PerSpeed == 100)//若已满速
                                    {
                                        //左电机不能再加速，只需要右电机减速(需要考虑为0的状况)
                                        if (motors[1].PerSpeed > 0)
                                            motors[1].Control(false, 5);
                                        else if (motors[1].PerSpeed == 0)
                                            motors[1].Control(true, 5, false);//处理PerSpeed为0的情况，令状态进入ForwardRightCircle
                                    }
                                    else
                                        Acc(5, 2, true);
                                }
                                break;
                            case State.ForwardAndTurnLeft:
                                {
                                    if (motors[1].PerSpeed == 100)//若已满速
                                        motors[0].Control(true, 7, true);//右电机不能再加速，只需要左电机加速(第三个参数处理PerSpeed为0的状况,且回正速度应更快)
                                    else
                                        Acc(6, 3, true);
                                }
                                break;
                            case State.Back: Stop();
                                break;
                            case State.BackAndTurnRight: Stop();
                                break;
                            case State.BackAndTurnLeft: Stop();
                                break;
                            case State.ForwardRightCircle: motors[1].Control(true, 5);//已进入前进向右打转状态，此时左电机一定是正向满速状态，右电机反向，只需加速右电机
                                break;
                            case State.ForwardLeftCircle: Stop();
                                break;
                            case State.BackRightCircle: Stop();
                                break;
                            case State.BackdLeftCircle: Stop();
                                break;
                            case State.CWCircle: //ForwardRightCircle状态持续到两个电机都满速则进入该状态
                                break;
                            case State.CCWCircle: Stop();
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case 10://1010 上左
                    {
                        switch (preState)
                        {
                            case State.Stop: Acc(3, 5, true);//前一刻静止，则此刻立刻正向加速，但右电机加的较快，状态将变为前进向左
                                break;
                            case State.Forward: //前一刻前进,下一刻状态将变为前进向左
                                {
                                    if (motors[1].PerSpeed == 100)//若已满速
                                        motors[0].Control(false, 3);//电机不能再加速，只需要左电机减速
                                    else
                                        Acc(3, 5, true);
                                }
                                break;
                            case State.ForwardAndTurnRight:
                                {
                                    if (motors[0].PerSpeed == 100)//若已满速
                                        motors[1].Control(true, 7, true);//左电机不能再加速，只需要右电机加速(第三个参数处理PerSpeed为0的状况,且回正速度应更快)
                                    else
                                        Acc(3, 6, true);

                                }
                                break;
                            case State.ForwardAndTurnLeft:
                                {
                                    if (motors[1].PerSpeed == 100)//若已满速
                                    {
                                        //右电机不能再加速，只需要左电机减速(需要考虑为0的状况)
                                        if (motors[0].PerSpeed > 0)
                                            motors[0].Control(false, 5);
                                        else if (motors[0].PerSpeed == 0)
                                            motors[0].Control(true, 5, false);//处理PerSpeed为0的情况，令状态进入ForwardLeftCircle
                                    }
                                    else
                                        Acc(2, 5, true);
                                }
                                break;
                            case State.Back: Stop();
                                break;
                            case State.BackAndTurnRight: Stop();
                                break;
                            case State.BackAndTurnLeft: Stop();
                                break;
                            case State.ForwardRightCircle: Stop();
                                break;
                            case State.ForwardLeftCircle: motors[0].Control(true, 5);//已进入前进向左打转状态，此时右电机一定是正向满速状态，左电机反向，只需加速左电机
                                break;
                            case State.BackRightCircle: Stop();
                                break;
                            case State.BackdLeftCircle: Stop();
                                break;
                            case State.CWCircle: Stop();
                                break;
                            case State.CCWCircle: //ForwardRightCircle状态持续到两个电机都满速则进入该状态
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case 11://1011 上 左右 按键冲突，视为上
                    {
                        goto case 8;
                    }
                case 12://1100 上下 按键冲突，视为静止
                    {
                        goto case 0;
                    }
                case 13://1101 上下右 按键冲突，视为右
                    {

                        goto case 1;
                    }
                case 14://1110 上下左 按键冲突，视为左
                    {

                        goto case 2;
                    }
                case 15://1111 上下左右 按键冲突，视为静止
                    {
                        goto case 0;
                    }
                default:
                    break;
            }

            this.UpdateState();
        }

        /// <summary>
        /// 更新小车状态
        /// </summary>
        private void UpdateState()
        {
            if (motors[0].PerSpeed > 0)//左电机正转
            {
                if (motors[1].PerSpeed > 0)//右电机正转
                {
                    if (motors[0].PerSpeed > motors[1].PerSpeed)
                        curState = State.ForwardAndTurnRight;
                    else if (motors[0].PerSpeed < motors[1].PerSpeed)
                        curState = State.ForwardAndTurnLeft;
                    else
                        curState = State.Forward;
                }
                else if (motors[1].PerSpeed < 0)//右电机反转
                {
                    if (motors[0].PerSpeed > Math.Abs(motors[1].PerSpeed))
                        curState = State.ForwardRightCircle;
                    else if (motors[0].PerSpeed < Math.Abs(motors[1].PerSpeed))
                        curState = State.BackdLeftCircle;
                    else
                        curState = State.CWCircle;
                }
                else//右电机静止
                {
                    curState = State.ForwardAndTurnRight;
                }
            }
            else if (motors[0].PerSpeed < 0)//左电机反转
            {
                if (motors[1].PerSpeed > 0)//右电机正转
                {
                    if (Math.Abs(motors[0].PerSpeed) > motors[1].PerSpeed)
                        curState = State.BackRightCircle;
                    else if (Math.Abs(motors[0].PerSpeed) < motors[1].PerSpeed)
                        curState = State.ForwardLeftCircle;
                    else
                        curState = State.CCWCircle;
                }
                else if (motors[1].PerSpeed < 0)//右电机反转
                {
                    if (motors[0].PerSpeed > motors[1].PerSpeed)
                        curState = State.BackAndTurnLeft;
                    else if (motors[0].PerSpeed < motors[1].PerSpeed)
                        curState = State.BackAndTurnRight;
                    else
                        curState = State.Back;
                }
                else//右电机静止
                {
                    curState = State.BackAndTurnRight;
                }
            }
            else//左电机静止
            {
                if (motors[1].PerSpeed > 0)//右电机正转
                {
                    curState = State.ForwardAndTurnLeft;
                }
                else if (motors[1].PerSpeed < 0)//右电机反转
                {
                    curState = State.BackAndTurnLeft;
                }
                else//右电机静止
                {
                    curState = State.Stop;
                }
            }
        }

        /// <summary>
        /// 电机立刻停止
        /// </summary>
        private void Stop()
        {
            motors[0].PerSpeed = 0;
            motors[1].PerSpeed = 0;

        }

        /// <summary>
        /// 不回正的减速
        /// </summary>
        /// <param name="lStep"></param>
        /// <param name="rStep"></param>
        private void Dec(int lStep, int rStep)
        {
            motors[0].Control(false, lStep);
            motors[1].Control(false, rStep);
        }

        /// <summary>
        /// 不回正的加速
        /// </summary>
        /// <param name="lStep">左电机加速的步长</param>
        /// <param name="rStep">右电机加速的步长</param>
        /// <param name="forward">是否正向加速</param>
        private void Acc(int lStep, int rStep, bool forward)
        {
            motors[0].Control(true, lStep, forward);
            motors[1].Control(true, rStep, forward);
        }

        /// <summary>
        /// 双轮转向一致时的减速，将会首先减速回正，再同时减速
        /// </summary>
        /// <param name="lStep">左电机正向减速的步长</param>
        /// <param name="rStep">右电机正向减速的步长</param>
        private void DualDec(int lStep, int rStep)
        {
            if (lStep > rStep)
            {
                if (Math.Abs(motors[0].PerSpeed) - lStep >= Math.Abs(motors[0].PerSpeed) - rStep)
                {
                    motors[0].Control(false, lStep);
                    motors[1].Control(false, rStep);
                }
                else//减至两电机速度相同,状态将会为forward
                {
                    motors[0].Control(false, rStep);
                    motors[1].Control(false, rStep);
                }
            }
            else
            {
                if (Math.Abs(motors[0].PerSpeed) - lStep <= Math.Abs(motors[0].PerSpeed) - rStep)
                {
                    motors[0].Control(false, lStep);
                    motors[1].Control(false, rStep);
                }
                else//减至两电机速度相同,状态将会为forward
                {
                    motors[0].Control(false, lStep);
                    motors[1].Control(false, lStep);
                }
            }

        }

        /// <summary>
        /// 双轮转向一致时的加速，将会加速回正，并同时加速
        /// </summary>
        /// <param name="lStep">左电机加速的步长</param>
        /// <param name="rStep">右电机加速的步长</param>
        /// <param name="forward">是否正向加速</param>
        private void DualAcc(int lStep, int rStep, bool forward)
        {
            if (forward)
            {
                if (lStep > rStep)//此时回正
                {
                    if (motors[0].PerSpeed + lStep <= motors[1].PerSpeed + rStep)
                    {
                        motors[0].Control(true, lStep, true);
                        motors[1].Control(true, rStep, true);
                    }//加至两电机速度相同,状态将会为forward
                    else
                    {
                        motors[0].Control(true, rStep, true);
                        motors[1].Control(true, rStep, true);
                    }
                }
                else if (lStep < rStep)//此时回正
                {
                    if (motors[0].PerSpeed + lStep >= motors[1].PerSpeed + rStep)
                    {
                        motors[0].Control(true, lStep, true);
                        motors[1].Control(true, rStep, true);
                    }//加至两电机速度相同，状态将会为forward
                    else
                    {
                        motors[0].Control(true, lStep, true);
                        motors[1].Control(true, lStep, true);
                    }
                }
                else
                {
                    motors[0].Control(true, lStep, true);
                    motors[1].Control(true, rStep, true);
                }
            }
            else
            {
                if (lStep > rStep)//此时回正
                {
                    if (motors[0].PerSpeed - lStep >= motors[1].PerSpeed - rStep)
                    {
                        motors[0].Control(true, lStep, false);
                        motors[1].Control(true, rStep, false);
                    }//加至两电机速度相同，状态将会为back
                    else
                    {
                        motors[0].Control(true, rStep, false);
                        motors[1].Control(true, rStep, false);
                    }
                }
                else if (lStep < rStep)//此时回正
                {
                    if (motors[0].PerSpeed - lStep <= motors[1].PerSpeed - rStep)
                    {
                        motors[0].Control(true, lStep, false);
                        motors[1].Control(true, rStep, false);
                    }//加至两电机速度相同，状态将会为back
                    else
                    {
                        motors[0].Control(true, lStep, false);
                        motors[1].Control(true, lStep, false);
                    }
                }
                else
                {
                    motors[0].Control(true, lStep, false);
                    motors[1].Control(true, rStep, false);
                }
            }
        }

        public DualMotors()
        {
            this.motors.Add(new Motor(0, 225));//左侧电机
            this.motors.Add(new Motor(1, 255));//右侧电机
        }

        private List<Motor> motors = new List<Motor>();
        private State preState;
        private State curState;

        /// <summary>
        /// 电机序列
        /// </summary>
        public List<Motor> Motors
        {
            get { return motors; }
            set { motors = value; }
        }

        /// <summary>
        /// 小车前一刻状态
        /// </summary>
        public State PreState
        {
            get { return preState; }
        }

        /// <summary>
        /// 小车当前状态
        /// </summary>
        public State CurState
        {
            get { return curState; }
        }

        /// <summary>
        /// 状态枚举
        /// </summary>
        public enum State : int
        {
            /// <summary>
            /// 静止，此时两电机应趋向0
            /// </summary>
            Stop = 0,
            /// <summary>
            /// 前进，此时两电机速度大于0
            /// </summary>
            Forward = 1,
            /// <summary>
            /// 前进右转，此时电机0和电机1的速度大于0，电机0的速度要大于电机1
            /// </summary>
            ForwardAndTurnRight = 2,
            /// <summary>
            /// 前进左转，此时电机0和电机1的速度大于0，电机1的速度要大于电机0
            /// </summary>
            ForwardAndTurnLeft = 3,
            /// <summary>
            /// 后退，此时两电机速度小于0
            /// </summary>
            Back = 4,
            /// <summary>
            /// 后退右转，此时电机0和电机1的速度小于0，电机0的速度的绝对值要大于电机1
            /// </summary>
            BackAndTurnRight = 5,
            /// <summary>
            /// 后退左转，此时电机0和电机1的速度小于0，电机1的速度的绝对值要大于电机0
            /// </summary>
            BackAndTurnLeft = 6,
            /// <summary>
            /// 前右打转，此时电机0的速度应大于0，电机1的速度应小于等于0，但电机0的速度的绝对值要远大于1的
            /// </summary>
            ForwardRightCircle = 7,
            /// <summary>
            /// 前左打转，此时电机1的速度应大于0，电机0的速度应小于等于0，但电机1的速度的绝对值要远大于0的
            /// </summary>
            ForwardLeftCircle = 8,
            /// <summary>
            /// 后右打转，此时电机0的速度应小于0，电机1的速度应大于等于0，但电机0的速度的绝对值要远大于1的
            /// </summary>
            BackRightCircle = 9,
            /// <summary>
            /// 后左打转，此时电机1的速度应小于0，电机0的速度应大于等于0，但电机1的速度的绝对值要远大于0的
            /// </summary>
            BackdLeftCircle = 10,
            /// <summary>
            /// 原地顺时针打转
            /// </summary>
            CWCircle = 11,
            /// <summary>
            /// 原地逆时针打转
            /// </summary>
            CCWCircle = 12
        }



    }
}
