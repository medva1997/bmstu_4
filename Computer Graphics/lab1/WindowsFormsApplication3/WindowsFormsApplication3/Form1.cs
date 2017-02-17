using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
        }

        private double area(Point a, Point b, Point c, Point d)
        {
            double ab = a.distance(b);
            double bc = b.distance(c);
            double cd = c.distance(d);
            double ad = a.distance(d);
            double p = (ab + bc + cd + ad) / 2;
            return Math.Sqrt(p*(p - ab) * (p - bc) * (p - cd) * (p - ad));
        }


        private double max(double p1, double p2, double r1, double r2)
        {
            if (p1 > p2)
                return p1 + r1;
            else
                return p2 + r2;
        }

        private double min(double p1, double p2, double r1, double r2)
        {
            if (p1 > p2)
                return p2 - r2;
            else
                return p1 - r1;
        }

        private Point getmiddle(Point o1, Point o2, double r1, double r2)
        {

            double xmin = min(o1.getx, o2.getx, r1, r2);
            double xmax = max(o1.getx, o2.getx, r1, r2);
            double ymin = min(o1.gety, o2.gety, r1, r2);
            double ymax = max(o1.gety, o2.gety, r1, r2);
            //System.Windows.Forms.MessageBox.Show("xmin,xmax,ymin,ymax"+xmin.ToString() + ' ' + xmax.ToString() + ' ' + ymin.ToString() + ' ' + ymax.ToString());
            return (new Point((xmax + xmin) / 2, (ymax + ymin) / 2));
        }

        private float sc(float k, float xo, float xm)
        {
            return k * xo + xm * (1 - k);
        }

        private Point s(Point O, Point A, float dx, float dy, float k)
        {
            double x = k * (A.getx + dx) + O.getx * (1 - k);
            double y = k * (A.gety + dy) + O.gety * (1 - k);
            return (new Point(x, y));
        }

        private void answinlabel()
        {
            label1.Visible = true;
            label5.Text = ('(' + (resp1.getx).ToString("0.###") + "; " + (resp1.gety).ToString("0.###") + ')');
            label5.Text += (" (" + (resp2.getx).ToString("0.###") + "; " + (resp2.gety).ToString("0.###") + ')');
            label5.Text += (" (" + (resp3.getx).ToString("0.###") + "; " + (resp3.gety).ToString("0.###") + ')');
            label4.Visible = true;
            label6.Text = ("(" + (resp4.getx).ToString("0.###") + "; " + (resp4.gety).ToString("0.###") + ')');
            label6.Text += (" (" + (resp5.getx).ToString("0.###") + "; " + (resp5.gety).ToString("0.###") + ')');
            label6.Text += (" (" + (resp6.getx).ToString("0.###") + "; " + (resp6.gety).ToString("0.###") + ')');
            label8.Visible = true;
            label7.Text = ('(' + (resinters11.getx).ToString("0.###") + "; " + (resinters11.gety).ToString("0.###") + ')');
            label7.Text += (" (" + (resinters12.getx).ToString("0.###") + "; " + (resinters12.gety).ToString("0.###") + ")\n");
            label7.Text += (" (" + (resinters22.getx).ToString("0.###") + "; " + (resinters22.gety).ToString("0.###") + ')');
            label7.Text += (" (" + (resinters21.getx).ToString("0.###") + "; " + (resinters21.gety).ToString("0.###") + ')');
            checkBox1.Visible = true;
        }

        private void graph()
        {
            Graphics graph = panel1.CreateGraphics();

            Pen pointpen1 = new Pen(Color.Red, 4);
            //точки на первой окружности 

            graph.DrawRectangle(pointpen1, sc(k, (float)resp1.getx + dx, (float)mid.getx), sc(k, LEN - (float)resp1.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen1, sc(k, (float)resp2.getx + dx, (float)mid.getx), sc(k, LEN - (float)resp2.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen1, sc(k, (float)resp3.getx + dx, (float)mid.getx), sc(k, LEN - (float)resp3.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen1, sc(k, (float)reso1.getx + dx, (float)mid.getx), sc(k, LEN - (float)reso1.gety + dy, (float)mid.gety), 1, 1);
            /*
            graph.DrawRectangle(pointpen1, (float)s(mid, resp1, dx, dy, k).getx, LEN - (float)s(mid, resp1, dx, dy, k).gety, 1, 1);
            graph.DrawRectangle(pointpen1, (float)s(mid, resp2, dx, dy, k).getx, LEN - (float)s(mid, resp2, dx, dy, k).gety, 1, 1);
            graph.DrawRectangle(pointpen1, (float)s(mid, resp3, dx, dy, k).getx, LEN - (float)s(mid, resp3, dx, dy, k).gety, 1, 1);
            graph.DrawRectangle(pointpen1, (float)s(mid, reso1, dx, dy, k).getx, LEN - (float)s(mid, reso1, dx, dy, k).gety, 1, 1);
            */
            //точки на второй окружности
            graph.DrawRectangle(pointpen1, sc(k, (float)resp4.getx + dx, (float)mid.getx), sc(k, LEN - (float)resp4.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen1, sc(k, (float)resp5.getx + dx, (float)mid.getx), sc(k, LEN - (float)resp5.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen1, sc(k, (float)resp6.getx + dx, (float)mid.getx), sc(k, LEN - (float)resp6.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen1, sc(k, (float)reso2.getx + dx, (float)mid.getx), sc(k, LEN - (float)reso2.gety + dy, (float)mid.gety), 1, 1);

            //окружности
            Pen arcpen1 = new Pen(Color.Black, 1);
            graph.DrawArc(arcpen1, sc(k, (float)(reso1.getx - resr1) + dx, (float)mid.getx), sc(k, (float)(LEN - reso1.gety - resr1 + dy), (float)mid.gety), (float)(2 * k * resr1), (float)(2 * k * resr1), 0, 360);
            graph.DrawArc(arcpen1, sc(k, (float)(reso2.getx - resr2) + dx, (float)mid.getx), sc(k, (float)(LEN - reso2.gety - resr2 + dy), (float)mid.gety), (float)(2 * k * resr2), (float)(2 * k * resr2), 0, 360);

            //точки касания
            Pen pointpen2 = new Pen(Color.Green, 4);
            graph.DrawRectangle(pointpen2, sc(k, (float)resinters11.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters11.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen2, sc(k, (float)resinters12.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters12.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen2, sc(k, (float)resinters21.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters21.gety + dy, (float)mid.gety), 1, 1);
            graph.DrawRectangle(pointpen2, sc(k, (float)resinters22.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters22.gety + dy, (float)mid.gety), 1, 1);
            //graph.DrawRectangle(pointpen2, sc(k, (float)rescross.getx + dx, (float)mid.getx), sc(k, LEN - (float)rescross.gety + dy, (float)mid.gety), 1, 1);

            //касательные
            Pen linepen = new Pen(Color.Black, 2);
            graph.DrawLine(linepen, sc(k, (float)resinters11.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters11.gety + dy, (float)mid.gety), sc(k, (float)resinters21.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters21.gety + dy, (float)mid.gety));
            graph.DrawLine(linepen, sc(k, (float)resinters12.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters12.gety + dy, (float)mid.gety), sc(k, (float)resinters22.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters22.gety + dy, (float)mid.gety));

            //радиусы
            graph.DrawLine(linepen, sc(k, (float)resinters11.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters11.gety + dy, (float)mid.gety), sc(k, (float)reso1.getx + dx, (float)mid.getx), sc(k, LEN - (float)reso1.gety + dy, (float)mid.gety));
            graph.DrawLine(linepen, sc(k, (float)resinters12.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters12.gety + dy, (float)mid.gety), sc(k, (float)reso1.getx + dx, (float)mid.getx), sc(k, LEN - (float)reso1.gety + dy, (float)mid.gety));
            graph.DrawLine(linepen, sc(k, (float)resinters21.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters21.gety + dy, (float)mid.gety), sc(k, (float)reso2.getx + dx, (float)mid.getx), sc(k, LEN - (float)reso2.gety + dy, (float)mid.gety));
            graph.DrawLine(linepen, sc(k, (float)resinters22.getx + dx, (float)mid.getx), sc(k, LEN - (float)resinters22.gety + dy, (float)mid.gety), sc(k, (float)reso2.getx + dx, (float)mid.getx), sc(k, LEN - (float)reso2.gety + dy, (float)mid.gety));
        }

        Point resp1 = new Point(0, 0);
        Point resp2 = new Point(0, 0);
        Point resp3 = new Point(0, 0);
        Point resp4 = new Point(0, 0);
        Point resp5 = new Point(0, 0);
        Point resp6 = new Point(0, 0);
        Point reso2 = new Point(0, 0);
        Point reso1 = new Point(0, 0);
        Point resinters11 = new Point(0, 0);
        Point resinters12 = new Point(0, 0);
        Point resinters21 = new Point(0, 0);
        Point resinters22 = new Point(0, 0);
        Point rescross = new Point(0, 0);
        double resr1 = 0.0;
        double resr2 = 0.0;
        float LEN;
        Point mid = new Point(0,0);
        float dx, dy, k;


        private void button1_Click(object sender, EventArgs e)
        {
            //clear
            label5.Text = "";
            label6.Text = "";
            panel1.Invalidate();
            panel1.Update();
            p1.Text = "";
            p2.Text = "";
            p3.Text = "";
            p4.Text = "";
            p5.Text = "";
            p6.Text = "";
            checkBox1.Checked = false;

            ;
            int count1 = dataGridView1.RowCount - 1;
            int count2 = dataGridView2.RowCount - 1;
            List<Point> listofpoints1 = new List<Point>();
            List<Point> listofpoints2 = new List<Point>();
            double x, y;
            for (int i = 0; i < count1; i++)
            {
                try
                {
                    x = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                    y = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                }
                catch
                {
                    MessageBox.Show("Найдено некорректно заполненое поле в строке " + (i + 1).ToString());
                    return;
                }
                listofpoints1.Add(new Point (x, y));
            }

            for (int i = 0; i < count2; i++)
            {
                try
                {
                    x = Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value);
                    y = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                }
                catch
                {
                    MessageBox.Show("Найдено некорректно заполненое поле в строке " + (i + 1).ToString());
                    return;
                }
                listofpoints2.Add(new Point(x, y));
            }

            bool Flag = false;
            double maxdiffsquare = -1;

            Line restanget1 = new Line(0, 0, 0); 
            Line restanget2 = new Line(0,0,0);


            for (int i = 0; i < count1-2; i++)
            {
                for (int j = i+1; j < count1-1; j++)
                {
                    for (int k = j+1; k < count1; k++)
                    {
                        if (listofpoints1[i].isarc(listofpoints1[j], listofpoints1[k]) == 1)
                        {
                            for (int l = 0; l < count2-2; l++)
                            {
                                for (int m = l+1; m < count2-1; m++)
                                {
                                    for (int n = m+1; n < count2; n++)
                                    {
                                        if (listofpoints2[l].isarc(listofpoints2[m], listofpoints2[n]) == 1)
                                        {
                                           
                                            //центры окружностей и радиусы
                                            Point o1 = listofpoints1[i].getcenter(listofpoints1[j], listofpoints1[k]);
                                            Point o2 = listofpoints2[l].getcenter(listofpoints2[m], listofpoints2[n]);
                                            double r1 = listofpoints1[i].getradius(o1);
                                            double r2 = listofpoints2[l].getradius(o2);
                                            //проверка на пересечение окружностей
                                            if ((o1.getx-o2.getx) * (o1.getx - o2.getx) + (o1.gety - o2.gety) * (o1.gety- o2.gety) <= (r1+r2)*(r1+r2))
                                                break;
                                            
                                            //общие внутренние касательные
                                            Line t = new Line(0, 0, 0);
                                            Line tanget1 = t.gettanget(o1, o2, r1, -r2, 1);
                                            Line tanget2 = t.gettanget(o1, o2, -r1, r2, 1);
                                            //tanget1 = new Line(o1, o2, r1, -r2, 1);
                                            //tanget2 = new Line(o1, o2, -r1, r2, 1);

                                            //точки пересечения окружностей и касательных
                                            Point inters11 = o1.intersection(tanget1, r1);
                                            Point inters12 = o1.intersection(tanget2, r1);
                                            Point inters21 = o2.intersection(tanget1, r2);
                                            Point inters22 = o2.intersection(tanget2, r2);
                                            Point cross = new Point(tanget1, tanget2);

                                            if (Math.Abs(area(inters11,o1,inters12,cross)-area(inters21,o2,inters22,cross))> maxdiffsquare)
                                            {
                                                maxdiffsquare = Math.Abs(area(inters11, o1, inters12, cross) - area(inters21, o2, inters22, cross));
                                                resp1 = listofpoints1[i];
                                                resp2 = listofpoints1[j];
                                                resp3 = listofpoints1[k];
                                                resp4 = listofpoints2[l];
                                                resp5 = listofpoints2[m];
                                                resp6 = listofpoints2[n];
                                                reso1 = o1;
                                                reso2 = o2;
                                                resr1 = r1;
                                                resr2 = r2;
                                                restanget1 = tanget1;
                                                restanget2 = tanget2;
                                                resinters11 = inters11;
                                                resinters12 = inters12;
                                                resinters21 = inters21;
                                                resinters22 = inters22;
                                                rescross = cross;
                              
                                                label9.Visible = true;
                                                label10.Visible = true;
                                                label10.Text = (maxdiffsquare.ToString("0.####")+" кв.единиц"); 
                                            }  
                                            Flag = true;

                                        }
                                    }
                                }
                            }
                        }                                       
                    }
                }
            }
            if (Flag == true)
            {

                //вывод результата: label5 - 1,2,3 точки, label6 - 4,5,6 точки, label7 - точки касания
                answinlabel();

                LEN = panel1.Height;
                mid = new Point(panel1.Width / 2, panel1.Height / 2);
                dx = (float)(mid.getx - getmiddle(reso1, reso2,resr1,resr2).getx);
                dy = (float)(mid.gety - (LEN - getmiddle(reso1, reso2, resr1, resr2).gety));
                float k1 = (LEN - 30) / (float)(max(reso1.gety, reso2.gety, resr1, resr2) - min(reso1.gety, reso2.gety, resr1, resr2));
                float k2 = (panel1.Width-30)/(float)(max(reso1.getx,reso2.getx,resr1,resr2) - min(reso1.getx, reso2.getx,resr1,resr2));
                if (k1 > k2)
                    k = k2;
                else
                    k = k1;
                graph();
 
            }
            else
                MessageBox.Show("Точки не найдены");

            
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            button1.BackColor = Color.Pink;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                
                p1.Text = ('(' + (resp1.getx).ToString("0.##") + "; " + (resp1.gety).ToString("0.##") + ')');
                p1.Location = new System.Drawing.Point((int)(sc(k, (float)resp1.getx + dx, (float)mid.getx)), (int)(sc(k, LEN - (float)resp1.gety + dy, (float)mid.gety) + 18));
                p2.Text = ('(' + (resp2.getx).ToString("0.##") + "; " + (resp2.gety).ToString("0.##") + ')');
                p2.Location = new System.Drawing.Point((int)(sc(k, (float)resp2.getx + dx, (float)mid.getx)), (int)(sc(k, LEN - (float)resp2.gety + dy, (float)mid.gety) + 18));
                p3.Text = ('(' + (resp3.getx).ToString("0.##") + "; " + (resp3.gety).ToString("0.##") + ')');
                p3.Location = new System.Drawing.Point((int)(sc(k, (float)resp3.getx + dx, (float)mid.getx)), (int)(sc(k, LEN - (float)resp3.gety + dy, (float)mid.gety) + 18));
                p4.Text = ('(' + (resp4.getx).ToString("0.##") + "; " + (resp4.gety).ToString("0.##") + ')');
                p4.Location = new System.Drawing.Point((int)(sc(k, (float)resp4.getx + dx, (float)mid.getx)), (int)(sc(k, LEN - (float)resp4.gety + dy, (float)mid.gety) + 18));
                p5.Text = ('(' + (resp5.getx).ToString("0.##") + "; " + (resp5.gety).ToString("0.##") + ')');
                p5.Location = new System.Drawing.Point((int)(sc(k, (float)resp5.getx + dx, (float)mid.getx)), (int)(sc(k, LEN - (float)resp5.gety + dy, (float)mid.gety) + 18));
                p6.Text = ('(' + (resp6.getx).ToString("0.##") + "; " + (resp6.gety).ToString("0.##") + ')');
                p6.Location = new System.Drawing.Point((int)(sc(k, (float)resp6.getx + dx, (float)mid.getx)), (int)(sc(k, LEN - (float)resp6.gety + dy, (float)mid.gety) + 18));
                graph();
            }
            else
            {
                
                p1.Text = "";
                p2.Text = "";
                p3.Text = "";
                p4.Text = "";
                p5.Text = "";
                p6.Text = "";
                graph();
            }

        }
    }
}
