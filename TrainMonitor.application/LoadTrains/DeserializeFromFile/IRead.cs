using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.LoadTrains.DeserializeFromfile;

public interface IRead
{
    Task<List<Train>> ReadJson(int index);
}