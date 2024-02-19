using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyRectangle : AbstractShape
{
    public MyPoint TopLeft { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    
    public MyRectangle(MyPoint topLeft, double width, double height, Brush backgroundColor, Brush penColor)
        : base(backgroundColor, penColor)
    {
        TopLeft = topLeft;
        Width = width;
        Height = height;
    }
    
    public MyRectangle(MyPoint topLeft, double width, double height)
    {
        TopLeft = topLeft;
        Width = width;
        Height = height;
    }
    
    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Rectangle rectangle = new()
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Width = this.Width,
            Height = this.Height,
        };
        
        Canvas.SetLeft(rectangle, TopLeft.X);
        Canvas.SetTop(rectangle, TopLeft.Y);
        canvas.Children.Add(rectangle);
    }

    public override string ToString() =>
        $"{nameof(MyRectangle)}:({TopLeft.X}-{TopLeft.Y}; Width={Width}; Height={Height}";
}