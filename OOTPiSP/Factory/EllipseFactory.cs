using System.Windows.Media;
using OOTPiSP.GeometryFigures.Ellipse;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Factory;

public class EllipseFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle, int canvasIndex)
    {
        return new MyEllipse(topLeft, downRight, bgColor, penColor, angle, canvasIndex);
    }
}