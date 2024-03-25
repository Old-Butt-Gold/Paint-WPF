using System.Windows;

namespace OOTPiSP;

public partial class App
{
    public App()
    {
        SplashScreen splashScreen = new("Assets/bg.jpg");
        splashScreen.Show(true, false);
    }
}