using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Rectangle;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Strategy;

public class RectangleDrawStrategy : IAbstractDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas, int angle = 0)
    {
        if (shape is MyRectangle myRectangle)
        {
            System.Windows.Shapes.Rectangle rectangle = new()
            {
                Fill = myRectangle.BackgroundColor,
                Stroke = myRectangle.PenColor,
                Width = myRectangle.GetWidth(),
                Height = myRectangle.GetHeight(),
                Tag = canvas.Children.Count,
            };
        
            Canvas.SetLeft(rectangle, myRectangle.TopLeft.X);
            Canvas.SetTop(rectangle, myRectangle.TopLeft.Y);
        
            myRectangle.Angle = angle;

            myRectangle.CanvasIndex = canvas.Children.Count;

            var CornerOXY = myRectangle.CornerOXY;
            
            if (CornerOXY == 2)
            {
                rectangle.RenderTransform = new RotateTransform(180 + myRectangle.Angle);
            }
        
            if (CornerOXY == 3)
            {
                rectangle.RenderTransform = new RotateTransform(90 + myRectangle.Angle);
            }

            if (CornerOXY == 1)
            {
                rectangle.RenderTransform = new RotateTransform(270 + myRectangle.Angle);
            }

            if (CornerOXY == 4)
            {
                rectangle.RenderTransform = new RotateTransform(myRectangle.Angle);
            }
            
            if (CornerOXY is 3 or 1)
            {
                (rectangle.Width, rectangle.Height) = (rectangle.Height, rectangle.Width);
            }
        
            canvas.Children.Add(rectangle);
        }
    }
}