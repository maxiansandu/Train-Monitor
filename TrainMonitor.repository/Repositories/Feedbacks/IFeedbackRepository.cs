using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Repositories.Feedbacks;

public interface IFeedbackRepository
{
    Task<FeedBack> AddAsync(FeedBack feedBack);

    Task<List<FeedBack>> GetAllFeedbacksForTrainAsync(int trainNumber);
}