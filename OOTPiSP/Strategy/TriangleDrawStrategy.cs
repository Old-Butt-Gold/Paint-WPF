using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.GeometryFigures.Triangle;

namespace OOTPiSP.Strategy;

public class TriangleDrawStrategy : IAbstractDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas)
    {
        if (shape is MyTriangle myTriangle)
        {
            
            double centerX = (myTriangle.TopLeft.X + myTriangle.VertexOX.X + myTriangle.VertexOY.X) / 3;
            double centerY = (myTriangle.TopLeft.Y + myTriangle.VertexOX.Y + myTriangle.VertexOY.Y) / 3;

            myTriangle.CanvasIndex = canvas.Children.Count;
            
            System.Windows.Shapes.Polygon polygon = new()
            {
                Fill = myTriangle.BackgroundColor,
                Stroke = myTriangle.PenColor,
                Points =
                {
                    new System.Windows.Point(myTriangle.TopLeft.X, myTriangle.TopLeft.Y),
                    new System.Windows.Point(myTriangle.VertexOX.X, myTriangle.VertexOX.Y),
                    new System.Windows.Point(myTriangle.VertexOY.X, myTriangle.VertexOY.Y),
                },
                RenderTransform = new RotateTransform(myTriangle.Angle, centerX, centerY),
                Tag = myTriangle.CanvasIndex,
            };

            canvas.Children.Add(polygon);
        } 
    }
}