using SharedComponents.AbstractClasses;

namespace SharedComponents.Interfaces;

public interface IPluginFunctionality
{
    public string Name { get; }
    public void SaveToFile(List<AbstractShape> abstractShapes);
    public bool LoadFile(List<AbstractShape> abstractShapes, Dictionary<object, AbstractFactory> dictionary);
}