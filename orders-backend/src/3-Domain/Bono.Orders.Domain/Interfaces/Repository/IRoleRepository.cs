using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Entities;

namespace Bono.Orders.Domain.Interfaces.Repository
{
    public interface IRoleRepository: IRepository<Role>
    {
        IEnumerable<Role> GetAll();
    }
}
