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

    public abstract void CalculateVertexByX(MyPoint vertex, MyPoint endPoint);
    public abstract void CalculateVertexByY(MyPoint vertex, MyPoint endPoint);

    public override string ToString() =>
        $"{nameof(MyTriangle)}: Vertex1=({TopLeft.X}-{TopLeft.Y}), Vertex2=({VertexOX.X}-{VertexOX.Y}), Vertex3=({VertexOY.X}-{VertexOY.Y})";
}