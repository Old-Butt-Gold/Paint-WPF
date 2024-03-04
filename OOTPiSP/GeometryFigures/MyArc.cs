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

        public override string ToString() =>
            $"{nameof(MyArc)}: StartPoint=({StartPoint.X},{StartPoint.Y}), EndPoint=({EndPoint.X},{EndPoint.Y})";
    }
}
