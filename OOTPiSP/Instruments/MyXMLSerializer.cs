using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Microsoft.Win32;
using SharedComponents;

namespace OOTPiSP.Instruments;

public class MyXMLSerializer
{
    public List<AbstractShapeXML>? Deserialize()
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "XML файлы (*.xml)|*.xml"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<AbstractShapeXML>));
                using FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate);

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
        }

        return null;
    }

    public void Serialize(IEnumerable<AbstractShape> abstractShapes)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            if (!saveFileDialog.FileName.EndsWith(".xml"))
            {
                saveFileDialog.FileName += ".xml";
            }

            using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);

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
}