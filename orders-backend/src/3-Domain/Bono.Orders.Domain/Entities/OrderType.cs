using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class OrderType : Entity
    {
        public OrderType(string type)
        {
            this.Id = Guid.NewGuid();
            this.Type = type;
            this.DateCreated = DateTime.Now;
            this.DateUpdated = DateTime.Now;
            this.IsDeleted = false;            
        }
        
        public string Type { get; set; }
    }
}
