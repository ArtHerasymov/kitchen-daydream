using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;

namespace Server.DAL
{
    public class OrderInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OrderContext>
    {
        protected override void Seed(OrderContext context)
        {
             var orders = new List<Order>
             {
                 new Order{Waiter = "John" , Status = "READY" },
                 new Order{Waiter = "Tina" , Status = "IN_PROGRESS"}
             };

             var items = new List<Item>
             {
                 new Item{Title = "Chineese Soup"},
                 new Item{Title = "Chineese Appetizer"},
                 new Item{Title = "Chineese Dessert" },

                 new Item{Title ="Italian Soup"},
                 new Item{Title="Italian Appetizer" },
                 new Item{Title="Italian Dessert"}
             };
             var tickets = new List<Ticket>
             {
                 new Ticket{Status = "READY"},
                 new Ticket{Status = "CANCELLED"}
             };

            foreach (Ticket s in tickets)
                context.Tickets.Add(s);
             context.SaveChanges();

             orders.ForEach(s => context.Orders.Add(s));
             context.SaveChanges();


             items.ForEach(s => context.Items.Add(s));
             context.SaveChanges();
  
        }
    }
}