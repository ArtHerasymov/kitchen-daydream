using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Order
    {
        public string Id { set; get; }
        public string Waiter { set; get; }
        public double Payment { set; get; }
        public List <String> Items { set; get; }
    }
}