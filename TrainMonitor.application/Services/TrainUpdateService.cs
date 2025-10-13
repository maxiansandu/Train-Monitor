using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using TrainMonitor.application.Hubs;
using TrainMonitor.application.LoadTrains.DeserializeFromfile;
using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Repositories.Trains;
using TrainMonitor.application.LoadTrains;

namespace TrainMonitor.application.Services;

public class TrainUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<TrainHub> _hubContext;
    private Collection<Train> _latestTrains = new();
    private string _lastFileHash = string.Empty;

    public TrainUpdateService(IServiceProvider serviceProvider, IHubContext<TrainHub> hubContext)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var index = 0;
        Root data = null;
        string fullPath = "/home/nicu/Documents/Trains/TrainMonitor.application/LoadTrains/task-j(1).json";
        var watcher = new FileSystemWatcher("/home/nicu/Documents/Trains/TrainMonitor.application/LoadTrains")
        {
            Filter = "task-j(1).json",
            NotifyFilter = NotifyFilters.LastWrite
        };

        watcher.Changed += async (s, e) =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var trainsRepository = scope.ServiceProvider.GetRequiredService<ITrainsRepositry>();
                var read = scope.ServiceProvider.GetRequiredService<IRead>();
                var data = await read.ReadJson(0);

                _latestTrains = new Collection<Train>(
                    data.Data.Select(train =>
                    {
                        int safeIndex = Math.Min(0, train.ReturnValue.StopObjArray.Count - 1);
                        return new Train
                        {
                            Id = train.ReturnValue.Id,
                            Name = train.Name,
                            TrainNumber = int.Parse(train.ReturnValue.Train),
                            DelayMinutes = train.ReturnValue.ArrivingTime,
                            NextStop = train.ReturnValue.StopObjArray[safeIndex].Title,
                            LastUpdated = DateTime.UtcNow
                        };
                    }).ToList()
                );

                await _hubContext.Clients.All.SendAsync("ReceiveTrainUpdate", _latestTrains);
                await trainsRepository.AddTrain(_latestTrains);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        };

        watcher.EnableRaisingEvents = true;
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var trainsRepository = scope.ServiceProvider.GetRequiredService<ITrainsRepositry>();
            var read = scope.ServiceProvider.GetRequiredService<IRead>();

            if (data == null)
            {
                data = await read.ReadJson(0);

            }

            _latestTrains = new Collection<Train>(
                data.Data.Select(train =>
                {
                    int safeIndex = Math.Min(index, train.ReturnValue.StopObjArray.Count - 1);
                    return new Train
                    {
                        Id = train.ReturnValue.Id,
                        Name = train.Name,
                        TrainNumber = int.Parse(train.ReturnValue.Train),
                        DelayMinutes = train.ReturnValue.ArrivingTime,
                        NextStop = train.ReturnValue.StopObjArray[safeIndex].Title,
                        LastUpdated = DateTime.UtcNow
                    };
                }).ToList()
            );

            await _hubContext.Clients.All.SendAsync("ReceiveTrainUpdate", _latestTrains);
            await trainsRepository.AddTrain(_latestTrains);

            index++;
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}
