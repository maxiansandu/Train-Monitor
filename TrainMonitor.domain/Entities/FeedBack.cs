namespace TrainMonitor.domain.Entities;

public class FeedBack
{
    public Guid Id { get; set; }
    public string Username{get;set;}
    public int TrainNumber{get;set;}
    public string ReasonForDelay{get;set;}
    public string AditionalMessage{get;set;}
    
    public string TrainId{get;set;}
    public Guid UserId{get;set;}
    public virtual Account Account {get;set;}
    public virtual Train Train{get;set;}
    
}