using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures
{
    public class MyArc : AbstractShape
    {
        public MyPoint StartPoint { get; set; }
        public MyPoint EndPoint { get; set; }

        public MyArc(MyPoint startPoint, MyPoint endPoint, Brush bgColor, Brush penColor) : base(bgColor, penColor)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public override void Draw(Canvas canvas)
        {
            double radiusX = Math.Abs(StartPoint.X - EndPoint.X) / 2;
            double radiusY = Math.Abs(StartPoint.Y - EndPoint.Y) / 2;
            double centerX = (StartPoint.X + EndPoint.X) / 2;
            double centerY = (StartPoint.Y + EndPoint.Y) / 2;

            double startAngle = Math.Atan2(StartPoint.Y - centerY, StartPoint.X - centerX) * 180 / Math.PI;
            double endAngle = Math.Atan2(EndPoint.Y - centerY, EndPoint.X - centerX) * 180 / Math.PI;

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
                Stroke = PenColor,
                Fill = BackgroundColor,
                Data = pathGeometry
            };

            canvas.Children.Add(path);
        }

        public override string ToString() =>
            $"{nameof(MyArc)}: StartPoint=({StartPoint.X},{StartPoint.Y}), EndPoint=({EndPoint.X},{EndPoint.Y})";
    }
}
