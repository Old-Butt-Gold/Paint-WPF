using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Triangle;

public class MyRightTriangle : MyTriangle
{
    public MyRightTriangle(MyPoint vertex, MyPoint endPoint, Brush bgColor, Brush penColor)
        : base(vertex, endPoint, bgColor, penColor)
    { }
    
    protected override void CalculateVertexByX(MyPoint vertex, MyPoint endPoint)
    {
        VertexOX = new(vertex.X + Math.Abs(vertex.X - endPoint.X), vertex.Y);
    }

    protected override void CalculateVertexByY(MyPoint vertex, MyPoint endPoint)
    {
        VertexOY = new(vertex.X, vertex.Y - Math.Abs(vertex.Y - endPoint.Y));
    }
}