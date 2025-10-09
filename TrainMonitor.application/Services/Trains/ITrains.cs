using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Services.Trains;

public interface ITrains
{
   Task AddTrain();
   Task<List<Train>> GetAllTrains(); 
}