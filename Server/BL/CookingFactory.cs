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
        Task<string> CookAsync(Item item);
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
        public abstract Task<string> CookAsync(Item item);
    }
    public abstract class DessertCourse : ICourse
    {
        public abstract Task<string> CookAsync(Item item);
    }
    public abstract class AppetizerCourse : ICourse
    {
        public abstract Task<string> CookAsync(Item item);
    }

    public class ChineeseMainCourse : MainCourse
    {
        private int TimeOfPreperation = 500;
        UnitOfWork u = new UnitOfWork();

        public override async Task<string> CookAsync(Item item)
        {
            await Task.Delay(TimeOfPreperation);
            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
            return "Finished";
        }
    }
    public class ChineeseDessertCourse : DessertCourse
    {
        private int TimeOfPreperation = 600;
        UnitOfWork u = new UnitOfWork();
        public override async Task<string> CookAsync(Item item)
        {
            await Task.Delay(TimeOfPreperation);
            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
            return "Finished";
        }

    }
    public class ChineeseAppetizerCourse : AppetizerCourse
    {
        private int TimeOfPreperation = 700;
        UnitOfWork u = new UnitOfWork();
        public override async Task<string> CookAsync(Item item)
        {
            await Task.Delay(TimeOfPreperation);
            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
            return "Finished";
        }
    }

    public class ItalianMainCourse : MainCourse
    {
        private int TimeOfPreperation = 100;
        public override async Task<string> CookAsync(Item item)
        {
            await Task.Delay(100);
            item.ChangeStatus();
            return "Finished";
        }
    }
    public class ItalianDessertCourse : DessertCourse
    {
        private int TimeOfPreperation = 250;
    
        public override async Task<string> CookAsync(Item item)
        {
            await Task.Delay(100);
            item.ChangeStatus();
            return "Finished";
        }
    }
    public class ItalianAppetizerCourse : AppetizerCourse
    {
        private int TimeOfPreperation = 310;

        public override async Task<string> CookAsync(Item item)
        {
            await Task.Delay(100);
            item.ChangeStatus();
            return "Finished";
        }
    }
}