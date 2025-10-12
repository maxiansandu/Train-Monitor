using System.Text.Json;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.LoadTrains.DeserializeFromfile;

public class Read:Root, IRead
{
    public async Task<List<Train>> ReadJson(int index)
    {
        string fileName = "/home/nicu/Documents/Trains/TrainMonitor.application/LoadTrains/task-j(1).json";
        string jsonString = File.ReadAllText(fileName);
        Root data = JsonSerializer.Deserialize<Root>(jsonString)!;
        var trainsList = new List<Train>();
        var totalTrains = data.Data.ToList();
        foreach (var train in totalTrains)
        {


            string id = train.ReturnValue.Id;
            string name = train.Name;
            string trainNumber = train.ReturnValue.Train;
            int? delay = train.ReturnValue.ArrivingTime;
            string? nextStop = null;
           
            int safeIndex;
            if (index >= train.ReturnValue.StopObjArray.Count)
            {
                safeIndex = train.ReturnValue.StopObjArray.Count - 1;
            }
            else
            {
                safeIndex = index;
            }

            nextStop = train.ReturnValue.StopObjArray[safeIndex].Title;
            trainsList.Add(new Train
            {
                Id = id,
                Name = name,
                TrainNumber = trainNumber,
                DelayMinutes = delay,
                NextStop = nextStop,
                LastUpdated = DateTime.UtcNow
            });
        }
        return trainsList;
    }
}
