using System.Drawing;
using System.Windows.Forms;

namespace InteractiveGraphExample
{

    /// <summary>
    /// 表示一个正方形
    /// </summary>
    public class Square : Polygon
    {

        protected int _sideOne;

        public Square(Form hostForm, Point basePoint, int sideWidth)
            : base(hostForm, basePoint)
        {
            _sideOne = sideWidth;
        }


        public override double Area => _sideOne * _sideOne;


        public override double Perimeter
        {
            get { return _sideOne * 4; }
        }


        protected internal override bool IsMouseOverByPoint(Point mousePoint)
        {
            return (mousePoint.X >= _basePoint.X &&
                    mousePoint.X <= _basePoint.X + _sideOne &&
                    mousePoint.Y >= _basePoint.Y &&
                    mousePoint.Y <= _basePoint.Y + _sideOne);
        }

        public override Point[] GetPoints()
        {
            return new Point[]
            {
                _basePoint,
                new Point(_basePoint.X + _sideOne,_basePoint.Y),
                new Point(_basePoint.X + _sideOne, _basePoint.Y + _sideOne),
                new Point(_basePoint.X, _basePoint.Y + _sideOne)
            };
        }



        public override bool IsMouseOver => _currentIsMouseOver;

    }


    /// <summary>
    /// 表示一个矩形
    /// </summary>
    public sealed class Rectangle : Square
    {
        //竖直方向的边
        private int _sideTwo;

        /// <summary>
        /// 定义一个矩形
        /// </summary>
        /// <param name="basePoint">该矩形左上角点的坐标</param>
        /// <param name="sideOneWidth">水平方向的边的长度</param>
        /// <param name="sideTwoWidth">垂直方向的边的长度</param>
        public Rectangle(
            Form hostForm,
            Point basePoint,
            int sideOneWidth,
            int sideTwoWidth) : base(hostForm, basePoint, sideOneWidth)
        {
            _sideTwo = sideTwoWidth;
        }

        /// <summary>
        /// 面积
        /// </summary>
        public sealed override double Area => _sideOne * _sideTwo;

        /// <summary>
        /// 周长
        /// </summary>
        public override double Perimeter => (_sideOne + _sideTwo) * 2;



        protected internal override bool IsMouseOverByPoint(Point mousePoint)
        {
            return (mousePoint.X >= _basePoint.X &&
                    mousePoint.X <= _basePoint.X + _sideOne &&
                    mousePoint.Y >= _basePoint.Y &&
                    mousePoint.Y <= _basePoint.Y + _sideTwo);
        }

        public override Point[] GetPoints()
        {
            return new Point[]
            {
                _basePoint,
                new Point(_basePoint.X + _sideOne,_basePoint.Y),
                new Point(_basePoint.X + _sideOne, _basePoint.Y + _sideTwo),
                new Point(_basePoint.X, _basePoint.Y + _sideTwo)
            };
        }

        public override bool IsMouseOver => _currentIsMouseOver;

    }



}
