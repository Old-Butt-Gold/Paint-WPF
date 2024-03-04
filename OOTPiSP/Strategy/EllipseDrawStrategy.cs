using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Ellipse;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Strategy;

public class EllipseDrawStrategy : IAbstractDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas)
    {
        if (shape is MyEllipse myEllipse)
        {
            double width = myEllipse.GetWidth();
            double height = myEllipse.GetHeight();
        
            System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse
            {
                Fill = myEllipse.BackgroundColor,
                Stroke = myEllipse.PenColor,
                Width =  width,
                Height = height
            };
        
            Canvas.SetLeft(ellipse, myEllipse.TopLeft.X);
            Canvas.SetTop(ellipse, myEllipse.TopLeft.Y);
        
            myEllipse.RecalculateCornerOxy(myEllipse.TopLeft, myEllipse.DownRight);

            var CornerOXY = myEllipse.CornerOXY;
            
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
    }
}