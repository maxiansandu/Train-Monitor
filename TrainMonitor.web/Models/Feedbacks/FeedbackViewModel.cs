namespace TrainMonitor.web.Models.Feedbacks;

public class FeedbackViewModel
{
    public int TrainNumber { get; set; }
    public string TrainName { get; set; }
    public List<FeedBackViewModelList> FeedBacksList { get; set; }
}