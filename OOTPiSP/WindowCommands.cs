using System.Windows.Input;

namespace OOTPiSP;

public class WindowCommands
{
    public static RoutedCommand RotateLeft { get; set; }
    public static RoutedCommand RotateRight { get; set; }
    public static RoutedCommand RotateReset { get; set; }
    
    
    static WindowCommands()
    {
        RotateLeft = new RoutedCommand("RotateLeft", typeof(MainWindow));
        RotateRight = new RoutedCommand("RotateRight", typeof(MainWindow));
        RotateReset = new RoutedCommand("RotateReset", typeof(MainWindow));
    }
}