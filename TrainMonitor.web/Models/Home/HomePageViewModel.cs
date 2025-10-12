namespace TrainMonitor.web.Models.Home;

public class HomePageViewModel
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public string TrainNumber { get; set; } = "";
    public int? DelayMinutes { get; set; }
    public string NextStop { get; set; } = "";
    public DateTime LastUpdated { get; set; }
}