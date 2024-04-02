using System.Windows.Input;

namespace OOTPiSP.Commands;

public static class ShapeCommands
{
    public static RoutedCommand RotateLeft { get; set; }
    public static RoutedCommand RotateRight { get; set; }
    public static RoutedCommand RotateReset { get; set; }
    public static RoutedCommand MoveUp { get; set; }
    public static RoutedCommand MoveLeft { get; set; }
    public static RoutedCommand MoveRight { get; set; }
    public static RoutedCommand MoveDown { get; set; }
    
    static ShapeCommands()
    {
        RotateLeft = new RoutedCommand("RotateLeft", typeof(MainWindow));
        RotateRight = new RoutedCommand("RotateRight", typeof(MainWindow));
        RotateReset = new RoutedCommand("RotateReset", typeof(MainWindow));
        MoveUp = new RoutedCommand("MoveUp", typeof(MainWindow));
        MoveLeft = new RoutedCommand("MoveLeft", typeof(MainWindow));
        MoveRight = new RoutedCommand("MoveRight", typeof(MainWindow));
        MoveDown = new RoutedCommand("MoveDown", typeof(MainWindow));
    }
}