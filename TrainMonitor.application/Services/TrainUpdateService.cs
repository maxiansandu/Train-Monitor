using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using TrainMonitor.domain.Entities;
using TrainMonitor.repository.Repositories.Trains;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using TrainMonitor.application.Hubs;
using TrainMonitor.application.LoadTrains.DeserializeFromfile;

namespace TrainMonitor.application.Services;
public class TrainUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<TrainHub> _hubContext;
    private readonly IRead _read;
    private readonly ITrainsRepositry _trainsRepositry;

    public TrainUpdateService(IServiceProvider serviceProvider, IHubContext<TrainHub> hubContext)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var index = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var read = scope.ServiceProvider.GetRequiredService<IRead>();
                var trainsRepository = scope.ServiceProvider.GetRequiredService<ITrainsRepositry>();

                var data = await read.ReadJson(index);
                index++;
                var trainsCollection = new Collection<Train>(data);

                await trainsRepository.AddTrain(trainsCollection);
                
                await _hubContext.Clients.All.SendAsync("ReceiveTrainUpdate", trainsCollection);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
