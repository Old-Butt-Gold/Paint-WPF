using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.GeometryFigures.Triangle;
using SharedComponents;

namespace OOTPiSP.Factory;

public class IsoscelesTriangleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new MyIsoscelesTriangle(topLeft, downRight, bgColor, penColor, angle);
    }
}