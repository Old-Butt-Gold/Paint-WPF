using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Triangle;

public class MyIsoscelesTriangle : MyTriangle
{
    public MyIsoscelesTriangle(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle, int canvasIndex)
        : base(topLeft, downRight, bgColor, penColor, angle, canvasIndex, "6")
    {
        CalculateVertexByX(TopLeft, DownRight);
        CalculateVertexByY(TopLeft, DownRight);
    }
    
    public sealed override void CalculateVertexByX(MyPoint vertex, MyPoint endPoint)
    {
        VertexOX = new(vertex.X + Math.Abs(endPoint.X - vertex.X), vertex.Y);
    }

    public sealed override void CalculateVertexByY(MyPoint vertex, MyPoint endPoint)
    {
        double sideX = Math.Abs(vertex.X - endPoint.X);
        double sideY = Math.Abs(vertex.Y - endPoint.Y);
        
        double center = sideX / 2;
        double height = sideY;
        VertexOY = new MyPoint(vertex.X + center, vertex.Y - height);
    }

    public override string ToString() =>
        $"{nameof(MyIsoscelesTriangle)}: Vertex1=({TopLeft.X}-{TopLeft.Y}), Vertex2=({VertexOX.X}-{VertexOX.Y}), Vertex3=({VertexOY.X}-{VertexOY.Y})";

}