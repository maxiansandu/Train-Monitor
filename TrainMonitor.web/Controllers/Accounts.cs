using EnviroSense.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using TrainMonitor.application.Services.Accounts;
using TrainMonitor.web.Models.Accounts;
using TrainMonitor.domain.Exceptions;
using TrainMonitor.web.Authentication;

namespace TrainMonitor.web.Controllers;

public class Accounts : Controller
{
    private readonly IAccountService _accountService;
    private readonly ISessionAuthentication _sessionAuthentication;
    public Accounts(IAccountService accountService, ISessionAuthentication sessionAuthentication)
    {
        _accountService = accountService;
        _sessionAuthentication = sessionAuthentication;
    }
    [TypeFilter(typeof(SignedOutFilter))]
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    [TypeFilter(typeof(SignedOutFilter))]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var isEmailTaken = await _accountService.IsEmailTacken(model.Email);
        if (isEmailTaken)
        {
            ModelState.AddModelError("", "An user with this email already exists");
            return View();
        }

        var accountCreated = await _accountService.Add(model.Email, model.Password);
        return RedirectToAction("SignIn");
    }
    [TypeFilter(typeof(SignedOutFilter))]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    [TypeFilter(typeof(SignedOutFilter))]
    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var account = await _sessionAuthentication.Login(model.Email, model.Password);

            return RedirectToAction("Index", "Home");

        }
        catch (AccountNotFoundException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }


    }
    [TypeFilter(typeof(SignedInFilter))]
    public ActionResult LogOut()
    {
        _sessionAuthentication.Logout();
        return RedirectToAction("SignIn");
    }
}