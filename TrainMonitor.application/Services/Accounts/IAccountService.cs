using System.Threading.Tasks;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Services.Accounts;

public interface IAccountService
{
    Task<bool> IsEmailTacken(string email);
    Task<Account> Add(string email, string password);

    Task<Account> GetAccountByEmail(string email);

    Task<Account> GetAccountById(Guid accountId);

}