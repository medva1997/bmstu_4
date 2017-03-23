using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics canvas;
        //Pen draw = new Pen(Color.Black, 1);
        //Brush drawb = new SolidBrush(Color.Black);
        //Pen fon = new Pen(Color.White, 1);
        //Brush fonb = new SolidBrush(Color.White);
        Color draw = Color.Black;
        Color fon = Color.White;
        double x0, y0, xf, yf;
        double xc, yc, r, alpha;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ClearCanvas()
        {
            canvas.Clear(Color.White);
        }

        public int IsCoordsChange(double x1, double y1, double x2, double y2)
        {
            if (x1 == x0 && y1 == y0 && x2 == xf && y2 == yf)
                return 1;
            else
                return 0;
        }

        public int IsParamsChange(double x, double y, double R, double A)
        {
            if (x == xc && y == yc && R == r && A == alpha)
                return 1;
            else
                return 0;
        }
        private void StdDrawLine(double x1, double y1, double x2, double y2, Color clr)
        {
            Pen p = new Pen(clr, 1);
            canvas.DrawLine(p, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        private int sign(double x)
        {
            if (x > 0)
                return 1;
            else if (x == 0)
                return 0;
            else
                return -1;
        }

        private void CdaDrawLine(double x1, double y1, double x2, double y2, Color clr)
        {
            Brush br = new SolidBrush(clr);
            if (x1 == x2 && y1 == y2)
                canvas.FillRectangle(br, (float)x1, (float)y1, 1, 1);
            else
            {
                double l;
                if (Math.Abs(x2 - x1) > Math.Abs(y2 - y1))
                    l = Math.Abs(x2 - x1);
                else
                    l = Math.Abs(y2 - y1);
                double dx = (x2 - x1) / l;
                double dy = (y2 - y1) / l;
                double x = x1;
                double y = y1;
                for (int i = 0; i < l; i++)
                {
                    canvas.FillRectangle(br, (float)x, (float)y, 1, 1);
                    x += dx;
                    y += dy;
                }
                //MessageBox.Show(x.ToString() + "   " + y.ToString());
            }
        }

        private void DrawSun(double x, double y, double R, double a, Color clr, Action<double, double, double, double, Color> FuncLine)
        {
            float angle = (float)(a / 180 * Math.PI);
            int N = (int)(2 * Math.PI / angle);
            for (float i = 0; i <= 2*Math.PI-angle; i+=angle)
            {
                FuncLine(x, y, x + Math.Cos(i) * R, y + Math.Sin(i) * R, clr);
                
            }
        }

        private void BresenhamInt(double x1, double y1, double x2, double y2, Color clr)
        {
            Brush br = new SolidBrush(clr);
            if (x1 == x2 && y1 == y2)
                canvas.FillRectangle(br, (float)x1, (float)y1, 1, 1);
            else
            {
                double dx = Math.Abs(x2 - x1);
                double dy = Math.Abs(y2 - y1);
                double stepx = sign(x2 - x1);
                double stepy = sign(y2 - y1);
                //double m = dy / dx;
                double flag;
                if (dy > dx)
                {
                    double tmp = dx;
                    dx = dy;
                    dy = tmp;
                    //m = 1 / m;
                    flag = 1;
                }
                else
                    flag = 0;
                //double f = m - 0.5;
                double f1 = 2 * dy - dx;
                double x = x1;
                double y = y1;
                for (int i = 0; i < dx; i++)
                {
                    canvas.FillRectangle(br, (float)x, (float)y, 1, 1);
                    if (f1 >= 0)
                    {
                        if (flag == 1)
                            x += stepx;
                        else
                            y += stepy;
                        //f -= 1;
                        f1 -= 2 * dx;
                    }
                    if (f1 < 0)
                    {
                        if (flag == 1)
                            y += stepy;
                        else
                            x += stepx;

                    }
                    f1 += 2 * dy;
                    //f += m;
                }
                //MessageBox.Show(x.ToString() + "   " + y.ToString());
            }
        }

        private void BresenhamFloat(double x1, double y1, double x2, double y2, Color clr)
        {
            Brush br = new SolidBrush(clr);
            if (x1 == x2 && y1 == y2)
                canvas.FillRectangle(br, (float)x1, (float)y1, 1, 1);
            else
            {
                double dx = Math.Abs(x2 - x1);
                double dy = Math.Abs(y2 - y1);
                double stepx = sign(x2 - x1);
                double stepy = sign(y2 - y1);
                double m = dy / dx;
                double flag;
                if (m > 1)
                {
                    double tmp = dx;
                    dx = dy;
                    dy = tmp;
                    m = 1 / m;
                    flag = 1;
                }
                else
                    flag = 0;
                double f = m - 0.5;
                double x = x1;
                double y = y1;
                for (int i = 0; i < dx; i++)
                {
                    canvas.FillRectangle(br, (float)x, (float)y, 1, 1);
                    if (f >= 0)
                    {
                        if (flag == 1)
                            x += stepx;
                        else
                            y += stepy;
                        f -= 1;
                    }
                    if (f < 0)
                    {
                        if (flag == 1)
                            y += stepy;
                        else
                            x += stepx;

                    }
                    f += m;
                }
                //MessageBox.Show(x.ToString() + "   " + y.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canvas = panel1.CreateGraphics();
            try
            {
                textBox1.Text = textBox1.Text.Replace('.', ',');
                textBox2.Text = textBox2.Text.Replace('.', ',');
                textBox3.Text = textBox3.Text.Replace('.', ',');
                textBox4.Text = textBox4.Text.Replace('.', ',');
                if (IsCoordsChange(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text)) == 0)
                    ClearCanvas();
                x0 = Convert.ToDouble(textBox1.Text);
                y0 = Convert.ToDouble(textBox2.Text);
                xf = Convert.ToDouble(textBox3.Text);
                yf = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные(1)");
                return;
            }
            if (checkBox1.Checked == false)
                CdaDrawLine(x0, y0, xf, yf, draw);
            else
                CdaDrawLine(x0, y0, xf, yf, fon);

            canvas = panel2.CreateGraphics();
            try
            {
                textBox5.Text = textBox5.Text.Replace('.', ',');
                textBox6.Text = textBox6.Text.Replace('.', ',');
                textBox7.Text = textBox7.Text.Replace('.', ',');
                textBox8.Text = textBox8.Text.Replace('.', ',');
                if (IsParamsChange(Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox7.Text), Convert.ToDouble(textBox8.Text)) == 0)
                    ClearCanvas();
                xc = Convert.ToDouble(textBox5.Text);
                yc = Convert.ToDouble(textBox6.Text);
                r = Convert.ToDouble(textBox7.Text);
                alpha = Convert.ToDouble(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные(2)");
                return;
            }
            if (checkBox1.Checked == false)
                DrawSun(xc, yc, r, alpha, draw, CdaDrawLine);
            else
                DrawSun(xc, yc, r, alpha, fon, CdaDrawLine);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            canvas = panel1.CreateGraphics();
            try
            {
                textBox1.Text = textBox1.Text.Replace('.', ',');
                textBox2.Text = textBox2.Text.Replace('.', ',');
                textBox3.Text = textBox3.Text.Replace('.', ',');
                textBox4.Text = textBox4.Text.Replace('.', ',');
                if (IsCoordsChange(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text)) == 0)
                    ClearCanvas();
                x0 = Convert.ToDouble(textBox1.Text);
                y0 = Convert.ToDouble(textBox2.Text);
                xf = Convert.ToDouble(textBox3.Text);
                yf = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные");
                return;
            }
            if (checkBox1.Checked == false)
                BresenhamFloat(x0, y0, xf, yf, draw);
            else
                BresenhamFloat(x0, y0, xf, yf, fon);

            canvas = panel2.CreateGraphics();
            try
            {
                textBox5.Text = textBox5.Text.Replace('.', ',');
                textBox6.Text = textBox6.Text.Replace('.', ',');
                textBox7.Text = textBox7.Text.Replace('.', ',');
                textBox8.Text = textBox8.Text.Replace('.', ',');
                if (IsParamsChange(Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox7.Text), Convert.ToDouble(textBox8.Text)) == 0)
                    ClearCanvas();
                xc = Convert.ToDouble(textBox5.Text);
                yc = Convert.ToDouble(textBox6.Text);
                r = Convert.ToDouble(textBox7.Text);
                alpha = Convert.ToDouble(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные(2)");
                return;
            }
            if (checkBox1.Checked == false)
                DrawSun(xc, yc, r, alpha, draw, BresenhamFloat);
            else
                DrawSun(xc, yc, r, alpha, fon, BresenhamFloat);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            canvas = panel1.CreateGraphics();
            try
            {
                textBox1.Text = textBox1.Text.Replace('.', ',');
                textBox2.Text = textBox2.Text.Replace('.', ',');
                textBox3.Text = textBox3.Text.Replace('.', ',');
                textBox4.Text = textBox4.Text.Replace('.', ',');
                if (IsCoordsChange(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text)) == 0)
                    ClearCanvas();
                x0 = Convert.ToDouble(textBox1.Text);
                y0 = Convert.ToDouble(textBox2.Text);
                xf = Convert.ToDouble(textBox3.Text);
                yf = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные");
                return;
            }
            if (checkBox1.Checked == false)
                BresenhamInt(x0, y0, xf, yf, draw);
            else
                BresenhamInt(x0, y0, xf, yf, fon);

            canvas = panel2.CreateGraphics();
            try
            {
                textBox5.Text = textBox5.Text.Replace('.', ',');
                textBox6.Text = textBox6.Text.Replace('.', ',');
                textBox7.Text = textBox7.Text.Replace('.', ',');
                textBox8.Text = textBox8.Text.Replace('.', ',');
                if (IsParamsChange(Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox7.Text), Convert.ToDouble(textBox8.Text)) == 0)
                    ClearCanvas();
                xc = Convert.ToDouble(textBox5.Text);
                yc = Convert.ToDouble(textBox6.Text);
                r = Convert.ToDouble(textBox7.Text);
                alpha = Convert.ToDouble(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные(2)");
                return;
            }
            if (checkBox1.Checked == false)
                DrawSun(xc, yc, r, alpha, draw, BresenhamInt);
            else
                DrawSun(xc, yc, r, alpha, fon, BresenhamInt);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            canvas = panel1.CreateGraphics();
            
            try
            {
                textBox1.Text = textBox1.Text.Replace('.', ',');
                textBox2.Text = textBox2.Text.Replace('.', ',');
                textBox3.Text = textBox3.Text.Replace('.', ',');
                textBox4.Text = textBox4.Text.Replace('.', ',');
                if (IsCoordsChange(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text)) == 0)
                    ClearCanvas();
                x0 = Convert.ToDouble(textBox1.Text);
                y0 = Convert.ToDouble(textBox2.Text);
                xf = Convert.ToDouble(textBox3.Text);
                yf = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные");
                return;
            }
            if (checkBox1.Checked == false)
                StdDrawLine(x0, y0, xf, yf, draw);
            else
                StdDrawLine(x0, y0, xf, yf, fon);

            canvas = panel2.CreateGraphics();
            try
            {
                textBox5.Text = textBox5.Text.Replace('.', ',');
                textBox6.Text = textBox6.Text.Replace('.', ',');
                textBox7.Text = textBox7.Text.Replace('.', ',');
                textBox8.Text = textBox8.Text.Replace('.', ',');
                if (IsParamsChange(Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox7.Text), Convert.ToDouble(textBox8.Text)) == 0)
                    ClearCanvas();
                xc = Convert.ToDouble(textBox5.Text);
                yc = Convert.ToDouble(textBox6.Text);
                r = Convert.ToDouble(textBox7.Text);
                alpha = Convert.ToDouble(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные(2)");
                return;
            }
            if (checkBox1.Checked == false)
                DrawSun(xc, yc, r, alpha, draw, StdDrawLine);
            else
                DrawSun(xc, yc, r, alpha, fon, StdDrawLine);
        }
    }
}
