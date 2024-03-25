using System.Windows;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP;

public partial class ShapeEditorWindow
{
    public AbstractShape Shape
    {
        get => (AbstractShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    public static readonly DependencyProperty ShapeProperty;

    static ShapeEditorWindow()
    {
        ShapeProperty = DependencyProperty.Register(
            "Shape", 
            typeof(AbstractShape), 
            typeof(ShapeEditorWindow));
    }
    
    public ShapeEditorWindow(AbstractShape shape)
    {
        InitializeComponent();
        Shape = shape;
    }

    void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        Shape.Angle %= 360;

        if (Shape.Angle < 0)
            Shape.Angle += 360;
    }
}