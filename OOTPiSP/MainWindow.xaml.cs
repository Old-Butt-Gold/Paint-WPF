using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using OOTPiSP.GeometryFigures;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP
{
    public partial class MainWindow : Window
    {
        private MyPoint _downMyPoint;
        private MyPoint _upMyPoint;
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        
            ShapeList shapes = new ShapeList();

            for (int i = 0; i < 5; i++)
            {
                shapes.Add(new MyCircle(new MyPoint(50 + i * 50, 50 + i * 25), 75, GetRandomBrush(), GetRandomBrush()));
            }
        
            for (int i = 0; i < 5; i++)
            {
                shapes.Add(new MyEllipse(new MyPoint(350 + i * 50, 50 + i * 25), 50, 25, GetRandomBrush(), GetRandomBrush()));
            }

            for (int i = 0; i < 5; i++)
            {
                shapes.Add(new MyLine(new MyPoint(50 * i + 50, 50), new MyPoint(50 * i + 250, 500), GetRandomBrush(), GetRandomBrush()));
            }

            for (int i = 0; i < 5; i++)
            {
                shapes.Add(new MyRectangle(new MyPoint(100 * i + 50, 400), 75, 40, GetRandomBrush(), GetRandomBrush()));
            }

            for (int i = 0; i < 5; i++)
            {
                shapes.Add(new MySquare(new MyPoint(100 * i + 50, 500), 50, GetRandomBrush(), GetRandomBrush()));
            }

            shapes.Add(new MyRightMyTriangle(new MyPoint(350, 150), 100, GetRandomBrush(), GetRandomBrush()));
            shapes.Add(new EquilateralMyTriangle(new MyPoint(450, 250), 100, GetRandomBrush(), GetRandomBrush()));
        
            MyPolygon myPolygon = new MyPolygon(GetRandomBrush(), GetRandomBrush());
            myPolygon.Add(new MyPoint(250, 50));
            myPolygon.Add(new MyPoint(25, 50));
            myPolygon.Add(new MyPoint(25, 100));
            myPolygon.Add(new MyPoint(250, 250));
            
        
            shapes.Add(myPolygon);
        
            shapes.DrawAll(Canvas);
        }

        Color GetRandomColor()
        {
            byte[] colorBytes = new byte[3];
            _random.NextBytes(colorBytes);
            return Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
        }

        SolidColorBrush GetRandomBrush() => new SolidColorBrush(GetRandomColor());
        
        void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePosition = e.GetPosition(Canvas);
                _downMyPoint = new MyPoint(mousePosition.X, mousePosition.Y);
            }
        }

        private void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                var mousePosition = e.GetPosition(Canvas);
                _upMyPoint = new MyPoint(mousePosition.X, mousePosition.Y);

                MyCircle myCircle = new MyCircle(new MyPoint(_downMyPoint.X, _downMyPoint.Y), (_upMyPoint.X - _downMyPoint.X) / 2, GetRandomBrush(), GetRandomBrush());
                myCircle.Draw(Canvas);
            }
        }
    }
}
