namespace TrainMonitor.domain.Entities;

public class Train
{
    public required string Id { get; set; }
    public string Name { get; set; } = null!;
    public int TrainNumber { get; set; }
    public int? DelayMinutes { get; set; }
    public string NextStop { get; set; } = "";
    public DateTime LastUpdated { get; set; }

    public bool HasFeedback = false;
    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
}

