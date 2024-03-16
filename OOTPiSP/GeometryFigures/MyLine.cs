using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyLine : AbstractShape
{
    public MyLine(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor) : base(topLeft, downRight, bgColor, borderColor)
    { }

    public override string ToString() =>
        $"{nameof(MyLine)}: Start:({TopLeft.X}-{TopLeft.Y}; End:({DownRight.X}-{DownRight.Y})";

}