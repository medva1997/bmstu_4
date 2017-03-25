using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VAlab2
{
    class PointT
    {
        float X;
        float Y;
        float p;

        public PointT(float x, float y, float P)
        {
            X = x;
            Y = y;
            p = P;
        }

        public float getx
        {
            get
            {
                return X;
            }
        }

        public float gety
        {
            get
            {
                return Y;
            }
        }

        public float getp
        {
            get
            {
                return p;
            }
        }


    }


}
