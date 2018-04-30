using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Server.Models;
using System.Threading.Tasks;
using Server.DAL;
using System.IO;
using Newtonsoft.Json;

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
        public abstract SoupCourse CreateSoupCourse();
    }

    public class ChineeseFactory : CookingFactory

    {
        public override SoupCourse CreateSoupCourse()
        {
            return new ChineeseSoupCourse();
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
        public override SoupCourse CreateSoupCourse()
        {
            return new ItalianSoupCourse();
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
    public abstract class SoupCourse : ICourse
    {
        public abstract void CookAsync(Item item);
    }


    public class ChineeseMainCourse : MainCourse
    {

        UnitOfWork u = new UnitOfWork();
        TechnologyReference reference = new TechnologyReference();
        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(reference.GetChineeseMainTime());

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }
    public class ChineeseDessertCourse : DessertCourse
    {
        TechnologyReference reference = new TechnologyReference();
        UnitOfWork u = new UnitOfWork();
        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(reference.GetChineeseDessertTime());

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }

    }
    public class ChineeseSoupCourse : SoupCourse
    {
        TechnologyReference reference = new TechnologyReference();
        UnitOfWork u = new UnitOfWork();
        public override void CookAsync(Item item)
        {
            Thread.Sleep(reference.GetChineeseSoupTime());

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }

    public class ItalianMainCourse : MainCourse
    {
        UnitOfWork u = new UnitOfWork();
        TechnologyReference reference = new TechnologyReference();
        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(reference.GetItalianMainTime());

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }
    public class ItalianDessertCourse : DessertCourse
    {
        UnitOfWork u = new UnitOfWork();
        TechnologyReference reference = new TechnologyReference();

        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(reference.GetItalianDessertTime());

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }
    public class ItalianSoupCourse : SoupCourse
    {
        UnitOfWork u = new UnitOfWork();
        TechnologyReference reference = new TechnologyReference();

        public override void CookAsync(Item item)
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.Sleep(reference.GetItalianSoupTime());

            item.ChangeStatus();
            u.ItemRepository.Update(item);
            u.Save();
        }
    }

    public class TechnologyReference
    {
        dynamic deserializedValue;
        public TechnologyReference()
        {
            string contents = File.ReadAllText(@"E:\CookingTechnologyAPI.json");
            deserializedValue = JsonConvert.DeserializeObject(contents);
        }

        public int GetChineeseSoupTime()
        {
            return deserializedValue["TimeOfPreperation"]["ChineesePreperation"]["LotusRootSoup"];
        }
        public int GetChineeseMainTime()
        {
            return deserializedValue["TimeOfPreperation"]["ChineesePreperation"]["Noodles"];
        }
        public int GetChineeseDessertTime()
        {
            return deserializedValue["TimeOfPreperation"]["ChineesePreperation"]["StrawberryKuchi"];
        }
        public int GetItalianSoupTime()
        {
            return deserializedValue["TimeOfPreperation"]["ItalianPreperation"]["RoastedSoup"];
        }
        public int GetItalianMainTime()
        {
            return deserializedValue["TimeOfPreperation"]["ItalianPreperation"]["Carbonara"];
        }
        public int GetItalianDessertTime()
        {
            return deserializedValue["TimeOfPreperation"]["ItalianPreperation"]["Pancakes"];
        }



    }
}