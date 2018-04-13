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
            /* var orders = new List<Order>
             {
                 new Order{Waiter = "John" , Status = "READY" , TicketID=1},
                 new Order{Waiter = "Tina" , Status = "IN_PROGRESS", TicketID=2}
             };

             var items = new List<Item>
             {
                 new Item{Title = "Chineese Soup", TicketID=1, OrderID = 1},
                 new Item{Title = "Chineese Appetizer", TicketID=2, OrderID=2},
                 new Item{Title = "Chineese Dessert", TicketID=1, OrderID=2},

                 new Item{Title ="Italian Soup", TicketID=1, OrderID = 1},
                 new Item{Title="Italian Appetizer",TicketID=1 , OrderID=2},
                 new Item{Title="Italian Dessert", TicketID=2, OrderID=1}
             };

             var tickets = new List<Ticket>
             {
                 new Ticket{Status = "READY"},
                 new Ticket{Status = "CANCELLED"}
             };

            UnitOfWork unitOfWork = new UnitOfWork();

            foreach (Ticket s in tickets)
                unitOfWork.TicketRepository.Insert(s);
            unitOfWork.Save();

            orders.ForEach(s => unitOfWork.OrderRepository.Insert(s));
            unitOfWork.Save();

            items.ForEach(s => unitOfWork.ItemRepository.Insert(s));
            unitOfWork.Save();
            */

        }
    }
}