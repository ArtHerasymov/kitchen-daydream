using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public abstract class State
    {
       public abstract void NextState(Discount context);
    }

    class RegularDiscount : State
    {
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
        public override void NextState(Discount context)
        {
            // Can't give you a better deal(yet)
        }
    }


}