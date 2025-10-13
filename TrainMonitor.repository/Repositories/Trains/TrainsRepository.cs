using Microsoft.EntityFrameworkCore;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Repositories.Trains;

public class TrainsRepository: ITrainsRepositry
{
    private readonly ApplicationDbContext _context;
    public TrainsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddTrain(IEnumerable<Train> trains)
    {
        foreach (var train in trains)
        {
            var existing = await _context.Trains.FirstOrDefaultAsync(t => t.Id == train.Id);
            if (existing != null)
            {
                existing.DelayMinutes = train.DelayMinutes;
                existing.NextStop = train.NextStop;
                existing.LastUpdated = DateTime.UtcNow;
            }
            else
            {
                _context.Trains.AddAsync(train);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Train>> GetAllTrainsAsync()
    {
        var trains = await _context.Trains.ToListAsync();
        return trains;
    }

    public async Task<Train> GetTrainByNumberAsync(int trainNumber)
    {
        return await _context.Trains.FirstOrDefaultAsync(t => t.TrainNumber == trainNumber);
    }

    public async Task SetFeedbackAsync(Train train)
    {
        train.HasFeedback = true;
        _context.Trains.Update(train);
        await _context.SaveChangesAsync();
    }
}