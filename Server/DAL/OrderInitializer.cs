using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Server.Models;

namespace Server.DAL
{
    public class OrderInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OrderContext>
    {
        protected override void Seed(OrderContext context)
        {
            var orders = new List<Order>
            {
                new Order {Id="123,43",Waiter="Heath Ledger", Payment=12554.3, Items="23,1234,3534".Split(',').ToList<String>()},
                new Order {Id="123,43",Waiter="Naomi Watts", Payment=12554.3, Items="23,1234,3534".Split(',').ToList<String>()},
                new Order {Id="123,43",Waiter="Benedict Cumberbatch", Payment=12554.3, Items="23,1234,3534".Split(',').ToList<String>()},
            };

            orders.ForEach(s => context.Orders.Add(s));
            context.SaveChanges();
        }
    }
}