using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Ellipse;

public class MyEllipse : AbstractShape
{
    public MyPoint TopLeft { get; set; }
    public MyPoint DownRight { get; set; }
    
    public MyEllipse(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor) : base(bgColor, penColor)
    {
        TopLeft = topLeft;
        DownRight = downRight;
    }

    public virtual double GetWidth() => Math.Abs(TopLeft.X - DownRight.X);
    public virtual double GetHeight() => Math.Abs(TopLeft.Y - DownRight.Y);

    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Width =  GetWidth(),
            Height = GetHeight()
        };
        
        Canvas.SetLeft(ellipse, TopLeft.X);
        Canvas.SetTop(ellipse, TopLeft.Y);
        
        RecalculateCornerOXY(TopLeft, DownRight);
        
        if (CornerOXY == 2)
        {
            ellipse.RenderTransform = new RotateTransform(180);
        }
        
        if (CornerOXY == 3)
        {
            ellipse.RenderTransform = new RotateTransform(90);
        }

        if (CornerOXY == 1)
        {
            ellipse.RenderTransform = new RotateTransform(270);
        }

        if (CornerOXY is 3 or 1)
        {
            (ellipse.Width, ellipse.Height) = (ellipse.Height, ellipse.Width);
        }
        
        canvas.Children.Add(ellipse);
    }
    
    public override string ToString() =>
        $"{nameof(MyEllipse)}:({TopLeft.X}-{TopLeft.Y}; RadiusX={GetWidth() / 2}; Height={GetHeight() / 2}";

}