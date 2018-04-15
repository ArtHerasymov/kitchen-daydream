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
        public abstract Discount AccessObject(UnitOfWork unitOfWork, int id);
    }
    public class DiscountProxy : AccessProxy
    {
        public Discount _realObject;
        UnitOfWork unitOfWork;
        Boolean isDuplicate;

        public override Discount AccessObject(UnitOfWork unitOfWork, int id)
        {
            this.unitOfWork = unitOfWork;
            if (unitOfWork.DiscountRepository.GetByID(id) == null)
            {
                isDuplicate = false;
                _realObject = new Discount();
                _realObject.DiscountID = id;
                _realObject.Status = "Regular";
                unitOfWork.DiscountRepository.Insert(_realObject);
                return _realObject;
            }
            else
            {
                isDuplicate = true;
                return unitOfWork.DiscountRepository.GetByID(id);
            }
        }

     
    }
}
