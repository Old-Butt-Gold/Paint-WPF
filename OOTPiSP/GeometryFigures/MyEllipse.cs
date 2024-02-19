using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyEllipse : AbstractShape
{
    public MyPoint TopLeft { get; set; }
    public double RadiusX { get; set; }
    public double RadiusY { get; set; }
    
    public MyEllipse(MyPoint topLeft, double x, double y, Brush bgColor, Brush penColor) : base(bgColor, penColor)
    {
        TopLeft = topLeft;
        RadiusX = x;
        RadiusY = y;
    }

    public MyEllipse(MyPoint topLeft, double x, double y)
    {
        TopLeft = topLeft;
        RadiusX = x;
        RadiusY = y;
    }

    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Width = RadiusX * 2,
            Height = RadiusY * 2
        };
        Canvas.SetLeft(ellipse, TopLeft.X);
        Canvas.SetTop(ellipse, TopLeft.Y);
        canvas.Children.Add(ellipse);
    }
    
    public override string ToString() =>
        $"{nameof(MyEllipse)}:({TopLeft.X}-{TopLeft.Y}; RadiusX={RadiusX}; Height={RadiusY}";

}