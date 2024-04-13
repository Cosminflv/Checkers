using System.IO;
using System.Text.Json;

public class JsonHandler
{
    public void SaveToJson<T>(string filePath, T data)
    {
        string jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, jsonData);
    }

    public T LoadFromJson<T>(string filePath)
    {
        string jsonData = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(jsonData);
    }
}
