using System.Windows.Media;
using OOTPiSP.GeometryFigures.Triangle;
using SharedComponents;

namespace OOTPiSP.Factory;

public class EquilateralTriangleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new EquilateralMyTriangle(topLeft, downRight, bgColor, penColor, angle);
    }
}