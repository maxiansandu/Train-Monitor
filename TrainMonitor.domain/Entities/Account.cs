namespace TrainMonitor.domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public ICollection<FeedBack> FeedBacks { get; set; }
}