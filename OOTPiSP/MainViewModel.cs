using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using OOTPiSP.Commands;
using OOTPiSP.Factory;
using SharedComponents;

namespace OOTPiSP;

public class MainViewModel : INotifyPropertyChanged
{
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
            window.AbstractShapes.Clear();
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
                window.AbstractShapes.Clear();
                window.Canvas.Children.Clear();
                foreach (var item in listShapes)
                {
                    if (_buttonActions.TryGetValue(item.TagShape, out var factory))
                    {
                        var shape = factory.CreateShape(item.TopLeft, item.DownRight, item.BackgroundColor, item.PenColor, item.Angle);
                        shape.StrokeThickness = item.StrokeThickness;

                        window.AbstractShapes.Add(shape);
                        shape.DrawAlgorithm(window.Canvas);

                        window.SetHandlers(shape.CanvasIndex);
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
                window.AbstractShapes = loadedShapes;
                window.Canvas.Children.Clear();
                foreach (var shape in loadedShapes)
                {
                    shape.DrawAlgorithm(window.Canvas);
                    window.SetHandlers(shape.CanvasIndex);
                }
                MessageBox.Show($"Список фигур успешно загружен!");
            }
        }
    }

    void JsonSave(object parameter)
    {
        if (parameter is MainWindow window)
        {
            new MyJsonSerializer().Serialize(window.AbstractShapes);
        }
    }

    void XmlSave(object parameter)
    {
        if (parameter is MainWindow window)
        {
            new MyXMLSerializer().Serialize(window.AbstractShapes);
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
            if (MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
                window.Close();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}