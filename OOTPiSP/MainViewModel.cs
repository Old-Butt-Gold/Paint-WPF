using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using OOTPiSP.Commands;
using OOTPiSP.Factory;
using SharedComponents;

namespace OOTPiSP;

public class MainViewModel : INotifyPropertyChanged
{
    const int DefaultAngleRotation = 2;
    const int DefaultMoveCoordinate = 2;

    public Brush FillSelectedBrush { get; set; } = Brushes.Black;
    public Brush StrokeSelectedBrush { get; set; } = Brushes.Black;
    
    public List<AbstractShape> AbstractShapes { get; set; } = [];
    
    public MyPoint MouseEndPosition { get; set; }
    
    public MyPoint DownMyPoint { get; set; }
    public MyPoint UpMyPoint { get; set; }
    
    public bool IsHandledButton { get; set; }
    
    public int Angle { get; set; }
    public int ArrowsX { get; set; }
    public int ArrowsY { get; set; }
    
    public ICommand MinimizeWindowCommand { get; private set; }
    public ICommand MaximizeWindowCommand { get; private set; }
    public ICommand ExitWindowCommand { get; private set; }
    public ICommand LoadPluginCommand { get; private set; }
    public ICommand XMLSaveCommand { get; private set; }
    public ICommand JsonSaveCommand { get; private set; }
    public ICommand JsonLoadCommand { get; private set; }
    public ICommand XMLLoadCommand { get; private set; }
    public ICommand ClearCommand { get; private set; }
    public ICommand SelectShapeCommand { get; private set; }
    public ICommand MouseWheelCommand { get; private set; }
    public ICommand MouseUpCommand { get; private set; }
    public ICommand MouseLeftDownCommand { get; private set; }
    public ICommand MouseRightDownCommand { get; private set; }
    public ICommand MouseMoveCommand { get; private set; }
    public ICommand RotateLeftCommand {get; private set;}
    public ICommand RotateRightCommand {get; private set;} 
    public ICommand RotateResetCommand {get; private set;}
    public ICommand MoveUpCommand {get; private set;}
    public ICommand MoveDownCommand {get; private set;} 
    public ICommand MoveRightCommand {get; private set;}
    public ICommand MoveLeftCommand {get; private set;} 
    
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
   
    AbstractFactory _factory = new CircleFactory();
    public AbstractFactory Factory
    {
        get => _factory;
        set
        {
            _factory = value;
            OnPropertyChanged(nameof(Factory));
        }
    }

    public MainViewModel()
    {
        MinimizeWindowCommand = new RelayCommand(MinimizeWindow);
        MaximizeWindowCommand = new RelayCommand(MaximizeWindow);
        ExitWindowCommand = new RelayCommand(ExitWindow);
        LoadPluginCommand = new RelayCommand(LoadPlugin);
        XMLSaveCommand = new RelayCommand(XmlSave);
        JsonSaveCommand = new RelayCommand(JsonSave);
        JsonLoadCommand = new RelayCommand(JsonLoad);
        XMLLoadCommand = new RelayCommand(XmlLoad);
        ClearCommand = new RelayCommand(Clear);
        SelectShapeCommand = new RelayCommand(SelectShape);
        
        MouseWheelCommand = new RelayCommand(MouseWheel, _ => AbstractShape.Canvas.Children.Count > 0);
        MouseUpCommand = new RelayCommand(MouseUp);
        MouseLeftDownCommand = new RelayCommand(MouseLeftDown);
        MouseRightDownCommand = new RelayCommand(MouseRightDown);
        MouseMoveCommand = new RelayCommand(MouseMove);
        
        RotateLeftCommand = new RelayCommand(RotateLeft, _ => AbstractShape.Canvas.Children.Count > 0);
        RotateRightCommand = new RelayCommand(RotateRight, _ => AbstractShape.Canvas.Children.Count > 0);
        RotateResetCommand = new RelayCommand(RotateReset, _ => AbstractShape.Canvas.Children.Count > 0);
        MoveUpCommand = new RelayCommand(MoveUp, _ => AbstractShape.Canvas.Children.Count > 0);
        MoveDownCommand = new RelayCommand(MoveDown, _ => AbstractShape.Canvas.Children.Count > 0);
        MoveLeftCommand = new RelayCommand(MoveLeft, _ => AbstractShape.Canvas.Children.Count > 0);
        MoveRightCommand = new RelayCommand(MoveRight, _ => AbstractShape.Canvas.Children.Count > 0);
    }

