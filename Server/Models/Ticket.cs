using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TicketID { get; set; }
        public string TicketType { get; set; }
        public string Deadline { get; set; }
        public string Status { get; set; }
        public string Chief { get; set; }
        public string Suchief { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}