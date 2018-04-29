 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Discount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiscountID { get; set; }
        public double Balance { get; set; }
        public String Status { get; set; }
        [NotMapped]
        public State State { get; set; }

        public void Upgrage(State state)
        {
            State = state;
            Status = State.ToString().Replace("Server.Models.","");
        }

        public double DetermineDiscountAmount()
        {
            return this.State.amount;      
        }

    }
}