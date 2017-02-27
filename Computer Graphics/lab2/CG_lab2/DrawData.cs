using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CG_lab2
{
    class DrawData
    {
        List<PointF> head;
        List<PointF> body;
        List<PointF> foot1;
        List<PointF> foot2;
        List<PointF> hand1;
        List<PointF> hand2;
        List<PointF> mustache;
        List<PointF> eye1;
        List<PointF> eye2;
        List<PointF> mouth;

        public DrawData()
        {
            head = new List<PointF>();
            body = new List<PointF>();
            foot1 = new List<PointF>();
            foot2 = new List<PointF>();
            hand1 = new List<PointF>();
            hand2 = new List<PointF>();
            eye1 = new List<PointF>();
            eye2 = new List<PointF>();
            mouth = new List<PointF>();
            mustache = new List<PointF>();
        }

        public void Data()
        {
            MessageBox.Show(eye1[0].ToString()+eye2[0].ToString());
        }

        public void AddHead(double X, double Y)
        {
            head.Add(new PointF((float)X,(float)Y));
        }
        
        public void AddBody(double X, double Y)
        {
            body.Add(new PointF((float)X, (float)Y));
        }

        public void AddEye(double X, double Y, int n)
        {
            if (n == 1)
                eye1.Add(new PointF((float)X, (float)Y));
            if (n == 2)
                eye2.Add(new PointF((float)X, (float)Y));
        }

        public void AddMouth(double X, double Y)
        {
            mouth.Add(new PointF((float)X, (float)Y));
        }

        public void AddPoint(double X, double Y, int n)
        {
            if (n == 1)
                foot1.Add(new PointF((float)X, (float)Y));
            if (n == 2)
                foot2.Add(new PointF((float)X, (float)Y));
            if (n == 3)
                hand1.Add(new PointF((float)X, (float)Y));
            if (n == 4)
                hand2.Add(new PointF((float)X, (float)Y));
            if (n == 5)
                mustache.Add(new PointF((float)X, (float)Y));
        }

        public void Draw(Graphics area, Pen pen)
        {
            DrawHead(area, pen);
            DrawBody(area, pen);
            DrawEyes(area, pen);
            DrawMouth(area, pen);
            DrawFoot(area, pen);
            DrawHands(area, pen);
            DrawMustache(area, pen);
        }

        public void DrawHead(Graphics area, Pen pen)
        {
            int count = head.Count;
            for (int i = 0; i < count - 1; i++)
                area.DrawLine(pen, head[i], head[i + 1]);
        }
        
        public void DrawBody(Graphics area, Pen pen)
        {
            int count = body.Count;
            for (int i = 0; i < count - 1; i++)
                area.DrawLine(pen, body[i], body[i + 1]);
        }

        public void DrawFoot(Graphics area, Pen pen)
        {
            area.DrawLine(pen, foot1[0], foot1[1]);
            area.DrawLine(pen, foot1[1], foot1[2]);

            area.DrawLine(pen, foot2[0], foot2[1]);
            area.DrawLine(pen, foot2[1], foot2[2]);
        }

        public void DrawEyes(Graphics area, Pen pen)
        {
            int count = eye1.Count;
            for (int i = 0; i < count - 1; i++)
                area.DrawLine(pen, eye1[i], eye1[i + 1]);
            for(int i = 0; i < count - 1; i++)
                area.DrawLine(pen, eye2[i], eye2[i + 1]);
        }

        public void DrawMouth(Graphics area, Pen pen)
        {
            int count = mouth.Count;
           
            for (int i = 0; i < count - 1; i++)
                area.DrawLine(pen, mouth[i], mouth[i + 1]);
        }

        public void DrawMustache(Graphics area, Pen pen)
        {
            int count = mustache.Count;
            for (int i = 0; i < count -1; i++)
                area.DrawLine(pen, mustache[i], mustache[i + 1]);
        }

        public void DrawHands(Graphics area, Pen pen)
        {
            area.DrawLine(pen, hand1[0], hand1[1]);
            area.DrawLine(pen, hand1[1], hand1[2]);
            area.DrawLine(pen, hand1[2], hand1[0]);

            area.DrawLine(pen, hand2[0], hand2[1]);
            area.DrawLine(pen, hand2[1], hand2[2]);
            area.DrawLine(pen, hand2[2], hand2[0]);
        }

        public DrawData TurnPic(float angle, PointF centre)
        {
            DrawData new_arr = new DrawData();
            new_arr.body = Turn(body, angle, centre);
            new_arr.head = Turn(head, angle, centre);
            new_arr.foot1 = Turn(foot1, angle, centre);
            new_arr.foot2 = Turn(foot2, angle, centre);
            new_arr.hand1 = Turn(hand1, angle, centre);
            new_arr.hand2 = Turn(hand2, angle, centre);
            new_arr.mustache = Turn(mustache, angle, centre);
            new_arr.eye1 = Turn(eye1, angle, centre);
            new_arr.eye2 = Turn(eye2, angle, centre);
            new_arr.mouth = Turn(mouth, angle, centre);
            return new_arr;
        }

        public DrawData MovePic(float dx, float dy)
        {
            DrawData new_arr = new DrawData();
            new_arr.body = Move(body, dx, dy);
            new_arr.head = Move(head, dx, dy);
            new_arr.foot1 = Move(foot1, dx, dy);
            new_arr.foot2 = Move(foot2, dx, dy);
            new_arr.hand1 = Move(hand1, dx, dy);
            new_arr.hand2 = Move(hand2, dx, dy);
            new_arr.mustache = Move(mustache, dx, dy);
            new_arr.eye1 = Move(eye1, dx, dy);
            new_arr.eye2 = Move(eye2, dx, dy);
            new_arr.mouth = Move(mouth, dx, dy);
            return new_arr;
        }

        public DrawData ScalePic(float kx, float ky, PointF centre)
        {
            DrawData new_arr = new DrawData();
            new_arr.body = Scale(body, kx, ky, centre);
            new_arr.head = Scale(head, kx, ky, centre);
            new_arr.foot1 = Scale(foot1, kx, ky, centre);
            new_arr.foot2 = Scale(foot2, kx, ky, centre);
            new_arr.hand1 = Scale(hand1, kx, ky, centre);
            new_arr.hand2 = Scale(hand2, kx, ky, centre);
            new_arr.mustache = Scale(mustache, kx, ky, centre);
            new_arr.eye1 = Scale(eye1, kx, ky, centre);
            new_arr.eye2 = Scale(eye2, kx, ky, centre);
            new_arr.mouth = Scale(mouth, kx, ky, centre);
            return new_arr;
        }

        private List<PointF> Scale(List<PointF> p, float kx, float ky, PointF centre)
        {
            List<PointF> p_new = new List<PointF>();
            for (int i = 0; i < p.Count; i++)
            {
                float X = kx * p[i].X + centre.X * (1 - kx);
                float Y = ky * p[i].Y + centre.Y * (1 - ky);
                p_new.Add(new PointF(X, Y));
            }
            return p_new;
        }

        private List<PointF> Move(List<PointF> p, float kx, float ky)
        {
            List<PointF> p_new = new List<PointF>();
            for (int i = 0; i < p.Count; i++)
            {
                float X = kx + p[i].X;
                float Y = ky + p[i].Y;
                p_new.Add(new PointF(X, Y));
            }
            return p_new;
        }

        private List<PointF> Turn(List<PointF> p, float angle, PointF centre)
        {
            angle = (float)(angle / 180 * Math.PI);
            List<PointF> p_new = new List<PointF>();
            for (int i = 0; i < p.Count; i++)
            {
                float X = (float)(centre.X + (p[i].X - centre.X) * Math.Cos(angle) + (p[i].Y - centre.Y) * Math.Sin(angle));
                float Y = (float)(centre.Y + (p[i].Y - centre.Y) * Math.Cos(angle) - (p[i].X - centre.X) * Math.Sin(angle));
                p_new.Add(new PointF(X, Y));
            }
            return p_new;
        }

    }
}
