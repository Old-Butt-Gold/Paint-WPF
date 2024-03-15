using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using OOTPiSP.GeometryFigures;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Strategy;

public class ArcDrawStrategy : IAbstractDrawStrategy
{
    public void Draw(AbstractShape shape, Canvas canvas, int angle = 0)
    {
        if (shape is MyArc myArc)
        {
            double radiusX = Math.Abs(myArc.StartPoint.X - myArc.EndPoint.X) / 2;
            double radiusY = Math.Abs(myArc.StartPoint.Y - myArc.EndPoint.Y) / 2;
            double centerX = (myArc.StartPoint.X + myArc.EndPoint.X) / 2;
            double centerY = (myArc.StartPoint.Y + myArc.EndPoint.Y) / 2;

            double startAngle = Math.Atan2(myArc.StartPoint.Y - centerY, myArc.StartPoint.X - centerX) * 180 / Math.PI;
            double endAngle = Math.Atan2(myArc.EndPoint.Y - centerY, myArc.EndPoint.X - centerX) * 180 / Math.PI;

            myArc.Angle = angle;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure
            {
                StartPoint = new System.Windows.Point(centerX + radiusX * Math.Cos(startAngle * Math.PI / 180),
                    centerY + radiusY * Math.Sin(startAngle * Math.PI / 180))
            };

            ArcSegment arcSegment = new ArcSegment
            {
                Point = new System.Windows.Point(centerX + radiusX * Math.Cos(endAngle * Math.PI / 180),
                    centerY + radiusY * Math.Sin(endAngle * Math.PI / 180)),
                Size = new System.Windows.Size(radiusX, radiusY),
                IsLargeArc = Math.Abs(startAngle - endAngle) > 180,
                SweepDirection = SweepDirection.Clockwise
            };
            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);

            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path
            {
                Stroke = myArc.PenColor,
                Fill = myArc.BackgroundColor,
                Data = pathGeometry,
                RenderTransform = new RotateTransform(myArc.Angle, centerX, centerY),
            };

            canvas.Children.Add(path);
        }
    }
}