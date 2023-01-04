using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class UserRole: Entity
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
