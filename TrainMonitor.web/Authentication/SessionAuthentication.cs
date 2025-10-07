using EnviroSense.Domain.Exceptions;
using TrainMonitor.application.Services.Accounts;
using TrainMonitor.domain.Entities;
using TrainMonitor.domain.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TrainMonitor.web.Authentication;

public class SessionAuthentication : ISessionAuthentication
{
    private readonly IAccountService _accountService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionAuthentication(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
    {
        _accountService = accountService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Account> Login(string email, string password)
    {
        var account = await _accountService.GetAccountByEmail(email);
        var isPasswordValid = BCryptNet.Verify(password, account.Password);

        if (isPasswordValid)
        {
            _httpContextAccessor.HttpContext?.Session.SetString("authenticated_account_id", account.Id.ToString());
            return account;
        }
        else
        {
            throw new AccountNotFoundException();
        }
    }

    public void Logout()
    {
        _httpContextAccessor.HttpContext?.Session.Clear();
    }

    public async Task<Guid?> CurrentAccountId()
    {
        var account = await CurrentAccount();

        return account?.Id;
    }

    public async Task<Account?> CurrentAccount()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null || httpContext.Session == null)
        {
            throw new SessionIsNotAvailableException();
        }

        var session = httpContext.Session;
        var accountId = session.GetString("authenticated_account_id");
        if (string.IsNullOrEmpty(accountId))
        {
            return null;
        }

        if (!Guid.TryParse(accountId, out var accountGuid))
        {
            throw new Exception("Unexpected format for account id. Must be guid.");
        }

        try
        {
            return await _accountService.GetAccountById(accountGuid);
        }
        catch (AccountNotFoundException)
        {
            return null;
        }
    }
}

