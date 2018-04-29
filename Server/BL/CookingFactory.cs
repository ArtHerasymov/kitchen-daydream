using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Server.Models;
using System.Threading.Tasks;
using Server.DAL;

namespace Server.BL
{

    interface ICourse
    {
        void CookAsync(Item item);
    }

    public abstract class CookingFactory
    {
        public abstract DessertCourse CreateDessertCourse();
        public abstract MainCourse CreateMainCourse();
        public abstract AppetizerCourse CreateAppetizerCourse();
    }

    public class ChineeseFactory : CookingFactory

    {
        public override AppetizerCourse CreateAppetizerCourse()
        {
            return new ChineeseAppetizerCourse();
        }

        public override DessertCourse CreateDessertCourse()
        {
            return new ChineeseDessertCourse();
        }

        public override MainCourse CreateMainCourse()
        {
            return new ChineeseMainCourse();
        }
    }

    public class ItalianFactory : CookingFactory
    {
        public override AppetizerCourse CreateAppetizerCourse()
        {
            return new ItalianAppetizerCourse();
        }

        public override DessertCourse CreateDessertCourse()
        {
            return new ItalianDessertCourse();
        }

        public override MainCourse CreateMainCourse()
        {
            return new ItalianMainCourse();
        }
    }


    public abstract class MainCourse : ICourse
    {
        public abstract void  CookAsync(Item item);
    }
    public abstract class DessertCourse : ICourse
    {
        public abstract void CookAsync(Item item);
    }
    public abstract class AppetizerCourse : ICourse
    {
        public abstract void CookAsync(Item item);
    }

    public class ChineeseMainCourse : MainCourse
    {
        private int TimeOfPreperation = 6000;
        UnitOfWork u = new UnitOfWork();

        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(TimeOfPreperation);

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }

    public class ChineeseDessertCourse : DessertCourse
    {
        private int TimeOfPreperation = 6000;
        UnitOfWork u = new UnitOfWork();
        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(TimeOfPreperation);

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }

    }

    public class ChineeseAppetizerCourse : AppetizerCourse
    {
        private int TimeOfPreperation = 6000;
        UnitOfWork u = new UnitOfWork();
        public override void CookAsync(Item item)
        {
            Thread.Sleep(TimeOfPreperation);

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }

    public class ItalianMainCourse : MainCourse
    {
        UnitOfWork u = new UnitOfWork();

        private int TimeOfPreperation = 5000;
        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(TimeOfPreperation);

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }
    public class ItalianDessertCourse : DessertCourse
    {
        UnitOfWork u = new UnitOfWork();

        private int TimeOfPreperation = 6100;

        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(TimeOfPreperation);

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }
    public class ItalianAppetizerCourse : AppetizerCourse
    {
        UnitOfWork u = new UnitOfWork();

        private int TimeOfPreperation = 7400;

        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(TimeOfPreperation);

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }
}