using TrainMonitor.application.Authentication;
using TrainMonitor.domain.Entities;

namespace TrainMonitor.web.Authentication;

public interface ISessionAuthentication : IAuthenticationContext
{
    Task<Account> Login(string email, string password);

    void Logout();
}
