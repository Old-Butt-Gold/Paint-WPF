using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures.Ellipse;

public class MyCircle : MyEllipse
{
    //int Corner;
    public MyCircle(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush borderColor) 
        : base(topLeft, downRight, bgColor, borderColor)
    { }

    public MyCircle(MyPoint topLeft, MyPoint downRight) 
        : base(topLeft, downRight) { }

    public override double GetHeight() => Math.Abs(TopLeft.X - DownRight.X);
    public override double GetWidth() => Math.Abs(TopLeft.X - DownRight.X);

    /*public override void Draw(Canvas canvas)
    {
        Ellipse ellipse = new Ellipse
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            Width = GetWidth(),
            Height = GetHeight(),
        };
        
        Canvas.SetLeft(ellipse, TopLeft.X); //-Radius?
        Canvas.SetTop(ellipse, TopLeft.Y); //-Radius?

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
        }#1#

        canvas.Children.Add(ellipse);
    }*/
    
    public override string ToString() =>
        $"{nameof(MyCircle)}:({TopLeft.X}-{TopLeft.Y}; Radius={GetHeight()};";

}