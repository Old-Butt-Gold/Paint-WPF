using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MySquare : AbstractShape
{
    
    public double Side { get; set; }
    public MyPoint TopLeft { get; set; }
    
    public MySquare(MyPoint topLeft, double side, Brush backgroundColor, Brush penColor)
        : base(backgroundColor, penColor)
    {
        TopLeft = topLeft;
        Side = side;
    }

    public MySquare(MyPoint topLeft, double side)
    {
        TopLeft = topLeft;
        Side = side;
    }

    public override string ToString() =>
        $"{nameof(MySquare)}:({TopLeft.X}-{TopLeft.Y}; Side={Side};";

    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Rectangle rectangle = new()
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Width = this.Side,
            Height = this.Side,
        };
        
        Canvas.SetLeft(rectangle, TopLeft.X);
        Canvas.SetTop(rectangle, TopLeft.Y);
        canvas.Children.Add(rectangle);
    }
}