using System;
using System.Collections.Generic;
using Bono.Orders.Data.Context;
using Bono.Orders.Domain.Entities;
using Bono.Orders.Domain.Interfaces.Repository;

namespace Bono.Orders.Data.Repositories
{
    public class OrderTypeRepository: Repository<OrderType>, IOrderTypeRepository
    {

        public OrderTypeRepository(BonoOrderContext context)
            : base(context) { }

        public IEnumerable<OrderType> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
