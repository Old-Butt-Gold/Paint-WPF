using System.Windows.Media;

namespace OOTPiSP.GeometryFigures.Shared;

public abstract class AbstractShape
{
    //public int CanvasIndex { get; set; } чтобы по индексу получать доступ к фигуре

    public AbstractShape(Brush bgColor, Brush penColor)
    {
        BackgroundColor = bgColor;
        PenColor = penColor;
    }
    
    public int CornerOXY { get; set; }

    public void RecalculateCornerOxy(MyPoint start, MyPoint end)
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