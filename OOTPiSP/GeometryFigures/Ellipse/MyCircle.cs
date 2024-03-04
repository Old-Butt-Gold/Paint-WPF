using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Ellipse;

public class MyCircle : MyEllipse
{
    //int Corner;
    public MyCircle(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor) 
        : base(topLeft, downRight, bgColor, borderColor)
    { }

    public override double GetHeight() => Math.Abs(TopLeft.X - DownRight.X);
    public override double GetWidth() => Math.Abs(TopLeft.X - DownRight.X);

    public override string ToString() =>
        $"{nameof(MyCircle)}:({TopLeft.X}-{TopLeft.Y}; Radius={GetHeight()};";

}