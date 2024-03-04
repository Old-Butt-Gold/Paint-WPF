﻿using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Ellipse;

public class MyEllipse : AbstractShape
{
    public MyPoint TopLeft { get; set; }
    public MyPoint DownRight { get; set; }
    
    public MyEllipse(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor) : base(bgColor, penColor)
    {
        TopLeft = topLeft;
        DownRight = downRight;
    }

    public virtual double GetWidth() => Math.Abs(TopLeft.X - DownRight.X);
    public virtual double GetHeight() => Math.Abs(TopLeft.Y - DownRight.Y);

    public override string ToString() =>
        $"{nameof(MyEllipse)}:({TopLeft.X}-{TopLeft.Y}; RadiusX={GetWidth() / 2}; Height={GetHeight() / 2}";

}