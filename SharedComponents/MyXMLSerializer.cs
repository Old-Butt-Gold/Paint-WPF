using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using OOTPiSP.GeometryFigures.Shared;

namespace SharedComponents;

public class MyXMLSerializer
{
    public List<AbstractShapeXML>? Deserialize(string fileName)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<AbstractShapeXML>));
            using FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);

            if (serializer.Deserialize(fs) is List<AbstractShapeXML> { Count: not 0 } loadedShapes)
            {
                return loadedShapes;
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при открытии файла XML: {ex.Message}");
            return null;
        }

        return null;
    }

    public void Serialize(string fileName, IEnumerable<AbstractShape> abstractShapes)
    {
        using FileStream fs = new FileStream(fileName, FileMode.Create);

        List<AbstractShapeXML> list = new();
        foreach (var item in abstractShapes)
        {
            list.Add(new()
            {
                Angle = item.Angle,
                BackgroundColor = item.BackgroundColor,
                DownRight = item.DownRight,
                PenColor = item.PenColor,
                StrokeThickness = item.StrokeThickness,
                TagShape = item.TagShape,
                TopLeft = item.TopLeft
            });
        }
            
        XmlSerializer serializer = new XmlSerializer(typeof(List<AbstractShapeXML>));
        serializer.Serialize(fs, list);
    }
}