using System.Windows.Media;
using OOTPiSP.DynamicLoad.GeometryFigures.Rectangle;
using SharedComponents;

namespace OOTPiSP.DynamicLoad.Factory;

public class SquareFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new MySquare(topLeft, downRight, bgColor, penColor, angle);
    }
}