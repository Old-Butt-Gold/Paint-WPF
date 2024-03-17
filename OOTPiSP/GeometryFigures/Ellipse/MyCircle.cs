using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Ellipse;

public class MyCircle : MyEllipse
{
    public MyCircle(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor, int angle, int canvasIndex) 
        : base(topLeft, downRight, bgColor, borderColor, angle, canvasIndex, "0")
    { }

    public override double GetHeight() => Math.Abs(TopLeft.X - DownRight.X);
    public override double GetWidth() => Math.Abs(TopLeft.X - DownRight.X);

    public override string ToString() =>
        $"{nameof(MyCircle)}:({TopLeft.X}-{TopLeft.Y}; Radius={GetHeight()};";

}