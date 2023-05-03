using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGraphExample
{
    public struct Color32
    {
        //private int value;
        //private uint value;

        private byte alpha, red, green, blue;


        public byte R { get { return red; } }

        public byte A { get { return alpha; } }

        public byte B { get { return blue; } }

        public byte G { get { return green; } }


        public int Value
        {
            get
            {
                int r = 0;
                r |= A << 24;
                r |= R << 16;
                r |= G << 8;
                r |= B;
                return r;
            }
        }
    }
}
