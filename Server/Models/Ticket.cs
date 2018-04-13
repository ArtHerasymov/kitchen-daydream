using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.Orders = new HashSet<Order>();
            this.Items = new HashSet<Item>();
        }

        public int TicketID { get; set; }
        public string TicketType { get; set; }
        public string Deadline { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}