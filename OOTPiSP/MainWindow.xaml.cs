using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OOTPiSP.Factory;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;

namespace OOTPiSP;

public partial class MainWindow : Window
{
    
    readonly Dictionary<string, (AbstractFactory Factory, IAbstractDrawStrategy Strategy, string text)> _buttonActions = new Dictionary<string, (AbstractFactory, IAbstractDrawStrategy, string)>
    {
        { "CircleButton", (new CircleFactory(), new EllipseDrawStrategy(), "Выбранный компонент: круг") },
        { "EllipseButton", (new EllipseFactory(), new EllipseDrawStrategy(), "Выбранный компонент: эллипс") },
        { "SquareButton", (new SquareFactory(), new RectangleDrawStrategy(), "Выбранный компонент: квадрат") },
        { "RectangleButton", (new RectangleFactory(), new RectangleDrawStrategy(), "Выбранный компонент: прямоугольник") },
        { "LineButton", (new LineFactory(), new LineDrawStrategy(), "Выбранный компонент: линия") },
        { "EquilateralTriangleButton", (new EquilateralTriangleFactory(), new TriangleDrawStrategy(), "Выбранный компонент: равносторонний треугольник") },
        { "IsoscelesTriangleButton", (new IsoscelesTriangleFactory(), new TriangleDrawStrategy(), "Выбранный компонент: равнобедренный треугольник") },
        { "RightTriangleButton", (new RightTriangleFactory(), new TriangleDrawStrategy(), "Выбранный компонент: прямоугольный треугольник") },
        { "ArcButton", (new ArcFactory(), new ArcDrawStrategy(), "Выбранный компонент: дуга") }
    };
    
    bool _isHandledButton;
    MyPoint _downMyPoint;
    MyPoint _upMyPoint;

    AbstractFactory Factory { get; set; } = new CircleFactory();

    IAbstractDrawStrategy DrawStrategy { get; set; } = new EllipseDrawStrategy();

    public MainWindow() => InitializeComponent();

    void Canvas_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        int count = Canvas.Children.Count; 
        if (count > 0)
            Canvas.Children.RemoveAt(count - 1);
    }
    
    void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _isHandledButton = false;
        var mousePosition = e.GetPosition(Canvas);
        _downMyPoint = new MyPoint(mousePosition.X, mousePosition.Y);
    }

    void Canvas_OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            var mousePosition = e.GetPosition(Canvas);
            
            if (_isHandledButton)
            {
                Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
            }
            else
                _isHandledButton = true;
            
            AbstractShape shape = Factory.CreateShape(new MyPoint(_downMyPoint.X, _downMyPoint.Y), 
                new MyPoint(mousePosition.X, mousePosition.Y),  Canvas.Background, PenColorPicker.SelectedBrush);
            DrawStrategy.Draw(shape, Canvas);
        }
    }

    void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            var mousePosition = e.GetPosition(Canvas);
            _upMyPoint = new MyPoint(mousePosition.X, mousePosition.Y);

            if (_isHandledButton)
            {
                Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
                _isHandledButton = false;
                AbstractShape shape = Factory.CreateShape(new MyPoint(_downMyPoint.X, _downMyPoint.Y),
                        new MyPoint(_upMyPoint.X, _upMyPoint.Y), FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush);
                DrawStrategy.Draw(shape, Canvas);
            }
        }
    }

    void Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string buttonName = button.Name;
            if (_buttonActions.TryGetValue(buttonName, out var action))
            {
                Factory = action.Factory;
                DrawStrategy = action.Strategy;
            }
        }
    }
}

