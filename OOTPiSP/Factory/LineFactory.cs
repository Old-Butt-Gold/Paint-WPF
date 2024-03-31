using System.Windows.Media;
using OOTPiSP.GeometryFigures;
using OOTPiSP.GeometryFigures.Shared;
using SharedComponents;

namespace OOTPiSP.Factory;

public class LineFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new MyLine(topLeft, downRight, bgColor, penColor, angle);
    }
}