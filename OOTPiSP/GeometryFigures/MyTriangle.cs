using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyTriangle : AbstractShape
{
    public MyPoint Vertex1 { get; set; }
    public MyPoint Vertex2 { get; set; }
    public MyPoint Vertex3 { get; set; }

    public MyTriangle(MyPoint vertex1, MyPoint vertex2, MyPoint vertex3, Brush bgColor, Brush penColor)
        : base(bgColor, penColor)
    {
        Vertex1 = vertex1;
        Vertex2 = vertex2;
        Vertex3 = vertex3;
    }

    public MyTriangle(MyPoint vertex1, MyPoint vertex2, MyPoint vertex3)
    {
        Vertex1 = vertex1;
        Vertex2 = vertex2;
        Vertex3 = vertex3;
    }

    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Polygon polygon = new()
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Points =
            {
                new System.Windows.Point(Vertex1.X, Vertex1.Y),
                new System.Windows.Point(Vertex2.X, Vertex2.Y),
                new System.Windows.Point(Vertex3.X, Vertex3.Y),
            }
        };
        
        Canvas.SetLeft(polygon, Vertex1.X);
        Canvas.SetTop(polygon, Vertex1.Y);
        canvas.Children.Add(polygon);
    }

    public override string ToString() =>
        $"{nameof(MyTriangle)}: Vertex1=({Vertex1.X}-{Vertex1.Y}), Vertex2=({Vertex2.X}-{Vertex2.Y}), Vertex3=({Vertex3.X}-{Vertex3.Y})";
}