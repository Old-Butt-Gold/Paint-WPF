using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Strategy;

public class LineDrawStrategy : IDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas)
    {
        if (shape is MyLine myLine)
        {
            System.Windows.Shapes.Line line = new()
            {
                Fill = myLine.BackgroundColor,
                Stroke = myLine.PenColor,
                X1 = myLine.TopLeft.X,
                X2 = myLine.DownRight.X,
                Y1 = myLine.TopLeft.Y,
                Y2 = myLine.DownRight.Y,
                Tag = myLine.CanvasIndex,
            };
            
            canvas.Children.Add(line);
        }
    }
}