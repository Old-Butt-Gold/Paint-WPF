using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyArc : AbstractShape
{
    public MyArc(MyPoint startPoint, MyPoint endPoint, Brush bgColor, Brush penColor) : base(startPoint, endPoint, bgColor, penColor) { }

    public override string ToString() =>
        $"{nameof(MyArc)}: TopLeft=({TopLeft.X},{TopLeft.Y}), DownRight=({DownRight.X},{DownRight.Y})";
}