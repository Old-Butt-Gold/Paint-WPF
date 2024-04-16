using System.Windows.Media;
using Adapter.External_Plugins;
using SharedComponents;
using SharedComponents.AbstractClasses;
using SharedComponents.AdapterInstruments;

namespace Adapter.StarAdapter;

public class AdapterShape : AbstractShape
{
    public override object TagShape => "А-Звезда";

    public AdapterShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle) : base(topLeft, downRight, bgColor, penColor, angle)
    {
        MySprite mySprite = new StarShape(bgColor, penColor, [new(topLeft.X, topLeft.Y), new(downRight.X, downRight.Y)], angle);
        DrawStrategy = new AdapterDrawStrategy(mySprite);
    }

    public override string ToString() => "А-звезда";
}