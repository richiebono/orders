using System;
using System.Collections.Generic;
using Bono.Orders.Data.Context;
using Bono.Orders.Domain.Entities;
using Bono.Orders.Domain.Interfaces.Repository;

namespace Bono.Orders.Data.Repositories
{
    public class UserRoleRepository: Repository<UserRole>, IUserRoleRepository
    {

        public UserRoleRepository(BonoOrderContext context)
            : base(context) { }

        public IEnumerable<UserRole> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
