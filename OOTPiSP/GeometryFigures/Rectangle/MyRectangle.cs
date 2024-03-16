﻿using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Rectangle;

public class MyRectangle : AbstractShape
{

    public MyRectangle(MyPoint topLeft, MyPoint downRight, Brush backgroundColor, Brush penColor)
        : base(topLeft, downRight, backgroundColor, penColor)
    { }
    
    public virtual double GetHeight() => Math.Abs(TopLeft.Y - DownRight.Y); 
    public virtual double GetWidth() => Math.Abs(TopLeft.X - DownRight.X); 
    
    public override string ToString() =>
        $"{nameof(MyRectangle)}:({TopLeft.X}-{TopLeft.Y}; Width={GetWidth()}; Height={GetHeight()}";
}