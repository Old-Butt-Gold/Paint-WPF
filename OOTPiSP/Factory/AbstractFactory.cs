using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Factory;

public abstract class AbstractFactory
{
    public abstract AbstractShape CreateShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor);
}