using System.Collections.Generic;
using Bono.Orders.Domain.Entities;

namespace Bono.Orders.Domain.Interfaces.Repository
{
    public interface IUserRoleRepository: IRepository<UserRole>
    {
        IEnumerable<UserRole> GetAll();
    }
}
