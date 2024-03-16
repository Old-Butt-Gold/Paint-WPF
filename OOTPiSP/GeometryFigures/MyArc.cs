using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyArc : AbstractShape
{
    public MyArc(MyPoint startPoint, MyPoint endPoint, Brush bgColor, Brush penColor, int angle) 
        : base(startPoint, endPoint, bgColor, penColor, angle) { }

    public override string ToString() =>
        $"{nameof(MyArc)}: TopLeft=({TopLeft.X},{TopLeft.Y}), DownRight=({DownRight.X},{DownRight.Y})";
}