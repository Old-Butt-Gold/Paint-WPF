using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;
using SharedComponents;

namespace OOTPiSP.GeometryFigures;

public class MyLine : AbstractShape
{
    public override object TagShape => "4";

    public MyLine(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor, int angle)
        : base(topLeft, downRight, bgColor, borderColor, angle)
    {
        DrawStrategy = new LineDrawStrategy();
    }

    public override string ToString() =>
        $"{nameof(MyLine)}: Start:({TopLeft.X}-{TopLeft.Y}; End:({DownRight.X}-{DownRight.Y})";

}