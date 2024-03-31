using System.Windows.Media;
using SharedComponents;

namespace Snowman;

public class SnowmanFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle) => new SnowmanShape(topLeft, downRight, bgColor, penColor, angle);
}