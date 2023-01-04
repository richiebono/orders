using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class Order : Entity
    {
        
        public string Title { get; set; }        
        public OrderType Type { get; set; }
        public string CustomerName { get; set; }
        public User User { get; set; }
        
    }
}
