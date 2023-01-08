using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bono.Orders.Data.Context;
using Bono.Orders.Domain.Entities;
using Bono.Orders.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bono.Orders.Data.Repositories
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {

        public OrderRepository(BonoOrderContext context) : base(context) 
        {
            context.Order.Include(x => x.Type).Include(x=> x.User);
        }

        public IEnumerable<Order> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

        public IQueryable<Order> Query(Expression<Func<Order, bool>> where)
        {
            return _context.Order.Include(x => x.User).Include(x => x.Type).Where(where);
        }

        public Order Find(Expression<Func<Order, bool>> where)
        {
            return _context.Order.Include(x => x.User).Include(x => x.Type).Where(where).FirstOrDefault();
        }
    }
}
