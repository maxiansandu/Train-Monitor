using TrainMonitor.domain.Entities;

namespace TrainMonitor.application.Authentication;

public interface IAuthenticationContext
{
    Task<Guid?> CurrentAccountId();

    Task<Account?> CurrentAccount();
}