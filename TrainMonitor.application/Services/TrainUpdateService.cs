using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using TrainMonitor.application.Hubs;
using TrainMonitor.application.LoadTrains.DeserializeFromfile;
using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Repositories.Trains;
using System.Security.Cryptography;
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

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var trainsRepository = scope.ServiceProvider.GetRequiredService<ITrainsRepositry>();
            var read = scope.ServiceProvider.GetRequiredService<IRead>();

            if (data == null)
            {
                data = await read.ReadJson(0);
                _lastFileHash = ComputeFileHash(fullPath); 
            }

            string currentHash = ComputeFileHash(fullPath);

            if (currentHash != _lastFileHash)
            {
                data = await read.ReadJson(0);
                _lastFileHash = currentHash; 
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
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
        }
    }

    private static string ComputeFileHash(string filePath)
    {
        if (!File.Exists(filePath))
            return string.Empty;

        using var md5 = MD5.Create();
        using var stream = File.OpenRead(filePath);
        byte[] hashBytes = md5.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}
