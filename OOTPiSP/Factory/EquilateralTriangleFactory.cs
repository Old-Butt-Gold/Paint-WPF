using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.GeometryFigures.Triangle;

namespace OOTPiSP.Factory;

public class EquilateralTriangleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle, int canvasIndex)
    {
        return new EquilateralMyTriangle(topLeft, downRight, bgColor, penColor, angle, canvasIndex);
    }
}