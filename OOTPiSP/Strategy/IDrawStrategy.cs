using System.Windows.Controls;
using System.Windows.Shapes;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Strategy;

public interface IDrawStrategy
{
    Shape Draw(AbstractShape shape);
}