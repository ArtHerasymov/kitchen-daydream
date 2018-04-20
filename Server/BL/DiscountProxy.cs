using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;
using Server.DAL;

namespace Server.BL
{
    public abstract class AccessProxy
    {
        public abstract void AccountDiscount(UnitOfWork unitOfWork, int id);
        public abstract double GetBalance();
    }

    public class DiscountProxy : AccessProxy
    {
        private Discount _realObject;
        UnitOfWork unitOfWork;

        public override void AccountDiscount(UnitOfWork unitOfWork, int id)
        {
            this.unitOfWork = unitOfWork;
            if (unitOfWork.DiscountRepository.GetByID(id) == null)
            {
                _realObject = new Discount();
                _realObject.DiscountID = id;
                _realObject.Status = "Regular";
                unitOfWork.DiscountRepository.Insert(_realObject);
                unitOfWork.Save();
            }
        }

        public override double GetBalance()
        {
            return _realObject.Balance;
        }

    }
}
