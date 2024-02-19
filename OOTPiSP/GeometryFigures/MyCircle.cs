using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyCircle : AbstractShape
{
    
    public MyPoint TopLeft { get; set; }
    public double Radius { get; set; }
    public MyCircle(MyPoint topLeft, double radius, Brush bgColor, Brush borderColor) : base(bgColor, borderColor)
    {
        TopLeft = topLeft;
        Radius = radius;
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
        Canvas.SetTop(ellipse, TopLeft.Y - Radius); //-Radius?

        if (Radius < 0)
        {
            //TODO 4 положения отрисовки, если точка уходит вверх, все равно рисует окружность вниз
            // Создаем вращающее преобразование на 90 градусов
            RotateTransform rotateTransform = new RotateTransform(90);
            ellipse.RenderTransform = rotateTransform;
        }

        canvas.Children.Add(ellipse);
    }
    
    public override string ToString() =>
        $"{nameof(MyCircle)}:({TopLeft.X}-{TopLeft.Y}; Radius={Radius};";

}