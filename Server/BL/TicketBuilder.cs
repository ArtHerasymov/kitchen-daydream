using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;
using Server.DAL;
using System.Threading.Tasks;
using System.Threading;

namespace Server.BL
{
    public interface ITicketBuilder
    {
        void SetTicketType();
        void AssignChiefs();
        void CalculateFinalPrice();
        void CalculateDeadline();
        void DecodeTitles();
        void GenerateTicketId();
        Task InitiateCookingAsync();
    
        Ticket GetTicket();
    }

    public class ChineeseBuilder : ITicketBuilder
    {
        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket;
        Order order;
        UnitOfWork unit = new UnitOfWork();

        public ChineeseBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
            this.ticket = new Ticket();
        }
   
        public void SetTicketType()
        {
            this.ticket.TicketType = "Chineese";
        }

        public void AssignChiefs()
        {
            ticket.Chief = "Kim Yin";
            ticket.Suchief = "Yao Min";
        }

        public void CalculateDeadline()
        {
            ticket.Deadline = DateTime.Now.AddHours(1).ToString();
        }

        public void CalculateFinalPrice()
        {
            Accounting accounting = new Accounting();
            UnitOfWork unit = new UnitOfWork();
        
            Discount discount = (Discount)unit.DiscountRepository.GetByID(order.DiscountID);
            switch (discount.Status)
            {
                case "RegularDiscount":
                    discount.Upgrage(new RegularDiscount());
                    break;
                case "EnhancedDiscount":
                    discount.Upgrage(new EnhancedDiscount());
                    break;
                case "VIPDiscount":
                    discount.Upgrage(new VIPDiscount());
                    break;
            }

            switch (order.Locale)
            {
                case "USA":
                    accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
                    break;
                case "EU":
                    accounting.SetCurrentSubsidiary(new EuropeanTaxPolicy());
                    break;
            }

            ticket.FinalPrice = order.InitialPrice + accounting.GetSalesTax(order.InitialPrice, this.ticket.TicketType) - discount.DetermineDiscountAmount() * order.InitialPrice;
            discount.Balance += ticket.FinalPrice;

            discount.State.NextState(discount);
            unit.Save();
        }

        public void GenerateTicketId()
        {
            ticket.TicketID = "CHI" + new Random().Next();
        }

        public void DecodeTitles()
        {
            foreach(string item in items)
            {
                Item i = new Item();
                i.Status = "IN_PROGRESS";

                if (item == "1")
                {
                    i.Title = "Chineese_Soup";
                    i.Code = 1;
                } else if(item == "2")
                {
                    i.Title = "Chineese_Noodles";
                    i.Code = 2;
                } else
                {
                    i.Title = "Chineese_Dessert";
                    i.Code = 3;
                }
                i.TicketID = ticket.TicketID;
                i.OrderID = order.OrderID;
                itemObjects.Add(i);
            }
            ticket.Items = itemObjects;
            unit.Save();
        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }

