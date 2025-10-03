using Microsoft.AspNetCore.Mvc;
using TrainMonitor.application.Services.Accounts;
using TrainMonitor.web.Models.Accounts;
using TrainMonitor.domain.Exceptions;

namespace TrainMonitor.web.Controllers;

public class Accounts : Controller
{
    private readonly IAccountService _accountService;
    public Accounts(IAccountService  accountService)
    {
        _accountService = accountService;
    }
    
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
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

        var accountCreated =await _accountService.Add(model.Email, model.Password);
        return RedirectToAction("SignIn");
    }

    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]

    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var account = await _accountService.Login(model.Password, model.Email);
            
            return RedirectToAction("Index", "Home");

        }
        catch (AccountNotFoundException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
        
    }
}