using System.Windows.Media;
using SharedComponents;

namespace Sun;

public class SunShape : AbstractShape
{
    public override object TagShape => "Sun";

    public SunShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle) : base(topLeft,
        downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new SunDrawStrategy();
    }

    public override string ToString() => "Солнце";
}