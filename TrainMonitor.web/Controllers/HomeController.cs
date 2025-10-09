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
        var viewModelList = trains.Select(t => new HomePageViewModel
        {
            Name = t.Name,
            TrainNumber = t.TrainNumber,
            DelayMinutes = t.DelayMinutes,
            NextStop = t.NextStop,
            LastUpdated = t.LastUpdated

        }).ToList();
        return View(viewModelList);
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
