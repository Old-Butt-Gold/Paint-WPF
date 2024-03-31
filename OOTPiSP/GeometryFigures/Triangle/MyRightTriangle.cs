using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using SharedComponents;

namespace OOTPiSP.GeometryFigures.Triangle;

public class MyRightTriangle : MyTriangle
{
    public override object TagShape => "7";

    public MyRightTriangle(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        CalculateVertexByX(TopLeft, DownRight);
        CalculateVertexByY(TopLeft, DownRight);
    }
    
    public sealed override void CalculateVertexByX(MyPoint vertex, MyPoint endPoint)
    {
        VertexOX = new(vertex.X + Math.Abs(vertex.X - endPoint.X), vertex.Y);
    }

    public sealed override void CalculateVertexByY(MyPoint vertex, MyPoint endPoint)
    {
        VertexOY = new(vertex.X, vertex.Y - Math.Abs(vertex.Y - endPoint.Y));
    }
}