using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public string TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }
        public String Status { get; set; }

        public Nullable<int> OrderID { get; set; }
        public virtual Order Order { get; set; }


        public Boolean ChangeStatus()
        {
            this.Status = "READY";
            return true;
        }
    }
}