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
        public string Items { get; set; }

        public ICollection<Item> ItemObjects { get; set; }
         

    }
}