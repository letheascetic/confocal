using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows;
using System.IO.Ports;
//
using System.IO;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using PI;
/*其中，1，2，3分表表示压电控制器的X，Y，Z轴*/
namespace PIControlDLL
{
    public partial class PIControlWidget : Form
    {
        struct currentValue
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public double[] Position;
            public double[] Voltage;
            public int[] svoStatus;
        }
        public double[] currentPosition = new double[3];
        StringBuilder buffer = new StringBuilder(1024);
        /*压电控制器ID*/
        int E761_ID = -1;
        /*实例化配置文件对象*/
        ConfigFile m_ConfigFile = new ConfigFile();
        public string strFilePath = Application.StartupPath + "\\ini\\Config.ini";//获取INI文件路径
        private string strSec = ""; //INI文件名
        public PIControlWidget()
        {
            InitializeComponent();
          //  this.TopMost = true;
            m_ConfigFile.m_PIControlWidget = this;
            label_E761.BackColor = Color.Red;
        }

        private void 背景颜色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //显示颜色对话框
            ColorDialog m_ColorDialog = new ColorDialog();
            DialogResult dr = m_ColorDialog.ShowDialog();
            //如果选中颜色，单击“确定”按钮则改变文本框的文本颜色
            if (dr == DialogResult.OK)
            {
                this.ForeColor = m_ColorDialog.Color;
            }
        }
        /*连接压电控制器是否成功*/
        public bool ConnectedStatus()
        {
            return E7XX.IsConnected(E761_ID) > 0 ? true : false;
        }
        /*连接压电控制器*/
        public bool connectPIControl()
        {
            if (numericUpDown_E761board.Text == "")
            {
                MessageBox.Show("板卡号为空，请设置板卡号，默认为1", "压电控制");
                return false;
            }
            E761_ID = E7XX.ConnectPciBoard(Convert.ToInt32(numericUpDown_E761board.Value));
            if (E761_ID == 0)
            {
                label_E761.BackColor = Color.Green;
                return true;
            }
            return false;
        }
        private void updateStatus()
        {
            /*每次对压电控制器进行操作之前都先去查询是否与压电控制器连接成功*/
            if (!ConnectedStatus())  //如果未连接压电控制器
            {
                connectPIControl();
            }
            /*压电控制器X,Y,Z轴当前位置*/
            currentValue m_currentValue;
            m_currentValue.Position = new double[1];
            m_currentValue.Voltage = new double[1];
            m_currentValue.svoStatus = new int[1];
            E7XX.qPOS(E761_ID, "1", m_currentValue.Position);
            textBox_YD_XLocation.Text = m_currentValue.Position[0].ToString("f3");
            E7XX.qPOS(E761_ID, "2", m_currentValue.Position);
            textBox_YD_YLocation.Text = m_currentValue.Position[0].ToString("f3");
            E7XX.qPOS(E761_ID, "3", m_currentValue.Position);
            textBox_YD_ZLocation.Text = m_currentValue.Position[0].ToString("f3");
            /*压电控制器X,Y,Z轴当前输出电压*/
            E7XX.qVOL(E761_ID, "1", m_currentValue.Voltage);
            textBox_YD_XVoltage.Text = m_currentValue.Voltage[0].ToString("f2");
            E7XX.qVOL(E761_ID, "2", m_currentValue.Voltage);
            textBox_YD_YVoltage.Text = m_currentValue.Voltage[0].ToString("f2");
            E7XX.qVOL(E761_ID, "3", m_currentValue.Voltage);
            textBox_YD_ZVoltage.Text = m_currentValue.Voltage[0].ToString("f2");
            /*压电控制器X,Y,Z轴开环/闭环状态*/
            E7XX.qSVO(E761_ID, "1", m_currentValue.svoStatus);
            textBox_YD_XStatus.Text = m_currentValue.svoStatus[0] == 1 ? "闭环" : "开环";
            button_x_svo.Text = m_currentValue.svoStatus[0] == 1 ? "设置开环" : "设置闭环";
            E7XX.qSVO(E761_ID, "2", m_currentValue.svoStatus);
            textBox_YD_YStatus.Text = m_currentValue.svoStatus[0] == 1 ? "闭环" : "开环";
            button_y_svo.Text = m_currentValue.svoStatus[0] == 1 ? "设置开环" : "设置闭环";
            E7XX.qSVO(E761_ID, "3", m_currentValue.svoStatus);
            textBox_YD_ZStatus.Text = m_currentValue.svoStatus[0] == 1 ? "闭环" : "开环";
            button_z_svo.Text = m_currentValue.svoStatus[0] == 1 ? "设置开环" : "设置闭环";
        }
        /*压电控制器状态查询*/
        private void button_Statuscheck_Click(object sender, EventArgs e)
        {
            updateStatus();
        }
        /*连接压电控制器*/
        private bool ConnectPIControl()
        {
            return E7XX.IsConnected(E761_ID) > 0 ? true : false;
        }
        /*初始化校正零点标签背景颜色*/
        private void label_status_Paint(object sender, PaintEventArgs e)
        {
            label_status.BackColor = Color.Red;
        }
        /*移动指定轴指定距离，axis：移动的轴；moveDistance：移动距离；waitSeconds：等待时间*/
        public bool moveDistance(string axis,double moveDistance, double waitSeconds)
        {
            if (!ConnectedStatus())
            {
                MessageBox.Show("未连接压电平台控制器","压电平台");
                return false;
            }
            double[] currentPosition = new double[1];     //先获取当前位置
            int res = E7XX.qPOS(E761_ID, axis, currentPosition);
            if (currentPosition[0] + moveDistance > 300 || currentPosition[0] + moveDistance < 0)   //移动范围
            {
                buffer.Clear();
                buffer.Append(String.Format("尝试移动距离越界:{0:f3}", moveDistance));//压电平台内部有自我保护机制
                //保护压电控制器
                return false;
            }
            int[] svoState = new int[1];
            res = E7XX.qSVO(E761_ID, axis, svoState);  //获取axis轴伺服状态（开环/闭环操作）
            if (svoState[0] != 1)
            {
                res = E7XX.SVO(E761_ID, axis, new int[] { 1 });
                if (res == 0 || res == -1)
                {
                    int ierror = E7XX.GetError(E761_ID);   //设置axis轴为闭环
                    buffer.Clear();
                    E7XX.TranslateError(ierror, buffer, buffer.Capacity);
                    return false;
                }
                svoState[0] = 1;//axis轴已变为闭环
            }
            double[] setDistance = new double[1];
            setDistance[0] = moveDistance;
            res = E7XX.MVR(E761_ID, axis, setDistance);//将axis轴移动设定距离
            if (res == 0 || res == -1)
            {
                int ierror = E7XX.GetError(E761_ID);
                buffer.Clear();
                E7XX.TranslateError(ierror, buffer, buffer.Capacity);
                return false;
            }
            DateTime startTime = DateTime.Now;
            while(true)
            {
                double[] newPosition = new double[1];
                res = E7XX.qPOS(E761_ID, axis, newPosition);
                if (res == 0 || res == -1)
                {
                    int ierror = E7XX.GetError(E761_ID);
                    buffer.Clear();
                    E7XX.TranslateError(ierror, buffer, buffer.Capacity);
                    return false;
                }
                bool arrive = true;
                if (Math.Abs(newPosition[0] - currentPosition[0] - moveDistance) > Convert.ToDouble(textBox_accuracy.Text))   //um单位
                {
                    arrive = false;
                }
                if (arrive)
                {
                    break;
                }
                if ((DateTime.Now - startTime).TotalSeconds > waitSeconds)
                {
                    buffer.Clear();
                    buffer.Append(String.Format(
                        "移动压电平台距离{0:f3}超时，目前位置{1:f3}", moveDistance, newPosition[0]));
                    textBox_message.AppendText("移动压电平台设定距离超时，目前位置：" + newPosition[0] + "\r\n");
                    return false;
                }
                Thread.Sleep(10);
            }
            updateStatus();  //状态查询
            return true;
        }
        /*移动指定轴到指定位置，axis：移动的轴；desiredZ：目标位置；waitSeconds：等待时间*/
        public bool MotoTagPosition(string axis, double desiredZ, double waitSeconds)
        {
            if (!ConnectedStatus())
            {
                MessageBox.Show("未连接压电平台控制器", "压电平台");
                return false;
            }
            if (desiredZ > 300 || desiredZ < 0)   //移动范围
            {
                buffer.Clear();
                buffer.Append(String.Format("尝试到达的坐标越界:{0:f3}", desiredZ));//压电平台内部有自我保护机制
                //保护压电控制器
                return false;
            }
            int[] svoState = new int[1];
            int res = E7XX.qSVO(E761_ID, axis, svoState);  //获取axis轴伺服状态（开环/闭环操作）
            string axisNmae="";
            switch (axis)
            {
                case "1":
                    axisNmae = "x";
                    break;
                case "2":
                    axisNmae = "y";
                    break;
                case "3":
                    axisNmae = "z";
                    break;
            }
            if (svoState[0] != 1)
            {
                res = E7XX.SVO(E761_ID, axis, new int[] { 1 });
                textBox_message.AppendText(axisNmae + "轴正在被设置为闭环状态");
                if (res == 0 || res == -1)
                {
                    textBox_message.AppendText(axisNmae + "轴设置为闭环状态失败");
                    int ierror = E7XX.GetError(E761_ID);   //设置axis轴为闭环
                    buffer.Clear();
                    E7XX.TranslateError(ierror, buffer, buffer.Capacity);
                    return false;
                }
                switch(axis)
                {
                    case "1":
                        button_x_svo.Text = "设置开环";
                        break;
                    case "2":
                        button_y_svo.Text = "设置开环";
                        break;
                    case "3":
                        button_z_svo.Text = "设置开环";
                        break;
                }
                svoState[0] = 1;//axis轴已变为闭环
            }
            double[] tagPosition = new double[1];
            tagPosition[0] = desiredZ;
            res = E7XX.MOV(E761_ID, axis, tagPosition);//将axis轴移动到目标位置处
            textBox_message.AppendText(axisNmae + "轴将被移动到" + Convert.ToString(desiredZ)+ "nm位置处\r\n");
            if (res == 0 || res == -1)
            {
                int ierror = E7XX.GetError(E761_ID);
                buffer.Clear();
                E7XX.TranslateError(ierror, buffer, buffer.Capacity);
                return false;
            }
            double[] zPos = new double[1];
            DateTime startTime = DateTime.Now;
            while (true)
            {
                res = E7XX.qPOS(E761_ID, axis, zPos);
                if (res == 0 || res == -1)
                {
                    int ierror = E7XX.GetError(E761_ID);
                    buffer.Clear();
                    E7XX.TranslateError(ierror, buffer, buffer.Capacity);
                    return false;
                }
                bool arrive = true;
                if (Math.Abs(desiredZ - zPos[0]) > Convert.ToDouble(textBox_accuracy.Text))   //um单位
                {
                    arrive = false;
                }
                if (arrive)
                {
                    break;
                }
                if ((DateTime.Now - startTime).TotalSeconds > waitSeconds)
                {
                    buffer.Clear();
                    buffer.Append(String.Format(
                        "移动压电平台到{0:f3}超时，目前位置{1:f3}", desiredZ, zPos[0]));
                    textBox_message.AppendText(axisNmae + "轴移动到目标位置超时，目前位置：" + zPos[0] + "\r\n");
                    return false;
                }
                Thread.Sleep(10);
            }
            textBox_message.AppendText(axisNmae + "轴被成功移动到" + Convert.ToString(desiredZ) + "nm位置处\r\n");
            updateStatus();  //状态查询
            return true;
        }
        //移动压电控制器Y轴,向正方向移动
        private void button_YD_Back_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus())  //先判断是否与压电平台连接成功
            {
                bool flag = ConnectPIControl();
                if (!flag)
                {
                    MessageBox.Show("PI控制器连接超时，请检查是否打开设备", "刻写");
                    return;
                }
            }
            bool res = moveDistance("2",Convert.ToDouble(textBox_YD_Y.Text) / 1000, 1);
            if (res == false)
            {
                MessageBox.Show("移动失败");
            }
            else
            {
                updateStatus();  //状态查询
            }
        }
        //移动压电控制器Y轴,向负方向移动
        private void button_YD_Forward_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus()) //先判断是否与压电平台连接成功
            {
               bool flag = ConnectPIControl();
                if(!flag)
                {
                    MessageBox.Show("PI控制器连接超时，请检查是否打开设备", "刻写");
                    return;
                }
            }
            bool res = moveDistance("2", -Convert.ToDouble(textBox_YD_Y.Text) / 1000, 1);
            if (res == false)
            {
                MessageBox.Show("移动失败");
            }
            else
            {
                updateStatus();  //状态查询
            }
        }
        //移动压电控制器X轴,向右移动
        private void button_YD_Right_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus()) //先判断是否与压电平台连接成功
            {
                bool flag = ConnectPIControl();
                if (!flag)
                {
                    MessageBox.Show("PI控制器连接超时，请检查是否打开设备", "刻写");
                    return;
                }
            }
            bool res = moveDistance("2", Convert.ToDouble(textBox_YD_X.Text) / 1000, 1);
            if (res == false)
            {
                MessageBox.Show("移动失败");
            }
            else
            {
                updateStatus();  //状态查询
            }
        }
        //移动压电控制器X轴,向左移动
        private void button_YD_Left_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus()) //先判断是否与压电平台连接成功
            {
                bool flag = ConnectPIControl();
                if (!flag)
                {
                    MessageBox.Show("PI控制器连接超时，请检查是否打开设备", "刻写");
                    return;
                }
            }
            bool res = moveDistance("2", -Convert.ToDouble(textBox_YD_X.Text) / 1000, 1);
            if (res == false)
            {
                MessageBox.Show("移动失败");
            }
            else
            {
                updateStatus();  //状态查询
            }
        }
        //移动压电控制器Z轴,向上移动
        private void button_YD_Up_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus()) //先判断是否与压电平台连接成功
            {
                MessageBox.Show("PI控制器连接超时，请检查是否打开设备", "刻写");
                return;
            }
            bool res = moveDistance("3", Convert.ToDouble(textBox_YD_Z.Text) / 1000, 1); ;  //移动压电控制器Z轴，1表示等待时间为1秒
            if (res == false)
            {
                MessageBox.Show("移动失败");
            }
            else
            {
                updateStatus();  //状态查询
            }
        }
        //电压归零
        private void button_Voltage_E761_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus())
            {
                connectPIControl();
            }
            int[] value = new int[1];
            E7XX.qSVO(E761_ID, "123", value);
            if(value[0] + value[0] + value[0] > 0)
            {
                E7XX.SVO(E761_ID, "123", new int[] {0,0,0});
            }
            Delay(100);  //等待0.1秒充分执行
            int res = E7XX.VOL(E761_ID,"123",new double[]{0,0,0 });  //调用该函数之前需要保证伺服状态是开环状态
            Delay(100);//等待0.1秒充分执行
            if (res <= 0)
            {
                textBox_message.AppendText("电压归零失败\r\n");
            }
            else
            {
                textBox_message.AppendText("电压归零成功\r\n");
            }
        }
        //延时毫秒
        public static void Delay(double milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }
        /*连接压电控制器*/
        private void button_ConnectE761_Click(object sender, EventArgs e)
        {
            if (button_ConnectE761.Text == "连接")
            {
                if (numericUpDown_E761board.Text=="")
                {
                    MessageBox.Show("板卡号为空","压电控制");
                    return;
                }
                E761_ID = E7XX.ConnectPciBoard(Convert.ToInt32(numericUpDown_E761board.Value));                           //延时1秒
                DateTime now = DateTime.Now;
                while (E761_ID < 0)
                {
                    DateTime time = DateTime.Now;
                    if ((time - now).Milliseconds > 200)    //等待0.2秒
                    {
                        break;
                    }
                }
                if (E761_ID < 0)
                {
                    textBox_message.AppendText("压电平台连接失败\r\n");
                    E761_ID = E7XX.ConnectPciBoard(Convert.ToInt32(numericUpDown_E761board.Value));                             //延时1秒
                    while (E761_ID < 0)
                    {
                        DateTime time = DateTime.Now;
                        if ((time - now).Milliseconds > 200)    //等待0.2秒
                        {
                            break;
                        }
                    }
                }
                else
                {
                    textBox_message.AppendText("压电平台连接成功\r\n");
                    button_ConnectE761.Text = "断开连接";
                    label_E761.BackColor = Color.Green;
                    updateStatus();//获取压电平台信息
                }
            }
            else
            {
                int[] movStaus = new int[3];
                int res = E7XX.IsMoving(E761_ID,"123", movStaus);      //断开连接之前先判断是否有轴正在运动
                if(res >= 0)
                {
                   if(movStaus[0] == 1 || movStaus[1] == 1 || movStaus[2] == 1 )
                   {
                        DialogResult isOk =  MessageBox.Show("轴正在移动，是否任然关闭连接?","压电平台", MessageBoxButtons.YesNo);
                        if(isOk == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                E7XX.CloseConnection((Int32)numericUpDown_E761board.Value);      //断开连接
                Delay(200);   //等待0.2秒
                if (E7XX.IsConnected(E761_ID) != 1)  //是否断开连接成功
                {
                    button_ConnectE761.Text = "连接";
                    label_E761.BackColor = Color.Red;
                    textBox_message.AppendText("压电平台断开连接成功\r\n");
                }
                else
                {
                    textBox_message.AppendText("压电平台断开连接失败\r\n");
                }
            }
        }
        private bool setSvoStatus(string axis,int status)
        {
            if (ConnectedStatus())
            {
                int[] svoStatus = new int[1];
                svoStatus[0] = status;
                int res = E7XX.SVO(E761_ID, axis, svoStatus);
                Delay(100);   //延时0.1秒
                int[] nowStatus = new int[1];
                E7XX.qSVO(E761_ID, axis, nowStatus);
                if (nowStatus[0] == status)
                {
                    string str = status == 1 ? "闭环设置操作成功" : "开环设置操作成功";
                    textBox_message.AppendText(str + "\r\n");
                }
                else
                {
                    string str = status == 1 ? "闭环设置操作失败" : "开环设置操作失败";
                    textBox_message.AppendText(str + "\r\n");
                    return false;
                }
                /*更新压电控制器X,Y,Z轴开环/闭环状态*/
                switch (axis)
                {
                    case "1":
                        textBox_YD_XStatus.Text = nowStatus[0] == 1 ? "闭环" : "开环";
                        break;
                    case "2":
                        textBox_YD_YStatus.Text = nowStatus[0] == 1 ? "闭环" : "开环";
                        break;
                    case "3":
                        textBox_YD_ZStatus.Text = nowStatus[0] == 1 ? "闭环" : "开环";
                        break;
                }
                return true;
            }
            else
            {
                textBox_message.AppendText("未连接压电控制器\r\n");
                return false;
            }
        }

        //零点校正E761
        private void button_zeroE761_Click(object sender, EventArgs e)
        {
            if (ConnectedStatus())
            {
                int res = E7XX.ATZ(E761_ID, "123", new double[] { 0, 0, 0 }, new int[] { 1, 1, 1 });
                Delay(100);//延时0.1s，等待零点校正执行到位
                if (res > 0)
                {
                    label_ZeroCalibration.BackColor = Color.Green;
                    textBox_message.AppendText("零点校正成功\r\n");
                    updateStatus();
                }
                else
                {
                    label_ZeroCalibration.BackColor = Color.Red;
                    textBox_message.AppendText("零点校正失败\r\n");
                }
            }
        }
        //移动压电控制器Z轴,向下移动
        private void button_YD_Down_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus()) //先判断是否与压电平台连接成功
            {
                bool flag = ConnectPIControl();
                if (!flag)
                {
                    MessageBox.Show("PI控制器连接超时，请检查是否打开设备", "刻写");
                    return;
                }
            }
            bool res = moveDistance("3", -Convert.ToDouble(textBox_YD_Z.Text) / 1000, 1); ;  //移动压电控制器Z轴，1表示等待时间为1秒
            if (res == false)
            {
                MessageBox.Show("移动失败");
            }
            else
            {
                updateStatus();  //状态查询
            }
        }
        //压电控制器初始化
        private void button_init_Click(object sender, EventArgs e)
        {
            if (!ConnectedStatus()) //先判断是否与压电平台连接成功)
            {
                MessageBox.Show("请检查硬件：未连接压电控制器！");
                return;
            }
            bool flag = moveDistance("3",Convert.ToDouble(textBox_initLocation.Text),1);
            if (flag)
            {
                textBox_message.AppendText("压电平台初始化成功\r\n");
                updateStatus();
            }
        }
        public void setType(bool flag)
        {
            radioButton_up.Checked = flag;
            radioButton_down.Checked = !flag;
        }
        //X轴移动到指定位置(绝对位置)
        private void button_X_Start_Click(object sender, EventArgs e)
        {
            if(textBox_X_AbsPosition.Text == "")
            {
                MessageBox.Show("未设置X轴需要移动的目标位置！", "压电平台");
                return;
            }
            bool res = MotoTagPosition("1", Convert.ToDouble(textBox_X_AbsPosition.Text) / 1000,1);  //移动到绝对位置         
            if (res == false)
            {
                textBox_message.AppendText("移动X轴到目标位置失败\r\n");
            }
            /*压电控制器X,Y,Z轴当前位置*/
            currentValue m_currentValue;
            m_currentValue.Position = new double[1];
            m_currentValue.Voltage = new double[1];
            m_currentValue.svoStatus = new int[1];
            E7XX.qPOS(E761_ID, "1", m_currentValue.Position);
            textBox_YD_XLocation.Text = m_currentValue.Position[0].ToString("f3");
            /*压电控制器X,Y,Z轴当前输出电压*/
            E7XX.qVOL(E761_ID, "1", m_currentValue.Voltage);
            textBox_YD_XVoltage.Text = m_currentValue.Voltage[0].ToString("f2");
            /*压电控制器X,Y,Z轴开环/闭环状态*/
            E7XX.qSVO(E761_ID, "1", m_currentValue.svoStatus);
            textBox_YD_XStatus.Text = m_currentValue.svoStatus[0] == 1 ? "闭环" : "开环";
            button_x_svo.Text = m_currentValue.svoStatus[0] == 1 ? "设置开环" : "设置闭环";
        }
        //Y轴移动到指定位置(绝对位置)
        private void button_Y_Start_Click(object sender, EventArgs e)
        {
            if (textBox_Y_AbsPosition.Text == "")
            {
                MessageBox.Show("未设置Y轴需要移动的目标位置！", "压电平台");
                return;
            }
            bool res = MotoTagPosition("2", Convert.ToDouble(textBox_Y_AbsPosition.Text) / 1000, 1); //移动到绝对位置         
            if (res == false)
            {
                textBox_message.AppendText("移动Y轴到目标位置失败\r\n");
            }
            /*压电控制器X,Y,Z轴当前位置*/
            currentValue m_currentValue;
            m_currentValue.Position = new double[1];
            m_currentValue.Voltage = new double[1];
            m_currentValue.svoStatus = new int[1];
            E7XX.qPOS(E761_ID, "2", m_currentValue.Position);
            textBox_YD_YLocation.Text = m_currentValue.Position[0].ToString("f3");
            /*压电控制器X,Y,Z轴当前输出电压*/
            E7XX.qVOL(E761_ID, "2", m_currentValue.Voltage);
            textBox_YD_YVoltage.Text = m_currentValue.Voltage[0].ToString("f2");
            /*压电控制器X,Y,Z轴开环/闭环状态*/
            E7XX.qSVO(E761_ID, "2", m_currentValue.svoStatus);
            textBox_YD_YStatus.Text = m_currentValue.svoStatus[0] == 1 ? "闭环" : "开环";
            button_y_svo.Text = m_currentValue.svoStatus[0] == 1 ? "设置开环" : "设置闭环";
        }
        //Z轴移动到指定位置(绝对位置)
        private void button_Z_Start_Click(object sender, EventArgs e)
        {
            if (textBox_Z_AbsPosition.Text == "")
            {
                MessageBox.Show("未设置Z轴需要移动的目标位置！", "压电平台");
                return;
            }
            bool res = MotoTagPosition("3", Convert.ToDouble(textBox_Z_AbsPosition.Text) / 1000, 1);    //移动到绝对位置
            if (res == false)
            {
                textBox_message.AppendText("移动Z轴到目标位置失败\r\n");
            }
            /*压电控制器X,Y,Z轴当前位置*/
            currentValue m_currentValue;
            m_currentValue.Position = new double[1];
            m_currentValue.Voltage = new double[1];
            m_currentValue.svoStatus = new int[1];
            E7XX.qPOS(E761_ID, "3", m_currentValue.Position);
            textBox_YD_ZLocation.Text = m_currentValue.Position[0].ToString("f3");
            /*压电控制器X,Y,Z轴当前输出电压*/
            E7XX.qVOL(E761_ID, "3", m_currentValue.Voltage);
            textBox_YD_ZVoltage.Text = m_currentValue.Voltage[0].ToString("f2");
            /*压电控制器X,Y,Z轴开环/闭环状态*/
            E7XX.qSVO(E761_ID, "3", m_currentValue.svoStatus);
            textBox_YD_ZStatus.Text = m_currentValue.svoStatus[0] == 1 ? "闭环" : "开环";
            button_z_svo.Text = m_currentValue.svoStatus[0] == 1 ? "设置开环" : "设置闭环";
        }

        //窗体关闭事件
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!File.Exists(strFilePath))//读取信息时先判读INI文件是否存在
            {
                // 创建文件
                FileStream fs = new FileStream(strFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
                StreamWriter sw = new StreamWriter(fs); // 创建写入流
                sw.Close(); //关闭文件
            }
            else
            {
                //根据INI文件名设置要写入INI文件的节点名称
                //此处的节点名称完全可以根据实际需要进行配置
                strSec = Path.GetFileNameWithoutExtension(strFilePath);
            }
        }
        //设置x轴伺服状态
        private void button_x_svo_Click(object sender, EventArgs e)
        {
            if(button_x_svo.Text == "设置闭环")
            {
                bool res = setSvoStatus("1", 1);
                if(res)
                {
                    button_x_svo.Text = "设置开环";
                }
            }
            else
            {
                bool res = setSvoStatus("1", 0);
                if (res)
                {
                    button_x_svo.Text = "设置闭环";
                }
            }
        }
        //设置y轴伺服状态
        private void button_y_svo_Click(object sender, EventArgs e)
        {
            if (button_y_svo.Text == "设置闭环")
            {
                bool res = setSvoStatus("2", 1);
                if (res)
                {
                    button_y_svo.Text = "设置开环";
                }
            }
            else
            {
                bool res = setSvoStatus("2", 0);
                if (res)
                {
                    button_y_svo.Text = "设置闭环";
                }
            }
        }
        //设置z轴伺服状态
        private void button_z_svo_Click(object sender, EventArgs e)
        {
            if (button_z_svo.Text == "设置闭环")
            {
                bool res = setSvoStatus("3", 1);
                if (res)
                {
                    button_z_svo.Text = "设置开环";
                }
            }
            else
            {
                bool res = setSvoStatus("3", 0);
                if (res)
                {
                    button_z_svo.Text = "设置闭环";
                }
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            E7XX.CloseConnection((Int32)numericUpDown_E761board.Value);      //断开连接
        }
        //保存界面
        private void button_save_Click(object sender, EventArgs e)
        {
            //创建图象，保存将来截取的图象
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics imgGraphics = Graphics.FromImage(image);
            //设置截屏区域
            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bit);
            g.CopyFromScreen(new Point(this.Location.X, this.Location.Y), new Point(0, 0), bit.Size);
            //保存
            SaveImage(bit);
        }
        //保存图象文件
        private void SaveImage(Bitmap image)
        {
            string exportDir = "../Image/";
            DateTime dt = DateTime.Now;
            string time = Convert.ToString(dt);
            time = time.Replace("/", "").Replace(":", "").Replace(" ", "");
            if (Directory.Exists(exportDir) == false)
            {
                Directory.CreateDirectory(exportDir);
            }
            else
            {
                DateTime date = DateTime.Now;
                string str = date.ToString().Substring(0, date.ToString().IndexOf(" ")).Replace("/", "");
                exportDir = exportDir + str + "\\";
                Directory.CreateDirectory(exportDir);
            }
            string fileName = exportDir + "_" + time + ".jpg";
            string extension = Path.GetExtension(fileName);
            if (extension == ".jpg")
            {
                image.Save(fileName, ImageFormat.Jpeg);
            }
            else
            {
                image.Save(fileName, ImageFormat.Bmp);
            }
            MessageBox.Show("导出图像成功" + fileName, "");
        }
    }
}

