using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyArc : AbstractShape
{
    public MyArc(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle, int canvasIndex) 
        : base(topLeft, downRight, bgColor, penColor, angle, canvasIndex, "8") { }

    public override string ToString() =>
        $"{nameof(MyArc)}: TopLeft=({TopLeft.X},{TopLeft.Y}), DownRight=({DownRight.X},{DownRight.Y})";
}