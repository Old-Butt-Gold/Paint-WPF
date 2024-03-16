using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using OOTPiSP.Factory;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;

namespace OOTPiSP;

public partial class MainWindow
{
    const int DefaultAngleRotation = 3;
    const int DefaultMoveCoordinate = 3;
    
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
    
    AbstractFactory Factory { get; set; } = new CircleFactory();
    IAbstractDrawStrategy DrawStrategy { get; set; } = new EllipseDrawStrategy();
    
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

    int _angle;
    int _arrowsX;
    int _arrowsY;
    
    MouseEventArgs? _mouseArgs;

    public MainWindow()
    {
        InitializeComponent();
        CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (_, _) =>
        {
            if (MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
                Close();
        }));

    }

    void Canvas_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            int count = Canvas.Children.Count;
            if (count > 0)
                Canvas.Children.RemoveAt(count - 1);
            return;
        }

        if (e is { ClickCount: 1, OriginalSource: Shape shape })
        {
            int tag = (int)shape.Tag;
            for (int i = tag + 1; i < Canvas.Children.Count; i++)
            {
                if (Canvas.Children[i] is Shape item)
                {
                    int tagTemp = (int)item.Tag;
                    item.Tag = --tagTemp;
                }
            }
            Canvas.Children.RemoveAt(tag);
        }
    }
    
    void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            if (e.Source is Shape frameworkElement)
            {
                MessageBox.Show(frameworkElement.Tag.ToString());
            }
            return;
        }
        
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
            
            AbstractShape shape = Factory.CreateShape(new MyPoint(_downMyPoint.X + _arrowsX, _downMyPoint.Y + _arrowsY), 
                new MyPoint(mousePosition.X + _arrowsX, mousePosition.Y + _arrowsY),  Canvas.Background, PenColorPicker.SelectedBrush);
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
                AbstractShape shape = Factory.CreateShape(new MyPoint(_downMyPoint.X + _arrowsX, _downMyPoint.Y + _arrowsY),
                        new MyPoint(_upMyPoint.X + _arrowsX, _upMyPoint.Y + _arrowsY), FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush);
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
    
    void RotateReset_OnExecuted(object sender, ExecutedRoutedEventArgs e)
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

    void MoveUp_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        _arrowsY -= DefaultMoveCoordinate;
        ChangeAngleShape(sender);
    }

    void MoveDown_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        _arrowsY += DefaultMoveCoordinate;
        ChangeAngleShape(sender);
    }

    void MoveRight_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        _arrowsX += DefaultMoveCoordinate;
        ChangeAngleShape(sender);
    }

    void MoveLeft_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        _arrowsX -= DefaultMoveCoordinate;
        ChangeAngleShape(sender);
    }
}