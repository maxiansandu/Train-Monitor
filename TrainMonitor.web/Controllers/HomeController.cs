using System.Diagnostics;
using EnviroSense.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using TrainMonitor.web.Models;

namespace TrainMonitor.web.Controllers;

public class HomeController : Controller
{
    [TypeFilter(typeof(SignedInFilter))]
    public IActionResult Index()
    {
        return View();
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
