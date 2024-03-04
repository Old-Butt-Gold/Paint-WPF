using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using OOTPiSP.GeometryFigures;
using OOTPiSP.GeometryFigures.Ellipse;
using OOTPiSP.GeometryFigures.Rectangle;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.GeometryFigures.Triangle;

namespace OOTPiSP;

public partial class MainWindow : Window
{
    MyPoint _downMyPoint;
    MyPoint _upMyPoint;
    bool _isHandledButton;
        
    readonly Random _random = new();

    public MainWindow()
    {
        InitializeComponent();
        
        ShapeList shapes = new ShapeList();

        
        for (int i = 0; i < 5; i++)
        {
            shapes.Add(new MyLine(new MyPoint(50 * i + 50, 50), new MyPoint(50 * i + 250, 500), GetRandomBrush(), GetRandomBrush()));
        }
        
        shapes.DrawAll(Canvas);
    }

    Color GetRandomColor()
    {
        byte[] colorBytes = new byte[3];
        _random.NextBytes(colorBytes);
        return Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
    }

    SolidColorBrush GetRandomBrush() => new(GetRandomColor());
        
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
            //_upMyPoint = new MyPoint(mousePosition.X, mousePosition.Y);
            
            if (_isHandledButton)
            {
                Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
            }
            else
                _isHandledButton = true;
            
            EquilateralMyTriangle tempCircle = new(new MyPoint(_downMyPoint.X, _downMyPoint.Y), 
                new MyPoint(mousePosition.X, mousePosition.Y),  Canvas.Background, GetRandomBrush());
                
            tempCircle.Draw(Canvas);
        }
    }

    void Canvas_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            var mousePosition = e.GetPosition(Canvas);
            _upMyPoint = new MyPoint(mousePosition.X, mousePosition.Y);

            if (_isHandledButton)
            {
                Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
                _isHandledButton = false;
                MyEllipse myCircle =
                    new(new MyPoint(_downMyPoint.X, _downMyPoint.Y),
                        new MyPoint(_upMyPoint.X, _upMyPoint.Y), GetRandomBrush(), GetRandomBrush());
                myCircle.Draw(Canvas);
            }
        }
    }
}