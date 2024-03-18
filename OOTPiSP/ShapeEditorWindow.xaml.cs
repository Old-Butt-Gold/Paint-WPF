using System.ComponentModel;
using System.Windows;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP;

public partial class ShapeEditorWindow : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    AbstractShape _shape;
    public AbstractShape Shape
    {
        get => _shape;
        set
        {
            _shape = value;
            OnPropertyChanged(nameof(Shape));
        }
    }

    public ShapeEditorWindow(AbstractShape shape)
    {
        InitializeComponent();
        Shape = shape;
    }

    void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        Shape.Angle %= 360;

        if (Shape.Angle < 0)
            Shape.Angle += 360;
    }

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}