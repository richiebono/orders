using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class Order : Entity
    {
        
        public OrderType Type { get; private set; }
        public string CustomerName { get; private set; }
        public User User { get; private set; }
        
        public Order(OrderType type, string customerName, User user)
        {
            Id = Guid.NewGuid();
            Type = type;
            CustomerName = customerName;
            User = user;
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            IsDeleted = false;
        }
        
    }
}
