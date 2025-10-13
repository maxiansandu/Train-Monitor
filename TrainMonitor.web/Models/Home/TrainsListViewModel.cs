namespace TrainMonitor.web.Models.Home;

public class TrainsListViewModel
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public int TrainNumber { get; set; }
    public int? DelayMinutes { get; set; }
    public string NextStop { get; set; } = "";
    public DateTime LastUpdated { get; set; }

    public bool HasFeedback { get; set; }
}