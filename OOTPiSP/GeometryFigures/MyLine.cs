using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyLine : AbstractShape
{
    public MyLine(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor, int angle) 
        : base(topLeft, downRight, bgColor, borderColor, angle)
    { }

    public override string ToString() =>
        $"{nameof(MyLine)}: Start:({TopLeft.X}-{TopLeft.Y}; End:({DownRight.X}-{DownRight.Y})";

}