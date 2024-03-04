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
    
    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Rectangle rectangle = new()
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Width = GetWidth(),
            Height = GetHeight(),
        };
        
        Canvas.SetLeft(rectangle, TopLeft.X);
        Canvas.SetTop(rectangle, TopLeft.Y);
        
        RecalculateCornerOXY(TopLeft, DownRight);
        
        if (CornerOXY == 2)
        {
            rectangle.RenderTransform = new RotateTransform(180);
        }
        
        if (CornerOXY == 3)
        {
            rectangle.RenderTransform = new RotateTransform(90);
        }

        if (CornerOXY == 1)
        {
            rectangle.RenderTransform = new RotateTransform(270);
        }

        if (CornerOXY is 3 or 1)
        {
            (rectangle.Width, rectangle.Height) = (rectangle.Height, rectangle.Width);
        }
        
        canvas.Children.Add(rectangle);
    }

    public override string ToString() =>
        $"{nameof(MyRectangle)}:({TopLeft.X}-{TopLeft.Y}; Width={GetWidth()}; Height={GetHeight()}";
}