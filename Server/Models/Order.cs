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
        public double InitialPrice { get; set; }
        public int DiscountID { get; set; }
        //public virtual Discount Discount { get; set; }

        public ICollection<Item> ItemObjects { get; set; }
         

    }
}