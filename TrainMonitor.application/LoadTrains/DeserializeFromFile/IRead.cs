using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.LoadTrains.DeserializeFromfile;

public interface IRead
{
    Task<Root> ReadJson(int index);
}