    private void MouseMove(object obj)
    {
        if (obj is MouseEventArgs e)
        {
            if (DownMyPoint is null) //Поскольку если начать вести от другого элемента, то будет Exception
                return;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (IsHandledButton)
                {
                    RemoveLastShape();
                }
                else
                {
                    IsHandledButton = true;
                }

                MouseEndPosition = GetMousePosition(e);
                var topLeftX = DownMyPoint.X + ArrowsX;
                var topLeftY = DownMyPoint.Y + ArrowsY;
                var downRightX = MouseEndPosition.X + ArrowsX;
                var downRightY = MouseEndPosition.Y + ArrowsY;

                AbstractShape shape = Factory.CreateShape(new(topLeftX, topLeftY),
                    new(downRightX, downRightY),
                    FillSelectedBrush, StrokeSelectedBrush, Angle);
                shape.DrawAlgorithm();
                AbstractShapes.Add(shape);
                SetHandlers(shape.CanvasIndex);
            }
        }
    }

    void RemoveLastShape()
    {
        int count = AbstractShapes.Count;
        if (count > 0)
        {
            AbstractShape.Canvas.Children.RemoveAt(count - 1);
            AbstractShapes.RemoveAt(count - 1);
        }

    }

    private void MouseRightDown(object obj)
    {
        if (obj is MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                RemoveLastShape();
                return;
            }

            if (e is { ClickCount: 1, OriginalSource: Shape shape })
            {
                int tag = (int)shape.Tag;
                for (int i = tag + 1; i < AbstractShape.Canvas.Children.Count; i++)
                {
                    if (AbstractShape.Canvas.Children[i] is Shape item)
                    {
                        int tagTemp = (int)item.Tag;
                        item.Tag = --tagTemp;
                    }
                }
                AbstractShape.Canvas.Children.RemoveAt(tag);

                for (int i = tag + 1; i < AbstractShapes.Count; i++)
                {
                    AbstractShapes[i].CanvasIndex--;
                }

                AbstractShapes.RemoveAt(tag);
            }
        }
    }

    private void MouseLeftDown(object obj)
    {
        ResetManipulationParams();
        if (obj is MouseButtonEventArgs e)
        {
            if (e is { ClickCount: 2, Source: Shape frameworkElement })
            {
                var tag = (int) frameworkElement.Tag;

                var shape = AbstractShapes[tag];

                var shapeEditorWindow = new ShapeEditorWindow(shape);
                shapeEditorWindow.ShowDialog();
                shape.DrawAlgorithm();

                SetHandlers(shape.CanvasIndex);
                return;
            }
        
            DownMyPoint = GetMousePosition(e);
        }
    }
    
    MyPoint GetMousePosition(MouseEventArgs e)
    {
        var mousePosition = e.GetPosition(AbstractShape.Canvas);
        return new(mousePosition.X, mousePosition.Y);
    }

    public void SetHandlers(int canvasIndex)
    {
        var canvasChild = AbstractShape.Canvas.Children[canvasIndex];
        canvasChild.PreviewMouseUp += (_, e) =>
        {
            if (MouseUpCommand.CanExecute(e))
                MouseUpCommand.Execute(e);
        };
        
        canvasChild.PreviewMouseWheel += (_, e) =>
        {
            if (MouseWheelCommand.CanExecute(e))
                MouseWheelCommand.Execute(e);
        };

        canvasChild.MouseEnter += (sender, _) =>
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
        };
        canvasChild.MouseLeave += (sender, args) =>
        {
            if (sender is Shape s)
            {
                s.ClearValue(Shape.EffectProperty);
                s.StrokeThickness -= 1;
            }
        };
    }

    void MouseUp(object obj)
    {
        ResetManipulationParams();
    }

    void ResetManipulationParams()
    {
        DownMyPoint = null;
        UpMyPoint = null;
        Angle = 0;
        ArrowsX = 0;
        ArrowsY = 0;
        MouseEndPosition = null;
        IsHandledButton = false;   
    }

    void RotateLeft(object obj)
    {
        Rotate(-DefaultAngleRotation);
    }
    
    void RotateRight(object obj)
    {
        Rotate(DefaultAngleRotation);
    }
    
    void RotateReset(object obj)
    {
        var currentIndex = AbstractShape.Canvas.Children.Count - 1;
        var shape = AbstractShapes[currentIndex];
        shape.Angle = 0;
        Angle = 0;
        shape.DrawAlgorithm();
        SetHandlers(shape.CanvasIndex);
    }

    void MoveUp(object obj)
    {
        Move(0, -DefaultMoveCoordinate);
    }
    
    void MoveDown(object obj)
    {
        Move(0, DefaultMoveCoordinate);
    }
    
    void MoveLeft(object obj)
    {
        Move(-DefaultMoveCoordinate, 0);
    }
    
    void MoveRight(object obj)
    {
        Move(DefaultMoveCoordinate, 0);
    }

    void MouseWheel(object obj)
    {
        if (obj is MouseWheelEventArgs e)
        {
            Rotate(e.Delta > 0 ? DefaultAngleRotation : -DefaultAngleRotation);
        }
    }
    
    void Rotate(int angleDelta)
    {
        var currentIndex = AbstractShape.Canvas.Children.Count - 1;
        var shape = AbstractShapes[currentIndex];
        shape.Angle += angleDelta;
        Angle = shape.Angle;
        shape.DrawAlgorithm();
        SetHandlers(shape.CanvasIndex);
    }

    void Move(int deltaX, int deltaY)
    {
        var currentIndex = AbstractShape.Canvas.Children.Count - 1;
        var shape = AbstractShapes[currentIndex];

        ArrowsX += deltaX;
        ArrowsY += deltaY;
        
        shape.TopLeft.X += deltaX;
        shape.TopLeft.Y += deltaY;
        
        shape.DownRight.X += deltaX;
        shape.DownRight.Y += deltaY;

        //Для корректного создания фигуры после движения фигуры при движении мыши
        MouseEndPosition.X += deltaX;
        MouseEndPosition.Y += deltaY;
        
        shape.DrawAlgorithm();
        SetHandlers(shape.CanvasIndex);
    }

    void SelectShape(object parameter)
    {
        if (_buttonActions.TryGetValue(parameter, out var action))
        {
            Factory = action;
        }
    }

    void Clear(object parameter)
    {
        if (parameter is MainWindow window)
        {
            AbstractShapes.Clear();
            window.Canvas.Children.Clear();
        }
    }

    private void XmlLoad(object parameter)
    {
        if (parameter is MainWindow window)
        {
            var listShapes = new MyXMLSerializer().Deserialize();
            if (listShapes is not null)
            {
                AbstractShapes.Clear();
                window.Canvas.Children.Clear();
                foreach (var item in listShapes)
                {
                    if (_buttonActions.TryGetValue(item.TagShape, out var factory))
                    {
                        var shape = factory.CreateShape(item.TopLeft, item.DownRight, item.BackgroundColor, item.PenColor, item.Angle);
                        shape.StrokeThickness = item.StrokeThickness;

                        AbstractShapes.Add(shape);
                        shape.DrawAlgorithm();

                        SetHandlers(shape.CanvasIndex);
                    }
                }
                MessageBox.Show($"Список фигур успешно загружен!");
            }
        }
    }

    void JsonLoad(object parameter)
    {
        if (parameter is MainWindow window)
        {
            var loadedShapes = new MyJsonSerializer().Deserialize();
            if (loadedShapes is { Count: not 0 })
            {
                AbstractShapes = loadedShapes;
                window.Canvas.Children.Clear();
                //Надо, ибо JSON заполняет свойства и вызывается метод отрисовки
                AbstractShapes.ForEach(shape => shape.CanvasIndex = -1);
                
                foreach (var shape in loadedShapes)
                {
                    shape.DrawAlgorithm();
                    SetHandlers(shape.CanvasIndex);
                }
                MessageBox.Show($"Список фигур успешно загружен!");
            }
        }
    }

    void JsonSave(object parameter)
    {
        if (parameter is MainWindow window)
        {
            new MyJsonSerializer().Serialize(AbstractShapes);
        }
    }

    void XmlSave(object parameter)
    {
        if (parameter is MainWindow window)
        {
            new MyXMLSerializer().Serialize(AbstractShapes);
        }
    }

    void LoadPlugin(object parameter)
    {
        if (parameter is MainWindow window)
        {
            PluginLoader pluginLoader = new();
            var factoryList = pluginLoader.LoadPlugin();
            foreach (var factory in factoryList)
            {
                var shape = factory.CreateShape(new(), new(), Brushes.Black, Brushes.Black, 0);
                _buttonActions[shape.TagShape] = factory;

                Button newButton = new Button
                {
                    Content = shape.ToString(),
                    Style = (Style)window.FindResource("ButtonStyle"),
                    Tag = shape.TagShape,
                    Command = SelectShapeCommand,
                };

                //Установление привязки
                Binding binding = new()
                {
                    RelativeSource = RelativeSource.Self,
                    Path = new PropertyPath("Tag")
                };
                newButton.SetBinding(Button.CommandParameterProperty, binding);


                int columnIndex = window.ButtonGrid.ColumnDefinitions.Count;
                window.ButtonGrid.ColumnDefinitions.Add(
                    new()
                    {
                        Width = new GridLength(1, GridUnitType.Star),
                    });
                Grid.SetColumn(newButton, columnIndex);
                window.ButtonGrid.Children.Add(newButton);
            }
        }
    }

    void MinimizeWindow(object parameter)
    {
        if (parameter is Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
    }

    void MaximizeWindow(object parameter)
    {
        if (parameter is Window window)
        {
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
    }

    void ExitWindow(object parameter)
    {
        if (parameter is Window window)
        {
            if (MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                window.Close();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}