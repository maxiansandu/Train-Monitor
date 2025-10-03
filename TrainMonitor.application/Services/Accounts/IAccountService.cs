using System.Threading.Tasks;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Services.Accounts;

public interface IAccountService
{
   Task<bool> IsEmailTacken(string email);
   Task<Account> Add(string email, string password);
   
   Task<Account>Login(string password, string email);

}