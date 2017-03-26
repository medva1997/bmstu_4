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
        private float F(float x, float k)
        {
            return (float)Math.Pow(x, k);
        }
        public Form1()
        {
            InitializeComponent();
            textBox2.Text = 3.ToString();
        }


        private void FillSlauMatrix(float[,]matrix, List<PointT> points, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    float sumA = 0, sumB = 0;
                    for (int k = 0; k < points.Count; k++)
                    {
                        sumA += points[k].getp * F(points[k].getx, i) * F(points[k].getx, j);
                        sumB += points[k].getp * points[k].gety * F(points[k].getx, j);
                    }
                    matrix[i, j] = sumA;
                    matrix[j, n] = sumB;
                }
            }
        }

        private void PrintMatrix(float[,]matrix, int n)
        {
            textBox1.Clear();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    textBox1.Text += (matrix[i,j].ToString() + " ");
                textBox1.Text += matrix[i,n].ToString()+ Environment.NewLine;
            }
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

        private PointF PicCenter(List<PointT> points)
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
            return(new PointF((xmin + xmax) / 2, (ymin + ymax) / 2));
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

        

        private float[] DrawPoints(bool Flag, List<PointT> points, PointF center, Color color, int size)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            //
            Pen p = new Pen(color, size);
            float coef = ScaleCoef(points, center);
            PointF c = PicCenter(points);
            List<PointT> new_p = Scale(points, coef, new PointF(0,0));
            if (Flag)
            {
                gr.Clear(Color.White);
                float[] str = new float[8]; //xmin xmax ymin ymax
                str[0] = center.X + new_p[0].getx - c.X * coef;
                str[1] = center.X + new_p[0].getx - c.X * coef;
                str[2] = center.Y - new_p[0].gety + c.Y * coef;
                str[3] = center.Y - new_p[0].gety + c.Y * coef;
                str[4] = points[0].getx;
                str[5] = points[0].getx;
                str[6] = points[0].gety;
                str[7] = points[0].gety;
                for (int i = 0; i < points.Count; i++)
                {
                    float x = center.X + new_p[i].getx - c.X * coef;
                    float y = center.Y - new_p[i].gety + c.Y * coef;
                    gr.DrawRectangle(p, x, y, 1, 1);
                    if (x < str[0])
                    {
                        str[0] = x;
                        str[4] = points[i].getx;
                    }
                    if (x > str[1])
                    {
                        str[1] = x;
                        str[5] = points[i].getx;
                    }
                    if (y < str[2])
                    {
                        str[2] = y;
                        str[6] = points[i].gety;
                    }
                    if (y > str[3])
                    {
                        str[3] = y;
                        str[7] = points[i].gety;
                    }
                }
                return str;
            }
            else
            {
                for (int i = 0; i < points.Count-1; i++)
                {
                    float x1 = center.X + new_p[i].getx - c.X * coef;
                    float y1 = center.Y - new_p[i].gety + c.Y * coef;
                    float x2 = center.X + new_p[i+1].getx - c.X * coef;
                    float y2 = center.Y - new_p[i+1].gety + c.Y * coef;
                    gr.DrawLine(p, x1, y1, x2, y2);
                }
                return null;
            }
        }


        private float[] Gauss(float[,] matrix, int rowCount, int colCount)
        {
            int i;
            int[] mask = new int[colCount - 1];
            for (i = 0; i < colCount - 1; i++) mask[i] = i;
            if (GaussDirectPass(ref matrix, ref mask, colCount, rowCount))
            {
                float[] answer = GaussReversePass(ref matrix, mask, colCount, rowCount);
                return answer;
            }
            else return null;
        }

        private bool GaussDirectPass(ref float[,] matrix, ref int[] mask,
            int colCount, int rowCount)
        {
            int i, j, k, maxId, tmpInt;
            float maxVal, tempDouble;
            for (i = 0; i < rowCount; i++)
            {
                maxId = i;
                maxVal = matrix[i, i];
                for (j = i + 1; j < colCount - 1; j++)
                    if (Math.Abs(maxVal) < Math.Abs(matrix[i, j]))
                    {
                        maxVal = matrix[i, j];
                        maxId = j;
                    }
                if (maxVal == 0) return false;
                if (i != maxId)
                {
                    for (j = 0; j < rowCount; j++)
                    {
                        tempDouble = matrix[j, i];
                        matrix[j, i] = matrix[j, maxId];
                        matrix[j, maxId] = tempDouble;
                    }
                    tmpInt = mask[i];
                    mask[i] = mask[maxId];
                    mask[maxId] = tmpInt;
                }
                for (j = 0; j < colCount; j++) matrix[i, j] /= maxVal;
                for (j = i + 1; j < rowCount; j++)
                {
                    float tempMn = matrix[j, i];
                    for (k = 0; k < colCount; k++)
                        matrix[j, k] -= matrix[i, k] * tempMn;
                }
            }
            return true;
        }

        private float[] GaussReversePass(ref float[,] matrix, int[] mask,
            int colCount, int rowCount)
        {
            int i, j, k;
            for (i = rowCount - 1; i >= 0; i--)
                for (j = i - 1; j >= 0; j--)
                {
                    float tempMn = matrix[j, i];
                    for (k = 0; k < colCount; k++)
                        matrix[j, k] -= matrix[i, k] * tempMn;
                }
            float[] answer = new float[rowCount];
            for (i = 0; i < rowCount; i++) answer[mask[i]] = matrix[i, colCount - 1];
            return answer;
        }

        private void DrawGraph(float[] C, List<PointT> points, float[] borders, int n, PointF center)
        {
            float N;
            if (borders[1] - borders[0] > borders[3] - borders[1])
                N = borders[1] - borders[0];
            else
                N = borders[3] - borders[1];
            float h = (borders[5] - borders[4]) / N;
            List<PointT> graph = new List<PointT>();
            for (float i = borders[4]; i < borders[5]; i += h)
            {
                float Y = C[0];
                for (int j = 1; j < n; j++)
                    Y += (float)(Math.Pow(i, j) * C[j]);
                graph.Add(new PointT(i, Y, 1));
            }
            DrawPoints(false, graph, center, Color.Green, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
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
                        dataGridView1.Rows[i].Cells[0].Value = a;
                        dataGridView1.Rows[i].Cells[1].Value = b;
                        dataGridView1.Rows[i].Cells[2].Value = c;
                        i++;
                    }
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            float p1 = 0f;
            float p2 = 7f;
            int N = 100;
            float h = (p2 - p1) / N;
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
            float[] str = DrawPoints(true, points, center, Color.Black, 2);
            int n = Convert.ToInt32(textBox2.Text);
            float[,] matrix = new float[n + 1, n + 2];
            FillSlauMatrix(matrix, points, n+1);
            PrintMatrix(matrix, n+1);
            float[] result = Gauss(matrix, n + 1, n + 2);
            for (int i = 0; i < n + 1; i++)
                textBox1.Text += "C" + i.ToString() + " = " + result[i].ToString()+ Environment.NewLine;
            DrawGraph(result, points, str, n+1, center);
            
        }

    }
}
