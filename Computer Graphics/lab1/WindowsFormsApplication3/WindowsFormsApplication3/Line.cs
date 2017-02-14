using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Line
    {
        double A, B, C;

        public Line(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }
        
        public Line gettanget(Point c1, Point c2, double r1, double r2, int k)
        { 
            
            double d = Math.Sqrt(Math.Pow(c2.getx - c1.getx, 2) + Math.Pow(c2.gety - c1.gety,2));
            //System.Windows.Forms.MessageBox.Show(d.ToString());
            double X = (c2.getx - c1.getx) / d;
            double Y = (c2.gety - c1.gety) / d;
            double R = (r2 - r1) / d;
            
            double a = R * X - k * Y * Math.Sqrt(1 - R*R);
            double b = R * Y + k * X * Math.Sqrt(1 - R*R);
            double c = r1 - (a * c1.getx + b * c1.gety);

            return (new Line(a, b, c));
        }
        
        /*
        public Line(Point c1, Point c2, double r1, double r2, int k)
        {
            double d = Math.Sqrt(Math.Pow(c2.getx - c1.getx, 2) + Math.Pow(c2.gety - c1.gety, 2));
            double x = (c2.getx - c1.getx) / d;
            double y = (c2.gety - c1.gety) / d;
            double R = (r2 - r1) / d;
            
            //double A = R * x - k * y * Math.Sqrt(1 - Math.Pow(R, 2));
            //double B = R * y + k * x * Math.Sqrt(1 - Math.Pow(R, 2));
            //double C = r1 - (A * c1.getx + B * c2.gety);
            
            double B = R * y + k * x * Math.Sqrt(1 - Math.Pow(R, 2) + Math.Pow(y, 2));
            double A = (R - R * Math.Pow(y, 2) - k * x * y * Math.Sqrt(1 - Math.Pow(R, 2) + Math.Pow(y, 2))) / x;
            double C = r1 - (A * c1.getx + B * c2.gety);
        }
        */     
        
        public double getA
        {
            get
            {
                return A;
            }
        }

        public double getB
        {
            get
            {
                return B;
            }
        }

        public double getC
        {
            get
            {
                return C;
            }
        }
    }
}
