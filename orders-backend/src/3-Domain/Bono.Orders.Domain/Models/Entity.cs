using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Orders.Domain.Models
{
    public class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime? DateUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public void SetDeleted(bool IsDeleted)
        {
            this.IsDeleted = IsDeleted;
        }
    }
}
