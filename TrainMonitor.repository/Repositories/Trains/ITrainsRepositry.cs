using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Repositories.Trains;

public interface ITrainsRepositry
{
    Task AddTrain(IEnumerable<Train> train);
    
    Task <List<Train>> GetAllTrainsAsync();
}