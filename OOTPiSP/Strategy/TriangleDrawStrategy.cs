using System.Windows.Controls;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.GeometryFigures.Triangle;

namespace OOTPiSP.Strategy;

public class TriangleDrawStrategy : IAbstractDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas)
    {
        if (shape is MyTriangle myTriangle)
        {
            myTriangle.RecalculateCornerOxy(myTriangle.TopLeft, myTriangle.DownRight);
        
            //Поменять сигнатуру треугольников, чтобы можно было применить вращение
        
            myTriangle.CalculateVertexByX(myTriangle.TopLeft, myTriangle.DownRight);
            
            myTriangle.CalculateVertexByY(myTriangle.TopLeft, myTriangle.DownRight);
        
            System.Windows.Shapes.Polygon polygon = new()
            {
                Fill = myTriangle.BackgroundColor,
                Stroke = myTriangle.PenColor,
                Points =
                {
                    new System.Windows.Point(myTriangle.TopLeft.X, myTriangle.TopLeft.Y),
                    new System.Windows.Point(myTriangle.VertexOX.X, myTriangle.VertexOX.Y),
                    new System.Windows.Point(myTriangle.VertexOY.X, myTriangle.VertexOY.Y),
                }
            };
        
            canvas.Children.Add(polygon);
        } 
    }
}