using System.Windows.Controls;
using System.Windows.Media;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class MyLine : AbstractShape
{
    public MyPoint Start { get; set; }
    public MyPoint End { get; set; }
    public MyLine(MyPoint start, MyPoint end, Brush bgColor, Brush borderColor) : base(bgColor, borderColor)
    {
        Start = start;
        End = end;
    }

    public MyLine(MyPoint start, MyPoint end)
    {
        Start = start;
        End = end;
    }

    public override void Draw(Canvas canvas)
    {
        System.Windows.Shapes.Line line = new()
        {
            Fill = BackgroundColor,
            Stroke = PenColor,
            X1 = Start.X,
            X2 = End.X,
            Y1 = Start.Y,
            Y2 = End.Y,
        };
        
        canvas.Children.Add(line);
    }
    
    public override string ToString() =>
        $"{nameof(MyLine)}: Start:({Start.X}-{Start.Y}; End:({End.X}-{End.Y})";

}