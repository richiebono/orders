using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class Role: Entity
    {
        public string Name { get; private set; }        
    }
}
