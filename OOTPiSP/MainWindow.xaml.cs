﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
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
        if (sender is Button button && _buttonActions.TryGetValue(button.Name, out var action))
        {
            (Factory, DrawStrategy) = action;
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
        }
    }
    
    void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e is { ClickCount: 2, Source: Shape frameworkElement })
        {
            MessageBox.Show(frameworkElement.Tag.ToString());
            return;
        }
        
        ResetManipulationParams();
        _downMyPoint = GetMousePosition(e);
    }

    void Canvas_OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
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
            DrawShape(topLeftX, topLeftY, downRightX, downRightY);
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
        _mouseArgs = null;
    }
    
    void RemoveLastShape()
    {
        int count = Canvas.Children.Count;
        if (count > 0)
            Canvas.Children.RemoveAt(count - 1);
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

    void DrawShape(double topLeftX, double topLeftY, double downRightX, double downRightY, Brush? bg = null)
    {
        AbstractShape shape = Factory.CreateShape(new(topLeftX, topLeftY), new(downRightX, downRightY),
            bg ?? Canvas.Background, PenColorPicker.SelectedBrush);

        DrawStrategy.Draw(shape, Canvas, _angle);

        Canvas.Children[^1].PreviewMouseUp += Canvas_OnPreviewMouseUp;
        Canvas.Children[^1].PreviewMouseWheel += Canvas_OnPreviewMouseWheel;
        
        Canvas.Children[^1].MouseEnter += (sender, _) => {
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
                    EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut },
                };

                var blurAnimation = new DoubleAnimation
                {
                    To = 100,
                    Duration = TimeSpan.FromSeconds(0.5),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever,
                    EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut }
                };
                
                effect.BeginAnimation(DropShadowEffect.ColorProperty, colorAnimation);
                effect.BeginAnimation(DropShadowEffect.BlurRadiusProperty, blurAnimation);
                s.Effect = effect;

                s.StrokeThickness += 3;
            }
        };
        Canvas.Children[^1].MouseLeave += (sender, _) => {
            if (sender is Shape s)
            {
                s.Effect = null;
                s.StrokeThickness -= 3;
            }
        };
    }
    
    

    void Canvas_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) => Rotate(sender, e.Delta > 0 ? DefaultAngleRotation : -DefaultAngleRotation);
    void RotateLeft_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Rotate(sender, -DefaultAngleRotation);
    void RotateRight_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Rotate(sender, DefaultAngleRotation);
    void RotateReset_OnExecuted(object sender, ExecutedRoutedEventArgs e) => _angle = 0;
    void MoveUp_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, 0, -DefaultMoveCoordinate);
    void MoveDown_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, 0, DefaultMoveCoordinate);
    void MoveRight_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, DefaultMoveCoordinate, 0);
    void MoveLeft_OnExecuted(object sender, ExecutedRoutedEventArgs e) => Move(sender, -DefaultMoveCoordinate, 0);
    
    void Rotate(object sender, int angleDelta)
    {
        _angle += angleDelta;
        
        if (_angle < 0)
            _angle += 360;
        _angle %= 360;
     
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