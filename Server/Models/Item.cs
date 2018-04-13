using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Item
    {

        public int ItemID { get; set; }
        public string Title { get; set; }
        public Nullable<int> TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }

        public Nullable<int> OrderID { get; set; }
        public virtual Order Order { get; set; }
    }
}