using System.Windows.Media;
using SharedComponents;

namespace Heart;

public class HeartFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        return new HeartShape(topLeft, downRight, bgColor, penColor, angle);
    }
}