        public async Task InitiateCookingAsync()
        {
            var reports = new List<Task<string>>();
            foreach (Item item in ticket.Items)
            {
                CookingFactory factory;
                ICourse course = null;
                switch (item.Code)
                {
                    case 1:
                        factory = new ChineeseFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 2:
                        factory = new ChineeseFactory();
                        course = factory.CreateAppetizerCourse();
                        break;
                    case 3:
                        factory = new ChineeseFactory();
                        course = factory.CreateDessertCourse();
                        break;
                    case 4:
                        factory = new ItalianFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 5:
                        factory = new ItalianFactory();
                        course = factory.CreateAppetizerCourse();
                        break;
                    case 6:
                        factory = new ItalianFactory();
                        course = factory.CreateDessertCourse();
                        break;
                }
          
                Thread thread = new Thread(() => course.CookAsync(item));
                thread.Start();
            }
        }
    }

    public class ItalianBuilder : ITicketBuilder
    {

        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        double finalPrice;
        string type;
        Order order;

        public ItalianBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
        }
      
        public void SetTicketType()
        {
            this.ticket.TicketType = "Italian";
        }
        public void AssignChiefs()
        {
            ticket.Chief = "Janne Papuo";
            ticket.Suchief = "Quentin Tarantino";
        }

        public void CalculateDeadline()
        {
            ticket.Deadline = DateTime.Now.AddHours(0.5).ToString();
        }

        public void CalculateFinalPrice()
        {
            Accounting accounting = new Accounting();
            UnitOfWork unit = new UnitOfWork();

            Discount discount = (Discount)unit.DiscountRepository.GetByID(order.DiscountID);
            switch (discount.Status)
            {
                case "Regular":
                    discount.Upgrage(new RegularDiscount());
                    break;
                case "Enhanced":
                    discount.Upgrage(new EnhancedDiscount());
                    break;
                case "VIPDiscount":
                    discount.Upgrage(new VIPDiscount());
                    break;
            }

            switch (order.Locale)
            {
                case "USA":
                    accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
                    break;
                case "EU":
                    accounting.SetCurrentSubsidiary(new EuropeanTaxPolicy());
                    break;
            }

            ticket.FinalPrice = order.InitialPrice + accounting.GetSalesTax(order.InitialPrice, this.ticket.TicketType) - discount.DetermineDiscountAmount() * order.InitialPrice;
            discount.Balance += ticket.FinalPrice;

            discount.State.NextState(discount);
            unit.Save();
        }

        public void DecodeTitles()
        {
            foreach (string item in items)
            {
                Item i = new Item();
                i.Status = "IN_PROGRESS";

                if (item == "4")
                {
                    i.Title = "Italian_Soup";
                    i.Code = 4;
                }
                else if (item == "5")
                {
                    i.Title = "Italian_Spaghetti";
                    i.Code = 5;
                }
                else
                {
                    i.Title = "Italian_Dessert";
                    i.Code = 6;
                }
                i.TicketID = ticket.TicketID;
                i.OrderID = order.OrderID;
                itemObjects.Add(i);
            }
            ticket.Items = itemObjects;
        }

        public void GenerateTicketId()
        {
            ticket.TicketID = "ITA" + new Random().Next();
        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }

        public async Task InitiateCookingAsync()
        {
            var reports = new List<Task<string>>();
            foreach (Item item in ticket.Items)
            {
                CookingFactory factory;
                ICourse course = null;
                switch (item.Code)
                {
                    case 1:
                        factory = new ChineeseFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 2:
                        factory = new ChineeseFactory();
                        course = factory.CreateAppetizerCourse();
                        break;
                    case 3:
                        factory = new ChineeseFactory();
                        course = factory.CreateDessertCourse();
                        break;
                    case 4:
                        factory = new ItalianFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 5:
                        factory = new ItalianFactory();
                        course = factory.CreateAppetizerCourse();
                        break;
                    case 6:
                        factory = new ItalianFactory();
                        course = factory.CreateDessertCourse();
                        break;
                }

                Thread thread = new Thread(() => course.CookAsync(item));
                thread.Start();
            }
        }
    }

    public class MixedBuilder : ITicketBuilder
    {
        String[] items;
        List<Item> itemObjects = new List<Item>();
        Ticket ticket = new Ticket();
        double finalPrice;
        string type;
        Order order;
    
        public void SetTicketType()
        {
            this.type = "MIXED";
        }

        public MixedBuilder(Order order)
        {
            this.order = order;
            this.items = order.Items.Split(',');
        }

        public void AssignChiefs()
        {
            ticket.Chief = "Johny Depp";
            ticket.Suchief = "Frank Sinatra";
        }

        public void CalculateDeadline()
        {
            ticket.Deadline = DateTime.Now.AddHours(1.5).ToString();
        }

        public void CalculateFinalPrice()
        {
            Accounting accounting = new Accounting();
            UnitOfWork unit = new UnitOfWork();

            Discount discount = (Discount)unit.DiscountRepository.GetByID(order.DiscountID);
            switch (discount.Status)
            {
                case "Regular":
                    discount.Upgrage(new RegularDiscount());
                    break;
                case "Enhanced":
                    discount.Upgrage(new EnhancedDiscount());
                    break;
                case "VIPDiscount":
                    discount.Upgrage(new VIPDiscount());
                    break;
            }

            switch (order.Locale)
            {
                case "USA":
                    accounting.SetCurrentSubsidiary(new AmericanTaxPolicy());
                    break;
                case "EU":
                    accounting.SetCurrentSubsidiary(new EuropeanTaxPolicy());
                    break;
            }

            ticket.FinalPrice = order.InitialPrice + accounting.GetSalesTax(order.InitialPrice, this.ticket.TicketType) - discount.DetermineDiscountAmount() * order.InitialPrice;
            discount.Balance += ticket.FinalPrice;

            discount.State.NextState(discount);
            unit.Save();
        }

        public void DecodeTitles()
        {
            foreach (string item in items)
            {
                Item i = new Item();
                i.Status = "IN_PROGRESS";

                if (item == "1")
                {
                    i.Title = "Chineese_Soup";
                    i.Code = 1;
                }
                else if (item == "2")
                {
                    i.Title = "Chineese_Noodles";
                    i.Code = 2;
                }
                else if(item == "3")
                {
                    i.Title = "Chineese_Dessert";
                    i.Code = 3;
                }
                else if (item == "4")
                {
                    i.Title = "Italian_Soup";
                    i.Code = 4;
                }
                else if (item == "5")
                {
                    i.Title = "Italian_Spaghetti";
                    i.Code = 5;
                }
                else
                {
                    i.Title = "Italian_Dessert";
                    i.Code = 6;
                }
                i.TicketID = ticket.TicketID;
                i.OrderID = order.OrderID;
                itemObjects.Add(i);
            }
            ticket.Items = itemObjects;
        }

        public void GenerateTicketId()
        {
            ticket.TicketID = "MIX" + new Random().Next();

        }

        public Ticket GetTicket()
        {
            return this.ticket;
        }

        public async Task InitiateCookingAsync()
        {
            var reports = new List<Task<string>>();
            foreach (Item item in ticket.Items)
            {
                CookingFactory factory;
                ICourse course = null;
                switch (item.Code)
                {
                    case 1:
                        factory = new ChineeseFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 2:
                        factory = new ChineeseFactory();
                        course = factory.CreateAppetizerCourse();
                        break;
                    case 3:
                        factory = new ChineeseFactory();
                        course = factory.CreateDessertCourse();
                        break;
                    case 4:
                        factory = new ItalianFactory();
                        course = factory.CreateMainCourse();
                        break;
                    case 5:
                        factory = new ItalianFactory();
                        course = factory.CreateAppetizerCourse();
                        break;
                    case 6:
                        factory = new ItalianFactory();
                        course = factory.CreateDessertCourse();
                        break;
                }

                Thread thread = new Thread(() => course.CookAsync(item));
                thread.Start();
            }
        }
    }

    public class Dispatcher
    {
        public Ticket BuildTicket(ITicketBuilder _builder)
        {
            _builder.SetTicketType();
            _builder.AssignChiefs();
            _builder.CalculateFinalPrice();
            _builder.CalculateDeadline();
            _builder.GenerateTicketId();
            _builder.DecodeTitles();
            _builder.InitiateCookingAsync();

            return _builder.GetTicket();
        }
    }
}