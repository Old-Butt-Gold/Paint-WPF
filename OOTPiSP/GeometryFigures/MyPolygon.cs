using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures
{
    public class MyPolygon : AbstractShape
    {
        List<MyPoint> Vertices { get; set; } = new();

        public MyPolygon(List<MyPoint> vertices, Brush bgColor, Brush penColor) : base(bgColor, penColor)
        {
            if (vertices.Count < 3)
                throw new ArgumentException("A polygon must have at least three vertices.", nameof(vertices));

            Vertices = vertices;
        }

        public MyPolygon(List<MyPoint> vertices)
        {
            if (vertices.Count < 3)
                throw new ArgumentException("A polygon must have at least three vertices.", nameof(vertices));

            Vertices = vertices;
        }

        public void Add(MyPoint myPoint) => Vertices.Add(myPoint);
        
        public void RemoveLast(MyPoint myPoint) => Vertices.RemoveAt(Vertices.Count - 1);
        
        public MyPolygon() { }
        public MyPolygon(Brush bgColor, Brush penColor) : base(bgColor, penColor) { }

        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Polygon polygon = new()
            {
                Fill = BackgroundColor,
                Stroke = PenColor,
            };
            
            foreach (var item in Vertices)
            {
                polygon.Points.Add(new(item.X, item.Y));
            }

            Canvas.SetLeft(polygon, Vertices[0].X);
            Canvas.SetTop(polygon, Vertices[0].Y);
            canvas.Children.Add(polygon);
        }

        public override string ToString()
        {
            var message = Vertices.Select(p => $"{p.X}-{p.Y}");
            return $"{nameof(MyPolygon)}: Vertices={string.Join("; ", message)}";
        }
    }
}