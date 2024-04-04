using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using SharedComponents;

namespace OOTPiSP;

public partial class MainWindow
{
    public MainViewModel MainViewModel { get; set; } = new();

    public MainWindow()
    {
        InitializeComponent();
        AbstractShape.Canvas = Canvas;
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

            for (int i = tag + 1; i < MainViewModel.AbstractShapes.Count; i++)
            {
                MainViewModel.AbstractShapes[i].CanvasIndex--;
            }

            MainViewModel.AbstractShapes.RemoveAt(tag);
        }
    }
    
    void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e is { ClickCount: 2, Source: Shape frameworkElement })
        {
            var tag = (int) frameworkElement.Tag;

            var shape = MainViewModel.AbstractShapes[tag];

            var shapeEditorWindow = new ShapeEditorWindow(shape);
            shapeEditorWindow.ShowDialog();
            shape.DrawAlgorithm();

            SetHandlers(shape.CanvasIndex);
            return;
        }
        
        MainViewModel.DownMyPoint = GetMousePosition(e);
    }

    void Canvas_OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (MainViewModel.DownMyPoint is null) //Поскольку если начать вести от другого элемента, то будет Exception
            return;

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            if (MainViewModel.IsHandledButton)
            {
                RemoveLastShape();
            }
            else
            {
                MainViewModel.IsHandledButton = true;
            }

            MainViewModel.MouseEndPosition = GetMousePosition(e);
            var topLeftX = MainViewModel.DownMyPoint.X + MainViewModel.ArrowsX;
            var topLeftY = MainViewModel.DownMyPoint.Y + MainViewModel.ArrowsY;
            var downRightX = MainViewModel.MouseEndPosition.X + MainViewModel.ArrowsX;
            var downRightY = MainViewModel.MouseEndPosition.Y + MainViewModel.ArrowsY;
            
            AbstractShape shape = MainViewModel.Factory.CreateShape(new(topLeftX, topLeftY), new(downRightX, downRightY),
                FillColorPicker.SelectedBrush, PenColorPicker.SelectedBrush, MainViewModel.Angle);
            shape.DrawAlgorithm();
            MainViewModel.AbstractShapes.Add(shape);
            SetHandlers(shape.CanvasIndex);
        }
    }
    
    void RemoveLastShape()
    {
        int count = Canvas.Children.Count;
        if (count > 0)
        {
            Canvas.Children.RemoveAt(count - 1);
            MainViewModel.AbstractShapes.RemoveAt(count - 1);
        }

    }
    
    
    MyPoint GetMousePosition(MouseEventArgs e)
    {
        var mousePosition = e.GetPosition(Canvas);
        return new(mousePosition.X, mousePosition.Y);
    }

    public void SetHandlers(int canvasIndex)
    {
        Canvas.Children[canvasIndex].PreviewMouseUp += (_, e) =>
        {
            if (MainViewModel.MouseUpCommand.CanExecute(e))
                MainViewModel.MouseUpCommand.Execute(e);
        };
        
        Canvas.Children[canvasIndex].PreviewMouseWheel += (_, e) =>
        {
            if (MainViewModel.MouseWheelCommand.CanExecute(e))
                MainViewModel.MouseWheelCommand.Execute(e);
        };
        
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

}