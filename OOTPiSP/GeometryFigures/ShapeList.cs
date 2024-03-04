using System.Windows.Controls;
using OOTPiSP.GeometryFigures.Shared;
using OOTPiSP.Strategy;

namespace OOTPiSP.GeometryFigures;

public class ShapeList
{
    readonly List<AbstractShape> _shapes = new();

    public IAbstractDrawStrategy DrawStrategy { get; set; }
    
    public ShapeList(IAbstractDrawStrategy iAbstractDrawStrategy)
    {
        DrawStrategy = iAbstractDrawStrategy;
    }

    public void Add(AbstractShape shape) => _shapes.Add(shape);

    public void DrawAll(Canvas canvas) => _shapes.ForEach(shape => DrawStrategy.Draw(shape, canvas));
}