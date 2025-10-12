using System.Diagnostics;
using EnviroSense.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using TrainMonitor.application.Services.Trains;
using TrainMonitor.web.Models;
using TrainMonitor.web.Models.Home;

namespace TrainMonitor.web.Controllers;

public class HomeController : Controller
{
    private readonly ITrains _trains;

    public HomeController(ITrains trains)
    {
        _trains = trains;
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
    public IActionResult Feedback(HomePageViewModel model)
    {
        var message  = model.AditionalMessage;
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
