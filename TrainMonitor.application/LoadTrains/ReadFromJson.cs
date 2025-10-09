using System.Text.Json;

namespace TrainMonitor.application.LoadTrains;

public class ReadFromJson
{
    public static async Task<IEnumerable<JsonElement>>GetAllBlocks(string path)
    {
        using FileStream fs = File.OpenRead(path);
        using JsonDocument doc = await JsonDocument.ParseAsync(fs);

        var root = doc.RootElement;
        if (!root.TryGetProperty("data", out var dataArray))
            throw new Exception("Data proprty not found");

        var arrayLength = dataArray.GetArrayLength();

        var elementCopy = JsonDocument.Parse(
            JsonSerializer.Serialize(dataArray)
        ).RootElement.Clone();

        return elementCopy.EnumerateArray().ToList();
    }
}