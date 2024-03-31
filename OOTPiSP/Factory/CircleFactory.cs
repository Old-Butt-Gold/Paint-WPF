using System.Windows.Media;
using OOTPiSP.GeometryFigures.Ellipse;
using OOTPiSP.GeometryFigures.Shared;
using SharedComponents;

namespace OOTPiSP.Factory;

public class CircleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new MyCircle(topLeft, downRight, bgColor, penColor, angle);
    }
}