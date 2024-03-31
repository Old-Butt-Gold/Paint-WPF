using System.IO;
using System.Text;
using System.Windows;
using Newtonsoft.Json;

namespace SharedComponents;

public class MyJsonSerializer
{
    public void Serialize(string fileName, IEnumerable<AbstractShape> abstractShapes)
    {
        using FileStream fs = new FileStream(fileName, FileMode.Create);
            
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Objects
        };
        string json = JsonConvert.SerializeObject(abstractShapes, settings);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        fs.Write(bytes, 0, bytes.Length);
    }

    public List<AbstractShape>? Deserialize(string fileName)
    {
        try
        {
            string json = File.ReadAllText(fileName);

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
            };
            List<AbstractShape>? loadedShapes = JsonConvert.DeserializeObject<List<AbstractShape>>(json, settings);
            return loadedShapes;

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при открытии файла JSON: {ex.Message}");
            return null;
        }
    }
}