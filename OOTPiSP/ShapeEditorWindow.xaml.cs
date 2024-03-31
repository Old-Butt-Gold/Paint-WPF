using System.Windows;
using System.Windows.Input;
using OOTPiSP.GeometryFigures.Shared;
using SharedComponents;

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

    void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!Shape.IsValid)
        {
            var list = Shape.GetErrors;
            MessageBox.Show("Присутствуют ошибки в данных: \n" + string.Join("\n", list), "Валидация", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        else
        {
            Close();
        }
    }

    void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e) => ButtonBase_OnClick(sender, null);
}