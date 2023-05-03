using System;
using System.Drawing;
using System.Windows.Forms;

namespace InteractiveGraphExample
{
    public class Circle : Shape
    {

        protected double _radius;

        protected double _area, _perimeter;

        public Circle(Form hostForm, Point origin, double radius)
            : base(hostForm, origin)
        {
            _basePoint = origin;
            _radius = radius;
            _area = Math.PI * _radius * _radius;
            _perimeter = 2 * Math.PI * _radius;
        }


        public override double Area => _area;

        public override double Perimeter => _perimeter;

        public override bool IsMouseOver => _currentIsMouseOver;

        protected internal override bool IsMouseOverByPoint(Point mousePoint)
        {
            var distance = Math.Sqrt(Math.Pow(mousePoint.X - _basePoint.X, 2) + Math.Pow(mousePoint.Y - _basePoint.Y, 2));
            return distance <= _radius;
        }



        public virtual System.Drawing.Rectangle GetRectangle()
        {
            return new System.Drawing.Rectangle(
                _basePoint.X - (int)_radius,
                _basePoint.Y - (int)_radius,
                (int)_radius * 2,
                (int)_radius * 2);

            /* x: 50
             * y: 0
             * width:250
             * height :200
             */
        }
    }


    public sealed class Ellipse : Circle
    {
        //假定用以存储短半轴
        private double _radius2;

        private double _a, _b;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostForm"></param>
        /// <param name="origin"></param>
        /// <param name="radius1">水平方向半轴</param>
        /// <param name="radius2">垂直方向半轴</param>
        public Ellipse(Form hostForm, Point origin, double radius1, double radius2)
            : base(hostForm, origin, radius1)
        {
            _radius2 = radius2;
            _area = Math.PI * _radius * _radius2;

            //if (radius1 >= radius2)
            //{
            //    _a = radius1;
            //    _b = radius2;
            //}
            //else
            //{
            //    _a = radius2;
            //    _b = radius1;
            //}

            _a = Math.Max(radius1, radius2); //R  a
            _b = Math.Min(radius1, radius2); //r  b
            //L=2πb+4(a-b) 
            _perimeter = (2 * Math.PI * _b) + 4 * (_a - _b);
        }


        public override System.Drawing.Rectangle GetRectangle()
        {
            return new System.Drawing.Rectangle(
                _basePoint.X - (int)_radius,
                _basePoint.Y - (int)_radius2,
                (int)_radius * 2,
                (int)_radius2 * 2);
        }

        protected internal override bool IsMouseOverByPoint(Point mousePoint)
        {
            //x^2/a^2+y^2/b^2=1，(a>b>0)是椭圆；
            //发现：
            //1 这个公式其实是以圆心为原点为基础的，所以要算出相对坐标点
            //2 哪个方向上是长半轴，就要用那个方向上的坐标跟 a 计算。也就是说不像公式上X Y出现的位置是固定的
            //在计算机系统中要发生变化

            if (_radius > _radius2)//长半轴在水平方向
            {
                //相对坐标系统的点
                var relativePoint = new Point(mousePoint.X - _basePoint.X, mousePoint.Y - _basePoint.Y);
                var part1 = Math.Pow(relativePoint.X, 2) / Math.Pow(_a, 2);
                var part2 = Math.Pow(relativePoint.Y, 2) / Math.Pow(_b, 2);
                return (part1 + part2) <= 1.0;

            }
            else if (_radius < _radius2)//长半轴在竖直方向
            {
                //相对坐标系统的点
                var relativePoint = new Point(mousePoint.X - _basePoint.X, mousePoint.Y - _basePoint.Y);
                var part1 = Math.Pow(relativePoint.Y, 2) / Math.Pow(_a, 2);
                var part2 = Math.Pow(relativePoint.X, 2) / Math.Pow(_b, 2);
                return (part1 + part2) <= 1.0;
            }
            else if (_radius == _radius2)
            {
                return base.IsMouseOverByPoint(mousePoint);
            }
            return false;

            //相对坐标系统的点,想试图改成常规坐标系统的方式来处理，但是还是不行
            //var relativePoint = new Point(mousePoint.X - _basePoint.X, _basePoint.Y - mousePoint.Y);
            //Console.WriteLine(relativePoint);
            //var part1 = Math.Pow(relativePoint.X, 2) / Math.Pow(_a, 2);
            //var part2 = Math.Pow(relativePoint.Y, 2) / Math.Pow(_b, 2);
            //return (part1 + part2) <= 1.0;
        }


    }

}
