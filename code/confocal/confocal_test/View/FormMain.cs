using confocal_test.Model;
using confocal_test.ViewModel;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_test.View
{
    public partial class FormMain : Form
    {
        private Mat scanImage;

        //矩形框坐标
        private Rectangle DrawRect = new Rectangle(0, 0, 100, 100);
        //鼠标按下时坐标
        private Point MouseDownP = new Point();
        //放大倍数
        private int zoom = 0;
        //鼠标移动矩形框次数，如果移动过矩形框则不从中心放大，以移动后的位置放大缩小，缩小为原大小，缩放数为0时，重置此数
        private int MoveCount = 0;

        private MainViewModel mMainVM;

        public FormMain()
        {
            InitializeComponent();
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this_MouseWheel);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormArea a = new FormArea();
            a.MdiParent = this;
            a.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            scanImage = new Mat(512, 512, DepthType.Cv8U, 3);
            // imageBox1.Image = scanImage;
            mMainVM = new MainViewModel();
            textBox1.DataBindings.Add("Text", mMainVM, "AID");
            textBox2.DataBindings.Add("Text", mMainVM, "CID");
            numericUpDown1.DataBindings.Add("Value", mMainVM, "AID");
            numericUpDown2.DataBindings.Add("Value", mMainVM, "CID");


            textBox3.DataBindings.Add("Text", CommonModel.GetCommonModel(), "ID");
            numericUpDown3.DataBindings.Add("Value", CommonModel.GetCommonModel(), "ID");
            textBox4.DataBindings.Add("Text", AModel.GetAModel(), "ID");
            numericUpDown4.DataBindings.Add("Value", AModel.GetAModel(), "ID");
        }

        private void this_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)

            {

                if (zoom < 80)// 最大放大80倍             

                    zoom++;
            }
            else
            {
                if (zoom > 0)
                    zoom--;
                if (zoom == 0) //放大倍数＝0,不放大，鼠标拖动标记归0      
                    MoveCount = 0;
            }

            //放大原理   

            //1、先获取放大倍数  
            //2、根据放大倍数，计算矩形框高度，高度＝控件高度－倍数×2；  
            //3、根据高度，提取矩形宽度，宽度＝高度×（控件宽度/控件高度），为保证矩形和原控件纵横比例，所以要乘以比例     
            //4、根据矩形框大小，和控件大小，计算矩形框在控件中的位置，即X,Y坐标           
            //1）如果没有拖动过矩形，则按默认中间位置取值，X＝（控件宽度－矩形宽度)/2,Y＝（控件高度－矩形高度）/2      
            //2）如果拖动过矩形框，则原X，Y坐标不变化       
            //5、根据得到的新矩形框的坐标和范围，判断是否超界，判断XY坐标
            //滚轮放大事件     
            //矩形区域高度=控件高度-放大缩小倍数*2        
            DrawRect.Height = this.Height - zoom * 2;

            //按比例计算宽度   

            DrawRect.Width = (int)(Convert.ToSingle(DrawRect.Height) * (Convert.ToSingle(this.Width) / Convert.ToSingle(this.Height)));

            if (MoveCount == 0)//没有拖动过，滚动滚轮才按中间放大缩小    
            {
                DrawRect.X = (this.Width - DrawRect.Width) / 2;
                DrawRect.Y = (this.Height - DrawRect.Height) / 2;
            }

            //===============判断是否超界＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝         
            //如果Right超过控件宽度          
            if (DrawRect.Right > this.Width)
            {
                DrawRect.X = DrawRect.X - (DrawRect.Right - this.Width) - 1;
                DrawRect.X = DrawRect.X > 1 ? DrawRect.X : 1;
            }
            //如果Bottom超过控件高度      
            if (DrawRect.Bottom > this.Height)
            {
                DrawRect.Y = DrawRect.Y - (DrawRect.Bottom - this.Height) - 1;
                DrawRect.Y = DrawRect.Y > 1 ? DrawRect.Y : 1;
            }
            DrawImg();
        }

        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (DrawRect.Contains(MouseDownP))//判断鼠标按下的坐标是否在红框中，确定是否拖动的红框     
                {
                    //拖动鼠标位置，矩形框新X＝矩形框原X＋（当前鼠标X－按下时X），原X＋偏移量        
                    //Y轴一样变化
                    DrawRect.X = DrawRect.X + (e.X - MouseDownP.X); //.Location = ClienP;        
                    DrawRect.Y = DrawRect.Y + (e.Y - MouseDownP.Y); // ClienP.Y;
                    //判断是否超过左上角           
                    if (DrawRect.X < 0)
                        DrawRect.X = 0;
                    if (DrawRect.Y < 0)
                        DrawRect.Y = 0;
                    //判断是否超过右下 角     
                    if (DrawRect.X > (this.Width - DrawRect.Width - 1))
                        DrawRect.X = this.Width - DrawRect.Width - 1;
                    if (DrawRect.Y > (this.Height - DrawRect.Height - 1))
                        DrawRect.Y = this.Height - DrawRect.Height - 1;
                    //画图              
                    DrawImg();
                    //计算完坐标系，鼠标按下坐标转换成当前鼠标坐标，以重新计算偏移           
                    MouseDownP.X = e.X;
                    MouseDownP.Y = e.Y;
                    //拖动过鼠标，鼠标拖动标记累加         
                    MoveCount++;
                }
            }
        }

        private void this_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownP = new Point(e.X, e.Y);
        }

        void DrawImg()
        {
            using (Graphics g = this.CreateGraphics())
            {
                //重绘背景
                g.FillRectangle(new SolidBrush(Color.LightGray), 0, 0, this.Width, this.Height);
                g.DrawString("滚轮放大，左键拖动矩形框" + zoom, new Font("黑体", 12f), new SolidBrush(Color.Green), 5, 5);
                g.DrawString("放大" + zoom, new Font("黑体", 12f), new SolidBrush(Color.Red), 5, 20);
                //重绘矩形
                g.DrawRectangle(new Pen(Color.Red), DrawRect);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAOI form = new FormAOI();
            form.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mMainVM.AID = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            mMainVM.CID = (int)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            CommonModel.GetCommonModel().ID = (int)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            AModel.GetAModel().ID = (int)numericUpDown4.Value;
        }
    }
}
