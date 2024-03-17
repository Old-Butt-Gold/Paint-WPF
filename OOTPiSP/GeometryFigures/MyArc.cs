using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;

namespace OOTPiSP.GeometryFigures;

public class MyArc : AbstractShape
{
    public override object TagShape => "8";

    public MyArc(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new ArcDrawStrategy();
    }

    public override string ToString() =>
        $"{nameof(MyArc)}: TopLeft=({TopLeft.X},{TopLeft.Y}), DownRight=({DownRight.X},{DownRight.Y})";
}