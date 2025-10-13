using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Repositories.Trains;

public interface ITrainsRepositry
{
    Task AddTrain(IEnumerable<Train> train);

    Task<List<Train>> GetAllTrainsAsync();

    Task<Train> GetTrainByNumberAsync(int trainNumber);

    Task SetFeedbackAsync(Train train);
}