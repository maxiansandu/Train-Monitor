using TrainMonitor.application.LoadTrains;
using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Repositories.Trains;

namespace TrainMonitor.application.Services.Trains;

public class Trains:LoadAllAllTrainsFromJson ,ITrains
{
    private readonly ITrainsRepositry _trainsRepositry;

    public Trains(ITrainsRepositry trainsRepositry)
    {
        _trainsRepositry = trainsRepositry;
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