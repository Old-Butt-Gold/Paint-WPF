using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyLine : AbstractShape
{
    public MyPoint TopLeft { get; set; }
    public MyPoint DownRight { get; set; }
    public MyLine(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor) : base(bgColor, borderColor)
    {
        TopLeft = topLeft;
        DownRight = downRight;
    }

    public override string ToString() =>
        $"{nameof(MyLine)}: Start:({TopLeft.X}-{TopLeft.Y}; End:({DownRight.X}-{DownRight.Y})";

}