using Bono.Orders.Application.ViewModels;
using Bono.Orders.Domain.Validations;

namespace Bono.Orders.Application.Interfaces
{
    public interface IUserService : IService<UserViewModel>
    {
        ValidationResult Authenticate(UserAuthenticateRequestViewModel user);
    }
}
