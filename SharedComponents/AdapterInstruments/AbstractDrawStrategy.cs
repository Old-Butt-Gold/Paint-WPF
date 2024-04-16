using System.Windows.Controls;
using System.Windows.Shapes;

namespace SharedComponents.AdapterInstruments;

public interface AbstractDrawStrategy
{
    Shape Draw(MySprite sprite, Canvas canvas);
}