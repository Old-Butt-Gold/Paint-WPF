using System.Windows.Media;
using OOTPiSP.GeometryFigures.Rectangle;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Factory;

public class SquareFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor)
    {
        return new MySquare(topLeft, downRight, bgColor, penColor);
    }
}