using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Triangle;

public abstract class MyTriangle : AbstractShape
{
    public MyPoint TopLeft { get; set; } 
    public MyPoint DownRight { get; set; } 
    public MyPoint VertexOX { get; set; } 
    public MyPoint VertexOY { get; set; } 

    protected MyTriangle(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor)
        : base(bgColor, penColor)
    {
        TopLeft = topLeft;
        DownRight = downRight;
    }

    protected MyTriangle(MyPoint topLeft, MyPoint downRight)
    {
        TopLeft = topLeft;
        DownRight = downRight;
    }

    protected abstract void CalculateVertexByX(MyPoint vertex, MyPoint endPoint);
    protected abstract void CalculateVertexByY(MyPoint vertex, MyPoint endPoint);

    public override void Draw(Canvas canvas)
    {
        CalculateVertexByX(TopLeft, DownRight);
        CalculateVertexByY(TopLeft, DownRight);
        
        System.Windows.Shapes.Polygon polygon = new()
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Points =
            {
                new System.Windows.Point(TopLeft.X, TopLeft.Y),
                new System.Windows.Point(VertexOX.X, VertexOX.Y),
                new System.Windows.Point(VertexOY.X, VertexOY.Y),
            }
        };
        
        canvas.Children.Add(polygon);
    }

    public override string ToString() =>
        $"{nameof(MyTriangle)}: Vertex1=({TopLeft.X}-{TopLeft.Y}), Vertex2=({VertexOX.X}-{VertexOX.Y}), Vertex3=({VertexOY.X}-{VertexOY.Y})";
}