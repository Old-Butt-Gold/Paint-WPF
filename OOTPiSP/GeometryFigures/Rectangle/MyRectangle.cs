using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Rectangle;

public class MyRectangle : AbstractShape
{
    public MyPoint TopLeft { get; set; }
    public MyPoint DownRight { get; set; }

    public MyRectangle(MyPoint topLeft, MyPoint downRight, Brush backgroundColor, Brush penColor)
        : base(backgroundColor, penColor)
    {
        TopLeft = topLeft;
        DownRight = downRight;
    }
    
    public virtual double GetHeight() => Math.Abs(TopLeft.Y - DownRight.Y); 
    public virtual double GetWidth() => Math.Abs(TopLeft.X - DownRight.X); 
    
    public override string ToString() =>
        $"{nameof(MyRectangle)}:({TopLeft.X}-{TopLeft.Y}; Width={GetWidth()}; Height={GetHeight()}";
}