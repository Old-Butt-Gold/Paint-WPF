﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using OOTPiSP.Factory;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;

namespace OOTPiSP;

public partial class MainWindow : Window
{
    bool _isHandledButton;
    MyPoint _downMyPoint;
    MyPoint _upMyPoint;

    AbstractFactory Factory { get; set; } = new CircleFactory();

    private IAbstractDrawStrategy DrawStrategy { get; set; } = new EllipseDrawStrategy();

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

    void CircleButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Круг";
        Factory = new CircleFactory();
        DrawStrategy = new EllipseDrawStrategy();
    }

    void EllipseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Эллипс";
        Factory = new EllipseFactory();
        DrawStrategy = new EllipseDrawStrategy();
    }

    void SquareButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Квадрат";
        Factory = new SquareFactory();
        DrawStrategy = new RectangleDrawStrategy();
    }

    void RectangleButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Прямоугольник";
        Factory = new RectangleFactory();
        DrawStrategy = new RectangleDrawStrategy();
    }

    void LineButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Линия";
        Factory = new LineFactory();
        DrawStrategy = new LineDrawStrategy();
    }

    void EquilateralTriangleButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Равносторонний треугольник";
        Factory = new EquilateralTriangleFactory();
        DrawStrategy = new TriangleDrawStrategy();
    }

    void IsoscelesTriangleButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Равнобедренный треугольник";
        Factory = new IsoscelesTriangleFactory();
        DrawStrategy = new TriangleDrawStrategy();
    }

    void RightTriangleButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Прямоугольный треугольник";
        Factory = new RightTriangleFactory();
        DrawStrategy = new TriangleDrawStrategy();
    }

    private void ArcButton_OnClick(object sender, RoutedEventArgs e)
    {
        Info.Text = "Выбранный компонент: Арка";
        Factory = new ArcFactory();
        DrawStrategy = new ArcDrawStrategy();
    }
}