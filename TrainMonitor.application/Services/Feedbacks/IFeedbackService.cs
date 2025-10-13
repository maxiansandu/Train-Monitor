using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Services.Feedbacks;

public interface IFeedbackService
{
    Task<FeedBack> Add(FeedBack feedBack);

    Task<List<FeedBack>> GetAllFeedbacksForTrain(int trainNumber);
}