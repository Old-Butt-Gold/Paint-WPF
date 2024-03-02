using System.Windows.Controls;
using System.Windows.Media;

namespace OOTPiSP.GeometryFigures.Shared;

public abstract class AbstractShape
{
    //public int CanvasIndex { get; set; } чтобы по индексу получать доступ к фигуре
    //int Corner; //плоскость OXY
    //double Angle; //угол поворота
    //Point TopLeft; //Точка отрисовки
    protected AbstractShape(Brush bgColor, Brush penColor)
    {
        BackgroundColor = bgColor;
        PenColor = penColor;
    }

    protected AbstractShape()
    {
        BackgroundColor = Brushes.Black;
        PenColor = Brushes.Black;
    }

    public abstract void Draw(Canvas canvas);

    public Brush BackgroundColor { get; }
    public Brush PenColor { get; }
}