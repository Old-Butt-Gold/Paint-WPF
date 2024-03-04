using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.GeometryFigures.Triangle;

namespace OOTPiSP.Factory;

public class EquilateralTriangleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor)
    {
        return new EquilateralMyTriangle(topLeft, downRight, bgColor, penColor);
    }
}