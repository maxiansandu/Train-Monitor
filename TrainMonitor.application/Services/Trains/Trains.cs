using TrainMonitor.application.LoadTrains;
using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Repositories.Trains;
using TrainMonitor.application.LoadTrains.DeserializeFromfile;


namespace TrainMonitor.application.Services.Trains;

public class Trains:LoadAllAllTrainsFromJson,ITrains
{
    private readonly ITrainsRepositry _trainsRepositry;
    private readonly IRead _read;

    public Trains(ITrainsRepositry trainsRepositry,  IRead read)
    {
        _trainsRepositry = trainsRepositry;
        _read = read;
    }
    public async Task AddTrain()
    {
        int index = 1;
        var trainsToAdd = await GetTrains(index);
        await _trainsRepositry.AddTrain(trainsToAdd);
    }
    
    public async Task<List<Train>> GetAllTrains()
    {
        return  await _trainsRepositry.GetAllTrainsAsync();
    }
}