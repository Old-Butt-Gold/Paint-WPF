using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;
using OOTPiSP.Strategy;

namespace OOTPiSP.GeometryFigures.Shared;

[Serializable]
public abstract class AbstractShape
{
    [JsonIgnore] 
    public IDrawStrategy DrawStrategy { get; protected set; }

    [JsonIgnore] 
    public int CanvasIndex { get; set; } = -1;
    
    [JsonIgnore]
    public virtual object TagShape { get; }

    public int Angle { get; set; }

    public MyPoint TopLeft { get; set; }
    
    public MyPoint DownRight { get; set; }
    
    protected AbstractShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        BackgroundColor = bgColor;
        PenColor = penColor;
        
        TopLeft = topLeft;
        DownRight = downRight;

        Angle = angle;
        
        RecalculateCornerOxy(topLeft, downRight);
    }

    public void DrawAlgorithm(Canvas canvas)
    {
        Shape drawnShape = DrawStrategy.Draw(this);
        if (drawnShape != null)
        {
            if (CanvasIndex < 0)
            {
                CanvasIndex = canvas.Children.Count;
                canvas.Children.Add(drawnShape);
            }
            else
            {
                canvas.Children.RemoveAt(CanvasIndex);
                canvas.Children.Insert(CanvasIndex, drawnShape);
            }
            drawnShape.Tag = CanvasIndex;
        }
    }


    [JsonIgnore]
    public int CornerOXY { get; private set; }

    void RecalculateCornerOxy(MyPoint start, MyPoint end)
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
    
    public double StrokeThickness { get; set; } = 1;
    
    public Brush BackgroundColor { get; set; }

    public Brush PenColor { get; set; }
}