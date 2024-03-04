using System.Windows.Controls;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.GeometryFigures;

public class ShapeList
{
    readonly List<AbstractShape> _shapes = new();

    public void Add(AbstractShape shape) => _shapes.Add(shape);

    public void DrawAll(Canvas canvas) => _shapes.ForEach(shape => shape.Draw(canvas));
}