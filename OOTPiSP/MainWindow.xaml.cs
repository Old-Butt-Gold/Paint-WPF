using System.Reflection;
using System.Runtime.Loader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Microsoft.Win32;
using OOTPiSP.Factory;
using SharedComponents;

namespace OOTPiSP;

public partial class MainWindow
{
    const int DefaultAngleRotation = 2;
    const int DefaultMoveCoordinate = 2;
    
    public List<AbstractShape> AbstractShapes { get; set; } = [];
    
    bool _isHandledButton;
    MyPoint _downMyPoint;
    MyPoint _upMyPoint;

    int _angle;
    int _arrowsX;
    int _arrowsY;
    
    MouseEventArgs? _mouseArgs;

    public MainViewModel MainViewModel { get; set; } = new();

    public MainWindow() => InitializeComponent();

    void Canvas_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            RemoveLastShape();
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

            for (int i = tag + 1; i < AbstractShapes.Count; i++)
            {
                AbstractShapes[i].CanvasIndex--;
            }

            AbstractShapes.RemoveAt(tag);
        }
    }
    
    void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e is { ClickCount: 2, Source: Shape frameworkElement })
        {
            var tag = (int) frameworkElement.Tag;

            var shape = AbstractShapes[tag];

            var shapeEditorWindow = new ShapeEditorWindow(shape);
            shapeEditorWindow.ShowDialog();
            
            shape.DrawAlgorithm(Canvas);

            SetHandlers(shape.CanvasIndex);
            return;
        }
        
        ResetManipulationParams();
        _downMyPoint = GetMousePosition(e);
    }

    void Canvas_OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (_downMyPoint is null) //Поскольку если начать вести от другого элемента, то будет Exception
            return;
        
        _mouseArgs = e;
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            if (_isHandledButton)
            {
                RemoveLastShape();
            }
            else
            {
                _isHandledButton = true;
            }

            var mousePosition = e.GetPosition(Canvas);
            var topLeftX = _downMyPoint.X + _arrowsX;
            var topLeftY = _downMyPoint.Y + _arrowsY;
            var downRightX = mousePosition.X + _arrowsX;
            var downRightY = mousePosition.Y + _arrowsY;
            DrawShape(topLeftX, topLeftY, downRightX, downRightY, Brushes.Transparent);
        }
    }

    void Canvas_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            _upMyPoint = GetMousePosition(e);

            if (_isHandledButton)
            {
                RemoveLastShape();
                _isHandledButton = false;
                
                var topLeftX = _downMyPoint.X + _arrowsX;
                var topLeftY = _downMyPoint.Y + _arrowsY;
                var downRightX = _upMyPoint.X + _arrowsX;
                var downRightY = _upMyPoint.Y + _arrowsY;
             
                DrawShape(topLeftX, topLeftY, downRightX, downRightY, FillColorPicker.SelectedBrush);
            }
        }

        _downMyPoint = null;
        _upMyPoint = null;
        _mouseArgs = null;
    }
    
    void RemoveLastShape()
    {
        int count = Canvas.Children.Count;
        if (count > 0)
        {
            Canvas.Children.RemoveAt(count - 1);
            AbstractShapes.RemoveAt(count - 1);
        }

    }
    
    void ResetManipulationParams()
    {
        _angle = 0;
        _arrowsX = 0;
        _arrowsY = 0;
        _isHandledButton = false;
    }
    
    MyPoint GetMousePosition(MouseEventArgs e)
    {
        var mousePosition = e.GetPosition(Canvas);
        return new(mousePosition.X, mousePosition.Y);
    }

    void DrawShape(double topLeftX, double topLeftY, double downRightX, double downRightY, Brush bg)
    {
        AbstractShape shape = MainViewModel.Factory.CreateShape(new(topLeftX, topLeftY), new(downRightX, downRightY),
            bg, PenColorPicker.SelectedBrush, _angle);
        
        AbstractShapes.Add(shape);
        shape.DrawAlgorithm(Canvas);

        SetHandlers(shape.CanvasIndex);
    }

    public void SetHandlers(int canvasIndex)
    {
        Canvas.Children[canvasIndex].PreviewMouseUp += Canvas_OnPreviewMouseUp;
        Canvas.Children[canvasIndex].PreviewMouseWheel += Canvas_OnPreviewMouseWheel;
        Canvas.Children[canvasIndex].MouseEnter += Shape_MouseEnter;
        Canvas.Children[canvasIndex].MouseLeave += Shape_MouseLeave;
    }

    void Shape_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is Shape s)
        {
            var effect = new DropShadowEffect
            {
                BlurRadius = 0,
                ShadowDepth = 0,
                RenderingBias = RenderingBias.Quality
            };

            var colorAnimation = new ColorAnimation
            {
                To = ((SolidColorBrush)s.Stroke).Color,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut },
            };

            var blurAnimation = new DoubleAnimation
            {
                To = 25,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut }
            };
                
            effect.BeginAnimation(DropShadowEffect.ColorProperty, colorAnimation);
            effect.BeginAnimation(DropShadowEffect.BlurRadiusProperty, blurAnimation);
            s.Effect = effect;

            s.StrokeThickness += 1;
        }
    }
    
    void Shape_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is Shape s)
        {
            s.Effect = null;
            s.StrokeThickness -= 1;
        }
    }
    
    void Canvas_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) => Rotate(sender, e.Delta > 0 ? DefaultAngleRotation : -DefaultAngleRotation);
    void RotateLeft_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Rotate(sender, -DefaultAngleRotation);
    void RotateRight_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Rotate(sender, DefaultAngleRotation);
    void RotateReset_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Rotate(sender, 360 - _angle);
    void MoveUp_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, 0, -DefaultMoveCoordinate);
    void MoveDown_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, 0, DefaultMoveCoordinate);
    void MoveRight_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, DefaultMoveCoordinate, 0);
    void MoveLeft_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, -DefaultMoveCoordinate, 0);
    
    void Rotate(object sender, int angleDelta)
    {
        _angle += angleDelta;
        
        if (_mouseArgs != null)
        {
            Canvas_OnPreviewMouseMove(sender, _mouseArgs);
        }
    }

    void Move(object sender, int deltaX, int deltaY)
    {
        _arrowsX += deltaX;
        _arrowsY += deltaY;
        if (_mouseArgs != null)
        {
            Canvas_OnPreviewMouseMove(sender, _mouseArgs);
        }
    }
}