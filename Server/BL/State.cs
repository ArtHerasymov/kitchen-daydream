using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public abstract class State
    {
        public double amount;
       public abstract void NextState(Discount context);
    }

    class RegularDiscount : State
    {
        public double amount = 0.1;
        public override void NextState(Discount context)
        {
            if (context.Balance > 500 && context.Balance < 10000)
            {
                context.Upgrage(new EnhancedDiscount());
                context.Status = "Enhanced";
            }
            else if (context.Balance >= 10000)
            {
                context.Upgrage(new VIPDiscount());
                context.Status = "VIP";
            }
        }
    }

    class EnhancedDiscount : State
    {
        public double amount = 0.2;
        public override void NextState(Discount context)
        {
            if (context.Balance > 10000)
            {
                context.Upgrage(new VIPDiscount());
                context.Status = "VIP"; 
            }
        }
    }

    class VIPDiscount : State
    {
        public double amount = 0.33;
        public override void NextState(Discount context)
        {
            Console.WriteLine("Can't give you a better deal, sorry!");
        }
    }
}