using System.Windows.Media;
using OOTPiSP.GeometryFigures.Rectangle;
using SharedComponents;

namespace OOTPiSP.Factory;

public class RectangleFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new MyRectangle(topLeft, downRight, bgColor, penColor, angle);
    }
}