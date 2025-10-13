using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Services.Trains;

public interface ITrains
{
    Task<List<Train>> GetAllTrains();

    Task<Train> GetTrainByNumber(int trainNumber);

    Task SetFeedback(Train train);
}