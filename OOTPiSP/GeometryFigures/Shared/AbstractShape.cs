using System.Windows.Input;
using System.Windows.Media;

namespace OOTPiSP.GeometryFigures.Shared;

public abstract class AbstractShape
{
    public int CanvasIndex { get; set; }

    public int Angle { get; set; }

    public MyPoint TopLeft { get; set; }
    
    public MyPoint DownRight { get; set; }
    
    public AbstractShape(MyPoint startPoint, MyPoint endPoint, Brush bgColor, Brush penColor, int angle)
    {
        BackgroundColor = bgColor;
        PenColor = penColor;
        
        TopLeft = startPoint;
        DownRight = endPoint;

        Angle = angle;
        
        RecalculateCornerOxy(startPoint, endPoint);
    }
    
    public int CornerOXY { get; private set; }

    private void RecalculateCornerOxy(MyPoint start, MyPoint end)
    {
        //X увеличивается вправо; Y увеличивает вниз (0; 0) – левый верхний угол
        if (end.X > start.X)
        {
            CornerOXY = end.Y > start.Y ? 4 : 1; 
        }
        else
        {
            CornerOXY = end.Y > start.Y ? 3 : 2;
        }
    }

    public Brush BackgroundColor { get; set; }
    public Brush PenColor { get; set; }

    public double StrokeThickness { get; set; } = 1;
}