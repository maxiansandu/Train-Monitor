using Microsoft.AspNetCore.Http;
using TrainMonitor.application.Services.Accounts;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Authentication;

public class AuthenticationContext : IAuthenticationContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAccountService _accountService;

    public AuthenticationContext(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
    {
        _httpContextAccessor = httpContextAccessor;
        _accountService = accountService;
    }
    public async Task<Guid?> CurrentAccountId()
    {
        var accountId = _httpContextAccessor.HttpContext.Session.GetString("authenticated_account_id");
        if (string.IsNullOrEmpty(accountId))
            return null;

        return Guid.Parse(accountId);
    }


    public async Task<Account?> CurrentAccount()
    {
        throw new NotImplementedException();
    }
}