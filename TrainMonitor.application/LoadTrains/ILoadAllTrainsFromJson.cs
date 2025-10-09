using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.LoadTrains;

public interface ILoadAllTrainsFromJson
{
    Task<IEnumerable<Train>> GetTrains(int index);
}