using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAlab2
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private float ScaleCoef(List<PointT> points, PointF center)
        {
            float xmin = points[0].getx;
            float ymin = points[0].gety;
            float xmax = points[0].getx;
            float ymax = points[0].gety;
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].getx > xmax)
                    xmax = points[i].getx;
                if (points[i].getx < xmin)
                    xmin = points[i].getx;
                if (points[i].gety > ymax)
                    ymax = points[i].gety;
                if (points[i].gety < ymin)
                    ymin = points[i].gety;
            }
            if (xmax - xmin != 0 || ymax - ymin != 0)
            {
                float k1 = (center.X * 2-100) / (xmax - xmin);
                float k2 = (center.Y * 2-100) / (ymax - ymin);
                if (k1 > k2)
                    return k2;
                else
                    return k1;
            }
            else
                return 1;
            
        }

        private List<PointT> Scale(List<PointT> p, float k, PointF centre)
        {
            List<PointT> p_new = new List<PointT>();
            for (int i = 0; i < p.Count; i++)
            {
                float X = k * (p[i].getx) + centre.X * (1 - k);
                float Y = k * (p[i].gety) + centre.Y * (1 - k);
                float P = p[i].getp;
                p_new.Add(new PointT(X, Y, P));
            }
            return p_new;
        }

        private void DrawPoints(List<PointT> points, PointF center)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            Pen p = new Pen(Color.Black, 2);
            List<PointT> new_p = Scale(points, ScaleCoef(points,center), new PointF(0,0));
            for (int i = 0; i < points.Count; i++)
            {
                gr.DrawRectangle(p, center.X - new_p[i].getx, center.Y - new_p[i].gety, 1, 1);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            { 
                string filename = openFileDialog1.FileName;
                using (StreamReader myStream = new StreamReader(filename, Encoding.Default))
                {
                    int i = 0;
                    while (!myStream.EndOfStream)
                    {
                        string line = myStream.ReadLine();
                        double a, b, c;
                        double.TryParse(line.Split(new char[] { ' ' })[0], out a);
                        double.TryParse(line.Split(new char[] { ' ' })[2], out c);
                        double.TryParse(line.Split(new char[] { ' ' })[1], out b);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = a.ToString();
                        dataGridView1.Rows[i].Cells[1].Value = b;
                        dataGridView1.Rows[i].Cells[2].Value = c;
                        i++;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List <PointT> points = new List<PointT>();
            PointF center = new PointF(pictureBox1.Width/2, pictureBox1.Height/2);
            double x, y, p;
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                x = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                y = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                p = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                points.Add(new PointT((float)x, (float)y, (float)p));
                
            }
            DrawPoints(points, center);
        }
    }
}
