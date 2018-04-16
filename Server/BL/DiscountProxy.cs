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
        public Discount _realObject;
        UnitOfWork unitOfWork;
        Boolean isDuplicate;

        public override void AccountDiscount(UnitOfWork unitOfWork, int id)
        {
            this.unitOfWork = unitOfWork;
            if (unitOfWork.DiscountRepository.GetByID(id) == null)
            {
                isDuplicate = false;
                _realObject = new Discount();
                _realObject.DiscountID = id;
                _realObject.Status = "Regular";
                unitOfWork.DiscountRepository.Insert(_realObject);
                unitOfWork.Save();
            }
            else
            {
                isDuplicate = true;
            }
        }



        public override double GetBalance()
        {
            return _realObject.Balance;
        }

    }
}
