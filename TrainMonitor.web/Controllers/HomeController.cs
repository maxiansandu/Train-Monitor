using System.Diagnostics;
using EnviroSense.Web.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TrainMonitor.application.Authentication;
using TrainMonitor.application.Services.Feedbacks;
using TrainMonitor.application.Services.Trains;
using TrainMonitor.domain.Entities;
using TrainMonitor.web.Models;
using TrainMonitor.web.Models.Home;

namespace TrainMonitor.web.Controllers;

public class HomeController : Controller
{
    private readonly ITrains _trains;
    private readonly IAuthenticationContext _authenticationContext;
    private readonly IFeedbackService  _feedbackService;
    

    public HomeController(ITrains trains, IAuthenticationContext authenticationContext, IFeedbackService feedbackService)
    {
        _trains = trains;
        _authenticationContext = authenticationContext;
        _feedbackService = feedbackService;

    }

    [TypeFilter(typeof(SignedInFilter))]
    public async Task<IActionResult> Index()
    {
        var trains = await _trains.GetAllTrains();
        var allTrains = new List<TrainsListViewModel>();
        foreach (var train in trains)
        {
            allTrains.Add(new TrainsListViewModel
            {
                Id = train.Id,
                Name = train.Name,
                TrainNumber = train.TrainNumber,
                DelayMinutes = train.DelayMinutes,
                NextStop = train.NextStop,
                LastUpdated = train.LastUpdated

            });

        }

        var model = new HomePageViewModel
        {
            TrainsList = allTrains,
        };
    return View(model);
    }
    [TypeFilter(typeof(SignedInFilter))]
    [HttpPost]
    public async Task<IActionResult> Feedback(HomePageViewModel model)
    {
       
        var train = await _trains.GetTrainByNumber(model.TrainNumber);
        var account = await _authenticationContext.CurrentAccount();
        
        var feedback = new FeedBack
        {
            Username = model.Username,
            ReasonForDelay = model.ReasonForDelay,
            AditionalMessage = model.AditionalMessage,
            TrainNumber = model.TrainNumber,
            Train = train,
            Account = account
        };
        
        var createdFeedback = await _feedbackService.Add(feedback);
        
        return RedirectToAction(nameof(Index));
    }
    
    [TypeFilter(typeof(SignedInFilter))]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
