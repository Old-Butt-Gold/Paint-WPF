﻿using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Triangle;

public class EquilateralMyTriangle : MyTriangle
{
    public EquilateralMyTriangle(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle, int canvasIndex)
        : base(topLeft, downRight, bgColor, penColor, angle, canvasIndex, "5")
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
        double side = Math.Abs(vertex.X - endPoint.X);
        double height = side * Math.Sqrt(3) / 2;
        VertexOY =  new MyPoint(vertex.X + side / 2, vertex.Y - height);
    }

    public override string ToString() =>
        $"{nameof(EquilateralMyTriangle)}: Vertex1=({TopLeft.X}-{TopLeft.Y}), Vertex2=({VertexOX.X}-{VertexOX.Y}), Vertex3=({VertexOY.X}-{VertexOY.Y})";
}