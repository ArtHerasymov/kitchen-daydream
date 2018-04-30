using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public enum Discounts {REGULAR= 10, ENHANCED = 20, VIP = 33}

    public abstract class State
    {
        public abstract void NextState(Discount context);
        public abstract Discounts GetAmount();
    }

    class RegularDiscount : State
    {
        public override Discounts GetAmount()
        {
            return Discounts.REGULAR;
        }

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
        public override Discounts GetAmount()
        {
            return Discounts.ENHANCED;
        }

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
        public override Discounts GetAmount()
        {
            return Discounts.VIP;
        }

        public override void NextState(Discount context)
        {
            Console.WriteLine("Can't give you a better deal, sorry!");
        }
    }
}