namespace OOTPiSP.GeometryFigures.Shared;

public class MyPoint(double x, double y)
{
    public MyPoint() : this(0, 0) { }

    public double X { get; private set; } = x;
    public double Y { get; private set; } = y;
}