using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Newtonsoft.Json;
using OOTPiSP.Strategy;

namespace OOTPiSP.GeometryFigures.Shared;

[Serializable]
public abstract partial class AbstractShape : IDataErrorInfo //, INotifyPropertyChanged
{
    int _angle;
    double _strokeThickness = 1;

    [JsonIgnore]
    public IDrawStrategy DrawStrategy { get; protected set; }

    [JsonIgnore] 
    public int CanvasIndex { get; set; } = -1;
    
    [JsonIgnore]
    public virtual object TagShape { get; }

    public int Angle
    {
        get => _angle;
        set
        {
            if (value is > -360 and < 360)
            {
                _angle = value;
                _errors["Angle"] = null;
            }
            else
            {
                _errors["Angle"] = "Угол должен быть в диапазоне от -360 до 360!";
            }
        }
    }

    public double StrokeThickness
    {
        get => _strokeThickness;
        set
        {
            if (value is >= 0 and <= 72)
            {
                _strokeThickness = value;
                _errors["StrokeThickness"] = null;
            }
            else
            {
                _errors["StrokeThickness"] = "Толщина карандаша должна быть от 0.0 до 72.0!";
            }

        }
    }

    public MyPoint TopLeft { get; set; }
    
    public MyPoint DownRight { get; set; }
    
    [JsonIgnore]
    public int CornerOXY { get; private set; }
    
    public Brush BackgroundColor { get; set; }

    public Brush PenColor { get; set; }
    
    protected AbstractShape(MyPoint topLeft, MyPoint downRight, Brush bgColor, Brush penColor, int angle)
    {
        BackgroundColor = bgColor;
        PenColor = penColor;
        
        TopLeft = topLeft;
        DownRight = downRight;

        Angle = angle % 360;
        
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
    
    #region IDataErrorInfo Members
    
    [JsonIgnore]
    [XmlIgnore]
    public string Error => string.Empty;

    Dictionary<string, string> _errors = new();

    [JsonIgnore]
    public bool IsValid => _errors.Values.All(x => x == null);

    [JsonIgnore]
    public IEnumerable<string> GetErrors => _errors.Values.Where(x => x != null);

    [JsonIgnore]
    [XmlIgnore]
    //Для валидации
    public string this[string columnName] => _errors.GetValueOrDefault(columnName);

    #endregion
}