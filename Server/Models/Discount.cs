﻿using System;
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
        private State State { get; set; }

        public void Upgrage(State state)
        {
            State = state;
        }

        public double DetermineDiscountAmount()
        {
            if (State is RegularDiscount)
                return 0.1;
            else if (State is EnhancedDiscount)
                return 0.2;
            else 
                return 0.25;
        
        }

    }
}