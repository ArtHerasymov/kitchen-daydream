using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string Waiter { get; set; }
        public string Status { get; set; }

        public Nullable<int> TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        

    }
}