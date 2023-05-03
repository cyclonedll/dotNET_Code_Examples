using System;
using System.Drawing;
using System.Windows.Forms;

namespace InteractiveGraphExample
{
    public sealed class Triangle : Polygon
    {

        internal Point _point2;

        internal Point _point3;

        public Triangle(Form hostForm, Point basePoint, Point point2, Point point3)
            : base(hostForm, basePoint)
        {
            _point2 = point2;
            _point3 = point3;
        }



        public override double Area
        {
            get
            {
                return GetArea(_basePoint, _point2, _point3);
            }
        }

        public static double GetArea(Point p1, Point p2, Point p3)
        {
            var p = GetPerimeter(p1, p2, p3, out double a, out double b, out double c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static double GetArea(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var p = GetPerimeter(x1, y1, x2, y2, x3, y3, out double a, out double b, out double c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static double GetPerimeter(Point p1, Point p2, Point p3)
        {
            var a = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            var b = Math.Sqrt(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p2.Y - p3.Y, 2));
            var c = Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));
            return (a + b + c);
        }

        public double GetPerimeter()
        {
            var a = Math.Sqrt(Math.Pow(_basePoint.X - _point2.X, 2) + Math.Pow(_basePoint.Y - _point2.Y, 2));
            var b = Math.Sqrt(Math.Pow(_point2.X - _point3.X, 2) + Math.Pow(_point2.Y - _point3.Y, 2));
            var c = Math.Sqrt(Math.Pow(_basePoint.X - _point3.X, 2) + Math.Pow(_basePoint.Y - _point3.Y, 2));
            return (a + b + c);
        }


        public static double GetPerimeter(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var a = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            var b = Math.Sqrt(Math.Pow(x2 - x3, 2) + Math.Pow(y2 - y3, 2));
            var c = Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2));
            return (a + b + c);
        }
        public static double GetPerimeter(Point p1, Point p2, Point p3, out double a, out double b, out double c)
        {
            a = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            b = Math.Sqrt(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p2.Y - p3.Y, 2));
            c = Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));
            return (a + b + c);
        }

        public static double GetPerimeter(int x1, int y1, int x2, int y2, int x3, int y3, out double a, out double b, out double c)
        {
            a = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            b = Math.Sqrt(Math.Pow(x2 - x3, 2) + Math.Pow(y2 - y3, 2));
            c = Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2));
            return (a + b + c);
        }



        public override double Perimeter
        {
            get
            {
                return GetPerimeter(_basePoint, _point2, _point3);
            }
        }

        public override bool IsMouseOver => _currentIsMouseOver;

        protected internal override bool IsMouseOverByPoint(Point mousePoint)
        {
            var area1 = GetArea(mousePoint, _basePoint, _point2);
            var area2 = GetArea(mousePoint, _basePoint, _point3);
            var area3 = GetArea(mousePoint, _point2, _point3);
            var sum = area1 + area2 + area3;
            //Console.WriteLine($"sum{sum} ,area{Area}, {sum==Area}");
            return Math.Round(sum) == Math.Round(Area);
        }


        public override Point[] GetPoints()
        {
            return new Point[]
            {
                new Point(_basePoint.X ,_basePoint.Y),
                new Point(_point2.X ,_point2.Y),
                new Point(_point3.X, _point3.Y)
            };
        }



    }
}
