using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Data.Extensions;
using Bono.Orders.Data.Mappings;
using Bono.Orders.Domain.Entities;
using Bono.Orders.Infrastructure.Utils;

namespace Bono.Orders.Data.Context
{
    public class BonoOrderContext: DbContext
    {
        private readonly Settings settings;
        private readonly Security security;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderType> OrderType { get; set; }

        public BonoOrderContext(DbContextOptions<BonoOrderContext> option, Settings settings, Security security) : base(option) 
        {
            this.settings = settings;
            this.security = security;

            if (settings.IsDevelopment)
            {
                Database.Migrate();
            }
            

        }

        #region "DBSets"

        

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyGlobalConfigurations();
            modelBuilder.SeedData(security);
            base.OnModelCreating(modelBuilder);
        }
    }
}
