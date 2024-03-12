namespace OOTPiSP.GeometryFigures.Shared;

public class MyPoint
{
    public MyPoint(double x, double y) => (X, Y) = (x, y);
    public MyPoint() : this(0, 0) { }

    public double X { get; private set; }
    public double Y { get; private set; }
}