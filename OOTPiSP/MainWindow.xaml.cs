using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OOTPiSP.Factory;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;

namespace OOTPiSP;

public partial class MainWindow
{
    const int DefaultAngleRotation = 3;
    
    readonly Dictionary<string, (AbstractFactory Factory, IAbstractDrawStrategy Strategy)> _buttonActions = new()
    {
        { "CircleButton", (new CircleFactory(), new EllipseDrawStrategy()) },
        { "EllipseButton", (new EllipseFactory(), new EllipseDrawStrategy()) },
        { "SquareButton", (new SquareFactory(), new RectangleDrawStrategy()) },
        { "RectangleButton", (new RectangleFactory(), new RectangleDrawStrategy()) },
        { "LineButton", (new LineFactory(), new LineDrawStrategy()) },
        { "EquilateralTriangleButton", (new EquilateralTriangleFactory(), new TriangleDrawStrategy()) },
        { "IsoscelesTriangleButton", (new IsoscelesTriangleFactory(), new TriangleDrawStrategy()) },
        { "RightTriangleButton", (new RightTriangleFactory(), new TriangleDrawStrategy()) },
        { "ArcButton", (new ArcFactory(), new ArcDrawStrategy()) }
    };
    
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
    
    bool _isHandledButton;
    MyPoint _downMyPoint;
    MyPoint _upMyPoint;

    AbstractFactory Factory { get; set; } = new CircleFactory();

    IAbstractDrawStrategy DrawStrategy { get; set; } = new EllipseDrawStrategy();

    int _angle;
    int _arrowsX;
    int _arrowsY;
    
    MouseEventArgs? _mouseArgs;
    
    public MainWindow()
    {
        InitializeComponent();
        CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (_, _) =>
        {
            if (MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }));
    }
    
    void Canvas_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        int count = Canvas.Children.Count; 
        if (count > 0)
            Canvas.Children.RemoveAt(count - 1);
    }
    
    void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _angle = 0;
        _arrowsX = 0;
        _arrowsY = 0;
        _isHandledButton = false;
        var mousePosition = e.GetPosition(Canvas);
        _downMyPoint = new MyPoint(mousePosition.X, mousePosition.Y);
    }

    void Canvas_OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        _mouseArgs = e;
        
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
            DrawStrategy.Draw(shape, Canvas, _angle);
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
                DrawStrategy.Draw(shape, Canvas, _angle);
            }
        }
        _mouseArgs = null;
    }
    
    void Canvas_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        _angle = e.Delta > 0 ? _angle + DefaultAngleRotation : _angle - DefaultAngleRotation;
        ChangeAngleShape(sender);
    }

    void RotateLeft_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        _angle -= DefaultAngleRotation;
        ChangeAngleShape(sender);
    }
    
    void RotateRight_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        _angle += DefaultAngleRotation;
        ChangeAngleShape(sender);
    }
    
    void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        _angle = 0;
        ChangeAngleShape(sender);
    }

    void ChangeAngleShape(object sender)
    {
        if (_angle < 0)
            _angle += 360;
        _angle %= 360;
        
        if (_mouseArgs != null)
        {
            Canvas_OnPreviewMouseMove(sender, _mouseArgs);
        }
    }
}