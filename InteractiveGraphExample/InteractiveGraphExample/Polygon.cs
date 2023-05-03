using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveGraphExample
{
    public abstract class Polygon : Shape
    {


        public Polygon(Form hostForm, Point basePoint) : base(hostForm, basePoint)
        { }

        /// <summary>
        /// 当被重写时，返回该形状的关键点坐标集合
        /// </summary>
        /// <returns></returns>
        public abstract Point[] GetPoints();



    }
}
