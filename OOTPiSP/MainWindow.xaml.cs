using SharedComponents;

namespace OOTPiSP;

public partial class MainWindow
{
    public MainViewModel MainViewModel { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        AbstractShape.Canvas = Canvas;
    }

}