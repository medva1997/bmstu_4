using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication3
{
    class Point
    {
        double X,Y;

        public Point(double xvalue, double yvalue)
        {
            X = xvalue;
            Y = yvalue;
        }

        public double distance(Point point2)
        {
            return Math.Sqrt((X - point2.X)* (X - point2.X)+ (Y - point2.Y)* (Y - point2.Y));
        }

        public int isarc(Point point2, Point point3)
        {
            double L12 = Math.Pow(X, 2) - Math.Pow(point2.X, 2) + Math.Pow(Y, 2) - Math.Pow(point2.Y, 2);
            double L13 = Math.Pow(X, 2) - Math.Pow(point3.X, 2) + Math.Pow(Y, 2) - Math.Pow(point3.Y, 2);
            if (X != point2.X)
            {
                double y0 = ((X - point2.X) * L13 - (X - point3.X) * L12) / (2 * (Y - point3.Y) * (X - point2.X) - 2 * (Y - point2.Y) * (X - point3.X));
                double x0 = (L12 - 2 * y0 * (Y - point2.Y)) / (2 * (X - point2.X));
                if (Math.Pow(point2.X - x0, 2) + Math.Pow(point2.Y - y0, 2) == Math.Pow(point3.X - x0, 2) + Math.Pow(point3.Y - y0, 2))
                    return 1;
                else return 0;
            }
            else if (X != point3.X)
            {
                double y0 = ((X - point2.X) * L13 - (X - point3.X) * L12) / (2 * (Y - point3.Y) * (X - point2.X) - 2 * (Y - point2.Y) * (X - point3.X));
                double x0 = (L13 - 2 * y0 * (Y - point3.Y)) / (2 * (X - point3.X));
                if (Math.Pow(point2.X - x0, 2) + Math.Pow(point2.Y - y0, 2) == Math.Pow(point3.X - x0, 2) + Math.Pow(point3.Y - y0, 2))
                    return 1;
                else return 0;
            }
            else return -1;
        }

        // центр окружности по трем точкам
        public Point getcenter(Point point2, Point point3)
        {
            double L12 = X * X + Y * Y - point2.X * point2.X - point2.Y * point2.Y;
            double L13 = X * X + Y * Y - point3.X * point3.X - point3.Y * point3.Y;
            if (X != point2.X)
            {
                double y0 = ((X - point2.X) * L13 - (X - point3.X) * L12) / (2 * ((Y - point3.Y) * (X - point2.X) - (Y - point2.Y) * (X - point3.X)));
                double x0 = (L12 - 2 * y0 * (Y - point2.Y)) / (2 * (X - point2.X));
                return (new Point(x0, y0));
            }
            else //(X == point2.X)
            {
                double y0 = ((X - point2.X) * L13 - (X - point3.X) * L12) / (2 * (Y - point3.Y) * (X - point2.X) - 2 * (Y - point2.Y) * (X - point3.X));
                double x0 = (L13 - 2 * y0 * (Y - point3.Y)) / (2 * (X - point3.X));
                return (new Point(x0, y0));
            }

        }


        public Point intersection(Line tangent, double r)
        {
            double k = -tangent.getA / tangent.getB;
            double b = - tangent.getC / tangent.getB;
            //double x = -1 * k * b / (1 + Math.Pow(k, 2))+X;
            //double y = k * x + b + Y;
            double x = (X - k * (b - Y)) / (1 + k*k);
            double y = k * x + b;
            return (new Point(x, y));
        }

        public Point(Line l1, Line l2)
        {
            X = (l2.getB * l1.getC - l2.getC * l1.getB) / (l2.getA * l1.getB - l2.getB * l1.getA);
            Y = (-l1.getC - l1.getA * X) / l1.getB;
        }

        public double getradius(Point center)
        {
                return Math.Sqrt(Math.Pow(X-center.X,2)+Math.Pow(Y-center.Y,2));
        }



        public double getx
        {
            get
            {
                return X;
            }
        }

        public double gety
        {
            get
            {
                return Y;
            }
        }

    }
}
