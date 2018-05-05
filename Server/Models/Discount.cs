using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Server.BL;
using Server.DAL;

namespace Server.Models
{
    public class Discount : AccessProxy
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

        public void TranscribeState()
        {
            switch (Status)
            {
                case "RegularDiscount":
                    this.State = new RegularDiscount();
                    break;
                case "EnhancedDiscount":
                    this.State = new EnhancedDiscount();
                    break;
                case "VIP":
                    this.State = new VIPDiscount();
                    break;
            }
        }

        public double DetermineDiscountAmount()
        {
            return (int)this.State.GetAmount() / 100.0;   
        }

        public override void AccountDiscount(UnitOfWork unitOfWork, int ?id)
        {
            unitOfWork.DiscountRepository.Insert(this);
            unitOfWork.Save();
        }

        public override double GetBalance()
        {
            return this.Balance;
        }
    }
}