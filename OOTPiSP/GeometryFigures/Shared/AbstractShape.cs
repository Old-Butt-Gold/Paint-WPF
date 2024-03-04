using System.Windows.Controls;
using System.Windows.Media;

namespace OOTPiSP.GeometryFigures.Shared;

public abstract class AbstractShape(Brush bgColor, Brush penColor)
{
    //public int CanvasIndex { get; set; } чтобы по индексу получать доступ к фигуре

    public int CornerOXY { get; set; }

    public void RecalculateCornerOXY(MyPoint start, MyPoint end)
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

    public abstract void Draw(Canvas canvas);

    public Brush BackgroundColor { get; } = bgColor;
    public Brush PenColor { get; } = penColor;
}