using Microsoft.EntityFrameworkCore;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Repositories.Feedbacks;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly ApplicationDbContext _context;
    public FeedbackRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<FeedBack> AddAsync(FeedBack feedBack)
    {
        await _context.FeedBacks.AddAsync(feedBack);
        await _context.SaveChangesAsync();
        return feedBack;
    }

    public async Task<List<FeedBack>> GetAllFeedbacksForTrainAsync(int trainNumber)
    {
        var feddbacksList = await _context.FeedBacks.Where(f => f.TrainNumber == trainNumber).ToListAsync();
        return feddbacksList;
    }
}