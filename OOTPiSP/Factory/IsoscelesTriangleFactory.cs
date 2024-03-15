﻿using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.GeometryFigures.Triangle;

namespace OOTPiSP.Factory;

public class IsoscelesTriangleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor)
    {
        return new MyIsoscelesTriangle(topLeft, downRight, bgColor, penColor);
    }
}