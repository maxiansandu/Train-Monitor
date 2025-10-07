using Microsoft.AspNetCore.Http;
using TrainMonitor.domain.Entities;
using TrainMonitor.domain.Exceptions;
using TrainMonitor.repository.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;


namespace TrainMonitor.application.Services.Accounts;
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;



    public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<bool> IsEmailTacken(string email)
    {
        return await _accountRepository.IsEmailTakenAsync(email);
    }

    public async Task<Account> Add(string email, string password)
    {

        string hashedPassword = BCryptNet.HashPassword(password, 10);
        var account = new Account()
        {
            Email = email,
            Password = hashedPassword,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };
        var accountAdded = await _accountRepository.AddAsync(account);

        return accountAdded;
    }

    public async Task<Account> GetAccountByEmail(string email)
    {
        return await _accountRepository.GetAccountByEmailAsync(email);
    }

    public Task<Account> GetAccountById(Guid accountId)
    {
        throw new NotImplementedException();
    }

}