using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Repositories;

public interface IAccountRepository
{
    Task<bool> IsEmailTakenAsync(string email);
    Task<Account> AddAsync(Account account);
    Task<Account> GetAccountByEmailAsync(string email);
    Task<Account> GetAccountByIdAsync(Guid accountId);
}