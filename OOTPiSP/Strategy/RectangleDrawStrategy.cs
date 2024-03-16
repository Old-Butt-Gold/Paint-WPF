using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Accessibility;
using OOTPiSP.GeometryFigures.Rectangle;
using OOTPiSP.GeometryFigures.Shared;
using static OOTPiSP.Strategy.IAbstractDrawStrategy;

namespace OOTPiSP.Strategy;

public class RectangleDrawStrategy : IAbstractDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas)
    {
        if (shape is MyRectangle myRectangle)
        {
            myRectangle.CanvasIndex = canvas.Children.Count;
            System.Windows.Shapes.Rectangle rectangle = new()
            {
                Fill = myRectangle.BackgroundColor,
                Stroke = myRectangle.PenColor,
                Width = myRectangle.GetWidth(),
                Height = myRectangle.GetHeight(),
                Tag = myRectangle.CanvasIndex,
            };
        
            Canvas.SetLeft(rectangle, myRectangle.TopLeft.X);
            Canvas.SetTop(rectangle, myRectangle.TopLeft.Y);
        
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