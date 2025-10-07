using Microsoft.EntityFrameworkCore;
using TrainMonitor.domain.Entities;
using TrainMonitor.domain.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TrainMonitor.repository.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;
    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> IsEmailTakenAsync(string email)
    {
        var result = _context.Accounts.Any(a => a.Email == email);
        ; return result;
    }

    public async Task<Account> AddAsync(Account account)
    {
        var savedAccount = await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return savedAccount.Entity;

    }

    public async Task<Account> GetAccountByEmailAsync(string email)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        if (account == null)
        {
            throw new AccountNotFoundException();
        }

        return account;
    }

    public async Task<Account> GetAccountByIdAsync(Guid accountId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
    }

}