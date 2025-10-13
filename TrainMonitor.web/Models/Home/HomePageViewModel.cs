namespace TrainMonitor.web.Models.Home;

public class HomePageViewModel
{
    public string Username{get;set;}
    public int TrainNumber{get;set;}
    public string ReasonForDelay{get;set;}
    public string AditionalMessage{get;set;}
    public List<TrainsListViewModel> TrainsList { get; set; }
}