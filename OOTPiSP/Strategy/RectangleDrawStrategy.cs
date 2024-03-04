using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Rectangle;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Strategy;

public class RectangleDrawStrategy : IAbstractDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas)
    {
        if (shape is MyRectangle myRectangle)
        {
            System.Windows.Shapes.Rectangle rectangle = new()
            {
                Fill = myRectangle.BackgroundColor,
                Stroke = myRectangle.PenColor,
                Width = myRectangle.GetWidth(),
                Height = myRectangle.GetHeight(),
            };
        
            Canvas.SetLeft(rectangle, myRectangle.TopLeft.X);
            Canvas.SetTop(rectangle, myRectangle.TopLeft.Y);
        
            myRectangle.RecalculateCornerOxy(myRectangle.TopLeft, myRectangle.DownRight);

            var CornerOXY = myRectangle.CornerOXY;
            
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
    }
}