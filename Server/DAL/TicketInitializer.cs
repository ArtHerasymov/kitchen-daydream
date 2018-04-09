using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;

namespace Server.DAL
{
    public class TicketInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<TicketContext>
    {
        protected override void Seed(TicketContext context)
        {
            var orders = new List<Order>
            {
                new Order{Waiter = "John" , Status = "READY", TicketID=1},
                new Order{Waiter = "Tina" , Status = "IN_PROGRESS", TicketID=2}
            };

            var items = new List<Item>
            {
                new Item{Title = "Soup", TicketID =1},
                new Item{Title = "Steak", TicketID =2}
            };

            var tickets = new List<Ticket>
            {
                new Ticket{Status = "READY",Orders = orders, Items = items},
                new Ticket{Status = "CANCELLED", Orders = orders, Items = items}
            };

            tickets.ForEach(s => context.Tickets.Add(s));
            context.SaveChanges();

         

            orders.ForEach(s => context.Orders.Add(s));
            context.SaveChanges();


            items.ForEach(s => context.Items.Add(s));
            context.SaveChanges();

        }
    }
}