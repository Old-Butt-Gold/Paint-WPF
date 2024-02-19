using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyCircle : AbstractShape
{
    //int Corner;
    public MyPoint TopLeft { get; set; }
    public double Radius { get; set; }
    public MyCircle(MyPoint topLeft, double radius, Brush bgColor, Brush borderColor) : base(bgColor, borderColor)
    {
        TopLeft = topLeft;
        Radius = radius;
        //Corner = corner;
    }

    public MyCircle(MyPoint topLeft, double radius)
    {
        TopLeft = topLeft;
        Radius = radius;
    }

    public override void Draw(Canvas canvas)
    {
        Ellipse ellipse = new Ellipse
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Width = Math.Abs(Radius) * 2,
            Height = Math.Abs(Radius) * 2,
        };
        
        Canvas.SetLeft(ellipse, TopLeft.X); //-Radius?
        Canvas.SetTop(ellipse, TopLeft.Y); //-Radius?

        if (Radius < 0)
        {
            RotateTransform rotateTransform = new RotateTransform(90);
            ellipse.RenderTransform = rotateTransform;
        }
        
        /*if (Corner == 2) //2 плоскость
        {
            RotateTransform rotateTransform = new RotateTransform(90);
            ellipse.RenderTransform = rotateTransform;
        }
        else if (Corner == 3) //3 плоскость
        {
            RotateTransform rotateTransform = new RotateTransform(180);
            ellipse.RenderTransform = rotateTransform;
        }
        else if (Corner == 4) //4 плоскость
        {
            RotateTransform rotateTransform = new RotateTransform(270);
            ellipse.RenderTransform = rotateTransform;
        }*/

        canvas.Children.Add(ellipse);
    }
    
    public override string ToString() =>
        $"{nameof(MyCircle)}:({TopLeft.X}-{TopLeft.Y}; Radius={Radius};";

}