using System.Windows.Media;
using OOTPiSP.GeometryFigures;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Factory;

public class ArcFactory : AbstractFactory
{
    public override AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor)
    {
        return new MyArc(topLeft, downRight, bgColor, penColor);
    }
}