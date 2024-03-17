using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Ellipse;

public class MyEllipse : AbstractShape
{
    protected MyEllipse(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, 
        int angle, int canvasIndex, object tagIndex) 
        : base(topLeft, downRight, bgColor, penColor, angle, canvasIndex, tagIndex)
    { }
    
    public MyEllipse(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle, int canvasIndex) 
        : base(topLeft, downRight, bgColor, penColor, angle, canvasIndex, "1")
    { }

    public virtual double GetWidth() => Math.Abs(TopLeft.X - DownRight.X);
    public virtual double GetHeight() => Math.Abs(TopLeft.Y - DownRight.Y);

    public override string ToString() =>
        $"{nameof(MyEllipse)}:({TopLeft.X}-{TopLeft.Y}; RadiusX={GetWidth() / 2}; Height={GetHeight() / 2}";

}