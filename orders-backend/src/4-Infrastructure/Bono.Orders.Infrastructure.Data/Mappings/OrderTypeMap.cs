using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Entities;

namespace Bono.Orders.Data.Mappings
{
    public class OrderTypeMap: IEntityTypeConfiguration<OrderType>
    {
        public void Configure(EntityTypeBuilder<OrderType> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Type).IsRequired();            
        }
    }
}
