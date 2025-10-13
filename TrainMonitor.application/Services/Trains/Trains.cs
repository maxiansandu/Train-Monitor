using TrainMonitor.application.LoadTrains;
using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Repositories.Trains;
using TrainMonitor.application.LoadTrains.DeserializeFromfile;


namespace TrainMonitor.application.Services.Trains;

public class Trains : ITrains
{
    private readonly ITrainsRepositry _trainsRepositry;
    private readonly IRead _read;

    public Trains(ITrainsRepositry trainsRepositry, IRead read)
    {
        _trainsRepositry = trainsRepositry;
        _read = read;
    }

    public async Task<List<Train>> GetAllTrains()
    {
        return await _trainsRepositry.GetAllTrainsAsync();
    }

    public async Task<Train> GetTrainByNumber(int trainNumber)
    {
        return await _trainsRepositry.GetTrainByNumberAsync(trainNumber);
    }

    public async Task SetFeedback(Train train)
    {
        await _trainsRepositry.SetFeedbackAsync(train);
    }
}