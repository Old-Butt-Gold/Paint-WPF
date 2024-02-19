using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class EquilateralMyTriangle : MyTriangle
{
    public EquilateralMyTriangle(MyPoint vertex, double side, Brush bgColor, Brush penColor)
        : base(vertex, CalculateVertexByX(vertex, side), CalculateVertexByY(vertex, side), bgColor, penColor)
    { }

    public EquilateralMyTriangle(MyPoint vertex, double side)
        : base(vertex, CalculateVertexByX(vertex, side), CalculateVertexByY(vertex, side))
    { }

    static MyPoint CalculateVertexByX(MyPoint vertex, double side) => new MyPoint(vertex.X + side, vertex.Y);

    static MyPoint CalculateVertexByY(MyPoint vertex, double side)
    {
        double height = side * Math.Sqrt(3) / 2;
        return new MyPoint(vertex.X + side / 2, vertex.Y - height);
    }

    public override string ToString() =>
        $"{nameof(EquilateralMyTriangle)}: Vertex1=({Vertex1.X}-{Vertex1.Y}), Vertex2=({Vertex2.X}-{Vertex2.Y}), Vertex3=({Vertex3.X}-{Vertex3.Y})";
}