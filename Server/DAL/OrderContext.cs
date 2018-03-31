using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Server.DAL
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("OrderContext") { }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}