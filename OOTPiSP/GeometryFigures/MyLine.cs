using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyLine : AbstractShape
{
    public MyLine(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor, int angle, int canvasIndex) 
        : base(topLeft, downRight, bgColor, borderColor, angle, canvasIndex, "4")
    { }

    public override string ToString() =>
        $"{nameof(MyLine)}: Start:({TopLeft.X}-{TopLeft.Y}; End:({DownRight.X}-{DownRight.Y})";

}