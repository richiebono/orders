using System;
using System.Collections.Generic;
using Bono.Orders.Data.Context;
using Bono.Orders.Domain.Entities;
using Bono.Orders.Domain.Interfaces.Repository;

namespace Bono.Orders.Data.Repositories
{
    public class RoleRepository: Repository<Role>, IRoleRepository
    {

        public RoleRepository(BonoOrderContext context)
            : base(context) { }

        public IEnumerable<Role> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
