using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;


namespace OOTPiSP.GeometryFigures;

public class MyRightMyTriangle : MyTriangle
{
    public MyRightMyTriangle(MyPoint vertex, double xAxis, double yAxis, Brush bgColor, Brush penColor)
        : base(vertex, CalculateVertexByX(vertex, xAxis), CalculateVertexByY(vertex, yAxis), bgColor, penColor)
    { }
    
    public MyRightMyTriangle(MyPoint vertex, double xAxis, double yAxis) 
        : base(vertex, CalculateVertexByX(vertex, xAxis), CalculateVertexByY(vertex, yAxis))
    { }

    public MyRightMyTriangle(MyPoint vertex, double side, Brush bgColor, Brush penColor) 
        : this(vertex, side, side, bgColor, penColor) { }

    public MyRightMyTriangle(MyPoint vertex, double side) : this(vertex, side, side) { }
    
    static MyPoint CalculateVertexByX(MyPoint vertex, double side) => new MyPoint(vertex.X + side, vertex.Y);

    static MyPoint CalculateVertexByY(MyPoint vertex, double side) => new MyPoint(vertex.X, vertex.Y - side);
    
}