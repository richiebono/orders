using Bono.Orders.Domain.Entities;
using System.Collections.Generic;

namespace Bono.Orders.Domain.Interfaces.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAll();
    }
}
