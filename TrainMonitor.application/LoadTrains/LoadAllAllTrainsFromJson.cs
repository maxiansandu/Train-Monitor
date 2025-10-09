using System.Text.Json;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.LoadTrains;

public class LoadAllAllTrainsFromJson:ReadFromJson ,ILoadAllTrainsFromJson
{
    public string Path { get; set; }

    public async Task<IEnumerable<Train>> GetTrains(int index)
    {
        this.Path = "/home/nicu/Documents/Trains/TrainMonitor.application/LoadTrains/task-j(1).json";
        var data = await GetAllBlocks(Path);
        var trainsList = new List<Train>();
        string? nextStop = null;
        foreach (var train in data)
        {
            
            var returnValue = train.GetProperty("returnValue");
            string id = returnValue.GetProperty("id").GetString() ?? "";
            string name = train.GetProperty("name").GetString() ?? "Unknown";
            string trainNum = returnValue.GetProperty("train").GetString() ?? "";
            
           int delay = returnValue.TryGetProperty("arrivingTime", out var at) && at.ValueKind == JsonValueKind.Number ? at.GetInt32() : 0;
            if (returnValue.TryGetProperty("stopObjArray", out var ns) 
                && ns.ValueKind == JsonValueKind.Array 
                && ns.GetArrayLength() > 0)
            {
                var firstStop = ns[index]; // primul element din array
                if (firstStop.TryGetProperty("title", out var title))
                {
                    nextStop = title.GetString() ?? "";
                }
            }
            
            trainsList.Add(new Train
            {
                Id = id,
                Name = name,
                TrainNumber = trainNum,
                DelayMinutes = delay,
                NextStop = nextStop,
                LastUpdated = DateTime.UtcNow
            });
        }
        return trainsList;

    }
    

}