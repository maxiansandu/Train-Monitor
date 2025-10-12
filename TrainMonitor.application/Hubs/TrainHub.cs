using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Hubs;
using Microsoft.AspNetCore.SignalR;

public class TrainHub : Hub
{
    // metoda poate fi folosită dacă vrei să trimiți manual mesaje din controller
    public async Task SendTrainUpdate(Train train)
    {
        await Clients.All.SendAsync("ReceiveTrainUpdate", train);
    }
}
