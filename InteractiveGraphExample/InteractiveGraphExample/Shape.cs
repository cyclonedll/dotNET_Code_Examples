using System;
using System.Drawing;
using System.Windows.Forms;

namespace InteractiveGraphExample
{

    /// <summary>
    /// 表示一个二维平面形状
    /// </summary>
    public abstract class Shape
    {

        protected Point _basePoint;

        protected Form _hostForm;

        public Shape(Form hostForm, Point basePoint)
        {
            _hostForm = hostForm;
            _basePoint = basePoint;

            hostForm.MouseMove += HostForm_MouseMove;
            hostForm.MouseDown += HostForm_MouseDown;
            hostForm.MouseUp += HostForm_MouseUp;
        }

        private bool _isDragging = false;

        private Point _dragStartPoint;
        private Point? _p2, _p3;

        private void HostForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isDragging)
            {
                _isDragging = false;
                _hostForm.Capture = false;
            }
        }

        private void HostForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _currentIsMouseOver)
            {
                Click?.Invoke(this, EventArgs.Empty);

                _isDragging = true;
                _hostForm.Capture = true;
                _dragStartPoint = new Point(e.X - _basePoint.X, e.Y - _basePoint.Y);
                if (this is Triangle tri)
                {
                    _p2 = new Point(e.X - tri._point2.X, e.Y - tri._point2.Y);
                    _p3 = new Point(e.X - tri._point3.X, e.Y - tri._point3.Y);
                }
            }
        }

        private bool _oldIsMouseOver = false;

        //当前状态
        protected bool _currentIsMouseOver = false;

        private void HostForm_MouseMove(object sender, MouseEventArgs e)
        {
            _currentIsMouseOver = IsMouseOverByPoint(e.Location);
            if (!_isDragging)
            {
                if (_currentIsMouseOver != _oldIsMouseOver)
                {
                    _oldIsMouseOver = _currentIsMouseOver;

                    if (_currentIsMouseOver) RaiseMouseEnterEvent();
                    else RaiseMouseLeaveEvent();
                }
            }
            else if (_isDragging)
            {
                _basePoint = new Point(e.X - _dragStartPoint.X, e.Y - _dragStartPoint.Y);
                if (this is Triangle tri)
                {
                    tri._point2 = new Point(e.X - _p2.Value.X, e.Y - _p2.Value.Y);
                    tri._point3 = new Point(e.X - _p3.Value.X, e.Y - _p3.Value.Y);
                }                 
                _hostForm.Invalidate();
            }
        }




        /// <summary>
        /// 当该方法被重写时，返回该形状的面积
        /// </summary>
        /// <returns></returns>
        public abstract double Area { get; }

        /// <summary>
        /// 当被重写时，返回该形状的周长
        /// </summary>
        /// <returns></returns>
        public abstract double Perimeter { get; }

        /// <summary>
        /// 当被重写时，返回鼠标是否落在该形状所在区域内
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <returns></returns>
        protected internal abstract bool IsMouseOverByPoint(Point mousePoint);



        /// <summary>
        /// 当鼠标指针进入该形状的区域时触发
        /// </summary>
        public event EventHandler MouseEnter;

        /// <summary>
        /// 当鼠标指针离开该形状的区域时触发
        /// </summary>
        public event EventHandler MouseLeave;

        public event EventHandler Click;

        /// <summary>
        /// 触发<see cref="MouseEnter"/>事件
        /// </summary>
        protected void RaiseMouseEnterEvent()
        {
            if (MouseEnter != null)
                MouseEnter(this, EventArgs.Empty);
        }

        /// <summary>
        /// 触发<see cref="MouseLeave"/>事件
        /// </summary>
        protected void RaiseMouseLeaveEvent()
        {
            MouseLeave?.Invoke(this, EventArgs.Empty);
        }

        public abstract bool IsMouseOver { get; }

    }
}
