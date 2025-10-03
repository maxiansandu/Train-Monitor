using TrainMonitor.domain.Entities;

namespace TrainMonitor.repository.Repositories;

public interface IAccountRepository
{
    Task<bool> IsEmailTakenAsync(string email);
    Task<Account> AddAsync(Account account);
    Task<Account> LogInAsync(string hashedPassword, string email);
}