using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CG_lab2
{
    public partial class Form1 : Form
    {
        DrawData allpoints;
        Stack<DrawData> condition;

        public Form1()
        {
            InitializeComponent();
            
        }
        

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.Location.ToString();
        }

        Graphics pan;
        Pen pen1 = new Pen(Color.DarkBlue, 2);

        private void GenHead(double X, double Y, double R)
        {
            double angle1 = (0.0 / 180) * Math.PI;                
            double angle2 = (360.0 / 180) * Math.PI;             
            double delta = 1 / R;
            double iterations = Math.Abs(angle2 - angle1) / delta;

            for (int i = 0; i <= iterations; i++)
            {
                double x = X + R * Math.Cos(angle1);
                double y = Y - R * Math.Sin(angle1);
                allpoints.AddHead(x, y);
                angle1 += delta;
            }
            
            double x1 = X + R * Math.Cos(angle1);
            double y1 = Y - R * Math.Sin(angle1);
            allpoints.AddHead(x1, y1);
        }
      
        private void GenBody(double X, double Y, double R1, double R2)
        {
            double angle1 = (0.0 / 180) * Math.PI;                 
            double angle2 = (360.0 / 180) * Math.PI;
            double delta;
            if (R1 > R2)
                delta = 1 / R1;
            else
                delta = 1 / R2;
            double iterations = Math.Abs(angle2 - angle1) / delta;
            for (int i = 0; i <= iterations; i++)
            {
                double x = X + R1 * Math.Cos(angle1);
                double y = Y - R2 * Math.Sin(angle1);
                allpoints.AddBody(x, y);
                angle1 += delta;
            }
            double x1 = X + R1 * Math.Cos(angle1);
            double y1 = Y - R2 * Math.Sin(angle1);
            allpoints.AddBody(x1, y1);
        }
       
        private void GenEyes(double X1, double Y1, double X2, double Y2, double R)
        {
            double angle1 = (0.0 / 180) * Math.PI;
            double angle2 = (360.0 / 180) * Math.PI;
            double delta = 1 / R;
            double iterations = Math.Abs(angle2 - angle1) / delta;

            for (int i = 0; i <= iterations; i++)
            {
                double x = X1 + R * Math.Cos(angle1);
                double y = Y1 - R * Math.Sin(angle1);
                allpoints.AddEye(x, y, 1);
                angle1 += delta;
            }
            double x1 = X1 + R * Math.Cos(angle1);
            double y1 = Y1 - R * Math.Sin(angle1);
            allpoints.AddEye(x1, y1, 1);

            for (int i = 0; i <= iterations; i++)
            {
                double x = X2 + R * Math.Cos(angle1);
                double y = Y2 - R * Math.Sin(angle1);
                allpoints.AddEye(x, y, 2);
                angle1 += delta;
            }
            x1 = X2 + R * Math.Cos(angle1);
            y1 = Y2 - R * Math.Sin(angle1);
            allpoints.AddEye(x1, y1, 2);
        }

        private void GenMouth(double X, double Y, double R)
        {
            double angle1 = (225.0 / 180) * Math.PI;
            double angle2 = (315.0 / 180) * Math.PI;
            double delta = 1 / R;
            double iterations = Math.Abs(angle2 - angle1) / delta;

            for (int i = 0; i <= iterations; i++)
            {
                double x = X + R * Math.Cos(angle1);
                double y = Y - R * Math.Sin(angle1);
                allpoints.AddMouth(x, y);
                angle1 += delta;
            }
            double x1 = X + R * Math.Cos(angle1);
            double y1 = Y - R * Math.Sin(angle1);
            allpoints.AddMouth(x1, y1);
        }

        private void GenFoot(double X0, double Y0, double r1, double r2)
        {
            double x = X0 + r1 * Math.Cos(260.0 / 180 * Math.PI);
            double y = Y0 - r2 * Math.Sin(260.0 / 180 * Math.PI);
            allpoints.AddPoint(x, y, 1);
            allpoints.AddPoint(x, y + 30, 1);
            allpoints.AddPoint(x - 20, y + 30, 1);

            x = X0 + r1 * Math.Cos(280.0 / 180 * Math.PI);
            y = Y0 - r2 * Math.Sin(280.0 / 180 * Math.PI);
            allpoints.AddPoint(x, y, 2);
            allpoints.AddPoint(x, y + 30, 2);
            allpoints.AddPoint(x + 20, y + 30, 2);

        }

        private void GenHand(double X0, double Y0)
        {
            double x = X0 - 25;
            double y = Y0 - 12.5;
            allpoints.AddPoint(x, y, 3);
            allpoints.AddPoint(x, y + 25, 3);
            allpoints.AddPoint(x - 50, y - 25, 3);

            x = X0 + 25;
            y = Y0 - 12.5;
            allpoints.AddPoint(x, y, 4);
            allpoints.AddPoint(x, y + 25, 4);
            allpoints.AddPoint(x + 37.5, y - 25, 4);
        }

        private void GenMustache(double X0, double Y0)
        {
            double x = X0 - 25;
            double y = Y0 - 62.5;
            allpoints.AddPoint(x, y, 5);
            allpoints.AddPoint(x+12.5, y, 5);
            allpoints.AddPoint(x+25, y+37.5, 5);
            allpoints.AddPoint(x+37.5, y-1, 5);
            allpoints.AddPoint(x+62.5, y-1, 5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allpoints = new DrawData();
            condition = new Stack<DrawData>();
            pan = panel1.CreateGraphics();
            float centerX = panel1.Width / 2;
            float centerY = panel1.Height / 2;
            
            GenHead(centerX, centerY-87.5, 37.5);
            GenBody(centerX, centerY+37.5, 50, 87.5);
            GenEyes(centerX-12.5,centerY-100,centerX+12.5,centerY-100,8);
            GenMouth(centerX, centerY - 87.5, 18);
            GenFoot(centerX, centerY + 37.5, 50, 87.5);
            GenHand(centerX, centerY + 37.5);
            GenMustache(centerX, centerY - 87.5);


            
            pan.FillRectangle(new SolidBrush(Color.White), 0, 0, panel1.Width, panel1.Height);
            allpoints.Draw(pan, pen1);
            if (condition.Count > 0)
                button5.Visible = true;
            condition.Push(allpoints);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double dx, dy;
            try
            {
                textBox1.Text = textBox1.Text.Replace('.', ',');
                textBox2.Text = textBox2.Text.Replace('.', ',');
                dx = Convert.ToDouble(textBox1.Text);
                dy = Convert.ToDouble(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные для операции переноса.");
                return;
            }
            
            condition.Push(allpoints);
            if (condition.Count > 0)
                button5.Visible = true;
            allpoints = allpoints.MovePic((float)dx,(float)dy);
            pan.FillRectangle(new SolidBrush(Color.White), 0, 0, panel1.Width, panel1.Height);
            allpoints.Draw(pan, pen1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double angle, x, y;
            try
            {
                textBox7.Text = textBox7.Text.Replace('.', ',');
                textBox8.Text = textBox8.Text.Replace('.', ',');
                textBox9.Text = textBox9.Text.Replace('.', ',');
                angle = Convert.ToDouble(textBox7.Text);
                x = Convert.ToDouble(textBox9.Text);
                y = Convert.ToDouble(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные для операции поворота.");
                return;
            }
            
            PointF centre = new PointF((float)x, (float)y);

            condition.Push(allpoints);
            if (condition.Count > 0)
                button5.Visible = true;
            allpoints = allpoints.TurnPic((float)angle, centre);
            pan.FillRectangle(new SolidBrush(Color.White), 0, 0, panel1.Width, panel1.Height);
            allpoints.Draw(pan, pen1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            allpoints = condition.Pop();
            if (condition.Count == 0)
                button5.Visible = false;
            pan.FillRectangle(new SolidBrush(Color.White), 0, 0, panel1.Width, panel1.Height);
            allpoints.Draw(pan, pen1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double kx, ky, x0, y0;
            try
            {
                textBox3.Text = textBox3.Text.Replace('.', ',');
                textBox4.Text = textBox4.Text.Replace('.', ',');
                textBox6.Text = textBox6.Text.Replace('.', ',');
                textBox5.Text = textBox5.Text.Replace('.', ',');
                kx = Convert.ToDouble(textBox6.Text);
                ky = Convert.ToDouble(textBox5.Text);
                x0 = Convert.ToDouble(textBox3.Text);
                y0 = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные для операции масштабирования.");
                return;
            }
            
            PointF centre = new PointF((float)x0, (float)y0);

            condition.Push(allpoints);
            if (condition.Count > 0)
                button5.Visible = true;
            allpoints = allpoints.ScalePic((float)kx, (float)ky, centre);
            pan.FillRectangle(new SolidBrush(Color.White), 0, 0, panel1.Width, panel1.Height);
            allpoints.Draw(pan, pen1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //allpoints = new DrawData();
            //condition = new Stack<DrawData>();
            //pan = panel1.CreateGraphics();
        }
    }
}
