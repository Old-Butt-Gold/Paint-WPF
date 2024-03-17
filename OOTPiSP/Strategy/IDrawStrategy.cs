using System.Windows.Controls;
using OOTPiSP.GeometryFigures.Shared;

namespace OOTPiSP.Strategy;

public interface IDrawStrategy
{
    void Draw(AbstractShape shape, Canvas canvas);
}