using System.Windows.Media;

namespace OOTPiSP.GeometryFigures.Shared;

public abstract class AbstractShape
{
    public int CanvasIndex { get; set; }

    public int Angle { get; set; }
    
    public MyPoint TopLeft { get; set; }
    
    public MyPoint DownRight { get; set; }
    
    public AbstractShape(MyPoint startPoint, MyPoint endPoint, Brush bgColor, Brush penColor)
    {
        BackgroundColor = bgColor;
        PenColor = penColor;
        
        TopLeft = startPoint;
        DownRight = endPoint;
        
        RecalculateCornerOxy(startPoint, endPoint);
    }
    
    public int CornerOXY { get; set; }

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

    public Brush BackgroundColor { get; }
    public Brush PenColor { get; }
}