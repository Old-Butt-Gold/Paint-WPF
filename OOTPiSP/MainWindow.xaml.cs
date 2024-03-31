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
    
    readonly Dictionary<object, AbstractFactory> _buttonActions = new()
    {
        { "0", new CircleFactory() },
        { "1", new EllipseFactory() },
        { "2", new SquareFactory() },
        { "3", new RectangleFactory() },
        { "4", new LineFactory() },
        { "5", new EquilateralTriangleFactory() },
        { "6", new IsoscelesTriangleFactory() },
        { "7", new RightTriangleFactory() },
        { "8", new ArcFactory() }
    };

    MyXMLSerializer MyXmlSerializer { get; set; } = new();
    MyJsonSerializer MyJsonSerializer { get; set; } = new();
    
    AbstractFactory Factory { get; set; } = new CircleFactory();
    List<AbstractShape> AbstractShapes { get; set; } = [];
    
    void Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && _buttonActions.TryGetValue(button.Tag, out var action))
        {
             Factory = action;
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
        AbstractShape shape = Factory.CreateShape(new(topLeftX, topLeftY), new(downRightX, downRightY),
            bg, PenColorPicker.SelectedBrush, _angle);
        
        AbstractShapes.Add(shape);
        shape.DrawAlgorithm(Canvas);

        SetHandlers(shape.CanvasIndex);
    }

    void SetHandlers(int canvasIndex)
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
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut },
            };

            var blurAnimation = new DoubleAnimation
            {
                To = 30,
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
    }
    
    void Shape_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is Shape s)
        {
            s.Effect = null;
            s.StrokeThickness -= 3;
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

    void Maximize_OnClick(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    
    void Minimize_OnClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    void JSONSave_OnClick(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            if (!saveFileDialog.FileName.EndsWith(".json"))
            {
                saveFileDialog.FileName += ".json";
            }

            MyJsonSerializer.Serialize(saveFileDialog.FileName, AbstractShapes);
        }
    }
    
    void XMLSave_OnClick(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            if (!saveFileDialog.FileName.EndsWith(".xml"))
            {
                saveFileDialog.FileName += ".xml";
            }
            MyXmlSerializer.Serialize(saveFileDialog.FileName, AbstractShapes);
        }
    }

    void JSONOpen_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "JSON файлы (*.json)|*.json"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            var loadedShapes = MyJsonSerializer.Deserialize(openFileDialog.FileName);
            if (loadedShapes is { Count: not 0 })
            {
                AbstractShapes = loadedShapes;
                Canvas.Children.Clear();
                foreach (var shape in AbstractShapes)
                {
                    shape.DrawAlgorithm(Canvas);

                    SetHandlers(shape.CanvasIndex);
                }
                MessageBox.Show($"Список фигур успешно загружен!");
            }
        }
    }
    
    void XMLOpen_OnClick(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "XML файлы (*.xml)|*.xml"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            var listShapes = MyXmlSerializer.Deserialize(openFileDialog.FileName);
            if (listShapes is not null)
            {
                AbstractShapes.Clear();
                Canvas.Children.Clear();
                foreach (var item in listShapes)
                {
                    if (_buttonActions.TryGetValue(item.TagShape, out var factory))
                    {
                        var shape = factory.CreateShape(
                            item.TopLeft, item.DownRight, item.BackgroundColor, item.PenColor, item.Angle);
                        shape.StrokeThickness = item.StrokeThickness;

                        AbstractShapes.Add(shape);
                        shape.DrawAlgorithm(Canvas);

                        SetHandlers(shape.CanvasIndex);
                    }
                }

                MessageBox.Show($"Список фигур успешно загружен!");
            }
        }
    }

    void ClearCanvas_OnClick(object sender, RoutedEventArgs e)
    {
        AbstractShapes.Clear();
        Canvas.Children.Clear();
    }

    void LoadPlugin_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "Динамическая библиотека (*.dll)|*.dll"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            var context = new AssemblyLoadContext("DynamicLoad", true);
            Assembly assembly = context.LoadFromAssemblyPath(openFileDialog.FileName);
            var types = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(AbstractFactory)));
            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is AbstractFactory factory)
                {
                    var shape = factory.CreateShape(new(), new(), Brushes.Black, Brushes.Black, 0);
                    _buttonActions[shape.TagShape] = factory;

                    Button newButton = new Button
                    {
                        Content = shape.ToString(),
                        Style = (Style)FindResource("ButtonStyle"),
                        Tag = shape.TagShape,
                    };
                    newButton.Click += Button_Click;

                    int columnIndex = ButtonGrid.ColumnDefinitions.Count;
                    ButtonGrid.ColumnDefinitions.Add(
                        new()
                        {
                            Width = new GridLength(1, GridUnitType.Star),
                        });
                    Grid.SetColumn(newButton, columnIndex);
                    ButtonGrid.Children.Add(newButton);
                }
            }
            context.Unload();
        }
    }
}