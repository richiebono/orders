using System;
using System.Collections.Generic;
using Bono.Orders.Data.Context;
using Bono.Orders.Domain.Entities;
using Bono.Orders.Domain.Interfaces.Repository;

namespace Bono.Orders.Data.Repositories
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {

        public OrderRepository(BonoOrderContext context)
            : base(context) { }

        public IEnumerable<Order> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
