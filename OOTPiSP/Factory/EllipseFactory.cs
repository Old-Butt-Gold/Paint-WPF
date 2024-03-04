using System.Windows.Media;
using OOTPiSP.GeometryFigures.Ellipse;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Factory;

public class EllipseFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor)
    {
        return new MyEllipse(topLeft, downRight, bgColor, penColor);
    }
}