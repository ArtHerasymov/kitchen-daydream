﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Server.Models;

namespace Server.DAL
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("OrderContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Discount> Discounts { get; set; }


    }
}