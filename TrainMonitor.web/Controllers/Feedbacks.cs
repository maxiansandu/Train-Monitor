using EnviroSense.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using TrainMonitor.application.Services.Feedbacks;
using TrainMonitor.web.Models.Feedbacks;

namespace TrainMonitor.web.Controllers;

public class Feedbacks : Controller
{
    private readonly IFeedbackService _feedbackService;
    public Feedbacks(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }
    [TypeFilter(typeof(SignedInFilter))]
    [HttpGet]
    public async Task<IActionResult> ViewFeedbacks(int trainNumber)
    {
        var feedbacks = await _feedbackService.GetAllFeedbacksForTrain(trainNumber);
        var feedbackList = new List<FeedBackViewModelList>();
        foreach (var feedback in feedbacks)
        {
            feedbackList.Add(new FeedBackViewModelList
            {
                Username = feedback.Username,
                ReasonForDelay = feedback.ReasonForDelay,
                AditionalMessage = feedback.AditionalMessage,
            }
            );
        }
        var firstFeedback = feedbacks.First();
        var model = new FeedbackViewModel
        {
            TrainNumber = firstFeedback.TrainNumber,
            TrainName = firstFeedback.Train.Name,
            FeedBacksList = feedbackList
        };

        return View(model);
    }
}