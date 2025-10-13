using System.Text.Json;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.LoadTrains.DeserializeFromfile;

public class Read : Root, IRead
{
    public async Task<Root> ReadJson(int index)
    {
        string fileName = "/home/nicu/Documents/Trains/TrainMonitor.application/LoadTrains/task-j(1).json";
        string jsonString = File.ReadAllText(fileName);
        Root data = JsonSerializer.Deserialize<Root>(jsonString)!;
        return data;
    }
}
