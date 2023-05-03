namespace InteractiveGraphExample
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        List<Shape> shapes = new List<Shape>();

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            shapes.Add(new Square(this, new Point(50, 50), 50));
            shapes.Add(new Rectangle(this, new Point(200, 50), 200, 30));
            shapes.Add(new Triangle(this,
                new Point(300, 100),
                new Point(250, 300),
                new Point(500, 200)));

            shapes.Add(new Circle(this, new Point(250, 300), 30));
            shapes.Add(new Ellipse(this, new Point(350, 250), 50, 30));
            shapes.Add(new Ellipse(this, new Point(100, 100), 30, 70));

            foreach (var shape in shapes)
            {
                shape.MouseEnter += Shape_MouseEnter;
                shape.MouseLeave += Shape_MouseLeave;
                shape.Click += Shape_Click;
            }
        }


        private void Shape_Click(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void Shape_MouseLeave(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        private void Shape_MouseEnter(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        //GDI+   ->   Graphic Device Interface plus
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var shape in shapes)
            {
                if (shape.IsMouseOver)
                {
                    if (shape is Circle circle)
                        e.Graphics.FillEllipse
                            (new System.Drawing.Drawing2D.LinearGradientBrush(
                                new Point(0, 0),
                                new Point(50, 50),
                                Color.Yellow,
                                Color.Purple), circle.GetRectangle());
                    //e.Graphics.DrawEllipse(Pens.Red, circle.GetRectangle());

                    else if (shape is Polygon polygon)
                        e.Graphics.DrawClosedCurve(new Pen(Color.Red, 5), polygon.GetPoints(), 0.0f, System.Drawing.Drawing2D.FillMode.Winding);
                }
                else//鼠标不在上边时
                {
                    if (shape is Circle circle)
                    {
                        e.Graphics.DrawEllipse(Pens.Black, circle.GetRectangle());
                        //ControlPaint.DrawRadioButton(e.Graphics, circle.GetRectangle(), ButtonState.Checked);
                        //ControlPaint.DrawButton(e.Graphics, circle.GetRectangle(), ButtonState.Normal);
                    }
                    else if (shape is Polygon polygon)
                        e.Graphics.DrawClosedCurve(Pens.Black, polygon.GetPoints(), 0.0f, System.Drawing.Drawing2D.FillMode.Winding);

                }

            }


            //e.Graphics.DrawEllipse(Pens.Black, 50, 50, 50, 60);

            base.OnPaint(e);
        }



    }
}