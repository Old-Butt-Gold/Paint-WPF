using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;

namespace OOTPiSP.GeometryFigures.Ellipse;

public class MyEllipse : AbstractShape
{
    public override object TagShape => "1";
    
    public MyEllipse(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
        : base(topLeft, downRight, bgColor, penColor, angle)
    {
        DrawStrategy = new EllipseDrawStrategy();
    }
    

    public virtual double GetWidth() => Math.Abs(TopLeft.X - DownRight.X);
    public virtual double GetHeight() => Math.Abs(TopLeft.Y - DownRight.Y);

    public override string ToString() =>
        $"{nameof(MyEllipse)}:({TopLeft.X}-{TopLeft.Y}; RadiusX={GetWidth() / 2}; Height={GetHeight() / 2}";

}