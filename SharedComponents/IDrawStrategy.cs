using System.Windows.Shapes;

namespace SharedComponents;

public interface IDrawStrategy
{
    Shape Draw(AbstractShape shape);
}