

using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Repositories.Feedbacks;

namespace TrainMonitor.application.Services.Feedbacks;

public class FeedbackService: IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }
    public async Task<FeedBack> Add(FeedBack feedBack)
    {
        return await _feedbackRepository.AddAsync(feedBack);
    }
}