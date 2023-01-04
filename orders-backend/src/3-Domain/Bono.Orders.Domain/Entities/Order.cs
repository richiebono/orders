using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class Order : Entity
    {
        
        public string Title { get; private set; }        
        public OrderType Type { get; private set; }
        public string CustomerName { get; private set; }
        public User User { get; private set; }
        
    }
}
