using confocal_test.Model;
using confocal_test.ViewModel;
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
    public partial class FormAOI : Form
    {
        //矩形框坐标
        private Rectangle DrawRect = new Rectangle(0, 0, 100, 100);
        //鼠标按下时坐标
        private Point MouseDownP = new Point();
        //放大倍数
        private int zoom = 0;
        //鼠标移动矩形框次数，如果移动过矩形框则不从中心放大，以移动后的位置放大缩小，缩小为原大小，缩放数为0时，重置此数
        private int MoveCount = 0;

        private AOIViewModel mAoiViewModel;

        public FormAOI()
        {
            InitializeComponent();
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this_MouseWheel);
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

            DrawRect.Height = this.Height - zoom * 2;
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

        private void FormAOI_Load(object sender, EventArgs e)
        {
            mAoiViewModel = new AOIViewModel();
            textBox1.DataBindings.Add("Text", mAoiViewModel, "AID");
            textBox2.DataBindings.Add("Text", mAoiViewModel, "CID");
            numericUpDown1.DataBindings.Add("Value", mAoiViewModel, "AID");
            numericUpDown2.DataBindings.Add("Value", mAoiViewModel, "CID");

            textBox3.DataBindings.Add("Text", CommonModel.GetCommonModel(), "ID");
            numericUpDown3.DataBindings.Add("Value", CommonModel.GetCommonModel(), "ID");
            textBox4.DataBindings.Add("Text", AModel.GetAModel(), "ID");
            numericUpDown4.DataBindings.Add("Value", AModel.GetAModel(), "ID");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

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
