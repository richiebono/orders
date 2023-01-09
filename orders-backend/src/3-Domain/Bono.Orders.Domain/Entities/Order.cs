using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class Order : Entity
    {        
        public OrderType Type { get; set; }
        public string CustomerName { get; set; }
        public User User { get; set; }
        
        public Order()
        {            
        }

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
